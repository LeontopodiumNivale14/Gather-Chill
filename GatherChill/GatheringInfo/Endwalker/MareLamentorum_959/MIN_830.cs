using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Endwalker.MareLamentorum_959
{
	public class MIN_830 : RouteInfo
	{
		public override uint Id => 830;
		public override uint ExpansionId => 4;
		public override uint ZoneId => 959;
		public override uint GatherType => 0;
		public override Vector2 MapPosition => new Vector2(-256.013f, 557.043f);
		public override int Radius => 9;

		public override HashSet<uint> NodeIds => new()
		{
			33963,
		};

		public override HashSet<uint> ItemIds => new()
		{
			36294,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
