using FFXIVClientStructs.FFXIV.Client.Game;
using FFXIVClientStructs.FFXIV.Client.UI.Agent;
using FFXIVClientStructs.Interop;
using GatherChill.Utilities.Tools;
using System.Collections.Generic;

namespace GatherChill.Utilities.Utility;

public static partial class Utils
{
    // A lot of this is grabbed from here: https://github.com/awgil/ffxiv_visland/blob/f10fc88eaf723416bdc16c11da4c400dcc8845c8/ffxiv_visland/Helpers/PurificationManager.cs#L24
    // Honestly, a lot more sane to do than trying to read the addon like I was thinking. . . 

    public static readonly InventoryType[] PlayerInventory =
    [
        InventoryType.Inventory1,
        InventoryType.Inventory2,
        InventoryType.Inventory3,
        InventoryType.Inventory4,
    ];

    public static unsafe List<Pointer<InventoryItem>> GetPurifyableItems()
    {
        List<Pointer<InventoryItem>> items = [];
        foreach (var inv in PlayerInventory)
        {
            var cont = InventoryManager.Instance()->GetInventoryContainer(inv);
            for (var i = 0; i < cont->Size; ++i)
                if (cont->GetInventorySlot(i)->Flags == InventoryItem.ItemFlags.Collectable && ExcelHelper.Sheet_Item.GetRow(cont->GetInventorySlot(i)->ItemId).AetherialReduce > 0)
                    items.Add(cont->GetInventorySlot(i));
        }
        return items;
    }

    public static bool CanPurifyAny() => GetPurifyableItems().Count > 0;
    public static unsafe bool HasPurifyItem(uint itemId) => GetPurifyableItems().Any(x => x.Value->ItemId == itemId);
    public static unsafe void PurifyItem(uint ItemId)
    {
        if (!HasPurifyItem(ItemId))
            return;

        var item = GetPurifyableItems().Where(x => x.Value->ItemId == ItemId).First();
        PurifyItem(item);
    } 
    public static unsafe void PurifyItem(Pointer<InventoryItem> item)
    {
        string tag = "Purify Item Attempt";

        var agent = AgentPurify.Instance();
        if (agent == null) 
        { 
            IceLogging.Debug("AgentPurify is null", tag); 
            return; 
        }

        agent->ReduceItem(item);
        IceLogging.Debug($"Reducing [{item.Value->ItemId}] {item.Value->Container}/{item.Value->Slot}", tag);
    }
}
