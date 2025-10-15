using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.TheRubySea_613
{
	public class BTN_465 : RouteInfo
	{
		public override uint Id => 465;
		public override uint ExpansionId => 2;
		public override uint ZoneId => 613;
		public override uint GatherType => 5;
		public override Vector2 MapPosition => new Vector2(349.013f, -473.946f);
		public override int Radius => 114;

		public override HashSet<uint> NodeIds => new()
		{
			32074,
			32075,
			32076,
		};

		public override HashSet<uint> ItemIds => new()
		{
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
