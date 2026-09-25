using GatherChill.Enums;
using GatherChill.Utilities.Tools;
using Lumina.Excel.Sheets;
using System.Collections.Generic;

namespace GatherChill.Utilities.GatheringHelpers;

public static partial class Gather_Util
{
    // I can't find the fucking thing to co-olate the aetherial reduction -> what items come from it...
    // And unfort, I do wanna tie them in for future things/in general *-sighs-*
    // Why do I hate myself with feature creep

    // List of gatherables -> Reducables was found here: https://ffxiv.consolegameswiki.com/wiki/Aetherial_Reduction
    // Then just converted into a nice list for me to be able to show users
    // This does contain the fishing ones currently as well, but I figured I might as well include them, even if I don't have a desire to automate fishing

    public class ReduceClass
    {
        public uint ItemId { get; set; } = 0;
        public string ItemName => ExcelHelper.Sheet_Item.GetRow(ItemId).Name.ToString();
        public uint IconId => ExcelHelper.Sheet_Item.GetRow(ItemId).Icon;
    }

    public class ReduceInfo
    {
        public uint NormalItemId { get; set; } = 0;
        public uint PrimeItemId { get; set; } = 0;
        public uint SublimeItemId { get; set; } = 0;
        public ExpansionEnum Expansion { get; set; } = ExpansionEnum.ARR;
        public List<ReduceClass> ResultItems { get; set; } = new();

        public IEnumerable<uint> AllItemIds()
        {
            if (NormalItemId != 0) yield return NormalItemId;
            if (PrimeItemId != 0) yield return PrimeItemId;
            if (SublimeItemId != 0) yield return SublimeItemId;
        }

        public Item ItemInfo(uint itemId) => ExcelHelper.Sheet_Item.GetRow(itemId);
    };

    public static List<ReduceInfo> ReducableItems = new()
    {
        new() // Granular Clay
        {
            NormalItemId = 12968,
            Expansion = ExpansionEnum.HW,
            ResultItems = new()
            {
                new() { ItemId = 12936 }, // Duskborne Aethersand
                new() { ItemId = 11 },    // Earth Crystal
                new() { ItemId = 17 },    // Earth Cluster
            },
        },
        new() // Lightning Moraine
        {
            NormalItemId = 5218,
            Expansion = ExpansionEnum.HW,
            ResultItems = new()
            {
                new() { ItemId = 12936 }, // Duskborne Aethersand
                new() { ItemId = 12 },    // Lightning Crystal
                new() { ItemId = 18 },    // Lightning Cluster
            },
        },
        new() // Pot Marjoram
        {
            NormalItemId = 33148,
            Expansion = ExpansionEnum.HW,
            ResultItems = new()
            {
                new() { ItemId = 12936 }, // Duskborne Aethersand
                new() { ItemId = 10 },    // Wind Crystal
                new() { ItemId = 16 },    // Wind Cluster
            },
        },
        new() // Fire Moraine
        {
            NormalItemId = 5214,
            Expansion = ExpansionEnum.HW,
            ResultItems = new()
            {
                new() { ItemId = 12936 }, // Duskborne Aethersand
                new() { ItemId = 8 },     // Fire Crystal
                new() { ItemId = 14 },    // Fire Cluster
            },
        },
        new() // Peat Moss
        {
            NormalItemId = 12969,
            Expansion = ExpansionEnum.HW,
            ResultItems = new()
            {
                new() { ItemId = 12937 }, // Dawnborne Aethersand
                new() { ItemId = 11 },    // Earth Crystal
                new() { ItemId = 17 },    // Earth Cluster
            },
        },
        new() // Bright Lightning Rock
        {
            NormalItemId = 12967,
            Expansion = ExpansionEnum.HW,
            ResultItems = new()
            {
                new() { ItemId = 12937 }, // Dawnborne Aethersand
                new() { ItemId = 12 },    // Lightning Crystal
                new() { ItemId = 18 },    // Lightning Cluster
            },
        },
        new() // Water Mint
        {
            NormalItemId = 33149,
            Expansion = ExpansionEnum.HW,
            ResultItems = new()
            {
                new() { ItemId = 12937 }, // Dawnborne Aethersand
                new() { ItemId = 10 },    // Wind Crystal
                new() { ItemId = 16 },    // Wind Cluster
            },
        },
        new() // Bright Fire Rock
        {
            NormalItemId = 12966,
            Expansion = ExpansionEnum.HW,
            ResultItems = new()
            {
                new() { ItemId = 12937 }, // Dawnborne Aethersand
                new() { ItemId = 8 },     // Fire Crystal
                new() { ItemId = 14 },    // Fire Cluster
            },
        },
        new() // Humic Soil
        {
            NormalItemId = 33147,
            Expansion = ExpansionEnum.HW,
            ResultItems = new()
            {
                new() { ItemId = 12939 }, // Leafborne Aethersand
                new() { ItemId = 11 },    // Earth Crystal
                new() { ItemId = 17 },    // Earth Cluster
            },
        },
        new() // Radiant Lightning Moraine
        {
            NormalItemId = 5224,
            Expansion = ExpansionEnum.HW,
            ResultItems = new()
            {
                new() { ItemId = 12938 }, // Landborne Aethersand
                new() { ItemId = 12 },    // Lightning Crystal
                new() { ItemId = 18 },    // Lightning Cluster
            },
        },
        new() // Wild Sage
        {
            NormalItemId = 33150,
            Expansion = ExpansionEnum.HW,
            ResultItems = new()
            {
                new() { ItemId = 12939 }, // Leafborne Aethersand
                new() { ItemId = 10 },    // Wind Crystal
                new() { ItemId = 16 },    // Wind Cluster
            },
        },
        new() // Radiant Fire Moraine
        {
            NormalItemId = 5220,
            Expansion = ExpansionEnum.HW,
            ResultItems = new()
            {
                new() { ItemId = 12938 }, // Landborne Aethersand
                new() { ItemId = 8 },     // Fire Crystal
                new() { ItemId = 14 },    // Fire Cluster
            },
        },
        new() // Lover's Laurel
        {
            NormalItemId = 15948,
            Expansion = ExpansionEnum.HW,
            ResultItems = new()
            {
                new() { ItemId = 15648 }, // Light-kissed Aethersand
                new() { ItemId = 9 },     // Ice Crystal
                new() { ItemId = 15 },    // Ice Cluster
            },
        },
        new() // Radiant Astral Moraine
        {
            NormalItemId = 15949,
            Expansion = ExpansionEnum.HW,
            ResultItems = new()
            {
                new() { ItemId = 15648 }, // Light-kissed Aethersand
                new() { ItemId = 13 },    // Water Crystal
                new() { ItemId = 19 },    // Water Cluster
            },
        },
        new() // Dacite
        {
            NormalItemId = 33152,
            Expansion = ExpansionEnum.StB,
            ResultItems = new()
            {
                new() { ItemId = 20015 }, // Everbright Aethersand
                new() { ItemId = 13 },    // Water Crystal
                new() { ItemId = 19 },    // Water Cluster
            },
        },
        new() // Doman Yellow
        {
            NormalItemId = 20012,
            Expansion = ExpansionEnum.StB,
            ResultItems = new()
            {
                new() { ItemId = 20013 }, // Dusklight Aethersand
                new() { ItemId = 11 },    // Earth Crystal
                new() { ItemId = 17 },    // Earth Cluster
            },
        },
        new() // Schorl
        {
            NormalItemId = 20009,
            Expansion = ExpansionEnum.StB,
            ResultItems = new()
            {
                new() { ItemId = 20014 }, // Dawnlight Aethersand
                new() { ItemId = 12 },    // Lightning Crystal
                new() { ItemId = 18 },    // Lightning Cluster
            },
        },
        new() // Countess Tea Leaves
        {
            NormalItemId = 33151,
            Expansion = ExpansionEnum.StB,
            ResultItems = new()
            {
                new() { ItemId = 20014 }, // Dawnlight Aethersand
                new() { ItemId = 10 },    // Wind Crystal
                new() { ItemId = 16 },    // Wind Cluster
            },
        },
        new() // Torreya Branch
        {
            NormalItemId = 19937,
            Expansion = ExpansionEnum.StB,
            ResultItems = new()
            {
                new() { ItemId = 20016 }, // Everborn Aethersand
                new() { ItemId = 9 },     // Ice Crystal
                new() { ItemId = 15 },    // Ice Cluster
            },
        },
        new() // Rhodolite
        {
            NormalItemId = 33153,
            Expansion = ExpansionEnum.StB,
            ResultItems = new()
            {
                new() { ItemId = 20013 }, // Dusklight Aethersand
                new() { ItemId = 8 },     // Fire Crystal
                new() { ItemId = 14 },    // Fire Cluster
            },
        },
        new() // Yanxian Verbena
        {
            NormalItemId = 23221,
            Expansion = ExpansionEnum.StB,
            ResultItems = new()
            {
                new() { ItemId = 23182 }, // Duskglow Aethersand
                new() { ItemId = 12 },    // Lightning Crystal
                new() { ItemId = 18 },    // Lightning Cluster
            },
        },
        new() // Yanxian Soil
        {
            NormalItemId = 23220,
            Expansion = ExpansionEnum.StB,
            ResultItems = new()
            {
                new() { ItemId = 23182 }, // Duskglow Aethersand
                new() { ItemId = 10 },    // Wind Crystal
                new() { ItemId = 16 },    // Wind Cluster
            },
        },
        new() // Voeburt Bichir
        {
            NormalItemId = 27542,
            Expansion = ExpansionEnum.ShB,
            ResultItems = new()
            {
                new() { ItemId = 27811 }, // Chiaroglow Aethersand
                new() { ItemId = 9 },     // Ice Crystal
                new() { ItemId = 15 },    // Ice Cluster
            },
        },
        new() // Poecilia
        {
            NormalItemId = 27543,
            Expansion = ExpansionEnum.ShB,
            ResultItems = new()
            {
                new() { ItemId = 27812 }, // Scuroglow Aethersand
                new() { ItemId = 13 },    // Water Crystal
                new() { ItemId = 19 },    // Water Cluster
            },
        },
        new() // Gale Rock
        {
            NormalItemId = 27805,
            Expansion = ExpansionEnum.ShB,
            ResultItems = new()
            {
                new() { ItemId = 27811 }, // Chiaroglow Aethersand
                new() { ItemId = 13 },    // Water Crystal
                new() { ItemId = 19 },    // Water Cluster
            },
        },
        new() // White Clay
        {
            NormalItemId = 27808,
            Expansion = ExpansionEnum.ShB,
            ResultItems = new()
            {
                new() { ItemId = 27811 }, // Chiaroglow Aethersand
                new() { ItemId = 11 },    // Earth Crystal
                new() { ItemId = 17 },    // Earth Cluster
            },
        },
        new() // Solarite
        {
            NormalItemId = 27806,
            Expansion = ExpansionEnum.ShB,
            ResultItems = new()
            {
                new() { ItemId = 27812 }, // Scuroglow Aethersand
                new() { ItemId = 12 },    // Lightning Crystal
                new() { ItemId = 18 },    // Lightning Cluster
            },
        },
        new() // Sweet Marjoram
        {
            NormalItemId = 27809,
            Expansion = ExpansionEnum.ShB,
            ResultItems = new()
            {
                new() { ItemId = 27812 }, // Scuroglow Aethersand
                new() { ItemId = 10 },    // Wind Crystal
                new() { ItemId = 16 },    // Wind Cluster
            },
        },
        new() // Bog Sage
        {
            NormalItemId = 27810,
            Expansion = ExpansionEnum.ShB,
            ResultItems = new()
            {
                new() { ItemId = 27814 }, // Agewood Aethersand
                new() { ItemId = 9 },     // Ice Crystal
                new() { ItemId = 15 },    // Ice Cluster
            },
        },
        new() // Shade Quartz
        {
            NormalItemId = 27807,
            Expansion = ExpansionEnum.ShB,
            ResultItems = new()
            {
                new() { ItemId = 27813 }, // Agedeep Aethersand
                new() { ItemId = 8 },     // Fire Crystal
                new() { ItemId = 14 },    // Fire Cluster
            },
        },
        new() // Fuchsia Bloom
        {
            NormalItemId = 30593,
            Expansion = ExpansionEnum.ShB,
            ResultItems = new()
            {
                new() { ItemId = 30590 }, // Levinstrike Aethersand
                new() { ItemId = 9 },     // Ice Crystal
                new() { ItemId = 15 },    // Ice Cluster
            },
        },
        new() // Thunder Rock
        {
            NormalItemId = 30591,
            Expansion = ExpansionEnum.ShB,
            ResultItems = new()
            {
                new() { ItemId = 30590 }, // Levinstrike Aethersand
                new() { ItemId = 11 },    // Earth Crystal
                new() { ItemId = 17 },    // Earth Cluster
            },
        },
        new() // Levin Mint
        {
            NormalItemId = 30592,
            Expansion = ExpansionEnum.ShB,
            ResultItems = new()
            {
                new() { ItemId = 30590 }, // Levinstrike Aethersand
                new() { ItemId = 12 },    // Lightning Crystal
                new() { ItemId = 18 },    // Lightning Cluster
            },
        },
        new() // Lunar Quartz
        {
            NormalItemId = 36285,
            Expansion = ExpansionEnum.EW,
            ResultItems = new()
            {
                new() { ItemId = 36223 }, // Moonlight Aethersand
                new() { ItemId = 11 },    // Earth Crystal
                new() { ItemId = 17 },    // Earth Cluster
            },
        },
        new() // Ewer Clay
        {
            NormalItemId = 36287,
            Expansion = ExpansionEnum.EW,
            ResultItems = new()
            {
                new() { ItemId = 36223 }, // Moonlight Aethersand
                new() { ItemId = 10 },    // Wind Crystal
                new() { ItemId = 16 },    // Wind Cluster
            },
        },
        new() // Gilled Topknot
        {
            NormalItemId = 36525,
            Expansion = ExpansionEnum.EW,
            ResultItems = new()
            {
                new() { ItemId = 36223 }, // Moonlight Aethersand
                new() { ItemId = 9 },     // Ice Crystal
                new() { ItemId = 15 },    // Ice Cluster
            },
        },
        new() // Verdigris Guppy
        {
            NormalItemId = 38939,
            Expansion = ExpansionEnum.EW,
            ResultItems = new()
            {
                new() { ItemId = 38936 }, // Earthbreak Aethersand
                new() { ItemId = 9 },     // Ice Crystal
                new() { ItemId = 15 },    // Ice Cluster
            },
        },
        new() // Othardian Lumpsucker
        {
            NormalItemId = 36577,
            Expansion = ExpansionEnum.EW,
            ResultItems = new()
            {
                new() { ItemId = 36226 }, // Endtide Aethersand
                new() { ItemId = 13 },    // Water Crystal
                new() { ItemId = 19 },    // Water Cluster
            },
        },
        new() // Ghostly Umbral Rock
        {
            NormalItemId = 36286,
            Expansion = ExpansionEnum.EW,
            ResultItems = new()
            {
                new() { ItemId = 36224 }, // Endstone Aethersand
                new() { ItemId = 12 },    // Lightning Crystal
                new() { ItemId = 18 },    // Lightning Cluster
            },
        },
        new() // Palm Chippings
        {
            NormalItemId = 36288,
            Expansion = ExpansionEnum.EW,
            ResultItems = new()
            {
                new() { ItemId = 36225 }, // Endwood Aethersand
                new() { ItemId = 8 },     // Fire Crystal
                new() { ItemId = 14 },    // Fire Cluster
            },
        },
        new() // Phyllinos
        {
            NormalItemId = 39240,
            Expansion = ExpansionEnum.EW,
            ResultItems = new()
            {
                new() { ItemId = 39241 }, // Pure Aqueous Glioaether
                new() { ItemId = 9 },     // Ice Crystal
                new() { ItemId = 15 },    // Ice Cluster
            },
        },
        new() // Siderite
        {
            PrimeItemId = 37694,
            SublimeItemId = 37695,
            Expansion = ExpansionEnum.EW,
            ResultItems = new()
            {
                new() { ItemId = 37696 }, // Igneous Glioaether
                new() { ItemId = 11 },    // Earth Crystal
                new() { ItemId = 17 },    // Earth Cluster
            },
        },
        new() // Earthen Quartz
        {
            NormalItemId = 38937,
            Expansion = ExpansionEnum.EW,
            ResultItems = new()
            {
                new() { ItemId = 38936 }, // Earthbreak Aethersand
                new() { ItemId = 11 },    // Earth Crystal
                new() { ItemId = 17 },    // Earth Cluster
            },
        },
        new() // Crystalbloom
        {
            PrimeItemId = 37691,
            SublimeItemId = 37692,
            Expansion = ExpansionEnum.EW,
            ResultItems = new()
            {
                new() { ItemId = 37693 }, // Verdurous Glioaether
                new() { ItemId = 10 },    // Wind Crystal
                new() { ItemId = 16 },    // Wind Cluster
            },
        },
        new() // Sophora Roots
        {
            NormalItemId = 38938,
            Expansion = ExpansionEnum.EW,
            ResultItems = new()
            {
                new() { ItemId = 38936 }, // Earthbreak Aethersand
                new() { ItemId = 8 },     // Fire Crystal
                new() { ItemId = 14 },    // Fire Cluster
            },
        },
        new() // Mayashell
        {
            NormalItemId = 37697,
            Expansion = ExpansionEnum.EW,
            ResultItems = new()
            {
                new() { ItemId = 37698 }, // Aqueous Glioaether
                new() { ItemId = 9 },     // Ice Crystal
                new() { ItemId = 15 },    // Ice Cluster
            },
        },
        new() // Sphongos
        {
            PrimeItemId = 39234,
            SublimeItemId = 39235,
            Expansion = ExpansionEnum.EW,
            ResultItems = new()
            {
                new() { ItemId = 39236 }, // Pure Verdurous Glioaether
                new() { ItemId = 13 },    // Water Crystal
                new() { ItemId = 19 },    // Water Cluster
            },
        },
        new() // Connoisseur's Miracle Apple
        {
            NormalItemId = 39807,
            Expansion = ExpansionEnum.EW,
            ResultItems = new()
            {
                new() { ItemId = 39818 }, // Customized Botanist's Component
                new() { ItemId = 13 },    // Water Crystal
                new() { ItemId = 19 },    // Water Cluster
            },
        },
        new() // Achondrite
        {
            PrimeItemId = 39237,
            SublimeItemId = 39238,
            Expansion = ExpansionEnum.EW,
            ResultItems = new()
            {
                new() { ItemId = 39239 }, // Pure Igneous Glioaether
                new() { ItemId = 12 },    // Lightning Crystal
                new() { ItemId = 18 },    // Lightning Cluster
            },
        },
        new() // Connoisseur's Soiled Femur
        {
            NormalItemId = 39805,
            Expansion = ExpansionEnum.EW,
            ResultItems = new()
            {
                new() { ItemId = 39817 }, // Customized Miner's Component
                new() { ItemId = 12 },    // Lightning Crystal
                new() { ItemId = 18 },    // Lightning Cluster
            },
        },
        new() // Chloroschist
        {
            PrimeItemId = 39909,
            SublimeItemId = 39910,
            Expansion = ExpansionEnum.EW,
            ResultItems = new()
            {
                new() { ItemId = 39908 }, // Concentrated Verdurous Glioaether
                new() { ItemId = 13 },    // Water Crystal
                new() { ItemId = 19 },    // Water Cluster
            },
        },
        new() // Haritaki
        {
            PrimeItemId = 39906,
            SublimeItemId = 39907,
            Expansion = ExpansionEnum.EW,
            ResultItems = new()
            {
                new() { ItemId = 39911 }, // Concentrated Igneous Glioaether
                new() { ItemId = 8 },     // Fire Crystal
                new() { ItemId = 14 },    // Fire Cluster
            },
        },
        new() // The Fury's Aegis
        {
            NormalItemId = 39912,
            Expansion = ExpansionEnum.EW,
            ResultItems = new()
            {
                new() { ItemId = 39913 }, // Concentrated Aqueous Glioaether
                new() { ItemId = 9 },     // Ice Crystal
                new() { ItemId = 15 },    // Ice Cluster (FIXED: was duplicated as ItemId 9 in the old data)
            },
        },
        new() // Fossilized Dragon's Scale
        {
            PrimeItemId = 41416,
            SublimeItemId = 41417,
            Expansion = ExpansionEnum.EW,
            ResultItems = new()
            {
                new() { ItemId = 41415 }, // Potent Verdurous Glioaether
                new() { ItemId = 10 },    // Wind Crystal
                new() { ItemId = 16 },    // Wind Cluster
            },
        },
        new() // Kukuru Beans
        {
            PrimeItemId = 41413,
            SublimeItemId = 41414,
            Expansion = ExpansionEnum.EW,
            ResultItems = new()
            {
                new() { ItemId = 41418 }, // Potent Igneous Glioaether
                new() { ItemId = 10 },    // Wind Crystal
                new() { ItemId = 16 },    // Wind Cluster
            },
        },
        new() // Stargilt Lobster
        {
            NormalItemId = 41419,
            Expansion = ExpansionEnum.EW,
            ResultItems = new()
            {
                new() { ItemId = 41420 }, // Potent Aqueous Glioaether
                new() { ItemId = 9 },     // Ice Crystal
                new() { ItemId = 15 },    // Ice Cluster
            },
        },
        new() // Electrocoal
        {
            NormalItemId = 43931,
            Expansion = ExpansionEnum.DT,
            ResultItems = new()
            {
                new() { ItemId = 44035 }, // Sungilt Aethersand
                new() { ItemId = 11 },    // Earth Crystal
                new() { ItemId = 17 },    // Earth Cluster
            },
        },
        new() // Goldbranch
        {
            NormalItemId = 43933,
            Expansion = ExpansionEnum.DT,
            ResultItems = new()
            {
                new() { ItemId = 44035 }, // Sungilt Aethersand
                new() { ItemId = 10 },    // Wind Crystal
                new() { ItemId = 16 },    // Wind Cluster
            },
        },
        new() // Longnose Gar
        {
            NormalItemId = 43847,
            Expansion = ExpansionEnum.DT,
            ResultItems = new()
            {
                new() { ItemId = 44038 }, // Mythbrine Aethersand
                new() { ItemId = 9 },     // Ice Crystal
                new() { ItemId = 15 },    // Ice Cluster
            },
        },
        new() // Sunlit Prism
        {
            NormalItemId = 43829,
            Expansion = ExpansionEnum.DT,
            ResultItems = new()
            {
                new() { ItemId = 44035 }, // Sungilt Aethersand
                new() { ItemId = 13 },    // Water Crystal
                new() { ItemId = 19 },    // Water Cluster
            },
        },
        new() // Brightwind Ore
        {
            NormalItemId = 43932,
            Expansion = ExpansionEnum.DT,
            ResultItems = new()
            {
                new() { ItemId = 44036 }, // Mythloam Aethersand
                new() { ItemId = 12 },    // Lightning Crystal
                new() { ItemId = 18 },    // Lightning Cluster
            },
        },
        new() // Volcanic Grass
        {
            NormalItemId = 43934,
            Expansion = ExpansionEnum.DT,
            ResultItems = new()
            {
                new() { ItemId = 44037 }, // Mythroot Aethersand
                new() { ItemId = 8 },     // Fire Crystal
                new() { ItemId = 14 },    // Fire Cluster
            },
        },
        new() // Purple Palate
        {
            NormalItemId = 46249,
            Expansion = ExpansionEnum.DT,
            ResultItems = new()
            {
                new() { ItemId = 46246 }, // Levinchrome Aethersand
                new() { ItemId = 9 },     // Ice Crystal
                new() { ItemId = 15 },    // Ice Cluster
            },
        },
        new() // Levin Quartz
        {
            NormalItemId = 46247,
            Expansion = ExpansionEnum.DT,
            ResultItems = new()
            {
                new() { ItemId = 46246 }, // Levinchrome Aethersand
                new() { ItemId = 12 },    // Lightning Crystal
                new() { ItemId = 18 },    // Lightning Cluster
            },
        },
        new() // Calamus Root
        {
            NormalItemId = 46248,
            Expansion = ExpansionEnum.DT,
            ResultItems = new()
            {
                new() { ItemId = 46246 }, // Levinchrome Aethersand
                new() { ItemId = 12 },    // Lightning Crystal
                new() { ItemId = 18 },    // Lightning Cluster
            },
        },
    };
}