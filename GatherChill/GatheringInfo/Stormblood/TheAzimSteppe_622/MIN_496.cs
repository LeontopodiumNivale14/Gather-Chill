using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.TheAzimSteppe_622
{
	public class MIN_496 : RouteInfo
	{
		public override uint Id => 496;
		public override uint ExpansionId => 2;
		public override uint ZoneId => 622;
		public override uint GatherType => 0;
		public override Vector2 MapPosition => new Vector2(662.843f, 149.699f);
		public override int Radius => 123;

		public override HashSet<uint> NodeIds => new()
		{
			31508,
			31701,
			32179,
		};

		public override HashSet<uint> ItemIds => new()
		{
			12,
			20009,
			23220,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
