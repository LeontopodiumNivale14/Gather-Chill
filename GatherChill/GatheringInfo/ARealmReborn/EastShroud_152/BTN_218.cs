using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.EastShroud_152
{
	public class BTN_218 : RouteInfo
	{
		public override uint Id => 218;
		public override uint ExpansionId => 0;
		public override uint ZoneId => 152;
		public override uint GatherType => 3;
		public override Vector2 MapPosition => new Vector2(-384.411f, 116.979f);
		public override int Radius => 40;

		public override HashSet<uint> NodeIds => new()
		{
			31006,
		};

		public override HashSet<uint> ItemIds => new()
		{
			5546,
			6209,
			10098,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
