using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.CoerthasCentralHighlands_155
{
	public class MIN_222 : RouteInfo
	{
		public override uint Id => 222;
		public override uint ExpansionId => 0;
		public override uint ZoneId => 155;
		public override uint GatherType => 0;
		public override Vector2 MapPosition => new Vector2(304.512f, -77.4628f);
		public override int Radius => 22;

		public override HashSet<uint> NodeIds => new()
		{
			31010,
		};

		public override HashSet<uint> ItemIds => new()
		{
			5121,
			10095,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
