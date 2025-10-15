using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.CoerthasCentralHighlands_155
{
	public class BTN_212 : RouteInfo
	{
		public override uint Id => 212;
		public override uint ExpansionId => 0;
		public override uint ZoneId => 155;
		public override uint GatherType => 2;
		public override Vector2 MapPosition => new Vector2(253.717f, 146.812f);
		public override int Radius => 99;

		public override HashSet<uint> NodeIds => new()
		{
			31000,
		};

		public override HashSet<uint> ItemIds => new()
		{
			5547,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
