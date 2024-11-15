using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using LAP.TUUA.ENTIDADES;
using System.Collections;

namespace LAP.TUUA.DAO
{
    public class DAO_Compania : DAO_BaseDatos
    {
        #region Fields

        public List<Compania> objListaCompania;
        #endregion

        #region Constructors
        public DAO_Compania(string sConfigSPPath)
            : base(sConfigSPPath)
        {
            objListaCompania = new List<Compania>();

        }
        public DAO_Compania(Database vhelper, Database vhelperLocal, Hashtable vhtSPConfig)
              : base(vhelper, vhelperLocal, vhtSPConfig)
        {
              objListaCompania = new List<Compania>();

        }

        public DAO_Compania(Hashtable htSPConfig)
            : base(htSPConfig)
        {
            objListaCompania = new List<Compania>();
        }
        #endregion

        #region Methods

        /// <summary>
        /// Saves a record to the objPrecioTicket table.
        /// </summary>
        public bool insertar(Compania objCompania)
        {
            try
            {
                Hashtable hsInsertUSP = (Hashtable)htSPConfig["CSP_INSERT"];
                string sNombreSP = (string)hsInsertUSP["CSP_INSERT"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Tip_Compania"], DbType.String, objCompania.STipCompania);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Dsc_Compania"], DbType.String, objCompania.SDscCompania);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Aerolinea"], DbType.String, objCompania.SCodAerolinea);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_SAP"], DbType.String, objCompania.SCodSAP);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_OACI"], DbType.String, objCompania.SCodOACI);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_IATA"], DbType.String, objCompania.SCodIATA);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Especial_Compania"], DbType.String, objCompania.SCodEspecialCompania);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Dsc_Ruc"], DbType.String, objCompania.SDscRuc);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Log_Usuario_Mod"], DbType.String, objCompania.SLogUsuarioMod);
                isRegistrado = base.mantenerSP(objCommandWrapper);
                return isRegistrado;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool insertarCrit(Compania objCompania)
        {
            try
            {
                //Hashtable hsInsertUSP = (Hashtable)htSPConfig["CSP_INSERT"];
                //string sNombreSP = (string)hsInsertUSP["CSP_INSERT"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand("usp_adm_pcs_compania_ins_crit");

                helper.AddInParameter(objCommandWrapper, "Tip_Compania", DbType.String, objCompania.STipCompania);
                helper.AddInParameter(objCommandWrapper, "Dsc_Compania", DbType.String, objCompania.SDscCompania);
                helper.AddInParameter(objCommandWrapper, "Cod_Aerolinea", DbType.String, objCompania.SCodAerolinea);
                helper.AddInParameter(objCommandWrapper, "Cod_SAP", DbType.String, objCompania.SCodSAP);
                helper.AddInParameter(objCommandWrapper, "Cod_OACI", DbType.String, objCompania.SCodOACI);
                helper.AddInParameter(objCommandWrapper, "Cod_IATA", DbType.String, objCompania.SCodIATA);
                helper.AddInParameter(objCommandWrapper, "Cod_Especial_Compania", DbType.String, objCompania.SCodEspecialCompania);
                helper.AddInParameter(objCommandWrapper, "Dsc_Ruc", DbType.String, objCompania.SDscRuc);
                helper.AddInParameter(objCommandWrapper, "Log_Usuario_Mod", DbType.String, objCompania.SLogUsuarioMod);
                helper.AddInParameter(objCommandWrapper, "IdTxCritica", DbType.Int64, objCompania.IdTxCritica);
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
        public bool actualizar(Compania objCompania)
        {
            try
            {
                Hashtable hsUpdateUSP = (Hashtable)htSPConfig["CSP_UPDATE"];
                string sNombreSP = (string)hsUpdateUSP["CSP_UPDATE"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Cod_Compania"], DbType.String, objCompania.SCodCompania);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Tip_Compania"], DbType.String, objCompania.STipCompania);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Dsc_Compania"], DbType.String, objCompania.SDscCompania);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Tip_Estado"], DbType.String, objCompania.STipEstado);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Cod_Especial_Compania"], DbType.String, objCompania.SCodEspecialCompania);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Cod_Aerolinea"], DbType.String, objCompania.SCodAerolinea);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Cod_SAP"], DbType.String, objCompania.SCodSAP);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Cod_OACI"], DbType.String, objCompania.SCodOACI);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Cod_IATA"], DbType.String, objCompania.SCodIATA);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Dsc_Ruc"], DbType.String, objCompania.SDscRuc);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Log_Usuario_Mod"], DbType.String, objCompania.SLogUsuarioMod);
                helper.AddOutParameter(objCommandWrapper, (string)hsUpdateUSP["Dsc_Message"], DbType.String, 255);
                isRegistrado = base.mantenerSP(objCommandWrapper);
                if (isRegistrado)
                {
                    objCompania.SLogFechaMod = helper.GetParameterValue(objCommandWrapper, (string)hsUpdateUSP["Dsc_Message"]).ToString();
                }
                return isRegistrado;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool actualizarCrit(Compania objCompania)
        {
            try
            {
                //Hashtable hsUpdateUSP = (Hashtable)htSPConfig["CSP_UPDATE"];
                //string sNombreSP = (string)hsUpdateUSP["CSP_UPDATE"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand("usp_adm_pcs_compania_upd_crit");
                helper.AddInParameter(objCommandWrapper, "Cod_Compania", DbType.String, objCompania.SCodCompania);
                helper.AddInParameter(objCommandWrapper, "Tip_Compania", DbType.String, objCompania.STipCompania);
                helper.AddInParameter(objCommandWrapper, "Dsc_Compania", DbType.String, objCompania.SDscCompania);
                helper.AddInParameter(objCommandWrapper, "Tip_Estado", DbType.String, objCompania.STipEstado);
                helper.AddInParameter(objCommandWrapper, "Cod_Especial_Compania", DbType.String, objCompania.SCodEspecialCompania);
                helper.AddInParameter(objCommandWrapper, "Cod_Aerolinea", DbType.String, objCompania.SCodAerolinea);
                helper.AddInParameter(objCommandWrapper, "Cod_SAP", DbType.String, objCompania.SCodSAP);
                helper.AddInParameter(objCommandWrapper, "Cod_OACI", DbType.String, objCompania.SCodOACI);
                helper.AddInParameter(objCommandWrapper, "Cod_IATA", DbType.String, objCompania.SCodIATA);
                helper.AddInParameter(objCommandWrapper, "Dsc_Ruc", DbType.String, objCompania.SDscRuc);
                helper.AddInParameter(objCommandWrapper, "Log_Usuario_Mod", DbType.String, objCompania.SLogUsuarioMod);
                helper.AddOutParameter(objCommandWrapper, "Dsc_Message", DbType.String, 255);

                helper.AddInParameter(objCommandWrapper, "IdTxCritica", DbType.Int64, objCompania.IdTxCritica);

                isRegistrado = base.mantenerSP(objCommandWrapper);
                if (isRegistrado)
                {
                    objCompania.SLogFechaMod = helper.GetParameterValue(objCommandWrapper, "Dsc_Message").ToString();
                }
                return isRegistrado;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Deletes a record from the objPrecioTicket table by its primary key.
        /// </summary>
        public bool eliminar(string sCodCompania)
        {
            try
            {
                Hashtable htDeleteUSP = (Hashtable)htSPConfig["USP_DELETE"];
                string sNombreSP = (string)htDeleteUSP["USP_DELETE"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);

                helper.AddInParameter(objCommandWrapper, (string)htDeleteUSP["Cod_Compania"], DbType.String, sCodCompania);


                isActualizado = base.mantenerSP(objCommandWrapper);
                return isActualizado;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }







        /// <summary>
        /// Selects all records from the objPrecioTicket table.
        /// </summary>

        public List<Compania> listar()
        {
            IDataReader objResult;
            Hashtable hsSelectAllUSP = (Hashtable)htSPConfig["COMPANIA_SELECTALL"];
            string sNombreSP = (string)hsSelectAllUSP["COMPANIA_SELECTALL"];
            objResult = base.listarDataReaderSP(sNombreSP, null);

            while (objResult.Read())
            {
                objListaCompania.Add(poblar(objResult));

            }
            objResult.Dispose();
            objResult.Close();
            return objListaCompania;
        }

        /// <summary>
        /// Selects a single record from the objPrecioTicket table.
        /// </summary>


        public Compania obtener(string sCodEspecialCompania)
        {

            Compania objCompania = null;
            IDataReader result;
            Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["CSP_SELECT"];
            string sNombreSP = (string)hsSelectByIdUSP["CSP_SELECT"];
            result = base.listarDataReaderSP(sNombreSP, sCodEspecialCompania);

            while (result.Read())
            {
                objCompania = poblar(result);

            }
            result.Dispose();
            result.Close();
            return objCompania;
        }

        /// <summary>
        /// Selects a single record from the objPrecioTicket table.
        /// </summary>


        public Compania obtenerxcodigo(string sCodigoCompania)
        {

            Compania objCompania = null;
            IDataReader result;
            Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["CSP_SELECT_BYCODIGO"];
            string sNombreSP = (string)hsSelectByIdUSP["CSP_SELECT_BYCODIGO"];
            result = base.listarDataReaderSP(sNombreSP, sCodigoCompania);

            while (result.Read())
            {
                objCompania = poblar(result);

            }
            result.Dispose();
            result.Close();
            return objCompania;
        }

        /// <summary>
        /// Selects a single record from the objPrecioTicket table.
        /// </summary>


        public Compania obtenerxnombre(string sNombreCompania)
        {

            Compania objCompania = null;
            IDataReader result;
            Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["CSP_SELECT_BYNOMBRE"];
            string sNombreSP = (string)hsSelectByIdUSP["CSP_SELECT_BYNOMBRE"];
            result = base.listarDataReaderSP(sNombreSP, sNombreCompania);

            while (result.Read())
            {
                objCompania = poblar(result);

            }
            result.Dispose();
            result.Close();
            return objCompania;
        }

        /// <summary>
        /// Realiza la consulta de usuarios deacuerdo a los filtros de Rol, Estado y Grupo.
        /// </summary>

        public DataTable consultarCompaniaxFiltro(string strEstado, string strTipo, string CadFiltro, string sOrdenacion)
        {
            DataTable result;
            Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["CSP_CONSULTARCOMPANIAFILTRO"];
            string sNombreSP = (string)hsSelectByIdUSP["CSP_CONSULTARCOMPANIAFILTRO"];
            result = base.ListarDataTableSP(sNombreSP, strEstado, strTipo, CadFiltro, sOrdenacion);
            return result;
        }
        // <summary>
        /// Realiza la obtención de aerolineas que se encuentran activas en el sistema.
        /// </summary>
        public List<Compania> ListarAerolineas(string strTipo, string strEstado)
        {IDataReader objResult;
            Hashtable hsSelectAllUSP = (Hashtable)htSPConfig["CSP_SELECTAEROLINEA"];
            string sNombreSP = (string)hsSelectAllUSP["CSP_SELECTAEROLINEA"];
            objResult = base.listarDataReaderSP(sNombreSP, strTipo, strEstado);

            while (objResult.Read())
            {
                objListaCompania.Add(poblar(objResult));

            }
            objResult.Dispose();
            objResult.Close();
            return objListaCompania;
        }


        public DataTable listarAllCompania()
        {
            DataTable result;
            Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["CSP_LISTARALLCOMPANIA"];
            string sNombreSP = (string)hsSelectByIdUSP["CSP_LISTARALLCOMPANIA"];
            result = base.ListarDataTableSP(sNombreSP, null);
            return result;
        }



        /// <summary>
        /// Creates a new instance of the objPrecioTicket class and populates it with data from the specified SqlDataReader.
        /// </summary>
        public Compania poblar(IDataReader dataReader)
        {
            Hashtable htSelectAllUSP = (Hashtable)htSPConfig["COMPANIA_SELECTALL"];
            Compania objCompania = new Compania();

            if (dataReader[(string)htSelectAllUSP["Cod_Compania"]] != DBNull.Value)
            objCompania.SCodCompania = (string)dataReader[(string)htSelectAllUSP["Cod_Compania"]];
            if (dataReader[(string)htSelectAllUSP["Tip_Compania"]] != DBNull.Value)
            objCompania.STipCompania = (string)dataReader[(string)htSelectAllUSP["Tip_Compania"]];
            if (dataReader[(string)htSelectAllUSP["Dsc_Compania"]] != DBNull.Value)
            objCompania.SDscCompania = (string)dataReader[(string)htSelectAllUSP["Dsc_Compania"]];
            if (dataReader[(string)htSelectAllUSP["Fch_Creacion"]] != DBNull.Value)
            objCompania.DtFchCreacion = Convert.ToDateTime(dataReader[(string)htSelectAllUSP["Fch_Creacion"]]);
            if (dataReader[(string)htSelectAllUSP["Tip_Estado"]] != DBNull.Value)
            objCompania.STipEstado = (string)dataReader[(string)htSelectAllUSP["Tip_Estado"]];
            if (dataReader[(string)htSelectAllUSP["Cod_Especial_Compania"]] != DBNull.Value)
            objCompania.SCodEspecialCompania = (string)dataReader[(string)htSelectAllUSP["Cod_Especial_Compania"]];
            if (dataReader[(string)htSelectAllUSP["Cod_IATA"]] != DBNull.Value)
            objCompania.SCodIATA = (string)dataReader[(string)htSelectAllUSP["Cod_IATA"]];
            if (dataReader[(string)htSelectAllUSP["Cod_OACI"]] != DBNull.Value)
            objCompania.SCodOACI = (string)dataReader[(string)htSelectAllUSP["Cod_OACI"]];
            if (dataReader[(string)htSelectAllUSP["Cod_SAP"]] != DBNull.Value)
            objCompania.SCodSAP = (string)dataReader[(string)htSelectAllUSP["Cod_SAP"]];
            if (dataReader[(string)htSelectAllUSP["Cod_Aerolinea"]] != DBNull.Value)
            objCompania.SCodAerolinea = (string)dataReader[(string)htSelectAllUSP["Cod_Aerolinea"]];
            if (dataReader[(string)htSelectAllUSP["Dsc_Ruc"]] != DBNull.Value)
            objCompania.SDscRuc = (string)dataReader[(string)htSelectAllUSP["Dsc_Ruc"]];
            if (dataReader[(string)htSelectAllUSP["Log_Usuario_Mod"]] != DBNull.Value)
            objCompania.SLogUsuarioMod = (string)dataReader[(string)htSelectAllUSP["Log_Usuario_Mod"]];
            if (dataReader[(string)htSelectAllUSP["Log_Fecha_Mod"]] != DBNull.Value)
            objCompania.SLogFechaMod = (string)dataReader[(string)htSelectAllUSP["Log_Fecha_Mod"]];
            if (dataReader[(string)htSelectAllUSP["Log_Hora_Mod"]] != DBNull.Value)
            objCompania.SLogHoraMod = (string)dataReader[(string)htSelectAllUSP["Log_Hora_Mod"]];
            return objCompania;
        }

        public DataTable listarCompania_xCodigoEspecial(String codigoEspecial)
        {
            DataTable result;
            Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["CIASP_XCODESPECIAL"];
            string sNombreSP = (string)hsSelectByIdUSP["CIASP_XCODESPECIAL"];
            result = base.ListarDataTableSP(sNombreSP, codigoEspecial);
            return result;
        }


        #endregion
    }
}




