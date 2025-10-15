using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Endwalker.Garlemald_958
{
	public class MIN_831 : RouteInfo
	{
		public override uint Id => 831;
		public override uint ExpansionId => 4;
		public override uint ZoneId => 958;
		public override uint GatherType => 1;
		public override Vector2 MapPosition => new Vector2(-434.787f, 20.2251f);
		public override int Radius => 8;

		public override HashSet<uint> NodeIds => new()
		{
			33964,
		};

		public override HashSet<uint> ItemIds => new()
		{
			36296,
			36298,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
