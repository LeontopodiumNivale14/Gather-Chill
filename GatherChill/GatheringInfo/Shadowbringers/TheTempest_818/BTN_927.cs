using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.TheTempest_818
{
	public class BTN_927 : RouteInfo
	{
		public override uint Id => 927;
		public override uint ExpansionId => 3;
		public override uint ZoneId => 818;
		public override uint GatherType => 3;
		public override Vector2 MapPosition => new Vector2(-80.3771f, -617.513f);
		public override int Radius => 139;

		public override HashSet<uint> NodeIds => new()
		{
			34457,
			34458,
			34459,
			34460,
			34461,
			34462,
		};

		public override HashSet<uint> ItemIds => new()
		{
			11,
			38824,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
