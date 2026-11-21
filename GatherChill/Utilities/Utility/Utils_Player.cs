using Dalamud.Game.ClientState.Objects.SubKinds;
using ECommons.ExcelServices;
using ECommons.GameHelpers;
using ECommons.Throttlers;
using FFXIVClientStructs.FFXIV.Client.Game;
using FFXIVClientStructs.FFXIV.Client.Game.UI;
using FFXIVClientStructs.FFXIV.Client.UI.Misc;
using GatherChill.Scheduler;
using GatherChill.Utilities.Tools;
using System.Collections.Generic;
using static ECommons.UIHelpers.AddonMasterImplementations.AddonMaster;
using static GatherChill.ConfigFiles.Config;
using static GatherChill.Ui.Tables.Table_Items;

namespace GatherChill.Utilities.Utility;

public static partial class Utils
{
    public static unsafe void MountAction()
    {
        bool useMount = C.MountId != 0 && PlayerState.Instance()->IsMountUnlocked(C.MountId);

        if (!Player.IsCasting && !Player.Mounting)
        {
            if (useMount)
            {
                ActionManager.Instance()->UseAction(ActionType.Mount, C.MountId);
                IceLogging.Info($"Attempting to mount: {C.MountName}");
            }
            else
            {
                ActionManager.Instance()->UseAction(ActionType.GeneralAction, 9);
                IceLogging.Info($"Resorting to using the mount roulette");
            }
        }
        /*
        if (!Player.IsCasting && !Player.Mounting)
        {
            ActionManager.Instance()->UseAction(ActionType.GeneralAction, 9);
        }
        */
    }
    public static unsafe void Dismount()
    {
        if (Player.Mounted)
        {
            ActionManager.Instance()->UseAction(ActionType.GeneralAction, 9);
        }
    }
    private const uint EventItemStart = 2_000_000;
    internal static bool TryResolveQuality(uint itemId, ItemQuality quality, out bool hq)
    {
        hq = false;
        var hqCount = GetItemCount(itemId, includeHq: true, includeNq: false);
        var nqCount = GetItemCount(itemId, includeHq: false, includeNq: true);

        if (quality == ItemQuality.HQOnly)
        {
            hq = true;
            return hqCount != 0;
        }

        if (quality == ItemQuality.NQOnly)
            return nqCount != 0;

        // PreferHQ
        if (hqCount != 0)
        {
            hq = true;
            return true;
        }

        return nqCount != 0;
    }
    public static unsafe int GetItemCount(uint itemID, bool includeHq = true, bool includeNq = true)
    {
        var inventory = InventoryManager.Instance();
        if (inventory == null)
            return 0;

        if (itemID >= EventItemStart)
            return inventory->GetItemCountInContainer(itemID, InventoryType.KeyItems);

        itemID = itemID >= 1_000_000 ? itemID - 1_000_000 : itemID;

        var count = 0;
        if (includeHq)
            count += inventory->GetInventoryItemCount(itemID, true);
        if (includeNq)
            count += inventory->GetInventoryItemCount(itemID, false);

        count += inventory->GetInventoryItemCount(itemID + 500_000);
        return count;
    }
    public static IPlayerCharacter? LocalPlayer => Svc.Objects.LocalPlayer;
    public static bool HasStatusId(params uint[] statusIDs)
    {
        if (LocalPlayer == null)
            return false;

        var statusID = LocalPlayer.StatusList
            .Select(se => se.StatusId)
            .ToList().Intersect(statusIDs)
            .FirstOrDefault();

        return statusID != default;
    }
    internal static uint GetItemStatusId(uint itemId)
    {
        itemId = itemId >= 1_000_000 ? itemId - 1_000_000 : itemId;
        if (itemId == 0)
            return 0;

        var item = ExcelHelper.Sheet_Item[itemId];
        if (!item.ItemAction.IsValid)
            return 0;

        return item.ItemAction.Value.Data[0];
    }
    public static float? GetStatusRemaining(uint statusId)
    {
        var player = LocalPlayer;
        if (player == null)
            return null;

        foreach (var status in player.StatusList)
        {
            if (status.StatusId != statusId)
                continue;

            return status.RemainingTime;
        }

        return null;
    }
    public static int GetGp()
    {
        if (Player.Available)
        {
            uint gp = LocalPlayer.CurrentGp;
            if (Player.Job is Job.MIN or Job.BTN or Job.FSH)
                return (int)gp;
            else
                return 0;
        }
        else
        {
            return 0;
        }
    }
    public static int MaxGp()
    {
        if (Player.Available)
        {
            uint gp = LocalPlayer.MaxGp;
            if (Player.Job is Job.MIN or Job.BTN or Job.FSH)
                return (int)gp;
            else
                return 0;
        }
        else
        {
            return 0;
        }
    }
    public static unsafe bool IsItemEquipped(uint ItemId) => InventoryManager.Instance()->GetItemCountInContainer(ItemId, InventoryType.EquippedItems) > 0;
    public unsafe static void TaskClassChange(Job job)
    {
        string tag = "Task: Equip Gearset";

        if (job == Player.Job || !EzThrottler.Throttle("Gearset", 250) || Player.IsBusy)
            return;
        var gearsets = RaptureGearsetModule.Instance();
        foreach (ref var gs in gearsets->Entries)
        {
            if (!RaptureGearsetModule.Instance()->IsValidGearset(gs.Id)) continue;
            if ((Job)gs.ClassJob == job)
            {
                if (gs.Flags.HasFlag(RaptureGearsetModule.GearsetFlag.MainHandMissing))
                {
                    if (GenericHelpers.TryGetAddonMaster<SelectYesno>("SelectYesno", out var select) && select.IsAddonReady)
                    {
                        select.Yes();
                    }
                    else
                    {
                        gearsets->EquipGearset(gs.Id);
                    }
                }

                var result = gearsets->EquipGearset(gs.Id);
                IceLogging.Debug($"Tried to equip gearset {gs.Id} for {job}, result={result}, flags={gs.Flags}", tag);
                return;
            }
        }

        if (EzThrottler.Throttle("No gearsets"))
            IceLogging.Verbose($"Hewwo. We have gotten thiws faw, which means thawt the geawset fow {job.ToString()} doesn't exist. Pwease make owne", tag);
        return;
    }
    public static unsafe void TaskEquipItem(uint itemId)
    {
        string tag = "Task: Equip Item";
        var itemSheet = ExcelHelper.Sheet_Item[itemId];

        var pos = FindItemInInventory(itemId, [InventoryType.Inventory1, InventoryType.Inventory2, InventoryType.Inventory3, InventoryType.Inventory4, InventoryType.ArmoryMainHand, InventoryType.ArmoryHands]);
        if (pos == null)
        {
            IceLogging.ChatError($"Failed to find the item: {itemSheet.Name} [ID: {itemId}] in inventory", tag);
            SchedulerMain.Fail($"{itemSheet.Name} isn't in the inventory");
            return;
        }

        InventoryManager.Instance()->MoveItemSlot(pos.Value.inv, pos.Value.slot, InventoryType.EquippedItems, (ushort)(itemSheet.EquipSlotCategory.RowId - 1), true);
    }
    private static unsafe (InventoryType inv, ushort slot)? FindItemInInventory(uint ItemId, IEnumerable<InventoryType> inventories)
    {
        foreach (var inv in inventories)
        {
            var cont = InventoryManager.Instance()->GetInventoryContainer(inv);
            for (ushort i = 0; i < cont->Size; ++i)
            {
                if (cont->GetInventorySlot(i)->ItemId == ItemId)
                {
                    return (inv, i);
                }
            }
        }
        return null;
    }
    public static unsafe bool NeedsRepair()
    {
        string tag = "Needs Repair";
        float below = C.Repair_Amount;

        var im = InventoryManager.Instance();
        if (im == null)
        {
            IceLogging.Error("InventoryManager was null", tag);
            return false;
        }

        var equipped = im->GetInventoryContainer(InventoryType.EquippedItems);
        if (equipped == null)
        {
            IceLogging.Error("InventoryContainer was null", tag);
            return false;
        }

        if (!equipped->IsLoaded)
        {
            IceLogging.Error($"InventoryContainer is not loaded", tag);
            return false;
        }

        for (var i = 0; i < equipped->Size; i++)
        {
            var item = equipped->GetInventorySlot(i);
            if (item == null)
                continue;

            var itemCondition = Convert.ToInt32(Convert.ToDouble(item->Condition) / 30000.0 * 100.0);

            if (itemCondition <= below)
            {
                IceLogging.Debug($"Found an item that needed repair. Condition: {itemCondition}", tag);
                return true;
            }
        }

        return false;
    }
    public static unsafe int GetFreeSlotCount()
    {
        var manager = InventoryManager.Instance();
        if (manager == null)
            return 0;

        return (int)manager->GetEmptySlotsInBag();
    }
}
