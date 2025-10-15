using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Dawntrail.Urqopacha_1187
{
	public class MIN_975 : RouteInfo
	{
		public override uint Id => 975;
		public override uint ExpansionId => 5;
		public override uint ZoneId => 1187;
		public override uint GatherType => 0;
		public override Vector2 MapPosition => new Vector2(-196.66f, 293.789f);
		public override int Radius => 183;

		public override HashSet<uint> NodeIds => new()
		{
			34755,
			34756,
			34757,
			34758,
			34759,
			34760,
		};

		public override HashSet<uint> ItemIds => new()
		{
			10,
			43913,
			44002,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
