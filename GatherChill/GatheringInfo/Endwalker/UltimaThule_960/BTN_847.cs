using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Endwalker.UltimaThule_960
{
	public class BTN_847 : RouteInfo
	{
		public override uint Id => 847;
		public override uint ExpansionId => 4;
		public override uint ZoneId => 960;
		public override uint GatherType => 2;
		public override Vector2 MapPosition => new Vector2(-695.761f, 5.241f);
		public override int Radius => 113;

		public override HashSet<uint> NodeIds => new()
		{
			34024,
			34025,
			34026,
			34027,
			34028,
			34029,
		};

		public override HashSet<uint> ItemIds => new()
		{
			13,
			36194,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
