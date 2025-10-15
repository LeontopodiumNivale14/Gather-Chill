using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Dawntrail.YakTel_1189
{
	public class BTN_1027 : RouteInfo
	{
		public override uint Id => 1027;
		public override uint ExpansionId => 5;
		public override uint ZoneId => 1189;
		public override uint GatherType => 2;
		public override Vector2 MapPosition => new Vector2(767.641f, 666.041f);
		public override int Radius => 37;

		public override HashSet<uint> NodeIds => new()
		{
			34987,
		};

		public override HashSet<uint> ItemIds => new()
		{
			43927,
			43928,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
