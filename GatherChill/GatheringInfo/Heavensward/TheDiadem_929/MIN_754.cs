using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheDiadem_929
{
	public class MIN_754 : RouteInfo
	{
		public override uint Id => 754;
		public override uint ExpansionId => 1;
		public override uint ZoneId => 929;
		public override uint GatherType => 0;
		public override Vector2 MapPosition => new Vector2(-428.741f, -593.716f);
		public override int Radius => 12;

		public override HashSet<uint> NodeIds => new()
		{
			33584,
		};

		public override HashSet<uint> ItemIds => new()
		{
			29946,
			31318,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
