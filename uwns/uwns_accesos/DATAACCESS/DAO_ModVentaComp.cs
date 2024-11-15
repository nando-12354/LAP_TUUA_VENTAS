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
    public class DAO_ModVentaComp : DAO_BaseDatos
    {
        #region Fields

        public List<ModVentaComp> objListaRoles;

        #endregion

        #region Constructors

        public DAO_ModVentaComp(string sConfigSPPath)
            : base(sConfigSPPath)
        {
            objListaRoles = new List<ModVentaComp>();
        }

        public DAO_ModVentaComp(Database vhelper, Database vhelperLocal, Hashtable vhtSPConfig)
              : base(vhelper, vhelperLocal, vhtSPConfig)
        {
              objListaRoles = new List<ModVentaComp>();
        }
        #endregion

        #region Methods

        /// <summary>
        /// Saves a record to the TUA_ModVentaComp table.
        /// </summary>
        public bool insertar(ModVentaComp objModComp)
        {
            try
            {
                Hashtable htInsertUSP = (Hashtable)htSPConfig["MODCOMPSP_INSERT"];
                string sNombreSP = (string)htInsertUSP["MODCOMPSP_INSERT"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                helper.AddInParameter(objCommandWrapper, (string)htInsertUSP["Cod_Compania"], DbType.String, objModComp.SCodCompania);
                helper.AddInParameter(objCommandWrapper, (string)htInsertUSP["Cod_Modalidad_Venta"], DbType.String, objModComp.SCodModalidadVenta);
                isRegistrado = base.mantenerSP(objCommandWrapper);
                return isRegistrado;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Updates a record in the TUA_ModVentaComp table.
        /// </summary>
        public bool actualizar(ModVentaComp objModComp)
        {
            try
            {
                Hashtable htUpdateUSP = (Hashtable)htSPConfig["MODCOMSP_UPDATE"];
                string sNombreSP = (string)htUpdateUSP["MODCOMSP_UPDATE"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                helper.AddInParameter(objCommandWrapper, (string)htUpdateUSP["Cod_Compania"], DbType.String, objModComp.SCodCompania);
                helper.AddInParameter(objCommandWrapper, (string)htUpdateUSP["Cod_Modalidad_Venta"], DbType.String, objModComp.SCodModalidadVenta);
                helper.AddInParameter(objCommandWrapper, (string)htUpdateUSP["Dsc_Valor_AcumuladoS"], DbType.String, objModComp.SDscValorAcumulado);
                isActualizado = base.mantenerSP(objCommandWrapper);
                return isActualizado;
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Deletes a record from the TUA_ModVentaComp table by its primary key.
        /// </summary>
        public bool eliminar(string sCodCompania, string sCodModalidadVenta)
        {
            try
            {
                Hashtable htDeleteUSP = (Hashtable)htSPConfig["MODCOMSP_DELETE"];
                string sNombreSP = (string)htDeleteUSP["MODCOMSP_DELETE"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);

                helper.AddInParameter(objCommandWrapper, (string)htDeleteUSP["Cod_Compania"], DbType.String, sCodCompania);
                helper.AddInParameter(objCommandWrapper, (string)htDeleteUSP["Cod_Modalidad_Venta"], DbType.String, sCodModalidadVenta);

                isActualizado = base.mantenerSP(objCommandWrapper);
                return isActualizado;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }


        /// <summary>
        /// Selects a single record from the TUA_ModVentaComp table.
        /// </summary>


        public List<ModVentaComp> ListarCompaniaxModVenta(string strCodModVenta, string strTipComp)
        {

            List<ModVentaComp> lista = new List<ModVentaComp>();
            IDataReader result;
            Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["MODVENTACOMP_SELECTBYMODVENTA"];
            string sNombreSP = (string)hsSelectByIdUSP["MODVENTACOMP_SELECTBYMODVENTA"];
            result = base.listarDataReaderSP(sNombreSP, strCodModVenta, strTipComp);

            while (result.Read())
            {
                lista.Add(poblar(result));
            }
            
            result.Close();
            return lista;
        }




        public ModVentaComp obtener(string sCodCompania, string strNomModalidad)
        {

            ModVentaComp objModVentaComp = null;
            IDataReader result;
            Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["MODVENTACOMP_SELECT"];
            string sNombreSP = (string)hsSelectByIdUSP["MODVENTACOMP_SELECT"];
            result = base.listarDataReaderSP(sNombreSP, sCodCompania, strNomModalidad);

            while (result.Read())
            {
                objModVentaComp = poblarxNomModalidad(result);

            }

            result.Close();
            return objModVentaComp;
        }


        public List<ModVentaComp> ListarModVentaCompxCompañia(string sCodCompania)
        {

            List<ModVentaComp> lista = new List<ModVentaComp>();
            IDataReader result;
            Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["MODVENTACOMP_SELECT_BY_COMPANIA"];
            string sNombreSP = (string)hsSelectByIdUSP["MODVENTACOMP_SELECT_BY_COMPANIA"];
            result = base.listarDataReaderSP(sNombreSP, sCodCompania);

            while (result.Read())
            {
                lista.Add(poblarxNomModalidad(result));
            }

            result.Close();
            return lista;
        }


        protected ModVentaComp poblar(IDataReader dataReader)
        {
            Hashtable htSelectAllUSP = (Hashtable)htSPConfig["MODVENTACOMP_SELECTALL"];
            ModVentaComp objModVentaComp = new ModVentaComp();
            objModVentaComp.SCodCompania = (string)dataReader[(string)htSelectAllUSP["Cod_Compania"]];
            objModVentaComp.SCodModalidadVenta = (string)dataReader[(string)htSelectAllUSP["Cod_Modalidad_Venta"]];
            if (dataReader[(string)htSelectAllUSP["Dsc_Valor_Acumulado"]] != DBNull.Value)
            {
                objModVentaComp.SDscValorAcumulado = (string)dataReader[(string)htSelectAllUSP["Dsc_Valor_Acumulado"]];
            }
            objModVentaComp.Tip_Compania = (string)dataReader[(string)htSelectAllUSP["Tip_Compania"]];
            objModVentaComp.Dsc_Compania = (string)dataReader[(string)htSelectAllUSP["Dsc_Compania"]];
            if (dataReader[(string)htSelectAllUSP["Dsc_Ruc"]] != DBNull.Value)
            {
                objModVentaComp.Dsc_Ruc = (string)dataReader[(string)htSelectAllUSP["Dsc_Ruc"]];
            }
            return objModVentaComp;
        }


        protected ModVentaComp poblarxNomModalidad(IDataReader dataReader)
        {
            Hashtable htSelectAllUSP = (Hashtable)htSPConfig["MODVENTACOMP_SELECTALL"];
            ModVentaComp objModVentaComp = new ModVentaComp();
            objModVentaComp.SCodCompania = (string)dataReader[(string)htSelectAllUSP["Cod_Compania"]];
            objModVentaComp.SCodModalidadVenta = (string)dataReader[(string)htSelectAllUSP["Cod_Modalidad_Venta"]];
            if (dataReader[(string)htSelectAllUSP["Dsc_Valor_Acumulado"]] != DBNull.Value)
            {
                objModVentaComp.SDscValorAcumulado = (string)dataReader[(string)htSelectAllUSP["Dsc_Valor_Acumulado"]];
            }
            return objModVentaComp;
        }

        public DataTable ListarAtributosWSAerolinea(string strAerolinea)
        {
            try
            {
                DataTable result;
                Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["PGSP_OBTENERPARMAERO"];
                string sNombreSP = (string)hsSelectByIdUSP["PGSP_OBTENERPARMAERO"];
                result = base.ListarDataTableSP(sNombreSP, strAerolinea);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
