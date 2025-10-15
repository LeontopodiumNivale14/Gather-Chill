using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.TheRubySea_613
{
	public class BTN_892 : RouteInfo
	{
		public override uint Id => 892;
		public override uint ExpansionId => 2;
		public override uint ZoneId => 613;
		public override uint GatherType => 5;
		public override Vector2 MapPosition => new Vector2(334.283f, 6.80627f);
		public override int Radius => 119;

		public override HashSet<uint> NodeIds => new()
		{
			34311,
			34312,
			34313,
		};

		public override HashSet<uint> ItemIds => new()
		{
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
