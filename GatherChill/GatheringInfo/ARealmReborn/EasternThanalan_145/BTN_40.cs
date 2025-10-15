using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.EasternThanalan_145
{
	public class BTN_40 : RouteInfo
	{
		public override uint Id => 40;
		public override uint ExpansionId => 0;
		public override uint ZoneId => 145;
		public override uint GatherType => 3;
		public override Vector2 MapPosition => new Vector2(-334.188f, -36.0263f);
		public override int Radius => 51;

		public override HashSet<uint> NodeIds => new()
		{
			30119,
			30144,
			30148,
			30149,
		};

		public override HashSet<uint> ItemIds => new()
		{
			2,
			4797,
			4821,
			4835,
			4836,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
