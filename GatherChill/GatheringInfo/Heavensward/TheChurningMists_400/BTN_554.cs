using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheChurningMists_400
{
	public class BTN_554 : RouteInfo
	{
		public override uint Id => 554;
		public override uint ExpansionId => 1;
		public override uint ZoneId => 400;
		public override uint GatherType => 3;
		public override Vector2 MapPosition => new Vector2(-716.932f, -466.318f);
		public override int Radius => 116;

		public override HashSet<uint> NodeIds => new()
		{
			32362,
			32363,
			32364,
			32365,
			32366,
			32367,
		};

		public override HashSet<uint> ItemIds => new()
		{
			11,
			23152,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
