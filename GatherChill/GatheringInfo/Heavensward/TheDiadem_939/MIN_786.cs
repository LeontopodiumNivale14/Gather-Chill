using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheDiadem_939
{
	public class MIN_786 : RouteInfo
	{
		public override uint Id => 786;
		public override uint ExpansionId => 1;
		public override uint ZoneId => 939;
		public override uint GatherType => 0;
		public override Vector2 MapPosition => new Vector2(135.302f, 8.76122f);
		public override int Radius => 890;

		public override HashSet<uint> NodeIds => new()
		{
			33644,
			33649,
			33656,
			33663,
			33670,
			33675,
			33677,
			33682,
			33684,
			33689,
			33696,
			33703,
			33710,
			33715,
			33717,
			33722,
			33724,
			33729,
			33736,
			33739,
		};

		public override HashSet<uint> ItemIds => new()
		{
			29939,
			31311,
			32007,
			32020,
			32030,
			32040,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
