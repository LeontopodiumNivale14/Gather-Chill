using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.Lakeland_813
{
	public class MIN_681 : RouteInfo
	{
		public override uint Id => 681;
		public override uint ExpansionId => 3;
		public override uint ZoneId => 813;
		public override uint GatherType => 0;
		public override Vector2 MapPosition => new Vector2(-836.88f, 624.631f);
		public override int Radius => 1;

		public override HashSet<uint> NodeIds => new()
		{
			32987,
		};

		public override HashSet<uint> ItemIds => new()
		{
			29968,
			30486,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
