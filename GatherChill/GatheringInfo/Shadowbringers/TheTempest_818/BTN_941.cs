using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.TheTempest_818
{
	public class BTN_941 : RouteInfo
	{
		public override uint Id => 941;
		public override uint ExpansionId => 3;
		public override uint ZoneId => 818;
		public override uint GatherType => 2;
		public override Vector2 MapPosition => new Vector2(801.55f, -401.141f);
		public override int Radius => 122;

		public override HashSet<uint> NodeIds => new()
		{
			34487,
			34488,
			34489,
		};

		public override HashSet<uint> ItemIds => new()
		{
			41292,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
