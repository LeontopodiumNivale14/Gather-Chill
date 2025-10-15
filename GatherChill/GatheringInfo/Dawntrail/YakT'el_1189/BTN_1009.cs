using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Dawntrail.YakTel_1189
{
	public class BTN_1009 : RouteInfo
	{
		public override uint Id => 1009;
		public override uint ExpansionId => 5;
		public override uint ZoneId => 1189;
		public override uint GatherType => 5;
		public override Vector2 MapPosition => new Vector2(363.483f, -837.177f);
		public override int Radius => 105;

		public override HashSet<uint> NodeIds => new()
		{
			34947,
			34948,
			34949,
		};

		public override HashSet<uint> ItemIds => new()
		{
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
