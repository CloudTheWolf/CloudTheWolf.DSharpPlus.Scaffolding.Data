using System;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Npgsql;

namespace CloudTheWolf.DSharpPlus.Scaffolding.Data
{
    public static class DatabaseFactory
    {
        /// <summary>
        /// Create a Database Object
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns>An instance of <see cref="IDatabase"/></returns>
        /// <exception cref="Exception"></exception>
        public static IDatabase CreateDatabase(IConfiguration configuration)
        {
            string databaseType = configuration.GetValue<string>("Database:type").ToLower();
            switch (databaseType)
            {
                case "mysql":
                    var mysqlConn = new MySqlConnectionStringBuilder
                    {
                        Server = configuration.GetValue<string>("Database.mysql.server"),
                        Port = configuration.GetValue<uint>("Database.mysql.port"),
                        UserID = configuration.GetValue<string>("Database.mysql.userid"),
                        Password = configuration.GetValue<string>("Database.mysql.password"),
                        Database = configuration.GetValue<string>("Database.mysql.database")
                    };
                    return new MySqlDatabase(mysqlConn.ToString());
                case "sqlsrv":
                    var sqlSrvConn = new SqlConnectionStringBuilder
                    {
                        DataSource = $"{configuration.GetValue<string>("Database.sqlsrv.server")},{configuration.GetValue<uint>("Database.sqlsrv.port")}",
                        UserID = configuration.GetValue<string>("Database.sqlsrv.userid"),
                        Password = configuration.GetValue<string>("Database.sqlsrv.password"),
                        InitialCatalog = configuration.GetValue<string>("Database.sqlsrv.database"),
                    };
                    return new SqlServerDatabase(sqlSrvConn.ConnectionString);
                case "pgsql":
                    var pgSrvConn = new NpgsqlConnectionStringBuilder
                    {
                        Host = configuration.GetValue<string>("Database.pgsql.server"),
                        Port = configuration.GetValue<int>("Database.pgsql.port"),
                        Username = configuration.GetValue<string>("Database.pgsql.userid"),
                        Password = configuration.GetValue<string>("Database.pgsql.password"),
                        Database = configuration.GetValue<string>("Database.pgsql.database")
                    };
                    return new PostgreSqlDatabase(pgSrvConn.ToString());
                case "surreal":
                    var surrealServer = configuration.GetValue<string>("Database.surreal.server");
                    var surrealUsername = configuration.GetValue<string>("Database.surreal.userid");
                    var surrealPassword = configuration.GetValue<string>("Database.surreal.password");
                    var surrealNamespace = configuration.GetValue<string>("Database.surreal.namespace");
                    var surrealDatabase = configuration.GetValue<string>("Database.surreal.database");
                    return new SurrealDbDatabase(surrealServer, surrealUsername, surrealPassword, surrealNamespace, surrealDatabase);
                case "sqlite":
                    var sqliteConn = new SqliteConnectionStringBuilder
                    {
                        DataSource = configuration.GetValue<string>("Database.sqlite.datasource"),
                        Mode = configuration.GetValue<SqliteOpenMode>("Database.sqlite.mode"),
                        Password = configuration.GetValue<string>("Database.sqlite.password"),
                    };
                    return new SqliteDatabase(sqliteConn.ToString());
                default:
                    throw new Exception("Unsupported Database Type");
            }
        }
    }
}
