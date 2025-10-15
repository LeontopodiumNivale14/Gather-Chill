using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Dawntrail.Kozamauka_1188
{
	public class BTN_986 : RouteInfo
	{
		public override uint Id => 986;
		public override uint ExpansionId => 5;
		public override uint ZoneId => 1188;
		public override uint GatherType => 2;
		public override Vector2 MapPosition => new Vector2(7.45543f, -600.99f);
		public override int Radius => 159;

		public override HashSet<uint> NodeIds => new()
		{
			34821,
			34822,
			34823,
			34824,
			34825,
			34826,
		};

		public override HashSet<uint> ItemIds => new()
		{
			12,
			43900,
			43979,
			44015,
			44851,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
