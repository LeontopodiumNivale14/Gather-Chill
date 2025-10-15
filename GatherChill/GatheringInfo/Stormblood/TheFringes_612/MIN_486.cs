using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.TheFringes_612
{
	public class MIN_486 : RouteInfo
	{
		public override uint Id => 486;
		public override uint ExpansionId => 2;
		public override uint ZoneId => 612;
		public override uint GatherType => 0;
		public override Vector2 MapPosition => new Vector2(141.639f, -334.44f);
		public override int Radius => 133;

		public override HashSet<uint> NodeIds => new()
		{
			32129,
			32130,
			32131,
			32132,
			32133,
			32134,
		};

		public override HashSet<uint> ItemIds => new()
		{
			10,
			17941,
			19871,
			19969,
			23149,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
