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
    public class DAO_TasaCambioHist : DAO_BaseDatos
    {
        #region Fields

        public List<TasaCambioHist> objListaTasaCambioHist;

        #endregion

        #region Constructors

        public DAO_TasaCambioHist(string sConfigSPPath)
            : base(sConfigSPPath)
        {
            objListaTasaCambioHist = new List<TasaCambioHist>();            
        }
        #endregion

        #region Methods

        /// <summary>
        /// Saves a record to the TUA_TasaCambioHist table.
        /// </summary>
        public bool insertar(TasaCambioHist objTasaCambioHist)
        {
            try
            {
           
                Hashtable hsInsertUSP = (Hashtable)htSPConfig["TCHSP_INSERT"];
                string sNombreSP = (string)hsInsertUSP["TCHSP_INSERT"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);

                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Tasa_Cambio"], DbType.String, objTasaCambioHist.SCodTasaCambio);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Tip_Cambio"], DbType.String, objTasaCambioHist.STipCambio);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Moneda"], DbType.String, objTasaCambioHist.SCodMoneda);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Imp_Valor"], DbType.Decimal, new Decimal(objTasaCambioHist.DImpValor));
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Moneda2"], DbType.String, objTasaCambioHist.SCodMoneda2);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Imp_Valor2"], DbType.Double, objTasaCambioHist.DImpValor2);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Fch_Ini"], DbType.DateTime, objTasaCambioHist.DtFchIni);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Fch_Fin"], DbType.DateTime, objTasaCambioHist.DtFchFin);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Log_Usuario_Mod"], DbType.String, objTasaCambioHist.SLogUsuarioMod);

                isRegistrado = base.mantenerSP(objCommandWrapper);
                
                return isRegistrado;
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Get records from the TUA_TasaCambioHist table.
        /// </summary>
        public DataTable obtener(string sCodTasaCambio)
        {
            DataTable result;

            //Begin Build Query --------------------
            String strSQL;
            String strWhere = "";
            strSQL = "SELECT tch.Cod_Tasa_Cambio, tch.Tip_Cambio, tch.Cod_Moneda, tch.Imp_Valor, tch.Cod_Moneda2, " +
                     " tch.Imp_Valor2, tch.Fch_Ini, tch.Fch_Fin, tch.Log_Usuario_Mod, tch.Log_Fecha_Mod, tch.Log_Hora_Mod," +
                     " tlc.Dsc_Campo, tm.Dsc_Moneda, tm.Dsc_Simbolo" +   
                     " FROM TUA_TasaCambioHist tch, TUA_ListaDeCampos tlc, TUA_Moneda tm" +
                     " WHERE tch.Tip_Cambio = tlc.Cod_Campo" +
                     " AND tlc.Nom_Campo = 'TipoTasaCambio' AND tch.Cod_Moneda = tm.Cod_Moneda";

            if (sCodTasaCambio != null && sCodTasaCambio.Length > 0)
            {
                strWhere = strWhere + " AND tch.Cod_Tasa_Cambio = '" + sCodTasaCambio.Trim() + "'";
            }
            strSQL = strSQL + strWhere + " ORDER BY tch.Cod_Tasa_Cambio DESC";
            //End Build Query --------------------

            result = base.ListarDataTableQY(strSQL);
            return result;
        }

        #endregion
    }
}
