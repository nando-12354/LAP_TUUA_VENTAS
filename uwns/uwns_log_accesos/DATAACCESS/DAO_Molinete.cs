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



        public DataTable listarAllMolinete(string strCodMolinete,string strDscIP)
        {
            DataTable result;
            Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["MSP_LISTARALLMOLINETE"];
            string sNombreSP = (string)hsSelectByIdUSP["MSP_LISTARALLMOLINETE"];
            result = base.ListarDataTableSP(sNombreSP, strCodMolinete, strDscIP);
            return result;
        }




        #endregion
    }
}
