using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.EastShroud_152
{
	public class MIN_209 : RouteInfo
	{
		public override uint Id => 209;
		public override uint ExpansionId => 0;
		public override uint ZoneId => 152;
		public override uint GatherType => 1;
		public override Vector2 MapPosition => new Vector2(-165.509f, 156.691f);
		public override int Radius => 42;

		public override HashSet<uint> NodeIds => new()
		{
			30544,
			30545,
			30546,
			30547,
		};

		public override HashSet<uint> ItemIds => new()
		{
			4,
			10,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
