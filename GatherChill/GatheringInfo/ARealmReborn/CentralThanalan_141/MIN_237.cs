using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.CentralThanalan_141
{
	public class MIN_237 : RouteInfo
	{
		public override uint Id => 237;
		public override uint ExpansionId => 0;
		public override uint ZoneId => 141;
		public override uint GatherType => 0;
		public override Vector2 MapPosition => new Vector2(186.846f, -279.553f);
		public override int Radius => 25;

		public override HashSet<uint> NodeIds => new()
		{
			31031,
		};

		public override HashSet<uint> ItemIds => new()
		{
			5147,
			5148,
			7589,
			10095,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
