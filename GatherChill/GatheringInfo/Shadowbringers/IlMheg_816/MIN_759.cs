using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.IlMheg_816
{
	public class MIN_759 : RouteInfo
	{
		public override uint Id => 759;
		public override uint ExpansionId => 3;
		public override uint ZoneId => 816;
		public override uint GatherType => 1;
		public override Vector2 MapPosition => new Vector2(710.42f, -638.769f);
		public override int Radius => 1;

		public override HashSet<uint> NodeIds => new()
		{
			33589,
		};

		public override HashSet<uint> ItemIds => new()
		{
			32952,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
