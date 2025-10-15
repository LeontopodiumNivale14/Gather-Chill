using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheDiadem_939
{
	public class MIN_799 : RouteInfo
	{
		public override uint Id => 799;
		public override uint ExpansionId => 1;
		public override uint ZoneId => 939;
		public override uint GatherType => 1;
		public override Vector2 MapPosition => new Vector2(627.857f, -405.828f);
		public override int Radius => 13;

		public override HashSet<uint> NodeIds => new()
		{
			33837,
		};

		public override HashSet<uint> ItemIds => new()
		{
			29947,
			31319,
			32048,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
