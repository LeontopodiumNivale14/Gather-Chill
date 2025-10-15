using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Endwalker.Garlemald_958
{
	public class MIN_834 : RouteInfo
	{
		public override uint Id => 834;
		public override uint ExpansionId => 4;
		public override uint ZoneId => 958;
		public override uint GatherType => 0;
		public override Vector2 MapPosition => new Vector2(533.008f, 668.837f);
		public override int Radius => 9;

		public override HashSet<uint> NodeIds => new()
		{
			33967,
		};

		public override HashSet<uint> ItemIds => new()
		{
			36215,
			36216,
			37279,
			37694,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
