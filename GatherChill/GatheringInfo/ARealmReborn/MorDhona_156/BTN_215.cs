using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.MorDhona_156
{
	public class BTN_215 : RouteInfo
	{
		public override uint Id => 215;
		public override uint ExpansionId => 0;
		public override uint ZoneId => 156;
		public override uint GatherType => 2;
		public override Vector2 MapPosition => new Vector2(529.757f, -397.057f);
		public override int Radius => 96;

		public override HashSet<uint> NodeIds => new()
		{
			31003,
		};

		public override HashSet<uint> ItemIds => new()
		{
			14,
			15,
			16,
			17,
			18,
			19,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
