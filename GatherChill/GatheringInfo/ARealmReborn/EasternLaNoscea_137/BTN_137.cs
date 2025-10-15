using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.EasternLaNoscea_137
{
	public class BTN_137 : RouteInfo
	{
		public override uint Id => 137;
		public override uint ExpansionId => 0;
		public override uint ZoneId => 137;
		public override uint GatherType => 2;
		public override Vector2 MapPosition => new Vector2(-111.078f, 202.16f);
		public override int Radius => 74;

		public override HashSet<uint> NodeIds => new()
		{
			30349,
			30350,
			30351,
			30352,
		};

		public override HashSet<uint> ItemIds => new()
		{
			7,
			4807,
			4842,
			4844,
			5391,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
