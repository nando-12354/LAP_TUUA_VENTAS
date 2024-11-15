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
    public class DAO_UsuarioHist : DAO_BaseDatos
    {
        #region Fields
        public List<UsuarioHist> objListaUsuarioHist;

        #endregion

        #region Constructors

        public DAO_UsuarioHist(string  htSPConfig)
            : base(htSPConfig)
        {
            objListaUsuarioHist = new List<UsuarioHist>();
//            this.htSPConfig = htSPConfig;

        }

        #endregion

        #region Methods

        /// <summary>
        /// Saves a record to the DAOUsuarioHist table.
        /// </summary>
        public bool insertar(UsuarioHist objUsuarioHist)
        {

            try
            {
                Hashtable hsInsertUSP = (Hashtable)htSPConfig["USP_INSERT"];
                string sNombreSP = (string)hsInsertUSP["USP_INSERT"];


                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);

                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Usuario"], DbType.String, objUsuarioHist.SCodUsuario);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Tip_Estado"], DbType.String, objUsuarioHist.STipEstado);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Log_Fecha_Mod"], DbType.DateTime, objUsuarioHist.DtLogFechaMod);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Log_Usuario_Mod"], DbType.String, objUsuarioHist.SLogUsuarioMod);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Log_Hora_Mod"], DbType.String, objUsuarioHist.SLogHoraMod);


                isRegistrado = base.mantenerSP(objCommandWrapper);
                return isRegistrado;
            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// Updates a record in the DAOUsuarioHists table.
        /// </summary>
        public bool actualizar(UsuarioHist objUsuarioHist)
        {

            try
            {
                Hashtable hsUpdateUSP = (Hashtable)htSPConfig["USP_UPDATE"];
                string sNombreSP = (string)hsUpdateUSP["USP_UPDATE"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);

                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Cod_Usuario"], DbType.String, objUsuarioHist.SCodUsuario);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Tip_Estado"], DbType.String, objUsuarioHist.STipEstado);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Log_Fecha_Mod"], DbType.DateTime, objUsuarioHist.DtLogFechaMod);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Log_Usuario_Mod"], DbType.String, objUsuarioHist.SLogUsuarioMod);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Log_Hora_Mod"], DbType.String, objUsuarioHist.SLogHoraMod);


                isActualizado = base.mantenerSP(objCommandWrapper);
                return isActualizado;

            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// Deletes a record from the DAOUsuarioHists table by its primary key.
        /// </summary>
        public bool eliminar(string sCodUsuario)
        {

            try
            {
                Hashtable hsDeleteUSP = (Hashtable)htSPConfig["USP_DELETE"];
                string sNombreSP = (string)hsDeleteUSP["USP_DELETE"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);

                helper.AddInParameter(objCommandWrapper, (string)hsDeleteUSP["Cod_Usuario"], DbType.String, sCodUsuario);


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
        public UsuarioHist obtener(string sCodUsuario)
        {
            UsuarioHist objUsuarioHist = null;
            IDataReader result;
            Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["USP_OBTENER_USUARIO_HIST"];
            string sNombreSP = (string)hsSelectByIdUSP["USP_OBTENER_USUARIO_HIST"];
            result = base.listarDataReaderSP(sNombreSP, sCodUsuario);

            while (result.Read())
            {
                objUsuarioHist = poblar(result);

            }
            
            result.Close();
            return objUsuarioHist;
        }

        /// <summary>
        /// Selects all records from the DAOUsuarioHist table.
        /// </summary>
        public List<UsuarioHist> listar()
        {
            IDataReader objResult;
            Hashtable hsSelectAllUSP = (Hashtable)htSPConfig["USP_SELECTALL"];
            string sNombreSP = (string)hsSelectAllUSP["USP_SELECTALL"];
            objResult = base.listarDataReaderSP(sNombreSP, null);

            while (objResult.Read())
            {
                objListaUsuarioHist.Add(poblar(objResult));

            }
            
            objResult.Close();
            return objListaUsuarioHist;
        }



        /// <summary>
        /// Creates a new instance of the DAOUsuarioHist class and populates it with data from the specified SqlDataReader.
        /// </summary>
        public UsuarioHist poblar(IDataReader dataReader)
        {

            Hashtable htSelectAllUSP = (Hashtable)htSPConfig["USP_SELECTALL"];
            UsuarioHist objUsuarioHist = new UsuarioHist();
            objUsuarioHist.SCodUsuario = (string)dataReader[(string)htSelectAllUSP["Cod_Usuario"]];
            objUsuarioHist.STipEstado = (string)dataReader[(string)htSelectAllUSP["Tip_Estado"]];
            objUsuarioHist.DtLogFechaMod = Convert.ToDateTime(dataReader[(string)htSelectAllUSP["Log_Fecha_Mod"]]);
            objUsuarioHist.SLogUsuarioMod = (string)dataReader[(string)htSelectAllUSP["Log_Usuario_Mod"]];
            objUsuarioHist.SLogHoraMod = (string)dataReader[(string)htSelectAllUSP["Log_Hora_Mod"]];

            return objUsuarioHist;
        }

        #endregion
    }
}
