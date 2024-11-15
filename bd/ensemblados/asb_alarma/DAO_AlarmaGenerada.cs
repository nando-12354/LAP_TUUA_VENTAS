using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System.Collections.Generic;
using System.Text;
using Microsoft.SqlServer.Server;

namespace LAP.TUUA.ALARMASCLR
{
    class DAO_AlarmaGenerada
    {
        SqlConnection con;

        public DAO_AlarmaGenerada(SqlConnection conex)
        {
            //con = new SqlConnection("context connection = true");
            con = conex;
            //con.Open();
        }

        public CnfgAlarma obtener(string codAlarma, string codModulo)
        {

            string strSQL = "SELECT Cod_Alarma,  Cod_Modulo,  Dsc_Fin_Mensaje,  Xml_Destinatarios,  Log_Usuario_Mod,  Log_Fecha_Mod,  Log_Hora_Mod  "+
                            "FROM TUA_Cnfg_Alarma  "+
                            "WHERE Cod_Alarma= '" + codAlarma + "'AND  " +
                            "Cod_Modulo = '" + codModulo + "' ";
            SqlCommand myCommand = new SqlCommand(strSQL, con);
            SqlDataReader reader = myCommand.ExecuteReader();
            
            CnfgAlarma objConfAlarma = null;

            if(reader.Read())
            {
                objConfAlarma = new CnfgAlarma();
                objConfAlarma.SCodAlarma = Convert.ToString(reader["Cod_Alarma"]);
                objConfAlarma.SCodModulo = Convert.ToString(reader["Cod_Modulo"]);
                objConfAlarma.SDscDestinatarios = Convert.ToString(reader["Xml_Destinatarios"]);
                objConfAlarma.SDscFinMensaje = Convert.ToString(reader["Dsc_Fin_Mensaje"]);
                objConfAlarma.SLogFechaMod = Convert.ToString(reader["Log_Fecha_Mod"]);
                objConfAlarma.SLogHoraMod = Convert.ToString(reader["Log_Hora_Mod"]);
            }

            //con.Close();
            reader.Close();
            return objConfAlarma;
        }


        public AlarmaGenerada obtener(string sIdAlarma)
        {

            string strSQL = "SELECT Cod_Alarma, Cod_Modulo, Dsc_Equipo, Dsc_Subject, Dsc_Body, Log_Usuario_Mod FROM TUA_AlarmaGenerada " +
                            "WHERE Cod_AlarmaGenerada = " + sIdAlarma;
                            
            SqlCommand myCommand = new SqlCommand(strSQL, con);
            SqlDataReader reader = myCommand.ExecuteReader();

            AlarmaGenerada objAlarmaGenerada = null;

            if (reader.Read())
            {
                objAlarmaGenerada = new AlarmaGenerada();
                objAlarmaGenerada.SCodAlarma = Convert.ToString(reader["Cod_Alarma"]);
                objAlarmaGenerada.SCodModulo = Convert.ToString(reader["Cod_Modulo"]);
                objAlarmaGenerada.SDscEquipo = Convert.ToString(reader["Dsc_Equipo"]);
                objAlarmaGenerada.SDscSubject = Convert.ToString(reader["Dsc_Subject"]);
                objAlarmaGenerada.SDscBody = Convert.ToString(reader["Dsc_Body"]);
                objAlarmaGenerada.SLogUsuarioMod = Convert.ToString(reader["Log_Usuario_Mod"]); 

            }

            //con.Close();
            reader.Close();
            return objAlarmaGenerada;
        }

        public bool insertar(AlarmaGenerada objAlarmaGenerada)
        {
            string strSP = "usp_alr_pcs_alarma_gen_ins";
            SqlCommand myCommand = new SqlCommand(strSP, con);
            myCommand.CommandType = CommandType.StoredProcedure;

            myCommand.Parameters.Add(new SqlParameter("@Cod_Alarma", SqlDbType.NChar, 8, "Cod_Alarma"));
            myCommand.Parameters.Add(new SqlParameter("@Cod_Modulo", SqlDbType.NChar, 3, "Cod_Modulo"));

            myCommand.Parameters.Add(new SqlParameter("@Dsc_Equipo", SqlDbType.VarChar, 50, "Dsc_Equipo"));
            myCommand.Parameters.Add(new SqlParameter("@Tip_Importancia", SqlDbType.NChar, 1, "Tip_Importancia"));
            myCommand.Parameters.Add(new SqlParameter("@Dsc_Subject", SqlDbType.VarChar, 15, "Dsc_Subject"));
            myCommand.Parameters.Add(new SqlParameter("@Dsc_Body", SqlDbType.VarChar, 500, "Dsc_Body"));
            myCommand.Parameters.Add(new SqlParameter("@Log_Usuario_Mod", SqlDbType.NChar, 7, "Log_Usuario_Mod"));
            myCommand.Parameters.Add(new SqlParameter("@Cod_Modulo_Mod", SqlDbType.NChar, 3, "Cod_Modulo_Mod"));
            myCommand.Parameters.Add(new SqlParameter("@Cod_SubModulo_Mod", SqlDbType.NChar, 5, "Cod_SubModulo_Mod"));

            myCommand.Parameters[0].Value = objAlarmaGenerada.SCodAlarma;
            myCommand.Parameters[1].Value = objAlarmaGenerada.SCodModulo;
            myCommand.Parameters[2].Value = objAlarmaGenerada.SDscEquipo;
            myCommand.Parameters[3].Value = objAlarmaGenerada.STipImportancia;
            myCommand.Parameters[4].Value = objAlarmaGenerada.SDscSubject;
            myCommand.Parameters[5].Value = objAlarmaGenerada.SDscBody;
            myCommand.Parameters[6].Value = objAlarmaGenerada.SLogUsuarioMod;
            myCommand.Parameters[7].Value = objAlarmaGenerada.SCodModulo;
            myCommand.Parameters[8].Value = objAlarmaGenerada.SCodSubModulo;

            int i = myCommand.ExecuteNonQuery();

            //con.Close();
            return true;
        }

        public string obtenerParametros(string campo)
        {

            string strSP = "usp_cfg_pcs_obtenerparametrosgenerales_sel";
            SqlCommand myCommand = new SqlCommand(strSP, con);
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.Parameters.Add(new SqlParameter("@Identificador", SqlDbType.NChar, 2, "Identificador"));
            myCommand.Parameters[0].Value = campo;
            SqlDataReader reader = myCommand.ExecuteReader();
            string valor = "";

            if (reader.Read())
            {
                valor = Convert.ToString(reader["Valor"]);
            }
            reader.Close();
            //con.Close();
            return valor;
        }

        public AlarmaGenerada ObtenerAlarmaGenerada(string codAlarma, string codModulo, string desEquipo, string userMod)
        {

            string strSP = "usp_alr_pcs_alarma_gen_obtener_sel";
            SqlCommand myCommand = new SqlCommand(strSP, con);
            myCommand.CommandType = CommandType.StoredProcedure;

            myCommand.Parameters.Add(new SqlParameter("@Cod_Alarma", SqlDbType.NChar, 8, "Cod_Alarma"));
            myCommand.Parameters.Add(new SqlParameter("@Cod_Modulo", SqlDbType.NChar, 3, "Cod_Modulo"));
            myCommand.Parameters.Add(new SqlParameter("@Dsc_Equipo", SqlDbType.VarChar, 50, "Dsc_Equipo"));
            myCommand.Parameters.Add(new SqlParameter("@Log_Usuario_Mod", SqlDbType.NChar, 7, "Log_Usuario_Mod"));


            myCommand.Parameters[0].Value = codAlarma;
            myCommand.Parameters[1].Value = codModulo;
            myCommand.Parameters[2].Value = desEquipo;
            myCommand.Parameters[3].Value = userMod;
            //SqlContext.Pipe.Send(strSP + "'" + codAlarma + "'," + "'" + codModulo + "'," + "'" + desEquipo + "'," + "'" + userMod + "'");
            SqlDataReader reader = myCommand.ExecuteReader();
            AlarmaGenerada objAlarma = null;
            
            if (reader.Read())
            {
                objAlarma = new AlarmaGenerada();
                objAlarma.ICodAlarmaGenerada = Convert.ToInt32(reader["Cod_AlarmaGenerada"]);
                objAlarma.SCodAlarma = Convert.ToString(reader["Cod_Alarma"]);
                objAlarma.SCodModulo = Convert.ToString(reader["Cod_Modulo"]);
                objAlarma.SDscEquipo = Convert.ToString(reader["Dsc_Equipo"]);
                objAlarma.DtFchGeneracion  = Convert.ToDateTime(reader["Fch_Generacion"]);
                objAlarma.DtFchActualizacion = Convert.ToDateTime(reader["Fch_Actualizacion"]);

                objAlarma.STipEstado = Convert.ToString(reader["Tip_Estado"]);
                objAlarma.STipImportancia = Convert.ToString(reader["Tip_Importancia"]);
                objAlarma.SFlgEstadoMail = Convert.ToString(reader["Flg_Estado_Mail"]);
                objAlarma.SDscSubject = Convert.ToString(reader["Dsc_Subject"]);
                objAlarma.SDscBody  = Convert.ToString(reader["Dsc_Body"]);
                objAlarma.SLogUsuarioMod = Convert.ToString(reader["Log_Usuario_Mod"]);
                objAlarma.SDscAtencion = Convert.ToString(reader["Dsc_Atencion"]);

            }
            reader.Close();
            //con.Close();
            return objAlarma;
        }

        public bool actualizar(AlarmaGenerada objAlarmaGenerada)
        {
            string strSP = "usp_alr_pcs_alarma_gen_upd_estate";
            SqlCommand myCommand = new SqlCommand(strSP, con);
            myCommand.CommandType = CommandType.StoredProcedure;

            myCommand.Parameters.Add(new SqlParameter("@Cod_AlarmaGenerada", SqlDbType.BigInt, 0, "Cod_AlarmaGenerada"));
            myCommand.Parameters.Add(new SqlParameter("@Flg_Estado_Mail", SqlDbType.NChar, 1, "Flg_Estado_Mail"));
 
            myCommand.Parameters[0].Value = objAlarmaGenerada.ICodAlarmaGenerada;
            myCommand.Parameters[1].Value = objAlarmaGenerada.SFlgEstadoMail;
  
            int i = myCommand.ExecuteNonQuery();
            //con.Close();
            return true;

        }
 

    }
}
