using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.WesternThanalan_140
{
	public class MIN_156 : RouteInfo
	{
		public override uint Id => 156;
		public override uint ExpansionId => 0;
		public override uint ZoneId => 140;
		public override uint GatherType => 0;
		public override Vector2 MapPosition => new Vector2(45.2664f, 360.204f);
		public override int Radius => 53;

		public override HashSet<uint> NodeIds => new()
		{
			30421,
			30422,
			30423,
			30424,
		};

		public override HashSet<uint> ItemIds => new()
		{
			7,
			5107,
			5110,
			5124,
			5433,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
