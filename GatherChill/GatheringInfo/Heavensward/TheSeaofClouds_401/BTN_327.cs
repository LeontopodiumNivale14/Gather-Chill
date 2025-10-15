using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheSeaofClouds_401
{
	public class BTN_327 : RouteInfo
	{
		public override uint Id => 327;
		public override uint ExpansionId => 1;
		public override uint ZoneId => 401;
		public override uint GatherType => 3;
		public override Vector2 MapPosition => new Vector2(-435.466f, -91.7619f);
		public override int Radius => 122;

		public override HashSet<uint> NodeIds => new()
		{
			31468,
			31471,
			31473,
		};

		public override HashSet<uint> ItemIds => new()
		{
			12971,
			12972,
			12973,
			15948,
			33148,
			33149,
			33150,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
