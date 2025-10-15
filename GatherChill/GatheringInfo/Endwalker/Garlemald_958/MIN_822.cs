using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Endwalker.Garlemald_958
{
	public class MIN_822 : RouteInfo
	{
		public override uint Id => 822;
		public override uint ExpansionId => 4;
		public override uint ZoneId => 958;
		public override uint GatherType => 1;
		public override Vector2 MapPosition => new Vector2(-22.9568f, 376.312f);
		public override int Radius => 116;

		public override HashSet<uint> NodeIds => new()
		{
			33930,
			33931,
			33932,
			33933,
			33934,
			33935,
		};

		public override HashSet<uint> ItemIds => new()
		{
			12,
			35601,
			36176,
			36241,
			36671,
			41070,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
