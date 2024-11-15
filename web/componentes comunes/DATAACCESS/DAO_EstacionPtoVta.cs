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
    public class DAO_EstacionPtoVta : DAO_BaseDatos
    {
        #region Fields

        public List<EstacionPtoVta> objListaEstacionPtoVta;
        #endregion

        #region Constructors
        public DAO_EstacionPtoVta(string sConfigSPPath)
            : base(sConfigSPPath)
        {
            objListaEstacionPtoVta = new List<EstacionPtoVta>();
        }
        #endregion

        #region Methods

        /// <summary>
        /// Saves a record to the objPrecioTicket table.
        /// </summary>
        public bool insertar(EstacionPtoVta objEstacionPtoVta)
        {
            try
            {
                Hashtable hsInsertUSP = (Hashtable)htSPConfig["ESPTOVTASP_INSERT"];
                string sNombreSP = (string)hsInsertUSP["ESPTOVTASP_INSERT"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);                
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Num_Ip_Equipo"], DbType.String, objEstacionPtoVta.SNumIpEquipo);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Flg_Estado"], DbType.String, objEstacionPtoVta.SFlgEstado);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Usuario_Creacion"], DbType.String, objEstacionPtoVta.SCodUsuarioCreacion);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Log_Usuario_Mod"], DbType.String, objEstacionPtoVta.SLogUsuarioMod);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Log_Fecha_Mod"], DbType.String, objEstacionPtoVta.DtLogFechaMod);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Log_Hora_Mod"], DbType.String, objEstacionPtoVta.SLogHoraMod);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Dsc_Estacion"], DbType.String, objEstacionPtoVta.SDscEstacion);
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
        public bool actualizar(EstacionPtoVta objEstacionPtoVta)
        {
            try
            {
                Hashtable hsUpdateUSP = (Hashtable)htSPConfig["ESPTOVTASP_UPDATE"];
                string sNombreSP = (string)hsUpdateUSP["ESPTOVTASP_UPDATE"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Cod_Equipo"], DbType.String, objEstacionPtoVta.SCodEquipo);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Num_Ip_Equipo"], DbType.String, objEstacionPtoVta.SNumIpEquipo);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Flg_Estado"], DbType.String, objEstacionPtoVta.SFlgEstado);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Cod_Usuario_Creacion"], DbType.String, objEstacionPtoVta.SCodUsuarioCreacion);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Log_Usuario_Mod"], DbType.String, objEstacionPtoVta.SLogUsuarioMod);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Log_Fecha_Mod"], DbType.String, objEstacionPtoVta.DtLogFechaMod);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Log_Hora_Mod"], DbType.String, objEstacionPtoVta.SLogHoraMod);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Dsc_Estacion"], DbType.String, objEstacionPtoVta.SDscEstacion);
                isActualizado = base.mantenerSP(objCommandWrapper);
                return isActualizado;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Deletes a record from the objPrecioTicket table by its primary key.
        /// </summary>
        public bool eliminar(string sCodEquipo, string  sLogUsuarioMod)
        {
            try
            {
                Hashtable htDeleteUSP = (Hashtable)htSPConfig["ESPTOVTASP_DELETE"];
                string sNombreSP = (string)htDeleteUSP["ESPTOVTASP_DELETE"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);

                helper.AddInParameter(objCommandWrapper, (string)htDeleteUSP["Cod_Equipo"], DbType.String, sCodEquipo);
                helper.AddInParameter(objCommandWrapper, (string)htDeleteUSP["Log_Usuario_Mod"], DbType.String, sLogUsuarioMod);
      
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

        public List<EstacionPtoVta> listar()
        {
            IDataReader objResult;
            Hashtable hsSelectAllUSP = (Hashtable)htSPConfig["USP_SELECTALL"];
            string sNombreSP = (string)hsSelectAllUSP["USP_SELECTALL"];
            objResult = base.listarDataReaderSP(sNombreSP, null);

            while (objResult.Read())
            {
                objListaEstacionPtoVta.Add(poblar(objResult));

            }
            objResult.Dispose();
            objResult.Close();
            return objListaEstacionPtoVta;
        }

        /// <summary>
        /// Selects a single record from the objPrecioTicket table.
        /// </summary>


        public EstacionPtoVta obtenerPtoVentaxIP(string strIP)
        {
            try
            {
                EstacionPtoVta objEstacionPtoVta = null;
                IDataReader result;
                Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["EPVSP_OBTENER_ESTACIONPTOVTA"];
                string sNombreSP = (string)hsSelectByIdUSP["EPVSP_OBTENER_ESTACIONPTOVTA"];
                result = base.listarDataReaderSP(sNombreSP, null, strIP);

                while (result.Read())
                {
                    objEstacionPtoVta = poblar(result);

                }
                result.Dispose();
                result.Close();
                return objEstacionPtoVta;
            }
            catch
            {
                throw;
            }
        }



        /// <summary>
        /// Creates a new instance of the objPrecioTicket class and populates it with data from the specified SqlDataReader.
        /// </summary>
        public EstacionPtoVta poblar(IDataReader dataReader)
        {
            Hashtable htSelectAllUSP = (Hashtable)htSPConfig["EPVSP_SELECTALL"];
            EstacionPtoVta objEstacionPtoVta = new EstacionPtoVta();
            objEstacionPtoVta.SCodEquipo = (string)dataReader[(string)htSelectAllUSP["Cod_Equipo"]];
            objEstacionPtoVta.SNumIpEquipo = (string)dataReader[(string)htSelectAllUSP["Num_Ip_Equipo"]];
            objEstacionPtoVta.SFlgEstado = (string)dataReader[(string)htSelectAllUSP["Flg_Estado"]];
            return objEstacionPtoVta;
        }


        public DataTable listarAllPuntoVenta()
        {
            DataTable objResult;
            Hashtable hsSelectAllUSP = (Hashtable)htSPConfig["ESPTOVTASP_SELECTALLESTACION"];
            string sNombreSP = (string)hsSelectAllUSP["ESPTOVTASP_SELECTALLESTACION"];
            objResult = base.ListarDataTableSP(sNombreSP, null);
            objResult.Dispose();
            return objResult;
        }


        public DataTable obtenerDetallePuntoVenta(string sCodEstacion,string strIP)
        {
            DataTable objResult;
            Hashtable hsSelectAllUSP = (Hashtable)htSPConfig["ESPTOVTASP_DETALLE"];
            string sNombreSP = (string)hsSelectAllUSP["ESPTOVTASP_DETALLE"];
            objResult = base.ListarDataTableSP(sNombreSP, sCodEstacion, strIP);
            objResult.Dispose();
            return objResult;
        }


        #endregion
    }
}