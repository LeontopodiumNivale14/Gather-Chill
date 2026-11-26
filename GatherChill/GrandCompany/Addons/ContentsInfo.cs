using ECommons.UIHelpers.AddonMasterImplementations;
using FFXIVClientStructs.FFXIV.Client.UI.Agent;
using FFXIVClientStructs.FFXIV.Component.GUI;
using GatherChill.Utilities.Tools;
using Callback = ECommons.Automation.Callback;

namespace GatherChill.GrandCompany.Addons;

public unsafe class ContentsInfo : AddonMasterBase<AtkUnitBase>
{
    // Only built for the Grand Company "Next Mission Allowance" button.
    // Each button in this window has its own callback value (not the list index),
    // so these values are hardcoded for the GC provisioning detail and nothing else.

    // Realistically, I only need the GC thing (for now)

    private const int OpenDetailEvent = 12;
    private const int ChangeTabEvent = 15;
    private const int FirstTab = 0;
    private const int GrandCompanyDetailValue = 1;

    // Stuff I can use if I need to debug where the fuck this goes (this will... probably absolutely change in EC)
    private const uint TimersAddonRow = 4598; // "Next Mission Allowance" in ENG
    private const uint ListComponentId = 4;
    private const uint SecondaryTextNodeId = 7;

    public ContentsInfo(nint addon) : base(addon) { }
    public ContentsInfo(void* addon) : base(addon) { }
    public override string AddonDescription { get; } = "Contents Info window";

    public void SelectFirstTab() => Callback.Fire(Addon, true, ChangeTabEvent, FirstTab);

    public void OpenGrandCompanyDetail() => Callback.Fire(Addon, true, OpenDetailEvent, GrandCompanyDetailValue);

    // ---- Debug helpers ----

    public int EntryCount
    {
        get
        {
            var list = Addon->GetComponentListById(ListComponentId);
            if (list == null)
                return 0;

            return list->GetItemCount();
        }
    }

    // If for some reason this changes spots, this'll return it (it's not 1-10, it's whatever order it decides it wants to be almost, but usually is 1)
    public int FindTimersIndex()
    {
        if (!ExcelHelper.Sheet_Addon.TryGetRow(TimersAddonRow, out var addonRow))
            return -1;

        var targetText = addonRow.Text.ExtractText();
        var count = EntryCount;

        for (var i = 0; i < count; i++)
        {
            if (GetSecondaryText(i) == targetText)
                return i;
        }

        return -1;
    }

    public string GetSecondaryText(int index)
    {
        var list = Addon->GetComponentListById(ListComponentId);
        if (list == null)
            return string.Empty;

        var renderer = list->GetItemRenderer(index);
        if (renderer == null)
            return string.Empty;

        var node = renderer->GetTextNodeById(SecondaryTextNodeId);
        if (node == null)
            return string.Empty;

        return GenericHelpers.ReadSeString(&node->NodeText).GetText();
    }
}