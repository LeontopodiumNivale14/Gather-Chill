using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheChurningMists_400
{
	public class BTN_391 : RouteInfo
	{
		public override uint Id => 391;
		public override uint ExpansionId => 1;
		public override uint ZoneId => 400;
		public override uint GatherType => 2;
		public override Vector2 MapPosition => new Vector2(500.012f, 462.301f);
		public override int Radius => 116;

		public override HashSet<uint> NodeIds => new()
		{
			31694,
			31695,
			31696,
			31697,
			31698,
			31699,
		};

		public override HashSet<uint> ItemIds => new()
		{
			11,
			14159,
			14160,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
