using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Collections;

namespace LAP.TUUA.DAO
{
    public class DAO_Depuracion : DAO_BaseDatos
    {
        #region Fields

        #endregion

        #region Constructors

        public DAO_Depuracion(string sConfigSPPath)
            : base(sConfigSPPath)
        {
        }

        #endregion

        #region Methods



        public bool DepuracionLocal(string strIP_Servidor, string strDsc_IP, string strDBName, int intnumero, ref string strMessage, ref string strInformation)
        {
            bool boResult;
            try
            {
                Hashtable hsInsertUSP = (Hashtable)htSPConfig["SP_DEPURACIONLOCAL"];
                string sNombreSP = (string)hsInsertUSP["SP_DEPURACIONLOCAL"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["IP_SERVIDOR"], DbType.String, strIP_Servidor);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["cDsc_IP"], DbType.String, strDsc_IP); 
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["DBName"], DbType.String, strDBName);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["numero"], DbType.Int32, intnumero);
 
     
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
