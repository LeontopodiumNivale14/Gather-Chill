using ECommons.GameHelpers;
using GatherChill.Scheduler.Handlers;
using GatherChill.Utilities.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace GatherChill.Ui.Tabs_Debug
{
    internal class CrazyArrow
    {
        private static Vector4 color = Vector4.Zero;

        private static Vector3 _position = Vector3.Zero;
        private static float _backRadius = 1.0f;
        private static float _tipRadius = 0.35f;
        private static float _depth = 1.5f;
        private static float _wingSpread = 0.7f;
        private static float _notchFrac = 0.33f;
        private static float _rotation = 0f;

        public static void Draw()
        {
            if (ImGui.Button("Set to player Pos"))
            {
                _position = Player.Position;
            }
            ImGui.SliderFloat("Back Radius", ref _backRadius, 0.1f, 3f);
            ImGui.SliderFloat("Tip Radius", ref _tipRadius, 0.05f, 2f);
            ImGui.SliderFloat("Depth", ref _depth, 0.1f, 5f);
            ImGui.SliderFloat("Wing Spread", ref _wingSpread, 0f, 3f);
            ImGui.SliderFloat("Notch Frac", ref _notchFrac, 0.05f, 0.8f);
            ImGui.SliderFloat("Rotation", ref _rotation, 0f, MathF.PI * 2f);
            ImGui.ColorEdit4("Color Editor", ref color);

            if (Player.Available)
            {
                // Draw slightly in front of the player so you can see it
                var pos = Player.Position + new Vector3(0, 0.1f, 0);
                PictoManager.DrawArrow(_position, Player.Rotation + _rotation, _backRadius, _tipRadius, _depth, _wingSpread, _notchFrac, Utils.ToUintABGR(color));
            }
        }
    }
}
