using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using DapperAPI.Database;
using DapperAPI.Models;
using Microsoft.Data.Sqlite;

namespace DapperAPI.QueryController
{
    public class CourseQueryController : ICourseQueryController
    {
        private readonly DatabaseConfig databaseConfig;
 
        public CourseQueryController(DatabaseConfig databaseConfig)
        {
            this.databaseConfig = databaseConfig;
        }
        
        public async Task<IEnumerable<Course>> GetAll()
        {
            await using var connection = new SqliteConnection(databaseConfig.Name);
            return await connection.QueryAsync<Course>("SELECT * FROM Course;");
        }
    }
}