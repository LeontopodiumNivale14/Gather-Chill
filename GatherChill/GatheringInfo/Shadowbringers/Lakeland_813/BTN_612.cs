using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.Lakeland_813
{
	public class BTN_612 : RouteInfo
	{
		public override uint Id => 612;
		public override uint ExpansionId => 3;
		public override uint ZoneId => 813;
		public override uint GatherType => 2;
		public override Vector2 MapPosition => new Vector2(692.81f, 171.571f);
		public override int Radius => 135;

		public override HashSet<uint> NodeIds => new()
		{
			32691,
			32692,
			32693,
			32694,
			32695,
			32696,
		};

		public override HashSet<uint> ItemIds => new()
		{
			11,
			27820,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
