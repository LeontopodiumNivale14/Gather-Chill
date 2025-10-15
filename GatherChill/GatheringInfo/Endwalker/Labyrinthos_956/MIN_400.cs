using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Endwalker.Labyrinthos_956
{
	public class MIN_400 : RouteInfo
	{
		public override uint Id => 400;
		public override uint ExpansionId => 4;
		public override uint ZoneId => 956;
		public override uint GatherType => 1;
		public override Vector2 MapPosition => new Vector2(-516.725f, -4.43639f);
		public override int Radius => 12;

		public override HashSet<uint> NodeIds => new()
		{
			34360,
		};

		public override HashSet<uint> ItemIds => new()
		{
			37822,
			39909,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
