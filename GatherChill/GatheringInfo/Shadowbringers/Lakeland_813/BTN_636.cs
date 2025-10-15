using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.Lakeland_813
{
	public class BTN_636 : RouteInfo
	{
		public override uint Id => 636;
		public override uint ExpansionId => 3;
		public override uint ZoneId => 813;
		public override uint GatherType => 5;
		public override Vector2 MapPosition => new Vector2(-698.67f, 572.703f);
		public override int Radius => 127;

		public override HashSet<uint> NodeIds => new()
		{
			32777,
			32778,
			32779,
		};

		public override HashSet<uint> ItemIds => new()
		{
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
