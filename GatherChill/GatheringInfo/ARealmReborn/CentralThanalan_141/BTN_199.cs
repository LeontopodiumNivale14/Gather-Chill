using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.CentralThanalan_141
{
	public class BTN_199 : RouteInfo
	{
		public override uint Id => 199;
		public override uint ExpansionId => 0;
		public override uint ZoneId => 141;
		public override uint GatherType => 2;
		public override Vector2 MapPosition => new Vector2(390.257f, -89.6899f);
		public override int Radius => 93;

		public override HashSet<uint> NodeIds => new()
		{
			30988,
			30989,
			30990,
			30991,
		};

		public override HashSet<uint> ItemIds => new()
		{
			6,
			12,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
