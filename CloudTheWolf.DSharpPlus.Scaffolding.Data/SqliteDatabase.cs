using System.Collections.Generic;
using System.Data;
using Dapper;
using Microsoft.Data.Sqlite;

namespace CloudTheWolf.DSharpPlus.Scaffolding.Data
{
    public class SqliteDatabase : IDatabase
    {
        private readonly string _connectionString;

        public SqliteDatabase(string connectionString)
        {
            _connectionString = connectionString;
        }

        private IDbConnection CreateConnection()
        {
            return new SqliteConnection(_connectionString);
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
