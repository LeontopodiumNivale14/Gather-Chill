using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheDiadem_901
{
	public class MIN_698 : RouteInfo
	{
		public override uint Id => 698;
		public override uint ExpansionId => 1;
		public override uint ZoneId => 901;
		public override uint GatherType => 0;
		public override Vector2 MapPosition => new Vector2(127.828f, -14.0608f);
		public override int Radius => 907;

		public override HashSet<uint> NodeIds => new()
		{
			33037,
			33042,
			33049,
			33056,
			33063,
			33068,
			33070,
			33075,
			33077,
			33082,
			33089,
			33096,
			33103,
			33108,
			33110,
			33115,
			33117,
			33122,
			33129,
		};

		public override HashSet<uint> ItemIds => new()
		{
			29896,
			29909,
			29919,
			29929,
			29939,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
