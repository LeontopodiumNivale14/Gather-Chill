using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.TheTempest_818
{
	public class MIN_766 : RouteInfo
	{
		public override uint Id => 766;
		public override uint ExpansionId => 3;
		public override uint ZoneId => 818;
		public override uint GatherType => 0;
		public override Vector2 MapPosition => new Vector2(561.288f, -46.1728f);
		public override int Radius => 1;

		public override HashSet<uint> NodeIds => new()
		{
			31510,
		};

		public override HashSet<uint> ItemIds => new()
		{
			32984,
			32986,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
