using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheDiadem_901
{
	public class MIN_803 : RouteInfo
	{
		public override uint Id => 803;
		public override uint ExpansionId => 1;
		public override uint ZoneId => 901;
		public override uint GatherType => 0;
		public override Vector2 MapPosition => new Vector2(309.314f, -258.448f);
		public override int Radius => 98;

		public override HashSet<uint> NodeIds => new()
		{
			33843,
			33844,
			33845,
		};

		public override HashSet<uint> ItemIds => new()
		{
			31769,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
