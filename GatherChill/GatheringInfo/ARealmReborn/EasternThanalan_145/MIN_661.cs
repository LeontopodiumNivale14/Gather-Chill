using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.EasternThanalan_145
{
	public class MIN_661 : RouteInfo
	{
		public override uint Id => 661;
		public override uint ExpansionId => 0;
		public override uint ZoneId => 145;
		public override uint GatherType => 1;
		public override Vector2 MapPosition => new Vector2(-419.001f, 244.045f);
		public override int Radius => 29;

		public override HashSet<uint> NodeIds => new()
		{
			32871,
			32872,
			32873,
			32874,
		};

		public override HashSet<uint> ItemIds => new()
		{
			28772,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
