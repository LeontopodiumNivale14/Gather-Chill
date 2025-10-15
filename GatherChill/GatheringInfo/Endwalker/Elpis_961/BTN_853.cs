using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Endwalker.Elpis_961
{
	public class BTN_853 : RouteInfo
	{
		public override uint Id => 853;
		public override uint ExpansionId => 4;
		public override uint ZoneId => 961;
		public override uint GatherType => 2;
		public override Vector2 MapPosition => new Vector2(173.97f, -791.958f);
		public override int Radius => 9;

		public override HashSet<uint> NodeIds => new()
		{
			34040,
		};

		public override HashSet<uint> ItemIds => new()
		{
			36306,
			36308,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
