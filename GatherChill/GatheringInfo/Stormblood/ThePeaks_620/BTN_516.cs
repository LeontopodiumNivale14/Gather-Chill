using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.ThePeaks_620
{
	public class BTN_516 : RouteInfo
	{
		public override uint Id => 516;
		public override uint ExpansionId => 2;
		public override uint ZoneId => 620;
		public override uint GatherType => 3;
		public override Vector2 MapPosition => new Vector2(252.624f, 488.872f);
		public override int Radius => 124;

		public override HashSet<uint> NodeIds => new()
		{
			32249,
			32250,
			32251,
			32252,
			32253,
			32254,
		};

		public override HashSet<uint> ItemIds => new()
		{
			12,
			19858,
			19912,
			19989,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
