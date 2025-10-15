using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Endwalker.Labyrinthos_956
{
	public class BTN_854 : RouteInfo
	{
		public override uint Id => 854;
		public override uint ExpansionId => 4;
		public override uint ZoneId => 956;
		public override uint GatherType => 3;
		public override Vector2 MapPosition => new Vector2(-574.57f, 43.3029f);
		public override int Radius => 16;

		public override HashSet<uint> NodeIds => new()
		{
			34041,
		};

		public override HashSet<uint> ItemIds => new()
		{
			36307,
			36309,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
