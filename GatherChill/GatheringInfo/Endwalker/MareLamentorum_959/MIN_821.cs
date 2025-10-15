using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Endwalker.MareLamentorum_959
{
	public class MIN_821 : RouteInfo
	{
		public override uint Id => 821;
		public override uint ExpansionId => 4;
		public override uint ZoneId => 959;
		public override uint GatherType => 0;
		public override Vector2 MapPosition => new Vector2(396.739f, 720.062f);
		public override int Radius => 106;

		public override HashSet<uint> NodeIds => new()
		{
			33924,
			33925,
			33926,
			33927,
			33928,
			33929,
		};

		public override HashSet<uint> ItemIds => new()
		{
			11,
			35602,
			36164,
			41071,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
