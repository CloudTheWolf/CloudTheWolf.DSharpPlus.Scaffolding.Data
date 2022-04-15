using CloudTheWolf.DSharpPlus.Scaffolding.Logging;
using Microsoft.Extensions.Logging;


namespace CloudTheWolf.DSharpPlus.Scaffolding.Data
{
    /// <summary>
    /// Access a Database
    /// </summary>
    public abstract class DataAccess
    {
        /// <summary>
        /// Load the SQL connection string into a new connection
        /// </summary>
        /// <param name="name">SQL Command</param>
        /// <param name="logger"></param>
        /// <returns></returns>
        public abstract string LoadConnectionString(string name, ILogger<Logger> logger);
        /// <summary>
        /// Execute an SQL Command
        /// </summary>
        /// <param name="sqlCommandString"></param>
        /// <param name="logger"></param>
        /// <returns></returns>
        public abstract string Request(string sqlCommandString, ILogger<Logger> logger);
        
    }
}
