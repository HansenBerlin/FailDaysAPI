using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Dapper;
using DapperAPI.Database;
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

        public async Task<IEnumerable<string>> GetCategorys()
        {
            await using var connection = new SqliteConnection(databaseConfig.Name);
            return await connection.QueryAsync<string>("SELECT Type FROM GradeCategory;");
        }

        public async Task CreateGradesForStudent(int studentId)
        {
            await using var connection = new SqliteConnection(databaseConfig.Name);
            IEnumerable<string> categorys = await connection.QueryAsync<string>("SELECT Type FROM GradeCategory;");
            connection.Open();
            foreach (string category in categorys)
            {
                SqliteCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO Grade (Number, StudentId, Category)";
                command.CommandText = "INSERT INTO Grade (Number, StudentId, Category) VALUES (@nmb, @stid, @cat)";
                command.Parameters.AddWithValue("nmb", 0.0);
                command.Parameters.AddWithValue("stid", studentId);
                command.Parameters.AddWithValue("cat", category);
                command.ExecuteNonQuery();
            }
        }
        
        public async Task UpdateGrade(string grade, int gradeId)
        {
            double parseString = double.Parse(grade, CultureInfo.CurrentCulture);
            parseString = Math.Round(parseString, 1);
            var connection = new SqliteConnection(databaseConfig.Name);
            connection.Open();
            SqliteCommand command = connection.CreateCommand();
            command.CommandText = "UPDATE Grade SET Number = :number WHERE Id =:idnr";
            command.Parameters.Add("idnr", SqliteType.Integer).Value = gradeId;
            command.Parameters.Add("number", SqliteType.Real).Value = parseString;
            command.ExecuteNonQuery();
        }
    }
}