using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

using LAP.TUUA.ENTIDADES;

namespace LAP.TUUA.DAO
{
    public class DAO_PrecioTicket : DAO_BaseDatos
    {
        #region Fields

        public List<PrecioTicket> objListaPrecioTicket;

        #endregion

        #region Constructors

        public DAO_PrecioTicket(string sConfigSPPathg)
            : base(sConfigSPPathg)
        {
            objListaPrecioTicket = new List<PrecioTicket>();
        }
        #endregion

        #region Methods
        
        /// <summary>
        /// Saves a record to the TUA_PrecioTicket table.
        /// </summary>
        public bool insertar2(PrecioTicket objPrecioTicket)
        {
            try
            {
                Hashtable hsInsertUSP = (Hashtable)htSPConfig["PTSP_INSERT"];
                string sNombreSP = (string)hsInsertUSP["PTSP_INSERT"];
                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);

                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Precio_Ticket"], DbType.String, objPrecioTicket.SCodPrecioTicket);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Tipo_Ticket"], DbType.String, objPrecioTicket.SCodTipoTicket);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Moneda"], DbType.String, objPrecioTicket.SCodMoneda);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Imp_Precio"], DbType.Decimal, objPrecioTicket.DImpPrecio);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Log_Usuario_Mod"], DbType.String, objPrecioTicket.SLogUsuarioMod);
                //add Tip_Ingreso and Tip_Estado
                helper.AddInParameter(objCommandWrapper, "Tip_Ingreso", DbType.String, objPrecioTicket.STipIngreso);
                helper.AddInParameter(objCommandWrapper, "Tip_Estado", DbType.String, objPrecioTicket.STipEstado);
                if (objPrecioTicket.DtFchProgramacion == DateTime.MinValue)
                {
                    helper.AddInParameter(objCommandWrapper, "Fch_Prog", DbType.DateTime, null);
                }
                else
                {
                    helper.AddInParameter(objCommandWrapper, "Fch_Prog", DbType.DateTime, objPrecioTicket.DtFchProgramacion);
                }
                isRegistrado = base.mantenerSP(objCommandWrapper);
                return isRegistrado;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Deletes a record from the TUA_PrecioTicket table by its primary key.
        /// </summary>
        public bool eliminar(string sCodPrecioTicket)
        {
            try
            {
                Hashtable hsDeleteUSP = (Hashtable)htSPConfig["PTSP_DELETE"];
                string sNombreSP = (string)hsDeleteUSP["PTSP_DELETE"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);

                helper.AddInParameter(objCommandWrapper, (string)hsDeleteUSP["Cod_Precio_Ticket"], DbType.String, sCodPrecioTicket);

                isActualizado = base.mantenerSP(objCommandWrapper);
                return isActualizado;

            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Get records from the TUA_PrecioTicket table.
        /// </summary>
        public DataTable obtener2(string sCodPrecioTicket)
        {
            DataTable result;

            //Begin Build Query --------------------
            String strSQL;
            String strWhere = "";
            strSQL = "SELECT pt.Cod_Precio_Ticket, pt.Cod_Tipo_Ticket, pt.Cod_Moneda, pt.Imp_Precio, pt.Fch_Creacion, pt.Log_Usuario_Mod, pt.Log_Fecha_Mod, pt.Log_Hora_Mod, tt.Dsc_Tipo_Ticket, tm.Dsc_Moneda, tm.Dsc_Simbolo, pt.Tip_Estado, pt.Tip_Ingreso, pt.Fch_Programacion, tu.Nom_Usuario+', '+tu.Ape_Usuario Nom_Usuario_Mod " +
                     " FROM TUA_PrecioTicket pt LEFT JOIN TUA_Usuario tu ON pt.Log_Usuario_Mod = tu.Cod_Usuario, TUA_TipoTicket tt, TUA_Moneda tm" +
                     " WHERE pt.Cod_Tipo_Ticket = tt.Cod_Tipo_Ticket" + 
                     " AND pt.Cod_Moneda = tm.Cod_Moneda";
            if (sCodPrecioTicket != null && sCodPrecioTicket.Length > 0)
            {
                strWhere = strWhere + " AND pt.Cod_Precio_Ticket = '" + sCodPrecioTicket.Trim() + "'";
            }
            strSQL = strSQL + strWhere + " ORDER BY pt.Cod_Tipo_Ticket";
            //End Build Query --------------------

            result = base.ListarDataTableQY(strSQL);
            return result;
        }

        public bool actualizar2(string Log_Usuario_Mod, string Cod_Modulo_Mod, string Cod_SubModulo_Mod)
        {
              try
              {
                    Hashtable hsUpdateUSP = (Hashtable)htSPConfig["PTSP_UPDATE"];
                    string sNombreSP = (string)hsUpdateUSP["PTSP_UPDATE"];

                    DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);

                    helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Log_Usuario_Mod"], DbType.String, Log_Usuario_Mod);
                    helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Cod_Modulo_Mod"], DbType.String, Cod_Modulo_Mod);
                    helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Cod_SubModulo_Mod"], DbType.String, Cod_SubModulo_Mod);

                    isActualizado = base.mantenerSP(objCommandWrapper);
                    return isActualizado;
              }
              catch (Exception ex)
              {

                    throw ex;
              }
        }
        
        public bool insertar(PrecioTicket objPrecioTicket)
        {
            try
            {
                Hashtable hsInsertUSP = (Hashtable)htSPConfig["TC_NEW_PTSP_INSERT"];
                string sNombreSP = (string)hsInsertUSP["TC_NEW_PTSP_INSERT"];
                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);

                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Precio_Ticket"], DbType.String, objPrecioTicket.SCodPrecioTicket);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Tipo_Ticket"], DbType.String, objPrecioTicket.SCodTipoTicket);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Moneda"], DbType.String, objPrecioTicket.SCodMoneda);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Imp_Precio"], DbType.Decimal, objPrecioTicket.DImpPrecio);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Log_Usuario_Mod"], DbType.String, objPrecioTicket.SLogUsuarioMod);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Tip_Ingreso"], DbType.String, objPrecioTicket.STipIngreso);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Tip_Estado"], DbType.String, objPrecioTicket.STipEstado);
                if (objPrecioTicket.DtFchProgramacion == DateTime.MinValue)
                {
                    helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Fch_Prog"], DbType.DateTime, null);
                }
                else
                {
                    helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Fch_Prog"], DbType.DateTime, objPrecioTicket.DtFchProgramacion);
                }
                helper.AddOutParameter(objCommandWrapper, (string)hsInsertUSP["Out_Cod_Retorno"], DbType.String, 3);
                
                isRegistrado = base.mantenerSP(objCommandWrapper);
                
                if (isRegistrado)//Ejecuto el Store Procedure satisfactoriamente
                {
                    objPrecioTicket.SCodRetorno = (string)helper.GetParameterValue(objCommandWrapper, (string)hsInsertUSP["Out_Cod_Retorno"]);
                }
                return isRegistrado;
            }
            catch (Exception)
            {
                throw;
            }
        }
		
		
		public DataTable obtener(string sCodPrecioTicket)
        {
            DataTable result;

            //Begin Build Query --------------------
            String strSQL;
            String strWhere = "";
            strSQL = "SELECT pt.Cod_Precio_Ticket, pt.Cod_Tipo_Ticket, pt.Cod_Moneda, pt.Imp_Precio, dbo.fnFormatDate (pt.Fch_Creacion, 'DD/MM/YYYY hh:xx:ss') Fch_Creacion, pt.Log_Usuario_Mod, pt.Log_Fecha_Mod, pt.Log_Hora_Mod, tt.Dsc_Tipo_Ticket, tm.Dsc_Moneda, tm.Dsc_Simbolo, pt.Tip_Estado, pt.Tip_Ingreso, dbo.fnFormatDate (pt.Fch_Programacion, 'DD/MM/YYYY hh:xx:ss') Fch_Programacion, tu.Nom_Usuario+', '+tu.Ape_Usuario Nom_Usuario_Mod " +
                     " FROM TUA_PrecioTicket pt LEFT JOIN TUA_Usuario tu ON pt.Log_Usuario_Mod = tu.Cod_Usuario, TUA_TipoTicket tt, TUA_Moneda tm" +
                     " WHERE pt.Cod_Tipo_Ticket = tt.Cod_Tipo_Ticket" + 
                     " AND pt.Cod_Moneda = tm.Cod_Moneda AND tt.Tip_Estado = '1'";
            if (sCodPrecioTicket != null && sCodPrecioTicket.Length > 0)
            {
                strWhere = strWhere + " AND pt.Cod_Precio_Ticket = '" + sCodPrecioTicket.Trim() + "'";
            }
            strSQL = strSQL + strWhere + " ORDER BY pt.Cod_Precio_Ticket DESC";
            //End Build Query --------------------

            result = base.ListarDataTableQY(strSQL);
            return result;
        }
		
		public bool actualizar()
        {
              try
              {
                    Hashtable hsUpdateUSP = (Hashtable)htSPConfig["PTSP_UPDATE"];
                    string sNombreSP = (string)hsUpdateUSP["PTSP_UPDATE"];

                    DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                    isActualizado = base.mantenerSP(objCommandWrapper);
                    return isActualizado;
              }
              catch (Exception ex)
              {

                    throw ex;
              }
        }  
       
        #endregion
    }
}
