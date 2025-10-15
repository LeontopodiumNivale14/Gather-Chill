using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.NorthernThanalan_147
{
	public class MIN_20 : RouteInfo
	{
		public override uint Id => 20;
		public override uint ExpansionId => 0;
		public override uint ZoneId => 147;
		public override uint GatherType => 1;
		public override Vector2 MapPosition => new Vector2(10.3019f, 328.574f);
		public override int Radius => 83;

		public override HashSet<uint> NodeIds => new()
		{
			30532,
			30533,
			30534,
			30535,
		};

		public override HashSet<uint> ItemIds => new()
		{
			4,
			5526,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
