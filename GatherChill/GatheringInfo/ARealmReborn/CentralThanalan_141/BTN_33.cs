using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.CentralThanalan_141
{
	public class BTN_33 : RouteInfo
	{
		public override uint Id => 33;
		public override uint ExpansionId => 0;
		public override uint ZoneId => 141;
		public override uint GatherType => 2;
		public override Vector2 MapPosition => new Vector2(-27.2814f, -65.9459f);
		public override int Radius => 64;

		public override HashSet<uint> NodeIds => new()
		{
			30090,
			30091,
			30092,
			30093,
		};

		public override HashSet<uint> ItemIds => new()
		{
			6,
			4786,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
