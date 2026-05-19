using Lumina.Excel;
using Lumina.Excel.Sheets;

namespace GatherChill.Utilities.Tools;

internal static class ExcelHelper
{
    internal static ExcelSheet<GatheringPoint> Sheet_GatherPoint;
    internal static ExcelSheet<GatheringPointTransient> Sheet_GatherPointTransient;
    internal static ExcelSheet<GatheringRarePopTimeTable> Sheet_GatherTimeTable;
    internal static ExcelSheet<GatheringItem> Sheet_GatheringItem;
    internal static SubrowExcelSheet<GatheringItemPoint> Sheet_GatheringItemPoint;
    internal static ExcelSheet<Item> Sheet_Item;
    internal static ExcelSheet<ExportedGatheringPoint> Sheet_ExportedGatherPoints;
    internal static ExcelSheet<SpearfishingNotebook> Sheet_SpearfishingNotebook;
    internal static ExcelSheet<SpearfishingItem> Sheet_SpearfishingItem;

    internal static ExcelSheet<TerritoryType> Sheet_TerritoryType;
    internal static ExcelSheet<PlaceName> Sheet_PlaceName;

    public static void Init()
    {
        Svc.Data.GameData.Options.PanicOnSheetChecksumMismatch = false;
        Sheet_GatherPoint = Svc.Data.GetExcelSheet<GatheringPoint>();
        Sheet_GatherPointTransient = Svc.Data.GetExcelSheet<GatheringPointTransient>();
        Sheet_GatherTimeTable = Svc.Data.GetExcelSheet<GatheringRarePopTimeTable>();
        Sheet_GatheringItem = Svc.Data.GetExcelSheet<GatheringItem>();
        Sheet_GatheringItemPoint = Svc.Data.GetSubrowExcelSheet<GatheringItemPoint>();
        Sheet_Item = Svc.Data.GetExcelSheet<Item>();
        Sheet_ExportedGatherPoints = Svc.Data.GetExcelSheet<ExportedGatheringPoint>();
        Sheet_SpearfishingNotebook = Svc.Data.GetExcelSheet<SpearfishingNotebook>();
        Sheet_SpearfishingItem = Svc.Data.GetExcelSheet<SpearfishingItem>();

        Sheet_TerritoryType = Svc.Data.GetExcelSheet<TerritoryType>();
        Sheet_PlaceName = Svc.Data.GetExcelSheet<PlaceName>();
    }

    public static string GetTerritoryName(uint territoryid)
    {
        return Sheet_TerritoryType.GetRow(territoryid).PlaceName.Value.Name.ToString();
    }
}
