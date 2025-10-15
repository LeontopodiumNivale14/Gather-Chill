using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.LowerLaNoscea_135
{
	public class MIN_187 : RouteInfo
	{
		public override uint Id => 187;
		public override uint ExpansionId => 0;
		public override uint ZoneId => 135;
		public override uint GatherType => 0;
		public override Vector2 MapPosition => new Vector2(312.814f, -144.461f);
		public override int Radius => 67;

		public override HashSet<uint> NodeIds => new()
		{
			30528,
			30529,
			30530,
			30531,
		};

		public override HashSet<uint> ItemIds => new()
		{
			5,
			5128,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
