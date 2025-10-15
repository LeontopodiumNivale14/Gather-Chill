using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.Yanxia_614
{
	public class MIN_488 : RouteInfo
	{
		public override uint Id => 488;
		public override uint ExpansionId => 2;
		public override uint ZoneId => 614;
		public override uint GatherType => 0;
		public override Vector2 MapPosition => new Vector2(509.913f, 9.1125f);
		public override int Radius => 133;

		public override HashSet<uint> NodeIds => new()
		{
			32141,
			32142,
			32143,
			32144,
			32145,
			32146,
		};

		public override HashSet<uint> ItemIds => new()
		{
			8,
			19872,
			19953,
			24568,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
