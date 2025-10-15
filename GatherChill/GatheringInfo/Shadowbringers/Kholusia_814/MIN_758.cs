using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.Kholusia_814
{
	public class MIN_758 : RouteInfo
	{
		public override uint Id => 758;
		public override uint ExpansionId => 3;
		public override uint ZoneId => 814;
		public override uint GatherType => 0;
		public override Vector2 MapPosition => new Vector2(733.766f, -459.707f);
		public override int Radius => 1;

		public override HashSet<uint> NodeIds => new()
		{
			33588,
		};

		public override HashSet<uint> ItemIds => new()
		{
			32955,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
