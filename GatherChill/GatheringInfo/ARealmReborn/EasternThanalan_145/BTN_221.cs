using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.EasternThanalan_145
{
	public class BTN_221 : RouteInfo
	{
		public override uint Id => 221;
		public override uint ExpansionId => 0;
		public override uint ZoneId => 145;
		public override uint GatherType => 3;
		public override Vector2 MapPosition => new Vector2(-457.494f, -280.03f);
		public override int Radius => 23;

		public override HashSet<uint> NodeIds => new()
		{
			31009,
		};

		public override HashSet<uint> ItemIds => new()
		{
			4800,
			10098,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
