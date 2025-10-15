using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Endwalker.Labyrinthos_956
{
	public class BTN_849 : RouteInfo
	{
		public override uint Id => 849;
		public override uint ExpansionId => 4;
		public override uint ZoneId => 956;
		public override uint GatherType => 3;
		public override Vector2 MapPosition => new Vector2(-545.632f, 664.529f);
		public override int Radius => 93;

		public override HashSet<uint> NodeIds => new()
		{
			34036,
			34050,
			34051,
		};

		public override HashSet<uint> ItemIds => new()
		{
			10,
			36287,
			38938,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
