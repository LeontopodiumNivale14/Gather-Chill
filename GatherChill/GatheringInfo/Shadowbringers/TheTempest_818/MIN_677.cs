using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.TheTempest_818
{
	public class MIN_677 : RouteInfo
	{
		public override uint Id => 677;
		public override uint ExpansionId => 3;
		public override uint ZoneId => 818;
		public override uint GatherType => 0;
		public override Vector2 MapPosition => new Vector2(-71.9389f, -329.161f);
		public override int Radius => 121;

		public override HashSet<uint> NodeIds => new()
		{
			32965,
			32966,
			32967,
			32968,
			32969,
			32970,
		};

		public override HashSet<uint> ItemIds => new()
		{
			28791,
			28910,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
