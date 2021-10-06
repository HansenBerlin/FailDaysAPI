using Dapper;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Threading.Tasks;
using DapperAPI.Database;
using Microsoft.AspNetCore.Mvc;

namespace DapperAPI.ProductMaster
{
    public class StudentProvider : IStudentProvider
    {
        private readonly DatabaseConfig databaseConfig;
 
        public StudentProvider(DatabaseConfig databaseConfig)
        {
            this.databaseConfig = databaseConfig;
        }

        public async Task<IEnumerable<Student>> GetDatasets()
        {
            await using var connection = new SqliteConnection(databaseConfig.Name);
            return await connection.QueryAsync<Student>("SELECT * FROM Student;");
        }

        public async Task<double> GetGrade([FromQuery(Name = "studentId")] int studentId)
        {
            await using var connection = new SqliteConnection(databaseConfig.Name);
            var test = await connection.QueryAsync<double>(
                $"SELECT AVG(Number)FROM Grade WHERE StudentId = {studentId.ToString()}");
            return test.AsList()[0];
        }
    }
}