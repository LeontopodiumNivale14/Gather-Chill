using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.WesternLaNoscea_138
{
	public class BTN_234 : RouteInfo
	{
		public override uint Id => 234;
		public override uint ExpansionId => 0;
		public override uint ZoneId => 138;
		public override uint GatherType => 3;
		public override Vector2 MapPosition => new Vector2(638.75f, 340.564f);
		public override int Radius => 45;

		public override HashSet<uint> NodeIds => new()
		{
			31028,
		};

		public override HashSet<uint> ItemIds => new()
		{
			7593,
			8024,
			10098,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
