using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Dawntrail.LivingMemory_1192
{
	public class BTN_1029 : RouteInfo
	{
		public override uint Id => 1029;
		public override uint ExpansionId => 5;
		public override uint ZoneId => 1192;
		public override uint GatherType => 3;
		public override Vector2 MapPosition => new Vector2(-637.213f, -699.634f);
		public override int Radius => 38;

		public override HashSet<uint> NodeIds => new()
		{
			34989,
		};

		public override HashSet<uint> ItemIds => new()
		{
			43930,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
