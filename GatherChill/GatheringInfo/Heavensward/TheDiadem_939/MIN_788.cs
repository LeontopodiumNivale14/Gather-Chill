using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheDiadem_939
{
	public class MIN_788 : RouteInfo
	{
		public override uint Id => 788;
		public override uint ExpansionId => 1;
		public override uint ZoneId => 939;
		public override uint GatherType => 0;
		public override Vector2 MapPosition => new Vector2(10.0632f, -208.93f);
		public override int Radius => 855;

		public override HashSet<uint> NodeIds => new()
		{
			33646,
			33651,
			33653,
			33658,
			33660,
			33665,
			33672,
			33679,
			33690,
			33691,
			33694,
			33699,
			33701,
			33706,
			33708,
			33713,
			33720,
			33727,
			33734,
		};

		public override HashSet<uint> ItemIds => new()
		{
			29941,
			31313,
			32013,
			32022,
			32032,
			32042,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
