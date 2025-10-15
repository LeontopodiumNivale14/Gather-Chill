using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Endwalker.Thavnair_957
{
	public class MIN_818 : RouteInfo
	{
		public override uint Id => 818;
		public override uint ExpansionId => 4;
		public override uint ZoneId => 957;
		public override uint GatherType => 0;
		public override Vector2 MapPosition => new Vector2(-100.934f, 473.142f);
		public override int Radius => 141;

		public override HashSet<uint> NodeIds => new()
		{
			33909,
			33910,
			33911,
			33912,
			33913,
			33914,
		};

		public override HashSet<uint> ItemIds => new()
		{
			9,
			36163,
			36673,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
