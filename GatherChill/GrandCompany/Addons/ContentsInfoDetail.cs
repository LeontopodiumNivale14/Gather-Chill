using ECommons.UIHelpers.AddonMasterImplementations;
using FFXIVClientStructs.FFXIV.Component.GUI;
using System.Collections.Generic;
using System.Text;

namespace GatherChill.GrandCompany.Addons;

public readonly record struct ProvisionSlot(int Slot, uint ItemId, uint IconId, int Amount, bool Bonus);

public readonly record struct CrafterSlot(int Slot, int ItemId, int IconId, int Amount, bool Bonus);

public unsafe class ContentsInfoDetail : AddonMasterBase<AtkUnitBase>
{
    private const int CrafterCountIndex = 56;
    private const int ProvisionCountIndex = 57;

    private const int CrafterItemIndex = 233;
    private const int CrafterIconIndex = 241;
    private const int CrafterAmountIndex = 249;
    private const int CrafterBonusIndex = 273;
    private const int CrafterSlots = 8;

    private const int ProvisionItemIndex = 292;
    private const int ProvisionIconIndex = 295;
    private const int ProvisionAmountIndex = 298;
    private const int ProvisionBonusIndex = 307;
    private const int ProvisionSlots = 3;

    public ContentsInfoDetail(nint addon) : base(addon) { }
    public ContentsInfoDetail(void* addon) : base(addon) { }
    public override string AddonDescription { get; } = "Contents Info detail window";

    public uint CrafterSupplyCount => Addon->AtkValues[CrafterCountIndex].UInt;
    public uint ProvisionMissionCount => Addon->AtkValues[ProvisionCountIndex].UInt;

    public List<CrafterSlot>? Crafting(out string problem)
    {
        problem = string.Empty;
        if (Addon->AtkValuesCount <= CrafterBonusIndex + CrafterSlots - 1)
        {
            problem = $"Detail has only {Addon->AtkValuesCount} values";
            return null;
        }

        var count = (int)Math.Min(CrafterSupplyCount, CrafterSlots);
        var slots = new List<CrafterSlot>();
        for (var i = 0; i < count; i++)
        {
            slots.Add(new CrafterSlot(
                i,
                Addon->AtkValues[CrafterItemIndex + i].Int,
                Addon->AtkValues[CrafterIconIndex + i].Int,
                Addon->AtkValues[CrafterAmountIndex + i].Int,
                Addon->AtkValues[CrafterBonusIndex + i].Bool));
        }

        return slots;
    }

    // Slot order is assumed to be Miner, Botanist, Fisher.
    public List<ProvisionSlot>? Provisioning(out string problem)
    {
        problem = string.Empty;
        if (Addon->AtkValuesCount <= ProvisionBonusIndex + ProvisionSlots - 1)
        {
            problem = $"Detail has only {Addon->AtkValuesCount} values";
            return null;
        }

        var count = (int)Math.Min(ProvisionMissionCount, ProvisionSlots);
        var slots = new List<ProvisionSlot>();
        for (var i = 0; i < count; i++)
        {
            var itemId = Addon->AtkValues[ProvisionItemIndex + i].Int;
            var iconId = Addon->AtkValues[ProvisionIconIndex + i].Int;
            var amount = Addon->AtkValues[ProvisionAmountIndex + i].Int;
            var bonus = Addon->AtkValues[ProvisionBonusIndex + i].Bool;

            if (itemId <= 0 || amount <= 0)
                continue;

            slots.Add(new ProvisionSlot(i, (uint)itemId, (uint)Math.Max(iconId, 0), amount, bonus));
        }

        return slots;
    }
}