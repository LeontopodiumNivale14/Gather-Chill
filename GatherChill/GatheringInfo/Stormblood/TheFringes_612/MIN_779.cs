using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.TheFringes_612
{
	public class MIN_779 : RouteInfo
	{
		public override uint Id => 779;
		public override uint ExpansionId => 2;
		public override uint ZoneId => 612;
		public override uint GatherType => 0;
		public override Vector2 MapPosition => new Vector2(-287.263f, 168.327f);
		public override int Radius => 156;

		public override HashSet<uint> NodeIds => new()
		{
			33622,
			33623,
			33624,
		};

		public override HashSet<uint> ItemIds => new()
		{
			32989,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
