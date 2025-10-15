using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Dawntrail.Shaaloani_1190
{
	public class MIN_1031 : RouteInfo
	{
		public override uint Id => 1031;
		public override uint ExpansionId => 5;
		public override uint ZoneId => 1190;
		public override uint GatherType => 0;
		public override Vector2 MapPosition => new Vector2(758.965f, 327.227f);
		public override int Radius => 38;

		public override HashSet<uint> NodeIds => new()
		{
			34991,
		};

		public override HashSet<uint> ItemIds => new()
		{
			44136,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
