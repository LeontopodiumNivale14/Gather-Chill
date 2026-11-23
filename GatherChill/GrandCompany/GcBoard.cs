using Dalamud.Game.Text.SeStringHandling;
using FFXIVClientStructs.FFXIV.Client.Game;
using FFXIVClientStructs.FFXIV.Client.Game.UI;
using FFXIVClientStructs.FFXIV.Client.System.String;
using FFXIVClientStructs.FFXIV.Client.UI;
using FFXIVClientStructs.FFXIV.Client.UI.Agent;
using FFXIVClientStructs.FFXIV.Component.GUI;
using Lumina.Excel;
using Lumina.Excel.Sheets;
using System.Collections.Generic;

namespace GatherChill.GrandCompany;

public enum EGcTab
{
    Supply,
    Provisioning,
    Expert,
}

public readonly record struct GcRequest(EGcTab Tab, uint ClassJob, uint ItemId, string ItemName, int Requested, int Exp, int Seals);

public readonly record struct GcRow(string ItemName, bool Greyed);

public static unsafe class GcBoard
{
    public const uint Miner = 16;
    public const uint Botanist = 17;
    private const uint Fisher = 18;

    private const int ResetHourUtc = 20;
    private const uint FirstCompanySeal = 20;
    private const uint RowNameNode = 6;
    private const string DutySheet = "GCSupplyDuty";
    private const string RewardSheet = "GCSupplyDutyReward";
    private const int DutyItems = 3;
    private const int DutyColumns = 2;
    private const int CrafterJobs = 8;
    private const int ProvisioningSealsColumn = 4;
    private const uint TimersSupply = 2;
    private const uint TimersProvisioning = 15;
    private const uint TimersFirstProvisioningRow = 17;
    private const int TimersProvisioningRows = 3;
    private const uint TimersRowName = 4;
    private const uint TimersRowRequested = 7;

    public static byte Company => PlayerState.Instance() != null ? PlayerState.Instance()->GrandCompany : (byte)0;

    public static uint CompanySeal => Company == 0 ? 0u : FirstCompanySeal + Company - 1u;

    public static bool Ready
    {
        get
        {
            var agent = AgentGrandCompanySupply.Instance();
            return agent != null && agent->IsAgentActive() && agent->SupplyProvisioningData != null;
        }
    }

    public static bool Supported(uint job) => job is Miner or Botanist;

    public static uint OfficerFor(byte company) => company switch
    {
        1 => 1002388u,
        2 => 1002394u,
        3 => 1002391u,
        _ => 0u,
    };

    public static List<GcRequest>? Read() => Read(out _);

    public static List<GcRequest>? Read(out string problem)
    {
        problem = string.Empty;
        var found = new List<GcRequest>();
        if (!Ready)
            return Unreadable("GC supply not ready", out problem);

        var items = AgentGrandCompanySupply.Instance()->SupplyProvisioningData->ProvisioningData;
        for (var i = 0; i < Math.Min(2, items.Length); i++)
        {
            ref var item = ref items[i];
            var job = Miner + (uint)i;
            if (!TryLevel(job, out var level))
                return Unreadable($"Can't read level for job {job}", out problem);

            var name = Clean(ref item.ItemName);
            if (level > 0 && (item.ItemId == 0 || item.NumRequested <= 0 || string.IsNullOrWhiteSpace(name)))
                return Unreadable($"Incomplete data for job {job}: item {item.ItemId}, '{name}' x{item.NumRequested}", out problem);

            if (item.ItemId != 0)
                found.Add(new GcRequest(EGcTab.Provisioning, job, item.ItemId, name, item.NumRequested, item.ExpReward, item.SealReward));
        }

        return found;
    }

    public static List<GcRow> Rows(AddonGrandCompanySupplyList* board)
    {
        var rows = new List<GcRow>();
        var list = board->SupplyProvisioningList;
        if (list == null)
            return rows;

        for (var i = 0; i < list->ListLength; i++)
        {
            var renderer = list->GetItemRenderer(i);
            var node = renderer == null ? null : renderer->AtkComponentButton.AtkComponentBase.UldManager.SearchNodeById(RowNameNode);
            var text = node == null ? null : node->GetAsAtkTextNode();
            rows.Add(new GcRow(text == null ? string.Empty : Clean(ref text->NodeText), list->GetItemDisabledState(i)));
        }

        return rows;
    }

    public static int RowOf(IReadOnlyList<GcRow> rows, string itemName)
    {
        for (var i = 0; i < rows.Count; i++)
        {
            if (SameName(rows[i].ItemName, itemName))
                return i;
        }

        return -1;
    }

    public static List<GcRequest>? FromTimers(AtkUnitBase* detail) => FromTimers(detail, out _);

    public static List<GcRequest>? FromTimers(AtkUnitBase* detail, out string problem)
    {
        problem = string.Empty;
        if (detail == null)
            return Unreadable("Timers detail closed", out problem);

        var supply = detail->GetNodeById(TimersSupply);
        var provisioning = detail->GetNodeById(TimersProvisioning);
        if (supply == null || provisioning == null || !supply->IsVisible() || !provisioning->IsVisible())
            return Unreadable($"Timers headings missing (supply {Visibility(supply)}, provisioning {Visibility(provisioning)})", out problem);

        var found = new List<GcRequest>();
        for (var i = 0u; i < TimersProvisioningRows; i++)
        {
            var rowId = TimersFirstProvisioningRow + i;
            var row = detail->GetNodeById(rowId);
            if (row == null)
                return Unreadable($"Timers row {rowId} missing", out problem);

            if (!row->IsVisible())
                continue;

            var component = row->GetComponent();
            if (component == null)
                return Unreadable($"Timers row {rowId} unreadable", out problem);

            var name = TextOf(component, TimersRowName);
            var requestedText = TextOf(component, TimersRowRequested);
            if (!int.TryParse(requestedText, out var requested) || requested <= 0)
                return Unreadable($"Timers row {rowId}: bad amount '{requestedText}' for '{name}'", out problem);

            var matches = Match(name, requested);
            if (matches.Count != 1)
                return Unreadable($"Timers row {rowId}: '{name}' matched {matches.Count} items ({GatheringLevels()})", out problem);

            var request = matches[0];
            if (!Supported(request.ClassJob))
                continue;

            if (found.Any(x => x.ClassJob == request.ClassJob))
                return Unreadable($"Timers row {rowId}: duplicate job {request.ClassJob}", out problem);

            found.Add(request);
        }

        return found;
    }

    public static int Held(uint itemId)
    {
        var bags = InventoryManager.Instance();
        return bags == null ? 0 : bags->GetInventoryItemCount(itemId) + bags->GetInventoryItemCount(itemId, true);
    }

    public static DateTime LastResetUtc(DateTime utcNow)
    {
        var today = utcNow.Date.AddHours(ResetHourUtc);
        return utcNow >= today ? today : today.AddDays(-1);
    }

    private static List<GcRequest>? Unreadable(string reason, out string problem)
    {
        problem = reason;
        return null;
    }

    private static string Visibility(AtkResNode* node) => node == null ? "missing" : node->IsVisible() ? "visible" : "hidden";

    private static string GatheringLevels()
        => string.Join(", ", new[] { Miner, Botanist, Fisher }.Select(job => TryLevel(job, out var level) ? $"job {job} level {level}" : $"job {job} level unavailable"));

    private static List<GcRequest> Match(string shown, int requested)
    {
        var found = new List<GcRequest>();
        var duty = Svc.Data.GetExcelSheet<RawRow>(name: DutySheet);
        var items = Svc.Data.GetExcelSheet<Item>();

        for (var job = Miner; job <= Fisher; job++)
        {
            if (!TryLevel(job, out var level) || level <= 0)
                continue;

            var column = (CrafterJobs + (int)(job - Miner)) * DutyItems * DutyColumns;
            var seen = new HashSet<uint>();
            for (var dutyLevel = 1; dutyLevel <= level; dutyLevel++)
            {
                if (!duty.TryGetRow((uint)dutyLevel, out var row))
                    continue;

                for (var i = 0; i < DutyItems; i++)
                {
                    var id = (uint)row.ReadInt32Column(column + (i * DutyColumns));
                    if (id == 0 || !seen.Add(id) || !items.TryGetRow(id, out var item))
                        continue;

                    var name = item.Name.ExtractText();
                    if (SameName(shown, name))
                        found.Add(new GcRequest(EGcTab.Provisioning, job, id, name, requested, 0, SealsFor(level)));
                }
            }
        }

        var exact = found.Where(x => string.Equals(shown.Trim(), x.ItemName, StringComparison.OrdinalIgnoreCase)).ToList();
        return exact.Count > 0 ? exact : found;
    }

    private static int SealsFor(int level)
        => Svc.Data.GetExcelSheet<RawRow>(name: RewardSheet).TryGetRow((uint)level, out var row)
            ? (int)row.ReadUInt32Column(ProvisioningSealsColumn)
            : 0;

    private static bool TryLevel(uint job, out int level)
    {
        level = 0;
        var player = PlayerState.Instance();
        if (player == null || !Svc.Data.GetExcelSheet<ClassJob>().TryGetRow(job, out var row) || row.ExpArrayIndex < 0)
            return false;

        level = player->ClassJobLevels[row.ExpArrayIndex];
        return true;
    }

    private static string TextOf(AtkComponentBase* component, uint nodeId)
    {
        var node = component->UldManager.SearchNodeById(nodeId);
        var text = node == null ? null : node->GetAsAtkTextNode();
        return text == null ? string.Empty : Clean(ref text->NodeText);
    }

    private static bool SameName(string shown, string name)
    {
        var bare = shown.Trim().TrimEnd('.', '…');
        return bare.Length > 0 && name.StartsWith(bare, StringComparison.OrdinalIgnoreCase);
    }

    private static string Clean(ref Utf8String text) => SeString.Parse(text.AsSpan()).TextValue;
}
