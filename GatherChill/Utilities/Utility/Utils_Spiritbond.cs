using FFXIVClientStructs.FFXIV.Client.Game;
using System;
using System.Collections.Generic;
using System.Text;

namespace GatherChill.Utilities.Utility;

public static unsafe partial class Utils
{
    #region SpiritbondItems

    public static ushort Weapon { get => InventoryManager.Instance()->GetInventoryContainer(InventoryType.EquippedItems)->Items[0].SpiritbondOrCollectability; }
    public static ushort Offhand { get => InventoryManager.Instance()->GetInventoryContainer(InventoryType.EquippedItems)->Items[1].SpiritbondOrCollectability; }
    public static ushort Helm { get => InventoryManager.Instance()->GetInventoryContainer(InventoryType.EquippedItems)->Items[2].SpiritbondOrCollectability; }
    public static ushort Body { get => InventoryManager.Instance()->GetInventoryContainer(InventoryType.EquippedItems)->Items[3].SpiritbondOrCollectability; }
    public static ushort Hands { get => InventoryManager.Instance()->GetInventoryContainer(InventoryType.EquippedItems)->Items[4].SpiritbondOrCollectability; }
    public static ushort Legs { get => InventoryManager.Instance()->GetInventoryContainer(InventoryType.EquippedItems)->Items[6].SpiritbondOrCollectability; }
    public static ushort Feet { get => InventoryManager.Instance()->GetInventoryContainer(InventoryType.EquippedItems)->Items[7].SpiritbondOrCollectability; }
    public static ushort Earring { get => InventoryManager.Instance()->GetInventoryContainer(InventoryType.EquippedItems)->Items[8].SpiritbondOrCollectability; }
    public static ushort Neck { get => InventoryManager.Instance()->GetInventoryContainer(InventoryType.EquippedItems)->Items[9].SpiritbondOrCollectability; }
    public static ushort Wrist { get => InventoryManager.Instance()->GetInventoryContainer(InventoryType.EquippedItems)->Items[10].SpiritbondOrCollectability; }
    public static ushort Ring1 { get => InventoryManager.Instance()->GetInventoryContainer(InventoryType.EquippedItems)->Items[11].SpiritbondOrCollectability; }
    public static ushort Ring2 { get => InventoryManager.Instance()->GetInventoryContainer(InventoryType.EquippedItems)->Items[12].SpiritbondOrCollectability; }

    #endregion

    public static bool SpiritbondUnlocked()
    {
        return QuestManager.IsQuestComplete(638);
    }

    public static bool Spiritbond_AnyReady()
    {
        if (!SpiritbondUnlocked())
            return false;

        if (Weapon == 10000) return true;
        if (Offhand == 10000) return true;
        if (Helm == 10000) return true;
        if (Body == 10000) return true;
        if (Hands == 10000) return true;
        if (Legs == 10000) return true;
        if (Feet == 10000) return true;
        if (Earring == 10000) return true;
        if (Neck == 10000) return true;
        if (Wrist == 10000) return true;
        if (Ring1 == 10000) return true;
        if (Ring2 == 10000) return true;

        return false;
    }
}
