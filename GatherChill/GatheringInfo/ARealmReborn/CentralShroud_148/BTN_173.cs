using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.CentralShroud_148
{
	public class BTN_173 : RouteInfo
	{
		public override uint Id => 173;
		public override uint ExpansionId => 0;
		public override uint ZoneId => 148;
		public override uint GatherType => 2;
		public override Vector2 MapPosition => new Vector2(158.1f, 469.518f);
		public override int Radius => 73;

		public override HashSet<uint> NodeIds => new()
		{
			30145,
			30146,
			30147,
			30312,
		};

		public override HashSet<uint> ItemIds => new()
		{
			4,
			10,
			5819,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
