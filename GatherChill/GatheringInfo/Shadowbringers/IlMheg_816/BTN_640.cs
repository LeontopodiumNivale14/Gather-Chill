using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.IlMheg_816
{
	public class BTN_640 : RouteInfo
	{
		public override uint Id => 640;
		public override uint ExpansionId => 3;
		public override uint ZoneId => 816;
		public override uint GatherType => 5;
		public override Vector2 MapPosition => new Vector2(216.09f, 197.391f);
		public override int Radius => 129;

		public override HashSet<uint> NodeIds => new()
		{
			32789,
			32790,
			32791,
		};

		public override HashSet<uint> ItemIds => new()
		{
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
