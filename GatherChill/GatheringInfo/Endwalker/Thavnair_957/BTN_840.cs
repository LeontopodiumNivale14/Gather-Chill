using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Endwalker.Thavnair_957
{
	public class BTN_840 : RouteInfo
	{
		public override uint Id => 840;
		public override uint ExpansionId => 4;
		public override uint ZoneId => 957;
		public override uint GatherType => 3;
		public override Vector2 MapPosition => new Vector2(19.3498f, -592.691f);
		public override int Radius => 139;

		public override HashSet<uint> NodeIds => new()
		{
			33988,
			33989,
			33990,
			33991,
			33992,
			33993,
		};

		public override HashSet<uint> ItemIds => new()
		{
			8,
			35600,
			36085,
			36098,
			36670,
			41069,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
