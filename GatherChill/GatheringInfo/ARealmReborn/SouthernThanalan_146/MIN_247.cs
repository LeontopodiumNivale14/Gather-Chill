using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.SouthernThanalan_146
{
	public class MIN_247 : RouteInfo
	{
		public override uint Id => 247;
		public override uint ExpansionId => 0;
		public override uint ZoneId => 146;
		public override uint GatherType => 0;
		public override Vector2 MapPosition => new Vector2(-215.813f, -544.755f);
		public override int Radius => 122;

		public override HashSet<uint> NodeIds => new()
		{
			31055,
		};

		public override HashSet<uint> ItemIds => new()
		{
			5120,
			10095,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
