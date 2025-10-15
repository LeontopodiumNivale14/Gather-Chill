using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.Yanxia_614
{
	public class MIN_500 : RouteInfo
	{
		public override uint Id => 500;
		public override uint ExpansionId => 2;
		public override uint ZoneId => 614;
		public override uint GatherType => 0;
		public override Vector2 MapPosition => new Vector2(418.426f, -607.431f);
		public override int Radius => 30;

		public override HashSet<uint> NodeIds => new()
		{
			32183,
		};

		public override HashSet<uint> ItemIds => new()
		{
			19972,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
