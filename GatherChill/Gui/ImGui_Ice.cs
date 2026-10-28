using Dalamud.Interface.Colors;
using Dalamud.Interface.Textures.TextureWraps;
using Dalamud.Interface.Utility.Raii;

namespace GatherChill.Gui
{
    internal class ImGui_Ice
    {
        public static ImRaii.ColorDisposable? PushChildBG() => C.UseIceTheme ? ImRaii.PushColor(ImGuiCol.ChildBg, Theme_Colors.ChildBg) : default;

        // Typically I JUST need the icon + string in the sameline,
        // so this works for me to throw the text -> Icon
        public static void Icon(FontAwesomeIcon icon, string? s = null)
        {
            ImGui.PushFont(UiBuilder.IconFont);
            ImGui.TextUnformatted(icon.ToIconString());
            ImGui.PopFont();
            if (s != null)
            {
                ImGui.SameLine();
                ImGui.TextUnformatted(s);
            }
        }

        public static void Icon(FontAwesomeIcon icon, Vector4 color, string? s = null)
        {
            using (ImRaii.PushColor(ImGuiCol.Text, ImGui.ColorConvertFloat4ToU32(color)))
            {
                using (ImRaii.PushFont(UiBuilder.IconFont))
                {
                    ImGui.TextUnformatted(icon.ToIconString());
                }
            }
            if (s != null)
            {
                ImGui.SameLine();
                ImGui.TextUnformatted(s);
            }
        }

        // Version of Ecommons, but in a format that I work with more
        public static void IconWithTooltip(FontAwesomeIcon icon, string? tooltip = null, bool sameLine = true)
        {
            if (sameLine)
                ImGui.SameLine();
            ImGui.PushFont(UiBuilder.IconFont);
            ImGui.TextUnformatted(icon.ToIconString());
            ImGui.PopFont();

            if (tooltip != null && ImGui.IsItemHovered(ImGuiHoveredFlags.AllowWhenDisabled))
            {
                ImGui.SetTooltip(tooltip);
            }
        }

        // ReSharper version that gives me the gameIcon. Allows for a cleaner image and less... blurry
        // Minor tradeoff. Still debating on how much is worth
        public static bool ImageButtonWithText(uint iconId, string label, string id, bool? isOn = null, float padding = 4f, float sidePadding = 4f)
        {
            var iconHeight = (int)MathF.Round(ImGui.GetFrameHeight() - sidePadding);
            if (GameIcons.TryGetScaledIcon(iconId, iconHeight, out var texture))
                return ImageButtonWithText(texture, label, id, new Vector2(iconHeight, iconHeight), isOn, padding, sidePadding);

            return ImGui.Button($"{label}##{id}");
        }

        private static bool ImageButtonWithText(IDalamudTextureWrap? texture, string label, string id, Vector2 imageSize, bool? isOn, float padding = 4f, float sidePadding = 4f)
        {
            var frameHeight = ImGui.GetFrameHeight();
            var textSize = ImGui.CalcTextSize(label);

            var iconHeight = frameHeight - sidePadding;
            var aspect = imageSize.X / imageSize.Y;
            var scaledImage = new Vector2(iconHeight * aspect, iconHeight);

            var buttonSize = new Vector2(
                sidePadding + scaledImage.X + padding + textSize.X + sidePadding,
                frameHeight
            );

            var pos = ImGui.GetCursorScreenPos();
            bool clicked = ImGui.InvisibleButton($"##{id}", buttonSize);

            bool hovered = ImGui.IsItemHovered();
            bool active = ImGui.IsItemActive();

            var drawList = ImGui.GetWindowDrawList();
            var rounding = ImGui.GetStyle().FrameRounding;

            uint bgColor = isOn == false
                ? ImGui.GetColorU32(ImGuiCol.Button, 0.5f) // dimmed, disabled-looking
                : active ? ImGui.GetColorU32(ImGuiCol.ButtonActive) :
                  hovered ? ImGui.GetColorU32(ImGuiCol.ButtonHovered) :
                            ImGui.GetColorU32(ImGuiCol.Button);
            drawList.AddRectFilled(pos, pos + buttonSize, bgColor, rounding);

            uint borderColor = isOn switch
            {
                true => C.UseIceTheme
                    ? ImGui.GetColorU32(Theme_Colors.OnAccentSubtle)
                    : ImGui.GetColorU32(ImGuiColors.ParsedGold),
                false => ImGui.GetColorU32(ImGuiCol.Border),
                null => ImGui.GetColorU32(ImGuiCol.Border)
            };
            float borderThickness = isOn == true ? 1.5f : 1f;
            drawList.AddRect(pos, pos + buttonSize, borderColor, rounding, ImDrawFlags.None, borderThickness);

            uint imageTint = isOn switch
            {
                true => ImGui.GetColorU32(Vector4.One),
                false => ImGui.GetColorU32(new Vector4(0.6f, 0.6f, 0.6f, 0.7f)),
                null => ImGui.GetColorU32(Vector4.One)
            };

            var imagePos = new Vector2(
                pos.X + sidePadding,
                pos.Y + (frameHeight - scaledImage.Y) / 2f
            );
            drawList.AddImage(texture.Handle, imagePos, imagePos + scaledImage, Vector2.Zero, Vector2.One, imageTint);

            var textPos = new Vector2(
                imagePos.X + scaledImage.X + padding,
                pos.Y + (frameHeight - textSize.Y) / 2f
            );
            drawList.AddText(textPos, ImGui.GetColorU32(ImGuiCol.Text), label);

            return clicked;
        }

        public static bool ImageButton(uint iconId, string id, bool? isOn = null, float sidePadding = 4f)
        {
            var iconHeight = (int)MathF.Round(ImGui.GetFrameHeight() - sidePadding);
            if (GameIcons.TryGetScaledIcon(iconId, iconHeight, out var texture))
                return ImageButton(texture, id, new Vector2(iconHeight, iconHeight), isOn, sidePadding);

            return ImGui.Button($"##{id}");
        }

        private static bool ImageButton(IDalamudTextureWrap? texture, string id, Vector2 imageSize, bool? isOn, float sidePadding = 4f)
        {
            var frameHeight = ImGui.GetFrameHeight();
            var aspect = imageSize.X / imageSize.Y;
            var scaledImage = new Vector2(frameHeight - sidePadding, frameHeight - sidePadding) with { X = (frameHeight - sidePadding) * aspect };

            var buttonSize = new Vector2(sidePadding + scaledImage.X + sidePadding, frameHeight);

            var pos = ImGui.GetCursorScreenPos();
            bool clicked = ImGui.InvisibleButton($"##{id}", buttonSize);

            bool hovered = ImGui.IsItemHovered();
            bool active = ImGui.IsItemActive();

            var drawList = ImGui.GetWindowDrawList();
            var rounding = ImGui.GetStyle().FrameRounding;

            uint bgColor = isOn == false
                ? ImGui.GetColorU32(ImGuiCol.Button, 0.5f) // dimmed, disabled-looking
                : active ? ImGui.GetColorU32(ImGuiCol.ButtonActive) :
                  hovered ? ImGui.GetColorU32(ImGuiCol.ButtonHovered) :
                            ImGui.GetColorU32(ImGuiCol.Button);
            drawList.AddRectFilled(pos, pos + buttonSize, bgColor, rounding);

            uint borderColor = isOn switch
            {
                true => C.UseIceTheme
                    ? ImGui.GetColorU32(Theme_Colors.OnAccentSubtle)
                    : ImGui.GetColorU32(ImGuiColors.ParsedGold),
                false => ImGui.GetColorU32(ImGuiCol.Border),
                null => ImGui.GetColorU32(ImGuiCol.Border)
            };
            float borderThickness = isOn == true ? 1.5f : 1f;
            drawList.AddRect(pos, pos + buttonSize, borderColor, rounding, ImDrawFlags.None, borderThickness);

            uint imageTint = isOn switch
            {
                true => ImGui.GetColorU32(Vector4.One),
                false => ImGui.GetColorU32(new Vector4(0.6f, 0.6f, 0.6f, 0.7f)),
                null => ImGui.GetColorU32(Vector4.One)
            };

            var imagePos = new Vector2(
                pos.X + sidePadding,
                pos.Y + (frameHeight - scaledImage.Y) / 2f
            );
            drawList.AddImage(texture.Handle, imagePos, imagePos + scaledImage, Vector2.Zero, Vector2.One, imageTint);

            return clicked;
        }

        // Borrowed from cosmic plugin... 
        public static void Draw_XPBar(float current, float needed, string? label = null, Vector2? size = null, bool showPercent = true)
        {
            var drawList = ImGui.GetWindowDrawList();
            var actualSize = size ?? new Vector2(ImGui.GetContentRegionAvail().X, 10);

            // Colors defined here since they're used across the board
            var blueColor = new Vector4(0.2f, 0.6f, 1f, 1f);       // Blue #3399ff  - Fill Bar Part #1 (Left Side)
            var greenColor = new Vector4(0.6f, 1f, 0.8f, 1f);      // Green #99ffcc - Fill Bar Part #2 (Right Side)
            var brassColor = new Vector4(0.71f, 0.55f, 0.18f, 1f); // Brass #b58d2e - Full Bar Part #1 (Left Side)
            var goldColor = new Vector4(1f, 0.84f, 0f, 1f);        // Gold #ffd600  - Full Bar Part #2 (Right Side)

            var isComplete = needed > 0 && current >= needed;
            var fraction = needed > 0 ? Math.Clamp(current / needed, 0f, 1f) : 0f;

            // Header row: label on the left, percent on the right.
            if (label != null || showPercent)
            {
                var headerPos = ImGui.GetCursorScreenPos();
                var lineHeight = ImGui.GetTextLineHeight();
                var spacing = ImGui.GetStyle().ItemSpacing.X;

                // Floor so we only show 100% when it's actually complete (99.9% shouldn't round up)
                var percentText = $"{(int)MathF.Floor(fraction * 100f)}%";
                var percentSize = showPercent ? ImGui.CalcTextSize(percentText) : Vector2.Zero;

                if (label != null)
                {
                    // Clip the label so a long one never runs underneath the percent text
                    var clipMax = new Vector2(headerPos.X + actualSize.X - percentSize.X - spacing, headerPos.Y + lineHeight);
                    drawList.PushClipRect(headerPos, clipMax, true);
                    drawList.AddText(headerPos, ImGui.GetColorU32(ImGuiCol.Text), label);
                    drawList.PopClipRect();
                }

                if (showPercent)
                {
                    var percentPos = new Vector2(headerPos.X + actualSize.X - percentSize.X, headerPos.Y);
                    var percentColor = isComplete ? goldColor : new Vector4(1f, 1f, 1f, 0.7f);
                    drawList.AddText(percentPos, ImGui.GetColorU32(percentColor), percentText);
                }

                // Reserve the header row so the bar lands underneath it
                ImGui.Dummy(new Vector2(actualSize.X, lineHeight));
            }

            // Bar position is grabbed AFTER the header so it sits below it
            var pos = ImGui.GetCursorScreenPos();
            var barStart = pos;
            var barEnd = new Vector2(pos.X + actualSize.X, pos.Y + actualSize.Y);

            // Draw background (dark gray)
            drawList.AddRectFilled(barStart, barEnd, ImGui.GetColorU32(new Vector4(0.15f, 0.15f, 0.15f, 1f)));

            // Case 1: At or above cap (show full gold)
            if (isComplete)
            {
                drawList.AddRectFilledMultiColor(
                    barStart, barEnd,
                    ImGui.GetColorU32(brassColor),  // top-left
                    ImGui.GetColorU32(goldColor),   // top-right
                    ImGui.GetColorU32(goldColor),   // bottom-right
                    ImGui.GetColorU32(brassColor)   // bottom-left
                );
            }
            // Case 2: Normal progression
            else if (fraction > 0f)
            {
                var filledEnd = new Vector2(pos.X + actualSize.X * fraction, pos.Y + actualSize.Y);
                drawList.AddRectFilledMultiColor(
                    barStart, filledEnd,
                    ImGui.GetColorU32(blueColor),  // top-left
                    ImGui.GetColorU32(greenColor), // top-right
                    ImGui.GetColorU32(greenColor), // bottom-right
                    ImGui.GetColorU32(blueColor)   // bottom-left
                );
            }

            // Reset to captured pos before Dummy so manual cursor shifts (e.g. vertical centering)
            // don't cause the Dummy to double-advance. No-op for normal usage.
            ImGui.SetCursorScreenPos(pos);
            ImGui.Dummy(actualSize);
        }

        public static void Table_VertCenterText(string text)
        {
            ImGui.AlignTextToFramePadding();
            ImGui.TextUnformatted(text);
        }

        // Experimental Shit
        private static readonly Vector4 PercentLow = new(0.90f, 0.30f, 0.30f, 1f); // Red
        private static readonly Vector4 PercentMid = new(0.95f, 0.80f, 0.30f, 1f); // Yellow
        private static readonly Vector4 PercentHigh = new(0.35f, 0.85f, 0.45f, 1f); // Green

        public static Vector4 PercentColor(float fraction)
        {
            fraction = Math.Clamp(fraction, 0f, 1f);

            if (fraction < 0.5f)
                return Vector4.Lerp(PercentLow, PercentMid, fraction * 2f);

            return Vector4.Lerp(PercentMid, PercentHigh, (fraction - 0.5f) * 2f);
        }

        // Everything in the panel that isn't the bar track: 3 text lines, 2 gaps, and padding.
        private static float VerticalBarChrome(bool framed, float padding)
        {
            var pad = framed ? padding : 0f;
            return ImGui.GetTextLineHeight() * 3f + ImGui.GetStyle().ItemSpacing.Y * 2f + pad * 2f;
        }

        public static float VerticalBarHeight(Vector2? barSize = null, bool framed = true, float padding = 6f)
        {
            var size = barSize ?? new Vector2(22f, 80f);
            return size.Y + VerticalBarChrome(framed, padding);
        }

        public static void VerticalBar(string label, float current, float max, Vector2? barSize = null, Vector4? colorOverride = null, bool framed = true, float padding = 6f, float? totalHeight = null)
        {
            var size = barSize ?? new Vector2(22f, 80f);

            // Stretch the track to fill the requested height, but never shrink below the natural size.
            if (totalHeight is { } height)
                size.Y = Math.Max(height - VerticalBarChrome(framed, padding), size.Y);

            var fraction = max <= 0f ? 0f : Math.Clamp(current / max, 0f, 1f);
            var fillColor = colorOverride ?? PercentColor(fraction);

            var currentText = ((int)current).ToString("N0");
            var maxText = $"/ {(int)max:N0}"; // divider baked in so it's measured AND drawn identically

            var labelSize = ImGui.CalcTextSize(label);
            var currentSize = ImGui.CalcTextSize(currentText);
            var maxSize = ImGui.CalcTextSize(maxText);

            var lineHeight = ImGui.GetTextLineHeight();
            var spacing = ImGui.GetStyle().ItemSpacing.Y;

            // Column is as wide as its widest element, everything centers inside it.
            var columnWidth = Math.Max(size.X, Math.Max(labelSize.X, Math.Max(currentSize.X, maxSize.X)));
            var contentHeight = lineHeight + spacing + size.Y + spacing + lineHeight + lineHeight;

            var pad = framed ? padding : 0f;
            var panelSize = new Vector2(columnWidth + pad * 2f, contentHeight + pad * 2f);

            var drawList = ImGui.GetWindowDrawList();
            var panelMin = ImGui.GetCursorScreenPos();
            var panelMax = panelMin + panelSize;

            // Reserve the full panel (content + padding) so surrounding widgets don't overlap.
            ImGui.Dummy(panelSize);

            // Content origin sits inside the padding.
            var pos = panelMin + new Vector2(pad, pad);

            float CenterX(float width) => pos.X + (columnWidth - width) * 0.5f;

            // 0) Panel background + border (drawn first so everything else sits on top)
            if (framed)
            {
                var panelRounding = ImGui.GetStyle().ChildRounding;
                drawList.AddRectFilled(panelMin, panelMax, ImGui.GetColorU32(ImGuiCol.ChildBg), panelRounding);
                drawList.AddRect(panelMin, panelMax, ImGui.GetColorU32(ImGuiCol.Border), panelRounding);
            }

            // 1) Label (top)
            var labelPos = new Vector2(CenterX(labelSize.X), pos.Y);
            drawList.AddText(labelPos, ImGui.GetColorU32(ImGuiCol.Text), label);

            // 2) Bar
            var barMin = new Vector2(CenterX(size.X), labelPos.Y + lineHeight + spacing);
            var barMax = new Vector2(barMin.X + size.X, barMin.Y + size.Y);
            var rounding = size.X * 0.25f;

            var trackColor = ImGui.GetColorU32(new Vector4(1f, 1f, 1f, 0.12f));
            drawList.AddRectFilled(barMin, barMax, trackColor, rounding);

            if (fraction > 0f)
            {
                var fillTop = barMax.Y - size.Y * fraction;
                drawList.AddRectFilled(new Vector2(barMin.X, fillTop), barMax, ImGui.GetColorU32(fillColor), rounding);
            }

            // Thin outline so the empty portion is still readable
            drawList.AddRect(barMin, barMax, ImGui.GetColorU32(new Vector4(1f, 1f, 1f, 0.25f)), rounding);

            // 3) Current amount (matches fill color)
            var currentPos = new Vector2(CenterX(currentSize.X), barMax.Y + spacing);
            drawList.AddText(currentPos, ImGui.GetColorU32(fillColor), currentText);

            // 4) Max amount with divider (dimmed)
            var maxPos = new Vector2(CenterX(maxSize.X), currentPos.Y + lineHeight);
            drawList.AddText(maxPos, ImGui.GetColorU32(new Vector4(1f, 1f, 1f, 0.55f)), maxText);
        }

        private const float StartAngle = -MathF.PI * 0.5f; // 12 o'clock

        /// <summary>
        /// Draws a circular progress ring with centered text.
        /// Call inside a window (e.g. an overlay) after setting the cursor position.
        /// </summary>
        public static void CircleBar(string label, int current, int max, float radius = 28f, float thickness = 4f, float valuePadding = 6f, uint? trackColor = null, uint? fillColor = null)
        {
            var fraction = max > 0 ? Math.Clamp(current / (float)max, 0f, 1f) : 0f;

            var drawList = ImGui.GetWindowDrawList();
            var origin = ImGui.GetCursorScreenPos();

            var valueText = $"{current}/{max}";
            var valueSize = ImGui.CalcTextSize(valueText);

            var ringSize = radius * 2f;
            var width = MathF.Max(ringSize, valueSize.X);
            var height = ringSize + valuePadding + valueSize.Y;

            // Reserve layout space for the ring + value so other widgets don't overlap
            ImGui.Dummy(new Vector2(width, height));

            // Ring is centered horizontally within the reserved width
            var center = new Vector2(origin.X + width * 0.5f, origin.Y + radius);
            var arcRadius = radius - thickness * 0.5f;

            var track = trackColor ?? ImGui.GetColorU32(new Vector4(1f, 1f, 1f, 0.12f));
            var fill = fillColor ?? ImGui.GetColorU32(new Vector4(0.35f, 0.75f, 1f, 1f));

            drawList.AddCircle(center, arcRadius, track, 64, thickness);
            DrawArc(drawList, center, arcRadius, thickness, fraction, fill);

            var textColor = ImGui.GetColorU32(ImGuiCol.Text);

            // Label centered inside the ring
            var labelSize = ImGui.CalcTextSize(label);
            var labelPos = new Vector2(center.X - labelSize.X * 0.5f, center.Y - labelSize.Y * 0.5f);
            drawList.AddText(labelPos, textColor, label);

            // Value centered below the ring
            var valuePos = new Vector2(
                origin.X + (width - valueSize.X) * 0.5f,
                origin.Y + ringSize + valuePadding);
            drawList.AddText(valuePos, textColor, valueText);
        }

        private static void DrawArc(ImDrawListPtr drawList, Vector2 center, float arcRadius, float thickness, float fraction, uint color)
        {
            if (fraction <= 0f)
                return;

            var endAngle = StartAngle + MathF.PI * 2f * fraction;

            drawList.PathClear();
            drawList.PathArcTo(center, arcRadius, StartAngle, endAngle, 64);
            drawList.PathStroke(color, ImDrawFlags.None, thickness);
        }

        public static void StatusDot(float radius = 5f, Vector4? color = null)
        {
            var dotColor = color ?? PercentLow; // same red as PercentLow

            var drawList = ImGui.GetWindowDrawList();
            var pos = ImGui.GetCursorScreenPos();

            var diameter = radius * 2f;

            // Frame height matches the row height of AlignTextToFramePadding text and buttons,
            // so the dot's vertical center lines up with the text's vertical center.
            var slotHeight = MathF.Max(diameter, ImGui.GetFrameHeight());

            // Reserve layout space so neighboring widgets don't overlap.
            ImGui.Dummy(new Vector2(diameter, slotHeight));

            var center = new Vector2(pos.X + radius, pos.Y + slotHeight * 0.5f);
            drawList.AddCircleFilled(center, radius, ImGui.GetColorU32(dotColor), 0);
        }
    }
}
