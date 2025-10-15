using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.Lakeland_813
{
	public class BTN_923 : RouteInfo
	{
		public override uint Id => 923;
		public override uint ExpansionId => 3;
		public override uint ZoneId => 813;
		public override uint GatherType => 2;
		public override Vector2 MapPosition => new Vector2(640.762f, -541.255f);
		public override int Radius => 133;

		public override HashSet<uint> NodeIds => new()
		{
			34434,
			34436,
			34438,
		};

		public override HashSet<uint> ItemIds => new()
		{
			38794,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
