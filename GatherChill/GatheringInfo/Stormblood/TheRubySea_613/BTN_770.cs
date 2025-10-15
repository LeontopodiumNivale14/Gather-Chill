using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.TheRubySea_613
{
	public class BTN_770 : RouteInfo
	{
		public override uint Id => 770;
		public override uint ExpansionId => 2;
		public override uint ZoneId => 613;
		public override uint GatherType => 2;
		public override Vector2 MapPosition => new Vector2(-776.311f, -285.909f);
		public override int Radius => 1;

		public override HashSet<uint> NodeIds => new()
		{
			33021,
		};

		public override HashSet<uint> ItemIds => new()
		{
			32997,
			32999,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
