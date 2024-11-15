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
    public class DAO_TemporalTicket : DAO_BaseDatos
    {
        #region Fields

        public List<TemporalTicket> objListaTemporalTicket;

        #endregion

        #region Constructors

        public DAO_TemporalTicket(string sConfigSPPath)
            : base(sConfigSPPath)
        {
            objListaTemporalTicket = new List<TemporalTicket>();
        }

        public DAO_TemporalTicket(Database vhelper, Database vhelperLocal, Hashtable vhtSPConfig)
            : base(vhelper, vhelperLocal, vhtSPConfig)
        {
            objListaTemporalTicket = new List<TemporalTicket>();
        }

        #endregion

        #region Adcionales G&S
        /// <summary>
        /// Obtains data from a parameter and then inserts it to a temporal table.
        /// </summary>
        public int Ingresar(TemporalTicket objTemporalTicket)
        {
            int intResultado = 0;
            try
            {
                DbCommand objComando = helper.GetStoredProcCommand("usp_ingresaticketxcod");
                helper.AddInParameter(objComando, "Cod_Numero_Ticket", DbType.String, objTemporalTicket.CodNumeroTicket);
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
        public void Eliminar(TemporalTicket objTemporalTicket)
        {
            try
            {
                DbCommand objComando = helper.GetStoredProcCommand("usp_reh_pcs_temporalticket_del");
                helper.AddInParameter(objComando, "Cod_Numero_Ticket", DbType.String, objTemporalTicket.CodNumeroTicket);
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
        public DataTable ListarAll(TemporalTicket objTemporalTicket)
        {
            DataSet dtsQuery;
            try
            {
                DbCommand objComando = helper.GetStoredProcCommand("usp_reh_pcs_temporaltickets_sel");
                helper.AddInParameter(objComando, "Fch_Vuelo", DbType.String, objTemporalTicket.FchVuelo);
                objComando.CommandTimeout = 0;
                dtsQuery = helper.ExecuteDataSet(objComando);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtsQuery.Tables[0];
        }

        public bool insertarRehabilitacionTicket(TicketEstHist objTicketEstHist, int flag, int sizeOutput)
        {
            bool isRegistrado = false;
            try
            {
                DbCommand objComando = helper.GetStoredProcCommand("usp_reh_pcs_ticket_ins");
                helper.AddInParameter(objComando, "Cod_Numero_Tickets", DbType.String, objTicketEstHist.SCodNumeroTicket);
                helper.AddInParameter(objComando, "Dsc_Num_Vuelo", DbType.String, "");
                helper.AddInParameter(objComando, "Causal_Rehabilitacion", DbType.String, objTicketEstHist.SCausalRehabilitacion);
                helper.AddInParameter(objComando, "Dsc_Num_Vuelos", DbType.String, "");
                helper.AddInParameter(objComando, "Causal_Rehabilitaciones", DbType.String, null);
                helper.AddInParameter(objComando, "Flg_Tipo", DbType.String, flag.ToString());
                helper.AddInParameter(objComando, "Log_Usuario_Mod", DbType.String, objTicketEstHist.SLogUsuarioMod);
                helper.AddOutParameter(objComando, "OutputListaTicket", DbType.String, sizeOutput);
                isRegistrado = Convert.ToBoolean(helper.ExecuteNonQuery(objComando));
                if (isRegistrado)
                {
                    objTicketEstHist.SCodNumeroTicket = (string)helper.GetParameterValue(objComando, "OutputListaTicket");
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
