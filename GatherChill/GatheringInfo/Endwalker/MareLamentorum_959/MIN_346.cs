using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Endwalker.MareLamentorum_959
{
	public class MIN_346 : RouteInfo
	{
		public override uint Id => 346;
		public override uint ExpansionId => 4;
		public override uint ZoneId => 959;
		public override uint GatherType => 0;
		public override Vector2 MapPosition => new Vector2(406.414f, 44.6484f);
		public override int Radius => 72;

		public override HashSet<uint> NodeIds => new()
		{
			34359,
		};

		public override HashSet<uint> ItemIds => new()
		{
			37820,
			39237,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
