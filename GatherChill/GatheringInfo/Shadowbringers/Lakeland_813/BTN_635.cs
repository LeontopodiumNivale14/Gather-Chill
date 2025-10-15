using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.Lakeland_813
{
	public class BTN_635 : RouteInfo
	{
		public override uint Id => 635;
		public override uint ExpansionId => 3;
		public override uint ZoneId => 813;
		public override uint GatherType => 5;
		public override Vector2 MapPosition => new Vector2(-347.9f, 549.572f);
		public override int Radius => 98;

		public override HashSet<uint> NodeIds => new()
		{
			32774,
			32775,
			32776,
		};

		public override HashSet<uint> ItemIds => new()
		{
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
