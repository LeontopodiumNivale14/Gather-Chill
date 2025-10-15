using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.EasternLaNoscea_137
{
	public class BTN_198 : RouteInfo
	{
		public override uint Id => 198;
		public override uint ExpansionId => 0;
		public override uint ZoneId => 137;
		public override uint GatherType => 2;
		public override Vector2 MapPosition => new Vector2(-213.639f, 538.524f);
		public override int Radius => 72;

		public override HashSet<uint> NodeIds => new()
		{
			30381,
			30382,
			30383,
			30384,
		};

		public override HashSet<uint> ItemIds => new()
		{
			7,
			13,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
