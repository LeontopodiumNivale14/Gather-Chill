using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheSeaofClouds_401
{
	public class BTN_348 : RouteInfo
	{
		public override uint Id => 348;
		public override uint ExpansionId => 1;
		public override uint ZoneId => 401;
		public override uint GatherType => 2;
		public override Vector2 MapPosition => new Vector2(-15.8187f, -809.312f);
		public override int Radius => 107;

		public override HashSet<uint> NodeIds => new()
		{
			31501,
		};

		public override HashSet<uint> ItemIds => new()
		{
			12902,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
