using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheDravanianForelands_398
{
	public class BTN_331 : RouteInfo
	{
		public override uint Id => 331;
		public override uint ExpansionId => 1;
		public override uint ZoneId => 398;
		public override uint GatherType => 3;
		public override Vector2 MapPosition => new Vector2(-252.223f, 467.969f);
		public override int Radius => 117;

		public override HashSet<uint> NodeIds => new()
		{
			31476,
			31477,
			31478,
		};

		public override HashSet<uint> ItemIds => new()
		{
			11,
			12968,
			12969,
			12970,
			15948,
			33147,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
