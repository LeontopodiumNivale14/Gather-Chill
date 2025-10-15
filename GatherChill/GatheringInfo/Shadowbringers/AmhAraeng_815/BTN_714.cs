using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.AmhAraeng_815
{
	public class BTN_714 : RouteInfo
	{
		public override uint Id => 714;
		public override uint ExpansionId => 3;
		public override uint ZoneId => 815;
		public override uint GatherType => 3;
		public override Vector2 MapPosition => new Vector2(537.489f, 602.105f);
		public override int Radius => 1;

		public override HashSet<uint> NodeIds => new()
		{
			33234,
		};

		public override HashSet<uint> ItemIds => new()
		{
			29974,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
