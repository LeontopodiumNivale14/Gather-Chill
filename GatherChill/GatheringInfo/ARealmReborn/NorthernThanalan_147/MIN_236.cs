using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.NorthernThanalan_147
{
	public class MIN_236 : RouteInfo
	{
		public override uint Id => 236;
		public override uint ExpansionId => 0;
		public override uint ZoneId => 147;
		public override uint GatherType => 0;
		public override Vector2 MapPosition => new Vector2(-208.212f, -66.3823f);
		public override int Radius => 44;

		public override HashSet<uint> NodeIds => new()
		{
			31030,
		};

		public override HashSet<uint> ItemIds => new()
		{
			5149,
			5150,
			7588,
			10095,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
