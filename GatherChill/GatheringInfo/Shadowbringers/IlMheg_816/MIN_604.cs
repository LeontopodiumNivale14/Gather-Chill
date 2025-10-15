using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.IlMheg_816
{
	public class MIN_604 : RouteInfo
	{
		public override uint Id => 604;
		public override uint ExpansionId => 3;
		public override uint ZoneId => 816;
		public override uint GatherType => 0;
		public override Vector2 MapPosition => new Vector2(248.98f, -409.757f);
		public override int Radius => 19;

		public override HashSet<uint> NodeIds => new()
		{
			32678,
		};

		public override HashSet<uint> ItemIds => new()
		{
			27727,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
