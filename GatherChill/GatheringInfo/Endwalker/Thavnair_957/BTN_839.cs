using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Endwalker.Thavnair_957
{
	public class BTN_839 : RouteInfo
	{
		public override uint Id => 839;
		public override uint ExpansionId => 4;
		public override uint ZoneId => 957;
		public override uint GatherType => 2;
		public override Vector2 MapPosition => new Vector2(-227.311f, 37.5474f);
		public override int Radius => 95;

		public override HashSet<uint> NodeIds => new()
		{
			33982,
			33983,
			33984,
			33985,
			33986,
			33987,
		};

		public override HashSet<uint> ItemIds => new()
		{
			8,
			36086,
			36087,
			36191,
			36673,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
