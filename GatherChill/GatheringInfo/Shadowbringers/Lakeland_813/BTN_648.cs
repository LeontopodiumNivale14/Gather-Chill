using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.Lakeland_813
{
	public class BTN_648 : RouteInfo
	{
		public override uint Id => 648;
		public override uint ExpansionId => 3;
		public override uint ZoneId => 813;
		public override uint GatherType => 3;
		public override Vector2 MapPosition => new Vector2(201.547f, 419.744f);
		public override int Radius => 99;

		public override HashSet<uint> NodeIds => new()
		{
			32807,
			32808,
			32809,
			32810,
			32811,
			32812,
		};

		public override HashSet<uint> ItemIds => new()
		{
			11,
			27783,
			27784,
			27785,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
