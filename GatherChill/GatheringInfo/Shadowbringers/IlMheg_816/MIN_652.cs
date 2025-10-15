using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.IlMheg_816
{
	public class MIN_652 : RouteInfo
	{
		public override uint Id => 652;
		public override uint ExpansionId => 3;
		public override uint ZoneId => 816;
		public override uint GatherType => 0;
		public override Vector2 MapPosition => new Vector2(594.765f, 475.377f);
		public override int Radius => 117;

		public override HashSet<uint> NodeIds => new()
		{
			32833,
			32834,
			32835,
			32836,
			32837,
			32838,
		};

		public override HashSet<uint> ItemIds => new()
		{
			13,
			28201,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
