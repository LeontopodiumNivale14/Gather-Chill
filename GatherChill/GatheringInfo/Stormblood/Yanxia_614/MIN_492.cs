using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.Yanxia_614
{
	public class MIN_492 : RouteInfo
	{
		public override uint Id => 492;
		public override uint ExpansionId => 2;
		public override uint ZoneId => 614;
		public override uint GatherType => 1;
		public override Vector2 MapPosition => new Vector2(373.346f, -738.879f);
		public override int Radius => 113;

		public override HashSet<uint> NodeIds => new()
		{
			31490,
			31491,
			32165,
		};

		public override HashSet<uint> ItemIds => new()
		{
			8,
			20010,
			23220,
			33152,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
