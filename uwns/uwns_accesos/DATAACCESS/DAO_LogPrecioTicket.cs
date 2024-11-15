using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using LAP.TUUA.ENTIDADES;
using System.Collections;

namespace LAP.TUUA.DAO.DAO
{
    public class DAOLogPrecioTicket : DAO_BaseDatos
    {
        #region Fields

        public List<LogPrecioTicket> objListaPrecioTicket;

        #endregion

        #region Constructors

        public DAOLogPrecioTicket(string htSPConfig)
            : base(htSPConfig)
        {
            objListaPrecioTicket = new List<LogPrecioTicket>();
		}

        #endregion

        #region Methods

        /// <summary>
        /// Saves a record to the DAOLogPrecioTicket table.
        /// </summary>
        public bool registrar(LogPrecioTicket objLogPrecioTicket)
        {
            try
            {
                Hashtable htInsertSP = (Hashtable)htSPConfig["LPTSP_INSERT"];
                string sNombreSP = (string)htInsertSP["LPTSP_INSERT"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                helper.AddInParameter(objCommandWrapper, (string)htInsertSP["Cod_Tipo_Ticket"], DbType.String, objLogPrecioTicket.SCodTipoTicket);
                helper.AddInParameter(objCommandWrapper, (string)htInsertSP["Cod_Moneda"], DbType.String, objLogPrecioTicket.SCodMoneda);
                helper.AddInParameter(objCommandWrapper, (string)htInsertSP["Imp_Precio_Actual"], DbType.Decimal, objLogPrecioTicket.DImpPrecioActual);
                helper.AddInParameter(objCommandWrapper, (string)htInsertSP["Imp_Precio_Anterior"], DbType.Decimal, objLogPrecioTicket.DImpPrecioAnterior);
                helper.AddInParameter(objCommandWrapper, (string)htInsertSP["Cod_Moneda_Base"], DbType.String, objLogPrecioTicket.SCodMonedaBase);
                helper.AddInParameter(objCommandWrapper, (string)htInsertSP["Fch_Inicio"], DbType.String, objLogPrecioTicket.DtFchInicio);
                helper.AddInParameter(objCommandWrapper, (string)htInsertSP["Fch_Final"], DbType.String, objLogPrecioTicket.DtFchFinal);
                helper.AddInParameter(objCommandWrapper, (string)htInsertSP["Log_Usuario_Mod"], DbType.String, objLogPrecioTicket.SLogUsuarioMod);
                helper.AddInParameter(objCommandWrapper, (string)htInsertSP["Log_Fecha_Mod"], DbType.DateTime, objLogPrecioTicket.DtLogFechaMod);
                isRegistrado = base.mantenerSP(objCommandWrapper);
                return isRegistrado;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<LogPrecioTicket> listar()
        {
            IDataReader objResult;
            Hashtable hsSelectAllUSP = (Hashtable)htSPConfig["LPTSP_SELECTALL"];
            string sNombreSP = (string)hsSelectAllUSP["LPTSP_SELECTALL"];
            objResult = base.listarDataReaderSP(sNombreSP, null);

            while (objResult.Read())
            {
                objListaPrecioTicket.Add(poblar(objResult));

            }
            
            objResult.Close();
            return objListaPrecioTicket;
        }

        /// <summary>
        /// Creates a new instance of the DAOLogPrecioTicket class and populates it with data from the specified SqlDataReader.
        /// </summary>
        public LogPrecioTicket poblar(IDataReader dataReader)
        {
            Hashtable htSelectAllUSP = (Hashtable)htSPConfig["USP_SELECTALL"];
            LogPrecioTicket objLogPrecioTicket = new LogPrecioTicket();
            objLogPrecioTicket.INumSecuencial = Int32.Parse((string)dataReader[(string)htSelectAllUSP["Num_Secuencial"]]);
            objLogPrecioTicket.SCodTipoTicket = (string)dataReader[(string)htSelectAllUSP["Cod_Tipo_Ticket"]];
            objLogPrecioTicket.SCodMoneda = (string)dataReader[(string)htSelectAllUSP["Cod_Moneda"]];
            objLogPrecioTicket.DImpPrecioActual = Double.Parse((string)dataReader[(string)htSelectAllUSP["Imp_Precio_Actual"]]);
            objLogPrecioTicket.DImpPrecioAnterior = Double.Parse((string)dataReader[(string)htSelectAllUSP["Imp_Precio_Anterior"]]);
            objLogPrecioTicket.SCodMonedaBase = (string)dataReader[(string)htSelectAllUSP["Cod_Moneda_Base"]];
            objLogPrecioTicket.DtFchInicio = Convert.ToDateTime(dataReader[(string)htSelectAllUSP["Fch_Inicio"]]);
            objLogPrecioTicket.DtFchFinal = Convert.ToDateTime(dataReader[(string)htSelectAllUSP["Fch_Final"]]);
            objLogPrecioTicket.SLogUsuarioMod = (string)dataReader[(string)htSelectAllUSP["Log_Usuario_Mod"]];
            objLogPrecioTicket.DtLogFechaMod = Convert.ToDateTime(dataReader[(string)htSelectAllUSP["Log_Fecha_Mod"]]);

            return objLogPrecioTicket;
        }

        #endregion
    }
}
