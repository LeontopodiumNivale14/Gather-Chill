using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.EasternThanalan_145
{
	public class MIN_226 : RouteInfo
	{
		public override uint Id => 226;
		public override uint ExpansionId => 0;
		public override uint ZoneId => 145;
		public override uint GatherType => 1;
		public override Vector2 MapPosition => new Vector2(203.933f, 87.1765f);
		public override int Radius => 86;

		public override HashSet<uint> NodeIds => new()
		{
			31014,
		};

		public override HashSet<uint> ItemIds => new()
		{
			5273,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
