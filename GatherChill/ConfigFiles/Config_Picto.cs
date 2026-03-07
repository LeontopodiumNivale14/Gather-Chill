using System;
using System.Collections.Generic;
using System.Text;

namespace GatherChill.ConfigFiles;

public partial class Config
{
    // Base Editor/Selected Colors
    public Vector4 Picto_NodeColor { get; set; } = new Vector4(0, 0, 0, 0);
    public Vector4 Picto_LandAreaColor { get; set; } = new Vector4(0, 0, 0, 0);
    public Vector4 Picto_SelectedColor { get; set; } = new Vector4(0f, 0f, 1f, 1f);
    public Vector4 Picto_SelectedFanColor { get; set; } = new Vector4(0f, 0f, 1f, 1f);
    public Vector4 Picto_RadiusColor { get; set; } = new Vector4(0f, 0f, 1f, 1f);
    public Vector4 Picto_TextColor { get; set; } = new Vector4(1f, 1f, 1f, 1f);
    public Vector4 Picto_DotColor { get; set; } = new Vector4(1f, 1f, 1f, 1f);

    public Dictionary<int, (Vector4 Primary, Vector4 Secondary)> Picto_GroupColors { get; set; } = new()
    {
        { 0, (new(1f, 0f, 0f, 1f),      new(0.8f, 0f, 0f, 1f))    }, // Red
        { 1, (new(0f, 1f, 0f, 1f),      new(0f, 0.8f, 0f, 1f))    }, // Green
        { 2, (new(1f, 0.67f, 0f, 1f),   new(0.8f, 0.53f, 0f, 1f)) }, // Orange
        { 3, (new(1f, 0f, 1f, 1f),      new(0.8f, 0f, 0.8f, 1f))  }, // Magenta
    };

    public float Picto_TextScale { get; set; } = 1;
    public bool UseVfx { get; set; } = false;
}
