using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheDiadem_939
{
	public class BTN_792 : RouteInfo
	{
		public override uint Id => 792;
		public override uint ExpansionId => 1;
		public override uint ZoneId => 939;
		public override uint GatherType => 3;
		public override Vector2 MapPosition => new Vector2(110.359f, -223.117f);
		public override int Radius => 912;

		public override HashSet<uint> NodeIds => new()
		{
			33740,
			33745,
			33752,
			33759,
			33766,
			33771,
			33773,
			33778,
			33780,
			33792,
			33799,
			33806,
			33811,
			33813,
			33818,
			33820,
			33825,
			33832,
		};

		public override HashSet<uint> ItemIds => new()
		{
			29936,
			31308,
			32006,
			32017,
			32027,
			32037,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
