using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.EastShroud_152
{
	public class BTN_216 : RouteInfo
	{
		public override uint Id => 216;
		public override uint ExpansionId => 0;
		public override uint ZoneId => 152;
		public override uint GatherType => 3;
		public override Vector2 MapPosition => new Vector2(25.0982f, 248.367f);
		public override int Radius => 36;

		public override HashSet<uint> NodeIds => new()
		{
			31004,
		};

		public override HashSet<uint> ItemIds => new()
		{
			5350,
			10098,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
