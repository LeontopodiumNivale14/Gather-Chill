using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.TheAzimSteppe_622
{
	public class BTN_519 : RouteInfo
	{
		public override uint Id => 519;
		public override uint ExpansionId => 2;
		public override uint ZoneId => 622;
		public override uint GatherType => 3;
		public override Vector2 MapPosition => new Vector2(-279.171f, 327.741f);
		public override int Radius => 144;

		public override HashSet<uint> NodeIds => new()
		{
			32262,
			32263,
			32264,
			32265,
			32266,
			32267,
		};

		public override HashSet<uint> ItemIds => new()
		{
			11,
			19866,
			19867,
			19914,
			19915,
			24569,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
