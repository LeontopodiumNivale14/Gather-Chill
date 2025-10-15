using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.TheAzimSteppe_622
{
	public class MIN_495 : RouteInfo
	{
		public override uint Id => 495;
		public override uint ExpansionId => 2;
		public override uint ZoneId => 622;
		public override uint GatherType => 1;
		public override Vector2 MapPosition => new Vector2(392.151f, -312.075f);
		public override int Radius => 139;

		public override HashSet<uint> NodeIds => new()
		{
			32173,
			32174,
			32175,
			32176,
			32177,
			32178,
		};

		public override HashSet<uint> ItemIds => new()
		{
			12,
			19952,
			19955,
			19956,
			24569,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
