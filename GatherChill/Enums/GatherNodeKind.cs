namespace GatherChill.Enums
{
    [Flags]
    public enum GatherNodeKind
    {
        None = 0,
        Regular = 1 << 0,
        Unspoiled = 1 << 1,
        Ephemeral = 1 << 2,
        Legendary = 1 << 3,
    }
}
