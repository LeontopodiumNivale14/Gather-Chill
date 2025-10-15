using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.Lakeland_813
{
	public class BTN_784 : RouteInfo
	{
		public override uint Id => 784;
		public override uint ExpansionId => 3;
		public override uint ZoneId => 813;
		public override uint GatherType => 3;
		public override Vector2 MapPosition => new Vector2(35.5405f, -433.775f);
		public override int Radius => 146;

		public override HashSet<uint> NodeIds => new()
		{
			33637,
			33638,
			33639,
		};

		public override HashSet<uint> ItemIds => new()
		{
			33012,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
