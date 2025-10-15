using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Dawntrail.YakTel_1189
{
	public class BTN_997 : RouteInfo
	{
		public override uint Id => 997;
		public override uint ExpansionId => 5;
		public override uint ZoneId => 1189;
		public override uint GatherType => 3;
		public override Vector2 MapPosition => new Vector2(-111.379f, -483.91f);
		public override int Radius => 159;

		public override HashSet<uint> NodeIds => new()
		{
			34887,
			34888,
			34889,
			34890,
			34891,
			34892,
		};

		public override HashSet<uint> ItemIds => new()
		{
			13,
			43978,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
