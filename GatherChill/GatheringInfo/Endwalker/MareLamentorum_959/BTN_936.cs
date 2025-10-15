using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Endwalker.MareLamentorum_959
{
	public class BTN_936 : RouteInfo
	{
		public override uint Id => 936;
		public override uint ExpansionId => 4;
		public override uint ZoneId => 959;
		public override uint GatherType => 3;
		public override Vector2 MapPosition => new Vector2(219.049f, -53.8288f);
		public override int Radius => 39;

		public override HashSet<uint> NodeIds => new()
		{
			34435,
		};

		public override HashSet<uint> ItemIds => new()
		{
			39710,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
