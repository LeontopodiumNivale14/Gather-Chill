using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheDiadem_929
{
	public class MIN_743 : RouteInfo
	{
		public override uint Id => 743;
		public override uint ExpansionId => 1;
		public override uint ZoneId => 929;
		public override uint GatherType => 0;
		public override Vector2 MapPosition => new Vector2(89.8048f, 17.7132f);
		public override int Radius => 807;

		public override HashSet<uint> NodeIds => new()
		{
			33393,
			33398,
			33400,
			33405,
			33412,
			33419,
			33426,
			33431,
			33433,
			33443,
			33450,
			33455,
			33457,
			33462,
			33464,
			33469,
			33476,
			33483,
		};

		public override HashSet<uint> ItemIds => new()
		{
			29940,
			31283,
			31292,
			31302,
			31312,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
