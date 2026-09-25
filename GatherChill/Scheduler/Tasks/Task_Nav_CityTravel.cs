using ECommons.GameHelpers;
using ECommons.Throttlers;
using GatherChill.Utilities.Tools;
using GatherChill.Utilities.Traveling;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GatherChill.Scheduler.Tasks
{
    internal class Task_Nav_CityTravel
    {
        public enum TravelType
        {
            Direct,
            Aethernet,
        }

        public class PathInfo
        {
            public Vector3 destination { get; set; } = Vector3.Zero;
            public float distance { get; set; } = 0;
            public List<Vector3> pathTo { get; set; } = null;
            public List<Vector3> pathFrom { get; set; } = null;
            public uint Aethernet_TravelTo { get; set; } = 0;
            public uint Aethernet_TravelFrom { get; set; } = 0;
        }

        public static Dictionary<TravelType, PathInfo> TravelMethod = new()
        {
            [TravelType.Direct] = new(),
            [TravelType.Aethernet] = new(),
        };
        public static Task? _PathCalculations = null;

        private static bool? Paths_Clear()
        {
            foreach (var path in TravelMethod)
            {
                path.Value.pathTo = null;
                path.Value.pathFrom = null;
                path.Value.destination = Vector3.Zero;
                path.Value.distance = 0;
                path.Value.Aethernet_TravelTo = 0;
                path.Value.Aethernet_TravelFrom = 0;
            }
            return true;
        }
        private static bool? CalculateDirect(Vector3 destination)
        {
            string tag = "Navmesh City: Aethernet";
            var method = TravelMethod[TravelType.Direct];

            var playerPosition = Player.Position;
            if (_PathCalculations == null)
            {
                _PathCalculations = Task.Run(async () =>
                {
                    method.pathTo = await FindPath(playerPosition, destination);
                });
                if (EzThrottler.Throttle("Started task"))
                    IceLogging.Verbose("Started to calculate path", tag);
                return false; // Keep checking
            }

            // Wait for completion
            if (!_PathCalculations.IsCompleted)
            {
                if (EzThrottler.Throttle("Calculating path message", 1000))
                {
                    IceLogging.Verbose("Still calculating path that would be direct (via navmesh)", tag);
                }

                return false; // Still calculating
            }

            // Done!
            _PathCalculations = null; // Reset for next use
            if (method.pathTo != null)
            {
                float distance = 0;

                for (int i = 0; i < method.pathTo.Count - 1; i++)
                {
                    var start = method.pathTo[i];
                    var end = method.pathTo[i + 1];

                    distance += Vector3.Distance(start, end);
                }

                if (distance > 0)
                {
                    method.distance = distance;
                }
            }
            IceLogging.Info($"Direct Pathing Complete", tag);
            return true;
        }
        private static bool? CalculateAethernet(Vector3 destination)
        {
            string tag = "Navmesh City: Aethernet";
            var territoryId = Player.Territory.RowId;

            if (TravelUtil.AetherDictionary.TryGetValue(territoryId, out var aethernetInfo))
            {
                // var closestAethernet = 
            }

            return false;
        }

        #region Navmesh Task

        private static async Task<List<Vector3>> FindPath(Vector3 position, Vector3 destination)
        {
            return await P.navmesh.Pathfind(position, destination, false);
        }

        #endregion
    }
}
