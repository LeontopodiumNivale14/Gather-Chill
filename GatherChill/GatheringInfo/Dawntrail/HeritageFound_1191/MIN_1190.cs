using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Dawntrail.HeritageFound_1191
{
	public class MIN_1190 : RouteInfo
	{
		public override uint Id => 1190;
		public override uint ExpansionId => 5;
		public override uint ZoneId => 1191;
		public override uint GatherType => 1;
		public override Vector2 MapPosition => new Vector2(-373.58f, -232.706f);
		public override int Radius => 16;

		public override HashSet<uint> NodeIds => new()
		{
			35239,
		};

		public override HashSet<uint> ItemIds => new()
		{
			45973,
			46244,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
