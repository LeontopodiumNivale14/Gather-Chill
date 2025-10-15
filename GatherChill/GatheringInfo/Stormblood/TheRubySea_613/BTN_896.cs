using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.TheRubySea_613
{
	public class BTN_896 : RouteInfo
	{
		public override uint Id => 896;
		public override uint ExpansionId => 2;
		public override uint ZoneId => 613;
		public override uint GatherType => 5;
		public override Vector2 MapPosition => new Vector2(795.898f, -463.529f);
		public override int Radius => 108;

		public override HashSet<uint> NodeIds => new()
		{
			34323,
			34324,
			34325,
		};

		public override HashSet<uint> ItemIds => new()
		{
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
