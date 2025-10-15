using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheChurningMists_400
{
	public class MIN_355 : RouteInfo
	{
		public override uint Id => 355;
		public override uint ExpansionId => 1;
		public override uint ZoneId => 400;
		public override uint GatherType => 1;
		public override Vector2 MapPosition => new Vector2(256.168f, -168.7f);
		public override int Radius => 123;

		public override HashSet<uint> NodeIds => new()
		{
			31358,
			31359,
			31360,
			31361,
			31362,
			31363,
		};

		public override HashSet<uint> ItemIds => new()
		{
			11,
			12532,
			12539,
			13759,
			13761,
			14955,
			17559,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
