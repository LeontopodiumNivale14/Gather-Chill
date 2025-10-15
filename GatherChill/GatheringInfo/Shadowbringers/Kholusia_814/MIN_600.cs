using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.Kholusia_814
{
	public class MIN_600 : RouteInfo
	{
		public override uint Id => 600;
		public override uint ExpansionId => 3;
		public override uint ZoneId => 814;
		public override uint GatherType => 1;
		public override Vector2 MapPosition => new Vector2(63.8097f, -157.867f);
		public override int Radius => 120;

		public override HashSet<uint> NodeIds => new()
		{
			32664,
			32665,
			32666,
			32667,
			32668,
			32669,
		};

		public override HashSet<uint> ItemIds => new()
		{
			8,
			26757,
			27803,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
