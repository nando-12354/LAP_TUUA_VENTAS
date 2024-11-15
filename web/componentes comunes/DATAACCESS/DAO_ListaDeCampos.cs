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
    public class DAO_ListaDeCampos : DAO_BaseDatos
    {
        #region Fields

        public List<ListaDeCampo> objLDeCampos;
        #endregion

        #region Constructors
        public DAO_ListaDeCampos(string sConfigSPPath)
            : base(sConfigSPPath)
        {
            objLDeCampos = new List<ListaDeCampo>();
        }

        public DAO_ListaDeCampos(Hashtable htSPConfig)
            : base(htSPConfig)
        {
            objLDeCampos = new List<ListaDeCampo>();
        }

        public DAO_ListaDeCampos(Database vhelper, Database vhelperLocal, Hashtable vhtSPConfig)
              : base(vhelper,vhelperLocal,  vhtSPConfig)
        {
              objLDeCampos = new List<ListaDeCampo>();
        }
        #endregion

        #region Methods

        /// <summary>
        /// Saves a record to the objPrecioTicket table.
        /// </summary>
        public bool insertar(ListaDeCampo objLDeCampos, int intTipo)
        {
            try
            {
                Hashtable hsMetodoUSP;
                string sNombreSP = "";
                if (intTipo == 1)
                {//Update
                    hsMetodoUSP = (Hashtable)htSPConfig["LCSP_UPDATE"];
                    sNombreSP = (string)hsMetodoUSP["LCSP_UPDATE"];                   
                }
                else
                {//Insert
                    hsMetodoUSP = (Hashtable)htSPConfig["LCSP_INSERT"];
                    sNombreSP = (string)hsMetodoUSP["LCSP_INSERT"];
                }
                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                helper.AddInParameter(objCommandWrapper, (string)hsMetodoUSP["Nom_Campo"], DbType.String, objLDeCampos.SNomCampo);
                helper.AddInParameter(objCommandWrapper, (string)hsMetodoUSP["Cod_Campo"], DbType.String, objLDeCampos.SCodCampo);
                helper.AddInParameter(objCommandWrapper, (string)hsMetodoUSP["Cod_Relativo"], DbType.String, objLDeCampos.SCodRelativo);
                helper.AddInParameter(objCommandWrapper, (string)hsMetodoUSP["Dsc_Campo"], DbType.String, objLDeCampos.SDscCampo);
                helper.AddInParameter(objCommandWrapper, (string)hsMetodoUSP["Log_Usuario_Mod"], DbType.String, objLDeCampos.SLogUsuarioMod);                
                isRegistrado = base.mantenerSP(objCommandWrapper);
                return isRegistrado;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Updates a record in the objPrecioTicket table.
        /// </summary>
        public bool actualizar(ListaDeCampo objLDeCampos)
        {
            try
            {
                Hashtable hsUpdateUSP = (Hashtable)htSPConfig["LCSP_UPDATE"];
                string sNombreSP = (string)hsUpdateUSP["LCSP_UPDATE"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Nom_Campo"], DbType.String, objLDeCampos.SNomCampo);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Cod_Campo"], DbType.String, objLDeCampos.SCodCampo);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Cod_Relativo"], DbType.String, objLDeCampos.SCodRelativo);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Dsc_Campo"], DbType.String, objLDeCampos.SDscCampo);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Log_Usuario_Mod"], DbType.String, objLDeCampos.SLogUsuarioMod);
                //helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Log_Fecha_Mod"], DbType.DateTime, objLDeCampos.DtLogFechaMod);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Log_Hora_Mod"], DbType.String, objLDeCampos.SLogHoraMod);
                isActualizado = base.mantenerSP(objCommandWrapper);
                return isActualizado;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool actualizarHoras(ListaDeCampo objLDeCampos)
        {
            try
            {
                Hashtable hsUpdateUSP = (Hashtable)htSPConfig["SINCRONIZACION_UPDATE"];
                string sNombreSP = (string)hsUpdateUSP["SINCRONIZACION_UPDATE"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Nom_Campo"], DbType.String, objLDeCampos.SNomCampo);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Cod_Campo"], DbType.String, objLDeCampos.SCodCampo);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Cod_Relativo"], DbType.String, objLDeCampos.SCodRelativo);
                //helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Dsc_Campo"], DbType.String, objLDeCampos.SDscCampo);
                //helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Log_Usuario_Mod"], DbType.String, objLDeCampos.SLogUsuarioMod);
                ////helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Log_Fecha_Mod"], DbType.DateTime, objLDeCampos.DtLogFechaMod);
                //helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Log_Hora_Mod"], DbType.String, objLDeCampos.SLogHoraMod);
                isActualizado = base.mantenerSP(objCommandWrapper);
                return isActualizado;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Deletes a record from the objPrecioTicket table by its primary key.
        /// </summary>
        public bool eliminar(string sNomCampo, string sCodCampo)
        {
            try
            {
                Hashtable htDeleteUSP = (Hashtable)htSPConfig["LCSP_DELETE"];
                string sNombreSP = (string)htDeleteUSP["LCSP_DELETE"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                helper.AddInParameter(objCommandWrapper, (string)htDeleteUSP["Nom_Campo"], DbType.String, sNomCampo);
                helper.AddInParameter(objCommandWrapper, (string)htDeleteUSP["Cod_Campo"], DbType.String, sCodCampo);

                isActualizado = base.mantenerSP(objCommandWrapper);
                return isActualizado;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        /// <summary>
        /// Selects all records from the objPrecioTicket table.
        /// </summary>

        public List<ListaDeCampo> listar()
        {
            IDataReader objResult;
            Hashtable hsSelectAllUSP = (Hashtable)htSPConfig["LCSP_SELECTALL"];
            string sNombreSP = (string)hsSelectAllUSP["LCSP_SELECTALL"];
            objResult = base.listarDataReaderSP(sNombreSP, null);

            while (objResult.Read())
            {
                objLDeCampos.Add(poblar(objResult));

            }
            objResult.Dispose();
            objResult.Close();
            return objLDeCampos;
        }

        /// <summary>
        /// Selects all records from the objPrecioTicket table.
        /// </summary>

        public List<ListaDeCampo> obtenerListadeCampo(string sNomCampo)
        {
            IDataReader objResult;
            List<ListaDeCampo> objListaCampo = new List<ListaDeCampo>();
            Hashtable hsSelectAllUSP = (Hashtable)htSPConfig["LCSP_OBTENER_LISTACAMPONOMBRE"];
            string sNombreSP = (string)hsSelectAllUSP["LCSP_OBTENER_LISTACAMPONOMBRE"];
            objResult = base.listarDataReaderSP(sNombreSP, sNomCampo);

            while (objResult.Read())
            {
                objListaCampo.Add(poblar(objResult));

            }
            objResult.Dispose();
            objResult.Close();
            return objListaCampo;
        }

        /// <summary>
        /// Selects a single record from the objPrecioTicket table.
        /// </summary>


        public ListaDeCampo obtener(string sNomCampo, string sCodCampo)
        {

            ListaDeCampo objLDeCampos = null;
            IDataReader result;
            Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["LCSP_OBTENER_LISTACAMPO"];
            string sNombreSP = (string)hsSelectByIdUSP["LCSP_OBTENER_LISTACAMPO"];
            result = base.listarDataReaderSP(sNombreSP, sNomCampo, sCodCampo);

            while (result.Read())
            {
                objLDeCampos = poblar(result);

            }
            result.Dispose();
            result.Close();
            return objLDeCampos;
        }





        /// <summary>
        /// Selects a single record from the objPrecioTicket table.
        /// </summary>

        public DataTable obtenerListaxNombre(string sNomCampo)
        {
            DataTable result;
            Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["LCSP_OBTENER_LISTACAMPONOMBRE"];
            string sNombreSP = (string)hsSelectByIdUSP["LCSP_OBTENER_LISTACAMPONOMBRE"];
            result = base.ListarDataTableSP(sNombreSP, sNomCampo);


            return result;
        }

        public string obtenerDescripcionTipo(string sNombreCampo, string sCodCampo)
        {
            DataTable dt;

            String strSQL = "SELECT Dsc_Campo FROM TUA_ListaDeCampos " +
                "WHERE Nom_Campo = '" + sNombreCampo + "' AND Cod_Campo = '" + sCodCampo + "'";

            dt = base.ListarDataTableQY(strSQL);

            string sDscTipoCia = sCodCampo;

            if (dt.Rows.Count > 0)
            {
                sDscTipoCia = dt.Rows[0]["Dsc_Campo"].ToString();
            }
            return sDscTipoCia;
        }

        /// <summary>
        /// Creates a new instance of the objPrecioTicket class and populates it with data from the specified SqlDataReader.
        /// </summary>
        public ListaDeCampo poblar(IDataReader dataReader)
        {
            Hashtable htSelectAllUSP = (Hashtable)htSPConfig["LCSP_SELECTALL"];
            ListaDeCampo objLDeCampos = new ListaDeCampo();
            if (dataReader[(string)htSelectAllUSP["Nom_Campo"]] != DBNull.Value)
                objLDeCampos.SNomCampo = (string)dataReader[(string)htSelectAllUSP["Nom_Campo"]];
            if (dataReader[(string)htSelectAllUSP["Cod_Campo"]] != DBNull.Value)
                objLDeCampos.SCodCampo = (string)dataReader[(string)htSelectAllUSP["Cod_Campo"]];
            if (dataReader[(string)htSelectAllUSP["Cod_Relativo"]] != DBNull.Value)
                objLDeCampos.SCodRelativo = (string)dataReader[(string)htSelectAllUSP["Cod_Relativo"]];
            if (dataReader[(string)htSelectAllUSP["Dsc_Campo"]] != DBNull.Value)
                objLDeCampos.SDscCampo = (string)dataReader[(string)htSelectAllUSP["Dsc_Campo"]];
            return objLDeCampos;
        }

        #endregion

        #region Additional_Methods

        public DataTable obtenerLista(string sNomCampo, string sCodCampo)
        {
            DataTable result;

            //Begin Build Query --------------------
            String strSQL;
            String strWhere = "";

            strSQL = "SELECT tlc.Nom_Campo, tlc.Cod_Campo, tlc.Cod_Relativo, tlc.Dsc_Campo, tlc.Log_Usuario_Mod, tlc.Log_Fecha_Mod, tlc.Log_Hora_Mod, " +
                         " CASE WHEN (tu.Ape_Usuario IS NULL AND tu.Nom_Usuario IS NULL) THEN tlc.Log_Usuario_Mod" +
                              " ELSE ISNULL(tu.Nom_Usuario,' - ') +', '+ ISNULL(tu.Ape_Usuario, ' - ') END Nom_Usuario" +
                     " FROM TUA_ListaDeCampos tlc LEFT JOIN TUA_Usuario tu ON tlc.Log_Usuario_Mod = tu.Cod_Usuario ";

            if (sNomCampo != null && sNomCampo.Length > 0)
            {
                strWhere = strWhere + " AND tlc.Nom_Campo = '" + sNomCampo.Trim() + "'";
            }
            if (sCodCampo != null && sCodCampo.Length > 0)
            {
                strWhere = strWhere + " AND tlc.Cod_Campo = '" + sCodCampo.Trim() + "'";
            }
            if (strWhere.Length > 0) strWhere = strWhere.Substring(4);
            
            if (strWhere.Length > 0 )
                strSQL = strSQL + " WHERE " + strWhere + " ORDER BY tlc.Nom_Campo, tlc.Cod_Campo ";
            else
                strSQL = strSQL + " ORDER BY tlc.Nom_Campo, tlc.Cod_Campo";
            //End Build Query --------------------

            result = base.ListarDataTableQY(strSQL);
            return result;
        } 


       
        #endregion

        //EAG
        public DataTable obtenerListaxNombreOrderByDesc(string sNomCampo)
        {
            DataTable result;
            Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["REH_OBTENER_LISTACAMPONOMBRE"];
            string sNombreSP = (string)hsSelectByIdUSP["REH_OBTENER_LISTACAMPONOMBRE"];
            result = base.ListarDataTableSP(sNombreSP, sNomCampo);


            return result;
        }
        //EAG
    
    }
}