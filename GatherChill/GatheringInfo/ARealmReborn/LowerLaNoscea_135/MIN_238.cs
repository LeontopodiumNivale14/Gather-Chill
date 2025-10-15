using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.LowerLaNoscea_135
{
	public class MIN_238 : RouteInfo
	{
		public override uint Id => 238;
		public override uint ExpansionId => 0;
		public override uint ZoneId => 135;
		public override uint GatherType => 0;
		public override Vector2 MapPosition => new Vector2(112.944f, 7.37091f);
		public override int Radius => 28;

		public override HashSet<uint> NodeIds => new()
		{
			31032,
		};

		public override HashSet<uint> ItemIds => new()
		{
			5146,
			5151,
			10095,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
