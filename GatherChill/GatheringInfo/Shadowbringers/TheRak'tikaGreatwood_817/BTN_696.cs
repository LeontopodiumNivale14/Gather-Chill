using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.TheRaktikaGreatwood_817
{
	public class BTN_696 : RouteInfo
	{
		public override uint Id => 696;
		public override uint ExpansionId => 3;
		public override uint ZoneId => 817;
		public override uint GatherType => 5;
		public override Vector2 MapPosition => new Vector2(-745.378f, 315.113f);
		public override int Radius => 20;

		public override HashSet<uint> NodeIds => new()
		{
			33031,
			33032,
			33033,
		};

		public override HashSet<uint> ItemIds => new()
		{
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
