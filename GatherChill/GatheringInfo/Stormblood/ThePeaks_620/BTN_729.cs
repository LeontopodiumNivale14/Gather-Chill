using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.ThePeaks_620
{
	public class BTN_729 : RouteInfo
	{
		public override uint Id => 729;
		public override uint ExpansionId => 2;
		public override uint ZoneId => 620;
		public override uint GatherType => 2;
		public override Vector2 MapPosition => new Vector2(619.986f, -613.585f);
		public override int Radius => 111;

		public override HashSet<uint> NodeIds => new()
		{
			33319,
			33320,
			33321,
			33322,
			33323,
			33324,
		};

		public override HashSet<uint> ItemIds => new()
		{
			11,
			30501,
			33232,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
