using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheDiadem_901
{
	public class BTN_806 : RouteInfo
	{
		public override uint Id => 806;
		public override uint ExpansionId => 1;
		public override uint ZoneId => 901;
		public override uint GatherType => 2;
		public override Vector2 MapPosition => new Vector2(286.125f, -321.697f);
		public override int Radius => 94;

		public override HashSet<uint> NodeIds => new()
		{
			33852,
			33853,
			33854,
		};

		public override HashSet<uint> ItemIds => new()
		{
			31767,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
