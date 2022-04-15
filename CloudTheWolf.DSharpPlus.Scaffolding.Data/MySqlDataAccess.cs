using System;
using System.Data;
using MySqlConnector;
using Newtonsoft.Json;
using CloudTheWolf.DSharpPlus.Scaffolding.Logging;
using Microsoft.Extensions.Logging;

namespace CloudTheWolf.DSharpPlus.Scaffolding.Data
{
    /// <summary>
    /// Access a MySql Database
    /// </summary>
    public class MySqlDataAccess : DataAccess
    {
        private MySqlConnection _sqlConnection;
        /// <summary>
        /// Load the MySql connection string into a new connection
        /// </summary>
        /// <param name="connStr"></param>
        /// <param name="logger"></param>
        /// <returns>SQL connection <see cref="String"/> for a new <see cref="MySqlConnection"/></returns>
        public override string LoadConnectionString(string connStr, ILogger<Logger> logger)
        {
            _sqlConnection = new MySqlConnection(connStr);
            return _sqlConnection.ConnectionString;
        }
        /// <summary>
        /// Execute a MySQL Command
        /// </summary>
        /// <param name="sqlCommandString">SQL Command</param>
        /// <param name="logger"></param>
        /// <returns>JSON String of results</returns>
        public override string Request(string sqlCommandString, ILogger<Logger> logger)
        {            
            try
            {
                DbConnect(logger);
                DataTable dt = new DataTable();
                using MySqlDataAdapter sda = new MySqlDataAdapter(sqlCommandString, _sqlConnection);
                sda.Fill(dt);

                return JsonConvert.SerializeObject(dt, Formatting.Indented);

            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                throw;
            }
        }

        private void DbConnect(ILogger<Logger> logger)
        {
            try
            {
                _sqlConnection.Open();
                logger.LogInformation("SQL Connection Opened");
                _sqlConnection.Close();
            }
            catch (MySqlException e)
            {
                logger.LogError(e.Message);
                throw;
            }


        }

    }
}
