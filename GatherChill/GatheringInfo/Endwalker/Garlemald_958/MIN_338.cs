using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Endwalker.Garlemald_958
{
	public class MIN_338 : RouteInfo
	{
		public override uint Id => 338;
		public override uint ExpansionId => 4;
		public override uint ZoneId => 958;
		public override uint GatherType => 1;
		public override Vector2 MapPosition => new Vector2(522.347f, -207.058f);
		public override int Radius => 58;

		public override HashSet<uint> NodeIds => new()
		{
			34358,
		};

		public override HashSet<uint> ItemIds => new()
		{
			37817,
			38934,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
