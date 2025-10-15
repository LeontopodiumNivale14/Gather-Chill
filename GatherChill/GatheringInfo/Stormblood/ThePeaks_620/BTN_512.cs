using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.ThePeaks_620
{
	public class BTN_512 : RouteInfo
	{
		public override uint Id => 512;
		public override uint ExpansionId => 2;
		public override uint ZoneId => 620;
		public override uint GatherType => 3;
		public override Vector2 MapPosition => new Vector2(-436.002f, -462.361f);
		public override int Radius => 117;

		public override HashSet<uint> NodeIds => new()
		{
			32230,
			32231,
			32232,
			32233,
			32234,
			32235,
		};

		public override HashSet<uint> ItemIds => new()
		{
			12,
			19850,
			19856,
			19859,
			19861,
			23148,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
