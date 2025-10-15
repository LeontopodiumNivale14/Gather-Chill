using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.EasternThanalan_145
{
	public class MIN_185 : RouteInfo
	{
		public override uint Id => 185;
		public override uint ExpansionId => 0;
		public override uint ZoneId => 145;
		public override uint GatherType => 0;
		public override Vector2 MapPosition => new Vector2(-442.02f, -119.64f);
		public override int Radius => 70;

		public override HashSet<uint> NodeIds => new()
		{
			30520,
			30521,
			30522,
			30523,
		};

		public override HashSet<uint> ItemIds => new()
		{
			2,
			5140,
			5143,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
