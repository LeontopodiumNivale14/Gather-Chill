namespace GatherChill.ConfigFiles;

public partial class Config
{
    public bool NavmeshMovementEnabled { get; set; } = true;
    public bool NavmeshVerboseLogging { get; set; } = false;
    public float NavmeshInteractDistance { get; set; } = 4f;
    public int NavmeshInteractRetries { get; set; } = 3;
}
