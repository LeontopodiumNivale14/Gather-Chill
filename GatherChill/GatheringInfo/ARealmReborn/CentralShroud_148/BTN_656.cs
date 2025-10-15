using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.CentralShroud_148
{
	public class BTN_656 : RouteInfo
	{
		public override uint Id => 656;
		public override uint ExpansionId => 0;
		public override uint ZoneId => 148;
		public override uint GatherType => 2;
		public override Vector2 MapPosition => new Vector2(-366.564f, -133.411f);
		public override int Radius => 32;

		public override HashSet<uint> NodeIds => new()
		{
			32851,
			32852,
			32853,
			32854,
		};

		public override HashSet<uint> ItemIds => new()
		{
			28769,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
