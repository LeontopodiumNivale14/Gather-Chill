using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.TheRubySea_613
{
	public class MIN_312 : RouteInfo
	{
		public override uint Id => 312;
		public override uint ExpansionId => 2;
		public override uint ZoneId => 613;
		public override uint GatherType => 0;
		public override Vector2 MapPosition => new Vector2(-14.8286f, 641.732f);
		public override int Radius => 1;

		public override HashSet<uint> NodeIds => new()
		{
			31441,
		};

		public override HashSet<uint> ItemIds => new()
		{
			32976,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
