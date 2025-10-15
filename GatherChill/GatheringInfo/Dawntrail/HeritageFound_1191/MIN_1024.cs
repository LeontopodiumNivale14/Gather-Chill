using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Dawntrail.HeritageFound_1191
{
	public class MIN_1024 : RouteInfo
	{
		public override uint Id => 1024;
		public override uint ExpansionId => 5;
		public override uint ZoneId => 1191;
		public override uint GatherType => 0;
		public override Vector2 MapPosition => new Vector2(653.131f, -661.373f);
		public override int Radius => 70;

		public override HashSet<uint> NodeIds => new()
		{
			34984,
		};

		public override HashSet<uint> ItemIds => new()
		{
			43922,
			44233,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
