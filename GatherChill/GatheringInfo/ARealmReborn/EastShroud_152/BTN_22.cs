using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.EastShroud_152
{
	public class BTN_22 : RouteInfo
	{
		public override uint Id => 22;
		public override uint ExpansionId => 0;
		public override uint ZoneId => 152;
		public override uint GatherType => 2;
		public override Vector2 MapPosition => new Vector2(-306.373f, 282.832f);
		public override int Radius => 78;

		public override HashSet<uint> NodeIds => new()
		{
			30042,
			30043,
			30044,
			30311,
		};

		public override HashSet<uint> ItemIds => new()
		{
			4,
			4810,
			5386,
			5405,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
