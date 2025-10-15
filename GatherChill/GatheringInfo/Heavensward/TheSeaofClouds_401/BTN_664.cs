using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheSeaofClouds_401
{
	public class BTN_664 : RouteInfo
	{
		public override uint Id => 664;
		public override uint ExpansionId => 1;
		public override uint ZoneId => 401;
		public override uint GatherType => 3;
		public override Vector2 MapPosition => new Vector2(-358.108f, 670.383f);
		public override int Radius => 108;

		public override HashSet<uint> NodeIds => new()
		{
			32887,
			32888,
			32889,
			32890,
			32891,
			32892,
		};

		public override HashSet<uint> ItemIds => new()
		{
			28779,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
