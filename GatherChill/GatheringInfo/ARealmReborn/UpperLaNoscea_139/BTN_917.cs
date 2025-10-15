using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.UpperLaNoscea_139
{
	public class BTN_917 : RouteInfo
	{
		public override uint Id => 917;
		public override uint ExpansionId => 0;
		public override uint ZoneId => 139;
		public override uint GatherType => 5;
		public override Vector2 MapPosition => new Vector2(149.252f, -15.3156f);
		public override int Radius => 103;

		public override HashSet<uint> NodeIds => new()
		{
			34400,
			34401,
			34402,
		};

		public override HashSet<uint> ItemIds => new()
		{
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
