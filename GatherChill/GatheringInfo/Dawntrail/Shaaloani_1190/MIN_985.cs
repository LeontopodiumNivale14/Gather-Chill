using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Dawntrail.Shaaloani_1190
{
	public class MIN_985 : RouteInfo
	{
		public override uint Id => 985;
		public override uint ExpansionId => 5;
		public override uint ZoneId => 1190;
		public override uint GatherType => 1;
		public override Vector2 MapPosition => new Vector2(-209.063f, 535.766f);
		public override int Radius => 133;

		public override HashSet<uint> NodeIds => new()
		{
			34815,
			34816,
			34817,
			34818,
			34819,
			34820,
		};

		public override HashSet<uint> ItemIds => new()
		{
			8,
			44007,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
