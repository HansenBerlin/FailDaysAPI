using Dapper;
using Microsoft.Data.Sqlite;
using System.Linq;

namespace DapperAPI.Database
{
    public class DatabaseBootstrap : IDatabaseBootstrap
    {
        private readonly DatabaseConfig databaseConfig;
 
        public DatabaseBootstrap(DatabaseConfig databaseConfig)
        {
            this.databaseConfig = databaseConfig;
        }
 
        public void Setup()
        {
            
            using var connection = new SqliteConnection(databaseConfig.Name);
 
            var table = connection.Query<string>("SELECT name FROM sqlite_master WHERE type='table' AND name = 'Project';");
            var tableName = table.FirstOrDefault();
            return;
            if (!string.IsNullOrEmpty(tableName) && tableName == "Student")
                return;
 
            connection.Execute("Create Table Student (" +
                               "Name VARCHAR(100) NOT NULL," +
                               "Description VARCHAR(1000) NULL);");
        }
    }
}