using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Dawntrail.HeritageFound_1191
{
	public class BTN_1003 : RouteInfo
	{
		public override uint Id => 1003;
		public override uint ExpansionId => 5;
		public override uint ZoneId => 1191;
		public override uint GatherType => 3;
		public override Vector2 MapPosition => new Vector2(-504.365f, -205.119f);
		public override int Radius => 182;

		public override HashSet<uint> NodeIds => new()
		{
			34923,
			34924,
			34925,
			34926,
			34927,
			34928,
		};

		public override HashSet<uint> ItemIds => new()
		{
			9,
			43985,
			44028,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
