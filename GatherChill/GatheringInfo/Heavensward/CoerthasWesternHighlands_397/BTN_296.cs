using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.CoerthasWesternHighlands_397
{
	public class BTN_296 : RouteInfo
	{
		public override uint Id => 296;
		public override uint ExpansionId => 1;
		public override uint ZoneId => 397;
		public override uint GatherType => 3;
		public override Vector2 MapPosition => new Vector2(-219.074f, -293.851f);
		public override int Radius => 143;

		public override HashSet<uint> NodeIds => new()
		{
			31382,
			31383,
			31384,
			31385,
			31386,
			31387,
		};

		public override HashSet<uint> ItemIds => new()
		{
			9,
			12597,
			12883,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
