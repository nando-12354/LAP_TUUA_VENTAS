using System.Configuration;

namespace SEAE_DAO
{
    public static class DataAccessFactory
    {
        public static IDataAccess GetDataAccess(string motor = null)
        {
            
            string connectionString = ConfigurationManager.ConnectionStrings["cnnstr"].ConnectionString;
            IDataAccess dataAccess = new SqlServerDataAccess(connectionString);
            
           
            return dataAccess;
        }
    
    }
}