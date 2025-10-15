using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.AmhAraeng_815
{
	public class BTN_922 : RouteInfo
	{
		public override uint Id => 922;
		public override uint ExpansionId => 3;
		public override uint ZoneId => 815;
		public override uint GatherType => 3;
		public override Vector2 MapPosition => new Vector2(-413.2f, -528.552f);
		public override int Radius => 131;

		public override HashSet<uint> NodeIds => new()
		{
			34427,
			34428,
			34429,
			34430,
			34431,
			34432,
		};

		public override HashSet<uint> ItemIds => new()
		{
			8,
			38821,
			38822,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
