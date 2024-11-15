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
    public class DAO_TemporalBoardingPass : DAO_BaseDatos
    {
        #region Fields

        public List<TemporalBoardingPass> objListaTemporalBoardingPass;

        #endregion

        #region Constructors

        public DAO_TemporalBoardingPass(string sConfigSPPath)
            : base(sConfigSPPath)
        {
            objListaTemporalBoardingPass = new List<TemporalBoardingPass>();
        }

        public DAO_TemporalBoardingPass(Database vhelper, Database vhelperLocal, Hashtable vhtSPConfig)
            : base(vhelper, vhelperLocal, vhtSPConfig)
        {
            objListaTemporalBoardingPass = new List<TemporalBoardingPass>();
        }

        #endregion

        #region Adcionales G&S
        /// <summary>
        /// Obtains data from a parameter and then inserts it to a temporal table.
        /// </summary>
        public int Ingresar(TemporalBoardingPass objTemporalBoardingPass)
        {
            int intResultado = 0;
            try
            {
                DbCommand objComando = helper.GetStoredProcCommand("usp_ingresaboardingpassxdsctrama");
                helper.AddInParameter(objComando, "Num_Checkin", DbType.String, objTemporalBoardingPass.NumCheckin);
                helper.AddInParameter(objComando, "Num_Vuelo", DbType.String, objTemporalBoardingPass.NumVuelo);
                helper.AddInParameter(objComando, "Num_Asiento", DbType.String, objTemporalBoardingPass.NumAsiento);
                helper.AddInParameter(objComando, "Nom_Pasajero", DbType.String, objTemporalBoardingPass.NomPasajero);
                helper.AddInParameter(objComando, "Fch_Vuelo", DbType.String, objTemporalBoardingPass.FchVuelo);
                helper.AddOutParameter(objComando, "RetVal", DbType.Int32, 8);
                objComando.CommandTimeout = 0;
                if (helper.ExecuteNonQuery(objComando) != 0)
                {
                    intResultado = Convert.ToInt32(helper.GetParameterValue(objComando, "RetVal"));
                }
                return intResultado;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Obtains data from a parameter and then inserts it to a temporal table.
        /// </summary>
        public void Eliminar(TemporalBoardingPass objTemporalBoardingPass)
        {
            try
            {
                DbCommand objComando = helper.GetStoredProcCommand("usp_reh_pcs_temporalboardingpass_del");
                helper.AddInParameter(objComando, "Cod_Numero_Bcbp", DbType.String, objTemporalBoardingPass.CodNumeroBcbp);
                objComando.CommandTimeout = 0;
                helper.ExecuteNonQuery(objComando);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Selects all records from the AlarmaGenerada table.
        /// </summary>
        public DataTable ListarAll(TemporalBoardingPass objTemporalBoardingPass)
        {
            DataSet dtsQuery;
            try
            {
                IDataReader dtrQuery = null;
                DbCommand objComando = helper.GetStoredProcCommand("usp_reh_pcs_temporalBoardingpass_sel");
                helper.AddInParameter(objComando, "Cod_Compania", DbType.String, objTemporalBoardingPass.CodCompania);
                helper.AddInParameter(objComando, "Fch_Vuelo", DbType.String, objTemporalBoardingPass.FchVuelo);
                helper.AddInParameter(objComando, "Num_Vuelo", DbType.String, objTemporalBoardingPass.NumVuelo);
                objComando.CommandTimeout = 0;
                dtsQuery = helper.ExecuteDataSet(objComando);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtsQuery.Tables[0];
        }
        /// <summary>
        /// Selects all records from the AlarmaGenerada table.
        /// </summary>
        public bool insertarRehabilitacionBCBP(BoardingBcbpEstHist boardingBcbpEstHist, int flag, int sizeOutput)
        {
            bool isRegistrado = false;
            try
            {
                DbCommand objComando = helper.GetStoredProcCommand("usp_reh_pcs_bcbp_ins_ampliacion");
                helper.AddInParameter(objComando, "ListaBCBPs", DbType.String, boardingBcbpEstHist.SListaBcbPs);
                helper.AddInParameter(objComando, "Causal_Rehabilitacion", DbType.String, (flag == 1 ? boardingBcbpEstHist.SCausalRehabilitacion : null));
                helper.AddInParameter(objComando, "Causal_Rehabilitaciones", DbType.String, (flag == 0 ? boardingBcbpEstHist.SCausalRehabilitacion : null));
                helper.AddInParameter(objComando, "Flg_Tipo", DbType.String, flag.ToString());
                helper.AddInParameter(objComando, "Log_Usuario_Mod", DbType.String, boardingBcbpEstHist.SLogUsuarioMod);
                helper.AddOutParameter(objComando, "OutputListaBCBPs", DbType.String, sizeOutput);
                helper.AddOutParameter(objComando, "OutputListaNroOpe", DbType.String, 3300);
                helper.AddOutParameter(objComando, "OutLogFechaRehabi", DbType.String, 20);
                helper.AddInParameter(objComando, "estado_asoc", DbType.String, boardingBcbpEstHist.SEstadoAsoc);
                helper.AddInParameter(objComando, "nro_vuelo_asoc", DbType.String, boardingBcbpEstHist.SNroVueloAsoc);
                helper.AddInParameter(objComando, "nro_asiento_asoc", DbType.String, boardingBcbpEstHist.SNroAsientoAsoc);
                helper.AddInParameter(objComando, "pasajero_asoc", DbType.String, boardingBcbpEstHist.SPasajeroAsoc);
                helper.AddInParameter(objComando, "fecha_vuelo_asoc", DbType.String, boardingBcbpEstHist.SFechaVueloAsoc);
                helper.AddInParameter(objComando, "compania_asoc", DbType.String, boardingBcbpEstHist.SCompaniaAsoc);
                helper.AddInParameter(objComando, "Lst_Bloqueados", DbType.String, boardingBcbpEstHist.Lst_Bloqueados);
                helper.AddInParameter(objComando, "Cod_Modulo_Mod", DbType.String, boardingBcbpEstHist.Cod_Modulo_Mod);
                helper.AddInParameter(objComando, "Cod_SubModulo_Mod", DbType.String, boardingBcbpEstHist.Cod_SubModulo_Mod);
                isRegistrado = Convert.ToBoolean(helper.ExecuteNonQuery(objComando));
                if (isRegistrado)
                {
                    boardingBcbpEstHist.SListaBcbPs = (string)helper.GetParameterValue(objComando, "OutputListaBCBPs");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return isRegistrado;
        }
        #endregion
    }
}
