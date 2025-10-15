using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Dawntrail.Shaaloani_1190
{
	public class BTN_989 : RouteInfo
	{
		public override uint Id => 989;
		public override uint ExpansionId => 5;
		public override uint ZoneId => 1190;
		public override uint GatherType => 2;
		public override Vector2 MapPosition => new Vector2(-458.056f, 28.8528f);
		public override int Radius => 185;

		public override HashSet<uint> NodeIds => new()
		{
			34839,
			34840,
			34841,
			34842,
			34843,
			34844,
		};

		public override HashSet<uint> ItemIds => new()
		{
			10,
			43987,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
