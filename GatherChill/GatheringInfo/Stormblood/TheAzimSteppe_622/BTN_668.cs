using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.TheAzimSteppe_622
{
	public class BTN_668 : RouteInfo
	{
		public override uint Id => 668;
		public override uint ExpansionId => 2;
		public override uint ZoneId => 622;
		public override uint GatherType => 3;
		public override Vector2 MapPosition => new Vector2(-270.808f, -293.246f);
		public override int Radius => 112;

		public override HashSet<uint> NodeIds => new()
		{
			32911,
			32912,
			32913,
			32914,
			32915,
			32916,
		};

		public override HashSet<uint> ItemIds => new()
		{
			28783,
			28789,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
