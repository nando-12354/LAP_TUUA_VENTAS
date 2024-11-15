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
        /// Saves a record to the DAOTicket table.
        /// </summary>
        public bool SincronizarCentralToLocalTicket(string strMaster, string strSlaves, int intSlaves, ref string strMessage,ref string strInformation)
        {
            bool boResult;
            try
            {
                Hashtable hsInsertUSP = (Hashtable)htSPConfig["SP_SINCCENTRALTOLOCAL_TICKET"];
                string sNombreSP = (string)hsInsertUSP["SP_SINCCENTRALTOLOCAL_TICKET"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);

                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Master"], DbType.String, strMaster);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["hostSlaves"], DbType.String, strSlaves);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Can_Slaves"], DbType.Int32, intSlaves);

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

        public bool SincronizarCentralToLocalBcbp(string strMaster, string strSlaves, int intSlaves, ref string strMessage, ref string strInformation)
        {
            bool boResult;
            try
            {
                Hashtable hsInsertUSP = (Hashtable)htSPConfig["SP_SINCCENTRALTOLOCAL_BCBP"];
                string sNombreSP = (string)hsInsertUSP["SP_SINCCENTRALTOLOCAL_BCBP"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);

                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Master"], DbType.String, strMaster);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["hostSlaves"], DbType.String, strSlaves);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Can_Slaves"], DbType.Int32, intSlaves);

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

        public bool SincronizarLocalToCentralTicket(string strMaster, ref string strMessage, ref string strInformation)
        {
            bool boResult;
            try
            {
                Hashtable hsInsertUSP = (Hashtable)htSPConfig["SP_SINCLOCALTOCENTRAL_TICKET"];
                string sNombreSP = (string)hsInsertUSP["SP_SINCLOCALTOCENTRAL_TICKET"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);

                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Master"], DbType.String, strMaster);
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

        public bool SincronizarLocalToCentralBcbp(string strMaster, ref string strMessage, ref string strInformation)
        {
            bool boResult;
            try
            {
                Hashtable hsInsertUSP = (Hashtable)htSPConfig["SP_SINCLOCALTOCENTRAL_BCBP"];
                string sNombreSP = (string)hsInsertUSP["SP_SINCLOCALTOCENTRAL_BCBP"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);

                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Master"], DbType.String, strMaster);
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

        public bool SincronizarSlavesToMasterTicket(string strMaster, string strSlaves, int intSlaves, ref string strFechaSincro, ref string strUltimaVez, ref string strMessage, ref string strInformation)
        {
            bool boResult;
            try
            {
                Hashtable hsInsertUSP = (Hashtable)htSPConfig["SP_SINCSLAVESTOMASTER_TICKET"];
                string sNombreSP = (string)hsInsertUSP["SP_SINCSLAVESTOMASTER_TICKET"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                objCommandWrapper.CommandTimeout = 0;
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Master"], DbType.String, strMaster);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["hostSlaves"], DbType.String, strSlaves);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Can_Slaves"], DbType.Int32, intSlaves);                
                helper.AddOutParameter(objCommandWrapper, "Fch_Sincroniza", DbType.String, 14);
                helper.AddOutParameter(objCommandWrapper, "UltimaVez", DbType.String, 14);
                helper.AddOutParameter(objCommandWrapper, (string)hsInsertUSP["Dsc_Message"], DbType.String, 255);
                helper.AddOutParameter(objCommandWrapper, "Dsc_Information", DbType.String, 2000);
                boResult = base.mantenerSP(objCommandWrapper);
                if (boResult)
                {
                    strMessage = (string)helper.GetParameterValue(objCommandWrapper, (string)hsInsertUSP["Dsc_Message"]);
                    strFechaSincro = (string)helper.GetParameterValue(objCommandWrapper, "Fch_Sincroniza");
                    strUltimaVez = (string)helper.GetParameterValue(objCommandWrapper, "UltimaVez");
                    strInformation = (string)helper.GetParameterValue(objCommandWrapper, "Dsc_Information");//kinzi
                }
                return boResult;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool SincronizarSlavesToMasterBcbp(string strMaster, string strSlaves, int intSlaves, ref string strFechaSincro, ref string strUltimaVez, ref string strMessage, ref string strInformation)
        {
            bool boResult;
            try
            {
                Hashtable hsInsertUSP = (Hashtable)htSPConfig["SP_SINCSLAVESTOMASTER_BCBP"];
                string sNombreSP = (string)hsInsertUSP["SP_SINCSLAVESTOMASTER_BCBP"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                objCommandWrapper.CommandTimeout = 0;
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Master"], DbType.String, strMaster);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["hostSlaves"], DbType.String, strSlaves);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Can_Slaves"], DbType.Int32, intSlaves);                
                helper.AddOutParameter(objCommandWrapper, "Fch_Sincroniza", DbType.String, 14);
                helper.AddOutParameter(objCommandWrapper, "UltimaVez", DbType.String, 14);
                helper.AddOutParameter(objCommandWrapper, (string)hsInsertUSP["Dsc_Message"], DbType.String, 255);
                helper.AddOutParameter(objCommandWrapper, "Dsc_Information", DbType.String, 2000);

                boResult = base.mantenerSP(objCommandWrapper);
                if (boResult)
                {
                    strMessage = (string)helper.GetParameterValue(objCommandWrapper, (string)hsInsertUSP["Dsc_Message"]);
                    strFechaSincro = (string)helper.GetParameterValue(objCommandWrapper, "Fch_Sincroniza");
                    strUltimaVez = (string)helper.GetParameterValue(objCommandWrapper, "UltimaVez");
                    strInformation = (string)helper.GetParameterValue(objCommandWrapper, "Dsc_Information");//kinzi
                }
                return boResult;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool SincronizarMasterToSlavesTicket(string strMaster, string strSlaves, int intSlaves, string strFechaSincro, string strUltimaVez, ref string strMessage, ref string strInformation)
        {
            bool boResult;
            try
            {
                Hashtable hsInsertUSP = (Hashtable)htSPConfig["SP_SINCMASTERTOSLAVES_TICKET"];
                string sNombreSP = (string)hsInsertUSP["SP_SINCMASTERTOSLAVES_TICKET"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                objCommandWrapper.CommandTimeout = 0;
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Master"], DbType.String, strMaster);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["hostSlaves"], DbType.String, strSlaves);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Can_Slaves"], DbType.Int32, intSlaves);
                helper.AddInParameter(objCommandWrapper, "Fch_Sincroniza", DbType.String, strFechaSincro);
                helper.AddInParameter(objCommandWrapper, "UltimaVez", DbType.String, strUltimaVez);

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

        public bool SincronizarMasterToSlavesBcbp(string strMaster, string strSlaves, int intSlaves, string strFechaSincro, string strUltimaVez, ref string strMessage, ref string strInformation)
        {
            bool boResult;
            try
            {
                Hashtable hsInsertUSP = (Hashtable)htSPConfig["SP_SINCMASTERTOSLAVES_BCBP"];
                string sNombreSP = (string)hsInsertUSP["SP_SINCMASTERTOSLAVES_BCBP"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                objCommandWrapper.CommandTimeout = 0;
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Master"], DbType.String, strMaster);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["hostSlaves"], DbType.String, strSlaves);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Can_Slaves"], DbType.Int32, intSlaves);
                helper.AddInParameter(objCommandWrapper, "Fch_Sincroniza", DbType.String, strFechaSincro);
                helper.AddInParameter(objCommandWrapper, "UltimaVez", DbType.String, strUltimaVez);

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

        public bool SincronizarCentralToLocalAtr(string strMaster, string strSlaves, int intSlaves, ref string strMessage, ref string strInformation)
        {
            bool boResult;
            try
            {
                Hashtable hsInsertUSP = (Hashtable)htSPConfig["SP_SINCCENTRALTOLOCAL_ATR"];
                string sNombreSP = (string)hsInsertUSP["SP_SINCCENTRALTOLOCAL_ATR"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);

                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Master"], DbType.String, strMaster);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["hostSlaves"], DbType.String, strSlaves);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Can_Slaves"], DbType.Int32, intSlaves);

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
