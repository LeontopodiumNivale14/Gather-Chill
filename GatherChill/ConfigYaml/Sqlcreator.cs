using GatherChill.Utilities;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.IO;

namespace GatherChill.ConfigYaml
{
    internal class Sqlcreator
    {
        public static void EnsureTables(SqliteConnection conn)
        {
            var commands = new[]
            {
        @"CREATE TABLE IF NOT EXISTS Routes (
            RouteNumber INTEGER PRIMARY KEY,
            Expansion TEXT NOT NULL,
            Area TEXT NOT NULL,
            AreaId INTEGER NOT NULL
        );",
        @"CREATE TABLE IF NOT EXISTS RouteNodeIds (
            RouteNumber INTEGER NOT NULL,
            NodeId INTEGER NOT NULL,
            PRIMARY KEY (RouteNumber, NodeId),
            FOREIGN KEY (RouteNumber) REFERENCES Routes(RouteNumber)
        );",
        @"CREATE TABLE IF NOT EXISTS GatherPoints (
            RouteNumber INTEGER NOT NULL,
            NodeId INTEGER NOT NULL,
            PositionX REAL, PositionY REAL, PositionZ REAL,
            LandZoneX REAL, LandZoneY REAL, LandZoneZ REAL,
            GatheringType INTEGER,
            ZoneId INTEGER,
            NodeSet INTEGER,
            FOREIGN KEY (RouteNumber) REFERENCES Routes(RouteNumber)
        );"
    };

            foreach (var commandText in commands)
            {
                using var cmd = conn.CreateCommand();
                cmd.CommandText = commandText;
                cmd.ExecuteNonQuery();
            }
        }

        public static void SaveToDatabase(RouteHelpers.RouteEntry config, SqliteConnection conn)
        {
            // Save route info
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"INSERT OR REPLACE INTO Routes (RouteNumber, Expansion, Area, AreaId)
                            VALUES ($num, $exp, $area, $areaId);";
                cmd.Parameters.AddWithValue("$num", config.RouteNumber);
                cmd.Parameters.AddWithValue("$exp", config.Expansion);
                cmd.Parameters.AddWithValue("$area", config.Area);
                cmd.Parameters.AddWithValue("$areaId", config.AreaId);
                cmd.ExecuteNonQuery();
            }

            // Clear old NodeIds and re-insert
            using (var del = conn.CreateCommand())
            {
                del.CommandText = "DELETE FROM RouteNodeIds WHERE RouteNumber = $num;";
                del.Parameters.AddWithValue("$num", config.RouteNumber);
                del.ExecuteNonQuery();
            }

            foreach (var nodeId in config.ListNodeIds)
            {
                using var insert = conn.CreateCommand();
                insert.CommandText = @"INSERT INTO RouteNodeIds (RouteNumber, NodeId)
                               VALUES ($num, $nid);";
                insert.Parameters.AddWithValue("$num", config.RouteNumber);
                insert.Parameters.AddWithValue("$nid", nodeId);
                insert.ExecuteNonQuery();
            }

            // Clear old GatherPoints
            using (var del = conn.CreateCommand())
            {
                del.CommandText = "DELETE FROM GatherPoints WHERE RouteNumber = $num;";
                del.Parameters.AddWithValue("$num", config.RouteNumber);
                del.ExecuteNonQuery();
            }

            foreach (var gp in config.GatherPoints)
            {
                using var insert = conn.CreateCommand();
                insert.CommandText = @"
            INSERT INTO GatherPoints (
                RouteNumber, NodeId,
                PositionX, PositionY, PositionZ,
                LandZoneX, LandZoneY, LandZoneZ,
                GatheringType, ZoneId, NodeSet
            ) VALUES (
                $route, $nodeId,
                $px, $py, $pz,
                $lx, $ly, $lz,
                $gt, $zone, $ns
            );";
                insert.Parameters.AddWithValue("$route", config.RouteNumber);
                insert.Parameters.AddWithValue("$nodeId", gp.NodeId);
                insert.Parameters.AddWithValue("$px", gp.Position.X);
                insert.Parameters.AddWithValue("$py", gp.Position.Y);
                insert.Parameters.AddWithValue("$pz", gp.Position.Z);
                insert.Parameters.AddWithValue("$lx", gp.LandZone.X);
                insert.Parameters.AddWithValue("$ly", gp.LandZone.Y);
                insert.Parameters.AddWithValue("$lz", gp.LandZone.Z);
                insert.Parameters.AddWithValue("$gt", gp.GatheringType);
                insert.Parameters.AddWithValue("$zone", gp.ZoneId);
                insert.Parameters.AddWithValue("$ns", gp.NodeSet);
                insert.ExecuteNonQuery();
            }
        }

        public static void UpdateDatabaseSchema(SqliteConnection conn)
        {
            try
            {
                // Check and update GatherPoints table
                using var cmd = conn.CreateCommand();
                cmd.CommandText = "PRAGMA table_info(GatherPoints);";
                using var reader = cmd.ExecuteReader();
                var existingGatherPointsColumns = new List<string>();
                while (reader.Read())
                {
                    existingGatherPointsColumns.Add(reader.GetString(1)); // Column name is at index 1
                }
                reader.Close();

                // Add columns to GatherPoints if they don't exist
                // Add columns to GatherPoints if they don't exist
                var gatherPointsColumnsToAdd = new[]
                {
                    ("UseRadialPositioning", "INTEGER DEFAULT 0"),
                    ("InnerRadius", "REAL DEFAULT 0.0"),
                    ("OuterRadius", "REAL DEFAULT 5.0"),
                    ("StartAngle", "REAL DEFAULT 0.0"),
                    ("EndAngle", "REAL DEFAULT 360.0"),
                    ("RotationOffset", "REAL DEFAULT 0.0")  // Add this line
                };

                foreach (var (columnName, columnDef) in gatherPointsColumnsToAdd)
                {
                    if (!existingGatherPointsColumns.Contains(columnName))
                    {
                        using var alterCmd = conn.CreateCommand();
                        alterCmd.CommandText = $"ALTER TABLE GatherPoints ADD COLUMN {columnName} {columnDef};";
                        alterCmd.ExecuteNonQuery();
                        Console.WriteLine($"Added column to GatherPoints: {columnName}");
                    }
                }

                // Check and update Routes table
                using var routesCmd = conn.CreateCommand();
                routesCmd.CommandText = "PRAGMA table_info(Routes);";
                using var routesReader = routesCmd.ExecuteReader();
                var existingRoutesColumns = new List<string>();
                while (routesReader.Read())
                {
                    existingRoutesColumns.Add(routesReader.GetString(1)); // Column name is at index 1
                }
                routesReader.Close();

                // Add columns to Routes if they don't exist
                var routesColumnsToAdd = new[]
                {
            ("GatheringType", "INTEGER DEFAULT 0")
        };

                foreach (var (columnName, columnDef) in routesColumnsToAdd)
                {
                    if (!existingRoutesColumns.Contains(columnName))
                    {
                        using var alterCmd = conn.CreateCommand();
                        alterCmd.CommandText = $"ALTER TABLE Routes ADD COLUMN {columnName} {columnDef};";
                        alterCmd.ExecuteNonQuery();
                        Console.WriteLine($"Added column to Routes: {columnName}");
                    }
                }

                Console.WriteLine("Database schema updated successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating schema: {ex.Message}");
            }
        }

        public static string SqlitePath => Path.Combine(
                                           Svc.PluginInterface.ConfigDirectory.FullName,
                                           "Routes.sqlite");
    }
}
