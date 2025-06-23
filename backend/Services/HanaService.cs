using Sap.Data.Hana;
using System.Data;

namespace backend.Services
{
    public class HanaService
    {
        private readonly IConfiguration _config;

        public HanaService(IConfiguration config)
        {
            _config = config;
        }

        public DataTable ExecuteQuery(string query)
        {
            var connectionString = _config.GetConnectionString("HanaDb");

            using var conn = new HanaConnection(connectionString);
            conn.Open();

            using var cmd = new HanaCommand(query, conn);
            using var reader = cmd.ExecuteReader();

            var result = new DataTable();
            result.Load(reader);

            return result;
        }
    }
}
