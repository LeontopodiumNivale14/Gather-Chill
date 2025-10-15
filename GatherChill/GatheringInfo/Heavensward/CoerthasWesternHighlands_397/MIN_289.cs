using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.CoerthasWesternHighlands_397
{
	public class MIN_289 : RouteInfo
	{
		public override uint Id => 289;
		public override uint ExpansionId => 1;
		public override uint ZoneId => 397;
		public override uint GatherType => 0;
		public override Vector2 MapPosition => new Vector2(-614.962f, -653.911f);
		public override int Radius => 40;

		public override HashSet<uint> NodeIds => new()
		{
			31704,
		};

		public override HashSet<uint> ItemIds => new()
		{
			16724,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
