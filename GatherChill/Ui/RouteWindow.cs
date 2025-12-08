

namespace GatherChill.Ui;

public class RoutesWindow : Window
{
    public RoutesWindow() : base("Gathering Routes###GatherChillRoutesWindow")
    {
        Flags = ImGuiWindowFlags.None;
        SizeConstraints = new()
        {
            MinimumSize = new(800, 600)
        };
        P.windowSystem.AddWindow(this);

    }

    public override void Draw()
    {
        var routeLoader = GatherChill.P.routeLoader;

        if (routeLoader.Routes.Count == 0)
        {
            ImGui.TextColored(new Vector4(1, 1, 0, 1), "No routes loaded.");
            ImGui.Text("Place .json route files in:");
            ImGui.Text(routeLoader.RoutesDirectory);

            if (ImGui.Button("Reload Routes"))
            {
                routeLoader.LoadAllRoutes();
            }
            return;
        }

        ImGui.Text($"Loaded Routes: {routeLoader.Routes.Count}");
        ImGui.Separator();

        if (ImGui.Button("Reload All Routes"))
        {
            routeLoader.LoadAllRoutes();
        }

        ImGui.SameLine();

        if (ImGui.Button("Open Routes Folder"))
        {
            System.Diagnostics.Process.Start("explorer.exe", routeLoader.RoutesDirectory);
        }

        ImGui.Separator();

        // Display routes in a table
        if (ImGui.BeginTable("RoutesTable", 6, ImGuiTableFlags.Borders | ImGuiTableFlags.RowBg | ImGuiTableFlags.Resizable | ImGuiTableFlags.SizingFixedFit))
        {
            ImGui.TableSetupColumn("Route ID");
            ImGui.TableSetupColumn("Zone");
            ImGui.TableSetupColumn("Job");
            ImGui.TableSetupColumn("Level");
            ImGui.TableSetupColumn("Nodes");
            ImGui.TableSetupColumn("Author");
            ImGui.TableHeadersRow();

            foreach (var route in routeLoader.Routes.Values)
            {
                ImGui.TableNextRow();

                ImGui.TableNextColumn();
                if (ImGui.Button($"{route.RouteId}##Edit Route_{route.RouteId}"))
                {

                }

                ImGui.TableNextColumn();
                ImGui.Text(route.ZoneName);

                ImGui.TableNextColumn();
                var jobName = route.GatheringJobId switch
                {
                    16 => "MIN",
                    17 => "BTN",
                    18 => "FSH",
                    _ => "???"
                };
                ImGui.Text(jobName);

                ImGui.TableNextColumn();
                ImGui.Text(route.LevelRequirement.ToString());

                ImGui.TableNextColumn();
                ImGui.Text(route.NodeIds.Count.ToString());

                ImGui.TableNextColumn();
                ImGui.Text(route.Author ?? "Unknown");
            }

            ImGui.EndTable();
        }
    }
}