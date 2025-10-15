using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheSeaofClouds_401
{
	public class BTN_360 : RouteInfo
	{
		public override uint Id => 360;
		public override uint ExpansionId => 1;
		public override uint ZoneId => 401;
		public override uint GatherType => 3;
		public override Vector2 MapPosition => new Vector2(2.85402f, -540.651f);
		public override int Radius => 121;

		public override HashSet<uint> NodeIds => new()
		{
			31424,
			31425,
			31426,
			31427,
			31428,
			31429,
		};

		public override HashSet<uint> ItemIds => new()
		{
			12,
			12887,
			13762,
			17560,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
