using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Dawntrail.Kozamauka_1188
{
	public class BTN_1007 : RouteInfo
	{
		public override uint Id => 1007;
		public override uint ExpansionId => 5;
		public override uint ZoneId => 1188;
		public override uint GatherType => 5;
		public override Vector2 MapPosition => new Vector2(-31.0441f, 584.508f);
		public override int Radius => 85;

		public override HashSet<uint> NodeIds => new()
		{
			34941,
			34942,
			34943,
		};

		public override HashSet<uint> ItemIds => new()
		{
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
