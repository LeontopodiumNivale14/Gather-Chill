using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.ThePeaks_620
{
	public class BTN_667 : RouteInfo
	{
		public override uint Id => 667;
		public override uint ExpansionId => 2;
		public override uint ZoneId => 620;
		public override uint GatherType => 2;
		public override Vector2 MapPosition => new Vector2(-745.002f, -402.112f);
		public override int Radius => 129;

		public override HashSet<uint> NodeIds => new()
		{
			32905,
			32906,
			32907,
			32908,
			32909,
			32910,
		};

		public override HashSet<uint> ItemIds => new()
		{
			28781,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
