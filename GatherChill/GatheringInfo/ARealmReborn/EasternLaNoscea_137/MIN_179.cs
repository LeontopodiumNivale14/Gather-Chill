using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.EasternLaNoscea_137
{
	public class MIN_179 : RouteInfo
	{
		public override uint Id => 179;
		public override uint ExpansionId => 0;
		public override uint ZoneId => 137;
		public override uint GatherType => 0;
		public override Vector2 MapPosition => new Vector2(391.978f, 286.365f);
		public override int Radius => 85;

		public override HashSet<uint> NodeIds => new()
		{
			30499,
			30500,
			30501,
			30502,
		};

		public override HashSet<uint> ItemIds => new()
		{
			7,
			5134,
			5135,
			5139,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
