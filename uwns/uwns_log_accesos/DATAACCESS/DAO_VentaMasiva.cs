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
    public class DAO_VentaMasiva : DAO_BaseDatos
    {
        #region Fields
        public List<VentaMasiva> objListaVentaMasiva;
        #endregion

        #region Constructors

        public DAO_VentaMasiva(string sConfigSPPath)
            : base(sConfigSPPath)
        {
            objListaVentaMasiva = new List<VentaMasiva>();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Saves a record to the DAOVentaMasiva table.
        /// </summary>
        public bool insertar(VentaMasiva objVentaMasiva)
        {

            try
            {
                Hashtable hsInsertUSP = (Hashtable)htSPConfig["VENTMASSP_INSERT"];
                string sNombreSP = (string)hsInsertUSP["VENTMASSP_INSERT"];


                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);

                //helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Cod_Venta_Masiva"], DbType.String, objVentaMasiva.SCodVentaMasiva);
                //helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Fch_Venta"], DbType.String, objVentaMasiva.DtFchVenta);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Can_Venta"], DbType.Int32, objVentaMasiva.DCanVenta);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Tip_Pago"], DbType.String, objVentaMasiva.Tip_Pago);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Num_Cheque_Trans"], DbType.String, objVentaMasiva.Num_Cheque_Trans);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Imp_Monto_Venta"], DbType.Decimal, objVentaMasiva.DImpMontoVenta);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Moneda"], DbType.String, objVentaMasiva.SCodMoneda);
                //helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Num_Rango_Inicial"], DbType.String, objVentaMasiva.INumRangoInicial);
                //helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Num_Rango_Final"], DbType.String, objVentaMasiva.INumRangoFinal);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Compania"], DbType.String, objVentaMasiva.SCodCompania);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Usuario"], DbType.String, objVentaMasiva.SCodUsuario);
                helper.AddOutParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Venta_Masiva"], DbType.String, 5);
                
                isRegistrado = base.ejecutarTrxSP(objCommandWrapper);
                objVentaMasiva.SCodVentaMasiva = (string)helper.GetParameterValue(objCommandWrapper, (string)hsInsertUSP["Cod_Venta_Masiva"]);
                return isRegistrado;
            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// Updates a record in the DAOVentaMasiva table.
        /// </summary>
        public bool actualizar(VentaMasiva objVentaMasiva)
        {

            try
            {
                Hashtable hsUpdateUSP = (Hashtable)htSPConfig["VENMASSP_UPDATE"];
                string sNombreSP = (string)hsUpdateUSP["VENMASSP_UPDATE"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);

                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Cod_Venta_Masiva"], DbType.String, objVentaMasiva.SCodVentaMasiva);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Num_Rango_Inicial"], DbType.String, objVentaMasiva.INumRangoInicial);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Num_Rango_Final"], DbType.String, objVentaMasiva.INumRangoFinal);


                isActualizado = base.mantenerSP(objCommandWrapper);
                return isActualizado;

            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// Deletes a record from the DAOVentaMasiva table by its primary key.
        /// </summary>
        public bool eliminar(string sCodVentaMasiva)
        {

            try
            {
                Hashtable hsDeleteUSP = (Hashtable)htSPConfig["USP_DELETE"];
                string sNombreSP = (string)hsDeleteUSP["USP_DELETE"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);

                helper.AddInParameter(objCommandWrapper, (string)hsDeleteUSP["Cod_Venta_Masiva"], DbType.String, sCodVentaMasiva);


                isActualizado = base.mantenerSP(objCommandWrapper);
                return isActualizado;

            }
            catch (Exception)
            {

                throw;
            }

        }



        /// <summary>
        /// Selects a single record from the DAOVentaMasiva table.
        /// </summary>
        public VentaMasiva obtener(string sCodVentaMasiva)
        {
            VentaMasiva objVentaMasiva = null;
            IDataReader result;
            Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["USP_OBTENER_VENTA_MASIVA"];
            string sNombreSP = (string)hsSelectByIdUSP["USP_OBTENER_VENTA_MASIVA"];
            result = base.listarDataReaderSP(sNombreSP, sCodVentaMasiva);

            while (result.Read())
            {
                objVentaMasiva = poblar(result);

            }
            
            result.Close();
            return objVentaMasiva;
        }

        /// <summary>
        /// Selects all records from the DAOVentaMasiva table.
        /// </summary>
        public List<VentaMasiva> listar()
        {
            IDataReader objResult;
            Hashtable hsSelectAllUSP = (Hashtable)htSPConfig["USP_SELECTALL"];
            string sNombreSP = (string)hsSelectAllUSP["USP_SELECTALL"];
            objResult = base.listarDataReaderSP(sNombreSP, null);

            while (objResult.Read())
            {
                objListaVentaMasiva.Add(poblar(objResult));

            }
            
            objResult.Close();
            return objListaVentaMasiva;
        }


        /// <summary>
        /// Creates a new instance of the DAOVentaMasiva class and populates it with data from the specified SqlDataReader.
        /// </summary>
        public VentaMasiva poblar(IDataReader dataReader)
        {

            Hashtable htSelectAllUSP = (Hashtable)htSPConfig["USP_SELECTALL"];
            VentaMasiva objVentaMasiva = new VentaMasiva();
            objVentaMasiva.SCodVentaMasiva = (string)dataReader[(string)htSelectAllUSP["Cod_Venta_Masiva"]];
            objVentaMasiva.DtFchVenta = (string)dataReader[(string)htSelectAllUSP["Fch_Venta"]];
            objVentaMasiva.DCanVenta = Int32.Parse(dataReader[(string)htSelectAllUSP["Can_Venta"]].ToString());
            objVentaMasiva.DImpMontoVenta = (decimal)dataReader[(string)htSelectAllUSP["Imp_Monto_Venta"]];
            objVentaMasiva.SCodMoneda = (string)dataReader[(string)htSelectAllUSP["Cod_Moneda"]];
            objVentaMasiva.INumRangoInicial = (string)dataReader[(string)htSelectAllUSP["Num_Rango_Inicial"]];
            objVentaMasiva.INumRangoFinal = (string)dataReader[(string)htSelectAllUSP["Num_Rango_Final"]];
            objVentaMasiva.SCodCompania = (string)dataReader[(string)htSelectAllUSP["Cod_Compania"]];
            objVentaMasiva.SCodUsuario = (string)dataReader[(string)htSelectAllUSP["Cod_Usuario"]];

            return objVentaMasiva;
        }

        #endregion
    }
}
