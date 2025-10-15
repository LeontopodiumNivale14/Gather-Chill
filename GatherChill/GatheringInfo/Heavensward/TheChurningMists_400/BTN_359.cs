using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheChurningMists_400
{
	public class BTN_359 : RouteInfo
	{
		public override uint Id => 359;
		public override uint ExpansionId => 1;
		public override uint ZoneId => 400;
		public override uint GatherType => 3;
		public override Vector2 MapPosition => new Vector2(-99.775f, 361.995f);
		public override int Radius => 152;

		public override HashSet<uint> NodeIds => new()
		{
			31418,
			31419,
			31420,
			31421,
			31422,
			31423,
		};

		public override HashSet<uint> ItemIds => new()
		{
			11,
			12640,
			13762,
			17559,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
