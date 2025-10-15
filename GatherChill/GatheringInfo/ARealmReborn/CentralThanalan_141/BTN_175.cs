using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.CentralThanalan_141
{
	public class BTN_175 : RouteInfo
	{
		public override uint Id => 175;
		public override uint ExpansionId => 0;
		public override uint ZoneId => 141;
		public override uint GatherType => 2;
		public override Vector2 MapPosition => new Vector2(158.48f, 459.158f);
		public override int Radius => 58;

		public override HashSet<uint> NodeIds => new()
		{
			30086,
			30087,
			30088,
			30089,
		};

		public override HashSet<uint> ItemIds => new()
		{
			6,
			12,
			5820,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
