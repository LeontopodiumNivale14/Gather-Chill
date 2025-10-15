using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.AzysLla_402
{
	public class BTN_350 : RouteInfo
	{
		public override uint Id => 350;
		public override uint ExpansionId => 1;
		public override uint ZoneId => 402;
		public override uint GatherType => 2;
		public override Vector2 MapPosition => new Vector2(23.4141f, -670.351f);
		public override int Radius => 58;

		public override HashSet<uint> NodeIds => new()
		{
			31503,
		};

		public override HashSet<uint> ItemIds => new()
		{
			12587,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
