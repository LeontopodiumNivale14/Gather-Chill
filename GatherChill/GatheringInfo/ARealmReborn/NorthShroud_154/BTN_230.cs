using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.NorthShroud_154
{
	public class BTN_230 : RouteInfo
	{
		public override uint Id => 230;
		public override uint ExpansionId => 0;
		public override uint ZoneId => 154;
		public override uint GatherType => 2;
		public override Vector2 MapPosition => new Vector2(-244.032f, 304.562f);
		public override int Radius => 59;

		public override HashSet<uint> NodeIds => new()
		{
			31024,
		};

		public override HashSet<uint> ItemIds => new()
		{
			7590,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
