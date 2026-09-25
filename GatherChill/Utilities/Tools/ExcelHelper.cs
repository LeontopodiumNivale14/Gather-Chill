using Lumina.Excel;
using Lumina.Excel.Sheets;

namespace GatherChill.Utilities.Tools;

internal static class ExcelHelper
{
    internal static ExcelSheet<GatheringPoint> Sheet_GatherPoint;
    internal static ExcelSheet<GatheringPointTransient> Sheet_GatherPointTransient;
    internal static ExcelSheet<GatheringRarePopTimeTable> Sheet_GatherTimeTable;
    internal static ExcelSheet<GatheringItem> Sheet_GatheringItem;
    internal static ExcelSheet<ExportedGatheringPoint> Sheet_ExportedGatherPoints;
    internal static ExcelSheet<SpearfishingNotebook> Sheet_SpearfishingNotebook;
    internal static ExcelSheet<SpearfishingItem> Sheet_SpearfishingItem;

    internal static ExcelSheet<TerritoryType> Sheet_TerritoryType;
    internal static ExcelSheet<PlaceName> Sheet_PlaceName;
    internal static ExcelSheet<ExVersion> Sheet_Expansion;

    internal static ExcelSheet<Item> Sheet_Item;
    internal static SubrowExcelSheet<GatheringItemPoint> Sheet_GatherItemPoint;

    internal static ExcelSheet<Aetheryte> Sheet_Aetheryte;
    internal static ExcelSheet<AetherCurrent> Sheet_AetherCurrent;
    internal static ExcelSheet<AetherCurrentCompFlgSet> Sheet_AethercurrentComplete;

    public static void Init()
    {
        Svc.Data.GameData.Options.PanicOnSheetChecksumMismatch = false;
        Sheet_GatherPoint = Svc.Data.GetExcelSheet<GatheringPoint>();
        Sheet_GatherPointTransient = Svc.Data.GetExcelSheet<GatheringPointTransient>();
        Sheet_GatherTimeTable = Svc.Data.GetExcelSheet<GatheringRarePopTimeTable>();
        Sheet_GatheringItem = Svc.Data.GetExcelSheet<GatheringItem>();
        Sheet_ExportedGatherPoints = Svc.Data.GetExcelSheet<ExportedGatheringPoint>();
        Sheet_SpearfishingNotebook = Svc.Data.GetExcelSheet<SpearfishingNotebook>();
        Sheet_SpearfishingItem = Svc.Data.GetExcelSheet<SpearfishingItem>();

        Sheet_TerritoryType = Svc.Data.GetExcelSheet<TerritoryType>();
        Sheet_PlaceName = Svc.Data.GetExcelSheet<PlaceName>();
        Sheet_Expansion = Svc.Data.GetExcelSheet<ExVersion>();

        Sheet_Item = Svc.Data.GetExcelSheet<Item>();
        Sheet_GatherItemPoint = Svc.Data.GetSubrowExcelSheet<GatheringItemPoint>();

        Sheet_Aetheryte = Svc.Data.GetExcelSheet<Aetheryte>();
        Sheet_AetherCurrent = Svc.Data.GetExcelSheet<AetherCurrent>();
        Sheet_AethercurrentComplete = Svc.Data.GetExcelSheet<AetherCurrentCompFlgSet>();
    }

    public static string GetTerritoryName(uint territoryid)
    {
        return Sheet_TerritoryType.GetRow(territoryid).PlaceName.Value.Name.ToString();
    }
}
