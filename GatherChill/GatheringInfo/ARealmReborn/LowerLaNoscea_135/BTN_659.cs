using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.LowerLaNoscea_135
{
	public class BTN_659 : RouteInfo
	{
		public override uint Id => 659;
		public override uint ExpansionId => 0;
		public override uint ZoneId => 135;
		public override uint GatherType => 3;
		public override Vector2 MapPosition => new Vector2(173.367f, 237.287f);
		public override int Radius => 57;

		public override HashSet<uint> NodeIds => new()
		{
			32863,
			32864,
			32865,
			32866,
		};

		public override HashSet<uint> ItemIds => new()
		{
			28768,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
