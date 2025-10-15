using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Endwalker.Garlemald_958
{
	public class BTN_935 : RouteInfo
	{
		public override uint Id => 935;
		public override uint ExpansionId => 4;
		public override uint ZoneId => 958;
		public override uint GatherType => 3;
		public override Vector2 MapPosition => new Vector2(-185.699f, -450.096f);
		public override int Radius => 35;

		public override HashSet<uint> NodeIds => new()
		{
			34433,
		};

		public override HashSet<uint> ItemIds => new()
		{
			39709,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
