using System;
using System.Collections.Generic;
using System.Text;

namespace GatherChill.ConfigFiles;

public partial class Config
{
    // Base Editor/Selected Colors

    public Vector4 Picto_GatherFanColor { get; set; } = new Vector4(0f, 0f, 1f, 1f);
    public Vector4 Picto_FlightFanColor { get; set; } = new Vector4(0f, 0f, 1f, 1f);
    public Vector4 Picto_SelectedFan { get; set; } = new Vector4(0f, 0f, 1f, 1f);
    public Vector4 Picto_TextColor { get; set; } = new Vector4(1f, 1f, 1f, 1f);

    public Dictionary<int, ColorEditor> Picto_GroupColors { get; set; } = new()
    {
        [0] = new() { Primary = new(1f, 0f, 0f, 1f), Secondary = new(0.8f, 0f, 0f, 1f) },
        [1] = new() { Primary = new(1f, 0f, 0f, 1f), Secondary = new(0.8f, 0f, 0f, 1f) },
        [2] = new() { Primary = new(1f, 0.67f, 0f, 1f), Secondary = new(0.8f, 0.53f, 0f, 1f) },
        [3] = new() { Primary = new(1f, 0f, 1f, 1f), Secondary = new(0.8f, 0f, 0.8f, 1f) }
    };

    public class ColorEditor
    {
        public Vector4 Primary = new();
        public Vector4 Secondary = new();
    }

    public float Picto_TextScale { get; set; } = 1;
    public bool UseVfx { get; set; } = false;
}
