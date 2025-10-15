using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.IlMheg_816
{
	public class BTN_628 : RouteInfo
	{
		public override uint Id => 628;
		public override uint ExpansionId => 3;
		public override uint ZoneId => 816;
		public override uint GatherType => 3;
		public override Vector2 MapPosition => new Vector2(168.571f, 766.002f);
		public override int Radius => 34;

		public override HashSet<uint> NodeIds => new()
		{
			32767,
		};

		public override HashSet<uint> ItemIds => new()
		{
			27833,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
