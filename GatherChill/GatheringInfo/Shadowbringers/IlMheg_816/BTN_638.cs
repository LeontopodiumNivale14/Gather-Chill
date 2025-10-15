using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.IlMheg_816
{
	public class BTN_638 : RouteInfo
	{
		public override uint Id => 638;
		public override uint ExpansionId => 3;
		public override uint ZoneId => 816;
		public override uint GatherType => 5;
		public override Vector2 MapPosition => new Vector2(-70.9519f, -414.264f);
		public override int Radius => 102;

		public override HashSet<uint> NodeIds => new()
		{
			32783,
			32784,
			32785,
		};

		public override HashSet<uint> ItemIds => new()
		{
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
