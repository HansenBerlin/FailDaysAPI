using System;
using System.Collections.Generic;
using System.Globalization;
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

        public async Task<IEnumerable<string>> GetCategorys()
        {
            await using var connection = new SqliteConnection(databaseConfig.Name);
            return await connection.QueryAsync<string>("SELECT Type FROM GradeCategory;");
        }
        
        public async Task<IEnumerable<Grade>> GetGradeObjectPerStudent(int studentId)
        {
            Int64 testInt = studentId;
            await using var connection = new SqliteConnection(databaseConfig.Name);
            return await connection.QueryAsync<Grade>(
                $"SELECT * FROM GradeView WHERE StudentId = {testInt}");
        }

        public async Task CreateGradesForStudent(int studentId)
        {
            await using var connection = new SqliteConnection(databaseConfig.Name);
            IEnumerable<string> categorys = await connection.QueryAsync<string>("SELECT Type FROM GradeCategory;");
            await connection.OpenAsync();
            foreach (string category in categorys)
            {
                SqliteCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO Grade (Number, StudentId, Category) VALUES (@nmb, @stid, @cat)";
                command.Parameters.AddWithValue("nmb", 0);
                command.Parameters.AddWithValue("stid", studentId);
                command.Parameters.AddWithValue("cat", category);
                command.ExecuteNonQuery();
            }

            await connection.CloseAsync();
        }
        
        public async Task UpdateGrade(decimal grade, int gradeId)
        {
            var connection = new SqliteConnection(databaseConfig.Name);
            await connection.OpenAsync();
            SqliteCommand command = connection.CreateCommand();
            command.CommandText = "UPDATE Grade SET Number = :number WHERE Id =:idnr";
            command.Parameters.Add("idnr", SqliteType.Integer).Value = gradeId;
            command.Parameters.Add("number", SqliteType.Real).Value = grade;
            command.ExecuteNonQuery();
            await connection.CloseAsync();
        }
    }
}