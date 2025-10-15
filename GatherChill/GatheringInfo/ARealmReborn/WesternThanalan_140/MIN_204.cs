using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.WesternThanalan_140
{
	public class MIN_204 : RouteInfo
	{
		public override uint Id => 204;
		public override uint ExpansionId => 0;
		public override uint ZoneId => 140;
		public override uint GatherType => 1;
		public override Vector2 MapPosition => new Vector2(102.67f, 87.5011f);
		public override int Radius => 54;

		public override HashSet<uint> NodeIds => new()
		{
			30445,
			30446,
			30447,
			30448,
		};

		public override HashSet<uint> ItemIds => new()
		{
			7,
			13,
			5814,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
