using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.TheRaktikaGreatwood_817
{
	public class BTN_647 : RouteInfo
	{
		public override uint Id => 647;
		public override uint ExpansionId => 3;
		public override uint ZoneId => 817;
		public override uint GatherType => 5;
		public override Vector2 MapPosition => new Vector2(-799.336f, 391.93f);
		public override int Radius => 30;

		public override HashSet<uint> NodeIds => new()
		{
			32806,
		};

		public override HashSet<uint> ItemIds => new()
		{
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
