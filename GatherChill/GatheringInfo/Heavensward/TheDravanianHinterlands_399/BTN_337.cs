using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheDravanianHinterlands_399
{
	public class BTN_337 : RouteInfo
	{
		public override uint Id => 337;
		public override uint ExpansionId => 1;
		public override uint ZoneId => 399;
		public override uint GatherType => 3;
		public override Vector2 MapPosition => new Vector2(-783.746f, 259.635f);
		public override int Radius => 142;

		public override HashSet<uint> NodeIds => new()
		{
			31485,
			31486,
			31488,
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
