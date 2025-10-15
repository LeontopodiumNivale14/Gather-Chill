using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Dawntrail.Kozamauka_1188
{
	public class BTN_1012 : RouteInfo
	{
		public override uint Id => 1012;
		public override uint ExpansionId => 5;
		public override uint ZoneId => 1188;
		public override uint GatherType => 5;
		public override Vector2 MapPosition => new Vector2(180.976f, 488.97f);
		public override int Radius => 1;

		public override HashSet<uint> NodeIds => new()
		{
			34956,
		};

		public override HashSet<uint> ItemIds => new()
		{
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
