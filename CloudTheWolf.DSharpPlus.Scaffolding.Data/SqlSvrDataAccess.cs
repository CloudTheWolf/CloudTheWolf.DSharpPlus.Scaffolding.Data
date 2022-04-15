using System;
using System.Data;
using CloudTheWolf.DSharpPlus.Scaffolding.Logging;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CloudTheWolf.DSharpPlus.Scaffolding.Data
{
    /// <summary>
    /// Access a SqlSrv Database
    /// </summary>
    public class SqlSvrDataAccess : DataAccess
    {
        private string _sqlConnectionString;
        private SqlConnection _sqlConnection;
        /// <summary>
        /// Load the SqlSrv connection string into a new connection
        /// </summary>
        /// <param name="connStr"></param>
        /// <param name="logger"></param>
        /// <returns>SQL connection <see cref="String"/> for a new <see cref="SqlConnection"/></returns>
        public override string LoadConnectionString(string connStr, ILogger<Logger> logger)
        {
            _sqlConnectionString = connStr;
            return _sqlConnectionString;
        }

        /// <summary>
        /// Execute a SqlSrv Command
        /// </summary>
        /// <param name="sqlCommandString">SQL Command</param>
        /// <param name="logger"></param>
        /// <returns>JSON String of results</returns>
        public override string Request(string sqlCommandString, ILogger<Logger> logger)
        {
            try
            {
                DataTable dt = new DataTable();
                using SqlDataAdapter sda = new SqlDataAdapter(sqlCommandString, _sqlConnection);
                sda.Fill(dt);

                return JsonConvert.SerializeObject(dt, Formatting.Indented);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
