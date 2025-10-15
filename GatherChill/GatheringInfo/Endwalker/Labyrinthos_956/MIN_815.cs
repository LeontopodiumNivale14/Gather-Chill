using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Endwalker.Labyrinthos_956
{
	public class MIN_815 : RouteInfo
	{
		public override uint Id => 815;
		public override uint ExpansionId => 4;
		public override uint ZoneId => 956;
		public override uint GatherType => 0;
		public override Vector2 MapPosition => new Vector2(544.8f, -593.332f);
		public override int Radius => 110;

		public override HashSet<uint> NodeIds => new()
		{
			33894,
			33895,
			33896,
			33897,
			33898,
			33899,
		};

		public override HashSet<uint> ItemIds => new()
		{
			13,
			36084,
			36174,
			36669,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
