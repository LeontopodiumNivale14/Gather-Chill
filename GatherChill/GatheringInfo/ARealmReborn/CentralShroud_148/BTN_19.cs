using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.CentralShroud_148
{
	public class BTN_19 : RouteInfo
	{
		public override uint Id => 19;
		public override uint ExpansionId => 0;
		public override uint ZoneId => 148;
		public override uint GatherType => 3;
		public override Vector2 MapPosition => new Vector2(-135.116f, -107.564f);
		public override int Radius => 68;

		public override HashSet<uint> NodeIds => new()
		{
			30008,
			30034,
			30035,
			30036,
			30320,
		};

		public override HashSet<uint> ItemIds => new()
		{
			4,
			4818,
			4832,
			5051,
			5514,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
