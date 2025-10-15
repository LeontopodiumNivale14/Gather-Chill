using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.TheAzimSteppe_622
{
	public class MIN_501 : RouteInfo
	{
		public override uint Id => 501;
		public override uint ExpansionId => 2;
		public override uint ZoneId => 622;
		public override uint GatherType => 0;
		public override Vector2 MapPosition => new Vector2(-827.258f, 401.582f);
		public override int Radius => 122;

		public override HashSet<uint> NodeIds => new()
		{
			32184,
		};

		public override HashSet<uint> ItemIds => new()
		{
			19971,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
