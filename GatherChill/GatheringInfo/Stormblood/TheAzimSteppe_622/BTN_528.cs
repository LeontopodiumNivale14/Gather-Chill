using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.TheAzimSteppe_622
{
	public class BTN_528 : RouteInfo
	{
		public override uint Id => 528;
		public override uint ExpansionId => 2;
		public override uint ZoneId => 622;
		public override uint GatherType => 3;
		public override Vector2 MapPosition => new Vector2(-88.561f, -584.764f);
		public override int Radius => 57;

		public override HashSet<uint> NodeIds => new()
		{
			32276,
		};

		public override HashSet<uint> ItemIds => new()
		{
			19991,
			21086,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
