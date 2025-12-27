using System.Collections.Generic;
using System.Text.Json.Serialization;
using ECommons.Configuration;

namespace GatherChill;
public class Config
{
    [JsonIgnore]
    public const int CurrentConfigVersion = 1;
    public Vector4 Picto_NodeColor { get; set; } = new Vector4(0, 0, 0, 0);
    public Vector4 Picto_LandAreaColor { get; set; } = new Vector4(0, 0, 0, 0);

    // Selected color - Bright Blue
    public Vector4 Picto_SelectedColor { get; set; } = new Vector4(0f, 0f, 1f, 1f); // RGBA: Blue

    // Group 0 colors - Red theme
    public Vector4 Picto_GroupColor1 { get; set; } = new Vector4(1f, 0f, 0f, 1f);      // Bright Red
    public Vector4 Picto_GroupColor2 { get; set; } = new Vector4(0.8f, 0f, 0f, 1f);    // Dark Red

    // Group 1 colors - Green theme
    public Vector4 Picto_GroupColor3 { get; set; } = new Vector4(0f, 1f, 0f, 1f);      // Bright Green
    public Vector4 Picto_GroupColor4 { get; set; } = new Vector4(0f, 0.8f, 0f, 1f);    // Dark Green

    // Group 2 colors - Orange theme
    public Vector4 Picto_GroupColor5 { get; set; } = new Vector4(1f, 0.67f, 0f, 1f);   // Bright Orange
    public Vector4 Picto_GroupColor6 { get; set; } = new Vector4(0.8f, 0.53f, 0f, 1f); // Dark Orange

    // Group 3 colors - Magenta theme
    public Vector4 Picto_GroupColor7 { get; set; } = new Vector4(1f, 0f, 1f, 1f);      // Bright Magenta
    public Vector4 Picto_GroupColor8 { get; set; } = new Vector4(0.8f, 0f, 0.8f, 1f);  // Dark Magenta
    public Vector4 Picto_RadiusColor { get; set; } = new Vector4(0f, 0f, 1f, 1f); // RGBA: Blue
    public Vector4 Picto_TextColor { get; set; } = new Vector4(1f, 1f, 1f, 1f); // White (RGBA)
    public float Picto_TextScale { get; set; } = 1;
    public bool UseVfx { get; set; } = false;


    public void Save()
    {
        EzConfig.Save();
    }
}