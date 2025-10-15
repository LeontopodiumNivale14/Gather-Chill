using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheDravanianForelands_398
{
	public class BTN_340 : RouteInfo
	{
		public override uint Id => 340;
		public override uint ExpansionId => 1;
		public override uint ZoneId => 398;
		public override uint GatherType => 3;
		public override Vector2 MapPosition => new Vector2(202.252f, -462.247f);
		public override int Radius => 28;

		public override HashSet<uint> NodeIds => new()
		{
			31493,
		};

		public override HashSet<uint> ItemIds => new()
		{
			12896,
			13765,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
