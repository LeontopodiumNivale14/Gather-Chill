using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.AmhAraeng_815
{
	public class BTN_611 : RouteInfo
	{
		public override uint Id => 611;
		public override uint ExpansionId => 3;
		public override uint ZoneId => 815;
		public override uint GatherType => 3;
		public override Vector2 MapPosition => new Vector2(544.603f, -233.566f);
		public override int Radius => 113;

		public override HashSet<uint> NodeIds => new()
		{
			32685,
			32686,
			32687,
			32688,
			32689,
			32690,
		};

		public override HashSet<uint> ItemIds => new()
		{
			8,
			26752,
			27827,
			28200,
			28202,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
