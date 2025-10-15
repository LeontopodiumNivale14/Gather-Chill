using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheDravanianHinterlands_399
{
	public class MIN_325 : RouteInfo
	{
		public override uint Id => 325;
		public override uint ExpansionId => 1;
		public override uint ZoneId => 399;
		public override uint GatherType => 1;
		public override Vector2 MapPosition => new Vector2(-548.394f, -329.049f);
		public override int Radius => 31;

		public override HashSet<uint> NodeIds => new()
		{
			31466,
		};

		public override HashSet<uint> ItemIds => new()
		{
			5160,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
