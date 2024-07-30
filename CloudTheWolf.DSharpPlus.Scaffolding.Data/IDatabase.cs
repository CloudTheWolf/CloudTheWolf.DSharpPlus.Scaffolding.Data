using System.Collections.Generic;


namespace CloudTheWolf.DSharpPlus.Scaffolding.Data
{
    public interface IDatabase
    {
        IEnumerable<dynamic> Query(string sql, object param = null);
        int Execute(string sql, object param = null);        
    }
}
