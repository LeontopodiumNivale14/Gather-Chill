using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.LowerLaNoscea_135
{
	public class MIN_150 : RouteInfo
	{
		public override uint Id => 150;
		public override uint ExpansionId => 0;
		public override uint ZoneId => 135;
		public override uint GatherType => 1;
		public override Vector2 MapPosition => new Vector2(12.3286f, 675.9f);
		public override int Radius => 75;

		public override HashSet<uint> NodeIds => new()
		{
			30552,
			30553,
			30554,
			30555,
		};

		public override HashSet<uint> ItemIds => new()
		{
			5,
			11,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
