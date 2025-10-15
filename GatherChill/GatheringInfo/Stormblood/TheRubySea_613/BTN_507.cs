using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.TheRubySea_613
{
	public class BTN_507 : RouteInfo
	{
		public override uint Id => 507;
		public override uint ExpansionId => 2;
		public override uint ZoneId => 613;
		public override uint GatherType => 2;
		public override Vector2 MapPosition => new Vector2(-37.9993f, -732.574f);
		public override int Radius => 118;

		public override HashSet<uint> NodeIds => new()
		{
			32200,
			32201,
			32202,
			32203,
			32204,
			32205,
		};

		public override HashSet<uint> ItemIds => new()
		{
			13,
			19863,
			19931,
			24567,
			35847,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
