using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.CentralShroud_148
{
	public class MIN_178 : RouteInfo
	{
		public override uint Id => 178;
		public override uint ExpansionId => 0;
		public override uint ZoneId => 148;
		public override uint GatherType => 0;
		public override Vector2 MapPosition => new Vector2(-368.396f, 29.0423f);
		public override int Radius => 82;

		public override HashSet<uint> NodeIds => new()
		{
			30495,
			30496,
			30497,
			30498,
		};

		public override HashSet<uint> ItemIds => new()
		{
			4,
			5136,
			5138,
			7008,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
