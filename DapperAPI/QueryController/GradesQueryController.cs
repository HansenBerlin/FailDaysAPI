using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using DapperAPI.Database;
using DapperAPI.Models;
using Microsoft.Data.Sqlite;

namespace DapperAPI.QueryController
{
    public class GradesQueryController : IGradesQueryController
    {
        private readonly DatabaseConfig databaseConfig;
 
        public GradesQueryController(DatabaseConfig databaseConfig)
        {
            this.databaseConfig = databaseConfig;
        }

        public async Task<IEnumerable<GradeCategorys>> GetCategorys()
        {
            await using var connection = new SqliteConnection(databaseConfig.Name);
            return await connection.QueryAsync<GradeCategorys>("SELECT * FROM GradeCategorys;");
        }
    }
}