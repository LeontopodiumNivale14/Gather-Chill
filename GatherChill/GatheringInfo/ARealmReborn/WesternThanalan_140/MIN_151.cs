using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.WesternThanalan_140
{
	public class MIN_151 : RouteInfo
	{
		public override uint Id => 151;
		public override uint ExpansionId => 0;
		public override uint ZoneId => 140;
		public override uint GatherType => 1;
		public override Vector2 MapPosition => new Vector2(-157.399f, 362.79f);
		public override int Radius => 64;

		public override HashSet<uint> NodeIds => new()
		{
			30401,
			30402,
			30403,
			30404,
		};

		public override HashSet<uint> ItemIds => new()
		{
			7,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
