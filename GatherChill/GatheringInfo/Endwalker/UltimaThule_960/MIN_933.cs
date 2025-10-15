using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Endwalker.UltimaThule_960
{
	public class MIN_933 : RouteInfo
	{
		public override uint Id => 933;
		public override uint ExpansionId => 4;
		public override uint ZoneId => 960;
		public override uint GatherType => 0;
		public override Vector2 MapPosition => new Vector2(-225.59f, 394.273f);
		public override int Radius => 16;

		public override HashSet<uint> NodeIds => new()
		{
			34417,
		};

		public override HashSet<uint> ItemIds => new()
		{
			39706,
			41416,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
