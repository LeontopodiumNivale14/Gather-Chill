using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheDiadem_939
{
	public class BTN_800 : RouteInfo
	{
		public override uint Id => 800;
		public override uint ExpansionId => 1;
		public override uint ZoneId => 939;
		public override uint GatherType => 2;
		public override Vector2 MapPosition => new Vector2(-592.959f, 446.091f);
		public override int Radius => 11;

		public override HashSet<uint> NodeIds => new()
		{
			33838,
		};

		public override HashSet<uint> ItemIds => new()
		{
			29944,
			31316,
			32045,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
