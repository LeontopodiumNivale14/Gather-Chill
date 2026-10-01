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
        // Normal or Prime item (an entry only ever has one of the two)
        public uint ItemId { get; set; } = 0;
        public uint SublimeItemId { get; set; } = 0;
        public ExpansionIds Expansion { get; set; } = ExpansionIds.ARR;
        public List<ReduceClass> ResultItems { get; set; } = new();

        public IEnumerable<uint> AllItemIds()
        {
            if (ItemId != 0) yield return ItemId;
            if (SublimeItemId != 0) yield return SublimeItemId;
        }

        public Item ItemInfo(uint itemId) => ExcelHelper.Sheet_Item.GetRow(itemId);
    };

    public static List<ReduceInfo> ReducableItems = new()
    {
        new() // Granular Clay
        {
            ItemId = 12968,
            Expansion = ExpansionIds.HW,
            ResultItems = new()
            {
                new() { ItemId = 12936 }, // Duskborne Aethersand
                new() { ItemId = 11 },    // Earth Crystal
                new() { ItemId = 17 },    // Earth Cluster
            },
        },
        new() // Lightning Moraine
        {
            ItemId = 5218,
            Expansion = ExpansionIds.HW,
            ResultItems = new()
            {
                new() { ItemId = 12936 }, // Duskborne Aethersand
                new() { ItemId = 12 },    // Lightning Crystal
                new() { ItemId = 18 },    // Lightning Cluster
            },
        },
        new() // Pot Marjoram
        {
            ItemId = 33148,
            Expansion = ExpansionIds.HW,
            ResultItems = new()
            {
                new() { ItemId = 12936 }, // Duskborne Aethersand
                new() { ItemId = 10 },    // Wind Crystal
                new() { ItemId = 16 },    // Wind Cluster
            },
        },
        new() // Fire Moraine
        {
            ItemId = 5214,
            Expansion = ExpansionIds.HW,
            ResultItems = new()
            {
                new() { ItemId = 12936 }, // Duskborne Aethersand
                new() { ItemId = 8 },     // Fire Crystal
                new() { ItemId = 14 },    // Fire Cluster
            },
        },
        new() // Peat Moss
        {
            ItemId = 12969,
            Expansion = ExpansionIds.HW,
            ResultItems = new()
            {
                new() { ItemId = 12937 }, // Dawnborne Aethersand
                new() { ItemId = 11 },    // Earth Crystal
                new() { ItemId = 17 },    // Earth Cluster
            },
        },
        new() // Bright Lightning Rock
        {
            ItemId = 12967,
            Expansion = ExpansionIds.HW,
            ResultItems = new()
            {
                new() { ItemId = 12937 }, // Dawnborne Aethersand
                new() { ItemId = 12 },    // Lightning Crystal
                new() { ItemId = 18 },    // Lightning Cluster
            },
        },
        new() // Water Mint
        {
            ItemId = 33149,
            Expansion = ExpansionIds.HW,
            ResultItems = new()
            {
                new() { ItemId = 12937 }, // Dawnborne Aethersand
                new() { ItemId = 10 },    // Wind Crystal
                new() { ItemId = 16 },    // Wind Cluster
            },
        },
        new() // Bright Fire Rock
        {
            ItemId = 12966,
            Expansion = ExpansionIds.HW,
            ResultItems = new()
            {
                new() { ItemId = 12937 }, // Dawnborne Aethersand
                new() { ItemId = 8 },     // Fire Crystal
                new() { ItemId = 14 },    // Fire Cluster
            },
        },
        new() // Humic Soil
        {
            ItemId = 33147,
            Expansion = ExpansionIds.HW,
            ResultItems = new()
            {
                new() { ItemId = 12939 }, // Leafborne Aethersand
                new() { ItemId = 11 },    // Earth Crystal
                new() { ItemId = 17 },    // Earth Cluster
            },
        },
        new() // Radiant Lightning Moraine
        {
            ItemId = 5224,
            Expansion = ExpansionIds.HW,
            ResultItems = new()
            {
                new() { ItemId = 12938 }, // Landborne Aethersand
                new() { ItemId = 12 },    // Lightning Crystal
                new() { ItemId = 18 },    // Lightning Cluster
            },
        },
        new() // Wild Sage
        {
            ItemId = 33150,
            Expansion = ExpansionIds.HW,
            ResultItems = new()
            {
                new() { ItemId = 12939 }, // Leafborne Aethersand
                new() { ItemId = 10 },    // Wind Crystal
                new() { ItemId = 16 },    // Wind Cluster
            },
        },
        new() // Radiant Fire Moraine
        {
            ItemId = 5220,
            Expansion = ExpansionIds.HW,
            ResultItems = new()
            {
                new() { ItemId = 12938 }, // Landborne Aethersand
                new() { ItemId = 8 },     // Fire Crystal
                new() { ItemId = 14 },    // Fire Cluster
            },
        },
        new() // Lover's Laurel
        {
            ItemId = 15948,
            Expansion = ExpansionIds.HW,
            ResultItems = new()
            {
                new() { ItemId = 15648 }, // Light-kissed Aethersand
                new() { ItemId = 9 },     // Ice Crystal
                new() { ItemId = 15 },    // Ice Cluster
            },
        },
        new() // Radiant Astral Moraine
        {
            ItemId = 15949,
            Expansion = ExpansionIds.HW,
            ResultItems = new()
            {
                new() { ItemId = 15648 }, // Light-kissed Aethersand
                new() { ItemId = 13 },    // Water Crystal
                new() { ItemId = 19 },    // Water Cluster
            },
        },
        new() // Dacite
        {
            ItemId = 33152,
            Expansion = ExpansionIds.StB,
            ResultItems = new()
            {
                new() { ItemId = 20015 }, // Everbright Aethersand
                new() { ItemId = 13 },    // Water Crystal
                new() { ItemId = 19 },    // Water Cluster
            },
        },
        new() // Doman Yellow
        {
            ItemId = 20012,
            Expansion = ExpansionIds.StB,
            ResultItems = new()
            {
                new() { ItemId = 20013 }, // Dusklight Aethersand
                new() { ItemId = 11 },    // Earth Crystal
                new() { ItemId = 17 },    // Earth Cluster
            },
        },
        new() // Schorl
        {
            ItemId = 20009,
            Expansion = ExpansionIds.StB,
            ResultItems = new()
            {
                new() { ItemId = 20014 }, // Dawnlight Aethersand
                new() { ItemId = 12 },    // Lightning Crystal
                new() { ItemId = 18 },    // Lightning Cluster
            },
        },
        new() // Countess Tea Leaves
        {
            ItemId = 33151,
            Expansion = ExpansionIds.StB,
            ResultItems = new()
            {
                new() { ItemId = 20014 }, // Dawnlight Aethersand
                new() { ItemId = 10 },    // Wind Crystal
                new() { ItemId = 16 },    // Wind Cluster
            },
        },
        new() // Torreya Branch
        {
            ItemId = 19937,
            Expansion = ExpansionIds.StB,
            ResultItems = new()
            {
                new() { ItemId = 20016 }, // Everborn Aethersand
                new() { ItemId = 9 },     // Ice Crystal
                new() { ItemId = 15 },    // Ice Cluster
            },
        },
        new() // Rhodolite
        {
            ItemId = 33153,
            Expansion = ExpansionIds.StB,
            ResultItems = new()
            {
                new() { ItemId = 20013 }, // Dusklight Aethersand
                new() { ItemId = 8 },     // Fire Crystal
                new() { ItemId = 14 },    // Fire Cluster
            },
        },
        new() // Yanxian Verbena
        {
            ItemId = 23221,
            Expansion = ExpansionIds.StB,
            ResultItems = new()
            {
                new() { ItemId = 23182 }, // Duskglow Aethersand
                new() { ItemId = 12 },    // Lightning Crystal
                new() { ItemId = 18 },    // Lightning Cluster
            },
        },
        new() // Yanxian Soil
        {
            ItemId = 23220,
            Expansion = ExpansionIds.StB,
            ResultItems = new()
            {
                new() { ItemId = 23182 }, // Duskglow Aethersand
                new() { ItemId = 10 },    // Wind Crystal
                new() { ItemId = 16 },    // Wind Cluster
            },
        },
        new() // Voeburt Bichir
        {
            ItemId = 27542,
            Expansion = ExpansionIds.ShB,
            ResultItems = new()
            {
                new() { ItemId = 27811 }, // Chiaroglow Aethersand
                new() { ItemId = 9 },     // Ice Crystal
                new() { ItemId = 15 },    // Ice Cluster
            },
        },
        new() // Poecilia
        {
            ItemId = 27543,
            Expansion = ExpansionIds.ShB,
            ResultItems = new()
            {
                new() { ItemId = 27812 }, // Scuroglow Aethersand
                new() { ItemId = 13 },    // Water Crystal
                new() { ItemId = 19 },    // Water Cluster
            },
        },
        new() // Gale Rock
        {
            ItemId = 27805,
            Expansion = ExpansionIds.ShB,
            ResultItems = new()
            {
                new() { ItemId = 27811 }, // Chiaroglow Aethersand
                new() { ItemId = 13 },    // Water Crystal
                new() { ItemId = 19 },    // Water Cluster
            },
        },
        new() // White Clay
        {
            ItemId = 27808,
            Expansion = ExpansionIds.ShB,
            ResultItems = new()
            {
                new() { ItemId = 27811 }, // Chiaroglow Aethersand
                new() { ItemId = 11 },    // Earth Crystal
                new() { ItemId = 17 },    // Earth Cluster
            },
        },
        new() // Solarite
        {
            ItemId = 27806,
            Expansion = ExpansionIds.ShB,
            ResultItems = new()
            {
                new() { ItemId = 27812 }, // Scuroglow Aethersand
                new() { ItemId = 12 },    // Lightning Crystal
                new() { ItemId = 18 },    // Lightning Cluster
            },
        },
        new() // Sweet Marjoram
        {
            ItemId = 27809,
            Expansion = ExpansionIds.ShB,
            ResultItems = new()
            {
                new() { ItemId = 27812 }, // Scuroglow Aethersand
                new() { ItemId = 10 },    // Wind Crystal
                new() { ItemId = 16 },    // Wind Cluster
            },
        },
        new() // Bog Sage
        {
            ItemId = 27810,
            Expansion = ExpansionIds.ShB,
            ResultItems = new()
            {
                new() { ItemId = 27814 }, // Agewood Aethersand
                new() { ItemId = 9 },     // Ice Crystal
                new() { ItemId = 15 },    // Ice Cluster
            },
        },
        new() // Shade Quartz
        {
            ItemId = 27807,
            Expansion = ExpansionIds.ShB,
            ResultItems = new()
            {
                new() { ItemId = 27813 }, // Agedeep Aethersand
                new() { ItemId = 8 },     // Fire Crystal
                new() { ItemId = 14 },    // Fire Cluster
            },
        },
        new() // Fuchsia Bloom
        {
            ItemId = 30593,
            Expansion = ExpansionIds.ShB,
            ResultItems = new()
            {
                new() { ItemId = 30590 }, // Levinstrike Aethersand
                new() { ItemId = 9 },     // Ice Crystal
                new() { ItemId = 15 },    // Ice Cluster
            },
        },
        new() // Thunder Rock
        {
            ItemId = 30591,
            Expansion = ExpansionIds.ShB,
            ResultItems = new()
            {
                new() { ItemId = 30590 }, // Levinstrike Aethersand
                new() { ItemId = 11 },    // Earth Crystal
                new() { ItemId = 17 },    // Earth Cluster
            },
        },
        new() // Levin Mint
        {
            ItemId = 30592,
            Expansion = ExpansionIds.ShB,
            ResultItems = new()
            {
                new() { ItemId = 30590 }, // Levinstrike Aethersand
                new() { ItemId = 12 },    // Lightning Crystal
                new() { ItemId = 18 },    // Lightning Cluster
            },
        },
        new() // Lunar Quartz
        {
            ItemId = 36285,
            Expansion = ExpansionIds.EW,
            ResultItems = new()
            {
                new() { ItemId = 36223 }, // Moonlight Aethersand
                new() { ItemId = 11 },    // Earth Crystal
                new() { ItemId = 17 },    // Earth Cluster
            },
        },
        new() // Ewer Clay
        {
            ItemId = 36287,
            Expansion = ExpansionIds.EW,
            ResultItems = new()
            {
                new() { ItemId = 36223 }, // Moonlight Aethersand
                new() { ItemId = 10 },    // Wind Crystal
                new() { ItemId = 16 },    // Wind Cluster
            },
        },
        new() // Gilled Topknot
        {
            ItemId = 36525,
            Expansion = ExpansionIds.EW,
            ResultItems = new()
            {
                new() { ItemId = 36223 }, // Moonlight Aethersand
                new() { ItemId = 9 },     // Ice Crystal
                new() { ItemId = 15 },    // Ice Cluster
            },
        },
        new() // Verdigris Guppy
        {
            ItemId = 38939,
            Expansion = ExpansionIds.EW,
            ResultItems = new()
            {
                new() { ItemId = 38936 }, // Earthbreak Aethersand
                new() { ItemId = 9 },     // Ice Crystal
                new() { ItemId = 15 },    // Ice Cluster
            },
        },
        new() // Othardian Lumpsucker
        {
            ItemId = 36577,
            Expansion = ExpansionIds.EW,
            ResultItems = new()
            {
                new() { ItemId = 36226 }, // Endtide Aethersand
                new() { ItemId = 13 },    // Water Crystal
                new() { ItemId = 19 },    // Water Cluster
            },
        },
        new() // Ghostly Umbral Rock
        {
            ItemId = 36286,
            Expansion = ExpansionIds.EW,
            ResultItems = new()
            {
                new() { ItemId = 36224 }, // Endstone Aethersand
                new() { ItemId = 12 },    // Lightning Crystal
                new() { ItemId = 18 },    // Lightning Cluster
            },
        },
        new() // Palm Chippings
        {
            ItemId = 36288,
            Expansion = ExpansionIds.EW,
            ResultItems = new()
            {
                new() { ItemId = 36225 }, // Endwood Aethersand
                new() { ItemId = 8 },     // Fire Crystal
                new() { ItemId = 14 },    // Fire Cluster
            },
        },
        new() // Phyllinos
        {
            ItemId = 39240,
            Expansion = ExpansionIds.EW,
            ResultItems = new()
            {
                new() { ItemId = 39241 }, // Pure Aqueous Glioaether
                new() { ItemId = 9 },     // Ice Crystal
                new() { ItemId = 15 },    // Ice Cluster
            },
        },
        new() // Siderite
        {
            ItemId = 37694,
            SublimeItemId = 37695,
            Expansion = ExpansionIds.EW,
            ResultItems = new()
            {
                new() { ItemId = 37696 }, // Igneous Glioaether
                new() { ItemId = 11 },    // Earth Crystal
                new() { ItemId = 17 },    // Earth Cluster
            },
        },
        new() // Earthen Quartz
        {
            ItemId = 38937,
            Expansion = ExpansionIds.EW,
            ResultItems = new()
            {
                new() { ItemId = 38936 }, // Earthbreak Aethersand
                new() { ItemId = 11 },    // Earth Crystal
                new() { ItemId = 17 },    // Earth Cluster
            },
        },
        new() // Crystalbloom
        {
            ItemId = 37691,
            SublimeItemId = 37692,
            Expansion = ExpansionIds.EW,
            ResultItems = new()
            {
                new() { ItemId = 37693 }, // Verdurous Glioaether
                new() { ItemId = 10 },    // Wind Crystal
                new() { ItemId = 16 },    // Wind Cluster
            },
        },
        new() // Sophora Roots
        {
            ItemId = 38938,
            Expansion = ExpansionIds.EW,
            ResultItems = new()
            {
                new() { ItemId = 38936 }, // Earthbreak Aethersand
                new() { ItemId = 8 },     // Fire Crystal
                new() { ItemId = 14 },    // Fire Cluster
            },
        },
        new() // Mayashell
        {
            ItemId = 37697,
            Expansion = ExpansionIds.EW,
            ResultItems = new()
            {
                new() { ItemId = 37698 }, // Aqueous Glioaether
                new() { ItemId = 9 },     // Ice Crystal
                new() { ItemId = 15 },    // Ice Cluster
            },
        },
        new() // Sphongos
        {
            ItemId = 39234,
            SublimeItemId = 39235,
            Expansion = ExpansionIds.EW,
            ResultItems = new()
            {
                new() { ItemId = 39236 }, // Pure Verdurous Glioaether
                new() { ItemId = 13 },    // Water Crystal
                new() { ItemId = 19 },    // Water Cluster
            },
        },
        new() // Connoisseur's Miracle Apple
        {
            ItemId = 39807,
            Expansion = ExpansionIds.EW,
            ResultItems = new()
            {
                new() { ItemId = 39818 }, // Customized Botanist's Component
                new() { ItemId = 13 },    // Water Crystal
                new() { ItemId = 19 },    // Water Cluster
            },
        },
        new() // Achondrite
        {
            ItemId = 39237,
            SublimeItemId = 39238,
            Expansion = ExpansionIds.EW,
            ResultItems = new()
            {
                new() { ItemId = 39239 }, // Pure Igneous Glioaether
                new() { ItemId = 12 },    // Lightning Crystal
                new() { ItemId = 18 },    // Lightning Cluster
            },
        },
        new() // Connoisseur's Soiled Femur
        {
            ItemId = 39805,
            Expansion = ExpansionIds.EW,
            ResultItems = new()
            {
                new() { ItemId = 39817 }, // Customized Miner's Component
                new() { ItemId = 12 },    // Lightning Crystal
                new() { ItemId = 18 },    // Lightning Cluster
            },
        },
        new() // Chloroschist
        {
            ItemId = 39909,
            SublimeItemId = 39910,
            Expansion = ExpansionIds.EW,
            ResultItems = new()
            {
                new() { ItemId = 39908 }, // Concentrated Verdurous Glioaether
                new() { ItemId = 13 },    // Water Crystal
                new() { ItemId = 19 },    // Water Cluster
            },
        },
        new() // Haritaki
        {
            ItemId = 39906,
            SublimeItemId = 39907,
            Expansion = ExpansionIds.EW,
            ResultItems = new()
            {
                new() { ItemId = 39911 }, // Concentrated Igneous Glioaether
                new() { ItemId = 8 },     // Fire Crystal
                new() { ItemId = 14 },    // Fire Cluster
            },
        },
        new() // The Fury's Aegis
        {
            ItemId = 39912,
            Expansion = ExpansionIds.EW,
            ResultItems = new()
            {
                new() { ItemId = 39913 }, // Concentrated Aqueous Glioaether
                new() { ItemId = 9 },     // Ice Crystal
                new() { ItemId = 15 },    // Ice Cluster
            },
        },
        new() // Fossilized Dragon's Scale
        {
            ItemId = 41416,
            SublimeItemId = 41417,
            Expansion = ExpansionIds.EW,
            ResultItems = new()
            {
                new() { ItemId = 41415 }, // Potent Verdurous Glioaether
                new() { ItemId = 10 },    // Wind Crystal
                new() { ItemId = 16 },    // Wind Cluster
            },
        },
        new() // Kukuru Beans
        {
            ItemId = 41413,
            SublimeItemId = 41414,
            Expansion = ExpansionIds.EW,
            ResultItems = new()
            {
                new() { ItemId = 41418 }, // Potent Igneous Glioaether
                new() { ItemId = 10 },    // Wind Crystal
                new() { ItemId = 16 },    // Wind Cluster
            },
        },
        new() // Stargilt Lobster
        {
            ItemId = 41419,
            Expansion = ExpansionIds.EW,
            ResultItems = new()
            {
                new() { ItemId = 41420 }, // Potent Aqueous Glioaether
                new() { ItemId = 9 },     // Ice Crystal
                new() { ItemId = 15 },    // Ice Cluster
            },
        },
        new() // Electrocoal
        {
            ItemId = 43931,
            Expansion = ExpansionIds.DT,
            ResultItems = new()
            {
                new() { ItemId = 44035 }, // Sungilt Aethersand
                new() { ItemId = 11 },    // Earth Crystal
                new() { ItemId = 17 },    // Earth Cluster
            },
        },
        new() // Goldbranch
        {
            ItemId = 43933,
            Expansion = ExpansionIds.DT,
            ResultItems = new()
            {
                new() { ItemId = 44035 }, // Sungilt Aethersand
                new() { ItemId = 10 },    // Wind Crystal
                new() { ItemId = 16 },    // Wind Cluster
            },
        },
        new() // Longnose Gar
        {
            ItemId = 43847,
            Expansion = ExpansionIds.DT,
            ResultItems = new()
            {
                new() { ItemId = 44038 }, // Mythbrine Aethersand
                new() { ItemId = 9 },     // Ice Crystal
                new() { ItemId = 15 },    // Ice Cluster
            },
        },
        new() // Sunlit Prism
        {
            ItemId = 43829,
            Expansion = ExpansionIds.DT,
            ResultItems = new()
            {
                new() { ItemId = 44035 }, // Sungilt Aethersand
                new() { ItemId = 13 },    // Water Crystal
                new() { ItemId = 19 },    // Water Cluster
            },
        },
        new() // Brightwind Ore
        {
            ItemId = 43932,
            Expansion = ExpansionIds.DT,
            ResultItems = new()
            {
                new() { ItemId = 44036 }, // Mythloam Aethersand
                new() { ItemId = 12 },    // Lightning Crystal
                new() { ItemId = 18 },    // Lightning Cluster
            },
        },
        new() // Volcanic Grass
        {
            ItemId = 43934,
            Expansion = ExpansionIds.DT,
            ResultItems = new()
            {
                new() { ItemId = 44037 }, // Mythroot Aethersand
                new() { ItemId = 8 },     // Fire Crystal
                new() { ItemId = 14 },    // Fire Cluster
            },
        },
        new() // Purple Palate
        {
            ItemId = 46249,
            Expansion = ExpansionIds.DT,
            ResultItems = new()
            {
                new() { ItemId = 46246 }, // Levinchrome Aethersand
                new() { ItemId = 9 },     // Ice Crystal
                new() { ItemId = 15 },    // Ice Cluster
            },
        },
        new() // Levin Quartz
        {
            ItemId = 46247,
            Expansion = ExpansionIds.DT,
            ResultItems = new()
            {
                new() { ItemId = 46246 }, // Levinchrome Aethersand
                new() { ItemId = 12 },    // Lightning Crystal
                new() { ItemId = 18 },    // Lightning Cluster
            },
        },
        new() // Calamus Root
        {
            ItemId = 46248,
            Expansion = ExpansionIds.DT,
            ResultItems = new()
            {
                new() { ItemId = 46246 }, // Levinchrome Aethersand
                new() { ItemId = 12 },    // Lightning Crystal
                new() { ItemId = 18 },    // Lightning Cluster
            },
        },
    };
}