using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.SouthernThanalan_146
{
	public class MIN_169 : RouteInfo
	{
		public override uint Id => 169;
		public override uint ExpansionId => 0;
		public override uint ZoneId => 146;
		public override uint GatherType => 1;
		public override Vector2 MapPosition => new Vector2(46.709f, 402.291f);
		public override int Radius => 72;

		public override HashSet<uint> NodeIds => new()
		{
			30471,
			30472,
			30473,
			30474,
		};

		public override HashSet<uint> ItemIds => new()
		{
			6,
			5523,
			5528,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
