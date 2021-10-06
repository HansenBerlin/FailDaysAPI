using System.Threading.Tasks;
using Dapper;
using DapperAPI.Database;
using Microsoft.Data.Sqlite;

namespace DapperAPI.ProductMaster
{
    public class StudentRepository : IStudentRepository
    {
        private readonly DatabaseConfig databaseConfig;
        
        public StudentRepository(DatabaseConfig databaseConfig)
        {
            this.databaseConfig = databaseConfig;
        }
 
        public async Task Create(Student student)
        {
            await using var connection = new SqliteConnection(databaseConfig.Name);
            //double test = await connection.ExecuteAsync("SELECT AVG(Number)FROM Grade WHERE StudentId = @MatNr");
            await connection.ExecuteAsync("INSERT INTO Student (MatNr, ClassId, FirstName, LastName)" +
                                          "VALUES (@MatNr, @ClassId, @FirstName, @LastName);", student);
        }
    }
}