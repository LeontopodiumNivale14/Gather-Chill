using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Dawntrail.YakTel_1189
{
	public class MIN_980 : RouteInfo
	{
		public override uint Id => 980;
		public override uint ExpansionId => 5;
		public override uint ZoneId => 1189;
		public override uint GatherType => 0;
		public override Vector2 MapPosition => new Vector2(606.413f, -644.911f);
		public override int Radius => 182;

		public override HashSet<uint> NodeIds => new()
		{
			34785,
			34786,
			34787,
			34788,
			34789,
			34790,
		};

		public override HashSet<uint> ItemIds => new()
		{
			9,
			43914,
			44004,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
