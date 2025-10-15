using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Endwalker.Thavnair_957
{
	public class BTN_889 : RouteInfo
	{
		public override uint Id => 889;
		public override uint ExpansionId => 4;
		public override uint ZoneId => 957;
		public override uint GatherType => 5;
		public override Vector2 MapPosition => new Vector2(-346.914f, 777.235f);
		public override int Radius => 119;

		public override HashSet<uint> NodeIds => new()
		{
			34302,
			34303,
			34304,
		};

		public override HashSet<uint> ItemIds => new()
		{
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
