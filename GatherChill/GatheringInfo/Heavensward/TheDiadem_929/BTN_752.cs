using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheDiadem_929
{
	public class BTN_752 : RouteInfo
	{
		public override uint Id => 752;
		public override uint ExpansionId => 1;
		public override uint ZoneId => 929;
		public override uint GatherType => 2;
		public override Vector2 MapPosition => new Vector2(127.786f, -13.3054f);
		public override int Radius => 805;

		public override HashSet<uint> NodeIds => new()
		{
			33492,
			33499,
			33506,
			33511,
			33513,
			33518,
			33520,
			33525,
			33532,
			33536,
			33541,
			33548,
			33555,
			33562,
			33567,
			33569,
			33574,
			33576,
			33581,
		};

		public override HashSet<uint> ItemIds => new()
		{
			29935,
			31280,
			31287,
			31296,
			31307,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
