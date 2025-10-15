using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.EasternThanalan_145
{
	public class MIN_160 : RouteInfo
	{
		public override uint Id => 160;
		public override uint ExpansionId => 0;
		public override uint ZoneId => 145;
		public override uint GatherType => 0;
		public override Vector2 MapPosition => new Vector2(-170.543f, -47.8507f);
		public override int Radius => 58;

		public override HashSet<uint> NodeIds => new()
		{
			30437,
			30438,
			30439,
			30440,
		};

		public override HashSet<uint> ItemIds => new()
		{
			2,
			5130,
			5132,
			5524,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
