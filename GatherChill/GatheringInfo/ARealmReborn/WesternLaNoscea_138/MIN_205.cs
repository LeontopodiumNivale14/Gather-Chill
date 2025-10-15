using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.WesternLaNoscea_138
{
	public class MIN_205 : RouteInfo
	{
		public override uint Id => 205;
		public override uint ExpansionId => 0;
		public override uint ZoneId => 138;
		public override uint GatherType => 1;
		public override Vector2 MapPosition => new Vector2(500.948f, 337.619f);
		public override int Radius => 65;

		public override HashSet<uint> NodeIds => new()
		{
			30992,
			30993,
			30994,
			30995,
		};

		public override HashSet<uint> ItemIds => new()
		{
			3,
			9,
			5817,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
