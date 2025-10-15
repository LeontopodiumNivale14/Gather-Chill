using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.WesternThanalan_140
{
	public class BTN_14 : RouteInfo
	{
		public override uint Id => 14;
		public override uint ExpansionId => 0;
		public override uint ZoneId => 140;
		public override uint GatherType => 3;
		public override Vector2 MapPosition => new Vector2(153.891f, -134.284f);
		public override int Radius => 76;

		public override HashSet<uint> NodeIds => new()
		{
			30052,
			30053,
			30066,
			30067,
		};

		public override HashSet<uint> ItemIds => new()
		{
			7,
			4778,
			4823,
			5341,
			5560,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
