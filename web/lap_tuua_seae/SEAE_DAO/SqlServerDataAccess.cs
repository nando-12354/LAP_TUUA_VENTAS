using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;

namespace SEAE_DAO
{
    public class SqlServerDataAccess : IDataAccess
    {
        private string _connectionString;

        public SqlServerDataAccess(string connectionString)
        {
            _connectionString = connectionString;
        }
        
        public async Task<IEnumerable<T>> Query<T>(string query, object param = null, CommandType? commandType = null)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.QueryAsync<T>(query, param,  commandTimeout: 3600, commandType:commandType);
            }
        }

        public async Task Execute(string query, object param = null, CommandType? commandType = null)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(query, param, commandTimeout: 3600, commandType:commandType);
            }
        }
    }
}