using Dalamud.Game.ClientState.Objects.Types;
using ECommons.Automation.NeoTaskManager;
using ECommons.DalamudServices.Legacy;
using ECommons.GameHelpers;
using ECommons.Reflection;
using ECommons.Throttlers;
using FFXIVClientStructs.FFXIV.Client.Game;
using FFXIVClientStructs.FFXIV.Client.Game.Control;
using FFXIVClientStructs.FFXIV.Client.Game.Object;
using FFXIVClientStructs.FFXIV.Client.UI.Agent;
using GatherChill.Utilities.Tools;
using Lumina.Excel.Sheets;

namespace GatherChill.Utilities.Utility;

public static partial class Utils
{
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
    public static unsafe void SetGatheringRing(uint territoryId, float worldX, float worldY, int radius, string? tooltip = "Node Location")
    {
        var territory = Svc.Data.GetExcelSheet<TerritoryType>().GetRow(territoryId);
        var map = territory.Map.Value;
        var agent = AgentMap.Instance();

        var intX = (int)(2 * worldX + 2048.0 / map.SizeFactor * 100 + 100.9);
        var intY = (int)(2 * worldY + 2048.0 / map.SizeFactor * 100 + 100.9);

        var x = (int)(intX - 100 - 2048.0 / map.SizeFactor * 100) / 2 - map.OffsetX;
        var y = (int)(intY - 100 - 2048.0 / map.SizeFactor * 100) / 2 - map.OffsetY;

        IceLogging.Debug($"Current map: {map.RowId} {territoryId} | {map.PlaceName.Value.Name} | {worldX} {worldY} | {x} {y} | {radius} | {tooltip}");

        agent->FlagMarkerCount = 0;
        agent->SetFlagMapMarker(territoryId, map.RowId, x, y);
        agent->TempMapMarkerCount = 0;
        agent->AddGatheringTempMarker(x, y, radius, tooltip: tooltip);
        agent->OpenMap(map.RowId, territoryId, tooltip, FFXIVClientStructs.FFXIV.Client.UI.Agent.MapType.GatheringLog);
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
