using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.AmhAraeng_815
{
	public class BTN_775 : RouteInfo
	{
		public override uint Id => 775;
		public override uint ExpansionId => 3;
		public override uint ZoneId => 815;
		public override uint GatherType => 2;
		public override Vector2 MapPosition => new Vector2(-145.254f, -48.5117f);
		public override int Radius => 1;

		public override HashSet<uint> NodeIds => new()
		{
			33026,
		};

		public override HashSet<uint> ItemIds => new()
		{
			33005,
			33007,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
