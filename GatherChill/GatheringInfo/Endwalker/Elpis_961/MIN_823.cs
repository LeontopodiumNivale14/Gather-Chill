using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Endwalker.Elpis_961
{
	public class MIN_823 : RouteInfo
	{
		public override uint Id => 823;
		public override uint ExpansionId => 4;
		public override uint ZoneId => 961;
		public override uint GatherType => 0;
		public override Vector2 MapPosition => new Vector2(157.608f, -582.728f);
		public override int Radius => 145;

		public override HashSet<uint> NodeIds => new()
		{
			33936,
			33937,
			33938,
			33939,
			33940,
			33941,
		};

		public override HashSet<uint> ItemIds => new()
		{
			8,
			35603,
			36165,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
