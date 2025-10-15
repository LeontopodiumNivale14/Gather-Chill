using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Endwalker.UltimaThule_960
{
	public class BTN_855 : RouteInfo
	{
		public override uint Id => 855;
		public override uint ExpansionId => 4;
		public override uint ZoneId => 960;
		public override uint GatherType => 3;
		public override Vector2 MapPosition => new Vector2(-600.988f, 586.433f);
		public override int Radius => 11;

		public override HashSet<uint> NodeIds => new()
		{
			34042,
		};

		public override HashSet<uint> ItemIds => new()
		{
			36310,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
