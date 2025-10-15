using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheDiadem_929
{
	public class BTN_751 : RouteInfo
	{
		public override uint Id => 751;
		public override uint ExpansionId => 1;
		public override uint ZoneId => 929;
		public override uint GatherType => 2;
		public override Vector2 MapPosition => new Vector2(87.1182f, -191.344f);
		public override int Radius => 864;

		public override HashSet<uint> NodeIds => new()
		{
			33491,
			33498,
			33503,
			33505,
			33510,
			33512,
			33517,
			33524,
			33531,
			33537,
			33542,
			33544,
			33549,
			33556,
			33563,
			33570,
			33575,
			33577,
			33582,
		};

		public override HashSet<uint> ItemIds => new()
		{
			29934,
			31276,
			31286,
			31297,
			31306,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
