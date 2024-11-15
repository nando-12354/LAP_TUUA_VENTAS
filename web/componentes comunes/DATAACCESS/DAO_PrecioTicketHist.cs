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
    public class DAO_PrecioTicketHist : DAO_BaseDatos
    {
        #region Fields

        public List<PrecioTicketHist> objListaPrecioTicketHist;

        #endregion

        #region Constructors

        public DAO_PrecioTicketHist(string sConfigSPPathg)
            : base(sConfigSPPathg)
        {
            objListaPrecioTicketHist = new List<PrecioTicketHist>();
        }
        #endregion

        #region Methods

        /// <summary>
        /// Saves a record to the TUA_PrecioTicketHist table.
        /// </summary>
        public bool insertar(PrecioTicketHist objPrecioTicketHist)
        {
            try
            {
                Hashtable hsInsertUSP = (Hashtable)htSPConfig["PTHSP_INSERT"];
                string sNombreSP = (string)hsInsertUSP["PTHSP_INSERT"];
                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);

                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Precio_Ticket"], DbType.String, objPrecioTicketHist.SCodPrecioTicket);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Tipo_Ticket"], DbType.String, objPrecioTicketHist.SCodTipoTicket);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Moneda"], DbType.String, objPrecioTicketHist.SCodMoneda);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Imp_Precio"], DbType.Decimal, objPrecioTicketHist.DImpPrecio);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Moneda2"], DbType.String, objPrecioTicketHist.SCodMoneda2);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Imp_Precio2"], DbType.Decimal, objPrecioTicketHist.DImpPrecio2);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Fch_Ini"], DbType.DateTime, objPrecioTicketHist.DtFchIni);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Fch_Fin"], DbType.DateTime, objPrecioTicketHist.DtFchFin);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Log_Usuario_Mod"], DbType.String, objPrecioTicketHist.SLogUsuarioMod);

                isRegistrado = base.mantenerSP(objCommandWrapper);
                return isRegistrado;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Deletes a record from the TUA_PrecioTicketHist table by its primary key.
        /// </summary>
        public bool eliminar(string sCodPrecioTicket)
        {
            try
            {
                Hashtable hsDeleteUSP = (Hashtable)htSPConfig["PTHSP_DELETE"];
                string sNombreSP = (string)hsDeleteUSP["PTHSP_DELETE"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);

                helper.AddInParameter(objCommandWrapper, (string)hsDeleteUSP["Cod_PrecioTicket"], DbType.String, sCodPrecioTicket);

                isActualizado = base.mantenerSP(objCommandWrapper);
                return isActualizado;

            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Get records from the TUA_PrecioTicketHist table.
        /// </summary>
        public DataTable obtener(string sCodPrecioTicket)
        {
            DataTable result;

            //Begin Build Query --------------------
            String strSQL;
            String strWhere = "";
            strSQL = "SELECT pth.Cod_Precio_Ticket, pth.Cod_Tipo_Ticket, pth.Cod_Moneda, pth.Imp_Precio," +
                        "pth.Cod_Moneda2, pth.Imp_Precio2, pth.Fch_Ini, pth.Fch_Fin, pth.Log_Usuario_Mod, " +
                        "pth.Log_Fecha_Mod, pth.Log_Hora_Mod, tt.Dsc_Tipo_Ticket, tm.Dsc_Moneda, tm.Dsc_Simbolo,CONVERT(CHAR(10),pth.Fch_Ini,103) Fch_IniSH,CONVERT(CHAR(10),pth.Fch_Fin,103) Fch_FinSH, " +

                        " case when (tu.Nom_Usuario is null and tu.Ape_Usuario is null) then pth.Log_Usuario_Mod " +
                        " ELSE ISNULL(tu.Nom_Usuario,' - ') +', '+ ISNULL(tu.Ape_Usuario, ' - ') END Nom_Usuario_Mod, " +

                        " dbo.fnFormatDate (pth.Fch_Ini, 'DD/MM/YYYY hh:xx:ss') Fch_Ini2, " +
                        " dbo.fnFormatDate (pth.Fch_Fin, 'DD/MM/YYYY hh:xx:ss') Fch_Fin2 " +
                     " FROM TUA_PrecioTicketHist pth LEFT JOIN TUA_Usuario tu ON pth.Log_Usuario_Mod = tu.Cod_Usuario, TUA_TipoTicket tt, TUA_Moneda tm" +
                      " WHERE pth.Cod_Tipo_Ticket = tt.Cod_Tipo_Ticket" +
                     " AND pth.Cod_Moneda = tm.Cod_Moneda";

            if (sCodPrecioTicket != null && sCodPrecioTicket.Length > 0)
            {
                strWhere = strWhere + " AND Cod_Precio_Ticket = '" + sCodPrecioTicket.Trim() + "'";
            }
            strSQL = strSQL + strWhere + " ORDER BY Cod_Precio_Ticket desc";
            //End Build Query --------------------

            result = base.ListarDataTableQY(strSQL);
            return result;
        }

        #endregion

    }
}
