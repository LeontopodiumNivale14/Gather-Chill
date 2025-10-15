using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheDiadem_929
{
	public class BTN_749 : RouteInfo
	{
		public override uint Id => 749;
		public override uint ExpansionId => 1;
		public override uint ZoneId => 929;
		public override uint GatherType => 3;
		public override Vector2 MapPosition => new Vector2(112.566f, -115.09f);
		public override int Radius => 964;

		public override HashSet<uint> NodeIds => new()
		{
			33489,
			33494,
			33496,
			33501,
			33508,
			33515,
			33522,
			33527,
			33529,
			33533,
			33539,
			33546,
			33551,
			33553,
			33558,
			33560,
			33565,
			33572,
			33579,
		};

		public override HashSet<uint> ItemIds => new()
		{
			29937,
			31281,
			31289,
			31300,
			31309,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
