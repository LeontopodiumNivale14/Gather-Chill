using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Dawntrail.HeritageFound_1191
{
	public class BTN_991 : RouteInfo
	{
		public override uint Id => 991;
		public override uint ExpansionId => 5;
		public override uint ZoneId => 1191;
		public override uint GatherType => 2;
		public override Vector2 MapPosition => new Vector2(551.289f, 317.905f);
		public override int Radius => 138;

		public override HashSet<uint> NodeIds => new()
		{
			34851,
			34852,
			34853,
			34854,
			34855,
			34856,
		};

		public override HashSet<uint> ItemIds => new()
		{
			9,
			44018,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
