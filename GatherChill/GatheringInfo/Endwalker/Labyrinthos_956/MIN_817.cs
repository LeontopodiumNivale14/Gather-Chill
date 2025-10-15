using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Endwalker.Labyrinthos_956
{
	public class MIN_817 : RouteInfo
	{
		public override uint Id => 817;
		public override uint ExpansionId => 4;
		public override uint ZoneId => 956;
		public override uint GatherType => 1;
		public override Vector2 MapPosition => new Vector2(187.402f, 137.038f);
		public override int Radius => 93;

		public override HashSet<uint> NodeIds => new()
		{
			33906,
			33907,
			33908,
		};

		public override HashSet<uint> ItemIds => new()
		{
			36292,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
