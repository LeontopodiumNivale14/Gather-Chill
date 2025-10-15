using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.TheAzimSteppe_622
{
	public class BTN_813 : RouteInfo
	{
		public override uint Id => 813;
		public override uint ExpansionId => 2;
		public override uint ZoneId => 622;
		public override uint GatherType => 3;
		public override Vector2 MapPosition => new Vector2(383.582f, -230.326f);
		public override int Radius => 123;

		public override HashSet<uint> NodeIds => new()
		{
			33885,
			33886,
			33887,
			33888,
			33889,
			33890,
		};

		public override HashSet<uint> ItemIds => new()
		{
			11,
			33233,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
