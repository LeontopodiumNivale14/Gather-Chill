using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheDiadem_901
{
	public class BTN_703 : RouteInfo
	{
		public override uint Id => 703;
		public override uint ExpansionId => 1;
		public override uint ZoneId => 901;
		public override uint GatherType => 3;
		public override Vector2 MapPosition => new Vector2(80.3754f, -193.805f);
		public override int Radius => 932;

		public override HashSet<uint> NodeIds => new()
		{
			33133,
			33138,
			33145,
			33152,
			33159,
			33164,
			33166,
			33171,
			33173,
			33178,
			33185,
			33192,
			33199,
			33204,
			33206,
			33211,
			33213,
			33218,
			33225,
		};

		public override HashSet<uint> ItemIds => new()
		{
			29895,
			29907,
			29916,
			29926,
			29936,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
