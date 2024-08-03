using System;
using System.Collections.Generic;
using SurrealDb.Net;
using CloudTheWolf.DSharpPlus.Scaffolding.Logging;
using Microsoft.Extensions.Logging;

namespace CloudTheWolf.DSharpPlus.Scaffolding.Data
{
    public class SurrealDbDatabase : IDatabase
    {
        private readonly SurrealDbClient _client;

        public SurrealDbDatabase(string serverUrl, string username, string password,string ns, string db)
        {
            var auth = new SurrealDb.Net.Models.Auth.RootAuth() 
            { 
                Username = username, 
                Password = password,                
            };
            _client = new SurrealDbClient(serverUrl);
            _client.SignIn(auth).Wait();
            _client.Use(ns, db).Wait();
        }

        public IEnumerable<dynamic> Query(string sql, object param = null)
        {
            return (IEnumerable<dynamic>)this._client.RawQuery(sql, (IReadOnlyDictionary<string, object>)param);
        }

        public int Execute(string sql, object param = null)
        {
            try
            {
                _client.RawQuery(sql, (IReadOnlyDictionary<string, object>)param).RunSynchronously();
                return 1;
            }
            catch (Exception ex)
            {
                Logger.Log.LogError(ex.Message);
                return 0;
            }
            
        }
    }
}
