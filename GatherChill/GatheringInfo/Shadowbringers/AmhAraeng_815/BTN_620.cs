using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.AmhAraeng_815
{
	public class BTN_620 : RouteInfo
	{
		public override uint Id => 620;
		public override uint ExpansionId => 3;
		public override uint ZoneId => 815;
		public override uint GatherType => 2;
		public override Vector2 MapPosition => new Vector2(-277.024f, 435.365f);
		public override int Radius => 135;

		public override HashSet<uint> NodeIds => new()
		{
			32739,
			32740,
			32741,
			32742,
			32743,
			32744,
		};

		public override HashSet<uint> ItemIds => new()
		{
			8,
			27686,
			27821,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
