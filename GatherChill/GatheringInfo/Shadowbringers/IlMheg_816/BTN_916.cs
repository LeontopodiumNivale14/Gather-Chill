using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.IlMheg_816
{
	public class BTN_916 : RouteInfo
	{
		public override uint Id => 916;
		public override uint ExpansionId => 3;
		public override uint ZoneId => 816;
		public override uint GatherType => 5;
		public override Vector2 MapPosition => new Vector2(-297.802f, -230.836f);
		public override int Radius => 75;

		public override HashSet<uint> NodeIds => new()
		{
			34397,
			34398,
			34399,
		};

		public override HashSet<uint> ItemIds => new()
		{
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
