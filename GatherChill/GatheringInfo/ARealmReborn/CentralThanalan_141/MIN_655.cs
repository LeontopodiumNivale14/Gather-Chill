using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.CentralThanalan_141
{
	public class MIN_655 : RouteInfo
	{
		public override uint Id => 655;
		public override uint ExpansionId => 0;
		public override uint ZoneId => 141;
		public override uint GatherType => 1;
		public override Vector2 MapPosition => new Vector2(24.5109f, -340.112f);
		public override int Radius => 35;

		public override HashSet<uint> NodeIds => new()
		{
			32847,
			32848,
			32849,
			32850,
		};

		public override HashSet<uint> ItemIds => new()
		{
			28767,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
