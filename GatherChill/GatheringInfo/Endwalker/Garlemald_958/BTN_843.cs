using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Endwalker.Garlemald_958
{
	public class BTN_843 : RouteInfo
	{
		public override uint Id => 843;
		public override uint ExpansionId => 4;
		public override uint ZoneId => 958;
		public override uint GatherType => 3;
		public override Vector2 MapPosition => new Vector2(193.751f, 233.785f);
		public override int Radius => 116;

		public override HashSet<uint> NodeIds => new()
		{
			34003,
			34004,
			34005,
			34006,
			34007,
			34008,
		};

		public override HashSet<uint> ItemIds => new()
		{
			11,
			36091,
			36092,
			36093,
			36204,
			41070,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
