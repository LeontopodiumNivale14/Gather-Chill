using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.Lakeland_813
{
	public class MIN_330 : RouteInfo
	{
		public override uint Id => 330;
		public override uint ExpansionId => 3;
		public override uint ZoneId => 813;
		public override uint GatherType => 0;
		public override Vector2 MapPosition => new Vector2(487.039f, 132.498f);
		public override int Radius => 1;

		public override HashSet<uint> NodeIds => new()
		{
			31443,
		};

		public override HashSet<uint> ItemIds => new()
		{
			32980,
			32982,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
