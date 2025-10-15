using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Endwalker.Thavnair_957
{
	public class MIN_932 : RouteInfo
	{
		public override uint Id => 932;
		public override uint ExpansionId => 4;
		public override uint ZoneId => 957;
		public override uint GatherType => 1;
		public override Vector2 MapPosition => new Vector2(147.766f, -420.814f);
		public override int Radius => 41;

		public override HashSet<uint> NodeIds => new()
		{
			34415,
		};

		public override HashSet<uint> ItemIds => new()
		{
			39708,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
