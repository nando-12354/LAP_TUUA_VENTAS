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
    public class DAO_Depuracion : DAO_BaseDatos
    {
        #region Fields

        public List<Depuracion> objListaDepuracion;

        #endregion


        #region Constructors

        public DAO_Depuracion(string sConfigSPPath)
            : base(sConfigSPPath)
        {
            objListaDepuracion = new List<Depuracion>();
        }

        public DAO_Depuracion(Database vhelper, Database vhelperLocal, Hashtable vhtSPConfig)
            : base(vhelper, vhelperLocal, vhtSPConfig)
        {
            objListaDepuracion = new List<Depuracion>();
        }

        public DAO_Depuracion(Hashtable htSPConfig)
            : base(htSPConfig)
        {
            objListaDepuracion = new List<Depuracion>();
        }
        #endregion


        #region Methods


        /// <summary>
        /// Saves a record to the TUA_Sincronizacion table.
        /// </summary>

        public DataTable ListarFiltroDepuracion(string sMolinete, string sEstado,
            string sTablaSincronizacion, string strFchDesde,
            string strFchHasta, string strHraDesde, string strHraHasta,
            string CadFiltro, string sOrdenacion)
        {


            DataTable result;
            Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["CSSP_DEPURACION_FILTRO"];
            string sNombreSP = (string)hsSelectByIdUSP["CSSP_DEPURACION_FILTRO"];
            result = base.ListarDataTableSP(sNombreSP, sMolinete, sEstado,
                 sTablaSincronizacion,
                strFchDesde, strFchHasta, strHraDesde, strHraHasta,
                CadFiltro, sOrdenacion);

            return result;
        }




        #endregion


    }
}
