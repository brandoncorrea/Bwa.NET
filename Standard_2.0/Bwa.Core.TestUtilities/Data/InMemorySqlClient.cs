using Bwa.Core.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Bwa.Core.TestUtilities.Data
{
    public class InMemorySqlClient : ISqlClient
    {
        public List<(string, string, IEnumerable<SqlParameter>)> Invocations = new List<(string, string, IEnumerable<SqlParameter>)>();
        public IEnumerable<Dictionary<string, object>> ReturnValue = Enumerable.Empty<Dictionary<string, object>>();
        public IEnumerable<Dictionary<string, object>> GetDataRows(string query) =>
            GetDataRows(query, Enumerable.Empty<SqlParameter>());
        public IEnumerable<Dictionary<string, object>> GetDataRows(string query, SqlParameter sqlParam) =>
            GetDataRows(query, new[] { sqlParam });
        public IEnumerable<Dictionary<string, object>> GetDataRows(string query, IEnumerable<SqlParameter> sqlParameters) =>
            GetDataRows(null, query, sqlParameters);
        public IEnumerable<Dictionary<string, object>> GetDataRows(string connectionString, string query) =>
            GetDataRows(connectionString, query, Enumerable.Empty<SqlParameter>());
        public IEnumerable<Dictionary<string, object>> GetDataRows(string connectionString, string query, SqlParameter sqlParam) =>
            GetDataRows(connectionString, query, new[] { sqlParam });
        public IEnumerable<Dictionary<string, object>> GetDataRows(string connectionString, string query, IEnumerable<SqlParameter> sqlParameters)
        {
            Invocations.Add((connectionString, query, sqlParameters));
            return ReturnValue;
        }
    }
}
