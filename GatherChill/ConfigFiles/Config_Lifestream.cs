using ECommons.GameHelpers;
using GatherChill.Enums;
using System.Collections.Generic;

namespace GatherChill.ConfigFiles;

public partial class Config
{
    public class CharacterInformation
    {
        public ReturnTypes WaitReturnPoint { get; set; } = ReturnTypes.None;
        public string WaitReturn_Command { get; set; } = string.Empty;
        public ReturnTypes PostReturnPoint { get; set; } = ReturnTypes.None;
        public string PostReturn_Command { get; set; } = string.Empty;
    }

    public Dictionary<ulong, CharacterInformation> CharacterConfig { get; set; } = new();

    public bool Return_Home { get; set; } = false;
    public bool Return_FC { get; set; } = false;
    public bool Return_Apartment { get; set; } = false;

    public CharacterInformation? GetCharacterInfo() => GetCharacterInfo(Player.CID);

    public CharacterInformation? GetCharacterInfo(ulong cid)
    {
        if (cid == 0)
            return null;

        return CharacterConfig.GetValueOrDefault(cid);
    }

    public CharacterInformation? GetOrCreateCharacterInfo(ulong cid)
    {
        if (cid == 0)
            return null;

        if (CharacterConfig.TryGetValue(cid, out var info))
            return info;

        info = new CharacterInformation();
        CharacterConfig[cid] = info;
        return info;
    }
}