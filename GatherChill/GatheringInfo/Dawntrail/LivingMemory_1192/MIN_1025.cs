using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Dawntrail.LivingMemory_1192
{
	public class MIN_1025 : RouteInfo
	{
		public override uint Id => 1025;
		public override uint ExpansionId => 5;
		public override uint ZoneId => 1192;
		public override uint GatherType => 1;
		public override Vector2 MapPosition => new Vector2(175.183f, -207.589f);
		public override int Radius => 24;

		public override HashSet<uint> NodeIds => new()
		{
			34985,
		};

		public override HashSet<uint> ItemIds => new()
		{
			43923,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
