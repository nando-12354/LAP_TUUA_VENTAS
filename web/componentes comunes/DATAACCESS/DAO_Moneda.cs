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
    public class DAO_Moneda : DAO_BaseDatos
    {
        #region Fields

        public List<Moneda> objListaMonedas;

        #endregion

        #region Constructors

        public DAO_Moneda(string sConfigSPPath)
            : base(sConfigSPPath)
        {
            objListaMonedas = new List<Moneda>();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Saves a record to the TUA_Moneda table.
        /// </summary>
        public bool insertar(Moneda objMoneda)
        {
            try
            {
                Hashtable htInsertUSP = (Hashtable)htSPConfig["MSP_INSERT"];
                string sNombreSP = (string)htInsertUSP["MSP_INSERT"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                helper.AddInParameter(objCommandWrapper, (string)htInsertUSP["Cod_Moneda"], DbType.String, objMoneda.SCodMoneda);
                helper.AddInParameter(objCommandWrapper, (string)htInsertUSP["Dsc_Moneda"], DbType.String, objMoneda.SDscMoneda);
                helper.AddInParameter(objCommandWrapper, (string)htInsertUSP["Dsc_Simbolo"], DbType.String, objMoneda.SDscSimbolo);
                helper.AddInParameter(objCommandWrapper, (string)htInsertUSP["Dsc_Nemonico"], DbType.String, objMoneda.SNemonico);
                helper.AddInParameter(objCommandWrapper, (string)htInsertUSP["Tip_Estado"], DbType.String, objMoneda.STipEstado);
                helper.AddInParameter(objCommandWrapper, (string)htInsertUSP["Log_Usuario_Mod"], DbType.String, objMoneda.SLogUsuarioMod);
                helper.AddInParameter(objCommandWrapper, (string)htInsertUSP["Log_Fecha_Mod"], DbType.String, objMoneda.DtLogFechaMod);
                helper.AddInParameter(objCommandWrapper, (string)htInsertUSP["Log_Hora_Mod"], DbType.String, objMoneda.SLogHoraMod);
                isRegistrado = base.mantenerSP(objCommandWrapper);
                return isRegistrado;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Updates a record in the TUA_Moneda table.
        /// </summary>
        public bool actualizar(Moneda objMoneda)
        {
            try
            {
                Hashtable htUpdateUSP = (Hashtable)htSPConfig["MSP_UPDATE"];
                string sNombreSP = (string)htUpdateUSP["MSP_UPDATE"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                helper.AddInParameter(objCommandWrapper, (string)htUpdateUSP["Cod_Moneda"], DbType.String, objMoneda.SCodMoneda);
                helper.AddInParameter(objCommandWrapper, (string)htUpdateUSP["Dsc_Moneda"], DbType.String, objMoneda.SDscMoneda);
                helper.AddInParameter(objCommandWrapper, (string)htUpdateUSP["Dsc_Simbolo"], DbType.String, objMoneda.SDscSimbolo);
                helper.AddInParameter(objCommandWrapper, (string)htUpdateUSP["Dsc_Nemonico"], DbType.String, objMoneda.SNemonico);
                helper.AddInParameter(objCommandWrapper, (string)htUpdateUSP["Tip_Estado"], DbType.String, objMoneda.STipEstado);
                helper.AddInParameter(objCommandWrapper, (string)htUpdateUSP["Log_Usuario_Mod"], DbType.String, objMoneda.SLogUsuarioMod);
                helper.AddInParameter(objCommandWrapper, (string)htUpdateUSP["Log_Fecha_Mod"], DbType.String, objMoneda.DtLogFechaMod);
                helper.AddInParameter(objCommandWrapper, (string)htUpdateUSP["Log_Hora_Mod"], DbType.String, objMoneda.SLogHoraMod);
                //helper.AddInParameter(objCommandWrapper, (string)htUpdateUSP["Cod_Modulo"], DbType.String, objMoneda.SModulo);
                //helper.AddInParameter(objCommandWrapper, (string)htUpdateUSP["Cod_SubModulo"], DbType.String, objMoneda.SSubModulo);
                isActualizado = base.mantenerSP(objCommandWrapper);
                return isActualizado;
            }
            catch (Exception)
            {
                throw;
            }

        }

        /// <summary>
        /// Deletes a record from the TUA_Moneda table by its primary key.
        /// </summary>
        public bool eliminar(string sCodMoneda)
        {
            try
            {
                Hashtable htDeleteUSP = (Hashtable)htSPConfig["MSP_DELETE"];
                string sNombreSP = (string)htDeleteUSP["MSP_DELETE"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);

                helper.AddInParameter(objCommandWrapper, (string)htDeleteUSP["Cod_Moneda"], DbType.String, sCodMoneda);


                isActualizado = base.mantenerSP(objCommandWrapper);
                return isActualizado;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }


        /// <summary>
        /// Selects a single record from the TUA_Moneda table.
        /// </summary>
        public Moneda obtener(string sCodMoneda)
        {

            Moneda objMoneda = null;
            IDataReader result;
            try
            {
                Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["MSP_OBTENER_MONEDA"];
                string sNombreSP = (string)hsSelectByIdUSP["MSP_OBTENER_MONEDA"];
                result = base.listarDataReaderSP(sNombreSP, sCodMoneda);

                while (result.Read())
                {
                    objMoneda = poblar(result);
                }
                result.Dispose();
                result.Close();
                return objMoneda;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Selects all records from the TUA_Moneda table.
        /// </summary>
        public List<Moneda> listar()
        {
            IDataReader objResult;
            try
            {
                Hashtable htSelectAllUSP = (Hashtable)htSPConfig["MONTCSP_SELECTALL"];
                string sNombreSP = (string)htSelectAllUSP["MONTCSP_SELECTALL"];
                objResult = base.listarDataReaderSP(sNombreSP, null);

                while (objResult.Read())
                {
                    objListaMonedas.Add(poblar(objResult));

                }
                objResult.Dispose();
                objResult.Close();
                return objListaMonedas;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Moneda> listarMonedasxTipoTicket()
        {
            IDataReader objResult;
            try
            {
                Hashtable htSelectAllUSP = (Hashtable)htSPConfig["MONTCSP_SELECTALLXTIPOTICKET"];
                string sNombreSP = (string)htSelectAllUSP["MONTCSP_SELECTALLXTIPOTICKET"];
                objResult = base.listarDataReaderSP(sNombreSP, null);

                while (objResult.Read())
                {
                    objListaMonedas.Add(poblarxTipoTicket(objResult));

                }
                objResult.Dispose();
                objResult.Close();
                return objListaMonedas;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<Moneda> ListarMonedasInter()
        {
            IDataReader objResult;
            try
            {
                Hashtable htSelectAllUSP = (Hashtable)htSPConfig["MSP_SELECTINTER"];
                string sNombreSP = (string)htSelectAllUSP["MSP_SELECTINTER"];
                objResult = base.listarDataReaderSP(sNombreSP, null);

                while (objResult.Read())
                {
                    objListaMonedas.Add(poblar(objResult));

                }
                objResult.Dispose();
                objResult.Close();
                return objListaMonedas;
            }
            catch (Exception)
            {

                throw;
            }
        }
        //public DataTable listar()
        //{
        //    DataTable objResult;
        //    Hashtable hsSelectAllUSP = (Hashtable)htSPConfig["MSP_SELECTALL"];
        //    string sNombreSP = (string)hsSelectAllUSP["MSP_SELECTALL"];
        //    objResult = base.ListarDataTableSP(sNombreSP, null);
        //    objResult.Dispose();

        //    return objResult;
        //}
        /// <summary>
        /// Creates a new instance of the TUA_Moneda class and populates it with data from the specified SqlDataReader.
        /// </summary>
        protected Moneda poblar(IDataReader dataReader)
        {
            Hashtable htSelectAllUSP = (Hashtable)htSPConfig["MSP_SELECTALL"];
            Moneda objMoneda = new Moneda();
            objMoneda.SCodMoneda = (string)dataReader[(string)htSelectAllUSP["Cod_Moneda"]];
            objMoneda.SDscMoneda = (string)dataReader[(string)htSelectAllUSP["Dsc_Moneda"]];
            objMoneda.SDscSimbolo = (string)dataReader[(string)htSelectAllUSP["Dsc_Simbolo"]];
            objMoneda.SLogUsuarioMod = (string)dataReader[(string)htSelectAllUSP["Log_Usuario_Mod"]];
            //objMoneda.DtLogFechaMod = Convert.ToDateTime(dataReader[(string)htSelectAllUSP["Log_Fecha_Mod"]]);
            objMoneda.SLogHoraMod = (string)dataReader[(string)htSelectAllUSP["Log_Hora_Mod"]];

            return objMoneda;
        }

        protected Moneda poblarxTipoTicket(IDataReader dataReader)
        {
            Hashtable htSelectAllUSP = (Hashtable)htSPConfig["MSP_SELECTALL"];
            Moneda objMoneda = new Moneda();
            objMoneda.SCodMoneda = (string)dataReader[(string)htSelectAllUSP["Cod_Moneda"]];
            objMoneda.SDscMoneda = (string)dataReader[(string)htSelectAllUSP["Dsc_Moneda"]];
            objMoneda.SDscSimbolo = (string)dataReader[(string)htSelectAllUSP["Dsc_Simbolo"]];
            //objMoneda.SLogUsuarioMod = (string)dataReader[(string)htSelectAllUSP["Log_Usuario_Mod"]];
            //objMoneda.DtLogFechaMod = Convert.ToDateTime(dataReader[(string)htSelectAllUSP["Log_Fecha_Mod"]]);
            //objMoneda.SLogHoraMod = (string)dataReader[(string)htSelectAllUSP["Log_Hora_Mod"]];

            return objMoneda;
        }

        public DataTable listarAllMonedas()
        {
            DataTable objResult;
            Hashtable hsSelectAllUSP = (Hashtable)htSPConfig["MSP_SELECTALL"];
            string sNombreSP = (string)hsSelectAllUSP["MSP_SELECTALL"];
            objResult = base.ListarDataTableSP(sNombreSP, null);
            objResult.Dispose();
            return objResult;
        }


        public DataTable obtenerDetalleMoneda(string sCodMoneda)
        {
            DataTable objResult;
            Hashtable hsSelectAllUSP = (Hashtable)htSPConfig["MSP_DETALLEMONEDA"];
            string sNombreSP = (string)hsSelectAllUSP["MSP_DETALLEMONEDA"];
            objResult = base.ListarDataTableSP(sNombreSP, sCodMoneda);
            objResult.Dispose();
            return objResult;
        }

        public DataTable obtenerMoneda(string sCodMoneda)
        {
              DataTable result;

              String strSQL = "select Cod_Moneda from TUA_Moneda " +
                                " where Cod_Moneda='"+ sCodMoneda+"' and Tip_Estado='1' ";
              result = base.ListarDataTableQY(strSQL);
              return result;
        }

        public string obtenerDescripcionMoneda(string sCodMoneda)
        {
            DataTable dt;

            String strSQL = "SELECT Dsc_Moneda + ' ( ' + Dsc_Simbolo + ' )' AS DescripcionMoneda " +
                            "FROM TUA_Moneda WHERE Cod_Moneda = '" + sCodMoneda + "'";
            dt = base.ListarDataTableQY(strSQL);

            string sDscMoneda = sCodMoneda;

            if (dt.Rows.Count > 0)
            {
                sDscMoneda = dt.Rows[0]["DescripcionMoneda"].ToString();
            }
            return sDscMoneda;
        }

        #endregion
    }
}
