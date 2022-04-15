using CloudTheWolf.DSharpPlus.Scaffolding.Logging;
using Microsoft.Extensions.Logging;
using System;

namespace CloudTheWolf.DSharpPlus.Scaffolding.Data
{
    /// <summary>
    /// Database Interface
    /// </summary>
    public interface IDatabase
    {
        /// <summary>
        /// Load the SQL connection string into a new connection
        /// </summary>
        /// <param name="name">SQL Command</param>
        /// <param name="logger"></param>
        /// <returns></returns>
        string LoadConnectionString(string name, ILogger<Logger> logger);
        /// <summary>
        /// Execute an SQL Command
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="logger"></param>
        /// <returns></returns>
        string Request(string sql, ILogger<Logger> logger);

        
    }
}
