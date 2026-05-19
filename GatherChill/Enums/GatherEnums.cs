namespace GatherChill.Enums
{
    public enum GatherBuffId
    {
        BoonIncrease_2,
        BoonIncrease_1,
        Tidings,
        YieldII,
        YieldI,
        BYII,
        BonusIntegrity,
        BonusIntegrity_Chance,
        FieldMasteryIII,
        FieldMasteryII,
        FieldMasteryI,
        FieldMasteryTemp,

        // Crystal Specfic Buffs
        TwelveBounty,
        GivingLand,
    }

    public enum CombinedGatherType
    {
        UNKNOWN = 0,
        Miner = 16,
        Botanist = 17,
        Fisher = 18
    }

    [Flags]
    public enum FilterFlags
    {
        None = 0,

        Miner = 0x000001,
        Botanist = 0x000002,

        All = 0x000003
    }
}
