using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Dawntrail.Shaaloani_1190
{
	public class MIN_1023 : RouteInfo
	{
		public override uint Id => 1023;
		public override uint ExpansionId => 5;
		public override uint ZoneId => 1190;
		public override uint GatherType => 0;
		public override Vector2 MapPosition => new Vector2(-612.303f, 133.207f);
		public override int Radius => 24;

		public override HashSet<uint> NodeIds => new()
		{
			34983,
		};

		public override HashSet<uint> ItemIds => new()
		{
			43920,
			43921,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
