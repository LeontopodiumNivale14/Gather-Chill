using ECommons.GameHelpers;
using FFXIVClientStructs.FFXIV.Client.UI.Misc;
using GatherChill.Utilities.Tools;
using Lumina.Excel.Sheets;

namespace GatherChill.Utilities.Utility;

public static partial class Utils
{
    public static unsafe bool TrySwapToGatheringJob(uint classJobId)
    {
        if (Player.ClassJob.RowId == classJobId)
            return true;

        if (Player.IsCasting || Player.Mounting || GenericHelpers.IsOccupied())
            return false;

        if (!TryGetGearsetForClassJob(classJobId, out var gearsetId))
        {
            IceLogging.Warning($"No gearset found for class {GetClassJobName(classJobId)} ({classJobId}).");
            return true;
        }

        var gearsetModule = RaptureGearsetModule.Instance();
        gearsetModule->EquipGearset(gearsetId);
        IceLogging.Info($"Switching to gearset {gearsetId + 1} ({GetClassJobName(classJobId)}).");
        return false;
    }

    private static unsafe bool TryGetGearsetForClassJob(uint classJobId, out byte gearsetId)
    {
        gearsetId = 0;
        var gearsetModule = RaptureGearsetModule.Instance();
        byte? parentFallback = null;

        var classRow = Svc.Data.GetExcelSheet<ClassJob>().GetRowOrDefault(classJobId);
        var parentJobId = classRow?.ClassJobParent.RowId ?? 0;

        for (byte i = 0; i < 100; i++)
        {
            var gearset = gearsetModule->GetGearset(i);
            if (gearset == null)
                continue;

            if (!gearset->Flags.HasFlag(RaptureGearsetModule.GearsetFlag.Exists))
                continue;

            if (gearset->Id != i)
                continue;

            if (gearset->ClassJob == classJobId)
            {
                gearsetId = i;
                return true;
            }

            if (parentFallback == null && parentJobId != 0 && gearset->ClassJob == parentJobId)
                parentFallback = i;
        }

        if (parentFallback != null)
        {
            gearsetId = parentFallback.Value;
            return true;
        }

        return TryGetHighestGearsetForClassJob(classJobId, out gearsetId);
    }

    private static unsafe bool TryGetHighestGearsetForClassJob(uint classJobId, out byte gearsetId)
    {
        gearsetId = 0;
        var gearsetModule = RaptureGearsetModule.Instance();
        RaptureGearsetModule.GearsetEntry? best = null;

        foreach (ref readonly var entry in gearsetModule->Entries)
        {
            if (!entry.Flags.HasFlag(RaptureGearsetModule.GearsetFlag.Exists))
                continue;

            if (entry.ClassJob != classJobId)
                continue;

            if (best == null || entry.ItemLevel > best.Value.ItemLevel)
                best = entry;
        }

        if (best == null)
            return false;

        gearsetId = best.Value.Id;
        return true;
    }

    private static string GetClassJobName(uint classJobId) => classJobId switch
    {
        16 => "Miner",
        17 => "Botanist",
        18 => "Fisher",
        _ => Svc.Data.GetExcelSheet<ClassJob>().GetRowOrDefault(classJobId)?.Name.ExtractText().ToString() ?? $"Job {classJobId}"
    };
}
