using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheDiadem_901
{
	public class BTN_712 : RouteInfo
	{
		public override uint Id => 712;
		public override uint ExpansionId => 1;
		public override uint ZoneId => 901;
		public override uint GatherType => 3;
		public override Vector2 MapPosition => new Vector2(393.411f, 587.416f);
		public override int Radius => 14;

		public override HashSet<uint> NodeIds => new()
		{
			33232,
		};

		public override HashSet<uint> ItemIds => new()
		{
			29945,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
