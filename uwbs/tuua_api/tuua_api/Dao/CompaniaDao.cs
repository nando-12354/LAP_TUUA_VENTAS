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
    public class CompaniaDao
    {
        public async Task<TUA_Compania> getCompaniaIATA(string cod_compania)
        {

            string connectionString = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;

            TUA_Compania compania = null;
            using (SqlConnection _con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand comando = new SqlCommand("select top 1 * from TUA_Compania where Cod_IATA =  '" + cod_compania + "'", _con);
                    _con.Open();
                    IDataReader dr = await comando.ExecuteReaderAsync();
                    compania = EntidadHelper.ConvertirAEntidades<TUA_Compania>(dr).FirstOrDefault();
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

            return compania;

        }

        public async Task<TUA_ModVentaComp> getModalidadCompania(string Cod_Compania, string Nom_Modalidad)
        {

            string connectionString = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;

            TUA_ModVentaComp compania = null;
            using (SqlConnection _con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand comando = new SqlCommand("usp_acs_cns_modventacomp_sel", _con);
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.Add("@Cod_Compania", SqlDbType.VarChar).Value = Cod_Compania;
                    comando.Parameters.Add("@Nom_Modalidad", SqlDbType.VarChar).Value = Nom_Modalidad;

                    _con.Open();
                    IDataReader dr = await comando.ExecuteReaderAsync();
                    compania = EntidadHelper.ConvertirAEntidades<TUA_ModVentaComp>(dr).FirstOrDefault();
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

            return compania;

        }

    }
}