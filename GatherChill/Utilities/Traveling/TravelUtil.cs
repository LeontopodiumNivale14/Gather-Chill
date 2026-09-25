using GatherChill.GatheringInfo;
using System.Collections.Generic;

namespace GatherChill.Utilities.Traveling;

public static partial class TravelUtil
{
    public class AethershardInfo
    {
        public uint ShardId { get; set; }
        public uint SelectionId { get; set; }
        public Vector3 Shard_Position { get; set; } = Vector3.Zero;
        public uint TerritoryId { get; set; } = 0;
        public List<FanInfo> Multi_Destinations { get; set; } = new();
        public FanInfo DestinationFan { get; set; } = new();
    }

    public static Dictionary<uint, Dictionary<uint, AethershardInfo>> AetherDictionary = new()
    {
        // Crystarium
        [819] = new()
        {
            [133] = new()
            {
                ShardId = 133,
                SelectionId = 0,
                Shard_Position = new Vector3(-65.0188f, 4.5318604f, 0.015197754f),
                TerritoryId = 819,
                DestinationFan = new()
                {
                    Fan_StartAngle = 39f,
                    Fan_EndAngle = 38f,
                    Fan_DistanceMin = 4.2f,
                    Fan_DistanceMax = 9f,
                    Fan_Height = 0f,
                },
                Multi_Destinations = new()
                {
                },
            },
            [149] = new()
            {
                ShardId = 149,
                SelectionId = 1,
                Shard_Position = new Vector3(-6.149414f, -7.736328f, 148.72961f),
                TerritoryId = 819,
                DestinationFan = new()
                {
                    Fan_StartAngle = 160f,
                    Fan_EndAngle = 159f,
                    Fan_DistanceMin = 1f,
                    Fan_DistanceMax = 3f,
                    Fan_Height = 0.44f,
                },
                Multi_Destinations = new()
                {
                },
            },
            [150] = new()
            {
                ShardId = 150,
                SelectionId = 2,
                Shard_Position = new Vector3(-107.37775f, -0.015319824f, -58.762512f),
                TerritoryId = 819,
                DestinationFan = new()
                {
                    Fan_StartAngle = 153f,
                    Fan_EndAngle = 72f,
                    Fan_DistanceMin = 1f,
                    Fan_DistanceMax = 3f,
                    Fan_Height = 0.22f,
                },
                Multi_Destinations = new()
                {
                },
            },
            [151] = new()
            {
                ShardId = 151,
                SelectionId = 3,
                Shard_Position = new Vector3(64.86609f, -0.015319824f, -18.173523f),
                TerritoryId = 819,
                DestinationFan = new()
                {
                    Fan_StartAngle = 94f,
                    Fan_EndAngle = 93f,
                    Fan_DistanceMin = 1f,
                    Fan_DistanceMax = 3f,
                    Fan_Height = 0.88f,
                },
                Multi_Destinations = new()
                {
                },
            },
            [152] = new()
            {
                ShardId = 152,
                SelectionId = 4,
                Shard_Position = new Vector3(35.477173f, -0.015319824f, 222.58337f),
                TerritoryId = 819,
                DestinationFan = new()
                {
                    Fan_StartAngle = 165f,
                    Fan_EndAngle = 40f,
                    Fan_DistanceMin = 1f,
                    Fan_DistanceMax = 3f,
                    Fan_Height = 0.72f,
                },
                Multi_Destinations = new()
                {
                },
            },
            [153] = new()
            {
                ShardId = 153,
                SelectionId = 5,
                Shard_Position = new Vector3(66.60559f, 35.99597f, -131.09033f),
                TerritoryId = 819,
                DestinationFan = new()
                {
                    Fan_StartAngle = 206f,
                    Fan_EndAngle = 97f,
                    Fan_DistanceMin = 1f,
                    Fan_DistanceMax = 3f,
                    Fan_Height = 0.2f,
                },
                Multi_Destinations = new()
                {
                },
            },
            [154] = new()
            {
                ShardId = 154,
                SelectionId = 6,
                Shard_Position = new Vector3(-52.506348f, 19.97406f, -173.35773f),
                TerritoryId = 819,
                DestinationFan = new()
                {
                    Fan_StartAngle = 63f,
                    Fan_EndAngle = 301f,
                    Fan_DistanceMin = 1f,
                    Fan_DistanceMax = 3f,
                    Fan_Height = 0.32f,
                },
                Multi_Destinations = new()
                {
                },
            },
            [155] = new()
            {
                ShardId = 155,
                SelectionId = 7,
                Shard_Position = new Vector3(-54.398438f, -37.70508f, -241.07733f),
                TerritoryId = 819,
                DestinationFan = new()
                {
                    Fan_StartAngle = 148f,
                    Fan_EndAngle = 25f,
                    Fan_DistanceMin = 1f,
                    Fan_DistanceMax = 3f,
                    Fan_Height = 1.04f,
                },
                Multi_Destinations = new()
                {
                },
            },
        },


        // Old Shar
        [962] = new()
        {
            [182] = new()
            {
                ShardId = 182,
                SelectionId = 0,
                Shard_Position = new Vector3(0.07623291f, 4.8065186f, -0.10687256f),
                TerritoryId = 962,
                DestinationFan = new()
                {
                    Fan_StartAngle = 0f,
                    Fan_EndAngle = 359f,
                    Fan_DistanceMin = 3.9f,
                    Fan_DistanceMax = 9f,
                    Fan_Height = 0f,
                },
                Multi_Destinations = new()
                {
                },
            },
            [184] = new()
            {
                ShardId = 184,
                SelectionId = 1,
                Shard_Position = new Vector3(-291.1574f, 20.004517f, -74.143616f),
                TerritoryId = 962,
                DestinationFan = new()
                {
                    Fan_StartAngle = 286f,
                    Fan_EndAngle = 174f,
                    Fan_DistanceMin = 1f,
                    Fan_DistanceMax = 3f,
                    Fan_Height = 0.7f,
                },
                Multi_Destinations = new()
                {
                },
            },
            [185] = new()
            {
                ShardId = 185,
                SelectionId = 2,
                Shard_Position = new Vector3(-92.21033f, 2.304016f, 29.709229f),
                TerritoryId = 962,
                DestinationFan = new()
                {
                    Fan_StartAngle = 0f,
                    Fan_EndAngle = 359f,
                    Fan_DistanceMin = 1f,
                    Fan_DistanceMax = 3f,
                    Fan_Height = 0.73f,
                },
                Multi_Destinations = new()
                {
                },
            },
            [186] = new()
            {
                ShardId = 186,
                SelectionId = 3,
                Shard_Position = new Vector3(-36.94214f, 41.367188f, -156.6034f),
                TerritoryId = 962,
                DestinationFan = new()
                {
                    Fan_StartAngle = 0f,
                    Fan_EndAngle = 359f,
                    Fan_DistanceMin = 1f,
                    Fan_DistanceMax = 3f,
                    Fan_Height = 0.33f,
                },
                Multi_Destinations = new()
                {
                },
            },
            [187] = new()
            {
                ShardId = 187,
                SelectionId = 4,
                Shard_Position = new Vector3(204.79126f, 21.774597f, -118.73047f),
                TerritoryId = 962,
                DestinationFan = new()
                {
                    Fan_StartAngle = 302f,
                    Fan_EndAngle = 229f,
                    Fan_DistanceMin = 1f,
                    Fan_DistanceMax = 3f,
                    Fan_Height = 0.51f,
                },
                Multi_Destinations = new()
                {
                },
            },
            [188] = new()
            {
                ShardId = 188,
                SelectionId = 5,
                Shard_Position = new Vector3(206.22559f, 1.8463135f, 13.77887f),
                TerritoryId = 962,
                DestinationFan = new()
                {
                    Fan_StartAngle = 0f,
                    Fan_EndAngle = 359f,
                    Fan_DistanceMin = 1f,
                    Fan_DistanceMax = 3f,
                    Fan_Height = 0.86f,
                },
                Multi_Destinations = new()
                {
                },
            },
            [189] = new()
            {
                ShardId = 189,
                SelectionId = 6,
                Shard_Position = new Vector3(16.494995f, -16.250854f, 127.73328f),
                TerritoryId = 962,
                DestinationFan = new()
                {
                    Fan_StartAngle = 0f,
                    Fan_EndAngle = 359f,
                    Fan_DistanceMin = 1f,
                    Fan_DistanceMax = 3f,
                    Fan_Height = 0.57f,
                },
                Multi_Destinations = new()
                {
                },
            },
        },

        // Raz-da-han
        [963] = new()
        {
            [183] = new()
            {
                ShardId = 183,
                SelectionId = 0,
                Shard_Position = new Vector3(25.986084f, 3.250122f, -27.023743f),
                TerritoryId = 963,
                DestinationFan = new()
                {
                    Fan_StartAngle = 0f,
                    Fan_EndAngle = 359f,
                    Fan_DistanceMin = 5f,
                    Fan_DistanceMax = 10f,
                    Fan_Height = 0f,
                },
                Multi_Destinations = new()
                {
                },
            },
            [191] = new()
            {
                ShardId = 191,
                SelectionId = 1,
                Shard_Position = new Vector3(-365.95715f, 44.99878f, -31.815125f),
                TerritoryId = 963,
                DestinationFan = new()
                {
                    Fan_StartAngle = 43f,
                    Fan_EndAngle = 313f,
                    Fan_DistanceMin = 1f,
                    Fan_DistanceMax = 3f,
                    Fan_Height = 1.17f,
                },
                Multi_Destinations = new()
                {
                },
            },
            [192] = new()
            {
                ShardId = 192,
                SelectionId = 2,
                Shard_Position = new Vector3(-156.14563f, 35.99597f, 27.725586f),
                TerritoryId = 963,
                DestinationFan = new()
                {
                    Fan_StartAngle = 183f,
                    Fan_EndAngle = 339f,
                    Fan_DistanceMin = 1f,
                    Fan_DistanceMax = 3f,
                    Fan_Height = 0.45f,
                },
                Multi_Destinations = new()
                {
                },
            },
            [193] = new()
            {
                ShardId = 193,
                SelectionId = 3,
                Shard_Position = new Vector3(-144.33508f, 27.969727f, 202.2583f),
                TerritoryId = 963,
                DestinationFan = new()
                {
                    Fan_StartAngle = 235f,
                    Fan_EndAngle = 128f,
                    Fan_DistanceMin = 1f,
                    Fan_DistanceMax = 3f,
                    Fan_Height = 0.23f,
                },
                Multi_Destinations = new()
                {
                },
            },
            [194] = new()
            {
                ShardId = 194,
                SelectionId = 4,
                Shard_Position = new Vector3(6.6071167f, -2.02948f, 110.55151f),
                TerritoryId = 963,
                DestinationFan = new()
                {
                    Fan_StartAngle = 246f,
                    Fan_EndAngle = 117f,
                    Fan_DistanceMin = 1f,
                    Fan_DistanceMax = 3f,
                    Fan_Height = 0.48f,
                },
                Multi_Destinations = new()
                {
                },
            },
            [195] = new()
            {
                ShardId = 195,
                SelectionId = 5,
                Shard_Position = new Vector3(-141.37488f, 3.982544f, -98.435974f),
                TerritoryId = 963,
                DestinationFan = new()
                {
                    Fan_StartAngle = 102f,
                    Fan_EndAngle = 247f,
                    Fan_DistanceMin = 1f,
                    Fan_DistanceMax = 3f,
                    Fan_Height = 0.63f,
                },
                Multi_Destinations = new()
                {
                },
            },
            [196] = new()
            {
                ShardId = 196,
                SelectionId = 6,
                Shard_Position = new Vector3(-42.61847f, -0.015319824f, -197.61963f),
                TerritoryId = 963,
                DestinationFan = new()
                {
                    Fan_StartAngle = 190f,
                    Fan_EndAngle = 92f,
                    Fan_DistanceMin = 1f,
                    Fan_DistanceMax = 3f,
                    Fan_Height = 1.16f,
                },
                Multi_Destinations = new()
                {
                },
            },
            [198] = new()
            {
                ShardId = 198,
                SelectionId = 7,
                Shard_Position = new Vector3(129.59485f, 26.993164f, 13.473633f),
                TerritoryId = 963,
                DestinationFan = new()
                {
                    Fan_StartAngle = 73f,
                    Fan_EndAngle = 305f,
                    Fan_DistanceMin = 1f,
                    Fan_DistanceMax = 2.8f,
                    Fan_Height = 0.37f,
                },
                Multi_Destinations = new()
                {
                },
            },
        },

        // Tuliyolli
        [1185] = new()
        {
            [216] = new()
            {
                ShardId = 216,
                SelectionId = 0,
                Shard_Position = new Vector3(-24.093994f, 0.77819824f, 7.583679f),
                TerritoryId = 1185,
                DestinationFan = new()
                {
                    Fan_StartAngle = 0f,
                    Fan_EndAngle = 359f,
                    Fan_DistanceMin = 5f,
                    Fan_DistanceMax = 10f,
                    Fan_Height = 0.51f,
                },
                Multi_Destinations = new()
                {
                },
            },
            [220] = new()
            {
                ShardId = 220,
                SelectionId = 3,
                Shard_Position = new Vector3(-149.73682f, -15.030151f, 198.90125f),
                TerritoryId = 1185,
                DestinationFan = new()
                {
                    Fan_StartAngle = 252f,
                    Fan_EndAngle = 85f,
                    Fan_DistanceMin = 1f,
                    Fan_DistanceMax = 3f,
                    Fan_Height = 0.48f,
                },
                Multi_Destinations = new()
                {
                },
            },
            [218] = new()
            {
                ShardId = 218,
                SelectionId = 1,
                Shard_Position = new Vector3(-413.68738f, 2.9754639f, -45.975464f),
                TerritoryId = 1185,
                DestinationFan = new()
                {
                    Fan_StartAngle = 0f,
                    Fan_EndAngle = 359f,
                    Fan_DistanceMin = 1f,
                    Fan_DistanceMax = 3f,
                    Fan_Height = 0.32f,
                },
                Multi_Destinations = new()
                {
                },
            },
            [219] = new()
            {
                ShardId = 219,
                SelectionId = 2,
                Shard_Position = new Vector3(-187.1214f, 39.93274f, 6.088318f),
                TerritoryId = 1185,
                DestinationFan = new()
                {
                    Fan_StartAngle = 0f,
                    Fan_EndAngle = 359f,
                    Fan_DistanceMin = 1f,
                    Fan_DistanceMax = 3f,
                    Fan_Height = 0.38f,
                },
                Multi_Destinations = new()
                {
                },
            },
            [221] = new()
            {
                ShardId = 221,
                SelectionId = 4,
                Shard_Position = new Vector3(-14.999634f, -10.025269f, 135.57642f),
                TerritoryId = 1185,
                DestinationFan = new()
                {
                    Fan_StartAngle = 203f,
                    Fan_EndAngle = 334f,
                    Fan_DistanceMin = 1f,
                    Fan_DistanceMax = 3f,
                    Fan_Height = 0.98f,
                },
                Multi_Destinations = new()
                {
                },
            },
            [222] = new()
            {
                ShardId = 222,
                SelectionId = 5,
                Shard_Position = new Vector3(-99.13794f, 100.72473f, -222.03406f),
                TerritoryId = 1185,
                DestinationFan = new()
                {
                    Fan_StartAngle = 0f,
                    Fan_EndAngle = 359f,
                    Fan_DistanceMin = 1f,
                    Fan_DistanceMax = 3f,
                    Fan_Height = 0.44f,
                },
                Multi_Destinations = new()
                {
                },
            },
            [223] = new()
            {
                ShardId = 223,
                SelectionId = 6,
                Shard_Position = new Vector3(166.27747f, -17.990417f, 38.742676f),
                TerritoryId = 1185,
                DestinationFan = new()
                {
                    Fan_StartAngle = 336f,
                    Fan_EndAngle = 199f,
                    Fan_DistanceMin = 1f,
                    Fan_DistanceMax = 3f,
                    Fan_Height = 0.44f,
                },
                Multi_Destinations = new()
                {
                },
            },
            [224] = new()
            {
                ShardId = 224,
                SelectionId = 7,
                Shard_Position = new Vector3(71.7937f, 47.074097f, -333.21124f),
                TerritoryId = 1185,
                DestinationFan = new()
                {
                    Fan_StartAngle = 0f,
                    Fan_EndAngle = 359f,
                    Fan_DistanceMin = 1f,
                    Fan_DistanceMax = 3f,
                    Fan_Height = 0.53f,
                },
                Multi_Destinations = new()
                {
                },
            },
        },

        // Solution Nine
        [1186] = new()
        {
            [235] = new()
            {
                ShardId = 235,
                SelectionId = 0,
                Shard_Position = new Vector3(-160.05188f, -0.015319824f, 21.591492f),
                TerritoryId = 1186,
                DestinationFan = new()
                {
                    Fan_StartAngle = 0f,
                    Fan_EndAngle = 359f,
                    Fan_DistanceMin = 1f,
                    Fan_DistanceMax = 3f,
                    Fan_Height = 0.53f,
                },
                Multi_Destinations = new()
                {
                },
            },
            [230] = new()
            {
                ShardId = 230,
                SelectionId = 1,
                Shard_Position = new Vector3(-30.441833f, -6.0579224f, 209.3385f),
                TerritoryId = 1186,
                DestinationFan = new()
                {
                    Fan_StartAngle = 0f,
                    Fan_EndAngle = 359f,
                    Fan_DistanceMin = 1f,
                    Fan_DistanceMax = 3f,
                    Fan_Height = 0.65f,
                },
                Multi_Destinations = new()
                {
                },
            },
            [231] = new()
            {
                ShardId = 231,
                SelectionId = 2,
                Shard_Position = new Vector3(382.6809f, 59.983154f, 76.67651f),
                TerritoryId = 1186,
                DestinationFan = new()
                {
                    Fan_StartAngle = 0f,
                    Fan_EndAngle = 359f,
                    Fan_DistanceMin = 1f,
                    Fan_DistanceMax = 3f,
                    Fan_Height = 0.35f,
                },
                Multi_Destinations = new()
                {
                },
            },
            [232] = new()
            {
                ShardId = 232,
                SelectionId = 3,
                Shard_Position = new Vector3(258.28943f, 50.736206f, 148.72961f),
                TerritoryId = 1186,
                DestinationFan = new()
                {
                    Fan_StartAngle = 0f,
                    Fan_EndAngle = 359f,
                    Fan_DistanceMin = 1f,
                    Fan_DistanceMax = 2.5f,
                    Fan_Height = 0.43f,
                },
                Multi_Destinations = new()
                {
                },
            },
            [233] = new()
            {
                ShardId = 233,
                SelectionId = 4,
                Shard_Position = new Vector3(374.77686f, 60.01367f, 325.67322f),
                TerritoryId = 1186,
                DestinationFan = new()
                {
                    Fan_StartAngle = 0f,
                    Fan_EndAngle = 359f,
                    Fan_DistanceMin = 1f,
                    Fan_DistanceMax = 3f,
                    Fan_Height = 0.44f,
                },
                Multi_Destinations = new()
                {
                },
            },
            [234] = new()
            {
                ShardId = 234,
                SelectionId = 5,
                Shard_Position = new Vector3(-32.059265f, 38.04065f, -345.2354f),
                TerritoryId = 1186,
                DestinationFan = new()
                {
                    Fan_StartAngle = 0f,
                    Fan_EndAngle = 359f,
                    Fan_DistanceMin = 1f,
                    Fan_DistanceMax = 3f,
                    Fan_Height = 0.41f,
                },
                Multi_Destinations = new()
                {
                },
            },
            [236] = new()
            {
                ShardId = 236,
                SelectionId = 7,
                Shard_Position = new Vector3(-378.13385f, 13.992493f, 136.49194f),
                TerritoryId = 1186,
                DestinationFan = new()
                {
                    Fan_StartAngle = 0f,
                    Fan_EndAngle = 359f,
                    Fan_DistanceMin = 1f,
                    Fan_DistanceMax = 3f,
                    Fan_Height = 0.61f,
                },
                Multi_Destinations = new()
                {
                },
            },
            [217] = new()
            {
                ShardId = 217,
                SelectionId = 6,
                Shard_Position = new Vector3(-0.015319824f, 8.987488f, -0.015319824f),
                TerritoryId = 1186,
                DestinationFan = new()
                {
                    Fan_StartAngle = 0f,
                    Fan_EndAngle = 0f,
                    Fan_DistanceMin = 1f,
                    Fan_DistanceMax = 3f,
                    Fan_Height = 0f,
                },
                Multi_Destinations = new()
                {
                    new()
                    {
                        Fan_StartAngle = 84f,
                        Fan_EndAngle = 96f,
                        Fan_DistanceMin = 9.3f,
                        Fan_DistanceMax = 10.4f,
                        Fan_Height = 0f,
                    },
                    new()
                    {
                        Fan_StartAngle = 132f,
                        Fan_EndAngle = 138f,
                        Fan_DistanceMin = 10f,
                        Fan_DistanceMax = 10.5f,
                        Fan_Height = 0f,
                    },
                    new()
                    {
                        Fan_StartAngle = 172f,
                        Fan_EndAngle = 186f,
                        Fan_DistanceMin = 9.4f,
                        Fan_DistanceMax = 10.3f,
                        Fan_Height = 0f,
                    },
                    new()
                    {
                        Fan_StartAngle = 223f,
                        Fan_EndAngle = 227f,
                        Fan_DistanceMin = 9.9f,
                        Fan_DistanceMax = 10.3f,
                        Fan_Height = 0f,
                    },
                    new()
                    {
                        Fan_StartAngle = 264f,
                        Fan_EndAngle = 276f,
                        Fan_DistanceMin = 9.7f,
                        Fan_DistanceMax = 10.5f,
                        Fan_Height = 0f,
                    },
                    new()
                    {
                        Fan_StartAngle = 312f,
                        Fan_EndAngle = 319f,
                        Fan_DistanceMin = 10f,
                        Fan_DistanceMax = 10.5f,
                        Fan_Height = 0f,
                    },
                    new()
                    {
                        Fan_StartAngle = 352f,
                        Fan_EndAngle = 8f,
                        Fan_DistanceMin = 9.2f,
                        Fan_DistanceMax = 10.6f,
                        Fan_Height = 0f,
                    },
                    new()
                    {
                        Fan_StartAngle = 42f,
                        Fan_EndAngle = 48f,
                        Fan_DistanceMin = 10.1f,
                        Fan_DistanceMax = 10.5f,
                        Fan_Height = 0f,
                    },
                },
            },
        },
    };
}
