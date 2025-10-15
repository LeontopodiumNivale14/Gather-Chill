using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Dawntrail.Shaaloani_1190
{
	public class MIN_978 : RouteInfo
	{
		public override uint Id => 978;
		public override uint ExpansionId => 5;
		public override uint ZoneId => 1190;
		public override uint GatherType => 0;
		public override Vector2 MapPosition => new Vector2(580.425f, -783.433f);
		public override int Radius => 208;

		public override HashSet<uint> NodeIds => new()
		{
			34773,
			34774,
			34775,
			34776,
			34777,
			34778,
		};

		public override HashSet<uint> ItemIds => new()
		{
			8,
			43901,
			43993,
			44852,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
