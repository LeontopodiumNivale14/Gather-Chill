using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheSeaofClouds_401
{
	public class BTN_356 : RouteInfo
	{
		public override uint Id => 356;
		public override uint ExpansionId => 1;
		public override uint ZoneId => 401;
		public override uint GatherType => 2;
		public override Vector2 MapPosition => new Vector2(193.057f, 543.821f);
		public override int Radius => 162;

		public override HashSet<uint> NodeIds => new()
		{
			31400,
			31401,
			31402,
			31403,
			31404,
			31405,
		};

		public override HashSet<uint> ItemIds => new()
		{
			13,
			12586,
			12891,
			13763,
			13764,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
