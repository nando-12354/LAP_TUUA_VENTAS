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
    public class UsuariosDao
    {
        //obtener llaves de destrabe

        public async Task<List<TUA_Destrabe>> getLlavesDestrabe()
        {

            string connectionString = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;

            List<TUA_Destrabe> destrabes = null;
            using (SqlConnection _con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand comando = new SqlCommand("select Cod_Usuario, Cod_Destrabe, Cod_Molinete from TUA_Usuario where Cod_Destrabe is not null and Tip_Estado_Actual = 'V' and Flg_Destrabe = 1", _con);
                    _con.Open();
                    IDataReader dr = await comando.ExecuteReaderAsync();
                    destrabes = EntidadHelper.ConvertirAEntidades<TUA_Destrabe>(dr);
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

            return destrabes;

        }


        public async Task registrarUsoDestrabe(TUA_Destrabe destrabe, TUA_Molinete molinete)
        {

            string connectionString = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;
            using (SqlConnection _con = new SqlConnection(connectionString))
            {
                try
                {

                    /*
                     
                     EXECUTE @RC = [dbo].[usp_acs_pcs_molinetedestrabe_upd] 
                       @Cod_Molinete
                      ,@Cod_Usuario
                      ,@Tip_Resultado
                     
                     */


                    SqlCommand comando = new SqlCommand("usp_acs_pcs_molinetedestrabe_upd", _con);
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.Add("@Cod_Molinete", SqlDbType.VarChar).Value = molinete.Cod_Molinete;
                    comando.Parameters.Add("@Cod_Usuario", SqlDbType.VarChar).Value = destrabe.Cod_Usuario;
                    comando.Parameters.Add("@Tip_Resultado", SqlDbType.VarChar).Value = Define.ID_OPER_DESTRAB_OK;
                    

                    _con.Open();
                    await comando.ExecuteNonQueryAsync();

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

        }





    }
}