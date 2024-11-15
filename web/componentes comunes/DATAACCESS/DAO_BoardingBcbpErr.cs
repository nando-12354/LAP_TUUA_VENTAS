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
    public class DAO_BoardingBcbpErr : DAO_BaseDatos
    {
        #region Fields

        public List<BoardingBcbpErr> objListaBoardingBcbpErr;

        #endregion

        #region Constructors

           
        public DAO_BoardingBcbpErr(string sConfigSPPath): base(sConfigSPPath)
        {
            objListaBoardingBcbpErr = new List<BoardingBcbpErr>();
            //this.htSPConfig = htSPConfig;
        }

        public DAO_BoardingBcbpErr(Database vhelper, Database vhelperLocal, Hashtable vhtSPConfig)
              : base(vhelper, vhelperLocal, vhtSPConfig)
        {
              objListaBoardingBcbpErr = new List<BoardingBcbpErr>();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Saves a record to the DAOBoardingBcbpErr table.
        /// </summary>

        public bool insertar(BoardingBcbpErr objBoardingBcbpErr)
        {
            try
            {
                Hashtable hsInsertUSP = (Hashtable)htSPConfig["BOARDINGERRSP_INSERT"];
                string sNombreSP = (string)hsInsertUSP["BOARDINGERRSP_INSERT"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Dsc_Trama_Bcbp"], DbType.String, objBoardingBcbpErr.SDscTramaBcbp);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Tip_Error"], DbType.String, objBoardingBcbpErr.Cod_Tip_Error);

                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Log_Usuario_Mod"], DbType.String, objBoardingBcbpErr.StrLogUsuarioMod);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Log_Fecha_Mod"], DbType.String, objBoardingBcbpErr.StrLogFechaMod);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Log_Hora_Mod"], DbType.String, objBoardingBcbpErr.StrLogHoraMod);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Tip_Ingreso"], DbType.String, objBoardingBcbpErr.StrTipIngreso);


                isRegistrado = base.mantenerSP(objCommandWrapper);
                return isRegistrado;
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Updates a record in the DAOBoardingBcbpErr table.
        /// </summary>

        public bool actualizar(BoardingBcbpErr objBoardingBcbpErr)
        {
            try
            {
                Hashtable hsUpdateUSP = (Hashtable)htSPConfig["USP_UPDATE"];
                string sNombreSP = (string)hsUpdateUSP["USP_UPDATE"];
                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Num_Secuencial"], DbType.Int32, objBoardingBcbpErr.INumSecuencial);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Dsc_Trama_Bcbp"], DbType.String, objBoardingBcbpErr.SDscTramaBcbp);
                isActualizado = base.mantenerSP(objCommandWrapper);
                return isActualizado;
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Deletes a record from the DAOBoardingBcbpErr table by its primary key.
        /// </summary>
        public bool eliminar(int iNumSecuencial)
        {
            try
            {
                Hashtable htDeleteUSP = (Hashtable)htSPConfig["USP_DELETE"];
                string sNombreSP = (string)htDeleteUSP["USP_DELETE"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);

                helper.AddInParameter(objCommandWrapper, (string)htDeleteUSP["Num_Secuencial"], DbType.Int32, iNumSecuencial);


                isActualizado = base.mantenerSP(objCommandWrapper);
                return isActualizado;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }


        /// <summary>
        /// Selects a single record from the DAOBoardingBcbpErr table.
        /// </summary>

        protected List<BoardingBcbpErr> listar()
        {
            IDataReader objResult;
            Hashtable hsSelectAllUSP = (Hashtable)htSPConfig["USP_SELECTALL"];
            string sNombreSP = (string)hsSelectAllUSP["USP_SELECTALL"];
            objResult = base.listarDataReaderSP(sNombreSP, null);

            while (objResult.Read())
            {
                objListaBoardingBcbpErr.Add(poblar(objResult));

            }
            objResult.Dispose();
            objResult.Close();
            return objListaBoardingBcbpErr;
        }



        /// <summary>
        /// Selects all records from the DAOBoardingBcbpErr table.
        /// </summary>

        public DataTable ListarLogErroresMolinete(string sFechaDesde, string sFechaHasta, string sHoraDesde,
            string sHoraHasta, string sIDError, string sTipoError, string sCompania, string sCodMolinete, string sTipoBoarding,
            string sTipIngreso, string sFchVuelo, string sNumVuelo, string sNumAsiento, string sNomPasajero, string sColumnaSort,
            int iStartRows, int iMaxRows, string sPaginacion, string sMostrarResumen, string sFlgTotalRows)
        {

            DataTable objResult;
            Hashtable hsSelectAllUSP = (Hashtable)htSPConfig["USP_OBTENER_LOGERRORESMOLINETE"];
            string sNombreSP = (string)hsSelectAllUSP["USP_OBTENER_LOGERRORESMOLINETE"];
            objResult = base.ListarDataTableSP(sNombreSP, sFechaDesde, sFechaHasta, sHoraDesde, sHoraHasta, sIDError, sTipoError, sCompania, sCodMolinete, sTipoBoarding, sTipIngreso, sFchVuelo, sNumVuelo, sNumAsiento, sNomPasajero, sColumnaSort, iStartRows, iMaxRows, sPaginacion, sMostrarResumen, sFlgTotalRows);
            return objResult;

        }

        /// <summary>
        /// Creates a new instance of the DAOBoardingBcbpErr class and populates it with data from the specified SqlDataReader.
        /// </summary>

        protected BoardingBcbpErr poblar(IDataReader dataReader)
        {
            Hashtable htSelectAllUSP = (Hashtable)htSPConfig["USP_SELECTALL"];
            BoardingBcbpErr objBoardingBcbpErr = new BoardingBcbpErr();
            objBoardingBcbpErr.INumSecuencial = (Int32)dataReader[(Int32)htSelectAllUSP["Num_Secuencial"]];
            objBoardingBcbpErr.SDscTramaBcbp = (string)dataReader[(string)htSelectAllUSP["Dsc_Trama_Bcbp"]];
            return objBoardingBcbpErr;
        }

        #endregion
    }
}