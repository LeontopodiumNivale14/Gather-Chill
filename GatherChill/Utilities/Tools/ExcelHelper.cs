using GatherChill.Utilities.Utility;
using Lumina.Excel;
using Lumina.Excel.Sheets;
using Action = Lumina.Excel.Sheets.Action;

namespace GatherChill.Utilities.Tools;

internal static class ExcelHelper
{
    // Gathering
    internal static ExcelSheet<GatheringPoint> Sheet_GatherPoint;
    internal static ExcelSheet<GatheringPointTransient> Sheet_GatherPointTransient;
    internal static ExcelSheet<GatheringRarePopTimeTable> Sheet_GatherTimeTable;
    internal static ExcelSheet<GatheringItem> Sheet_GatheringItem;
    internal static SubrowExcelSheet<GatheringItemPoint> Sheet_GatherItemPoint;
    internal static ExcelSheet<ExportedGatheringPoint> Sheet_ExportedGatherPoints;
    internal static ExcelSheet<GatheringType> Sheet_GatheringType;

    // Spearfishing
    internal static ExcelSheet<SpearfishingNotebook> Sheet_SpearfishingNotebook;
    internal static ExcelSheet<SpearfishingItem> Sheet_SpearfishingItem;

    // Items
    internal static ExcelSheet<Item> Sheet_Item;
    internal static ExcelSheet<EventItem> Sheet_EventItem;

    // World / Territory
    internal static ExcelSheet<TerritoryType> Sheet_TerritoryType;
    internal static ExcelSheet<PlaceName> Sheet_PlaceName;
    internal static ExcelSheet<ExVersion> Sheet_Expansion;
    internal static ExcelSheet<Town> Sheet_Town;

    // Aetherytes / Aether Currents
    internal static ExcelSheet<Aetheryte> Sheet_Aetheryte;
    internal static ExcelSheet<AetherCurrent> Sheet_AetherCurrent;
    internal static ExcelSheet<AetherCurrentCompFlgSet> Sheet_AethercurrentComplete;

    // Misc
    internal static ExcelSheet<Mount> Sheet_Mount;
    internal static ExcelSheet<Action> Sheet_Action;
    internal static ExcelSheet<Quest> Sheet_Quest;
    internal static ExcelSheet<Addon> Sheet_Addon;

    public static void Init()
    {
        // Gathering
        Sheet_GatherPoint = Svc.Data.GetExcelSheet<GatheringPoint>();
        Sheet_GatherPointTransient = Svc.Data.GetExcelSheet<GatheringPointTransient>();
        Sheet_GatherTimeTable = Svc.Data.GetExcelSheet<GatheringRarePopTimeTable>();
        Sheet_GatheringItem = Svc.Data.GetExcelSheet<GatheringItem>();
        Sheet_GatherItemPoint = Svc.Data.GetSubrowExcelSheet<GatheringItemPoint>();
        Sheet_ExportedGatherPoints = Svc.Data.GetExcelSheet<ExportedGatheringPoint>();
        Sheet_GatheringType = Svc.Data.GetExcelSheet<GatheringType>();

        // Spearfishing
        Sheet_SpearfishingNotebook = Svc.Data.GetExcelSheet<SpearfishingNotebook>();
        Sheet_SpearfishingItem = Svc.Data.GetExcelSheet<SpearfishingItem>();

        // Items
        Sheet_Item = Svc.Data.GetExcelSheet<Item>();
        Sheet_EventItem = Svc.Data.GetExcelSheet<EventItem>();

        // World / Territory
        Sheet_TerritoryType = Svc.Data.GetExcelSheet<TerritoryType>();
        Sheet_PlaceName = Svc.Data.GetExcelSheet<PlaceName>();
        Sheet_Expansion = Svc.Data.GetExcelSheet<ExVersion>();
        Sheet_Town = Svc.Data.GetExcelSheet<Town>();

        // Aetherytes / Aether Currents
        Sheet_Aetheryte = Svc.Data.GetExcelSheet<Aetheryte>();
        Sheet_AetherCurrent = Svc.Data.GetExcelSheet<AetherCurrent>();
        Sheet_AethercurrentComplete = Svc.Data.GetExcelSheet<AetherCurrentCompFlgSet>();

        // Misc
        Sheet_Mount = Svc.Data.GetExcelSheet<Mount>();
        Sheet_Action = Svc.Data.GetExcelSheet<Action>();
        Sheet_Quest = Svc.Data.GetExcelSheet<Quest>();
        Sheet_Addon = Svc.Data.GetExcelSheet<Addon>();

        // Doing this for the consumables so I don't have to constantly look it up
        Utils.Init();
    }

    public static string GetTerritoryName(uint territoryId)
    {
        if (!Sheet_TerritoryType.TryGetRow(territoryId, out var territory))
            return string.Empty;

        return territory.PlaceName.Value.Name.ToString();
    }
}