using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Endwalker.MareLamentorum_959
{
	public class MIN_835 : RouteInfo
	{
		public override uint Id => 835;
		public override uint ExpansionId => 4;
		public override uint ZoneId => 959;
		public override uint GatherType => 1;
		public override Vector2 MapPosition => new Vector2(-598.067f, 92.4404f);
		public override int Radius => 27;

		public override HashSet<uint> NodeIds => new()
		{
			33968,
		};

		public override HashSet<uint> ItemIds => new()
		{
			36167,
			36215,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
