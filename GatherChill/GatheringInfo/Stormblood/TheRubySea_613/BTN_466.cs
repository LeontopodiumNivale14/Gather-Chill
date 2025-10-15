using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.TheRubySea_613
{
	public class BTN_466 : RouteInfo
	{
		public override uint Id => 466;
		public override uint ExpansionId => 2;
		public override uint ZoneId => 613;
		public override uint GatherType => 5;
		public override Vector2 MapPosition => new Vector2(-234.18f, -53.1182f);
		public override int Radius => 110;

		public override HashSet<uint> NodeIds => new()
		{
			32077,
			32078,
			32079,
		};

		public override HashSet<uint> ItemIds => new()
		{
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
