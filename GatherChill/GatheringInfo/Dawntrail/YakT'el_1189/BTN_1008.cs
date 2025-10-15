using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Dawntrail.YakTel_1189
{
	public class BTN_1008 : RouteInfo
	{
		public override uint Id => 1008;
		public override uint ExpansionId => 5;
		public override uint ZoneId => 1189;
		public override uint GatherType => 5;
		public override Vector2 MapPosition => new Vector2(-771.453f, -693.053f);
		public override int Radius => 119;

		public override HashSet<uint> NodeIds => new()
		{
			34944,
			34945,
			34946,
		};

		public override HashSet<uint> ItemIds => new()
		{
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
