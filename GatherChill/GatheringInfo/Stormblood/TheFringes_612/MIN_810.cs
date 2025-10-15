using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.TheFringes_612
{
	public class MIN_810 : RouteInfo
	{
		public override uint Id => 810;
		public override uint ExpansionId => 2;
		public override uint ZoneId => 612;
		public override uint GatherType => 0;
		public override Vector2 MapPosition => new Vector2(469.358f, 65.2898f);
		public override int Radius => 146;

		public override HashSet<uint> NodeIds => new()
		{
			33867,
			33868,
			33869,
			33870,
			33871,
			33872,
		};

		public override HashSet<uint> ItemIds => new()
		{
			10,
			33231,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
