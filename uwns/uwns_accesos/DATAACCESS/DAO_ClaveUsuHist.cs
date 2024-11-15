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
    public class DAO_ClaveUsuHist : DAO_BaseDatos
    {
        #region Fields

        public List<ClaveUsuHist> objClaveUsuHist;
        #endregion

        #region Constructors
        public DAO_ClaveUsuHist(string htSPConfig)
            : base(htSPConfig)
        {
            objClaveUsuHist = new List<ClaveUsuHist>();

        }
        #endregion

        #region Methods

        /// <summary>
        /// Saves a record to the objPrecioTicket table.
        /// </summary>
        public bool insertar(ClaveUsuHist objClaveUsuHist)
        {
            try
            {
                Hashtable hsInsertUSP = (Hashtable)htSPConfig["USP_INSERT"];
                string sNombreSP = (string)hsInsertUSP["USP_INSERT"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Usuario"], DbType.String, objClaveUsuHist.SCodUsuario);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Log_Fecha_Mod"], DbType.DateTime, objClaveUsuHist.SLogFechaMod);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Dsc_Clave"], DbType.String, objClaveUsuHist.SDscClave);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Log_Usuario_Mod"], DbType.String, objClaveUsuHist.SLogUsuarioMod);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Log_Hora_Modificado"], DbType.String, objClaveUsuHist.SLogHoraMod);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Num_Contador_Usuario"], DbType.Int32, objClaveUsuHist.INumContadorUsuario);
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
        public bool actualizar(ClaveUsuHist objClaveUsuHist)
        {
            try
            {
                Hashtable hsUpdateUSP = (Hashtable)htSPConfig["UHSP_UPDATE"];
                string sNombreSP = (string)hsUpdateUSP["UHSP_UPDATE"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Cod_Usuario"], DbType.String, objClaveUsuHist.SCodUsuario);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Log_Fecha_Mod"], DbType.DateTime, objClaveUsuHist.SLogFechaMod);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Dsc_Clave"], DbType.String, objClaveUsuHist.SDscClave);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Log_Usuario_Mod"], DbType.String, objClaveUsuHist.SLogUsuarioMod);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Log_Hora_Modificado"], DbType.String, objClaveUsuHist.SLogHoraMod);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Num_Contador_Usuario"], DbType.Int32, objClaveUsuHist.INumContadorUsuario);
                isActualizado = base.mantenerSP(objCommandWrapper);
                return isActualizado;
            }
            catch (Exception)
            {
                throw;
            }
        }




        /// <summary>
        /// Deletes a record from the objPrecioTicket table by its primary key.
        /// </summary>
        public bool eliminar(string sCodUsuario, int iNumContadorUsuario)
        {
            try
            {
                Hashtable htDeleteUSP = (Hashtable)htSPConfig["UHSP_DELETE"];
                string sNombreSP = (string)htDeleteUSP["UHSP_DELETE"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);

                helper.AddInParameter(objCommandWrapper, (string)htDeleteUSP["Cod_Usuario"], DbType.String, sCodUsuario);
                helper.AddInParameter(objCommandWrapper, (string)htDeleteUSP["Num_Contador_Usuario"], DbType.String, iNumContadorUsuario);


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

        public List<ClaveUsuHist> listar()
        {
            IDataReader objResult;
            Hashtable hsSelectAllUSP = (Hashtable)htSPConfig["UHSP_SELECTALL"];
            string sNombreSP = (string)hsSelectAllUSP["UHSP_SELECTALL"];
            objResult = base.listarDataReaderSP(sNombreSP, null);


            while (objResult.Read())
            {
                objClaveUsuHist.Add(poblar(objResult));

            }
            
            objResult.Close();
            return objClaveUsuHist;
        }

        /// <summary>
        /// Selects a single record from the objPrecioTicket table.
        /// </summary>


        public bool obtener(string sCodUsuario, string sDscClave,int iNum)
        {
            IDataReader result;
            Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["UHSP_OBTENER_CLAVEUSUHIST"];
            string sNombreSP = (string)hsSelectByIdUSP["UHSP_OBTENER_CLAVEUSUHIST"];
            result = base.listarDataReaderSP(sNombreSP, sCodUsuario, sDscClave,iNum);

            while (result.Read())
            {
                return true;

            }
            
            result.Close();
            return false;
        }


        /// <summary>
        /// Selects a single record from the objPrecioTicket table.
        /// </summary>

        public ClaveUsuHist obtenerUsuarioHist(string sCodUsuario)
        {
            ClaveUsuHist objClaveUsuHist = null;
            IDataReader result;
            Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["UHSP_OBTENER_USUHIST"];
            string sNombreSP = (string)hsSelectByIdUSP["UHSP_OBTENER_USUHIST"];
            result = base.listarDataReaderSP(sNombreSP, sCodUsuario);

            while (result.Read())
            {
                objClaveUsuHist=poblar(result);

            }
            
            result.Close();
            return objClaveUsuHist;
        }

        /// <summary>
        /// Creates a new instance of the objPrecioTicket class and populates it with data from the specified SqlDataReader.
        /// </summary>
        public ClaveUsuHist poblar(IDataReader dataReader)
        {
            Hashtable htSelectAllUSP = (Hashtable)htSPConfig["UHSP_SELECTALL"];
            ClaveUsuHist objClaveUsuHist = new ClaveUsuHist();
            objClaveUsuHist.SCodUsuario = (string)dataReader[(string)htSelectAllUSP["Cod_Usuario"]];
            objClaveUsuHist.SLogFechaMod = (string)dataReader[(string)htSelectAllUSP["Log_Fecha_Mod"]];
            objClaveUsuHist.SDscClave = (string)dataReader[(string)htSelectAllUSP["Dsc_Clave"]];
            objClaveUsuHist.SLogUsuarioMod = (string)dataReader[(string)htSelectAllUSP["Log_Usuario_Mod"]];
            objClaveUsuHist.SLogHoraMod = (string)dataReader[(string)htSelectAllUSP["Log_Hora_Mod"]];
            return objClaveUsuHist;
        }

        #endregion
    }
}


