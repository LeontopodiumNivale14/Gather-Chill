using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheDiadem_939
{
	public class BTN_801 : RouteInfo
	{
		public override uint Id => 801;
		public override uint ExpansionId => 1;
		public override uint ZoneId => 939;
		public override uint GatherType => 3;
		public override Vector2 MapPosition => new Vector2(397.468f, 587.731f);
		public override int Radius => 15;

		public override HashSet<uint> NodeIds => new()
		{
			33839,
		};

		public override HashSet<uint> ItemIds => new()
		{
			29945,
			31317,
			32046,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
