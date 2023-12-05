using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudTheWolf.DSharpPlus.Scaffolding.Data
{
    using System.Reflection.Metadata.Ecma335;

    using CloudTheWolf.DSharpPlus.Scaffolding.Logging;

    using Microsoft.Extensions.Logging;

    using SurrealDb.Net;
    using SurrealDb.Net.Models.Auth;

    internal class SurrealDbDataAccess : DataAccess
    {
        private static SurrealDbClient _db;

        public override string LoadConnectionString(string address, ILogger<Logger> logger)
        {
            _db = new SurrealDbClient(address);
            return _db.ToString();
        }

        [Obsolete]
        public override string Request(string sqlCommandString, ILogger<Logger> logger)
        {
            throw new NotImplementedException();
        }

        public async Task<SurrealDbClient> SurrealClientAsync(string username, string password, string database, string nameSpace = "")
        {
            if (string.IsNullOrWhiteSpace(nameSpace))
            {
                nameSpace = database;
            }

            await _db.SignIn(
                new DatabaseAuth
                    {
                        Username = username, Password = password, Database = database, Namespace = nameSpace
                    });
            return _db;
        }
    }
}
