using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.TheAzimSteppe_622
{
	public class BTN_476 : RouteInfo
	{
		public override uint Id => 476;
		public override uint ExpansionId => 2;
		public override uint ZoneId => 622;
		public override uint GatherType => 5;
		public override Vector2 MapPosition => new Vector2(86.5185f, 59.3523f);
		public override int Radius => 96;

		public override HashSet<uint> NodeIds => new()
		{
			32107,
			32108,
			32109,
		};

		public override HashSet<uint> ItemIds => new()
		{
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
