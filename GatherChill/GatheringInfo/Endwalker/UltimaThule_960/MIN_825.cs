using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Endwalker.UltimaThule_960
{
	public class MIN_825 : RouteInfo
	{
		public override uint Id => 825;
		public override uint ExpansionId => 4;
		public override uint ZoneId => 960;
		public override uint GatherType => 0;
		public override Vector2 MapPosition => new Vector2(-148.412f, -455.62f);
		public override int Radius => 108;

		public override HashSet<uint> NodeIds => new()
		{
			33948,
			33949,
			33950,
			33951,
			33952,
			33953,
		};

		public override HashSet<uint> ItemIds => new()
		{
			10,
			36166,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
