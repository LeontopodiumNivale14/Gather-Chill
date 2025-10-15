using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheSeaofClouds_401
{
	public class BTN_720 : RouteInfo
	{
		public override uint Id => 720;
		public override uint ExpansionId => 1;
		public override uint ZoneId => 401;
		public override uint GatherType => 3;
		public override Vector2 MapPosition => new Vector2(354.11f, 36.259f);
		public override int Radius => 117;

		public override HashSet<uint> NodeIds => new()
		{
			33265,
			33266,
			33267,
			33268,
			33269,
			33270,
		};

		public override HashSet<uint> ItemIds => new()
		{
			29669,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
