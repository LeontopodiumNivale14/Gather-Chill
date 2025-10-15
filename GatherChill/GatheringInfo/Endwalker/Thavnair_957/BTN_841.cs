using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Endwalker.Thavnair_957
{
	public class BTN_841 : RouteInfo
	{
		public override uint Id => 841;
		public override uint ExpansionId => 4;
		public override uint ZoneId => 957;
		public override uint GatherType => 3;
		public override Vector2 MapPosition => new Vector2(123.809f, 455.264f);
		public override int Radius => 101;

		public override HashSet<uint> NodeIds => new()
		{
			33994,
			33995,
			33996,
		};

		public override HashSet<uint> ItemIds => new()
		{
			36301,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
