using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheDiadem_901
{
	public class BTN_704 : RouteInfo
	{
		public override uint Id => 704;
		public override uint ExpansionId => 1;
		public override uint ZoneId => 901;
		public override uint GatherType => 3;
		public override Vector2 MapPosition => new Vector2(84.8167f, -47.3311f);
		public override int Radius => 902;

		public override HashSet<uint> NodeIds => new()
		{
			33134,
			33139,
			33141,
			33146,
			33153,
			33160,
			33167,
			33172,
			33174,
			33179,
			33184,
			33191,
			33196,
			33198,
			33203,
			33205,
			33210,
			33217,
			33224,
		};

		public override HashSet<uint> ItemIds => new()
		{
			29900,
			29908,
			29917,
			29927,
			29937,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
