using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Endwalker.Elpis_961
{
	public class BTN_846 : RouteInfo
	{
		public override uint Id => 846;
		public override uint ExpansionId => 4;
		public override uint ZoneId => 961;
		public override uint GatherType => 3;
		public override Vector2 MapPosition => new Vector2(-36.5265f, 114.458f);
		public override int Radius => 110;

		public override HashSet<uint> NodeIds => new()
		{
			34018,
			34019,
			34020,
			34021,
			34022,
			34023,
		};

		public override HashSet<uint> ItemIds => new()
		{
			12,
			35603,
			36089,
			36094,
			36205,
			41072,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
