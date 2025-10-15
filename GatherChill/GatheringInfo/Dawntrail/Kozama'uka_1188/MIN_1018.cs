using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Dawntrail.Kozamauka_1188
{
	public class MIN_1018 : RouteInfo
	{
		public override uint Id => 1018;
		public override uint ExpansionId => 5;
		public override uint ZoneId => 1188;
		public override uint GatherType => 0;
		public override Vector2 MapPosition => new Vector2(810.255f, -8.3798f);
		public override int Radius => 124;

		public override HashSet<uint> NodeIds => new()
		{
			34970,
			34971,
			34972,
		};

		public override HashSet<uint> ItemIds => new()
		{
			43917,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
