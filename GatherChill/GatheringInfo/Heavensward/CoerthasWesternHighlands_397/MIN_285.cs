using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.CoerthasWesternHighlands_397
{
	public class MIN_285 : RouteInfo
	{
		public override uint Id => 285;
		public override uint ExpansionId => 1;
		public override uint ZoneId => 397;
		public override uint GatherType => 0;
		public override Vector2 MapPosition => new Vector2(440.884f, -499.976f);
		public override int Radius => 141;

		public override HashSet<uint> NodeIds => new()
		{
			31316,
			31317,
			31318,
			31319,
			31320,
			31321,
		};

		public override HashSet<uint> ItemIds => new()
		{
			9,
			12551,
			12559,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
