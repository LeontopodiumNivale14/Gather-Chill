using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheSeaofClouds_401
{
	public class BTN_342 : RouteInfo
	{
		public override uint Id => 342;
		public override uint ExpansionId => 1;
		public override uint ZoneId => 401;
		public override uint GatherType => 2;
		public override Vector2 MapPosition => new Vector2(664.428f, -58.3798f);
		public override int Radius => 232;

		public override HashSet<uint> NodeIds => new()
		{
			31495,
		};

		public override HashSet<uint> ItemIds => new()
		{
			12889,
			12944,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
