using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.Lakeland_813
{
	public class MIN_598 : RouteInfo
	{
		public override uint Id => 598;
		public override uint ExpansionId => 3;
		public override uint ZoneId => 813;
		public override uint GatherType => 0;
		public override Vector2 MapPosition => new Vector2(729.8f, -361.432f);
		public override int Radius => 125;

		public override HashSet<uint> NodeIds => new()
		{
			32657,
			32658,
			32659,
			32660,
			32661,
			32662,
		};

		public override HashSet<uint> ItemIds => new()
		{
			12,
			27701,
			27703,
			27782,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
