using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.CentralThanalan_141
{
	public class MIN_194 : RouteInfo
	{
		public override uint Id => 194;
		public override uint ExpansionId => 0;
		public override uint ZoneId => 141;
		public override uint GatherType => 1;
		public override Vector2 MapPosition => new Vector2(-345.022f, 55.4201f);
		public override int Radius => 77;

		public override HashSet<uint> NodeIds => new()
		{
			30556,
			30557,
			30558,
			30559,
		};

		public override HashSet<uint> ItemIds => new()
		{
			6,
			5228,
			5267,
			5518,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
