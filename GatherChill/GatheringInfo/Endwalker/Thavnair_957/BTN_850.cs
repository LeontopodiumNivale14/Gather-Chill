using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Endwalker.Thavnair_957
{
	public class BTN_850 : RouteInfo
	{
		public override uint Id => 850;
		public override uint ExpansionId => 4;
		public override uint ZoneId => 957;
		public override uint GatherType => 2;
		public override Vector2 MapPosition => new Vector2(262.784f, -291.113f);
		public override int Radius => 134;

		public override HashSet<uint> NodeIds => new()
		{
			34037,
			34052,
			34053,
		};

		public override HashSet<uint> ItemIds => new()
		{
			8,
			36288,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
