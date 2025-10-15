using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.SouthShroud_153
{
	public class BTN_27 : RouteInfo
	{
		public override uint Id => 27;
		public override uint ExpansionId => 0;
		public override uint ZoneId => 153;
		public override uint GatherType => 2;
		public override Vector2 MapPosition => new Vector2(239.939f, -121.078f);
		public override int Radius => 71;

		public override HashSet<uint> NodeIds => new()
		{
			30057,
			30058,
			30059,
			30317,
		};

		public override HashSet<uint> ItemIds => new()
		{
			3,
			4813,
			5359,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
