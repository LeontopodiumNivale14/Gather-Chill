using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.EasternLaNoscea_137
{
	public class BTN_214 : RouteInfo
	{
		public override uint Id => 214;
		public override uint ExpansionId => 0;
		public override uint ZoneId => 137;
		public override uint GatherType => 2;
		public override Vector2 MapPosition => new Vector2(459.65f, 275.427f);
		public override int Radius => 73;

		public override HashSet<uint> NodeIds => new()
		{
			31002,
		};

		public override HashSet<uint> ItemIds => new()
		{
			4816,
			7734,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
