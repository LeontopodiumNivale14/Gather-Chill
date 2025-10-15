using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.Lakeland_813
{
	public class BTN_622 : RouteInfo
	{
		public override uint Id => 622;
		public override uint ExpansionId => 3;
		public override uint ZoneId => 813;
		public override uint GatherType => 3;
		public override Vector2 MapPosition => new Vector2(506.734f, 751.952f);
		public override int Radius => 90;

		public override HashSet<uint> NodeIds => new()
		{
			32751,
			33615,
			33616,
		};

		public override HashSet<uint> ItemIds => new()
		{
			11,
			27810,
			30592,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
