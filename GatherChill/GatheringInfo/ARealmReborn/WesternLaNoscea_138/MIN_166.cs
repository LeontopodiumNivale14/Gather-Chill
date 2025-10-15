using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.WesternLaNoscea_138
{
	public class MIN_166 : RouteInfo
	{
		public override uint Id => 166;
		public override uint ExpansionId => 0;
		public override uint ZoneId => 138;
		public override uint GatherType => 1;
		public override Vector2 MapPosition => new Vector2(245.742f, 136.686f);
		public override int Radius => 84;

		public override HashSet<uint> NodeIds => new()
		{
			30457,
			30458,
			30459,
			30460,
		};

		public override HashSet<uint> ItemIds => new()
		{
			3,
			5229,
			5230,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
