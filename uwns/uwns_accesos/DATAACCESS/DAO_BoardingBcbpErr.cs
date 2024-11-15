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
                helper.AddInParameter(objCommandWrapper, "Tip_Ingreso", DbType.String, objBoardingBcbpErr.StrTipIngreso);

                helper.AddInParameter(objCommandWrapper, "Cod_Equipo_Mod", DbType.String, objBoardingBcbpErr.Cod_Equipo_Mod);
                helper.AddInParameter(objCommandWrapper, "Tip_Boarding", DbType.String, objBoardingBcbpErr.Tip_Boarding);
                helper.AddInParameter(objCommandWrapper, "Cod_Compania", DbType.String, objBoardingBcbpErr.Cod_Compania);
                helper.AddInParameter(objCommandWrapper, "Num_Vuelo", DbType.String, objBoardingBcbpErr.Num_Vuelo);
                helper.AddInParameter(objCommandWrapper, "Fch_Vuelo", DbType.String, objBoardingBcbpErr.Fch_Vuelo);
                helper.AddInParameter(objCommandWrapper, "Num_Asiento", DbType.String, objBoardingBcbpErr.Num_Asiento);
                helper.AddInParameter(objCommandWrapper, "Nom_Pasajero", DbType.String, objBoardingBcbpErr.Nom_Pasajero);
                helper.AddInParameter(objCommandWrapper, "Log_Error", DbType.String, objBoardingBcbpErr.Log_Error);

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
            
            objResult.Close();
            return objListaBoardingBcbpErr;
        }



        /// <summary>
        /// Selects all records from the DAOBoardingBcbpErr table.
        /// </summary>

        protected BoardingBcbpErr obtener(int iNumSecuencial)
        {

            BoardingBcbpErr objBoardingBcbpErr = null;
            IDataReader result;
            Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["USP_OBTENER_BOARDINGBCBPERR"];
            string sNombreSP = (string)hsSelectByIdUSP["USP_OBTENER_BOARDINGBCBPERR"];
            result = base.listarDataReaderSP(sNombreSP, iNumSecuencial);

            while (result.Read())
            {
                objBoardingBcbpErr = poblar(result);

            }
            
            result.Close();
            return objBoardingBcbpErr;
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
