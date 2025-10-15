using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Endwalker.Thavnair_957
{
	public class MIN_819 : RouteInfo
	{
		public override uint Id => 819;
		public override uint ExpansionId => 4;
		public override uint ZoneId => 957;
		public override uint GatherType => 0;
		public override Vector2 MapPosition => new Vector2(-212.676f, -112.423f);
		public override int Radius => 124;

		public override HashSet<uint> NodeIds => new()
		{
			33915,
			33916,
			33917,
		};

		public override HashSet<uint> ItemIds => new()
		{
			36291,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
