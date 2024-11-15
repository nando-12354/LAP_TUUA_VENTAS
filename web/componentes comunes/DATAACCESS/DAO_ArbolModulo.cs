using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.VisualBasic;
using LAP.TUUA.ENTIDADES;
using LAP.TUUA.UTIL;
using System.Collections;

namespace LAP.TUUA.DAO
{
    public class DAO_ArbolModulo : DAO_BaseDatos
    {
        #region Fields

        public List<ArbolModulo> objListaArbolModulo;

        #endregion

        #region Constructors

        public DAO_ArbolModulo(string htSPConfig)
            : base(htSPConfig)
        {
            objListaArbolModulo = new List<ArbolModulo>();
        }

        public DAO_ArbolModulo(Hashtable htSPConfig)
            : base(htSPConfig)
        {
            objListaArbolModulo = new List<ArbolModulo>();
        }

        #endregion

        #region Methods

       
        /// <summary>
        /// Selects all records from the ArbolModulo table.
        /// </summary>
        public DataTable listar(string sCodUsuario)
        {
            DataTable objResult;
            Hashtable hsSelectAllUSP = (Hashtable)htSPConfig["AMSP_SELECTALL_BY_CODUSUARIO"];
            string sNombreSP = (string)hsSelectAllUSP["AMSP_SELECTALL_BY_CODUSUARIO"];
            objResult = base.ListarDataTableSP(sNombreSP, sCodUsuario);
            objResult.Dispose();

            return objResult;
        }

        /// <summary>
        /// Selects all records from the ArbolModulo table.
        /// </summary>
        public DataTable listarArbolModuloxRol(string sCodRol)
        {
            DataTable objResult;
            Hashtable hsSelectAllUSP = (Hashtable)htSPConfig["AMSP_SELECTALL_BY_CODROL"];
            string sNombreSP = (string)hsSelectAllUSP["AMSP_SELECTALL_BY_CODROL"];
            objResult = base.ListarDataTableSP(sNombreSP, sCodRol);
            objResult.Dispose();

            return objResult;
        }


        /// <summary>
        /// Selects a single record from the objArbolModulo table.
        /// </summary>

        public ArbolModulo obtener(string sCodModulo, string sCodProceso, string sCodRol)
        {
            ArbolModulo objArbolModulo = null;
            IDataReader result;
            Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["AMSP_OBTENER_ARBOLMODULO"];
            string sNombreSP = (string)hsSelectByIdUSP["AMSP_OBTENER_ARBOLMODULO"];
            result = base.listarDataReaderSP(sNombreSP, sCodModulo, sCodProceso, sCodRol);

            while (result.Read())
            {
                objArbolModulo = poblar(result);

            }
            result.Dispose();
            result.Close();
            return objArbolModulo;
        }

        public List<ArbolModulo> ListarPerfilVenta(string strUsuario)
        {
            IDataReader objResult;
            Hashtable htSelectAllUSP = (Hashtable)htSPConfig["AMSP_SELECTPERFILVENTA"];
            string sNombreSP = (string)htSelectAllUSP["AMSP_SELECTPERFILVENTA"];
            objResult = base.listarDataReaderSP(sNombreSP, strUsuario);

            while (objResult.Read())
            {
                objListaArbolModulo.Add(poblarByUsuario(objResult));

            }
            objResult.Dispose();
            objResult.Close();
            return objListaArbolModulo;
        }
		
        public List<ArbolModulo> ListarPerfilArchiving(string strUsuario)
        {
            IDataReader objResult;
            Hashtable htSelectAllUSP = (Hashtable)htSPConfig["SP_SELECTPERFILARCHIVAMIENTO"];
            string sNombreSP = (string)htSelectAllUSP["SP_SELECTPERFILARCHIVAMIENTO"];
            objResult = base.listarDataReaderSP(sNombreSP, strUsuario);

            while (objResult.Read())
            {
                objListaArbolModulo.Add(poblarByUsuario(objResult));

            }
            objResult.Dispose();
            objResult.Close();
            return objListaArbolModulo;
        }		

        /// <summary>
        /// Creates a new instance of the TUA_ArbolModulo class and populates it with data from the specified SqlDataReader.
        /// </summary>

        public ArbolModulo poblar(IDataReader dataReader)
        {
            Hashtable htSelectAllUSP = (Hashtable)htSPConfig["AMSP_SELECTALL"];
            ArbolModulo objArbolModulo = new ArbolModulo();

            if (dataReader[(string)htSelectAllUSP["Cod_Proceso"]] != DBNull.Value)
                objArbolModulo.SCodProceso = (string)dataReader[(string)htSelectAllUSP["Cod_Proceso"]];
            if (dataReader[(string)htSelectAllUSP["Cod_Proceso_Padre"]] != DBNull.Value)
                objArbolModulo.SCodProcesoPadre = (string)dataReader[(string)htSelectAllUSP["Cod_Proceso_Padre"]];
            if (dataReader[(string)htSelectAllUSP["Cod_Modulo"]] != DBNull.Value)
                objArbolModulo.SCodModulo = (string)dataReader[(string)htSelectAllUSP["Cod_Modulo"]];
            if (dataReader[(string)htSelectAllUSP["Id_Proceso"]] != DBNull.Value)
                objArbolModulo.SIdProceso = (string)dataReader[(string)htSelectAllUSP["Id_Proceso"]];
            if (dataReader[(string)htSelectAllUSP["Dsc_Proceso"]] != DBNull.Value)
                objArbolModulo.SDscProceso = (string)dataReader[(string)htSelectAllUSP["Dsc_Proceso"]];
            if (dataReader[(string)htSelectAllUSP["Tip_Nivel"]] != DBNull.Value)
                objArbolModulo.ITipNivel = (Int32)dataReader[(string)htSelectAllUSP["Tip_Nivel"]];
            if (dataReader[(string)htSelectAllUSP["Tip_Estado"]] != DBNull.Value)
                objArbolModulo.STipEstado = (string)dataReader[(string)htSelectAllUSP["Tip_Estado"]];
            if (dataReader[(string)htSelectAllUSP["Flg_Permitido"]] != DBNull.Value)
                objArbolModulo.SFlgPermitido = (string)dataReader[(string)htSelectAllUSP["Flg_Permitido"]];
            if (dataReader[(string)htSelectAllUSP["Dsc_Archivo"]] != DBNull.Value)
                objArbolModulo.SDscArchivo = (string)dataReader[(string)htSelectAllUSP["Dsc_Archivo"]];
            if (dataReader[(string)htSelectAllUSP["Dsc_Icono"]] != DBNull.Value)
                objArbolModulo.SDscIcono = (string)dataReader[(string)htSelectAllUSP["Dsc_Icono"]];
            if (dataReader[(string)htSelectAllUSP["Dsc_Texto_Ayuda"]] != DBNull.Value)
                objArbolModulo.SDscTextoAyuda = (string)dataReader[(string)htSelectAllUSP["Dsc_Texto_Ayuda"]];
            if (dataReader[(string)htSelectAllUSP["Dsc_Ind_Critic"]] != DBNull.Value)
                objArbolModulo.SDscIndCritic = (string)dataReader[(string)htSelectAllUSP["Dsc_Ind_Critic"]];
            if (dataReader[(string)htSelectAllUSP["Dsc_Color_Critic"]] != DBNull.Value)
                objArbolModulo.SDscColorCritic = (string)dataReader[(string)htSelectAllUSP["Dsc_Color_Critic"]];
            if (dataReader[(string)htSelectAllUSP["Dsc_Tab_Filtro"]] != DBNull.Value)
                objArbolModulo.SDscTabFiltro = (string)dataReader[(string)htSelectAllUSP["Dsc_Tab_Filtro"]];
            if (dataReader[(string)htSelectAllUSP["Dsc_Licencia"]] != DBNull.Value)
                objArbolModulo.SDscLicencia = (string)dataReader[(string)htSelectAllUSP["Dsc_Licencia"]];
            if (dataReader[(string)htSelectAllUSP["Num_Pos_Modulo"]] != DBNull.Value)
                objArbolModulo.INumPosModulo = (Int32)dataReader[(string)htSelectAllUSP["Num_Pos_Modulo"]];
            if (dataReader[(string)htSelectAllUSP["Num_Pos_Proceso"]] != DBNull.Value)
                objArbolModulo.INumPosProceso = (Int32)dataReader[(string)htSelectAllUSP["Num_Pos_Proceso"]];
            if (dataReader[(string)htSelectAllUSP["Dsc_Modulo"]] != DBNull.Value)
                objArbolModulo.SDscModulo = (string)dataReader[(string)htSelectAllUSP["Dsc_Modulo"]];
            if (dataReader[(string)htSelectAllUSP["Tip_Modulo"]] != DBNull.Value)
                objArbolModulo.STipModulo = (string)dataReader[(string)htSelectAllUSP["Tip_Modulo"]];
            return objArbolModulo;
        }

        public ArbolModulo poblarByUsuario(IDataReader dataReader)
        {
            Hashtable htSelectAllUSP = (Hashtable)htSPConfig["AMSP_SELECTALL_BY_CODUSUARIO"];
            ArbolModulo objArbolModulo = new ArbolModulo();

            if (dataReader[(string)htSelectAllUSP["Cod_Proceso"]] != DBNull.Value)
                objArbolModulo.SCodProceso = (string)dataReader[(string)htSelectAllUSP["Cod_Proceso"]];
            if (dataReader[(string)htSelectAllUSP["Cod_Modulo"]] != DBNull.Value)
                objArbolModulo.SCodModulo = (string)dataReader[(string)htSelectAllUSP["Cod_Modulo"]];
            if (dataReader[(string)htSelectAllUSP["Id_Proceso"]] != DBNull.Value)
                objArbolModulo.SIdProceso = (string)dataReader[(string)htSelectAllUSP["Id_Proceso"]];
            if (dataReader[(string)htSelectAllUSP["Dsc_Proceso"]] != DBNull.Value)
                objArbolModulo.SDscProceso = (string)dataReader[(string)htSelectAllUSP["Dsc_Proceso"]];
            if (dataReader[(string)htSelectAllUSP["Dsc_Archivo"]] != DBNull.Value)
                objArbolModulo.SDscArchivo = (string)dataReader[(string)htSelectAllUSP["Dsc_Archivo"]];
            if (dataReader[(string)htSelectAllUSP["Dsc_Icono"]] != DBNull.Value)
                objArbolModulo.SDscIcono = (string)dataReader[(string)htSelectAllUSP["Dsc_Icono"]];
            if (dataReader[(string)htSelectAllUSP["Dsc_Modulo"]] != DBNull.Value)
                objArbolModulo.SDscModulo = (string)dataReader[(string)htSelectAllUSP["Dsc_Modulo"]];
            if (dataReader[(string)htSelectAllUSP["Flg_Permitido"]] != DBNull.Value)
                objArbolModulo.SFlgPermitido = (string)dataReader[(string)htSelectAllUSP["Flg_Permitido"]];
            return objArbolModulo;

        }

        #endregion
    }
}
