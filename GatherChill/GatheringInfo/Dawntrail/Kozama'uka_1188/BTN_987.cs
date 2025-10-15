using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Dawntrail.Kozamauka_1188
{
	public class BTN_987 : RouteInfo
	{
		public override uint Id => 987;
		public override uint ExpansionId => 5;
		public override uint ZoneId => 1188;
		public override uint GatherType => 2;
		public override Vector2 MapPosition => new Vector2(692.601f, 625.063f);
		public override int Radius => 166;

		public override HashSet<uint> NodeIds => new()
		{
			34827,
			34828,
			34829,
			34830,
			34831,
			34832,
		};

		public override HashSet<uint> ItemIds => new()
		{
			12,
			44014,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
