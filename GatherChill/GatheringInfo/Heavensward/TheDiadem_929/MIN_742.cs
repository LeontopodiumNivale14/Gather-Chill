using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheDiadem_929
{
	public class MIN_742 : RouteInfo
	{
		public override uint Id => 742;
		public override uint ExpansionId => 1;
		public override uint ZoneId => 929;
		public override uint GatherType => 0;
		public override Vector2 MapPosition => new Vector2(46.6485f, -138.927f);
		public override int Radius => 947;

		public override HashSet<uint> NodeIds => new()
		{
			33392,
			33397,
			33404,
			33411,
			33418,
			33423,
			33425,
			33430,
			33432,
			33437,
			33444,
			33451,
			33458,
			33463,
			33465,
			33470,
			33472,
			33477,
			33484,
			33487,
		};

		public override HashSet<uint> ItemIds => new()
		{
			29939,
			31278,
			31291,
			31301,
			31311,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
