﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Bwa.Core.Data
{
    public class SqlClient : ISqlClient
    {
        public string ConnectionString { get; set; }
        public string Server { get; set; }
        public bool UseIntegratedSecurity { get; set; }
        public string Database { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int CommandTimeout { get; set; } = 120;

        public IEnumerable<Dictionary<string, object>> GetDataRows(string connectionString, string query) =>
            GetDataRows(connectionString, query, Array.Empty<SqlParameter>());

        public IEnumerable<Dictionary<string, object>> GetDataRows(string connectionString, string query, SqlParameter sqlParam) =>
            GetDataRows(connectionString, query, new SqlParameter[] { sqlParam });

        public IEnumerable<Dictionary<string, object>> GetDataRows(string connectionString, string query, IEnumerable<SqlParameter> sqlParameters) =>
            ExecuteSql(connectionString, query, sqlParameters);

        public IEnumerable<Dictionary<string, object>> GetDataRows(string query) =>
            GetDataRows(GetConnectionString(), query);

        public IEnumerable<Dictionary<string, object>> GetDataRows(string query, SqlParameter sqlParam) =>
            GetDataRows(GetConnectionString(), query, sqlParam);

        public IEnumerable<Dictionary<string, object>> GetDataRows(string query, IEnumerable<SqlParameter> sqlParameters) =>
            ExecuteSql(GetConnectionString(), query, sqlParameters);

        private string GetConnectionString()
        {
            if (!string.IsNullOrWhiteSpace(ConnectionString))
                return ConnectionString;

            var connectionString = $"Server={Server};";

            if (!string.IsNullOrWhiteSpace(Database))
                connectionString += $"Database={Database};";
            if (UseIntegratedSecurity)
                connectionString += "Trusted_Connection=True;";
            else
            {
                if (!string.IsNullOrWhiteSpace(Username))
                    connectionString += $"User Id={Username};";
                if (!string.IsNullOrWhiteSpace(Password))
                    connectionString += $"Password={Password};";
            }
            return connectionString;
        }

        private IEnumerable<Dictionary<string, object>> ExecuteSql(string connectionString, string query, IEnumerable<SqlParameter> sqlParameters)
        {
            var connection = new SqlConnection(connectionString);
            var command = new SqlCommand(query, connection)
            {
                CommandTimeout = CommandTimeout
            };

            command.Parameters.AddRange(sqlParameters.ToArray());
            connection.Open();
            var da = new SqlDataAdapter(command);
            var dataTable = new DataTable();
            da.Fill(dataTable);
            connection.Close();
            da.Dispose();
            return dataTable.Select().Select(RowToDictionary);
        }

        private Dictionary<string, object> RowToDictionary(DataRow row)
        {
            var obj = new Dictionary<string, object>();
            foreach (DataColumn column in row.Table.Columns)
                obj.Add(column.ColumnName, row[column]);
            return obj;
        }
    }
}
