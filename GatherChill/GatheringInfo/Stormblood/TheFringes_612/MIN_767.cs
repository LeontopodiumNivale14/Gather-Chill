using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.TheFringes_612
{
	public class MIN_767 : RouteInfo
	{
		public override uint Id => 767;
		public override uint ExpansionId => 2;
		public override uint ZoneId => 612;
		public override uint GatherType => 0;
		public override Vector2 MapPosition => new Vector2(504.163f, 495.058f);
		public override int Radius => 1;

		public override HashSet<uint> NodeIds => new()
		{
			31511,
		};

		public override HashSet<uint> ItemIds => new()
		{
			32987,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
