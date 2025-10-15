using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheChurningMists_400
{
	public class BTN_298 : RouteInfo
	{
		public override uint Id => 298;
		public override uint ExpansionId => 1;
		public override uint ZoneId => 400;
		public override uint GatherType => 3;
		public override Vector2 MapPosition => new Vector2(-19.7994f, -6.90747f);
		public override int Radius => 154;

		public override HashSet<uint> NodeIds => new()
		{
			31394,
			31395,
			31396,
			31397,
			31398,
			31399,
		};

		public override HashSet<uint> ItemIds => new()
		{
			11,
			12885,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
