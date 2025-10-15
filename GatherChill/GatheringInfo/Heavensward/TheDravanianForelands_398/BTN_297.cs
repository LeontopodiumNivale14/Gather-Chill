using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheDravanianForelands_398
{
	public class BTN_297 : RouteInfo
	{
		public override uint Id => 297;
		public override uint ExpansionId => 1;
		public override uint ZoneId => 398;
		public override uint GatherType => 3;
		public override Vector2 MapPosition => new Vector2(751.667f, -151.244f);
		public override int Radius => 143;

		public override HashSet<uint> NodeIds => new()
		{
			31388,
			31389,
			31390,
			31391,
			31392,
			31393,
		};

		public override HashSet<uint> ItemIds => new()
		{
			8,
			12598,
			12880,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
