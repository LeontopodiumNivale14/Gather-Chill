using Dalamud.Interface.Textures;
using Dalamud.Interface.Utility.Raii;
using GatherChill.ConfigYaml;
using GatherChill.Utilities;
using Microsoft.Data.Sqlite;
using Pictomancy;
using System.Collections.Generic;
using static GatherChill.Utilities.GatheringHelper;

namespace GatherChill.Ui.DebugTabs
{
    internal class RouteEditor
    {
        private static SqliteConnection? dbConn;
        private static RouteRepository? routeRepo;
        private static GatheringHelper.RouteEntry? cachedRoute = null;
        private static uint cachedRouteNumber = 0;

        // Add these fields to store the original database values
        private static string currentExpansion = string.Empty;
        private static string currentArea = string.Empty;
        private static int currentAreaId = 0;

        // Old Stuff

        private static string selectedExpansion = null!;
        private static string selectedZone = null!;
        private static int selectedRouteNumber = -1;

        private static List<string> expansions = new();
        private static List<string> zones = new();
        private static List<GatheringHelper.RouteEntry> routes = new();

        private static string editableExpansion = string.Empty;
        private static string editableArea = string.Empty;
        private static int editableAreaId = 0;
        private static bool loadedRouteData = false;

        private static Stack<(string Expansion, string Area, int AreaId)> undoStack = new();
        private static Stack<(string Expansion, string Area, int AreaId)> redoStack = new();

        // Add these fields for managing the detailed route data
        private static List<uint> currentNodeIds = new();
        private static List<GatheringHelper.GathNodeInfo> currentGatherPoints = new();
        private static List<uint> editableNodeIds = new();
        private static List<GatheringHelper.GathNodeInfo> editableGatherPoints = new();

        // New fields for adding new entries
        private static uint newNodeId = 0;
        private static GatheringHelper.GathNodeInfo newGatherPoint = new();

        // UI state
        private static int selectedNodeIndex = -1;
        private static int selectedGatherPointIndex = -1;

        private static int editableGatheringType = 0;
        private static int currentGatheringType = 0;

        // Picto Stuff
        private static bool Picto_ShowTarget = true;
        private static bool Picto_ShowRadius = true;


        public class RouteRepository
        {
            private readonly SqliteConnection _conn;

            public RouteRepository(SqliteConnection conn)
            {
                _conn = conn;
                if (_conn.State != System.Data.ConnectionState.Open)
                    _conn.Open();
            }

            public GatheringHelper.RouteEntry? GetRoute(uint routeNumber)
            {
                GatheringHelper.RouteEntry? route = null;

                using (var cmd = _conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT Expansion, Area, AreaId FROM Routes WHERE RouteNumber = @num;";
                    cmd.Parameters.AddWithValue("@num", routeNumber);
                    using var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        route = new GatheringHelper.RouteEntry
                        {
                            RouteNumber = routeNumber,
                            Expansion = reader.GetString(0),
                            Area = reader.GetString(1),
                            AreaId = (uint)reader.GetInt32(2)
                        };
                    }
                }

                if (route == null)
                    return null;

                // Load ListNodeIds
                using (var cmd = _conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT NodeId FROM RouteNodeIds WHERE RouteNumber = @num;";
                    cmd.Parameters.AddWithValue("@num", routeNumber);
                    using var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        route.ListNodeIds.Add((uint)reader.GetInt32(0));
                    }
                }

                // Load GatherPoints
                using (var cmd = _conn.CreateCommand())
                {
                    cmd.CommandText = @"
                SELECT NodeId, PositionX, PositionY, PositionZ,
                       LandZoneX, LandZoneY, LandZoneZ,
                       GatheringType, ZoneId, NodeSet
                FROM GatherPoints
                WHERE RouteNumber = @num;";
                    cmd.Parameters.AddWithValue("@num", routeNumber);
                    using var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        route.GatherPoints.Add(new GatheringHelper.GathNodeInfo
                        {
                            NodeId = (uint)reader.GetInt32(0),
                            Position = new Vector3(
                                reader.GetFloat(1),
                                reader.GetFloat(2),
                                reader.GetFloat(3)
                            ),
                            LandZone = new Vector3(
                                reader.GetFloat(4),
                                reader.GetFloat(5),
                                reader.GetFloat(6)
                            ),
                            GatheringType = reader.GetInt32(7),
                            ZoneId = reader.GetInt32(8),
                            NodeSet = (uint)reader.GetInt32(9)
                        });
                    }
                }

                return route;
            }

            public void UpdateRoute(GatheringHelper.RouteEntry route)
            {
                using var transaction = _conn.BeginTransaction();

                // Update Routes table
                using (var cmd = _conn.CreateCommand())
                {
                    cmd.CommandText = @"
                UPDATE Routes SET Expansion = @exp, Area = @area, AreaId = @areaId
                WHERE RouteNumber = @num;";
                    cmd.Parameters.AddWithValue("@exp", route.Expansion);
                    cmd.Parameters.AddWithValue("@area", route.Area);
                    cmd.Parameters.AddWithValue("@areaId", route.AreaId);
                    cmd.Parameters.AddWithValue("@num", route.RouteNumber);
                    cmd.ExecuteNonQuery();
                }

                // Update RouteNodeIds: easiest is delete all for route then insert current
                using (var cmd = _conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM RouteNodeIds WHERE RouteNumber = @num;";
                    cmd.Parameters.AddWithValue("@num", route.RouteNumber);
                    cmd.ExecuteNonQuery();
                }
                foreach (var nodeId in route.ListNodeIds)
                {
                    using var insertCmd = _conn.CreateCommand();
                    insertCmd.CommandText = "INSERT INTO RouteNodeIds (RouteNumber, NodeId) VALUES (@num, @node);";
                    insertCmd.Parameters.AddWithValue("@num", route.RouteNumber);
                    insertCmd.Parameters.AddWithValue("@node", nodeId);
                    insertCmd.ExecuteNonQuery();
                }

                // Update GatherPoints: delete all then insert fresh
                using (var cmd = _conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM GatherPoints WHERE RouteNumber = @num;";
                    cmd.Parameters.AddWithValue("@num", route.RouteNumber);
                    cmd.ExecuteNonQuery();
                }
                foreach (var gp in route.GatherPoints)
                {
                    using var insertCmd = _conn.CreateCommand();
                    insertCmd.CommandText = @"
                INSERT INTO GatherPoints (
                    RouteNumber, NodeId, PositionX, PositionY, PositionZ,
                    LandZoneX, LandZoneY, LandZoneZ, GatheringType, ZoneId, NodeSet)
                VALUES (
                    @routeNum, @nodeId, @posX, @posY, @posZ,
                    @lzX, @lzY, @lzZ, @gType, @zoneId, @nodeSet);";

                    insertCmd.Parameters.AddWithValue("@routeNum", route.RouteNumber);
                    insertCmd.Parameters.AddWithValue("@nodeId", gp.NodeId);
                    insertCmd.Parameters.AddWithValue("@posX", gp.Position.X);
                    insertCmd.Parameters.AddWithValue("@posY", gp.Position.Y);
                    insertCmd.Parameters.AddWithValue("@posZ", gp.Position.Z);
                    insertCmd.Parameters.AddWithValue("@lzX", gp.LandZone.X);
                    insertCmd.Parameters.AddWithValue("@lzY", gp.LandZone.Y);
                    insertCmd.Parameters.AddWithValue("@lzZ", gp.LandZone.Z);
                    insertCmd.Parameters.AddWithValue("@gType", gp.GatheringType);
                    insertCmd.Parameters.AddWithValue("@zoneId", gp.ZoneId);
                    insertCmd.Parameters.AddWithValue("@nodeSet", gp.NodeSet);
                    insertCmd.ExecuteNonQuery();
                }

                transaction.Commit();
            }

            public void DeleteRoute(uint routeNumber)
            {
                using var transaction = _conn.BeginTransaction();

                using (var cmd = _conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM Routes WHERE RouteNumber = @num;";
                    cmd.Parameters.AddWithValue("@num", routeNumber);
                    cmd.ExecuteNonQuery();
                }
                using (var cmd = _conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM RouteNodeIds WHERE RouteNumber = @num;";
                    cmd.Parameters.AddWithValue("@num", routeNumber);
                    cmd.ExecuteNonQuery();
                }
                using (var cmd = _conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM GatherPoints WHERE RouteNumber = @num;";
                    cmd.Parameters.AddWithValue("@num", routeNumber);
                    cmd.ExecuteNonQuery();
                }

                transaction.Commit();
            }

            public void AddRoute(GatheringHelper.RouteEntry route)
            {
                using var transaction = _conn.BeginTransaction();

                using (var cmd = _conn.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO Routes (RouteNumber, Expansion, Area, AreaId) VALUES (@num, @exp, @area, @areaId);";
                    cmd.Parameters.AddWithValue("@num", route.RouteNumber);
                    cmd.Parameters.AddWithValue("@exp", route.Expansion);
                    cmd.Parameters.AddWithValue("@area", route.Area);
                    cmd.Parameters.AddWithValue("@areaId", route.AreaId);
                    cmd.ExecuteNonQuery();
                }

                foreach (var nodeId in route.ListNodeIds)
                {
                    using var insertCmd = _conn.CreateCommand();
                    insertCmd.CommandText = "INSERT INTO RouteNodeIds (RouteNumber, NodeId) VALUES (@num, @node);";
                    insertCmd.Parameters.AddWithValue("@num", route.RouteNumber);
                    insertCmd.Parameters.AddWithValue("@node", nodeId);
                    insertCmd.ExecuteNonQuery();
                }

                foreach (var gp in route.GatherPoints)
                {
                    using var insertCmd = _conn.CreateCommand();
                    insertCmd.CommandText = @"
                INSERT INTO GatherPoints (
                    RouteNumber, NodeId, PositionX, PositionY, PositionZ,
                    LandZoneX, LandZoneY, LandZoneZ, GatheringType, ZoneId, NodeSet)
                VALUES (
                    @routeNum, @nodeId, @posX, @posY, @posZ,
                    @lzX, @lzY, @lzZ, @gType, @zoneId, @nodeSet);";

                    insertCmd.Parameters.AddWithValue("@routeNum", route.RouteNumber);
                    insertCmd.Parameters.AddWithValue("@nodeId", gp.NodeId);
                    insertCmd.Parameters.AddWithValue("@posX", gp.Position.X);
                    insertCmd.Parameters.AddWithValue("@posY", gp.Position.Y);
                    insertCmd.Parameters.AddWithValue("@posZ", gp.Position.Z);
                    insertCmd.Parameters.AddWithValue("@lzX", gp.LandZone.X);
                    insertCmd.Parameters.AddWithValue("@lzY", gp.LandZone.Y);
                    insertCmd.Parameters.AddWithValue("@lzZ", gp.LandZone.Z);
                    insertCmd.Parameters.AddWithValue("@gType", gp.GatheringType);
                    insertCmd.Parameters.AddWithValue("@zoneId", gp.ZoneId);
                    insertCmd.Parameters.AddWithValue("@nodeSet", gp.NodeSet);
                    insertCmd.ExecuteNonQuery();
                }

                transaction.Commit();
            }
        }

        #region SQL Funcitons

        public static void LoadExpansions(SqliteConnection conn)
        {
            expansions.Clear();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT DISTINCT Expansion FROM Routes ORDER BY Expansion;";
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
                expansions.Add(reader.GetString(0));
        }

        public static void LoadZones(SqliteConnection conn, string expansion)
        {
            zones.Clear();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT DISTINCT Area FROM Routes WHERE Expansion = @exp ORDER BY Area;";
            cmd.Parameters.AddWithValue("@exp", expansion);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
                zones.Add(reader.GetString(0));
        }

        private static void LoadRoutes(SqliteConnection conn, string expansion, string zone)
        {
            routes.Clear();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"
        SELECT RouteNumber, Expansion, Area, AreaId
        FROM Routes
        WHERE Expansion = @exp AND Area = @area";
            cmd.Parameters.AddWithValue("@exp", expansion);
            cmd.Parameters.AddWithValue("@area", zone);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var route = new GatheringHelper.RouteEntry
                {
                    RouteNumber = (uint)reader.GetInt32(0),
                    Expansion = reader.GetString(1),
                    Area = reader.GetString(2),
                    AreaId = (uint)reader.GetInt32(3),
                };
                routes.Add(route);
            }
        }

        private static void LoadRouteForEdit(SqliteConnection conn, int routeNumber)
        {
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT Expansion, Area, AreaId, COALESCE(GatheringType, 0) as GatheringType FROM Routes WHERE RouteNumber = @num;";
            cmd.Parameters.AddWithValue("@num", routeNumber);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                // Store current DB values (for display)
                currentExpansion = reader.GetString(0);
                currentArea = reader.GetString(1);
                currentAreaId = reader.GetInt32(2);
                currentGatheringType = reader.GetInt32(3);

                // Set editable values (for editing)
                editableExpansion = currentExpansion;
                editableArea = currentArea;
                editableAreaId = currentAreaId;
                editableGatheringType = currentGatheringType;

                loadedRouteData = true;
            }

            // Load RouteNodeIds
            currentNodeIds.Clear();
            editableNodeIds.Clear();
            using (var nodeCmd = conn.CreateCommand())
            {
                nodeCmd.CommandText = "SELECT NodeId FROM RouteNodeIds WHERE RouteNumber = @num ORDER BY NodeId;";
                nodeCmd.Parameters.AddWithValue("@num", routeNumber);
                using var nodeReader = nodeCmd.ExecuteReader();
                while (nodeReader.Read())
                {
                    uint nodeId = (uint)nodeReader.GetInt32(0);
                    currentNodeIds.Add(nodeId);
                    editableNodeIds.Add(nodeId);
                }
            }

            // Load GatherPoints with new radial fields (no longer loading GatheringType)
            currentGatherPoints.Clear();
            editableGatherPoints.Clear();
            using (var gatherCmd = conn.CreateCommand())
            {
                gatherCmd.CommandText = @"
            SELECT NodeId, PositionX, PositionY, PositionZ,
                   LandZoneX, LandZoneY, LandZoneZ,
                   ZoneId, NodeSet,
                   COALESCE(UseRadialPositioning, 0) as UseRadialPositioning,
                   COALESCE(InnerRadius, 0.0) as InnerRadius,
                   COALESCE(OuterRadius, 5.0) as OuterRadius,
                   COALESCE(StartAngle, 0.0) as StartAngle,
                   COALESCE(EndAngle, 360.0) as EndAngle
            FROM GatherPoints
            WHERE RouteNumber = @num
            ORDER BY NodeId;";
                gatherCmd.Parameters.AddWithValue("@num", routeNumber);
                using var gatherReader = gatherCmd.ExecuteReader();
                while (gatherReader.Read())
                {
                    var gatherPoint = new GatheringHelper.GathNodeInfo
                    {
                        NodeId = (uint)gatherReader.GetInt32(0),
                        Position = new Vector3(
                            gatherReader.GetFloat(1),
                            gatherReader.GetFloat(2),
                            gatherReader.GetFloat(3)
                        ),
                        LandZone = new Vector3(
                            gatherReader.GetFloat(4),
                            gatherReader.GetFloat(5),
                            gatherReader.GetFloat(6)
                        ),
                        GatheringType = currentGatheringType, // Set from route level
                        ZoneId = gatherReader.GetInt32(7),
                        NodeSet = (uint)gatherReader.GetInt32(8),
                        // New radial fields
                        UseRadialPositioning = gatherReader.GetInt32(9) != 0,
                        InnerRadius = gatherReader.GetFloat(10),
                        OuterRadius = gatherReader.GetFloat(11),
                        StartAngle = gatherReader.GetFloat(12),
                        EndAngle = gatherReader.GetFloat(13)
                    };
                    currentGatherPoints.Add(gatherPoint);
                    editableGatherPoints.Add(new GatheringHelper.GathNodeInfo
                    {
                        NodeId = gatherPoint.NodeId,
                        Position = gatherPoint.Position,
                        LandZone = gatherPoint.LandZone,
                        GatheringType = currentGatheringType,
                        ZoneId = gatherPoint.ZoneId,
                        NodeSet = gatherPoint.NodeSet,
                        UseRadialPositioning = gatherPoint.UseRadialPositioning,
                        InnerRadius = gatherPoint.InnerRadius,
                        OuterRadius = gatherPoint.OuterRadius,
                        StartAngle = gatherPoint.StartAngle,
                        EndAngle = gatherPoint.EndAngle
                    });
                }
            }

            // Reset UI state
            selectedNodeIndex = -1;
            selectedGatherPointIndex = -1;
        }

        public static GatheringHelper.RouteEntry? GetRouteEntry(SqliteConnection conn, uint routeNumber)
        {
            GatheringHelper.RouteEntry? route = null;

            // Load base route info
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT Expansion, Area, AreaId FROM Routes WHERE RouteNumber = @num;";
                cmd.Parameters.AddWithValue("@num", routeNumber);
                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    route = new GatheringHelper.RouteEntry
                    {
                        RouteNumber = routeNumber,
                        Expansion = reader.GetString(0),
                        Area = reader.GetString(1),
                        AreaId = (uint)reader.GetInt32(2)
                    };
                }
            }

            if (route == null)
                return null;

            // Load ListNodeIds
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT NodeId FROM RouteNodeIds WHERE RouteNumber = @num;";
                cmd.Parameters.AddWithValue("@num", routeNumber);
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    route.ListNodeIds.Add((uint)reader.GetInt32(0));
                }
            }

            // Load GatherPoints
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"
            SELECT NodeId, PositionX, PositionY, PositionZ,
                   LandZoneX, LandZoneY, LandZoneZ,
                   GatheringType, ZoneId, NodeSet
            FROM GatherPoints
            WHERE RouteNumber = @num;";
                cmd.Parameters.AddWithValue("@num", routeNumber);
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    route.GatherPoints.Add(new GatheringHelper.GathNodeInfo
                    {
                        NodeId = (uint)reader.GetInt32(0),
                        Position = new Vector3(
                            reader.GetFloat(1),
                            reader.GetFloat(2),
                            reader.GetFloat(3)
                        ),
                        LandZone = new Vector3(
                            reader.GetFloat(4),
                            reader.GetFloat(5),
                            reader.GetFloat(6)
                        ),
                        GatheringType = reader.GetInt32(7),
                        ZoneId = reader.GetInt32(8),
                        NodeSet = (uint)reader.GetInt32(9)
                    });
                }
            }

            return route;
        }

        private static GatheringHelper.RouteEntry? GetCachedRoute(SqliteConnection conn, uint routeNumber)
        {
            if (cachedRoute == null || cachedRouteNumber != routeNumber)
            {
                cachedRoute = GetRouteEntry(conn, routeNumber);
                cachedRouteNumber = routeNumber;
            }
            return cachedRoute;
        }

        private static void InitDb()
        {
            if (dbConn == null)
            {
                dbConn = new SqliteConnection($"Data Source={Sqlcreator.SqlitePath}");
                dbConn.Open();
                routeRepo = new RouteRepository(dbConn);

                // Update schema (only runs once, safe to call multiple times)
                Sqlcreator.UpdateDatabaseSchema(dbConn);
            }
        }

        private static void RefreshCache(uint routeNumber)
        {
            if (routeRepo == null) return;

            if (cachedRoute == null || cachedRouteNumber != routeNumber)
            {
                cachedRoute = routeRepo.GetRoute(routeNumber);
                cachedRouteNumber = routeNumber;
            }
        }


        // Call this on route selection change or manual refresh
        private static void RefreshCache(SqliteConnection conn, uint routeNumber)
        {
            cachedRoute = GetRouteEntry(conn, routeNumber);
            cachedRouteNumber = routeNumber;
        }

        private static void SaveAllRouteData(SqliteConnection conn, int routeNumber)
        {
            using var transaction = conn.BeginTransaction();

            try
            {
                // Update Routes table with GatheringType
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                UPDATE Routes SET Expansion = @exp, Area = @area, AreaId = @areaId, GatheringType = @gatheringType
                WHERE RouteNumber = @num;";
                    cmd.Parameters.AddWithValue("@exp", editableExpansion);
                    cmd.Parameters.AddWithValue("@area", editableArea);
                    cmd.Parameters.AddWithValue("@areaId", editableAreaId);
                    cmd.Parameters.AddWithValue("@gatheringType", editableGatheringType);
                    cmd.Parameters.AddWithValue("@num", routeNumber);
                    cmd.ExecuteNonQuery();
                }

                // Update RouteNodeIds
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM RouteNodeIds WHERE RouteNumber = @num;";
                    cmd.Parameters.AddWithValue("@num", routeNumber);
                    cmd.ExecuteNonQuery();
                }
                foreach (var nodeId in editableNodeIds)
                {
                    using var insertCmd = conn.CreateCommand();
                    insertCmd.CommandText = "INSERT INTO RouteNodeIds (RouteNumber, NodeId) VALUES (@num, @node);";
                    insertCmd.Parameters.AddWithValue("@num", routeNumber);
                    insertCmd.Parameters.AddWithValue("@node", nodeId);
                    insertCmd.ExecuteNonQuery();
                }

                // Update GatherPoints (no longer saving GatheringType per point)
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM GatherPoints WHERE RouteNumber = @num;";
                    cmd.Parameters.AddWithValue("@num", routeNumber);
                    cmd.ExecuteNonQuery();
                }
                foreach (var gp in editableGatherPoints)
                {
                    using var insertCmd = conn.CreateCommand();
                    insertCmd.CommandText = @"
                INSERT INTO GatherPoints (
                    RouteNumber, NodeId, PositionX, PositionY, PositionZ,
                    LandZoneX, LandZoneY, LandZoneZ, ZoneId, NodeSet,
                    UseRadialPositioning, InnerRadius, OuterRadius, StartAngle, EndAngle)
                VALUES (
                    @routeNum, @nodeId, @posX, @posY, @posZ,
                    @lzX, @lzY, @lzZ, @zoneId, @nodeSet,
                    @useRadial, @innerRadius, @outerRadius, @startAngle, @endAngle);";

                    insertCmd.Parameters.AddWithValue("@routeNum", routeNumber);
                    insertCmd.Parameters.AddWithValue("@nodeId", gp.NodeId);
                    insertCmd.Parameters.AddWithValue("@posX", gp.Position.X);
                    insertCmd.Parameters.AddWithValue("@posY", gp.Position.Y);
                    insertCmd.Parameters.AddWithValue("@posZ", gp.Position.Z);
                    insertCmd.Parameters.AddWithValue("@lzX", gp.LandZone.X);
                    insertCmd.Parameters.AddWithValue("@lzY", gp.LandZone.Y);
                    insertCmd.Parameters.AddWithValue("@lzZ", gp.LandZone.Z);
                    insertCmd.Parameters.AddWithValue("@zoneId", gp.ZoneId);
                    insertCmd.Parameters.AddWithValue("@nodeSet", gp.NodeSet);
                    insertCmd.Parameters.AddWithValue("@useRadial", gp.UseRadialPositioning ? 1 : 0);
                    insertCmd.Parameters.AddWithValue("@innerRadius", gp.InnerRadius);
                    insertCmd.Parameters.AddWithValue("@outerRadius", gp.OuterRadius);
                    insertCmd.Parameters.AddWithValue("@startAngle", gp.StartAngle);
                    insertCmd.Parameters.AddWithValue("@endAngle", gp.EndAngle);
                    insertCmd.ExecuteNonQuery();
                }

                transaction.Commit();

                // Update current values after successful save
                currentExpansion = editableExpansion;
                currentArea = editableArea;
                currentAreaId = editableAreaId;
                currentGatheringType = editableGatheringType;
                currentNodeIds.Clear();
                currentNodeIds.AddRange(editableNodeIds);
                currentGatherPoints.Clear();
                foreach (var gp in editableGatherPoints)
                {
                    currentGatherPoints.Add(new GatheringHelper.GathNodeInfo
                    {
                        NodeId = gp.NodeId,
                        Position = gp.Position,
                        LandZone = gp.LandZone,
                        GatheringType = currentGatheringType,
                        ZoneId = gp.ZoneId,
                        NodeSet = gp.NodeSet,
                        UseRadialPositioning = gp.UseRadialPositioning,
                        InnerRadius = gp.InnerRadius,
                        OuterRadius = gp.OuterRadius,
                        StartAngle = gp.StartAngle,
                        EndAngle = gp.EndAngle
                    });
                }
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }


        #endregion

        public static void Draw()
        {
            InitDb();

            if (expansions.Count == 0)
                LoadExpansions(dbConn!);

            // --- Expansion Dropdown ---
            if (ImGui.BeginCombo("Expansion", selectedExpansion ?? "Select Expansion"))
            {
                foreach (var exp in expansions)
                {
                    if (ImGui.Selectable(exp, exp == selectedExpansion))
                    {
                        selectedExpansion = exp;
                        selectedZone = null!;
                        selectedRouteNumber = -1;

                        // Load zones for the selected expansion
                        LoadZones(dbConn, selectedExpansion);
                    }
                }
                ImGui.EndCombo();
            }

            // --- Zone Dropdown ---
            if (!string.IsNullOrEmpty(selectedExpansion) && zones.Count > 0)
            {
                if (ImGui.BeginCombo("Zone", selectedZone ?? "Select Zone"))
                {
                    foreach (var zone in zones)
                    {
                        if (ImGui.Selectable(zone, zone == selectedZone))
                        {
                            selectedZone = zone;
                            selectedRouteNumber = -1;

                            // Load routes for the selected expansion and zone
                            LoadRoutes(dbConn, selectedExpansion, selectedZone);
                        }
                    }
                    ImGui.EndCombo();
                }
            }

            // --- Route Selector ---
            if (!string.IsNullOrEmpty(selectedZone) && routes.Count > 0)
            {
                var childHeight = ImGui.GetContentRegionAvail().Y;
                var labelSize = ImGui.CalcTextSize("Route #9999");
                float childWidth = labelSize.X + ImGui.GetStyle().FramePadding.X * 10; // padding + buffer

                ImGui.BeginChild("##RouteSelector", new Vector2(childWidth, childHeight), true);
                foreach (var route in routes.Where(r => r.Expansion == selectedExpansion && r.Area == selectedZone))
                {
                    bool selected = (route.RouteNumber == selectedRouteNumber);
                    if (ImGui.Selectable($"Route #{route.RouteNumber}", selected))
                    {
                        selectedRouteNumber = (int)route.RouteNumber;
                        loadedRouteData = false; // Reset this so route data gets reloaded
                    }
                }
                ImGui.EndChild();
            }

            if (selectedRouteNumber != -1)
            {
                ImGui.SameLine();

                // Calculate proper window size for the editor
                var availableSpace = ImGui.GetContentRegionAvail();

                if (ImGui.BeginChild("##Route Editor", new Vector2(0, 0), true))
                {
                    // Just a text to show which route is currently selected
                    ImGui.Text($"Selected Route: {selectedRouteNumber}");

                    if (!loadedRouteData)
                        LoadRouteForEdit(dbConn, selectedRouteNumber);

                    ImGui.Separator();
                    ImGui.Text("Current Route Info (Database Values)");
                    ImGui.TextColored(new Vector4(1, 1, 0, 1), $"Route #{selectedRouteNumber}");
                    ImGui.Text($"Expansion: {currentExpansion}");
                    ImGui.Text($"Zone Name: {currentArea}");
                    ImGui.Text($"Area ID: {currentAreaId}");
                    ImGui.AlignTextToFramePadding();
                    ImGui.Text($"Gathering Type: {currentGatheringType}");

                    // Icon Stuff here
                    ISharedImmediateTexture? icon = GatheringNodeDict[(uint)currentGatheringType].ShinyIcon;
                    Vector2 size = new Vector2(25, 25);
                    float zoomFactor = 0.25f; // 25% zoom-in
                    float cropAmount = zoomFactor / 2; // Crop equally from all sides

                    Vector2 uv0 = new Vector2(cropAmount, cropAmount);
                    Vector2 uv1 = new Vector2(1 - cropAmount, 1 - cropAmount);

                    ImGui.SameLine();
                    ImGui.Image(icon.GetWrapOrEmpty().ImGuiHandle, size);
                    ImGui.Text($"Node IDs: {string.Join(", ", currentNodeIds)}");
                    ImGui.Text($"Gather Points: {currentGatherPoints.Count} entries");
                    ImGui.Separator();

                    // Calculate space for tabs and save button
                    var tabAreaHeight = ImGui.GetContentRegionAvail().Y - 40; // Leave space for save button

                    // Tabbed interface for editing different aspects
                    if (ImGui.BeginChild("##TabArea", new Vector2(0, tabAreaHeight), false))
                    {
                        if (ImGui.BeginTabBar("EditTabs"))
                        {
                            // Basic Info Tab
                            if (ImGui.BeginTabItem("Basic Info"))
                            {
                                ImGui.Text($"Editing Route #{selectedRouteNumber}");

                                // Expansion dropdown
                                if (ImGui.BeginCombo("Expansion##Edit", editableExpansion))
                                {
                                    foreach (var exp in expansions)
                                    {
                                        if (ImGui.Selectable(exp, exp == editableExpansion))
                                            editableExpansion = exp;
                                    }
                                    ImGui.EndCombo();
                                }

                                // Editable Area text box
                                ImGui.InputText("Zone Name", ref editableArea, 100);
                                ImGui.InputInt("Area ID", ref editableAreaId);

                                // NEW: Gathering Type input
                                ImGui.SliderInt("Gathering Type", ref editableGatheringType, 0, 5);

                                ImGui.EndTabItem();
                            }

                            // Route Node IDs Tab
                            if (ImGui.BeginTabItem("Node IDs"))
                            {
                                ImGui.Text($"Route Node IDs ({editableNodeIds.Count} entries)");

                                // List of current node IDs
                                if (ImGui.BeginChild("##NodeIdsList", new Vector2(0, 200), true))
                                {
                                    for (int i = 0; i < editableNodeIds.Count; i++)
                                    {
                                        bool selected = (i == selectedNodeIndex);
                                        if (ImGui.Selectable($"Node ID: {editableNodeIds[i]}", selected))
                                        {
                                            selectedNodeIndex = i;
                                        }
                                    }
                                    ImGui.EndChild();
                                }

                                // Add new node ID
                                ImGui.Separator();
                                ImGui.Text("Add New Node ID:");
                                int newNodeIdInt = (int)newNodeId;
                                ImGui.InputInt("New Node ID", ref newNodeIdInt);
                                newNodeId = (uint)Math.Max(0, newNodeIdInt);
                                if (ImGui.Button("Add Node ID"))
                                {
                                    if (newNodeId > 0 && !editableNodeIds.Contains(newNodeId))
                                    {
                                        editableNodeIds.Add(newNodeId);
                                        editableNodeIds.Sort();
                                        newNodeId = 0;
                                    }
                                }

                                // Remove selected node ID
                                ImGui.SameLine();
                                if (ImGui.Button("Remove Selected") && selectedNodeIndex >= 0 && selectedNodeIndex < editableNodeIds.Count)
                                {
                                    editableNodeIds.RemoveAt(selectedNodeIndex);
                                    selectedNodeIndex = -1;
                                }

                                ImGui.EndTabItem();
                            }

                            // Gather Points Tab with improved layout
                            if (ImGui.BeginTabItem("Gather Points"))
                            {
                                ImGui.Text($"Gather Points ({editableGatherPoints.Count} entries)");

                                // Limit list to 5 items max, make it scrollable
                                float itemHeight = ImGui.GetTextLineHeightWithSpacing();
                                float maxListHeight = itemHeight * 5 + ImGui.GetStyle().FramePadding.Y * 2;

                                if (ImGui.BeginChild("##GatherPointsList", new Vector2(0, maxListHeight), true))
                                {
                                    for (int i = 0; i < editableGatherPoints.Count; i++)
                                    {
                                        var gp = editableGatherPoints[i];
                                        bool selected = (i == selectedGatherPointIndex);
                                        string displayText = gp.UseRadialPositioning
                                            ? $"Node {gp.NodeId} - Zone {gp.ZoneId} [RADIAL: {gp.InnerRadius:F1}-{gp.OuterRadius:F1}]"
                                            : $"Node {gp.NodeId} - Zone {gp.ZoneId} [FIXED]";

                                        if (ImGui.Selectable(displayText, selected))
                                        {
                                            selectedGatherPointIndex = i;
                                        }
                                    }
                                    ImGui.EndChild();
                                }

                                // bools to be used across both
                                bool currentlyTargeting = Svc.Targets.Target != null;

                                ImGui.Separator();

                                // Calculate remaining space for sub-tabs
                                var remainingTabSpace = ImGui.GetContentRegionAvail().Y;

                                // Sub-tabs for Edit and Add
                                if (ImGui.BeginChild("##GatherPointSubTabArea", new Vector2(0, remainingTabSpace), false))
                                {
                                    if (ImGui.BeginTabBar("GatherPointSubTabs"))
                                    {
                                        // Edit tab - only show if a gather point is selected
                                        bool hasSelection = selectedGatherPointIndex >= 0 && selectedGatherPointIndex < editableGatherPoints.Count;

                                        if (hasSelection && ImGui.BeginTabItem($"Edit Selected (Node {editableGatherPoints[selectedGatherPointIndex].NodeId})"))
                                        {
                                            // Calculate remaining space for the editor
                                            var editSpace = ImGui.GetContentRegionAvail();

                                            if (ImGui.BeginChild("##EditSelectedGatherPoint", new Vector2(0, editSpace.Y - 30), true))
                                            {
                                                var gp = editableGatherPoints[selectedGatherPointIndex];

                                                ImGui.Text("Picto Settings");
                                                ImGui.Checkbox("Show Target Info", ref Picto_ShowTarget);
                                                ImGui.SameLine();
                                                ImGui.Checkbox("Show Fan", ref Picto_ShowRadius);

                                                if (Picto_ShowTarget && gp.Position != Vector3.Zero)
                                                {
                                                    using (var drawList = PictoService.Draw())
                                                    {
                                                        Vector3 position = gp.Position;
                                                        float innerRadius = gp.InnerRadius;
                                                        float outerRadius = gp.OuterRadius;
                                                        float minAngle = GatheringHelper.RadialPositioning.GetStartAngleRadians(gp.StartAngle);
                                                        float maxAngle = GatheringHelper.RadialPositioning.GetStartAngleRadians(gp.EndAngle);

                                                        float dotRadius = C.DotRadius;
                                                        uint dotColor = C.PictoWPColor;
                                                        uint fanColor = C.PictoCircleColor;

                                                        drawList.AddDot(position, dotRadius, dotColor);
                                                        if (Picto_ShowRadius)
                                                        {
                                                            drawList.AddFanFilled(position, innerRadius, outerRadius, minAngle, maxAngle, fanColor);
                                                        }
                                                    }
                                                }

                                                int nodeIdInt = (int)gp.NodeId;
                                                ImGui.SetNextItemWidth(200);
                                                ImGui.InputInt("Node ID", ref nodeIdInt);
                                                gp.NodeId = (uint)Math.Max(0, nodeIdInt);
                                                ImGui.SameLine();
                                                using (ImRaii.Disabled(!currentlyTargeting))
                                                {
                                                    if (ImGui.Button("Current Target"))
                                                    {
                                                        gp.NodeId = Svc.Targets.Target.DataId;
                                                    }
                                                }

                                                int zoneId = gp.ZoneId;
                                                ImGui.SetNextItemWidth(200);
                                                ImGui.InputInt("Zone ID", ref zoneId);
                                                gp.ZoneId = zoneId;

                                                ImGui.Separator();
                                                // Radial positioning toggle
                                                bool useRadial = gp.UseRadialPositioning;
                                                if (ImGui.Checkbox("Use Radial Positioning", ref useRadial))
                                                {
                                                    gp.UseRadialPositioning = useRadial;
                                                }

                                                if (gp.UseRadialPositioning)
                                                {
                                                    // Radial positioning with nested tabs
                                                    ImGui.Text("Radial Area Editing");
                                                    var pos = gp.Position;
                                                    ImGui.SetNextItemWidth(200);
                                                    ImGui.InputFloat3("Target Position", ref pos);
                                                    gp.Position = pos;
                                                    ImGui.SameLine();
                                                    using (ImRaii.Disabled(!currentlyTargeting))
                                                    {
                                                        if (ImGui.Button("Target's POS"))
                                                        {
                                                            gp.Position = Svc.Targets.Target.Position;
                                                        }
                                                    }

                                                    ImGui.Separator();

                                                    // Nested tabs for Basic and Advanced radial settings
                                                    if (ImGui.BeginTabBar("RadialSettingsTabs"))
                                                    {
                                                        if (ImGui.BeginTabItem("Basic"))
                                                        {
                                                            ImGui.Text("Radial Settings:");

                                                            float innerRadius = gp.InnerRadius;
                                                            ImGui.SetNextItemWidth(200);
                                                            ImGui.SliderFloat("Inner Radius", ref innerRadius, 0.0f, 5.0f, "%.1f");
                                                            gp.InnerRadius = innerRadius;

                                                            float outerRadius = gp.OuterRadius;
                                                            ImGui.SetNextItemWidth(200);
                                                            ImGui.SliderFloat("Outer Radius", ref outerRadius, gp.InnerRadius + 0.1f, 5.0f, "%.1f");
                                                            gp.OuterRadius = Math.Max(outerRadius, gp.InnerRadius + 0.1f);

                                                            float startAngle = gp.StartAngle;
                                                            ImGui.SetNextItemWidth(200);
                                                            ImGui.SliderFloat("Start Angle", ref startAngle, 0.0f, 360.0f, "%.0f°");
                                                            gp.StartAngle = startAngle;

                                                            float endAngle = gp.EndAngle;
                                                            ImGui.SetNextItemWidth(200);
                                                            ImGui.SliderFloat("End Angle", ref endAngle, 0.0f, 360.0f, "%.0f°");
                                                            gp.EndAngle = endAngle;

                                                            // Preview button
                                                            if (ImGui.Button("Generate Preview Point"))
                                                            {
                                                                var previewPoint = RadialPositioning.GetRandomPointInFan(
                                                                    gp.Position, gp.InnerRadius, gp.OuterRadius, gp.StartAngle, gp.EndAngle);
                                                                ImGui.SetTooltip($"Preview point: X={previewPoint.X:N2}, Y={previewPoint.Y:N2}, Z={previewPoint.Z:N2}");
                                                            }

                                                            ImGui.EndTabItem();
                                                        }

                                                        if (ImGui.BeginTabItem("Advanced"))
                                                        {
                                                            ImGui.Text("For Pictomancy (Radian Values):");
                                                            ImGui.TextColored(new Vector4(0.7f, 1.0f, 0.7f, 1.0f), $"Start Angle: {RadialPositioning.GetStartAngleRadians(gp.StartAngle):N2} radians");
                                                            ImGui.TextColored(new Vector4(0.7f, 1.0f, 0.7f, 1.0f), $"End Angle: {RadialPositioning.GetEndAngleRadians(gp.EndAngle):N2} radians");
                                                            ImGui.TextColored(new Vector4(0.7f, 1.0f, 0.7f, 1.0f), $"Fan Width: {RadialPositioning.GetFanWidthRadians(gp.StartAngle, gp.EndAngle):N2} radians");

                                                            ImGui.Separator();
                                                            ImGui.Text("Copy Values:");

                                                            // Copy buttons for easy use
                                                            if (ImGui.Button("Copy Start Angle (Radians)"))
                                                            {
                                                                ImGui.SetClipboardText(RadialPositioning.GetStartAngleRadians(gp.StartAngle).ToString("N3"));
                                                            }
                                                            ImGui.SameLine();
                                                            if (ImGui.Button("Copy End Angle (Radians)"))
                                                            {
                                                                ImGui.SetClipboardText(RadialPositioning.GetEndAngleRadians(gp.EndAngle).ToString("N3"));
                                                            }
                                                            ImGui.SameLine();
                                                            if (ImGui.Button("Copy Fan Width (Radians)"))
                                                            {
                                                                ImGui.SetClipboardText(RadialPositioning.GetFanWidthRadians(gp.StartAngle, gp.EndAngle).ToString("N3"));
                                                            }

                                                            ImGui.Separator();
                                                            ImGui.Text("Direct Radian Input:");

                                                            float innerRadius = gp.InnerRadius;
                                                            ImGui.SetNextItemWidth(200);
                                                            ImGui.SliderFloat("Inner Radius", ref innerRadius, 0.0f, 5.0f, "%.1f");
                                                            gp.InnerRadius = innerRadius;

                                                            float outerRadius = gp.OuterRadius;
                                                            ImGui.SetNextItemWidth(200);
                                                            ImGui.SliderFloat("Outer Radius", ref outerRadius, gp.InnerRadius + 0.1f, 5.0f, "%.1f");
                                                            gp.OuterRadius = Math.Max(outerRadius, gp.InnerRadius + 0.1f);

                                                            float startAngleRad = RadialPositioning.DegreesToRadians(gp.StartAngle);
                                                            ImGui.SetNextItemWidth(200);
                                                            if (ImGui.SliderFloat("Start Angle (Radians)", ref startAngleRad, 0.0f, 6.28f, "%.3f"))
                                                            {
                                                                gp.StartAngle = RadialPositioning.RadiansToDegrees(startAngleRad);
                                                            }

                                                            float endAngleRad = RadialPositioning.DegreesToRadians(gp.EndAngle);
                                                            ImGui.SetNextItemWidth(200);
                                                            if (ImGui.SliderFloat("End Angle (Radians)", ref endAngleRad, 0.0f, 6.28f, "%.3f"))
                                                            {
                                                                gp.EndAngle = RadialPositioning.RadiansToDegrees(endAngleRad);
                                                            }

                                                            ImGui.EndTabItem();
                                                        }

                                                        ImGui.EndTabBar();
                                                    }
                                                }
                                                else
                                                {
                                                    // Fixed positioning controls
                                                    ImGui.Text("Fixed Position:");
                                                    var pos = gp.Position;
                                                    ImGui.SetNextItemWidth(200);
                                                    ImGui.InputFloat3("Target Position", ref pos);
                                                    gp.Position = pos;
                                                    ImGui.SameLine();
                                                    using (ImRaii.Disabled(!currentlyTargeting))
                                                    {
                                                        if (ImGui.Button("Target's POS"))
                                                        {
                                                            gp.Position = Svc.Targets.Target.Position;
                                                        }
                                                    }

                                                    // Common fields (always shown) - removed Gathering Type
                                                    var landZone = gp.LandZone;
                                                    ImGui.SetNextItemWidth(200);
                                                    ImGui.InputFloat3("Land Zone", ref landZone);
                                                    gp.LandZone = landZone;
                                                    ImGui.SameLine();
                                                    if (ImGui.Button("CurrentPOS"))
                                                    {
                                                        gp.LandZone = Svc.ClientState.LocalPlayer.Position;
                                                    }
                                                }

                                                // Set the gathering type from route level
                                                gp.GatheringType = editableGatheringType;
                                                editableGatherPoints[selectedGatherPointIndex] = gp;

                                                ImGui.EndChild(); // End EditSelectedGatherPoint child
                                            }

                                            // Remove button at the bottom of edit tab
                                            if (ImGui.Button("Remove This Gather Point"))
                                            {
                                                editableGatherPoints.RemoveAt(selectedGatherPointIndex);
                                                selectedGatherPointIndex = -1;
                                            }

                                            ImGui.EndTabItem();
                                        }

                                        // Add New Gather Point Tab
                                        if (ImGui.BeginTabItem("Add New"))
                                        {
                                            // Calculate remaining space for the add form
                                            var addSpace = ImGui.GetContentRegionAvail();

                                            if (ImGui.BeginChild("##AddNewGatherPoint", new Vector2(0, addSpace.Y - 30), true))
                                            {

                                                ImGui.Text("Picto Settings");
                                                ImGui.Checkbox("Show Target Info", ref Picto_ShowTarget);
                                                ImGui.SameLine();
                                                ImGui.Checkbox("Show Fan", ref Picto_ShowRadius);

                                                if (Picto_ShowTarget && newGatherPoint.Position != Vector3.Zero)
                                                {
                                                    using (var drawList = PictoService.Draw())
                                                    {
                                                        Vector3 position = newGatherPoint.Position;
                                                        float innerRadius = newGatherPoint.InnerRadius;
                                                        float outerRadius = newGatherPoint.OuterRadius;
                                                        float minAngle = GatheringHelper.RadialPositioning.GetStartAngleRadians(newGatherPoint.StartAngle);
                                                        float maxAngle = GatheringHelper.RadialPositioning.GetStartAngleRadians(newGatherPoint.EndAngle);

                                                        float dotRadius = C.DotRadius;
                                                        uint dotColor = C.PictoWPColor;
                                                        uint fanColor = C.PictoCircleColor;

                                                        drawList.AddDot(position, dotRadius, dotColor);
                                                        if (Picto_ShowRadius)
                                                        {
                                                            drawList.AddFanFilled(position, innerRadius, outerRadius, minAngle, maxAngle, fanColor);
                                                        }
                                                    }
                                                }

                                                // Node Id
                                                int newNodeIdGPInt = (int)newGatherPoint.NodeId;
                                                ImGui.SetNextItemWidth(200);
                                                ImGui.InputInt("Node ID", ref newNodeIdGPInt);
                                                newGatherPoint.NodeId = (uint)Math.Max(0, newNodeIdGPInt);
                                                ImGui.SameLine();
                                                using (ImRaii.Disabled(!currentlyTargeting))
                                                {
                                                    if (ImGui.Button("Current Target"))
                                                    {
                                                        newGatherPoint.NodeId = Svc.Targets.Target.DataId;
                                                    }
                                                }

                                                // Zone Id
                                                int newZoneId = newGatherPoint.ZoneId;
                                                ImGui.SetNextItemWidth(200);
                                                ImGui.InputInt("Zone ID", ref newZoneId);
                                                newGatherPoint.ZoneId = newZoneId;
                                                ImGui.SameLine();
                                                if (ImGui.Button("Current Zone"))
                                                {
                                                    newGatherPoint.ZoneId = (int)Utils.CurrentTerritory();
                                                }

                                                ImGui.Separator();

                                                // Radial positioning toggle for new gather point
                                                bool newUseRadial = newGatherPoint.UseRadialPositioning;
                                                if (ImGui.Checkbox("Use Radial Positioning", ref newUseRadial))
                                                {
                                                    newGatherPoint.UseRadialPositioning = newUseRadial;
                                                }

                                                if (newGatherPoint.UseRadialPositioning)
                                                {
                                                    ImGui.Text("Radial Area Editing");
                                                    var newPos = newGatherPoint.Position;
                                                    ImGui.SetNextItemWidth(200);
                                                    ImGui.InputFloat3("Target's Position", ref newPos);
                                                    newGatherPoint.Position = newPos;
                                                    ImGui.SameLine();
                                                    using (ImRaii.Disabled(!currentlyTargeting))
                                                    {
                                                        if (ImGui.Button("Target POS"))
                                                        {
                                                            newGatherPoint.Position = Svc.Targets.Target.Position;
                                                        }
                                                    }

                                                    ImGui.Separator();

                                                    // Nested tabs for Basic and Advanced radial settings (Add New version)
                                                    if (ImGui.BeginTabBar("NewRadialSettingsTabs"))
                                                    {
                                                        if (ImGui.BeginTabItem("Basic"))
                                                        {
                                                            ImGui.Text("Radial Settings:");

                                                            float newInnerRadius = newGatherPoint.InnerRadius;
                                                            ImGui.SetNextItemWidth(200);
                                                            ImGui.SliderFloat("Inner Radius", ref newInnerRadius, 0.0f, 5.0f, "%.1f");
                                                            newGatherPoint.InnerRadius = newInnerRadius;

                                                            float newOuterRadius = newGatherPoint.OuterRadius;
                                                            ImGui.SetNextItemWidth(200);
                                                            ImGui.SliderFloat("Outer Radius", ref newOuterRadius, newGatherPoint.InnerRadius + 0.1f, 5.0f, "%.1f");
                                                            newGatherPoint.OuterRadius = Math.Max(newOuterRadius, newGatherPoint.InnerRadius + 0.1f);

                                                            float newStartAngle = newGatherPoint.StartAngle;
                                                            ImGui.SetNextItemWidth(200);
                                                            ImGui.SliderFloat("Start Angle", ref newStartAngle, 0.0f, 360.0f, "%.0f°");
                                                            newGatherPoint.StartAngle = newStartAngle;

                                                            float newEndAngle = newGatherPoint.EndAngle;
                                                            ImGui.SetNextItemWidth(200);
                                                            ImGui.SliderFloat("End Angle", ref newEndAngle, 0.0f, 360.0f, "%.0f°");
                                                            newGatherPoint.EndAngle = newEndAngle;

                                                            // Preview button for new gather point
                                                            if (ImGui.Button("Generate Preview Point"))
                                                            {
                                                                var previewPoint = RadialPositioning.GetRandomPointInFan(
                                                                    newGatherPoint.Position, newGatherPoint.InnerRadius, newGatherPoint.OuterRadius,
                                                                    newGatherPoint.StartAngle, newGatherPoint.EndAngle);
                                                                ImGui.SetTooltip($"Preview point: X={previewPoint.X:F2}, Y={previewPoint.Y:F2}, Z={previewPoint.Z:F2}");
                                                            }

                                                            ImGui.EndTabItem();
                                                        }

                                                        if (ImGui.BeginTabItem("Advanced"))
                                                        {
                                                            ImGui.Text("For Pictomancy (Radian Values):");
                                                            ImGui.TextColored(new Vector4(0.7f, 1.0f, 0.7f, 1.0f), $"Start Angle: {RadialPositioning.GetStartAngleRadians(newGatherPoint.StartAngle):N3} radians");
                                                            ImGui.TextColored(new Vector4(0.7f, 1.0f, 0.7f, 1.0f), $"End Angle: {RadialPositioning.GetEndAngleRadians(newGatherPoint.EndAngle):N3} radians");
                                                            ImGui.TextColored(new Vector4(0.7f, 1.0f, 0.7f, 1.0f), $"Fan Width: {RadialPositioning.GetFanWidthRadians(newGatherPoint.StartAngle, newGatherPoint.EndAngle):N3} radians");

                                                            ImGui.Separator();
                                                            ImGui.Text("Copy Values:");

                                                            // Copy buttons for new gather point
                                                            if (ImGui.Button("Copy Start Angle (Radians)##New"))
                                                            {
                                                                ImGui.SetClipboardText(RadialPositioning.GetStartAngleRadians(newGatherPoint.StartAngle).ToString("N3"));
                                                            }
                                                            ImGui.SameLine();
                                                            if (ImGui.Button("Copy End Angle (Radians)##New"))
                                                            {
                                                                ImGui.SetClipboardText(RadialPositioning.GetEndAngleRadians(newGatherPoint.EndAngle).ToString("N3"));
                                                            }
                                                            ImGui.SameLine();
                                                            if (ImGui.Button("Copy Fan Width (Radians)##New"))
                                                            {
                                                                ImGui.SetClipboardText(RadialPositioning.GetFanWidthRadians(newGatherPoint.StartAngle, newGatherPoint.EndAngle).ToString("N3"));
                                                            }

                                                            ImGui.Separator();
                                                            ImGui.Text("Direct Radian Input:");

                                                            float newInnerRadius = newGatherPoint.InnerRadius;
                                                            ImGui.SetNextItemWidth(200);
                                                            ImGui.SliderFloat("Inner Radius", ref newInnerRadius, 0.0f, 5.0f, "%.1f");
                                                            newGatherPoint.InnerRadius = newInnerRadius;

                                                            float newOuterRadius = newGatherPoint.OuterRadius;
                                                            ImGui.SetNextItemWidth(200);
                                                            ImGui.SliderFloat("Outer Radius", ref newOuterRadius, newGatherPoint.InnerRadius + 0.1f, 5.0f, "%.1f");
                                                            newGatherPoint.OuterRadius = Math.Max(newOuterRadius, newGatherPoint.InnerRadius + 0.1f);
                                                            float newStartAngleRad = RadialPositioning.DegreesToRadians(newGatherPoint.StartAngle);
                                                            ImGui.SetNextItemWidth(200);
                                                            if (ImGui.SliderFloat("Start Angle (Radians)##New", ref newStartAngleRad, 0.0f, 6.28f, "%.3f"))
                                                            {
                                                                newGatherPoint.StartAngle = RadialPositioning.RadiansToDegrees(newStartAngleRad);
                                                            }

                                                            float newEndAngleRad = RadialPositioning.DegreesToRadians(newGatherPoint.EndAngle);
                                                            ImGui.SetNextItemWidth(200);
                                                            if (ImGui.SliderFloat("End Angle (Radians)##New", ref newEndAngleRad, 0.0f, 6.28f, "%.3f"))
                                                            {
                                                                newGatherPoint.EndAngle = RadialPositioning.RadiansToDegrees(newEndAngleRad);
                                                            }

                                                            ImGui.EndTabItem();
                                                        }

                                                        ImGui.EndTabBar();
                                                    }
                                                }
                                                else
                                                {
                                                    ImGui.Text("Set Positioning");
                                                    var newPos = newGatherPoint.Position;
                                                    ImGui.SetNextItemWidth(200);
                                                    ImGui.InputFloat3("Target Position", ref newPos);
                                                    newGatherPoint.Position = newPos;
                                                    ImGui.SameLine();
                                                    if (ImGui.Button("Set to Target's POS"))
                                                    {
                                                        newGatherPoint.Position = Svc.Targets.Target.Position;
                                                    }

                                                    // Landing Zone, used if not on radial landing
                                                    var newLandZone = newGatherPoint.LandZone;
                                                    ImGui.SetNextItemWidth(200);
                                                    ImGui.InputFloat3("Land Zone", ref newLandZone);
                                                    newGatherPoint.LandZone = newLandZone;
                                                    ImGui.SameLine();
                                                    if (ImGui.Button("CurrentPOS"))
                                                    {
                                                        newGatherPoint.LandZone = Svc.ClientState.LocalPlayer.Position;
                                                    }
                                                }

                                                ImGui.EndChild(); // End AddNewGatherPoint child
                                            }

                                            // Add button at the bottom of add tab
                                            if (ImGui.Button("Add Gather Point"))
                                            {
                                                if (newGatherPoint.NodeId > 0)
                                                {
                                                    // Set gathering type from route level
                                                    newGatherPoint.GatheringType = editableGatheringType;

                                                    editableGatherPoints.Add(new GatheringHelper.GathNodeInfo
                                                    {
                                                        NodeId = newGatherPoint.NodeId,
                                                        Position = newGatherPoint.Position,
                                                        LandZone = newGatherPoint.LandZone,
                                                        GatheringType = editableGatheringType,
                                                        ZoneId = newGatherPoint.ZoneId,
                                                        NodeSet = newGatherPoint.NodeSet,
                                                        UseRadialPositioning = newGatherPoint.UseRadialPositioning,
                                                        InnerRadius = newGatherPoint.InnerRadius,
                                                        OuterRadius = newGatherPoint.OuterRadius,
                                                        StartAngle = newGatherPoint.StartAngle,
                                                        EndAngle = newGatherPoint.EndAngle
                                                    });
                                                    newGatherPoint = new GatheringHelper.GathNodeInfo(); // Reset with default values
                                                }
                                            }

                                            ImGui.EndTabItem();
                                        }

                                        ImGui.EndTabBar(); // End GatherPointSubTabs
                                    }
                                    ImGui.EndChild(); // End GatherPointSubTabArea
                                }

                                ImGui.EndTabItem(); // End Gather Points main tab
                            }

                            ImGui.EndTabBar(); // End EditTabs
                        }
                        ImGui.EndChild(); // End TabArea
                    }

                    // Save button at the bottom - now properly positioned
                    ImGui.Separator();
                    if (ImGui.Button("Save All Changes"))
                    {
                        try
                        {
                            SaveAllRouteData(dbConn, selectedRouteNumber);

                            loadedRouteData = false;
                            LoadZones(dbConn, editableExpansion);
                            LoadRoutes(dbConn, editableExpansion, editableArea);
                            selectedZone = editableArea;

                            RefreshCache(dbConn, (uint)selectedRouteNumber);

                            ImGui.OpenPopup("SavedPopup");
                        }
                        catch (Exception ex)
                        {
                            ImGui.OpenPopup("ErrorPopup");
                        }
                    }

                    // Optional confirmation popup
                    if (ImGui.BeginPopup("SavedPopup"))
                    {
                        ImGui.Text("All changes saved successfully!");
                        ImGui.EndPopup();
                    }

                    // Error popup
                    if (ImGui.BeginPopup("ErrorPopup"))
                    {
                        ImGui.Text("Error saving changes!");
                        ImGui.EndPopup();
                    }

                    ImGui.EndChild();
                }
            }
        }
    }
}
