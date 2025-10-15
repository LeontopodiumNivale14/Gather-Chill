using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.Kholusia_814
{
	public class MIN_603 : RouteInfo
	{
		public override uint Id => 603;
		public override uint ExpansionId => 3;
		public override uint ZoneId => 814;
		public override uint GatherType => 0;
		public override Vector2 MapPosition => new Vector2(623.026f, 86.2274f);
		public override int Radius => 25;

		public override HashSet<uint> NodeIds => new()
		{
			32677,
		};

		public override HashSet<uint> ItemIds => new()
		{
			27726,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
