using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.TheRubySea_613
{
	public class BTN_468 : RouteInfo
	{
		public override uint Id => 468;
		public override uint ExpansionId => 2;
		public override uint ZoneId => 613;
		public override uint GatherType => 5;
		public override Vector2 MapPosition => new Vector2(-855.371f, 765.994f);
		public override int Radius => 95;

		public override HashSet<uint> NodeIds => new()
		{
			32083,
			32084,
			32085,
		};

		public override HashSet<uint> ItemIds => new()
		{
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
