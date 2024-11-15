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
    public class BoardingDao
    {
        public async Task<TUA_BoardingBcbp> getBoarding(string strCod_Compania, string strNum_Vuelo, string strFechVuelo, string strNumeroAsiento,
                                      string strNomPasajero, string strNumCheckin)
        {

            string connectionString = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;

            TUA_BoardingBcbp boarding = null;
            using (SqlConnection _con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand comando = new SqlCommand("usp_acs_cns_boardingbcbp_sel", _con);
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.Add("@Cod_Compania", SqlDbType.VarChar).Value = strCod_Compania;
                    comando.Parameters.Add("@Num_Vuelo", SqlDbType.VarChar).Value = strNum_Vuelo;
                    comando.Parameters.Add("@Fch_Vuelo", SqlDbType.VarChar).Value = strFechVuelo;
                    comando.Parameters.Add("@Num_Asiento", SqlDbType.VarChar).Value = strNumeroAsiento;
                    comando.Parameters.Add("@Nom_Pasajero", SqlDbType.VarChar).Value = strNomPasajero;
                    comando.Parameters.Add("@Tip_Estado", SqlDbType.VarChar).Value = null;
                    comando.Parameters.Add("@Cod_Unico_Bcbp", SqlDbType.VarChar).Value = null;
                    comando.Parameters.Add("@Num_Secuencial_Bcbp", SqlDbType.VarChar).Value = null;
                    comando.Parameters.Add("@Num_Checkin", SqlDbType.VarChar).Value = strNumCheckin;

                    _con.Open();
                    IDataReader dr = await comando.ExecuteReaderAsync();
                    boarding = EntidadHelper.ConvertirAEntidades<TUA_BoardingBcbp>(dr).FirstOrDefault();
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

            return boarding;

        }

        public async Task registrarBoarding(TUA_BoardingBcbp boarding, TUA_Molinete molinete) {
            string connectionString = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;
            using (SqlConnection _con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand comando = new SqlCommand("usp_acs_pcs_boardingbcbp_ins", _con);
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.Add("@Cod_Compania", SqlDbType.VarChar).Value = boarding.Cod_Compania;
                    comando.Parameters.Add("@Num_Vuelo", SqlDbType.VarChar).Value = boarding.Num_Vuelo;
                    comando.Parameters.Add("@Fch_Vuelo", SqlDbType.VarChar).Value = boarding.Fch_Vuelo;
                    comando.Parameters.Add("@Num_Asiento", SqlDbType.VarChar).Value = boarding.Num_Asiento;
                    comando.Parameters.Add("@Nom_Pasajero", SqlDbType.VarChar).Value = boarding.Nom_Pasajero;
                    comando.Parameters.Add("@Dsc_Trama_Bcbp", SqlDbType.VarChar).Value = boarding.Dsc_Trama_Bcbp;
                    comando.Parameters.Add("@Log_Usuario_Mod", SqlDbType.VarChar).Value = boarding.Log_Usuario_Mod;
                    comando.Parameters.Add("@Log_Fecha_Mod", SqlDbType.VarChar).Value = boarding.Log_Fecha_Mod;
                    comando.Parameters.Add("@Log_Hora_Mod", SqlDbType.VarChar).Value = boarding.Log_Hora_Mod;
                    comando.Parameters.Add("@Tip_Ingreso", SqlDbType.VarChar).Value = boarding.Tip_Ingreso;
                    comando.Parameters.Add("@Tip_Estado", SqlDbType.VarChar).Value = boarding.Tip_Estado;
                    comando.Parameters.Add("@Cod_Equipo_Mod", SqlDbType.VarChar).Value = molinete.Cod_Molinete;
                    comando.Parameters.Add("@Dsc_Boarding_Estado", SqlDbType.VarChar).Value = "USADO";
                    comando.Parameters.Add("@Cod_Modulo_Mod", SqlDbType.VarChar).Value = null;// no se usa
                    comando.Parameters.Add("@Cod_SubModulo_Mod", SqlDbType.VarChar).Value = null;// no se usa
                    comando.Parameters.Add("@Cod_Rol", SqlDbType.VarChar).Value = null;// no se usa
                    comando.Parameters.Add("@Cod_Unico_Bcbp", SqlDbType.VarChar).Value = null;// no se usa
                    comando.Parameters.Add("@Cod_Unico_Bcbp_Rel", SqlDbType.VarChar).Value = null;// no se usa
                    comando.Parameters.Add("@Flg_Sincroniza", SqlDbType.VarChar).Value = '0';// no se usa
                    comando.Parameters.Add("@Tip_Pasajero", SqlDbType.VarChar).Value = boarding.Tip_Pasajero;
                    comando.Parameters.Add("@Tip_Vuelo", SqlDbType.VarChar).Value = boarding.Tip_Vuelo;
                    comando.Parameters.Add("@Tip_Transferencia", SqlDbType.VarChar).Value = boarding.Tip_Trasbordo;
                    comando.Parameters.Add("@Dsc_Destino", SqlDbType.VarChar).Value = boarding.Dsc_Destino;
                    comando.Parameters.Add("@Cod_Eticket", SqlDbType.VarChar).Value = boarding.Cod_Eticket;
                    comando.Parameters.Add("@Nro_Boarding", SqlDbType.VarChar).Value = boarding.Num_Checkin;
                    comando.Parameters.Add("@Flg_WSError", SqlDbType.VarChar).Value = null;// no se usa
                    comando.Parameters.Add("@Flg_Incluye_Tuua", SqlDbType.VarChar).Value = null; // no se usa
                    comando.Parameters.Add("@Dsc_Observacion", SqlDbType.VarChar).Value = null;// no se usa
                    comando.Parameters.Add("@Num_Airline_Code", SqlDbType.VarChar).Value = null; // no se usa
                    comando.Parameters.Add("@Num_Document_Form", SqlDbType.VarChar).Value = null; // no se usa
                    comando.Parameters.Add("@Num_Checkin", SqlDbType.VarChar).Value = boarding.Num_Checkin;


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

        public async Task registrarPaxTransito(TUA_BoardingBcbp boarding, TUA_Molinete molinete) {

            string connectionString = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;
            using (SqlConnection _con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand comando = new SqlCommand("usp_tuua_insertar_bp_transito", _con);
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.Add("@num_vuelo", SqlDbType.VarChar).Value = boarding.Dsc_Trama_Bcbp.Trim().Substring(39, 5).Trim(); 
                    comando.Parameters.Add("@fecVueloNum", SqlDbType.VarChar).Value = boarding.Dsc_Trama_Bcbp.Trim().Substring(44, 3).Trim();
                    comando.Parameters.Add("@num_asiento", SqlDbType.VarChar).Value = boarding.Num_Asiento;
                    comando.Parameters.Add("@num_checkin", SqlDbType.VarChar).Value = boarding.Num_Checkin;
                    comando.Parameters.Add("@nom_pasajero", SqlDbType.VarChar).Value = boarding.Nom_Pasajero;
                    comando.Parameters.Add("@dsc_trama_bcbp", SqlDbType.VarChar).Value = boarding.Dsc_Trama_Bcbp;
                    comando.Parameters.Add("@dsc_destino", SqlDbType.VarChar).Value = boarding.Dsc_Destino;
                    comando.Parameters.Add("@cod_iata", SqlDbType.VarChar).Value = boarding.Dsc_Trama_Bcbp.Trim().Substring(36,3).Trim();
                    comando.Parameters.Add("@fch_registro", SqlDbType.DateTime).Value = DateTime.Now;
                    comando.Parameters.Add("@tip_vuelo", SqlDbType.VarChar).Value = boarding.Tip_Vuelo;
                    comando.Parameters.Add("@tip_status_pax", SqlDbType.VarChar).Value = boarding.Dsc_Trama_Bcbp.Trim().Substring(57, 1).Trim();
                    comando.Parameters.Add("@cod_molinete", SqlDbType.VarChar).Value = molinete.Cod_Molinete;
                    comando.Parameters.Add("@dsc_archivo", SqlDbType.VarChar).Value = "TUUA_API";
                    comando.Parameters.Add("@usuario", SqlDbType.VarChar).Value = "USR_SVC";
                    
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