using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.NorthShroud_154
{
	public class BTN_171 : RouteInfo
	{
		public override uint Id => 171;
		public override uint ExpansionId => 0;
		public override uint ZoneId => 154;
		public override uint GatherType => 2;
		public override Vector2 MapPosition => new Vector2(252.405f, 18.9156f);
		public override int Radius => 54;

		public override HashSet<uint> NodeIds => new()
		{
			30329,
			30330,
			30331,
			30332,
		};

		public override HashSet<uint> ItemIds => new()
		{
			5,
			11,
			5816,
			5818,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
