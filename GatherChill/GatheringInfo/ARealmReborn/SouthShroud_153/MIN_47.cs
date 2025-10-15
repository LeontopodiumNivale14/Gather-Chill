using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.SouthShroud_153
{
	public class MIN_47 : RouteInfo
	{
		public override uint Id => 47;
		public override uint ExpansionId => 0;
		public override uint ZoneId => 153;
		public override uint GatherType => 0;
		public override Vector2 MapPosition => new Vector2(-272.097f, -100.237f);
		public override int Radius => 48;

		public override HashSet<uint> NodeIds => new()
		{
			30105,
			30106,
			30107,
			30461,
		};

		public override HashSet<uint> ItemIds => new()
		{
			3,
			5113,
			5491,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
