using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Endwalker.UltimaThule_960
{
	public class MIN_826 : RouteInfo
	{
		public override uint Id => 826;
		public override uint ExpansionId => 4;
		public override uint ZoneId => 960;
		public override uint GatherType => 1;
		public override Vector2 MapPosition => new Vector2(-0.477829f, 598.825f);
		public override int Radius => 130;

		public override HashSet<uint> NodeIds => new()
		{
			33954,
			33955,
			33956,
			33957,
			33958,
			33959,
		};

		public override HashSet<uint> ItemIds => new()
		{
			10,
			36178,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
