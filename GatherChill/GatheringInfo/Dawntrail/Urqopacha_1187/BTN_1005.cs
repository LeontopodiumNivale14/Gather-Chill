using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Dawntrail.Urqopacha_1187
{
	public class BTN_1005 : RouteInfo
	{
		public override uint Id => 1005;
		public override uint ExpansionId => 5;
		public override uint ZoneId => 1187;
		public override uint GatherType => 5;
		public override Vector2 MapPosition => new Vector2(-739.507f, -98.4267f);
		public override int Radius => 113;

		public override HashSet<uint> NodeIds => new()
		{
			34935,
			34936,
			34937,
		};

		public override HashSet<uint> ItemIds => new()
		{
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
