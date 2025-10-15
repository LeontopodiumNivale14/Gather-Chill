using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.TheFringes_612
{
	public class BTN_509 : RouteInfo
	{
		public override uint Id => 509;
		public override uint ExpansionId => 2;
		public override uint ZoneId => 612;
		public override uint GatherType => 3;
		public override Vector2 MapPosition => new Vector2(-477.902f, 183.036f);
		public override int Radius => 91;

		public override HashSet<uint> NodeIds => new()
		{
			32212,
			32213,
			32214,
			32215,
			32216,
			32217,
		};

		public override HashSet<uint> ItemIds => new()
		{
			8,
			17945,
			19853,
			19917,
			19982,
			23149,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
