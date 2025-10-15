using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.SouthernThanalan_146
{
	public class MIN_177 : RouteInfo
	{
		public override uint Id => 177;
		public override uint ExpansionId => 0;
		public override uint ZoneId => 146;
		public override uint GatherType => 0;
		public override Vector2 MapPosition => new Vector2(196.754f, 962.896f);
		public override int Radius => 64;

		public override HashSet<uint> NodeIds => new()
		{
			30491,
			30492,
			30493,
			30494,
		};

		public override HashSet<uint> ItemIds => new()
		{
			6,
			5114,
			5137,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
