using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheSeaofClouds_401
{
	public class MIN_716 : RouteInfo
	{
		public override uint Id => 716;
		public override uint ExpansionId => 1;
		public override uint ZoneId => 401;
		public override uint GatherType => 0;
		public override Vector2 MapPosition => new Vector2(211.385f, 15.7831f);
		public override int Radius => 110;

		public override HashSet<uint> NodeIds => new()
		{
			33241,
			33242,
			33243,
			33244,
			33245,
			33246,
		};

		public override HashSet<uint> ItemIds => new()
		{
			29671,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
