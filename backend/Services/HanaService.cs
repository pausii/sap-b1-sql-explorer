// using Sap.Data.Hana;
using System.Data;
using System.Data.Odbc;

namespace backend.Services
{
    public class HanaService
    {
        private readonly IConfiguration _config;

        public HanaService(IConfiguration config)
        {
            _config = config;
        }

        public List<Dictionary<string, object>> ExecuteQuery(string query)
        {
            var result = new List<Dictionary<string, object>>();
            var connectionString = _config.GetConnectionString("HanaDb");

            using var conn = new OdbcConnection(connectionString);
            conn.Open();

            using var cmd = new OdbcCommand(query, conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var row = new Dictionary<string, object>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    // Console.WriteLine( reader.GetValue(i).GetType());
                    row[reader.GetName(i)] = reader.GetValue(i);
                }
                result.Add(row);
            }

            return result;
        }

        public List<Dictionary<string, object>> GetTables(string schema)
        {
            var tables = new List<Dictionary<string, object>>();
            var connectionString = _config.GetConnectionString("HanaDb");

            using var conn = new OdbcConnection(connectionString);
            conn.Open();

            var query = $@"
        SELECT SCHEMA_NAME, TABLE_NAME, TABLE_OID, FIXED_PART_SIZE, IS_LOGGED, 
               IS_SYSTEM_TABLE, IS_COLUMN_TABLE, TABLE_TYPE, IS_INSERT_ONLY, 
               IS_TENANT_SHARED_DATA, IS_TENANT_SHARED_METADATA, SESSION_TYPE, 
               IS_TEMPORARY, TEMPORARY_TABLE_TYPE, IS_USER_DEFINED_TYPE, 
               HAS_PRIMARY_KEY, IS_REPLICA, HAS_STRUCTURED_PRIVILEGE_CHECK, 
               IS_SERIES_TABLE 
        FROM SYS.TABLES 
        WHERE SCHEMA_NAME = ? 
        -- LIMIT 10";

            using var cmd = new OdbcCommand(query, conn);
            cmd.Parameters.AddWithValue("@schema", schema); // ✅ Gunakan parameterized query

            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var row = new Dictionary<string, object>();

                for (int i = 0; i < reader.FieldCount; i++)
                {
                    row[reader.GetName(i)] = reader.GetValue(i);
                }

                tables.Add(row);
            }

            return tables; // ✅ return yang benar
        }

        public List<Dictionary<string, object>> GetColumnNames(string schema, string tableName)
        {
            var columns = new List<Dictionary<string, object>>();
            var connectionString = _config.GetConnectionString("HanaDb");
            using var conn = new OdbcConnection(connectionString);
            conn.Open();
            var query = @"
                SELECT 
                    *
                    -- COLUMN_NAME, DATA_TYPE_NAME, IS_NULLABLE, LENGTH, SCALE, DEFAULT_VALUE
                FROM TABLE_COLUMNS
                WHERE SCHEMA_NAME = ? AND TABLE_NAME = ?";


            using var cmd = new OdbcCommand(query, conn);
            cmd.Parameters.AddWithValue(null, schema);      // urutan pertama -> untuk SCHEMA_NAME
            cmd.Parameters.AddWithValue(null, tableName);   // urutan kedua  -> untuk TABLE_NAME

            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var column = new Dictionary<string, object>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    column[reader.GetName(i)] = reader.GetValue(i);
                }
                columns.Add(column);
            }

            return columns;
        }

    }
}
