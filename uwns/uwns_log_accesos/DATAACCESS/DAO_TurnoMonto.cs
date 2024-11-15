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
    public class DAO_TurnoMonto : DAO_BaseDatos
    {
        #region Fields

        public List<TurnoMonto> objListaTurnoMonto;

        #endregion

        #region Constructors

        public DAO_TurnoMonto(string sConfigSPPath): base(sConfigSPPath)
        {
            objListaTurnoMonto = new List<TurnoMonto>();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Saves a record to the DAOTurnoMontos table.
        /// </summary>
        public bool insertar(TurnoMonto objTurnoMonto)
        {

            try
            {
                Hashtable hsInsertUSP = (Hashtable)htSPConfig["TURMONT_INSERT"];
                string sNombreSP = (string)hsInsertUSP["TURMONT_INSERT"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);

                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Imp_Monto_Inicial"], DbType.Decimal, objTurnoMonto.DImpMontoInicial);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Imp_Monto_Final"], DbType.Decimal, objTurnoMonto.DImpMontoFinal);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Imp_Monto_Actual"], DbType.Decimal, objTurnoMonto.DImpMontoActual);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Turno"], DbType.String, objTurnoMonto.SCodTurno);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Moneda"], DbType.String, objTurnoMonto.SCodMoneda.Trim());


                isRegistrado = base.mantenerSP(objCommandWrapper);
                return isRegistrado;
            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// Updates a record in the DAOTurnoMontos table.
        /// </summary>
        public bool actualizar(TurnoMonto objTurnoMonto)
        {

            try
            {
                Hashtable hsUpdateUSP = (Hashtable)htSPConfig["TURMONT_UPDATE"];
                string sNombreSP = (string)hsUpdateUSP["TURMONT_UPDATE"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);

                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Imp_Monto_Inicial"], DbType.Double, objTurnoMonto.DImpMontoInicial);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Imp_Monto_Final"], DbType.Double, objTurnoMonto.DImpMontoFinal);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Imp_Monto_Actual"], DbType.Double, objTurnoMonto.DImpMontoActual);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Cod_Turno"], DbType.String, objTurnoMonto.SCodTurno);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Cod_Moneda"], DbType.String, objTurnoMonto.SCodMoneda);


                isActualizado = base.mantenerSP(objCommandWrapper);
                return isActualizado;

            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// Deletes a record from the DAOTurnoMontos table by its primary key.
        /// </summary>
        public bool eliminar(string sCodTurno, string sCodMoneda)
        {

            try
            {

                Hashtable hsDeleteUSP = (Hashtable)htSPConfig["USP_DELETE"];
                string sNombreSP = (string)hsDeleteUSP["USP_DELETE"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);

                helper.AddInParameter(objCommandWrapper, (string)hsDeleteUSP["Cod_Turno"], DbType.String, sCodTurno);
                helper.AddInParameter(objCommandWrapper, (string)hsDeleteUSP["Cod_Moneda"], DbType.String, sCodMoneda);


                isActualizado = base.mantenerSP(objCommandWrapper);
                return isActualizado;

            }
            catch (Exception)
            {

                throw;
            }

        }


        /// <summary>
        /// Selects a single record from the DAOTurnoMontos table.
        /// </summary>
        public TurnoMonto obtener(string sCodTurno, string sCodMoneda)
        {
            TurnoMonto objTurnoMonto = null;
            IDataReader result;
            try
            {
                Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["USP_OBTENER_TURNO_MONTO"];
                string sNombreSP = (string)hsSelectByIdUSP["USP_OBTENER_TURNO_MONTO"];
                result = base.listarDataReaderSP(sNombreSP, sCodTurno, sCodMoneda);

                while (result.Read())
                {
                    objTurnoMonto = poblar(result);

                }
                
                result.Close();
                return objTurnoMonto;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Selects all records from the DAOTurnoMontos table.
        /// </summary>
        public List<TurnoMonto> listar(string sCodTurno)
        {
            IDataReader objResult;
            try
            {
                Hashtable hsSelectAllUSP = (Hashtable)htSPConfig["TURMONSP_SELECTBYTUR"];
                string sNombreSP = (string)hsSelectAllUSP["TURMONSP_SELECTBYTUR"];
                objResult = base.listarDataReaderSP(sNombreSP, sCodTurno);

                while (objResult.Read())
                {
                    objListaTurnoMonto.Add(poblar(objResult));
                }
                
                objResult.Close();
                return objListaTurnoMonto;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Creates a new instance of the DAOTurnoMontos class and populates it with data from the specified SqlDataReader.
        /// </summary>
        public TurnoMonto poblar(IDataReader dataReader)
        {
            try
            {
                Hashtable htSelectAllUSP = (Hashtable)htSPConfig["TURMONSP_SELECTALL"];
                TurnoMonto objTurnoMonto = new TurnoMonto();
                if (dataReader[(string)htSelectAllUSP["Imp_Monto_Inicial"]] != DBNull.Value)
                    objTurnoMonto.DImpMontoInicial = decimal.Parse(dataReader[(string)htSelectAllUSP["Imp_Monto_Inicial"]].ToString());
                if (dataReader[(string)htSelectAllUSP["Imp_Monto_Final"]] != DBNull.Value)
                    objTurnoMonto.DImpMontoFinal = decimal.Parse(dataReader[(string)htSelectAllUSP["Imp_Monto_Final"]].ToString());
                if (dataReader[(string)htSelectAllUSP["Imp_Monto_Actual"]] != DBNull.Value)
                    objTurnoMonto.DImpMontoActual = decimal.Parse(dataReader[(string)htSelectAllUSP["Imp_Monto_Actual"]].ToString());
                if (dataReader[(string)htSelectAllUSP["Cod_Turno"]] != DBNull.Value)
                    objTurnoMonto.SCodTurno = (string)dataReader[(string)htSelectAllUSP["Cod_Turno"]];
                if (dataReader[(string)htSelectAllUSP["Cod_Moneda"]] != DBNull.Value)
                    objTurnoMonto.SCodMoneda = (string)dataReader[(string)htSelectAllUSP["Cod_Moneda"]];

                return objTurnoMonto;
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion
    }
}
