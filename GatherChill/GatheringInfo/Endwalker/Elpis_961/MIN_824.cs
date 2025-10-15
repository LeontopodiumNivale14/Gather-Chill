using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Endwalker.Elpis_961
{
	public class MIN_824 : RouteInfo
	{
		public override uint Id => 824;
		public override uint ExpansionId => 4;
		public override uint ZoneId => 961;
		public override uint GatherType => 1;
		public override Vector2 MapPosition => new Vector2(-375.087f, -160.808f);
		public override int Radius => 108;

		public override HashSet<uint> NodeIds => new()
		{
			33942,
			33943,
			33944,
			33945,
			33946,
			33947,
		};

		public override HashSet<uint> ItemIds => new()
		{
			8,
			36177,
			36181,
			36263,
			41072,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
