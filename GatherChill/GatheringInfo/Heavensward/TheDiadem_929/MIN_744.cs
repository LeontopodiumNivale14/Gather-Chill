using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheDiadem_929
{
	public class MIN_744 : RouteInfo
	{
		public override uint Id => 744;
		public override uint ExpansionId => 1;
		public override uint ZoneId => 929;
		public override uint GatherType => 0;
		public override Vector2 MapPosition => new Vector2(40.1966f, 113.205f);
		public override int Radius => 886;

		public override HashSet<uint> NodeIds => new()
		{
			33394,
			33399,
			33401,
			33406,
			33408,
			33413,
			33420,
			33427,
			33438,
			33439,
			33442,
			33447,
			33449,
			33454,
			33456,
			33461,
			33468,
			33475,
			33482,
		};

		public override HashSet<uint> ItemIds => new()
		{
			29941,
			31284,
			31293,
			31303,
			31313,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
