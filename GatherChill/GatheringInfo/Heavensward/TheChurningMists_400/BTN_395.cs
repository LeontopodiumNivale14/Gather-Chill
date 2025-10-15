using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheChurningMists_400
{
	public class BTN_395 : RouteInfo
	{
		public override uint Id => 395;
		public override uint ExpansionId => 1;
		public override uint ZoneId => 400;
		public override uint GatherType => 2;
		public override Vector2 MapPosition => new Vector2(-627.088f, -641.389f);
		public override int Radius => 112;

		public override HashSet<uint> NodeIds => new()
		{
			31507,
		};

		public override HashSet<uint> ItemIds => new()
		{
			14957,
			15647,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
