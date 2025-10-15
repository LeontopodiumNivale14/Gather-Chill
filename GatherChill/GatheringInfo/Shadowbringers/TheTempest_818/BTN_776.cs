using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.TheTempest_818
{
	public class BTN_776 : RouteInfo
	{
		public override uint Id => 776;
		public override uint ExpansionId => 3;
		public override uint ZoneId => 818;
		public override uint GatherType => 2;
		public override Vector2 MapPosition => new Vector2(798.554f, -492.99f);
		public override int Radius => 1;

		public override HashSet<uint> NodeIds => new()
		{
			33027,
		};

		public override HashSet<uint> ItemIds => new()
		{
			33006,
			33008,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
