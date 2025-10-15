using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheDiadem_939
{
	public class BTN_797 : RouteInfo
	{
		public override uint Id => 797;
		public override uint ExpansionId => 1;
		public override uint ZoneId => 939;
		public override uint GatherType => 2;
		public override Vector2 MapPosition => new Vector2(-510.782f, -235.976f);
		public override int Radius => 212;

		public override HashSet<uint> NodeIds => new()
		{
			33787,
			33830,
		};

		public override HashSet<uint> ItemIds => new()
		{
			29935,
			31307,
			32009,
			32015,
			32025,
			32036,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
