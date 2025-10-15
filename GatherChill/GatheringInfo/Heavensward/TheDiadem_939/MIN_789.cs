using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheDiadem_939
{
	public class MIN_789 : RouteInfo
	{
		public override uint Id => 789;
		public override uint ExpansionId => 1;
		public override uint ZoneId => 939;
		public override uint GatherType => 1;
		public override Vector2 MapPosition => new Vector2(103.014f, -108.373f);
		public override int Radius => 865;

		public override HashSet<uint> NodeIds => new()
		{
			33647,
			33654,
			33659,
			33661,
			33666,
			33668,
			33673,
			33680,
			33687,
			33693,
			33698,
			33700,
			33705,
			33712,
			33719,
			33726,
			33731,
			33733,
			33738,
		};

		public override HashSet<uint> ItemIds => new()
		{
			29942,
			31314,
			32008,
			32023,
			32033,
			32044,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
