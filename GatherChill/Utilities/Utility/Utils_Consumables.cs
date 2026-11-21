using GatherChill.Utilities.Tools;
using Lumina.Excel.Sheets;
using System.Collections.Generic;

namespace GatherChill.Utilities.Utility;

public static partial class Utils
{
    internal static (uint Id, string Name)[] GatherFood;
    internal static (uint Id, string Name)[] CrafterFood;
    internal static (uint Id, string Name)[] Pots;
    internal static (uint Id, string Name)[] SpiritbondPots;
    internal static (uint Id, string Name)[] CrafterManuals;
    internal static (uint Id, string Name)[] GatherManuals;
    internal static (uint Id, string Name)[] SquadronManuals;

    internal static void Init()
    {
        GatherFood = ExcelHelper.Sheet_Item.Where(IsGatherFood).Select(x => (x.RowId, x.Name.ToString())).ToArray();
        CrafterFood = ExcelHelper.Sheet_Item.Where(IsCraftersFood).Select(x => (x.RowId, x.Name.ToString())).ToArray();
        Pots = ExcelHelper.Sheet_Item.Where(IsCraftersPot).Select(x => (x.RowId, x.Name.ToString())).ToArray();
        CrafterManuals = ExcelHelper.Sheet_Item.Where(IsCrafterManual).Select(x => (x.RowId, x.Name.ToString())).ToArray();
        GatherManuals = ExcelHelper.Sheet_Item.Where(IsGatherManual).Select(x => (x.RowId, x.Name.ToString())).ToArray();
        SquadronManuals = ExcelHelper.Sheet_Item.Where(IsSquadronManual).Select(x => (x.RowId, x.Name.ToString())).ToArray();
        SpiritbondPots = ExcelHelper.Sheet_Item.Where(IsSpiritbondPot).Select(x => (x.RowId, x.Name.ToString())).ToArray();
    }

    internal static ItemFood? GetItemConsumableProperties(Item item, bool hq)
    {
        if (!item.ItemAction.IsValid)
            return null;
        var action = item.ItemAction.Value;
        var actionParams = hq ? action.DataHQ : action.Data; // [0] = status, [1] = extra == ItemFood row, [2] = duration
        if (actionParams[0] is not 48 and not 49)
            return null; // not 'well fed' or 'medicated'
        return Svc.Data.GetExcelSheet<ItemFood>()?.GetRow(actionParams[1]);
    }
    internal static bool IsGatherFood(Item item)
    {
        if (item.ItemUICategory.RowId != 46)
            return false; // not a 'meal'
        var consumable = GetItemConsumableProperties(item, false);
        return consumable != null && consumable.Value.Params.Any(p => p.BaseParam.RowId is 10 or 72 or 73); // gp/gathering/perception
    }
    internal static bool IsCraftersFood(Item item)
    {
        if (item.ItemUICategory.RowId != 46)
            return false; // not a 'meal'
        var consumable = GetItemConsumableProperties(item, false);
        return consumable != null && consumable.Value.Params.Any(p => p.BaseParam.RowId is 11 or 70 or 71); // cp/craftsmanship/control
    }
    internal static bool IsCraftersPot(Item item)
    {
        if (item.ItemUICategory.RowId != 44)
            return false; // not a 'medicine'
        var consumable = GetItemConsumableProperties(item, false);
        return consumable != null && consumable.Value.Params.Any(p => p.BaseParam.RowId is 11 or 70 or 71 or 69 or 68); // cp/craftsmanship/control/increased spiritbond/reduced durability loss
    }
    internal static bool IsCrafterManual(Item item)
    {
        if (item.ItemUICategory.RowId != 63)
            return false; // not 'other'
        if (!item.ItemAction.IsValid)
            return false;
        var action = item.ItemAction.Value;
        return action.Action.RowId == 816 && action.Data[0] is 300 or 301 or 1751 or 5329;
    }
    internal static bool IsGatherManual(Item item)
    {
        if (item.ItemUICategory.RowId != 63)
            return false; // not 'other'
        if (!item.ItemAction.IsValid)
            return false;
        var action = item.ItemAction.Value;
        return action.Action.RowId == 816 && action.Data[0] is 302 or 303 or 1752 or 5330;
    }
    internal static bool IsSquadronManual(Item item)
    {
        if (item.ItemUICategory.RowId != 63)
            return false; // not 'other'
        if (!item.ItemAction.IsValid)
            return false;
        var action = item.ItemAction.Value;
        return action.Action.RowId == 816 && action.Data[0] is 2291 or 2292 or 2293 or 2294;
    }

    internal static bool IsSpiritbondPot(Item item)
    {
        if (item.ItemUICategory.RowId != 44)
            return false; // not a 'medicine'
        var consumable = GetItemConsumableProperties(item, false);
        return consumable != null && consumable.Value.Params.Any(p => p.BaseParam.RowId is 69); // increased spiritbond
    }

    private static readonly Dictionary<uint, string> ItemEffectCache = new();

    internal static string GetItemEffectText(uint itemId)
    {
        if (itemId == 0)
            return "";

        if (ItemEffectCache.TryGetValue(itemId, out var cached))
            return cached;

        return ItemEffectCache[itemId] = BuildItemEffectText(itemId);
    }

    private static string FormatBonus(bool relative, int value, int max)
        => relative ? $"+{value}% (max {max})" : $"+{value}";

    // Manual item ID -> (EXP earned the +150% lasts for, level it's halved at)
    private static readonly Dictionary<uint, (int ExpEarned, int HalvedAtLevel)> ManualEffects = new()
    {
        [4633] = (250_000, 40),
        [4635] = (500_000, 50),
        [12668] = (1_750_000, 70),
        [26553] = (2_000_000, 80),
    };

    private static string BuildItemEffectText(uint itemId)
    {
        if (ManualEffects.TryGetValue(itemId, out var manual))
            return $"+150% EXP\nLasts for {manual.ExpEarned:N0} EXP earned\nHalved at Lv. {manual.HalvedAtLevel}";

        var item = ExcelHelper.Sheet_Item[itemId];
        if (!item.ItemAction.IsValid)
            return "";

        var action = item.ItemAction.Value;
        var statusId = (uint)action.Data[0];

        // Food (Well Fed 48) and potions (Medicated 49): stats come from the ItemFood row
        if (statusId is 48 or 49)
        {
            var food = Svc.Data.GetExcelSheet<ItemFood>()?.GetRow(action.Data[1]);
            if (food == null)
                return "";

            var lines = new List<string>();
            foreach (var param in food.Value.Params)
            {
                if (param.BaseParam.RowId == 0)
                    continue;

                var stat = param.BaseParam.Value.Name.ToString();
                var nq = FormatBonus(param.IsRelative, param.Value, param.Max);
                var hq = FormatBonus(param.IsRelative, param.ValueHQ, param.MaxHQ);
                lines.Add($"{stat}: {nq}  |  HQ: {hq}");
            }

            return string.Join("\n", lines);
        }

        // Anything else with a status: fall back to the status's own description
        var status = Svc.Data.GetExcelSheet<Status>()?.GetRow(statusId);
        return status == null ? "" : status.Value.Description.ExtractText();
    }
}
