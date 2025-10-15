using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.LowerLaNoscea_135
{
	public class BTN_31 : RouteInfo
	{
		public override uint Id => 31;
		public override uint ExpansionId => 0;
		public override uint ZoneId => 135;
		public override uint GatherType => 3;
		public override Vector2 MapPosition => new Vector2(258.314f, 38.0522f);
		public override int Radius => 70;

		public override HashSet<uint> NodeIds => new()
		{
			30080,
			30083,
			30084,
			30085,
		};

		public override HashSet<uint> ItemIds => new()
		{
			5,
			4783,
			4784,
			4804,
			4808,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
