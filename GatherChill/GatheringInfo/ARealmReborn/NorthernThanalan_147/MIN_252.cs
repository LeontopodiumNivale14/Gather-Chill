using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.NorthernThanalan_147
{
	public class MIN_252 : RouteInfo
	{
		public override uint Id => 252;
		public override uint ExpansionId => 0;
		public override uint ZoneId => 147;
		public override uint GatherType => 0;
		public override Vector2 MapPosition => new Vector2(78.9402f, 140.118f);
		public override int Radius => 113;

		public override HashSet<uint> NodeIds => new()
		{
			31060,
		};

		public override HashSet<uint> ItemIds => new()
		{
			10093,
			10095,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
