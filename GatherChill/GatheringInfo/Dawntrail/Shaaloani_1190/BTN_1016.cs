using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Dawntrail.Shaaloani_1190
{
	public class BTN_1016 : RouteInfo
	{
		public override uint Id => 1016;
		public override uint ExpansionId => 5;
		public override uint ZoneId => 1190;
		public override uint GatherType => 3;
		public override Vector2 MapPosition => new Vector2(-262.156f, -552.335f);
		public override int Radius => 131;

		public override HashSet<uint> NodeIds => new()
		{
			34964,
			34965,
			34966,
		};

		public override HashSet<uint> ItemIds => new()
		{
			10,
			43933,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
