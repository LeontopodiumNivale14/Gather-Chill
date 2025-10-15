using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.IlMheg_816
{
	public class BTN_930 : RouteInfo
	{
		public override uint Id => 930;
		public override uint ExpansionId => 3;
		public override uint ZoneId => 816;
		public override uint GatherType => 2;
		public override Vector2 MapPosition => new Vector2(217.978f, -123.821f);
		public override int Radius => 181;

		public override HashSet<uint> NodeIds => new()
		{
			34472,
			34473,
			34474,
		};

		public override HashSet<uint> ItemIds => new()
		{
			39807,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
