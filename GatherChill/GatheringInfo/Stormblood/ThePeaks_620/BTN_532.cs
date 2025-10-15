using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.ThePeaks_620
{
	public class BTN_532 : RouteInfo
	{
		public override uint Id => 532;
		public override uint ExpansionId => 2;
		public override uint ZoneId => 620;
		public override uint GatherType => 3;
		public override Vector2 MapPosition => new Vector2(-381.567f, -759.642f);
		public override int Radius => 105;

		public override HashSet<uint> NodeIds => new()
		{
			32290,
			32291,
			32292,
			32293,
			32294,
			32295,
		};

		public override HashSet<uint> ItemIds => new()
		{
			12,
			20783,
			20784,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
