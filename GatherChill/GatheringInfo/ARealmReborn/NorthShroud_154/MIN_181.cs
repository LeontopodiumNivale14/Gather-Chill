using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.NorthShroud_154
{
	public class MIN_181 : RouteInfo
	{
		public override uint Id => 181;
		public override uint ExpansionId => 0;
		public override uint ZoneId => 154;
		public override uint GatherType => 0;
		public override Vector2 MapPosition => new Vector2(198.285f, 22.912f);
		public override int Radius => 74;

		public override HashSet<uint> NodeIds => new()
		{
			31049,
			31050,
			31051,
			31052,
		};

		public override HashSet<uint> ItemIds => new()
		{
			5,
			2001391,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
