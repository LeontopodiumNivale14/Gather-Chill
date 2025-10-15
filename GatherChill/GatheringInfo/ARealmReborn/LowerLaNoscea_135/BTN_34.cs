using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.LowerLaNoscea_135
{
	public class BTN_34 : RouteInfo
	{
		public override uint Id => 34;
		public override uint ExpansionId => 0;
		public override uint ZoneId => 135;
		public override uint GatherType => 2;
		public override Vector2 MapPosition => new Vector2(661.98f, -219.948f);
		public override int Radius => 56;

		public override HashSet<uint> NodeIds => new()
		{
			30094,
			30095,
			30096,
			30097,
		};

		public override HashSet<uint> ItemIds => new()
		{
			5,
			4811,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
