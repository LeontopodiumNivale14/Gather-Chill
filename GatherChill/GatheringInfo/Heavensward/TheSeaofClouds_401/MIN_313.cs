using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheSeaofClouds_401
{
	public class MIN_313 : RouteInfo
	{
		public override uint Id => 313;
		public override uint ExpansionId => 1;
		public override uint ZoneId => 401;
		public override uint GatherType => 1;
		public override Vector2 MapPosition => new Vector2(129.171f, 435.946f);
		public override int Radius => 102;

		public override HashSet<uint> NodeIds => new()
		{
			31452,
			31455,
			31463,
		};

		public override HashSet<uint> ItemIds => new()
		{
			12,
			5218,
			5224,
			12967,
			15949,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
