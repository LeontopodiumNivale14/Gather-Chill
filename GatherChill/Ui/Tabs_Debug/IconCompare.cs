using Dalamud.Interface.Textures;
using GatherChill.Gui;
using System;

namespace GatherChill.Ui.Tabs_Debug;

/// <summary>
/// Debug view: GameIcons (CPU Lanczos3 resize) vs Dalamud's native GPU-scaled icon, side by side.
/// Both are drawn at real pixel size, which is what the player actually sees.
/// </summary>
public static class GameIconsCompare
{
    private static readonly int[] Sizes = [16, 20, 24, 32, 40, 48, 64, 96, 128];

    private static int iconId = 0;
    private static bool grey = false;

    public static void Draw()
    {
        ImGui.SetNextItemWidth(150);
        ImGui.InputInt("Icon ID", ref iconId);
        ImGui.Checkbox("Grey", ref grey);
        ImGui.SameLine();
        if (ImGui.Button("Reload icon")) Dispose();

        // iconId <= 0 means zero or negative: nothing to look up
        if (iconId <= 0)
        {
            ImGui.TextUnformatted("Enter an icon ID.");
            return;
        }

        ImGui.TextUnformatted("GameIcons goes through the same cached path the plugin uses. Native is the full-res game icon scaled by the GPU.");
        ImGui.TextUnformatted("Native ignores the Grey checkbox.");

        if (!ImGui.BeginTable("##iconCompare", 3, ImGuiTableFlags.Borders | ImGuiTableFlags.SizingFixedFit)) return;

        ImGui.TableSetupColumn("Size");
        ImGui.TableSetupColumn("GameIcons");
        ImGui.TableSetupColumn("Native (GPU)");
        ImGui.TableHeadersRow();

        foreach (var size in Sizes)
        {
            ImGui.TableNextRow();

            ImGui.TableNextColumn();
            ImGui.TextUnformatted($"{size}px");

            ImGui.TableNextColumn();
            DrawGameIcons((uint)iconId, size);

            ImGui.TableNextColumn();
            DrawNative((uint)iconId, size);
        }

        ImGui.EndTable();
    }

    // Kept so existing calls from the plugin's Dispose still compile. Drops the cached resizes of the current icon.
    public static void Dispose()
    {
        if (iconId <= 0) return;
        GameIcons.Invalidate((uint)iconId);
    }

    private static void DrawGameIcons(uint id, int size)
    {
        // Owned and cached by GameIcons, so no Dispose here.
        if (!GameIcons.TryGetScaledIcon(id, size, out var texture, grey) || texture == null)
        {
            ImGui.TextUnformatted("failed (see log)");
            return;
        }

        ImGui.Image(texture.Handle, new Vector2(size));
    }

    // Shared texture owned by Dalamud: no caching and no Dispose. The wrap is only valid for this frame.
    private static void DrawNative(uint id, int size)
    {
        if (!Svc.Texture.TryGetFromGameIcon(new GameIconLookup(id), out var shared))
        {
            ImGui.TextUnformatted("not found");
            return;
        }

        if (!shared.TryGetWrap(out var wrap, out _) || wrap == null)
        {
            ImGui.TextUnformatted("loading...");
            return;
        }

        ImGui.Image(wrap.Handle, new Vector2(size));
    }
}