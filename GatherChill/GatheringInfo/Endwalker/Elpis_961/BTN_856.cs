using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Endwalker.Elpis_961
{
	public class BTN_856 : RouteInfo
	{
		public override uint Id => 856;
		public override uint ExpansionId => 4;
		public override uint ZoneId => 961;
		public override uint GatherType => 2;
		public override Vector2 MapPosition => new Vector2(583.402f, -336.205f);
		public override int Radius => 10;

		public override HashSet<uint> NodeIds => new()
		{
			34043,
		};

		public override HashSet<uint> ItemIds => new()
		{
			36195,
			36214,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
