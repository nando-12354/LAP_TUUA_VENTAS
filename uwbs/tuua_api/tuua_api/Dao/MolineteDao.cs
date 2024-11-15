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
    public class MolineteDao
    {
        public async Task<List<TUA_Molinete>> getMolinetes() {

            string connectionString = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;

            List<TUA_Molinete> molinetes = null;
            using (SqlConnection _con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand comando = new SqlCommand("select * from TUA_Molinete", _con);
                    _con.Open();
                    IDataReader dr = await comando.ExecuteReaderAsync();
                    molinetes = EntidadHelper.ConvertirAEntidades<TUA_Molinete>(dr);
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

            return molinetes;

        }

        //obtener grupo por puerta de embarque
        public async Task<TUA_Puerta_Grupo> getGrupoPorNumPuerta(string num_puerta) {
            string connectionString = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;
            TUA_Puerta_Grupo grupo = null;

            using (SqlConnection _con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand comando = new SqlCommand("select  * from TUA_Puerta_Grupo where num_puerta =  '" + num_puerta + "'", _con);
                    _con.Open();
                    IDataReader dr = await comando.ExecuteReaderAsync();
                    grupo = EntidadHelper.ConvertirAEntidades<TUA_Puerta_Grupo>(dr).FirstOrDefault();
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
            return grupo;

        }


        public async Task<TUA_Molinete> getMolineteID(string cod_molinete)
        {

            string connectionString = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;

            TUA_Molinete molinete = null;
            using (SqlConnection _con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand comando = new SqlCommand("select * from TUA_Molinete where Cod_Molinete  = '"+cod_molinete+"'", _con);
                    _con.Open();
                    IDataReader dr = await comando.ExecuteReaderAsync();
                    molinete = EntidadHelper.ConvertirAEntidades<TUA_Molinete>(dr).FirstOrDefault();
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

            return molinete;

        }



    }
}