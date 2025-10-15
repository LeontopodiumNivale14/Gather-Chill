using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.EasternThanalan_145
{
	public class MIN_201 : RouteInfo
	{
		public override uint Id => 201;
		public override uint ExpansionId => 0;
		public override uint ZoneId => 145;
		public override uint GatherType => 1;
		public override Vector2 MapPosition => new Vector2(141.748f, -95.2652f);
		public override int Radius => 55;

		public override HashSet<uint> NodeIds => new()
		{
			30479,
			30480,
			30481,
			30482,
		};

		public override HashSet<uint> ItemIds => new()
		{
			2,
			8,
			5815,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
