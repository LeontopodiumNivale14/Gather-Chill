using GatherChill.Utilities.Tools;

namespace GatherChill.Utilities.Utility;

public static partial class Utils
{
    // Is of category Crystal. 
    private const uint CrystalCategory = 59;

    // Acts like a crystal with buffs so track too.
    private const uint UnaspectedCrystal = 10099;

    /// <summary>
    /// Does it gather like a crystal: i.e., shards, crystals, clusters, and Unaspected Crystal.
    /// </summary>
    public static bool IsCrystal(uint itemId) => itemId == UnaspectedCrystal || InCrystalCategory(itemId);

    /// <summary>
    /// Shards, Crystals, and clusters have a hard cap of 9999 and cannot make a new stack in inventory.
    /// </summary>
    public static int? CrystalCap(uint itemId)
    {
        if (ExcelHelper.Sheet_Item?.GetRowOrDefault(itemId) is not { } item
            || item.ItemUICategory.RowId != CrystalCategory || item.StackSize == 0)
            return null;

        return (int)item.StackSize;
    }

    private static bool InCrystalCategory(uint itemId) =>
        ExcelHelper.Sheet_Item?.GetRowOrDefault(itemId) is { ItemUICategory.RowId: CrystalCategory } ;

    /// <summary>
    /// Have we capped crystals as there is a hard limit?
    /// </summary>
    public static bool IsCrystalCapped(uint itemId) => CrystalCap(itemId) is { } cap && GetItemCount(itemId) >= cap;
}
