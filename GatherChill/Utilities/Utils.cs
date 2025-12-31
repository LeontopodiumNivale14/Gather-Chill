using Dalamud.Game.ClientState.Objects.Types;
using ECommons.Automation.NeoTaskManager;
using ECommons.DalamudServices.Legacy;
using ECommons.GameHelpers;
using ECommons.Reflection;
using ECommons.Throttlers;
using FFXIVClientStructs.FFXIV.Client.Game;
using FFXIVClientStructs.FFXIV.Client.Game.Control;
using FFXIVClientStructs.FFXIV.Client.Game.Object;
using FFXIVClientStructs.FFXIV.Client.Game.UI;
using Lumina.Excel.Sheets;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace GatherChill.Utilities;

public static class Utils
{
    public static bool HasPlugin(string name) => DalamudReflector.TryGetDalamudPlugin(name, out _, false, true);
    public static TaskManagerConfiguration TaskConfig => new(timeLimitMS: 10 * 60 * 3000, abortOnTimeout: false);
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
    public static void TargetgameObject(IGameObject? gameObject)
    {
        var x = gameObject;
        var currentTarget = Svc.Targets.Target;
        if (currentTarget != null && currentTarget.BaseId == x.BaseId)
            return;

        if (!GenericHelpers.IsOccupied())
        {
            if (x != null)
            {
                if (EzThrottler.Throttle($"Throttle targeting: {x.BaseId}"))
                {
                    // IceLogging.Info($"Attempting to set the target to: {x.BaseId} | {x.Name}", "[Target Game Object]");
                    Svc.Targets.SetTarget(x);
                }
            }
        }
    }
    public static unsafe void InteractWithObject(IGameObject? gameObject)
    {
        try
        {
            if (gameObject == null || !gameObject.IsTargetable)
                return;
            var gameObjectPointer = (GameObject*)gameObject.Address;
            TargetSystem.Instance()->InteractWithObject(gameObjectPointer, false);
        }
        catch (Exception ex)
        {
            // IceLogging.Error($"InteractWithObject: Exception: {ex}");
        }
    }

    public class GatherPointInfo
    {
        public uint Type { get; set; }
        public uint Level { get; set; }
        public uint TerritoryId { get; set; }
        public string ZoneName { get; set; }
        public string PlaceName { get; set; }
        public uint ExpId { get; set; }
        public string ExpansionName { get; set; }
        public SortedSet<uint> NodeIds { get; set; }
        public List<uint> ItemIds { get; set; }
        public int Radius { get; set; }
    }

    public static Dictionary<uint, GatherPointInfo> SheetInfo = new();
    public static void UpdateSheetInfo()
    {
        var sheet_gatherPoint = Svc.Data.GetExcelSheet<GatheringPoint>();
        var sheet_TerritoryType = Svc.Data.GetExcelSheet<TerritoryType>();
        var sheet_PlaceName = Svc.Data.GetExcelSheet<PlaceName>();

        foreach (var gatherPoint in sheet_gatherPoint)
        {
            uint routeId = 0;

            uint nodeId = gatherPoint.RowId;
            uint type = 0;
            uint level = 0;
            uint territoryId = 0;
            uint expansion = 0;
            string expansionName = "ARealmReborn";
            string zoneName = "???";
            string placeName = "???";
            List<uint> itemIds = new();
            int radius = 0;


            if (gatherPoint.GatheringPointBase.Value.GatheringLevel == 0)
            {
                // This isn't actually a node, so continueing on
                continue;
            }
            else
            {
                var gatherPointBase = gatherPoint.GatheringPointBase.Value; // Sheet: GatherPointBase
                routeId = gatherPointBase.RowId;
                type = gatherPointBase.GatheringType.Value.RowId; // Column 1
                if (type is 0 or 1)
                    type = 16;
                else if (type is 2 or 3)
                    type = 17;
                else if (type is 4 or 5)
                    type = 18;

                level = gatherPointBase.GatheringLevel; // Column 2

                for (int i = 0; i <= 7; i++) // Items 0-7
                {
                    var gatheringItemId = gatherPointBase.Item[i].RowId;
                    if (Svc.Data.GetExcelSheet<GatheringItem>().TryGetRow(gatheringItemId, out var gatherItem))
                    {
                        var itemId = gatherItem.Item.RowId;
                        if (itemId != 0)
                            itemIds.Add(itemId);
                    }
                }

                if (itemIds.Count == 0)
                    continue;
            }

            if (gatherPoint.TerritoryType.Value.RowId != 0 || gatherPoint.TerritoryType.Value.RowId != 1)
            {
                var territoryType = gatherPoint.TerritoryType.Value;
                territoryId = territoryType.RowId;
                if (territoryType.Map.IsValid)
                    zoneName = territoryType.PlaceName.Value.Name.ToString();

                if (zoneName == string.Empty)
                    continue;

                expansion = territoryType.ExVersion.Value.RowId;
            }
            else
            {
                // These shouldn't exist, and probably exist currently for leve purposes. So skipping them
                continue;
            }

            if (gatherPoint.PlaceName.IsValid)
            {
                var placeNameSheet = gatherPoint.PlaceName.Value;
                placeName = placeNameSheet.Name.ToString();
            }

            switch (expansion)
            {
                case 0: expansionName = "ARealmReborn"; break;
                case 1: expansionName = "Heavensward"; break;
                case 2: expansionName = "Stormblood"; break;
                case 3: expansionName = "Shadowbringers"; break;
                case 4: expansionName = "Endwalker"; break;
                case 5: expansionName = "Dawntrail"; break;
                default: expansionName = "???"; break;
            }

            if (Svc.Data.GetExcelSheet<ExportedGatheringPoint>().TryGetRow(routeId, out var exportSheet))
            {
                radius = exportSheet.Radius;
            }

            if (SheetInfo.TryGetValue(routeId, out var routeInfo))
            {
                if (zoneName != "???")
                    routeInfo.ZoneName = zoneName;
                if (placeName != "???")
                    routeInfo.PlaceName = placeName;
                routeInfo.NodeIds.Add(nodeId);
            }
            else
            {
                SheetInfo.Add(routeId, new GatherPointInfo()
                {
                    Type = type, 
                    Level = level,
                    TerritoryId = territoryId,
                    ZoneName = zoneName,
                    PlaceName = placeName,
                    NodeIds = new() { nodeId },
                    ItemIds = itemIds,
                    Radius = radius,
                    ExpId = expansion,
                    ExpansionName = expansionName,
                });
            }
        }
    }

    public static uint ToUintABGR(Vector4 col)
    {
        byte a = (byte)(col.W * 255);
        byte b = (byte)(col.Z * 255);
        byte g = (byte)(col.Y * 255);
        byte r = (byte)(col.X * 255);
        return (uint)((a << 24) | (b << 16) | (g << 8) | r);
    }

    public static Vector4 FromUintABGR(uint color)
    {
        float a = ((color >> 24) & 0xFF) / 255f;
        float b = ((color >> 16) & 0xFF) / 255f;
        float g = ((color >> 8) & 0xFF) / 255f;
        float r = (color & 0xFF) / 255f;
        return new Vector4(r, g, b, a);
    }

    public static float DegreesToRadians(float degrees)
    {
        return degrees * (MathF.PI / 180f);
    }
}
