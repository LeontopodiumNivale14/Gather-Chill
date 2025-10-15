using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheDravanianForelands_398
{
	public class BTN_358 : RouteInfo
	{
		public override uint Id => 358;
		public override uint ExpansionId => 1;
		public override uint ZoneId => 398;
		public override uint GatherType => 3;
		public override Vector2 MapPosition => new Vector2(-610.036f, 527.973f);
		public override int Radius => 147;

		public override HashSet<uint> NodeIds => new()
		{
			31412,
			31413,
			31414,
			31415,
			31416,
			31417,
		};

		public override HashSet<uint> ItemIds => new()
		{
			8,
			12638,
			13762,
			17561,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
