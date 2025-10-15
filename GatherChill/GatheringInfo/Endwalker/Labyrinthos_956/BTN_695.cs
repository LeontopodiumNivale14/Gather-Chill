using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Endwalker.Labyrinthos_956
{
	public class BTN_695 : RouteInfo
	{
		public override uint Id => 695;
		public override uint ExpansionId => 4;
		public override uint ZoneId => 956;
		public override uint GatherType => 2;
		public override Vector2 MapPosition => new Vector2(332.06f, -504.018f);
		public override int Radius => 23;

		public override HashSet<uint> NodeIds => new()
		{
			34363,
		};

		public override HashSet<uint> ItemIds => new()
		{
			37821,
			38933,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
