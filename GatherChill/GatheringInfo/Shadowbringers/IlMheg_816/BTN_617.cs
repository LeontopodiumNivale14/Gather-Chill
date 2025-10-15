using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.IlMheg_816
{
	public class BTN_617 : RouteInfo
	{
		public override uint Id => 617;
		public override uint ExpansionId => 3;
		public override uint ZoneId => 816;
		public override uint GatherType => 3;
		public override Vector2 MapPosition => new Vector2(-442.769f, 71.0564f);
		public override int Radius => 138;

		public override HashSet<uint> NodeIds => new()
		{
			32721,
			32722,
			32723,
			32724,
			32725,
			32726,
		};

		public override HashSet<uint> ItemIds => new()
		{
			13,
			26754,
			26756,
			27750,
			27823,
			27824,
			27825,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
