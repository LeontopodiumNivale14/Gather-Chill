using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.EastShroud_152
{
	public class BTN_251 : RouteInfo
	{
		public override uint Id => 251;
		public override uint ExpansionId => 0;
		public override uint ZoneId => 152;
		public override uint GatherType => 3;
		public override Vector2 MapPosition => new Vector2(61.2381f, 453.841f);
		public override int Radius => 51;

		public override HashSet<uint> NodeIds => new()
		{
			31059,
		};

		public override HashSet<uint> ItemIds => new()
		{
			10096,
			10098,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
