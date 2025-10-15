using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Endwalker.Labyrinthos_956
{
	public class BTN_838 : RouteInfo
	{
		public override uint Id => 838;
		public override uint ExpansionId => 4;
		public override uint ZoneId => 956;
		public override uint GatherType => 3;
		public override Vector2 MapPosition => new Vector2(325.341f, -225.836f);
		public override int Radius => 107;

		public override HashSet<uint> NodeIds => new()
		{
			33976,
			33977,
			33978,
			33979,
			33980,
			33981,
		};

		public override HashSet<uint> ItemIds => new()
		{
			10,
			35848,
			36083,
			36088,
			36202,
			36669,
			41068,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
