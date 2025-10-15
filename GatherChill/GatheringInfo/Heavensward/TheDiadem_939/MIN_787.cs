using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheDiadem_939
{
	public class MIN_787 : RouteInfo
	{
		public override uint Id => 787;
		public override uint ExpansionId => 1;
		public override uint ZoneId => 939;
		public override uint GatherType => 0;
		public override Vector2 MapPosition => new Vector2(143.35f, -143.849f);
		public override int Radius => 821;

		public override HashSet<uint> NodeIds => new()
		{
			33645,
			33650,
			33652,
			33657,
			33664,
			33671,
			33678,
			33683,
			33685,
			33695,
			33702,
			33707,
			33709,
			33714,
			33716,
			33721,
			33728,
			33735,
		};

		public override HashSet<uint> ItemIds => new()
		{
			29940,
			31312,
			32012,
			32021,
			32031,
			32041,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
