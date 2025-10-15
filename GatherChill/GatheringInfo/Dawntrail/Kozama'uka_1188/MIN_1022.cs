using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Dawntrail.Kozamauka_1188
{
	public class MIN_1022 : RouteInfo
	{
		public override uint Id => 1022;
		public override uint ExpansionId => 5;
		public override uint ZoneId => 1188;
		public override uint GatherType => 0;
		public override Vector2 MapPosition => new Vector2(-723.795f, -704.928f);
		public override int Radius => 47;

		public override HashSet<uint> NodeIds => new()
		{
			34982,
		};

		public override HashSet<uint> ItemIds => new()
		{
			43919,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
