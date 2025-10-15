using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.IlMheg_816
{
	public class MIN_938 : RouteInfo
	{
		public override uint Id => 938;
		public override uint ExpansionId => 3;
		public override uint ZoneId => 816;
		public override uint GatherType => 0;
		public override Vector2 MapPosition => new Vector2(-384.966f, -565.31f);
		public override int Radius => 155;

		public override HashSet<uint> NodeIds => new()
		{
			34478,
			34479,
			34480,
		};

		public override HashSet<uint> ItemIds => new()
		{
			41286,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
