using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.IlMheg_816
{
	public class BTN_651 : RouteInfo
	{
		public override uint Id => 651;
		public override uint ExpansionId => 3;
		public override uint ZoneId => 816;
		public override uint GatherType => 3;
		public override Vector2 MapPosition => new Vector2(445.088f, 759.432f);
		public override int Radius => 140;

		public override HashSet<uint> NodeIds => new()
		{
			32827,
			32828,
			32829,
			32830,
			32831,
			32832,
		};

		public override HashSet<uint> ItemIds => new()
		{
			13,
			28201,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
