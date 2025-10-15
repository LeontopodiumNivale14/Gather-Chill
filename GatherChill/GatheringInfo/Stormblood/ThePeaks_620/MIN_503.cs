using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.ThePeaks_620
{
	public class MIN_503 : RouteInfo
	{
		public override uint Id => 503;
		public override uint ExpansionId => 2;
		public override uint ZoneId => 620;
		public override uint GatherType => 0;
		public override Vector2 MapPosition => new Vector2(-278.115f, 621.511f);
		public override int Radius => 19;

		public override HashSet<uint> NodeIds => new()
		{
			32186,
		};

		public override HashSet<uint> ItemIds => new()
		{
			19959,
			21085,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
