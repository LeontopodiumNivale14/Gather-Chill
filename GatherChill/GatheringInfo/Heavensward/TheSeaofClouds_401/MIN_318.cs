using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheSeaofClouds_401
{
	public class MIN_318 : RouteInfo
	{
		public override uint Id => 318;
		public override uint ExpansionId => 1;
		public override uint ZoneId => 401;
		public override uint GatherType => 1;
		public override Vector2 MapPosition => new Vector2(-740.089f, -747.542f);
		public override int Radius => 83;

		public override HashSet<uint> NodeIds => new()
		{
			31459,
		};

		public override HashSet<uint> ItemIds => new()
		{
			5159,
			12901,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
