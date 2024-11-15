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
    public class DAO_BoardingBcbpEstHist : DAO_BaseDatos
    {
        #region Fields

        public List<BoardingBcbpEstHist> objListaBoardingBcbp;

        #endregion

        #region Constructors

        public DAO_BoardingBcbpEstHist(string sConfigSPPath)
               : base(sConfigSPPath)
        {
            objListaBoardingBcbp = new List<BoardingBcbpEstHist>();
        }

        public DAO_BoardingBcbpEstHist(Database vhelper, Database vhelperLocal, Hashtable vhtSPConfig)
              : base(vhelper, vhelperLocal, vhtSPConfig)
        {
              objListaBoardingBcbp = new List<BoardingBcbpEstHist>();
        }

        #endregion
        

        #region Methods

        public DataTable DetalleBoardingEstHist(string Cod_Compania, string Num_Vuelo, string Fch_Vuelo,
                                                string Num_Asiento, string Nom_Pasajero)
        {
              DataTable objResult;
              Hashtable hsSelectAllUSP = (Hashtable)htSPConfig["BPSP_OBTENER_BOARDINGESTHIST"];
              string sNombreSP = (string)hsSelectAllUSP["BPSP_OBTENER_BOARDINGESTHIST"];
              objResult = base.ListarDataTableSP(sNombreSP, Cod_Compania, Num_Vuelo, Fch_Vuelo,
                                                 Num_Asiento,Nom_Pasajero);
              objResult.Dispose();
              return objResult;
        }


        public DataTable DetalleBoardingEstHist(String Num_Secuencial_Bcbp)
        {
            DataTable objResult;
            Hashtable hsSelectAllUSP = (Hashtable)htSPConfig["BPSP_OBTENER_xNUMSEC_BOARDINGESTHIST"];
            string sNombreSP = (string)hsSelectAllUSP["BPSP_OBTENER_xNUMSEC_BOARDINGESTHIST"];
            objResult = base.ListarDataTableSP(sNombreSP, Num_Secuencial_Bcbp);
            objResult.Dispose();
            return objResult;
        }

        public bool insertarRehabilitacionBCBP(BoardingBcbpEstHist boardingBcbpEstHist, int flag, int sizeOutput)
        {
            try
            {
                Hashtable hsInsertUSP = (Hashtable)htSPConfig["REHSP_BCBPINS"];
                string sNombreSP = (string)hsInsertUSP["REHSP_BCBPINS"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["ListaBCBPs"], DbType.String, boardingBcbpEstHist.SListaBcbPs);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Causal_Rehabilitacion"], DbType.String, (flag == 1 ? boardingBcbpEstHist.SCausalRehabilitacion : null));
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Causal_Rehabilitaciones"], DbType.String, (flag == 0 ? boardingBcbpEstHist.SCausalRehabilitacion : null));
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Flg_Tipo"], DbType.String, flag.ToString());
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Log_Usuario_Mod"], DbType.String, boardingBcbpEstHist.SLogUsuarioMod);
                helper.AddOutParameter(objCommandWrapper, (string)hsInsertUSP["OutputListaBCBPs"], DbType.String, sizeOutput);

                isRegistrado = base.mantenerSP(objCommandWrapper);
                if (isRegistrado)
                {
                    boardingBcbpEstHist.SListaBcbPs = (string)helper.GetParameterValue(objCommandWrapper, (string)hsInsertUSP["OutputListaBCBPs"]);

                }
                return isRegistrado;
            }
            catch (Exception)
            {

                throw;
            }

        }

        #endregion

    }
}
