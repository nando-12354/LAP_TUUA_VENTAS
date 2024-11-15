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
    public class TicketDao
    {
        public async Task<TUA_Ticket> obtenerTicket(string sCodNumeroTicket) {
            string connectionString = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;
            TUA_Ticket ticket = null;
            using (SqlConnection _con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand comando = new SqlCommand("usp_acs_cns_ticket_sel", _con);
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.Add("@Cod_Numero_Ticket", SqlDbType.VarChar).Value = sCodNumeroTicket;
               
                    _con.Open();
                    IDataReader dr = await comando.ExecuteReaderAsync();
                    ticket = EntidadHelper.ConvertirAEntidades<TUA_Ticket>(dr).FirstOrDefault();
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
            return ticket;
        }

        public async Task actualizarTicket(TUA_Ticket ticket) {
            string connectionString = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;
           
            using (SqlConnection _con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand comando = new SqlCommand("usp_acs_pcs_ticket_upd", _con);
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.Add("@Cod_Numero_Ticket", SqlDbType.VarChar).Value = ticket.Cod_Numero_Ticket;
                    comando.Parameters.Add("@Cod_Compania", SqlDbType.VarChar).Value= ticket.Cod_Compania;
                    comando.Parameters.Add("@Cod_Venta_Masiva",SqlDbType.VarChar).Value= ticket.Cod_Venta_Masiva;
                    comando.Parameters.Add("@Dsc_Num_Vuelo", SqlDbType.VarChar).Value= ticket.Dsc_Num_Vuelo;
                    comando.Parameters.Add("@Fch_Vuelo", SqlDbType.VarChar).Value= ticket.Fch_Vuelo;
                    comando.Parameters.Add("@Tip_Estado_Actual", SqlDbType.VarChar).Value= ticket.Tip_Estado_Actual;
                    comando.Parameters.Add("@Fch_Creacion", SqlDbType.VarChar).Value= ticket.Fch_Creacion;
                    comando.Parameters.Add("@Cod_Turno", SqlDbType.VarChar).Value= ticket.Cod_Turno;
                    comando.Parameters.Add("@Cod_Usuario_Venta", SqlDbType.VarChar).Value= ticket.Log_Hora_Mod;
                    comando.Parameters.Add("@Imp_Precio", SqlDbType.Decimal).Value = ticket.Imp_Precio;
                    comando.Parameters.Add("@Cod_Moneda", SqlDbType.VarChar).Value = ticket.Cod_Moneda;
                    comando.Parameters.Add("@Fch_Vencimiento", SqlDbType.VarChar).Value = ticket.Fch_Vencimiento;
                    comando.Parameters.Add("@Cod_Modalidad_Venta", SqlDbType.VarChar).Value = ticket.Cod_Modalidad_Venta;
                    comando.Parameters.Add("@Num_Rehabilitaciones", SqlDbType.Int).Value = ticket.Num_Rehabilitaciones;
                    comando.Parameters.Add("@Cod_Tipo_Ticket", SqlDbType.VarChar).Value= ticket.Cod_Tipo_Ticket;
                    comando.Parameters.Add("@Dsc_Motivo", SqlDbType.VarChar).Value= ticket.Dsc_Motivo;
                    comando.Parameters.Add("@Tip_Anula", SqlDbType.VarChar).Value = ticket.Tip_Anulacion;
                    comando.Parameters.Add("@Cod_Pto_Venta", SqlDbType.VarChar).Value = ticket.Cod_Equipo_Mod;
                    comando.Parameters.Add("@Flg_Sincroniza", SqlDbType.VarChar).Value = ticket.Flg_Sincroniza;

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


        //obtiene la lista de tipos de ticket activos
        public async Task<List<TUA_TipoTicket>> obtenerTiposTicket()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;
            List<TUA_TipoTicket> ls = null;
            using (SqlConnection _con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand comando = new SqlCommand("select * from TUA_TipoTicket where Tip_Estado = 1", _con);
                    comando.CommandType = CommandType.Text;
               
                    _con.Open();
                    IDataReader dr = await comando.ExecuteReaderAsync();
                    ls = EntidadHelper.ConvertirAEntidades<TUA_TipoTicket>(dr);
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
            return ls;
        }
    }
}