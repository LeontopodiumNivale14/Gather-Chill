using Microsoft.Data.Sqlite;

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

        public static void SaveToDatabase(RouteConfig config, SqliteConnection conn)
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
    }
}
