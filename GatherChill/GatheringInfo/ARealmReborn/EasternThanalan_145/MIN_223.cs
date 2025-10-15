using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.EasternThanalan_145
{
	public class MIN_223 : RouteInfo
	{
		public override uint Id => 223;
		public override uint ExpansionId => 0;
		public override uint ZoneId => 145;
		public override uint GatherType => 0;
		public override Vector2 MapPosition => new Vector2(329.369f, 11.574f);
		public override int Radius => 35;

		public override HashSet<uint> NodeIds => new()
		{
			31011,
		};

		public override HashSet<uint> ItemIds => new()
		{
			5118,
			6199,
			10095,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
