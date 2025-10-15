using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.UpperLaNoscea_139
{
	public class MIN_186 : RouteInfo
	{
		public override uint Id => 186;
		public override uint ExpansionId => 0;
		public override uint ZoneId => 139;
		public override uint GatherType => 0;
		public override Vector2 MapPosition => new Vector2(439.039f, 190.804f);
		public override int Radius => 52;

		public override HashSet<uint> NodeIds => new()
		{
			30524,
			30525,
			30526,
			30527,
		};

		public override HashSet<uint> ItemIds => new()
		{
			5,
			5115,
			5145,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
