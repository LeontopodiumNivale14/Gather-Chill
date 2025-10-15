using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheDravanianHinterlands_399
{
	public class BTN_292 : RouteInfo
	{
		public override uint Id => 292;
		public override uint ExpansionId => 1;
		public override uint ZoneId => 399;
		public override uint GatherType => 2;
		public override Vector2 MapPosition => new Vector2(-742.938f, 276.183f);
		public override int Radius => 61;

		public override HashSet<uint> NodeIds => new()
		{
			31707,
		};

		public override HashSet<uint> ItemIds => new()
		{
			5392,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
