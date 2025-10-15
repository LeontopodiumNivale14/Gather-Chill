using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.CentralThanalan_141
{
	public class BTN_148 : RouteInfo
	{
		public override uint Id => 148;
		public override uint ExpansionId => 0;
		public override uint ZoneId => 141;
		public override uint GatherType => 2;
		public override Vector2 MapPosition => new Vector2(50.2292f, 258.683f);
		public override int Radius => 97;

		public override HashSet<uint> NodeIds => new()
		{
			30389,
			30390,
			30391,
			30392,
		};

		public override HashSet<uint> ItemIds => new()
		{
			6,
			4831,
			5351,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
