using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.TheAzimSteppe_622
{
	public class BTN_475 : RouteInfo
	{
		public override uint Id => 475;
		public override uint ExpansionId => 2;
		public override uint ZoneId => 622;
		public override uint GatherType => 5;
		public override Vector2 MapPosition => new Vector2(-130.312f, 112.76f);
		public override int Radius => 92;

		public override HashSet<uint> NodeIds => new()
		{
			32104,
			32105,
			32106,
		};

		public override HashSet<uint> ItemIds => new()
		{
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
