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
    public class DAO_Sincronizacion : DAO_BaseDatos
    {
        #region Fields

        public List<Sincronizacion> objListaSincronizacion;

        #endregion


        #region Constructors

        public DAO_Sincronizacion(string sConfigSPPath)
            : base(sConfigSPPath)
        {
            objListaSincronizacion = new List<Sincronizacion>();
        }

        public DAO_Sincronizacion(Database vhelper, Database vhelperLocal, Hashtable vhtSPConfig)
              : base(vhelper, vhelperLocal, vhtSPConfig)
        {
            objListaSincronizacion = new List<Sincronizacion>();
        }

        public DAO_Sincronizacion(Hashtable htSPConfig)
            :base(htSPConfig)
        {
            objListaSincronizacion = new List<Sincronizacion>();
        }
        #endregion


        #region Methods


        /// <summary>
        /// Saves a record to the TUA_Sincronizacion table.
        /// </summary>

        public DataTable ListarSincronizacion()
        {
            DataTable result;
            Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["CSSP_SINCRONIZACION_SEL"];
            string sNombreSP = (string)hsSelectByIdUSP["CSSP_SINCRONIZACION_SEL"];
            result = base.ListarDataTableSP(sNombreSP, null);


            return result;
        }

        public DataTable ListarFiltroSincronizacion(string sMolinete, string sEstado,
            string sTipoSincronizacion, string sTablaSincronizacion, string strFchDesde,
            string strFchHasta, string strHraDesde, string strHraHasta,            
            string CadFiltro, string sOrdenacion)
        {
   

            DataTable result;
            Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["CSSP_SINCRONIZACION_FILTRO"];
            string sNombreSP = (string)hsSelectByIdUSP["CSSP_SINCRONIZACION_FILTRO"];
            result = base.ListarDataTableSP(sNombreSP, sMolinete, sEstado, 
                sTipoSincronizacion, sTablaSincronizacion, 
                strFchDesde,strFchHasta,strHraDesde,strHraHasta,               
                CadFiltro, sOrdenacion);
           
        return result;
        }
        
        public bool actualizarestado(Sincronizacion objListaSincronizacion)
        {
            try
            {
                Hashtable hsUpdateUSP = (Hashtable)htSPConfig["UPDATE_ESTADO_SINCRONIZA"];
                string sNombreSP = (string)hsUpdateUSP["UPDATE_ESTADO_SINCRONIZA"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Cod_Sincronizacion"], DbType.String, objListaSincronizacion.SCodigoSincronizacion);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Tip_Estado"], DbType.String, objListaSincronizacion.STipoEstado);
                 isActualizado = base.mantenerSP(objCommandWrapper);
                return isActualizado;
            }
            catch (Exception)
            {
                throw;
            }
        }



       

        #endregion


    }
}
