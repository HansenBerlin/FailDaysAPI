using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using DapperAPI.Database;
using DapperAPI.Utilities;
using Microsoft.Data.Sqlite;

namespace DapperAPI.QueryController
{
    public class DataBasePopulator : IDatabasePopulator
    {
        private readonly IRandomObjectCreator randomObject;
        private readonly DatabaseConfig databaseConfig;
        private readonly IStudentQueryController studentQueryController;

        public DataBasePopulator(DatabaseConfig databaseConfig, IRandomObjectCreator randomObject, IStudentQueryController studentQueryController)
        {
            this.databaseConfig = databaseConfig;
            this.randomObject = randomObject;
            this.studentQueryController = studentQueryController;
        }

        public async Task CreateStudents()
        {
            studentQueryController.DeleteAllRows("Student");
            studentQueryController.DeleteAllRows("Grade");
            studentQueryController.DeleteAllRows("Team");
            studentQueryController.DeleteAllRows("Teammember");
            studentQueryController.DeleteAllRows("Teamarbeit");

            CreateTeam();
            int lastIndex = GetLastTeamIndex().Result;
            int firstWorksheetTeam = 0;
            int projectTeam = 0;
            
            for (int i = 0; i < 36000; i++)
            {
                var student = randomObject.CreateRandomStudent();
                studentQueryController.Create(student);
                CreateGradesForStudent(student.MatNr);
                if (i % 3 == 0)
                {
                    firstWorksheetTeam++;
                    CreateTeam();
                    CreateTeamWork(firstWorksheetTeam + lastIndex, "Arbeitsblatt 1", "leer");
                }
                if (i % 6 == 0)
                {
                    projectTeam++;
                    //string subject = randomObject.Subject(projectTeam - 1);
                    CreateTeam();
                    CreateTeamWork(projectTeam + lastIndex, "Projektarbeit", "subject");
                }

                CreateTeamMember(firstWorksheetTeam + lastIndex, student.MatNr);
                CreateTeamMember(projectTeam + lastIndex, student.MatNr);
            }
        }
        
        private async Task CreateGradesForStudent(int studentId)
        {
            await using var connection = new SqliteConnection(databaseConfig.Name);
            IEnumerable<string> categorys = await connection.QueryAsync<string>("SELECT Type FROM GradeCategory;");
            await connection.OpenAsync();
            foreach (string category in categorys)
            {
                SqliteCommand command = connection.CreateCommand();
                //command.CommandText = "INSERT INTO Grade (Number, StudentId, Category)";
                command.CommandText = "INSERT INTO Grade (Number, StudentId, Category) VALUES (@nmb, @stid, @cat)";
                command.Parameters.AddWithValue("nmb", randomObject.GetRandomGrade());
                command.Parameters.AddWithValue("stid", studentId);
                command.Parameters.AddWithValue("cat", category);
                command.ExecuteNonQuery();
            }

            //await connection.CloseAsync();
        }
        
        private async Task CreateTeam()
        {
            await using var connection = new SqliteConnection(databaseConfig.Name);
            await connection.OpenAsync();
            SqliteCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO Team (Id) VALUES (null)";
            //command.Parameters.AddWithValue("ID", null);
            command.ExecuteNonQuery();
            //await connection.CloseAsync();
        }
        
        private async Task CreateTeamMember(int teamId, int studentId)
        {
            await using var connection = new SqliteConnection(databaseConfig.Name);
            await connection.OpenAsync();
            SqliteCommand command = connection.CreateCommand();
            //command.CommandText = "INSERT INTO Grade (Number, StudentId, Category)";
            command.CommandText = "INSERT INTO Teammember (MemberId, TeamId) VALUES (@stId, @tmId)";
            command.Parameters.AddWithValue("stId", studentId);
            command.Parameters.AddWithValue("tmId", teamId);
            command.ExecuteNonQuery();
            //await connection.CloseAsync();
        }
        
        private async Task CreateTeamWork(int teamId, string category, string subject)
        {
            await using var connection = new SqliteConnection(databaseConfig.Name);
            await connection.OpenAsync();
            SqliteCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO Teamarbeiten (Kategorie, TeamId, Thema) VALUES (@cat, @tmId, @sbj)";
            command.Parameters.AddWithValue("cat", category);
            command.Parameters.AddWithValue("tmId", teamId);
            command.Parameters.AddWithValue("sbj", subject);
            command.ExecuteNonQuery();
            //await connection.CloseAsync();
        }
        
        private async Task<int> GetLastTeamIndex()
        {
            await using var connection = new SqliteConnection(databaseConfig.Name);
            var test = await connection.QueryAsync<int>(
                "SELECT * FROM Team ORDER BY id DESC LIMIT 1");
            return test.AsList()[0];
        }
        
        
    }
}