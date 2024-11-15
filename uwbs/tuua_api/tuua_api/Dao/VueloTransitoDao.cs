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
    public class VueloTransitoDao
    {
        //obtener vuelos por tipo de vuelo, llegada o salida, nacio o inter

        //para vuelos de llegadas considerar los vuelos de las ultimas 6 horas.

        //para vuelos de salidas considerarlos vuelos de las siguientes 6 horas.

        public async Task<List<TUA_Vuelo_Transito>> obtenerVuelosTransitoActuales(string tipo_operacion, string tipo_vuelo, int ventana_horas) {
            List<TUA_Vuelo_Transito> ls = null;
            string connectionString = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;
            DateTime fechaActual = DateTime.Now;
            
            using (SqlConnection _con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand comando = new SqlCommand("usp_obtener_vuelos_transito_ventana_horas", _con);
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.Add("@fecha_actual", SqlDbType.DateTime).Value = fechaActual;
                    comando.Parameters.Add("@Tip_Vuelo", SqlDbType.Char).Value = tipo_vuelo;
                    comando.Parameters.Add("@Tip_Operacion", SqlDbType.Char).Value = tipo_operacion;
                    comando.Parameters.Add("@ventana_horas", SqlDbType.Int).Value = ventana_horas;
                    _con.Open();
                    IDataReader urReader = await comando.ExecuteReaderAsync();
                    ls = EntidadHelper.ConvertirAEntidades<TUA_Vuelo_Transito>(urReader);

                    return ls;
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

        public async Task<TUA_Vuelo_Transito> obtenerVueloTransitoCodigo(string Cod_Iata, string Nro_Vuelo,DateTime Fch_Prog, string Tip_Operacion)
        {
            TUA_Vuelo_Transito vuelo = null;
            string connectionString = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;

            using (SqlConnection _con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand comando = new SqlCommand("usp_tua_obtener_vuelo_transito_codigo", _con);
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.Add("@Cod_Iata", SqlDbType.VarChar).Value = Cod_Iata;
                    comando.Parameters.Add("@Nro_Vuelo", SqlDbType.VarChar).Value = Nro_Vuelo;
                    comando.Parameters.Add("@Fch_Prog", SqlDbType.DateTime).Value = Fch_Prog;
                    comando.Parameters.Add("@Tip_Operacion", SqlDbType.Char).Value = Tip_Operacion;
                    
                    _con.Open();
                    IDataReader urReader = await comando.ExecuteReaderAsync();
                    vuelo = EntidadHelper.ConvertirAEntidades<TUA_Vuelo_Transito>(urReader).FirstOrDefault();

                    return vuelo;
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



        public async Task<TUA_Vuelo_Transito> obtenerVueloTransitoNumVuelo(string Num_Vuelo, DateTime Fch_Prog, string Tip_Operacion)
        {
            TUA_Vuelo_Transito vuelo = null;
            string connectionString = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;

            using (SqlConnection _con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand comando = new SqlCommand("usp_tua_obtener_vuelo_transito_num_vuelo", _con);
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.Add("@Num_Vuelo", SqlDbType.VarChar).Value = Num_Vuelo;
                    comando.Parameters.Add("@Fch_Prog", SqlDbType.DateTime).Value = Fch_Prog;
                    comando.Parameters.Add("@Tip_Operacion", SqlDbType.Char).Value = Tip_Operacion;

                    _con.Open();
                    IDataReader urReader = await comando.ExecuteReaderAsync();
                    vuelo = EntidadHelper.ConvertirAEntidades<TUA_Vuelo_Transito>(urReader).FirstOrDefault();

                    return vuelo;
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



        //registrar pasajero en transito

        public async Task registrar_pasajero_transito(TUA_Transito pax) {
            string connectionString = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;
            using (SqlConnection _con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand comando = new SqlCommand("usp_insertar_tuua_transito", _con);
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.Add("@Num_Vuelo_Origen", SqlDbType.VarChar).Value = pax.Num_Vuelo_Origen;
                    comando.Parameters.Add("@Fch_Vuelo_Origen", SqlDbType.DateTime).Value = pax.Fch_Vuelo_Origen;
                    comando.Parameters.Add("@Num_Vuelo_Destino", SqlDbType.VarChar).Value = pax.Num_Vuelo_Destino;
                    comando.Parameters.Add("@Fch_Vuelo_Destino", SqlDbType.DateTime).Value = pax.Fch_Vuelo_Destino;

                    comando.Parameters.Add("@Trama_Origen", SqlDbType.VarChar).Value = pax.Trama_Origen;
                    comando.Parameters.Add("@Trama_Destino", SqlDbType.VarChar).Value = pax.Trama_Destino;
                    comando.Parameters.Add("@Cod_Molinete", SqlDbType.VarChar).Value = pax.Cod_Molinete;

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