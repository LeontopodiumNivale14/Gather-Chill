using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.EasternLaNoscea_137
{
	public class BTN_134 : RouteInfo
	{
		public override uint Id => 134;
		public override uint ExpansionId => 0;
		public override uint ZoneId => 137;
		public override uint GatherType => 3;
		public override Vector2 MapPosition => new Vector2(253.477f, 429.91f);
		public override int Radius => 44;

		public override HashSet<uint> NodeIds => new()
		{
			30337,
			30338,
			30339,
			30340,
		};

		public override HashSet<uint> ItemIds => new()
		{
			7,
			4791,
			4837,
			4838,
			5543,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
