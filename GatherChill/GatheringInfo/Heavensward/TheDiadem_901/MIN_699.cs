using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheDiadem_901
{
	public class MIN_699 : RouteInfo
	{
		public override uint Id => 699;
		public override uint ExpansionId => 1;
		public override uint ZoneId => 901;
		public override uint GatherType => 0;
		public override Vector2 MapPosition => new Vector2(64.3755f, -114.95f);
		public override int Radius => 930;

		public override HashSet<uint> NodeIds => new()
		{
			33038,
			33043,
			33045,
			33050,
			33057,
			33064,
			33071,
			33076,
			33078,
			33083,
			33088,
			33095,
			33100,
			33102,
			33107,
			33109,
			33114,
			33121,
			33128,
		};

		public override HashSet<uint> ItemIds => new()
		{
			29901,
			29910,
			29920,
			29930,
			29940,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
