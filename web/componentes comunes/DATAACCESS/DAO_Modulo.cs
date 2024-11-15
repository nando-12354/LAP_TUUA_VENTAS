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
    public class DAO_Modulo : DAO_BaseDatos
    {
        #region Fields

        public List<Modulo> objListaModulos;

        #endregion

        #region Constructors

        public DAO_Modulo(string  htSPConfig)
            : base(htSPConfig)
        {
            objListaModulos = new List<Modulo>();
            //this.htSPConfig = htSPConfig;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Selects all records from the TUA_Modulo table.
        /// </summary>
        public List<Modulo> listar()
        {
            IDataReader objResult;
            Hashtable htSelectAllUSP = (Hashtable)htSPConfig["MODSP_SELECTALL"];
            string sNombreSP = (string)htSelectAllUSP["MODSP_SELECTALL"];
            objResult = base.listarDataReaderSP(sNombreSP, null);

            while (objResult.Read())
            {
                objListaModulos.Add(poblar(objResult));
            }
            objResult.Dispose();
            objResult.Close();
            return objListaModulos;
        }


        /// <summary>
        /// Selects all records from the TUA_Modulo table.
        /// </summary>
        /// <returns></returns>
        public DataTable ListarAllModulo()
        {
            DataTable result;
            Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["MODSP_SELECTALL"];
            string sNombreSP = (string)hsSelectByIdUSP["MODSP_SELECTALL"];
            result = base.ListarDataTableSP(sNombreSP, null);


            return result;
        }


        /// <summary>
        /// Creates a new instance of the TUA_Modulo class and populates it with data from the specified SqlDataReader.
        /// </summary>
        public Modulo poblar(IDataReader dataReader)
        {
            Hashtable htSelectAllUSP = (Hashtable)htSPConfig["MODSP_SELECTALL"];
            Modulo objModulo = new Modulo();
            if (dataReader[(string)htSelectAllUSP["Cod_Modulo"]] != DBNull.Value)
            objModulo.SCodModulo = (string)dataReader[(string)htSelectAllUSP["Cod_Modulo"]];
            if (dataReader[(string)htSelectAllUSP["Dsc_Modulo"]] != DBNull.Value)
            objModulo.SDscModulo = (string)dataReader[(string)htSelectAllUSP["Dsc_Modulo"]];
            if (dataReader[(string)htSelectAllUSP["Id_Modulo"]] != DBNull.Value)
            objModulo.SIdModulo = (string)dataReader[(string)htSelectAllUSP["Id_Modulo"]];
            if (dataReader[(string)htSelectAllUSP["Dsc_Icono"]] != DBNull.Value)
            objModulo.SDscIcono = (string)dataReader[(string)htSelectAllUSP["Dsc_Icono"]];
            if (dataReader[(string)htSelectAllUSP["Dsc_Icono_Out"]] != DBNull.Value)
            objModulo.SDscIcono_Out = (string)dataReader[(string)htSelectAllUSP["Dsc_Icono_Out"]];
            if (dataReader[(string)htSelectAllUSP["Tip_Modulo"]] != DBNull.Value)
            objModulo.STipModulo = (string)dataReader[(string)htSelectAllUSP["Tip_Modulo"]];
            if (dataReader[(string)htSelectAllUSP["Dsc_Archivo"]] != DBNull.Value)
            objModulo.SDscArchivo = (string)dataReader[(string)htSelectAllUSP["Dsc_Archivo"]];
            if (dataReader[(string)htSelectAllUSP["Dsc_Texto_Ayuda"]] != DBNull.Value)
            objModulo.SDscTextoAyuda = (string)dataReader[(string)htSelectAllUSP["Dsc_Texto_Ayuda"]];
            if (dataReader[(string)htSelectAllUSP["Num_Posicion"]] != DBNull.Value)
            objModulo.INumPosicion = Int32.Parse((string)dataReader[(string)htSelectAllUSP["Num_Posicion"]]);

            return objModulo;
        }

        #endregion
    }
}
