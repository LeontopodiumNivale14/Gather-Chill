using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Dawntrail.Kozamauka_1188
{
	public class BTN_1020 : RouteInfo
	{
		public override uint Id => 1020;
		public override uint ExpansionId => 5;
		public override uint ZoneId => 1188;
		public override uint GatherType => 3;
		public override Vector2 MapPosition => new Vector2(255.073f, 182.247f);
		public override int Radius => 138;

		public override HashSet<uint> NodeIds => new()
		{
			34976,
			34977,
			34978,
		};

		public override HashSet<uint> ItemIds => new()
		{
			43924,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
