using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.WesternThanalan_140
{
	public class MIN_154 : RouteInfo
	{
		public override uint Id => 154;
		public override uint ExpansionId => 0;
		public override uint ZoneId => 140;
		public override uint GatherType => 0;
		public override Vector2 MapPosition => new Vector2(261.123f, 209.031f);
		public override int Radius => 61;

		public override HashSet<uint> NodeIds => new()
		{
			30002,
			30413,
			30414,
			30415,
			30416,
		};

		public override HashSet<uint> ItemIds => new()
		{
			2,
			7,
			5106,
			5488,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
