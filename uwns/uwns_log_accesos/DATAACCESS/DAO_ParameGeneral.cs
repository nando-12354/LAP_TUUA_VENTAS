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
    public class DAO_ParameGeneral : DAO_BaseDatos
    {
        #region Fields

        public List<ParameGeneral> objListaParametros;

        #endregion

        #region Constructors

        public DAO_ParameGeneral(string sConfigSPPath)
            : base(sConfigSPPath)
        {
            objListaParametros = new List<ParameGeneral>();
        }

        public DAO_ParameGeneral(Hashtable htSPConfig)
            : base(htSPConfig)
        {
            objListaParametros = new List<ParameGeneral>();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Selects a single record from the TUA_ParameGeneral table.
        /// </summary>
        public ParameGeneral obtener(string sCodParam)
        {

            ParameGeneral objParametro = null;
            IDataReader result;
            Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["PGSP_OBTENERPARAMETROSGENERALES"];
            string sNombreSP = (string)hsSelectByIdUSP["PGSP_OBTENERPARAMETROSGENERALES"];
            result = base.listarDataReaderSP(sNombreSP, sCodParam);

            while (result.Read())
            {
                objParametro = poblar(result);
            }

            result.Close();
            return objParametro;
        }

        /// <summary>
        /// Selects all records from the TUA_ParameGeneral table.
        /// </summary>
        public List<ParameGeneral> listar()
        {
            IDataReader objResult;
            Hashtable htSelectAllUSP = (Hashtable)htSPConfig["PGSP_SELECTALL"];
            string sNombreSP = (string)htSelectAllUSP["PGSP_SELECTALL"];
            objResult = base.listarDataReaderSP(sNombreSP, null);

            while (objResult.Read())
            {
                objListaParametros.Add(poblar(objResult));
            }

            objResult.Close();
            return objListaParametros;
        }


        /// <summary>
        /// Selects all records from the TUA_ParameGeneral table.
        /// </summary>
        public List<ParameGeneral> listarAtributosGenerales()
        {
            IDataReader objResult;
            Hashtable htSelectAllUSP = (Hashtable)htSPConfig["PGSP_SELECT_ATRIB_GENER"];
            string sNombreSP = (string)htSelectAllUSP["PGSP_SELECT_ATRIB_GENER"];
            objResult = base.listarDataReaderSP(sNombreSP, null);

            while (objResult.Read())
            {
                objListaParametros.Add(poblar(objResult));
            }

            objResult.Close();
            return objListaParametros;
        }

        /// <summary>
        /// Creates a new instance of the TUA_ParameGeneral class and populates it with data from the specified SqlDataReader.
        /// </summary>
        public ParameGeneral poblar(IDataReader dataReader)
        {
            Hashtable htSelectAllUSP = (Hashtable)htSPConfig["PGSP_SELECTALL"];
            ParameGeneral objParam = new ParameGeneral();
            if (dataReader[(string)htSelectAllUSP["Identificador"]] != DBNull.Value)
            objParam.SIdentificador = (string)dataReader[(string)htSelectAllUSP["Identificador"]];
            if (dataReader[(string)htSelectAllUSP["Descripcion"]] != DBNull.Value)
            objParam.SDescripcion = (string)dataReader[(string)htSelectAllUSP["Descripcion"]];
            if (dataReader[(string)htSelectAllUSP["TipoParametro"]] != DBNull.Value)
            objParam.STipoParametro = (string)dataReader[(string)htSelectAllUSP["TipoParametro"]];
            if (dataReader[(string)htSelectAllUSP["TipoParametroDet"]] != DBNull.Value)
            objParam.STipoParametroDet = (string)dataReader[(string)htSelectAllUSP["TipoParametroDet"]];
            if (dataReader[(string)htSelectAllUSP["TipoValor"]] != DBNull.Value)
            objParam.STipoValor = (string)dataReader[(string)htSelectAllUSP["TipoValor"]];
            if (dataReader[(string)htSelectAllUSP["Valor"]] != DBNull.Value)
            objParam.SValor = (string)dataReader[(string)htSelectAllUSP["Valor"]];
            if (dataReader[(string)htSelectAllUSP["CampoLista"]] != DBNull.Value)
            objParam.SCampoLista = (string)dataReader[(string)htSelectAllUSP["CampoLista"]];
            if (dataReader[(string)htSelectAllUSP["IdentificadorPadre"]] != DBNull.Value)
            objParam.SIdentificadorPadre = (string)dataReader[(string)htSelectAllUSP["IdentificadorPadre"]];
            if (dataReader[(string)htSelectAllUSP["Log_Usuario_Mod"]] != DBNull.Value)
            objParam.SLog_Usuario_Mod = (string)dataReader[(string)htSelectAllUSP["Log_Usuario_Mod"]];
            if (dataReader[(string)htSelectAllUSP["Log_Fecha_Mod"]] != DBNull.Value)
            objParam.SLog_Fecha_Mod = (string)dataReader[(string)htSelectAllUSP["Log_Fecha_Mod"]];
            if (dataReader[(string)htSelectAllUSP["Log_Hora_Mod"]] != DBNull.Value)
            objParam.SLog_Hora_Mod = (string)dataReader[(string)htSelectAllUSP["Log_Hora_Mod"]];

            return objParam;
        }


        public DataTable ObtenerParametroGeneral(string sIdentificador)
        {
            DataTable result;
            Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["PARAMGEN_OBTENERVALORPARAMGENER"];
            string sNombreSP = (string)hsSelectByIdUSP["PARAMGEN_OBTENERVALORPARAMGENER"];
            result = base.ListarDataTableSP(sNombreSP, sIdentificador);


            return result;
        }


        public DataTable ListarAllParametroGeneral(string strParametro)
        {
            DataTable result;
            Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["PARAMGEN_LISTARALLPARAMEGENERAL"];
            string sNombreSP = (string)hsSelectByIdUSP["PARAMGEN_LISTARALLPARAMEGENERAL"];
            result = base.ListarDataTableSP(sNombreSP, strParametro);


            return result;
        }

        public DataTable DetalleParametroGeneralxIdentificador(string sIdentificador)
        {
            DataTable result;
            Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["PARAMGEN_DETALLEPARAMEGENERALXID"];
            string sNombreSP = (string)hsSelectByIdUSP["PARAMGEN_DETALLEPARAMEGENERALXID"];
            result = base.ListarDataTableSP(sNombreSP, sIdentificador);

            return result;
        }


        public bool grabarParametroGeneral(string sValoresFormulario, string sValoresGrilla, string sParametroVenta)
        {
            try
            {
                Hashtable htDeleteUSP = (Hashtable)htSPConfig["PGSP_GRABARPARAMETROSGENERALES"];
                string sNombreSP = (string)htDeleteUSP["PGSP_GRABARPARAMETROSGENERALES"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                helper.AddInParameter(objCommandWrapper, (string)htDeleteUSP["CadenaFormulario"], DbType.String, sValoresFormulario);
                helper.AddInParameter(objCommandWrapper, (string)htDeleteUSP["CadenaGrillas"], DbType.String, sValoresGrilla);
                helper.AddInParameter(objCommandWrapper, (string)htDeleteUSP["ParametroVenta"], DbType.String, sParametroVenta);
                isActualizado = base.mantenerSP(objCommandWrapper);
                return isActualizado;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public DataTable obtenerFecha()
        {
              DataTable result;
              String strSQL;
              strSQL = "select (SELECT SUBSTRING([dbo].[NTPFunction](),1,8))+(SELECT SUBSTRING([dbo].[NTPFunction](),10,15)) Fecha";
              result = base.ListarDataTableQY(strSQL);
              return result;
        }



        #endregion
    }
}
