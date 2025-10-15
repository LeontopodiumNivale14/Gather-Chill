using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.Yanxia_614
{
	public class BTN_670 : RouteInfo
	{
		public override uint Id => 670;
		public override uint ExpansionId => 2;
		public override uint ZoneId => 614;
		public override uint GatherType => 3;
		public override Vector2 MapPosition => new Vector2(-552.016f, 737.976f);
		public override int Radius => 103;

		public override HashSet<uint> NodeIds => new()
		{
			32923,
			32924,
			32925,
			32926,
			32927,
			32928,
		};

		public override HashSet<uint> ItemIds => new()
		{
			28785,
			28909,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
