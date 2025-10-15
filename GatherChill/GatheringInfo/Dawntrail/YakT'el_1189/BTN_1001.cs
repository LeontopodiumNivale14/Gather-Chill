using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Dawntrail.YakTel_1189
{
	public class BTN_1001 : RouteInfo
	{
		public override uint Id => 1001;
		public override uint ExpansionId => 5;
		public override uint ZoneId => 1189;
		public override uint GatherType => 3;
		public override Vector2 MapPosition => new Vector2(386.154f, -816.934f);
		public override int Radius => 146;

		public override HashSet<uint> NodeIds => new()
		{
			34911,
			34912,
			34913,
			34914,
			34915,
			34916,
		};

		public override HashSet<uint> ItemIds => new()
		{
			13,
			43914,
			44043,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
