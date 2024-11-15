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
      public class DAO_LogTasaCambio : DAO_BaseDatos
      {

        #region Fields

        public List<LogTasaCambio> objListaLogTasaCambio;

        #endregion

        #region Constructors
        public DAO_LogTasaCambio(string sConfigSPPathg)
            : base(sConfigSPPathg)
        {
            objListaLogTasaCambio = new List<LogTasaCambio>();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Saves a record to the TUA_TasaCambio table.
        /// </summary>
        public bool insertar(LogTasaCambio objLogTasaCambio)
        {
              try
              {
                    Hashtable hsInsertUSP = (Hashtable)htSPConfig["LTCSP_INSERT"];
                    string sNombreSP = (string)hsInsertUSP["LTCSP_INSERT"];

                    DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                    helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Moneda"], DbType.String, objLogTasaCambio.SCodMoneda);
                    helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Imp_Compra"], DbType.Decimal, objLogTasaCambio.DImpCompra);
                    helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Imp_Venta"], DbType.Decimal, objLogTasaCambio.DImpVenta);
                    helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Log_Usuario_Mod"], DbType.String, objLogTasaCambio.SLogUsuarioMod);
                    helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Log_Fecha_Mod"], DbType.String, objLogTasaCambio.SLogFechaMod);
                    helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Log_Hora_Mod"], DbType.String, objLogTasaCambio.SLogHoraMod);
                    isRegistrado = base.mantenerSP(objCommandWrapper);
                    return isRegistrado;

              }
              catch (Exception)
              {

                    throw;
              }
        }

        #endregion


      }
}
