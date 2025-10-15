using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.Lakeland_813
{
	public class MIN_599 : RouteInfo
	{
		public override uint Id => 599;
		public override uint ExpansionId => 3;
		public override uint ZoneId => 813;
		public override uint GatherType => 0;
		public override Vector2 MapPosition => new Vector2(697.782f, 453.018f);
		public override int Radius => 163;

		public override HashSet<uint> NodeIds => new()
		{
			32663,
			33611,
			33612,
		};

		public override HashSet<uint> ItemIds => new()
		{
			12,
			27806,
			30591,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
