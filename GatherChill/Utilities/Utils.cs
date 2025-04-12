using Dalamud.Game.ClientState.Conditions;
using Dalamud.Game.ClientState.Objects.Types;
using Dalamud.Interface.Textures;
using ECommons.Automation.NeoTaskManager;
using ECommons.DalamudServices.Legacy;
using ECommons.ExcelServices;
using ECommons.GameHelpers;
using ECommons.Logging;
using ECommons.Reflection;
using ECommons.Throttlers;
using FFXIVClientStructs.FFXIV.Client.Game;
using FFXIVClientStructs.FFXIV.Client.Game.Control;
using FFXIVClientStructs.FFXIV.Client.Game.Object;
using FFXIVClientStructs.FFXIV.Client.Game.UI;
using FFXIVClientStructs.FFXIV.Client.UI;
using Lumina.Excel.Sheets;
using System.Collections.Generic;

namespace GatherChill.Utilities;

public static unsafe class Utils
{
    #region Plugin/Ecoms stuff

    public static bool HasPlugin(string name) => DalamudReflector.TryGetDalamudPlugin(name, out _, false, true);
    internal static bool GenericThrottle => FrameThrottler.Throttle("AutoRetainerGenericThrottle", 10);
    public static TaskManagerConfiguration DConfig => new(timeLimitMS: 10 * 60 * 3000, abortOnTimeout: false);

    public static void PluginVerbos(string message) => PluginLog.Verbose(message);
    public static void PluginInfo(string message) => PluginLog.Information(message);
    public static void PluginDebug(string message) => PluginLog.Debug(message);

    #endregion

    #region Player Information

    public static uint GetClassJobId() => Svc.ClientState.LocalPlayer!.ClassJob.RowId;
    public static unsafe int GetLevel(int expArrayIndex = -1)
    {
        if (expArrayIndex == -1) expArrayIndex = Svc.ClientState.LocalPlayer?.ClassJob.Value.ExpArrayIndex ?? 0;
        return UIState.Instance()->PlayerState.ClassJobLevels[expArrayIndex];
    }
    internal static unsafe short GetCurrentLevelFromSheet(Job? job = null)
    {
        PlayerState* playerState = PlayerState.Instance();
        return playerState->ClassJobLevels[Svc.Data.GetExcelSheet<ClassJob>().GetRowOrDefault((uint)(job ?? (Player.Available ? Player.Object.GetJob() : 0)))?.ExpArrayIndex ?? 0];
    }

    public static bool IsInZone(uint zoneID) => Svc.ClientState.TerritoryType == zoneID;
    public static uint CurrentTerritory() => GameMain.Instance()->CurrentTerritoryTypeId;

    public static bool IsBetweenAreas => Svc.Condition[ConditionFlag.BetweenAreas] || Svc.Condition[ConditionFlag.BetweenAreas51];

    public static bool PlayerNotBusy()
    {
        return Player.Available
               && Player.Object.CastActionId == 0
               && !IsOccupied()
               && !Svc.Condition[ConditionFlag.Jumping]
               && !Svc.Condition[ConditionFlag.Jumping61]
               && Player.Object.IsTargetable
               && !Player.IsAnimationLocked;
    }

    internal static unsafe float GetDistanceToPlayer(Vector3 v3) => Vector3.Distance(v3, Player.GameObject->Position);
    internal static unsafe float GetDistanceToPlayer(IGameObject gameObject) => GetDistanceToPlayer(gameObject.Position);

    public static unsafe int GetItemCount(int itemID, bool includeHq = true)
    => includeHq ? InventoryManager.Instance()->GetInventoryItemCount((uint)itemID, true)
    + InventoryManager.Instance()->GetInventoryItemCount((uint)itemID) + InventoryManager.Instance()->GetInventoryItemCount((uint)itemID + 500_000)
    : InventoryManager.Instance()->GetInventoryItemCount((uint)itemID) + InventoryManager.Instance()->GetInventoryItemCount((uint)itemID + 500_000);

    #endregion

    #region Target Information

    internal static bool? TargetgameObject(IGameObject? gameObject)
    {
        var x = gameObject;
        if (Svc.Targets.Target != null && Svc.Targets.Target.DataId == x.DataId)
            return true;

        if (!IsOccupied())
        {
            if (x != null)
            {
                if (EzThrottler.Throttle($"Throttle Targeting {x.DataId}"))
                {
                    Svc.Targets.SetTarget(x);
                    ECommons.Logging.PluginLog.Information($"Setting the target to {x.DataId}");
                }
            }
        }
        return false;
    }
    internal static bool TryGetObjectByDataId(ulong dataId, out IGameObject? gameObject) => (gameObject = Svc.Objects.OrderBy(GetDistanceToPlayer).FirstOrDefault(x => x.DataId == dataId)) != null;
    internal static unsafe void InteractWithObject(IGameObject? gameObject)
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
            Svc.Log.Info($"InteractWithObject: Exception: {ex}");
        }
    }

    #endregion

    #region Addon Information

    public static bool IsAddonActive(string AddonName) // Used to see if the addon is active/ready to be fired on
    {
        var addon = RaptureAtkUnitManager.Instance()->GetAddonByName(AddonName);
        return addon != null && addon->IsVisible && addon->IsReady;
    }

    #endregion

    #region LoadOnBoot

    public static void UpdateDictionaries()
    {
        var GathTypeSheets = Svc.Data.GetExcelSheet<GatheringType>();
        var GatheringPointBaseSheets = Svc.Data.GetExcelSheet<GatheringPointBase>();
        var GatheringItemSheet = Svc.Data.GetExcelSheet<GatheringItem>();

        foreach (var entry in GathTypeSheets)
        {
            var key = entry.RowId;
            var name = entry.Name.ToString();
            ISharedImmediateTexture? mainIcon = null;
            ISharedImmediateTexture? offIcon = null;
            if (entry.IconMain is { } IconMain)
            {
                if (Svc.Texture.TryGetFromGameIcon(IconMain, out var icon))
                {
                    mainIcon = icon;
                }
            }
            if (entry.IconOff is { } IconOff)
            {
                if (Svc.Texture.TryGetFromGameIcon(IconOff, out var icon))
                {
                    offIcon = icon;
                }
            }

            if (!GatheringNodeDict.ContainsKey(key))
            {
                GatheringNodeDict[key] = new GatheringTypes
                {
                    Name = name,
                    MainIcon = mainIcon,
                    ShinyIcon = offIcon
                };
            }
        }

        foreach (var entry in GatheringPointBaseSheets)
        {
            var key = entry.RowId.ToInt();
            var gatheringType = entry.GatheringType.RowId.ToInt();
            var level = entry.GatheringLevel;
            if (level == 0 || gatheringType is (4 or 5))
                continue;

            HashSet<uint> items = new();
            for (int i = 0; i < 8; i++)
            {
                var item = entry.Item[i].RowId;
                if (item != 0)
                {
                    PluginLog.Debug($"Key: {key} | Gather Item: {item}");
                    var itemId = GatheringItemSheet.GetRow(item).Item.RowId;
                    items.Add(itemId);
                }
            }

            if (!GatheringPointBaseDict.ContainsKey(key))
            {
                GatheringPointBaseDict[key] = new GatheringPointBaseInformation
                {
                    GatheringType = gatheringType,
                    GatheringLevel = level,
                    Items = items
                };
            }
        }
    }

    #endregion

}
