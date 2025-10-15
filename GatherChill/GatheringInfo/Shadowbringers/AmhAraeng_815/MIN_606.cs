using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.AmhAraeng_815
{
	public class MIN_606 : RouteInfo
	{
		public override uint Id => 606;
		public override uint ExpansionId => 3;
		public override uint ZoneId => 815;
		public override uint GatherType => 0;
		public override Vector2 MapPosition => new Vector2(-75.2747f, 373.778f);
		public override int Radius => 34;

		public override HashSet<uint> NodeIds => new()
		{
			32680,
		};

		public override HashSet<uint> ItemIds => new()
		{
			27729,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
