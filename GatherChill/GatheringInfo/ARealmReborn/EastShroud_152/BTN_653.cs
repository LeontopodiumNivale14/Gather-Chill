using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.EastShroud_152
{
	public class BTN_653 : RouteInfo
	{
		public override uint Id => 653;
		public override uint ExpansionId => 0;
		public override uint ZoneId => 152;
		public override uint GatherType => 2;
		public override Vector2 MapPosition => new Vector2(-436.262f, 102.77f);
		public override int Radius => 63;

		public override HashSet<uint> NodeIds => new()
		{
			32839,
			32840,
			32841,
			32842,
		};

		public override HashSet<uint> ItemIds => new()
		{
			28765,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
