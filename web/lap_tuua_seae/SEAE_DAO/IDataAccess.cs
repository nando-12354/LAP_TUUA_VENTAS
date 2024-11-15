using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace SEAE_DAO
{
    public interface IDataAccess
    {
        Task<IEnumerable<T>> Query<T>(string query, object param = null, CommandType? commandType = null);
        Task Execute(string query, object param = null,  CommandType? commandType = null);
    }
}