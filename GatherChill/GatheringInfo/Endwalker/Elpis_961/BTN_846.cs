using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Endwalker.Elpis_961
{
	public class BTN_846 : RouteInfo
	{
		public override uint Id => 846;
		public override uint ExpansionId => 4;
		public override uint ZoneId => 961;
		public override uint GatherType => 3;
		public override Vector2 MapPosition => new Vector2(-36.5265f, 114.458f);
		public override int Radius => 110;

		public override HashSet<uint> NodeIds => new()
		{
			34018,
			34019,
			34020,
			34021,
			34022,
			34023,
		};

		public override HashSet<uint> ItemIds => new()
		{
			12,
			35603,
			36089,
			36094,
			36205,
			41072,
		};

		public override List<NodeInfo> Nodes => new()
		{
			new NodeInfo
			{
				NodeId = 34018,
				NodePosition = new Vector3(-41.94958f, -0.697382f, 173.7239f),
				LandingInfo = new LandingInfo
				{
					LandZone = new Vector3(-40.964645f, -1.4125742f, 174.92636f),
					UseRadial = true,
					InnerRadius = 1f,
					OuterRadius = 3f,
					StartAngle = 0f,
					EndAngle = 360f,
					RotationOffset = 0f
				}
			},
			new NodeInfo
			{
				NodeId = 34019,
				NodePosition = new Vector3(-65.74567f, -2.738637f, 195.961f),
				LandingInfo = new LandingInfo
				{
					LandZone = new Vector3(-64.37266f, -3.3859138f, 194.19154f),
					UseRadial = true,
					InnerRadius = 1f,
					OuterRadius = 3f,
					StartAngle = 0f,
					EndAngle = 360f,
					RotationOffset = 0f
				}
			},
			new NodeInfo
			{
				NodeId = 34019,
				NodePosition = new Vector3(-65.74567f, -2.738637f, 195.961f),
				LandingInfo = new LandingInfo
				{
					LandZone = new Vector3(-15.277368f, 3.7422287f, 207.66191f),
					UseRadial = true,
					InnerRadius = 1f,
					OuterRadius = 3f,
					StartAngle = 0f,
					EndAngle = 360f,
					RotationOffset = 0f
				}
			},
			new NodeInfo
			{
				NodeId = 34020,
				NodePosition = new Vector3(44.78436f, 7.229512f, 98.57001f),
				LandingInfo = new LandingInfo
				{
					LandZone = new Vector3(43.157482f, 6.1558733f, 98.0925f),
					UseRadial = true,
					InnerRadius = 1f,
					OuterRadius = 3f,
					StartAngle = 0f,
					EndAngle = 360f,
					RotationOffset = 0f
				}
			},
			new NodeInfo
			{
				NodeId = 34021,
				NodePosition = new Vector3(34.40783f, 3.908499f, 67.13975f),
				LandingInfo = new LandingInfo
				{
					LandZone = new Vector3(46.460236f, 6.640382f, 86.80276f),
					UseRadial = true,
					InnerRadius = 1f,
					OuterRadius = 3f,
					StartAngle = 0f,
					EndAngle = 360f,
					RotationOffset = 0f
				}
			},
			new NodeInfo
			{
				NodeId = 34021,
				NodePosition = new Vector3(34.40783f, 3.908499f, 67.13975f),
				LandingInfo = new LandingInfo
				{
					LandZone = new Vector3(34.86939f, 2.7543724f, 69.03007f),
					UseRadial = true,
					InnerRadius = 1f,
					OuterRadius = 3f,
					StartAngle = 0f,
					EndAngle = 360f,
					RotationOffset = 0f
				}
			},
			new NodeInfo
			{
				NodeId = 34022,
				NodePosition = new Vector3(-124.86f, -11.29384f, 89.80068f),
				LandingInfo = new LandingInfo
				{
					LandZone = new Vector3(-123.36684f, -11.992108f, 88.66787f),
					UseRadial = true,
					InnerRadius = 1f,
					OuterRadius = 3f,
					StartAngle = 0f,
					EndAngle = 360f,
					RotationOffset = 0f
				}
			},
			new NodeInfo
			{
				NodeId = 34023,
				NodePosition = new Vector3(-91.27831f, -8.489767f, 69.08894f),
				LandingInfo = new LandingInfo
				{
					LandZone = new Vector3(-93.40204f, -9.5247135f, 69.31429f),
					UseRadial = true,
					InnerRadius = 1f,
					OuterRadius = 3f,
					StartAngle = 0f,
					EndAngle = 360f,
					RotationOffset = 0f
				}
			},
			new NodeInfo
			{
				NodeId = 34023,
				NodePosition = new Vector3(-91.27831f, -8.489767f, 69.08894f),
				LandingInfo = new LandingInfo
				{
					LandZone = new Vector3(-116.4118f, -12.443328f, 40.19519f),
					UseRadial = true,
					InnerRadius = 1f,
					OuterRadius = 3f,
					StartAngle = 0f,
					EndAngle = 360f,
					RotationOffset = 0f
				}
			},
		};
	}
}
