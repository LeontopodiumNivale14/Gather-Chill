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
    public static unsafe bool GetItemCount(uint itemID, out int count, bool includeHq = true, bool includeNq = true)
    {
        try
        {
            itemID = itemID >= 1_000_000 ? itemID - 1_000_000 : itemID;
            count = 0;
            if (includeHq)
                count += InventoryManager.Instance()->GetInventoryItemCount(itemID, true);
            if (includeNq)
                count += InventoryManager.Instance()->GetInventoryItemCount(itemID, false);
            count += InventoryManager.Instance()->GetInventoryItemCount(itemID + 500_000);
            return true;
        }
        catch
        {
            count = 0;
            return false;
        }
    }
    public static unsafe int GetItemCount(uint itemID, bool includeHq = true, bool includeNq = true)
    {
        var inventory = InventoryManager.Instance();
        if (inventory == null)
            return 0;

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
            SchedulerMain.DisablePlugin();
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
}
