using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.SouthShroud_153
{
	public class MIN_240 : RouteInfo
	{
		public override uint Id => 240;
		public override uint ExpansionId => 0;
		public override uint ZoneId => 153;
		public override uint GatherType => 1;
		public override Vector2 MapPosition => new Vector2(-234.822f, 457.929f);
		public override int Radius => 106;

		public override HashSet<uint> NodeIds => new()
		{
			31034,
		};

		public override HashSet<uint> ItemIds => new()
		{
			7610,
			7763,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
