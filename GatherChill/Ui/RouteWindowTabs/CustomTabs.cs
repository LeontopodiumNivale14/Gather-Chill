using Dalamud.Interface.Utility;
using System.Collections.Generic;

namespace GatherChill.Ui.RouteWindowTabs
{
    internal class CustomTabs
    {
        public Dictionary<string, bool> CategoryStates = new();
        public uint selectedRoute = 0;

        public bool DrawCategoryButton(string label, string categoryId, FontAwesomeIcon? icon = null, float spacingAfter = 5)
        {
            float scale = ImGuiHelpers.GlobalScale;

            // Default coloring here
            var headerColor = ImGui.GetColorU32(ImGuiCol.Button);
            var textColor = ImGui.GetColorU32(ImGuiCol.Text);

            // Setting the values of the content size (padding, spacing, ect) that way it's used across the board
            float horizontalPadding = 8 * scale;
            float verticalPadding = 4 * scale;
            float iconTextSpacing = 4 * scale;

            // These are to make sure that they're drawn in place
            var drawList = ImGui.GetWindowDrawList();
            var cursorPos = ImGui.GetCursorScreenPos();

            // Calculate text size
            var textSize = ImGui.CalcTextSize(label);

            // Calculate icon width if present
            float iconWidth = 0;
            if (icon.HasValue)
            {
                iconWidth = textSize.Y + iconTextSpacing;
            }

            // Calculate button dimensions based on content
            float contentWidth = horizontalPadding * 2 + iconWidth + textSize.X;
            float contentHeight = verticalPadding * 2 + textSize.Y;

            // If it doesn't already exist, then creating an entry in the category state
            if (!CategoryStates.ContainsKey(categoryId))
                CategoryStates[categoryId] = false;

            bool isExpanded = CategoryStates[categoryId];
            bool isHovered = ImGui.IsMouseHoveringRect(cursorPos, new Vector2(cursorPos.X + contentWidth, cursorPos.Y + contentHeight))
                          && ImGui.IsWindowHovered(ImGuiHoveredFlags.AllowWhenBlockedByPopup | ImGuiHoveredFlags.ChildWindows);
            bool isClicked = isHovered && ImGui.IsMouseClicked(ImGuiMouseButton.Left);

            if (isClicked)
            {
                foreach (var tab in CategoryStates) 
                {
                    CategoryStates[tab.Key] = false;
                }

                CategoryStates[categoryId] = !CategoryStates[categoryId];
                isExpanded = CategoryStates[categoryId];
            }

            // Color changing! Based on the various states
            if (isExpanded)
                headerColor = ImGui.GetColorU32(ImGuiCol.TabActive);
            if (isHovered)
                headerColor = ImGui.GetColorU32(ImGuiCol.HeaderHovered);

            // Draw background rectangle with rounded corners (scaled)
            drawList.AddRectFilled(cursorPos, new Vector2(cursorPos.X + contentWidth, cursorPos.Y + contentHeight), headerColor, 5.0f * scale);

            // Position cursor with padding
            ImGui.SetCursorScreenPos(new Vector2(cursorPos.X + horizontalPadding, cursorPos.Y + verticalPadding));

            // Draw icon if provided
            if (icon.HasValue)
            {
                ImGuiEx.Icon(icon.Value);
                ImGui.SameLine(0, iconTextSpacing);
            }

            ImGui.Text(label);

            // Create an invisible button to properly reserve space and handle layout
            ImGui.SetCursorScreenPos(cursorPos);
            ImGui.InvisibleButton($"##{categoryId}_btn", new Vector2(contentWidth, contentHeight));

            // Add spacing after the button (scaled)
            ImGui.SameLine(0, spacingAfter * scale);

            return isExpanded;
        }
        public void EndCategoryButtonRow()
        {
            ImGui.NewLine();
            ImGui.Separator();
        }
    }
}
