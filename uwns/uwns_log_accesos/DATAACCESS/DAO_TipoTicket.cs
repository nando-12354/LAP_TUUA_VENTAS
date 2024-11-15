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
    public class DAO_TipoTicket : DAO_BaseDatos
    {
        #region Fields
        public List<TipoTicket> objListaTipoTicket;

        #endregion

        #region Constructors

        public DAO_TipoTicket(string sConfigSPPath)
            : base(sConfigSPPath)
        {
            objListaTipoTicket = new List<TipoTicket>();

        }
        public DAO_TipoTicket(Database vhelper, Database vhelperLocal, Hashtable vhtSPConfig)
              : base(vhelper, vhelperLocal, vhtSPConfig)
        {
              objListaTipoTicket = new List<TipoTicket>();

        }
        #endregion

        #region Methods

        /// <summary>
        /// Saves a record to the DAOTipoTicket table.
        /// </summary>
        public bool insertar(TipoTicket objTipoTicket)
        {

            try
            {
                Hashtable hsInsertUSP = (Hashtable)htSPConfig["TTSP_INSERT"];
                string sNombreSP = (string)hsInsertUSP["TTSP_INSERT"];


                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Dsc_Tipo_Ticket"], DbType.String, objTipoTicket.SNomTipoTicket);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Tip_Vuelo"], DbType.String, objTipoTicket.STipVuelo);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Tip_Pasajero"], DbType.String, objTipoTicket.STipPasajero);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Tip_Trasbordo"], DbType.String, objTipoTicket.STipTrasbordo);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Log_Usuario_Mod"], DbType.String, objTipoTicket.SLogUsuarioMod);

                isRegistrado = base.ejecutarTrxSP(objCommandWrapper);
                return isRegistrado;
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        /// <summary>
        /// Updates a record in the DAOTipoTicket table.
        /// </summary>
        public bool actualizar(TipoTicket objTipoTicket)
        {

            try
            {
                Hashtable hsUpdateUSP = (Hashtable)htSPConfig["TTSP_UPDATE"];
                string sNombreSP = (string)hsUpdateUSP["TTSP_UPDATE"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Cod_Tipo_Ticket"], DbType.String, objTipoTicket.SCodTipoTicket);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Dsc_Tipo_Ticket"], DbType.String, objTipoTicket.SNomTipoTicket);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Tip_Vuelo"], DbType.String, objTipoTicket.STipVuelo);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Tip_Estado"], DbType.String, objTipoTicket.STipEstado);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Tip_Pasajero"], DbType.String, objTipoTicket.STipPasajero);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Tip_Trasbordo"], DbType.String, objTipoTicket.STipTrasbordo);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Log_Usuario_Mod"], DbType.String, objTipoTicket.SLogUsuarioMod);

                isActualizado = base.mantenerSP(objCommandWrapper);
                return isActualizado;
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        /// <summary>
        /// Deletes a record from the DAOTipoTicket table by its primary key.
        /// </summary>
        public bool eliminar(string sCodTipoTicket)
        {

            try
            {
                Hashtable hsDeleteUSP = (Hashtable)htSPConfig["TTSP_DELETE"];
                string sNombreSP = (string)hsDeleteUSP["TTSP_DELETE"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                {
                    helper.AddInParameter(objCommandWrapper, (string)hsDeleteUSP["Cod_Tipo_Ticket"], DbType.String, sCodTipoTicket);
                };

                isActualizado = base.mantenerSP(objCommandWrapper);
                return isActualizado;
            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// Selects a single record from the DAOTipoTicket table.
        /// </summary>
        public TipoTicket obtener(string sCodTipoTicket)
        {
            TipoTicket objTipoTicket = null;
            IDataReader result;
            Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["TTSP_OBTENER_TIPO_TICKET"];
            string sNombreSP = (string)hsSelectByIdUSP["TTSP_OBTENER_TIPO_TICKET"];
            result = base.listarDataReaderSP(sNombreSP, sCodTipoTicket);

            while (result.Read())
            {
                objTipoTicket = poblar(result);

            }
            
            result.Close();
            return objTipoTicket;
        }

        public TipoTicket ObtenerPrecioTicket(string strTipoVuelo, string strTipoPas, string strTipoTrans)
        {
            TipoTicket objTipoTicket = null;
            IDataReader result;
            try
            {
                Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["TTSP_OBTENER_PRECIO_TICKET"];
                string sNombreSP = (string)hsSelectByIdUSP["TTSP_OBTENER_PRECIO_TICKET"];
                result = base.listarDataReaderSP(sNombreSP, strTipoVuelo, strTipoPas, strTipoTrans);

                while (result.Read())
                {
                    objTipoTicket = poblar(result);

                }
                
                result.Close();
                return objTipoTicket;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Selects all records from the DAOTipoTicket table.
        /// </summary>
        public List<TipoTicket> listar()
        {
            IDataReader objResult;
            Hashtable hsSelectAllUSP = (Hashtable)htSPConfig["TTSP_SELECTALL"];
            string sNombreSP = (string)hsSelectAllUSP["TTSP_SELECTALL"];
            objResult = base.listarDataReaderSP(sNombreSP, null);

            while (objResult.Read())
            {
                objListaTipoTicket.Add(poblar(objResult));

            }
            
            objResult.Close();
            return objListaTipoTicket;
        }

          public List<TipoTicket> listarTabla()
          {
                IDataReader objResult;
                Hashtable hsSelectAllUSP = (Hashtable)htSPConfig["TTSP_SELECTALL"];
                string sNombreSP = (string)hsSelectAllUSP["TTSP_SELECTALL"];
                objResult = base.listarDataReaderSP(sNombreSP, null);

                while (objResult.Read())
                {
                      objListaTipoTicket.Add(poblarTabla(objResult));

                }
                
                objResult.Close();
                return objListaTipoTicket;
          }


          public TipoTicket poblarTabla(IDataReader dataReader)
          {
                

                Hashtable htSelectAllUSP = (Hashtable)htSPConfig["TTSP_SELECTALL"];
                TipoTicket objTipoTicket = new TipoTicket();
                if (dataReader[(string)htSelectAllUSP["Cod_Tipo_Ticket"]] != DBNull.Value)
                      objTipoTicket.SCodTipoTicket = (string)dataReader[(string)htSelectAllUSP["Cod_Tipo_Ticket"]];
                if (dataReader[(string)htSelectAllUSP["Tip_Vuelo"]] != DBNull.Value)
                      objTipoTicket.STipVuelo = (string)dataReader[(string)htSelectAllUSP["Tip_Vuelo"]];
                if (dataReader[(string)htSelectAllUSP["Tip_Pasajero"]] != DBNull.Value)
                      objTipoTicket.STipPasajero = (string)dataReader[(string)htSelectAllUSP["Tip_Pasajero"]];
                if (dataReader[(string)htSelectAllUSP["Tip_Trasbordo"]] != DBNull.Value)
                      objTipoTicket.STipTrasbordo = (string)dataReader[(string)htSelectAllUSP["Tip_Trasbordo"]];
                if (dataReader[(string)htSelectAllUSP["Cod_Moneda"]] != DBNull.Value)
                      objTipoTicket.SCodMoneda = (string)dataReader[(string)htSelectAllUSP["Cod_Moneda"]];
                if (dataReader[(string)htSelectAllUSP["Imp_Precio"]] != DBNull.Value)
                      objTipoTicket.DImpPrecio = decimal.Parse(dataReader[(string)htSelectAllUSP["Imp_Precio"]].ToString());
                if (dataReader[(string)htSelectAllUSP["Tip_Estado"]] != DBNull.Value)
                      objTipoTicket.STipEstado = (string)dataReader[(string)htSelectAllUSP["Tip_Estado"]];
                if (dataReader[(string)htSelectAllUSP["Log_Usuario_Mod"]] != DBNull.Value)
                      objTipoTicket.SLogUsuarioMod = (string)dataReader[(string)htSelectAllUSP["Log_Usuario_Mod"]];
                if (dataReader[(string)htSelectAllUSP["Log_Fecha_Mod"]] != DBNull.Value)
                      objTipoTicket.SLogFechaMod = (string)dataReader[(string)htSelectAllUSP["Log_Fecha_Mod"]];
                if (dataReader[(string)htSelectAllUSP["Log_Hora_Mod"]] != DBNull.Value)
                      objTipoTicket.SLogHoraMod = (string)dataReader[(string)htSelectAllUSP["Log_Hora_Mod"]];
                if (dataReader[(string)htSelectAllUSP["Dsc_Tipo_Ticket"]] != DBNull.Value)
                      objTipoTicket.SNomTipoTicket = (string)dataReader[(string)htSelectAllUSP["Dsc_Tipo_Ticket"]];


                return objTipoTicket;
          }

        /// <summary>
        /// Selects all records from the DAOTipoTicket table.
        /// </summary>
        public DataTable listarAll()
        {

            DataTable result;
            Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["TTSP_SELECTALL"];
            string sNombreSP = (string)hsSelectByIdUSP["TTSP_SELECTALL"];
            result = base.ListarDataTableSP(sNombreSP, null);

            return result;
        }

        /// <summary>
        /// Creates a new instance of the DAOTipoTicket class and populates it with data from the specified SqlDataReader.
        /// </summary>
        public TipoTicket poblar(IDataReader dataReader)
        {
            Hashtable htSelectAllUSP = (Hashtable)htSPConfig["TTSP_SELECTALL"];
            TipoTicket objTipoTicket = new TipoTicket();
            if (dataReader[(string)htSelectAllUSP["Cod_Tipo_Ticket"]] != DBNull.Value)
                objTipoTicket.SCodTipoTicket = (string)dataReader[(string)htSelectAllUSP["Cod_Tipo_Ticket"]];
            if (dataReader[(string)htSelectAllUSP["Dsc_Tipo_Ticket"]] != DBNull.Value)
                objTipoTicket.SNomTipoTicket = (string)dataReader[(string)htSelectAllUSP["Dsc_Tipo_Ticket"]];
            if (dataReader[(string)htSelectAllUSP["Tip_Vuelo"]] != DBNull.Value)
                objTipoTicket.STipVuelo = (string)dataReader[(string)htSelectAllUSP["Tip_Vuelo"]];
            if (dataReader[(string)htSelectAllUSP["Imp_Precio"]] != DBNull.Value)
                objTipoTicket.DImpPrecio = decimal.Parse(dataReader[(string)htSelectAllUSP["Imp_Precio"]].ToString());
            if (dataReader[(string)htSelectAllUSP["Tip_Estado"]] != DBNull.Value)
                objTipoTicket.STipEstado = (string)dataReader[(string)htSelectAllUSP["Tip_Estado"]];
            if (dataReader[(string)htSelectAllUSP["Cod_Moneda"]] != DBNull.Value)
                objTipoTicket.SCodMoneda = (string)dataReader[(string)htSelectAllUSP["Cod_Moneda"]];
            if (dataReader[(string)htSelectAllUSP["Tip_Pasajero"]] != DBNull.Value)
                objTipoTicket.STipPasajero = (string)dataReader[(string)htSelectAllUSP["Tip_Pasajero"]];
            if (dataReader[(string)htSelectAllUSP["Tip_Trasbordo"]] != DBNull.Value)
                objTipoTicket.STipTrasbordo = (string)dataReader[(string)htSelectAllUSP["Tip_Trasbordo"]];
            if (dataReader[(string)htSelectAllUSP["Log_Fecha_Mod"]] != DBNull.Value)
                objTipoTicket.SLogFechaMod = (string)dataReader[(string)htSelectAllUSP["Log_Fecha_Mod"]];
            if (dataReader[(string)htSelectAllUSP["Log_Usuario_Mod"]] != DBNull.Value)
                objTipoTicket.SLogUsuarioMod = (string)dataReader[(string)htSelectAllUSP["Log_Usuario_Mod"]];
            if (dataReader[(string)htSelectAllUSP["Log_Hora_Mod"]] != DBNull.Value)
                objTipoTicket.SLogHoraMod = (string)dataReader[(string)htSelectAllUSP["Log_Hora_Mod"]];
            if (dataReader[(string)htSelectAllUSP["Dsc_Simbolo"]] != DBNull.Value)
                objTipoTicket.Dsc_Simbolo = (string)dataReader[(string)htSelectAllUSP["Dsc_Simbolo"]];


            return objTipoTicket;
        }

          

        #endregion
    }
}
