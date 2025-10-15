using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.SouthShroud_153
{
	public class BTN_28 : RouteInfo
	{
		public override uint Id => 28;
		public override uint ExpansionId => 0;
		public override uint ZoneId => 153;
		public override uint GatherType => 2;
		public override Vector2 MapPosition => new Vector2(-243.74f, 440.325f);
		public override int Radius => 72;

		public override HashSet<uint> NodeIds => new()
		{
			30060,
			30061,
			30062,
			30318,
		};

		public override HashSet<uint> ItemIds => new()
		{
			3,
			5390,
			5409,
			5562,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
