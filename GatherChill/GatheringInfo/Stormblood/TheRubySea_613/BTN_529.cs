using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.TheRubySea_613
{
	public class BTN_529 : RouteInfo
	{
		public override uint Id => 529;
		public override uint ExpansionId => 2;
		public override uint ZoneId => 613;
		public override uint GatherType => 3;
		public override Vector2 MapPosition => new Vector2(-433.095f, -356.709f);
		public override int Radius => 155;

		public override HashSet<uint> NodeIds => new()
		{
			32277,
			32278,
			32279,
			32280,
			32281,
			32282,
		};

		public override HashSet<uint> ItemIds => new()
		{
			13,
			17947,
			19880,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
