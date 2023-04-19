using System.Collections.Generic;
using System.Data.SqlClient;

namespace Bwa.Core.Data
{
    public interface ISqlClient
    {
        IEnumerable<Dictionary<string, object>> GetDataRows(string query);
        IEnumerable<Dictionary<string, object>> GetDataRows(string query, SqlParameter sqlParam);
        IEnumerable<Dictionary<string, object>> GetDataRows(string query, IEnumerable<SqlParameter> sqlParameters);
        IEnumerable<Dictionary<string, object>> GetDataRows(string connectionString, string query);
        IEnumerable < Dictionary<string, object> > GetDataRows(string connectionString, string query, SqlParameter sqlParam);
        IEnumerable<Dictionary<string, object>> GetDataRows(string connectionString, string query, IEnumerable<SqlParameter> sqlParameters);
    }
}
