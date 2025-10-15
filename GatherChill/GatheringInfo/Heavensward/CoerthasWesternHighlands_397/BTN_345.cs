using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.CoerthasWesternHighlands_397
{
	public class BTN_345 : RouteInfo
	{
		public override uint Id => 345;
		public override uint ExpansionId => 1;
		public override uint ZoneId => 397;
		public override uint GatherType => 3;
		public override Vector2 MapPosition => new Vector2(-7.78436f, -53.6913f);
		public override int Radius => 49;

		public override HashSet<uint> NodeIds => new()
		{
			31498,
		};

		public override HashSet<uint> ItemIds => new()
		{
			4833,
			12903,
			14154,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
