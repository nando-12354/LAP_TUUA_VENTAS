using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tuua_modulo_salida.Entidades;

namespace tuua_modulo_salida.Dao
{
    public class DAO_TemporalTicket
    {


        public int Ingresar(TemporalTicket objTemporalTicket)
        {
            int intResultado = 0;
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["connStrTUUA"].ConnectionString;

                using (SqlConnection _con = new SqlConnection(connectionString))
                {
                    try
                    {
                        if (_con.State == ConnectionState.Closed)
                            _con.Open();

                        int retVal = 8;
                        SqlCommand comando = new SqlCommand("usp_ingresaticketxcod", _con);
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Parameters.Add(new SqlParameter("@Cod_Numero_Ticket", objTemporalTicket.CodNumeroTicket));
                        var pOut = comando.Parameters.Add(new SqlParameter("@RetVal", retVal));
                        pOut.Direction = ParameterDirection.Output;

                        if (comando.ExecuteNonQuery() != 0)
                        {

                            intResultado = Int32.Parse(pOut.Value.ToString());
                        }

                        return intResultado;
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
            catch (Exception ex1)
            {
                throw ex1;
            }
        }


    }
}
