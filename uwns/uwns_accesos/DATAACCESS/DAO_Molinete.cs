using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using LAP.TUUA.ENTIDADES;
using System.Collections;
using System.Data.SqlClient;
using System.Configuration;

namespace LAP.TUUA.DAO
{
    public class DAO_Molinete : DAO_BaseDatos
    {
        #region Fields

        public List<Molinete> objListaMolinete;
        #endregion


        #region Constructors
        public DAO_Molinete(string sConfigSPPath)
            : base(sConfigSPPath)
        {
            objListaMolinete = new List<Molinete>();

        }

        public DAO_Molinete() {

        }

        public DAO_Molinete(Database vhelper, Database vhelperLocal, Hashtable vhtSPConfig)
              : base(vhelper, vhelperLocal, vhtSPConfig)
        {
            objListaMolinete = new List<Molinete>();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Saves a record to the objPrecioTicket table.
        /// </summary>
        public bool insertar(Molinete objMolinete)
        {
            try
            {
                Hashtable hsInsertUSP = (Hashtable)htSPConfig["MSP_INSERTMOLINETE"];
                string sNombreSP = (string)hsInsertUSP["MSP_INSERTMOLINETE"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Molinete"], DbType.String, objMolinete.SCodMolinete);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Dsc_Ip"], DbType.String, objMolinete.SDscIp);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Dsc_Molinete"], DbType.String, objMolinete.SDscMolinete);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Tip_Documento"], DbType.DateTime, objMolinete.STipDocumento);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Tip_Vuelo"], DbType.String, objMolinete.STipVuelo);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Tip_Acceso"], DbType.String, objMolinete.STipAcceso);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Tip_Estado"], DbType.String, objMolinete.STipEstado);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Log_Usuario_Mod"], DbType.String, objMolinete.SLogUsuarioMod);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Log_Fecha_Mod"], DbType.String, objMolinete.DtLogFechaMod);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Log_Hora_Mod"], DbType.String, objMolinete.SLogHoraMod);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Est_Master"], DbType.String, objMolinete.SEstMaster);
                isRegistrado = base.mantenerSP(objCommandWrapper);
                return isRegistrado;
            }
            catch (Exception)
            {
                throw;
            }
        }




        /// <summary>
        /// Updates a record in the objPrecioTicket table.
        /// </summary>
        public bool actualizar(Molinete objMolinete)
        {
            try
            {
                Hashtable hsUpdateUSP = (Hashtable)htSPConfig["MSP_UPDATEMOLINETE"];
                string sNombreSP = (string)hsUpdateUSP["MSP_UPDATEMOLINETE"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Cod_Molinetes"], DbType.String, objMolinete.SCodMolinete);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["hostMolinetes"], DbType.String, objMolinete.SDscIp);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Dsc_Molinetes"], DbType.String, objMolinete.SDscMolinete);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Tip_Documentos"], DbType.String, objMolinete.STipDocumento);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Tip_Vuelos"], DbType.String, objMolinete.STipVuelo);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Tip_Accesos"], DbType.String, objMolinete.STipAcceso);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Tip_Estados"], DbType.String, objMolinete.STipEstado);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Log_Usuario_Mod"], DbType.String, objMolinete.SLogUsuarioMod);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Est_Masters"], DbType.String, objMolinete.SEstMaster);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Dsc_DBName"], DbType.String, objMolinete.SDscDBName);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Dsc_DBUser"], DbType.String, objMolinete.SDscDBUser);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Dsc_DBPassword"], DbType.String, objMolinete.SDscDBPassword);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Can_Molinetes"], DbType.Int32, objMolinete.ICantidad);
                isActualizado = base.mantenerSP(objCommandWrapper);
                return isActualizado;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Updates a record in the objPrecioTicket table.
        /// </summary>
        public bool actualizarTipoVueloMolinete(string Cod_Molinete, string TipoVuelo, string Cod_Usuario, string TipoResultado, string TipoConexion)
        {
            try
            {
                Hashtable hsUpdateUSP = (Hashtable)htSPConfig["TIPOMOLINETE_UPDATE"];
                string sNombreSP = (string)hsUpdateUSP["TIPOMOLINETE_UPDATE"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Cod_Molinete"], DbType.String, Cod_Molinete);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Tip_Molinete"], DbType.String, TipoVuelo);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Cod_Usuario"], DbType.String, Cod_Usuario);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Tip_Resultado"], DbType.String, TipoResultado);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Tip_Conexion"], DbType.String, TipoConexion);
                isActualizado = base.mantenerSPSinAuditoria(objCommandWrapper);
                return isActualizado;
            }
            catch (Exception)
            {
                throw;
            }
        }



        public DataTable listarAllMolinete(string strCodMolinete,string strDscIP)
        {
            DataTable result;
            Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["MSP_LISTARALLMOLINETE"];
            string sNombreSP = (string)hsSelectByIdUSP["MSP_LISTARALLMOLINETE"];
            result = base.ListarDataTableSP(sNombreSP, strCodMolinete, strDscIP);
            return result;
        }

        public Molinete obtenerMolineteCodigo(string cod_molinete) { 
            string connectionString = ConfigurationManager.ConnectionStrings["tuuacnx"].ConnectionString;

            Molinete molinete = null;
            using (SqlConnection _con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand comando = new SqlCommand("select * from TUA_Molinete where Cod_Molinete  = '"+cod_molinete+"'", _con);
                    _con.Open();
                    IDataReader dr = comando.ExecuteReader();
                    if (dr != null) {
                        while (dr.Read())
                        {
                            molinete = poblarMolinete(dr);
                        }
                    }
                    
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

        public  TUA_Puerta_Grupo getGrupoPorNumPuerta(string num_puerta)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["tuuacnx"].ConnectionString;
            TUA_Puerta_Grupo grupo = null;

            using (SqlConnection _con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand comando = new SqlCommand("select  * from TUA_Puerta_Grupo where num_puerta =  '" + num_puerta + "'", _con);
                    _con.Open();
                    IDataReader dr =  comando.ExecuteReader();
                    if (dr != null)
                    {
                        while (dr.Read())
                        {
                            grupo = publarPuertaGrupo(dr);
                        }
                    }
                    
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


        public TUA_Puerta_Grupo publarPuertaGrupo(IDataReader dataReader) {
            TUA_Puerta_Grupo grupo = new TUA_Puerta_Grupo();

            if (dataReader["cod_grupo"] != DBNull.Value) grupo.cod_grupo = (string)dataReader["cod_grupo"];
            if (dataReader["num_puerta"] != DBNull.Value) grupo.num_puerta = (string)dataReader["num_puerta"];
          
            return grupo;
        }

        public Molinete poblarMolinete(IDataReader dataReader) {
            Molinete molinete = new Molinete();
            if (dataReader["Cod_Molinete"] != DBNull.Value) molinete.SCodMolinete = (string)dataReader["Cod_Molinete"];
            if (dataReader["Dsc_Ip"] != DBNull.Value) molinete.SDscIp = (string)dataReader["Dsc_Ip"];
            if (dataReader["Dsc_Molinete"] != DBNull.Value) molinete.SDscMolinete = (string)dataReader["Dsc_Molinete"];
            if (dataReader["Tip_Documento"] != DBNull.Value) molinete.STipDocumento = (string)dataReader["Tip_Documento"];
            if (dataReader["Tip_Vuelo"] != DBNull.Value) molinete.STipVuelo = (string)dataReader["Tip_Vuelo"];
            if (dataReader["Tip_Acceso"] != DBNull.Value) molinete.STipAcceso = (string)dataReader["Tip_Acceso"];
            if (dataReader["Tip_Estado"] != DBNull.Value) molinete.STipEstado = (string)dataReader["Tip_Estado"];
            if (dataReader["Log_Usuario_Mod"] != DBNull.Value) molinete.SLogUsuarioMod = (string)dataReader["Log_Usuario_Mod"];
            if (dataReader["Log_Fecha_Mod"] != DBNull.Value) molinete.DtLogFechaMod = (string)dataReader["Log_Fecha_Mod"];
            if (dataReader["Log_Hora_Mod"] != DBNull.Value) molinete.SLogHoraMod = (string)dataReader["Log_Hora_Mod"];
            if (dataReader["Est_Master"] != DBNull.Value) molinete.SEstMaster = (string)dataReader["Est_Master"];
            if (dataReader["Dsc_DBName"] != DBNull.Value) molinete.SDscDBName = (string)dataReader["Dsc_DBName"];
            if (dataReader["Dsc_DBUser"] != DBNull.Value) molinete.SDscDBUser = (string)dataReader["Dsc_DBUser"];
            if (dataReader["Dsc_DBPassword"] != DBNull.Value) molinete.SDscDBPassword = (string)dataReader["Dsc_DBPassword"];
            if (dataReader["cod_grupo"] != DBNull.Value) molinete.Cod_grupo = (string)dataReader["cod_grupo"];
            return molinete;

        }
        #endregion
    }
}
