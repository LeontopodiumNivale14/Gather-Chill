using Dalamud.Game.ClientState.Objects.SubKinds;
using ECommons.GameHelpers;
using FFXIVClientStructs.FFXIV.Client.Game;

namespace GatherChill.Utilities.Utility;

public static partial class Utils
{
    public static unsafe void MountAction()
    {
        /*
        // bool useMount = C.MountId != 0 && PlayerState.Instance()->IsMountUnlocked(C.MountId);

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
        */
        if (!Player.IsCasting && !Player.Mounting)
        {
            ActionManager.Instance()->UseAction(ActionType.GeneralAction, 9);
        }
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
        uint gp = LocalPlayer.CurrentGp;
        return (int)gp;
    }
    public static int MaxGp()
    {
        var maxGp = LocalPlayer.MaxGp;
        return (int)maxGp;
    }
}
