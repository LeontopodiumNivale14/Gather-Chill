using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Endwalker.Thavnair_957
{
	public class MIN_833 : RouteInfo
	{
		public override uint Id => 833;
		public override uint ExpansionId => 4;
		public override uint ZoneId => 957;
		public override uint GatherType => 1;
		public override Vector2 MapPosition => new Vector2(545.909f, 173.446f);
		public override int Radius => 12;

		public override HashSet<uint> NodeIds => new()
		{
			33966,
		};

		public override HashSet<uint> ItemIds => new()
		{
			36300,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
