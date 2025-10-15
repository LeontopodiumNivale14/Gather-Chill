using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.WesternThanalan_140
{
	public class MIN_159 : RouteInfo
	{
		public override uint Id => 159;
		public override uint ExpansionId => 0;
		public override uint ZoneId => 140;
		public override uint GatherType => 1;
		public override Vector2 MapPosition => new Vector2(147.235f, -140.314f);
		public override int Radius => 68;

		public override HashSet<uint> NodeIds => new()
		{
			30004,
			30433,
			30434,
			30435,
			30436,
		};

		public override HashSet<uint> ItemIds => new()
		{
			7,
			5268,
			5269,
			5519,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
