using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.UpperLaNoscea_139
{
	public class BTN_232 : RouteInfo
	{
		public override uint Id => 232;
		public override uint ExpansionId => 0;
		public override uint ZoneId => 139;
		public override uint GatherType => 2;
		public override Vector2 MapPosition => new Vector2(381.315f, 199.044f);
		public override int Radius => 70;

		public override HashSet<uint> NodeIds => new()
		{
			31026,
		};

		public override HashSet<uint> ItemIds => new()
		{
			5365,
			7595,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
