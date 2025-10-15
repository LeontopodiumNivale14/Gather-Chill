using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.TheRaktikaGreatwood_817
{
	public class MIN_781 : RouteInfo
	{
		public override uint Id => 781;
		public override uint ExpansionId => 3;
		public override uint ZoneId => 817;
		public override uint GatherType => 1;
		public override Vector2 MapPosition => new Vector2(-303.857f, 455.647f);
		public override int Radius => 107;

		public override HashSet<uint> NodeIds => new()
		{
			33628,
			33629,
			33630,
		};

		public override HashSet<uint> ItemIds => new()
		{
			32991,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
