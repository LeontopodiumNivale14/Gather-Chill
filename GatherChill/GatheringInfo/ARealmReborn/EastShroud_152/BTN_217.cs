using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.EastShroud_152
{
	public class BTN_217 : RouteInfo
	{
		public override uint Id => 217;
		public override uint ExpansionId => 0;
		public override uint ZoneId => 152;
		public override uint GatherType => 3;
		public override Vector2 MapPosition => new Vector2(-190.643f, -109.235f);
		public override int Radius => 20;

		public override HashSet<uint> NodeIds => new()
		{
			31005,
		};

		public override HashSet<uint> ItemIds => new()
		{
			5545,
			10098,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
