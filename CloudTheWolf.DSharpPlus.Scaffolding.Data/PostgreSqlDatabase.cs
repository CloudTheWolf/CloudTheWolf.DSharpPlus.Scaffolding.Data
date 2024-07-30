using System.Collections.Generic;
using System.Data;
using Npgsql;
using Dapper;

namespace CloudTheWolf.DSharpPlus.Scaffolding.Data
{
    public class PostgreSqlDatabase : IDatabase
    {
        private readonly string _connectionString;

        public PostgreSqlDatabase(string connectionString)
        {
            _connectionString = connectionString;
        }

        private IDbConnection CreateConnection()
        {
            return new NpgsqlConnection(_connectionString);
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
