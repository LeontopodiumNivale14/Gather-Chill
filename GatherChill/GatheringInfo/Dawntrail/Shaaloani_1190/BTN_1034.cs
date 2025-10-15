using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Dawntrail.Shaaloani_1190
{
	public class BTN_1034 : RouteInfo
	{
		public override uint Id => 1034;
		public override uint ExpansionId => 5;
		public override uint ZoneId => 1190;
		public override uint GatherType => 2;
		public override Vector2 MapPosition => new Vector2(-531.047f, 480.764f);
		public override int Radius => 7;

		public override HashSet<uint> NodeIds => new()
		{
			34994,
		};

		public override HashSet<uint> ItemIds => new()
		{
			44140,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
