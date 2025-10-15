using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.TheTempest_818
{
	public class MIN_939 : RouteInfo
	{
		public override uint Id => 939;
		public override uint ExpansionId => 3;
		public override uint ZoneId => 818;
		public override uint GatherType => 0;
		public override Vector2 MapPosition => new Vector2(647.735f, 46.9015f);
		public override int Radius => 151;

		public override HashSet<uint> NodeIds => new()
		{
			34481,
			34482,
			34483,
		};

		public override HashSet<uint> ItemIds => new()
		{
			41290,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
