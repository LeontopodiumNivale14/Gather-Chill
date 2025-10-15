using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheDiadem_901
{
	public class BTN_706 : RouteInfo
	{
		public override uint Id => 706;
		public override uint ExpansionId => 1;
		public override uint ZoneId => 901;
		public override uint GatherType => 2;
		public override Vector2 MapPosition => new Vector2(102.59f, -83.9511f);
		public override int Radius => 899;

		public override HashSet<uint> NodeIds => new()
		{
			33136,
			33143,
			33150,
			33156,
			33162,
			33169,
			33177,
			33186,
			33193,
			33201,
			33212,
			33219,
			33222,
		};

		public override HashSet<uint> ItemIds => new()
		{
			29894,
			29904,
			29914,
			29924,
			29934,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
