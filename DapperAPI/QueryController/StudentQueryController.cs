using System;
using Dapper;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DapperAPI.Database;
using DapperAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DapperAPI.QueryController
{
    public class StudentQueryController : IStudentQueryController
    {
        private readonly DatabaseConfig databaseConfig;
 
        public StudentQueryController(DatabaseConfig databaseConfig)
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
        
        public async Task<IEnumerable<Grade>> GetGradeObjectPerStudent(int studentId)
        {
            Int64 testInt = studentId;
            await using var connection = new SqliteConnection(databaseConfig.Name);
            var test = await connection.QueryAsync<Grade>(
                $"SELECT * FROM Grade WHERE StudentId = {testInt}");
            return test;
            //SELECT CAST(field123 AS REAL) AS field123 FROM ...
        }
        
        public async Task Create(Student student)
        {
            await using var connection = new SqliteConnection(databaseConfig.Name);
            //double test = await connection.ExecuteAsync("SELECT AVG(Number)FROM Grade WHERE StudentId = @MatNr");
            await connection.ExecuteAsync("INSERT INTO Student (MatNr, CourseId, FirstName, LastName)" +
                                          "VALUES (@MatNr, @CourseId, @FirstName, @LastName);", student);
        }
        
        public async Task DeleteAllRows(string tableName)
        {
            await using var connection = new SqliteConnection(databaseConfig.Name);
            connection.Open();
            var commandDeleteStudents = connection.CreateCommand();
            commandDeleteStudents.CommandText = $"DELETE FROM {tableName}";
            await commandDeleteStudents.ExecuteNonQueryAsync();
            connection.Close();
        }
    }
}