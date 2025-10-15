using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheChurningMists_400
{
	public class BTN_662 : RouteInfo
	{
		public override uint Id => 662;
		public override uint ExpansionId => 1;
		public override uint ZoneId => 400;
		public override uint GatherType => 2;
		public override Vector2 MapPosition => new Vector2(-559.919f, -186.104f);
		public override int Radius => 104;

		public override HashSet<uint> NodeIds => new()
		{
			32875,
			32876,
			32877,
			32878,
			32879,
			32880,
		};

		public override HashSet<uint> ItemIds => new()
		{
			28775,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
