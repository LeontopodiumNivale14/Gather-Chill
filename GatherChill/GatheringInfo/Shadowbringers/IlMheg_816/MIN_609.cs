using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.IlMheg_816
{
	public class MIN_609 : RouteInfo
	{
		public override uint Id => 609;
		public override uint ExpansionId => 3;
		public override uint ZoneId => 816;
		public override uint GatherType => 0;
		public override Vector2 MapPosition => new Vector2(454.442f, -44.3393f);
		public override int Radius => 35;

		public override HashSet<uint> NodeIds => new()
		{
			32683,
		};

		public override HashSet<uint> ItemIds => new()
		{
			27705,
			28717,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
