using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.Kholusia_814
{
	public class MIN_601 : RouteInfo
	{
		public override uint Id => 601;
		public override uint ExpansionId => 3;
		public override uint ZoneId => 814;
		public override uint GatherType => 1;
		public override Vector2 MapPosition => new Vector2(575.474f, -148.006f);
		public override int Radius => 136;

		public override HashSet<uint> NodeIds => new()
		{
			32670,
			33613,
			33614,
		};

		public override HashSet<uint> ItemIds => new()
		{
			8,
			27807,
			30591,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
