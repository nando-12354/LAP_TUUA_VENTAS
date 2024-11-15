///V.1.4.6.0
///Enrique Montes Balbuena
///Copyright ( Copyright © HIPER S.A. )
///

using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Collections;

namespace LAP.TUUA.DAO
{
    public class DAO_Sincroniza : DAO_BaseDatos
    {
        #region Fields

        #endregion

        #region Constructors

        public DAO_Sincroniza(string sConfigSPPath)
            : base(sConfigSPPath)
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Descripcion : Sincronizacion de Ticket del Servidor Local al Servidor Central
        /// Parametro : strIP_Servidor // Recibe la IP del Servidor Central
        /// Parametro : strDBUser // Recibe el usuario de Base de Datos del Servidor Central
        /// Parametro : strDBPassword // Recibe el password de Base de Datos del Servidor Central
        /// Parametro : strDsc_IP // Recibe la IP del Servidor Local
        /// Parametro : intInter_reg // Recibe el numero de registros (50) a verificar en el Stored Procedure si esta cancelado el proceso
        /// Parametro : intModo_SIN // Modo Sincronismo - Recibe los siguientes valores 1=Automatico 2=Horas Programadas
        /// Parametro : strMessage // Recibe el mensaje de Error si hubiera alguan Excepcion en el Stored Procedure
        /// Parametro : strInformation // Recibe mensaje de conformidad de la Sincronizacion
        /// </summary>
        /// <returns>bool</returns>
        public bool SincronizarLocalToCentralTicket(string strIP_SERVIDOR, string strDBName, string strDsc_IP, int intInter_reg, int intModo_SIN, ref string strMessage, ref string strInformation)
        {
            bool boResult;
            try
            {
                Hashtable hsInsertUSP = (Hashtable)htSPConfig["SP_SINCLOCALTOCENTRAL_TC_BP"];
                string sNombreSP = (string)hsInsertUSP["SP_SINCLOCALTOCENTRAL_TC_BP"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);

                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["IP_SERVIDOR"], DbType.String, strIP_SERVIDOR);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["DBName"], DbType.String, strDBName);

                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["cDsc_IP"], DbType.String, strDsc_IP);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["nInter_reg"], DbType.Int32, intInter_reg);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Modo_SIN"], DbType.Int32, intModo_SIN);

                helper.AddOutParameter(objCommandWrapper, (string)hsInsertUSP["Dsc_Message"], DbType.String, 255);
                helper.AddOutParameter(objCommandWrapper, (string)hsInsertUSP["Dsc_Information"], DbType.String, 2000);
                boResult = base.mantenerSP(objCommandWrapper);
                if (boResult)
                {
                    strMessage = (string)helper.GetParameterValue(objCommandWrapper, (string)hsInsertUSP["Dsc_Message"]);
                    strInformation = (string)helper.GetParameterValue(objCommandWrapper, (string)hsInsertUSP["Dsc_Information"]);
                }
                return boResult;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Descripcion : Sincronizacion de Boarding Pass del Servidor Local al Servidor Central
        /// Parametro : intModo_SIN // Modo Sincronismo - Recibe los siguientes valores 1=Automatico 2=Horas Programadas
        /// Parametro : strMessage // Recibe el mensaje de Error si hubiera alguan Excepcion en el Stored Procedure
        /// Parametro : strInformation // Recibe mensaje de conformidad de la Sincronizacion
        /// </summary>
        /// <returns>bool</returns>
        public bool SincronizarCentralToLocalAtr(ref string strMessage, ref string strInformation)
        {
            bool boResult;
            try
            {
                Hashtable hsInsertUSP = (Hashtable)htSPConfig["SP_SINCCENTRALTOLOCAL_TABLAS"];
                string sNombreSP = (string)hsInsertUSP["SP_SINCCENTRALTOLOCAL_TABLAS"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);

                helper.AddOutParameter(objCommandWrapper, (string)hsInsertUSP["Dsc_Message"], DbType.String, 255);
                helper.AddOutParameter(objCommandWrapper, (string)hsInsertUSP["Dsc_Information"], DbType.String, 2000);
                boResult = base.mantenerSP(objCommandWrapper);
                if (boResult)
                {
                    strMessage = (string)helper.GetParameterValue(objCommandWrapper, (string)hsInsertUSP["Dsc_Message"]);
                    strInformation = (string)helper.GetParameterValue(objCommandWrapper, (string)hsInsertUSP["Dsc_Information"]);
                }
                return boResult;
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion
    }
}
