using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.IlMheg_816
{
	public class BTN_639 : RouteInfo
	{
		public override uint Id => 639;
		public override uint ExpansionId => 3;
		public override uint ZoneId => 816;
		public override uint GatherType => 5;
		public override Vector2 MapPosition => new Vector2(-58.7351f, -13.1144f);
		public override int Radius => 113;

		public override HashSet<uint> NodeIds => new()
		{
			32786,
			32787,
			32788,
		};

		public override HashSet<uint> ItemIds => new()
		{
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
