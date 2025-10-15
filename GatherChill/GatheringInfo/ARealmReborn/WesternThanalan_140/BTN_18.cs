using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.WesternThanalan_140
{
	public class BTN_18 : RouteInfo
	{
		public override uint Id => 18;
		public override uint ExpansionId => 0;
		public override uint ZoneId => 140;
		public override uint GatherType => 3;
		public override Vector2 MapPosition => new Vector2(92.6846f, 94.7202f);
		public override int Radius => 54;

		public override HashSet<uint> NodeIds => new()
		{
			30068,
			30069,
			30070,
			30071,
		};

		public override HashSet<uint> ItemIds => new()
		{
			7,
			4777,
			4779,
			4829,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
