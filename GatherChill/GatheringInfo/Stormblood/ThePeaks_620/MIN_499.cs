using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.ThePeaks_620
{
	public class MIN_499 : RouteInfo
	{
		public override uint Id => 499;
		public override uint ExpansionId => 2;
		public override uint ZoneId => 620;
		public override uint GatherType => 0;
		public override Vector2 MapPosition => new Vector2(274.512f, -443.543f);
		public override int Radius => 37;

		public override HashSet<uint> NodeIds => new()
		{
			32182,
		};

		public override HashSet<uint> ItemIds => new()
		{
			17944,
			19973,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
