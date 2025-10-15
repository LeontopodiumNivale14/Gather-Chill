using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheDiadem_939
{
	public class BTN_794 : RouteInfo
	{
		public override uint Id => 794;
		public override uint ExpansionId => 1;
		public override uint ZoneId => 939;
		public override uint GatherType => 3;
		public override Vector2 MapPosition => new Vector2(79.7511f, 56.5883f);
		public override int Radius => 833;

		public override HashSet<uint> NodeIds => new()
		{
			33742,
			33747,
			33749,
			33754,
			33756,
			33761,
			33768,
			33775,
			33782,
			33786,
			33790,
			33795,
			33797,
			33802,
			33804,
			33809,
			33816,
			33823,
			33835,
		};

		public override HashSet<uint> ItemIds => new()
		{
			29938,
			31310,
			32011,
			32019,
			32028,
			32039,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
