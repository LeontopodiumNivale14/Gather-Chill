using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Endwalker.Thavnair_957
{
	public class BTN_894 : RouteInfo
	{
		public override uint Id => 894;
		public override uint ExpansionId => 4;
		public override uint ZoneId => 957;
		public override uint GatherType => 5;
		public override Vector2 MapPosition => new Vector2(-832.466f, 336.759f);
		public override int Radius => 88;

		public override HashSet<uint> NodeIds => new()
		{
			34317,
			34318,
			34319,
		};

		public override HashSet<uint> ItemIds => new()
		{
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
