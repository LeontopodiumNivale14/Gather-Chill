using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.CoerthasCentralHighlands_155
{
	public class MIN_225 : RouteInfo
	{
		public override uint Id => 225;
		public override uint ExpansionId => 0;
		public override uint ZoneId => 155;
		public override uint GatherType => 1;
		public override Vector2 MapPosition => new Vector2(94.7936f, 159.225f);
		public override int Radius => 77;

		public override HashSet<uint> NodeIds => new()
		{
			31013,
		};

		public override HashSet<uint> ItemIds => new()
		{
			5158,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
