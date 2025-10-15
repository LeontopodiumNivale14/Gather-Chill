using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.SouthShroud_153
{
	public class BTN_657 : RouteInfo
	{
		public override uint Id => 657;
		public override uint ExpansionId => 0;
		public override uint ZoneId => 153;
		public override uint GatherType => 3;
		public override Vector2 MapPosition => new Vector2(-237.067f, 554.389f);
		public override int Radius => 56;

		public override HashSet<uint> NodeIds => new()
		{
			32855,
			32856,
			32857,
			32858,
		};

		public override HashSet<uint> ItemIds => new()
		{
			28773,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
