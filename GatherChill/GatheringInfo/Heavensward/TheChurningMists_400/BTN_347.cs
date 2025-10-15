using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheChurningMists_400
{
	public class BTN_347 : RouteInfo
	{
		public override uint Id => 347;
		public override uint ExpansionId => 1;
		public override uint ZoneId => 400;
		public override uint GatherType => 3;
		public override Vector2 MapPosition => new Vector2(-246.559f, 719.794f);
		public override int Radius => 46;

		public override HashSet<uint> NodeIds => new()
		{
			31500,
		};

		public override HashSet<uint> ItemIds => new()
		{
			12634,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
