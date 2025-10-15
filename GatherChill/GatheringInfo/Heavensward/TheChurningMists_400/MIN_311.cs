using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheChurningMists_400
{
	public class MIN_311 : RouteInfo
	{
		public override uint Id => 311;
		public override uint ExpansionId => 1;
		public override uint ZoneId => 400;
		public override uint GatherType => 1;
		public override Vector2 MapPosition => new Vector2(-151.76f, 718.336f);
		public override int Radius => 150;

		public override HashSet<uint> NodeIds => new()
		{
			31448,
			31469,
			31470,
		};

		public override HashSet<uint> ItemIds => new()
		{
			8,
			5214,
			5220,
			12966,
			15949,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
