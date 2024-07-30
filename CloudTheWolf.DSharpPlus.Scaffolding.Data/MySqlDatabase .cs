using System.Collections.Generic;
using System.Data;
using Dapper;
using MySql.Data.MySqlClient;

namespace CloudTheWolf.DSharpPlus.Scaffolding.Data
{
    public class MySqlDatabase : IDatabase
    {
        private readonly string _connectionString;

        public MySqlDatabase(string connectionString)
        {
            _connectionString = connectionString;
        }

        private IDbConnection CreateConnection()
        {
            return new MySqlConnection(_connectionString);
        }

        public IEnumerable<dynamic> Query(string sql, object param = null)
        {
            using (var dbConnection = CreateConnection())
            {
                dbConnection.Open();
                return dbConnection.Query(sql, param);
            }
        }

        public int Execute(string sql, object param = null)
        {
            using (var dbConnection = CreateConnection())
            {
                dbConnection.Open();
                return dbConnection.Execute(sql, param);
            }
        }
    }
}
