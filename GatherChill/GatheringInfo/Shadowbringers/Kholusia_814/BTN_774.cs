using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.Kholusia_814
{
	public class BTN_774 : RouteInfo
	{
		public override uint Id => 774;
		public override uint ExpansionId => 3;
		public override uint ZoneId => 814;
		public override uint GatherType => 2;
		public override Vector2 MapPosition => new Vector2(322.27f, 563.528f);
		public override int Radius => 1;

		public override HashSet<uint> NodeIds => new()
		{
			33025,
		};

		public override HashSet<uint> ItemIds => new()
		{
			33003,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
