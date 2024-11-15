using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using tuua_api.Entidades;
using tuua_api.Util;

namespace tuua_api.Dao
{
    public class VueloProgramadoDao
    {
        public async Task<TUA_VueloProgramado> getVueloProgramado(string StrFchVuelo, string strNum_Vuelo)
        {

            string connectionString = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;

            TUA_VueloProgramado vuelo = null;
            using (SqlConnection _con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand comando = new SqlCommand("usp_acs_cns_vueloprogramado_sel", _con);
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.Add("@Fch_Vuelo", SqlDbType.VarChar).Value = StrFchVuelo;
                    comando.Parameters.Add("@Num_Vuelo", SqlDbType.VarChar).Value = strNum_Vuelo;
                
                    _con.Open();
                    IDataReader dr = await comando.ExecuteReaderAsync();
                    vuelo = EntidadHelper.ConvertirAEntidades<TUA_VueloProgramado>(dr).FirstOrDefault();
                }
                catch (Exception ex)
                {

                    throw ex;
                }
                finally
                {
                    _con.Close();
                }
            }

            return vuelo;

        }
    }
}