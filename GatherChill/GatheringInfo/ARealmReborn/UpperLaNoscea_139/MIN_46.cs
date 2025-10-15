using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.UpperLaNoscea_139
{
	public class MIN_46 : RouteInfo
	{
		public override uint Id => 46;
		public override uint ExpansionId => 0;
		public override uint ZoneId => 139;
		public override uint GatherType => 1;
		public override Vector2 MapPosition => new Vector2(347.362f, 33.4797f);
		public override int Radius => 62;

		public override HashSet<uint> NodeIds => new()
		{
			30397,
			30398,
			30399,
			30400,
		};

		public override HashSet<uint> ItemIds => new()
		{
			5,
			5272,
			7010,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
