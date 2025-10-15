using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Endwalker.Thavnair_957
{
	public class BTN_857 : RouteInfo
	{
		public override uint Id => 857;
		public override uint ExpansionId => 4;
		public override uint ZoneId => 957;
		public override uint GatherType => 3;
		public override Vector2 MapPosition => new Vector2(190.022f, 9.69062f);
		public override int Radius => 16;

		public override HashSet<uint> NodeIds => new()
		{
			34044,
		};

		public override HashSet<uint> ItemIds => new()
		{
			36207,
			36214,
			37278,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
