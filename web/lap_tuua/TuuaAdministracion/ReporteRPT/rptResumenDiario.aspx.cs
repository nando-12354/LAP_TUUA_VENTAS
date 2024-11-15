using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using LAP.TUUA.UTIL;
using LAP.TUUA.CONTROL;

public partial class ReporteRPT_rptResumenDiario : System.Web.UI.Page
{
    protected bool Flg_Error;
    DataTable dt_consulta = new DataTable();
    DataTable dt_consulta_operacion = new DataTable();
    DataTable dt_consulta_tasacambio = new DataTable();
    DataTable dtRemesa = new DataTable();
    BO_Consultas objConsulta = new BO_Consultas();

    CrystalDecisions.CrystalReports.Engine.ReportDocument obRpt;

    protected void Page_Load(object sender, EventArgs e)
    {
        String sTipo = Request.QueryString["iTipo"];
        String sFechaDesde = null;
        String sFechaHasta = null;
        String sHoraDesde = null;
        String sHoraHasta = null;
        String sTurnoDesde = null;
        String sTurnoHasta = null;
        //Resumen Dario por Fecha
        if (sTipo == "1" || sTipo == "3")
        {
            sFechaDesde = Request.QueryString["fechaDesde"];
            sFechaHasta = Request.QueryString["fechaHasta"];
            sHoraDesde = Request.QueryString["horaDesde"];
            sHoraHasta = Request.QueryString["horaHasta"];
        }
        else if (sTipo == "2")
        {
            sTurnoDesde = Request.QueryString["turnoDesde"];
            sTurnoHasta = Request.QueryString["turnoHasta"];
        }

        if (Session["Rpt_ResumenDiario"] != null)
        {
            dt_consulta = (DataTable)Session["Rpt_ResumenDiario"];
            generarReporte(sTipo, sFechaDesde, sHoraDesde, sFechaHasta, sHoraHasta, sTurnoDesde, sTurnoHasta);
        }
        else
        {
            consultarDatos(sTipo, sFechaDesde, sHoraDesde, sFechaHasta, sHoraHasta, sTurnoDesde, sTurnoHasta);
            generarReporte(sTipo, sFechaDesde, sHoraDesde, sFechaHasta, sHoraHasta, sTurnoDesde, sTurnoHasta);
        }
    }
    public void generarReporte(String sTipo, String sFechaDesde, String sHoraDesde, String sFechaHasta, String sHoraHasta, String sTurnoDesde, String sTurnoHasta)
    {
        try
        {
            //Pintar el reporte                 
            obRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            obRpt.Load(Server.MapPath("").ToString() + @"\rptResumenDiario.rpt");

            DataSet dtset = new DataSet();
            dt_consulta_operacion.TableName = "dtDetalleResumenCompra";
            dt_consulta_tasacambio.TableName = "dtDetalleResumenTasaCambio";
            dtset.Tables.Add(dt_consulta_operacion.Copy());
            dtset.Tables.Add(dt_consulta_tasacambio.Copy());
            obRpt.SetDataSource(dtset);

            if (dt_consulta == null || dt_consulta.Rows.Count == 0)
                cargarDefaultParametros();
            else
                cargarBasicParametros();
            cargarResumenParametros(sTipo);
            if (sTipo == "1" || sTipo == "3")
            {
                obRpt.SetParameterValue("pTitulo", "Resumen Diario");
                obRpt.SetParameterValue("pFiltroDesde", "Fecha Desde: ");
                obRpt.SetParameterValue("pFiltroHasta", "Fecha Hasta: ");
                obRpt.SetParameterValue("pRangoInicial", Fecha.convertSQLToFecha(sFechaDesde, sHoraDesde));
                obRpt.SetParameterValue("pRangoFinal", Fecha.convertSQLToFecha(sFechaHasta, sHoraHasta));
            }
            else if (sTipo == "2")
            {
                obRpt.SetParameterValue("pTitulo", "Resumen Diario por Turnos");
                obRpt.SetParameterValue("pFiltroDesde", "Turno Desde: ");
                obRpt.SetParameterValue("pFiltroHasta", "Turno Hasta: ");
                obRpt.SetParameterValue("pRangoInicial", sTurnoDesde);
                obRpt.SetParameterValue("pRangoFinal", sTurnoHasta);
            }
            obRpt.SetParameterValue("pCuenta", (string)Session["Cta_Usuario"]);
            crptvResumenDiario.ReportSource = obRpt;
        }
        catch (Exception ex)
        {
            Flg_Error = true;
            ErrorHandler.Trace(ErrorHandler.Desc_Mensaje, ex.Message);
        }
        finally
        {
            if (Flg_Error)
            {
                Response.Redirect("PaginaError.aspx");
            }
        }
    }

    public void cargarDefaultParametros()
    {
        obRpt.SetParameterValue("pEfectivo0", "0");
        obRpt.SetParameterValue("pEfectivo1", "0");
        obRpt.SetParameterValue("pEfectivo2", "0");
        obRpt.SetParameterValue("pEfectivo3", "0");
        obRpt.SetParameterValue("pTransferencia0", "0");
        obRpt.SetParameterValue("pTransferencia1", "0");
        obRpt.SetParameterValue("pTransferencia2", "0");
        obRpt.SetParameterValue("pTransferencia3", "0");
        obRpt.SetParameterValue("pCheque0", "0");
        obRpt.SetParameterValue("pCheque1", "0");
        obRpt.SetParameterValue("pCheque2", "0");
        obRpt.SetParameterValue("pCheque3", "0");
        obRpt.SetParameterValue("pTarjetaCredito0", "0");
        obRpt.SetParameterValue("pTarjetaCredito1", "0");
        obRpt.SetParameterValue("pTarjetaCredito2", "0");
        obRpt.SetParameterValue("pTarjetaCredito3", "0");
        obRpt.SetParameterValue("pTarjetaDebito0", "0");
        obRpt.SetParameterValue("pTarjetaDebito1", "0");
        obRpt.SetParameterValue("pTarjetaDebito2", "0");
        obRpt.SetParameterValue("pTarjetaDebito3", "0");
        obRpt.SetParameterValue("pCredito0", "0");
        obRpt.SetParameterValue("pCredito1", "0");
        obRpt.SetParameterValue("pCredito2", "0");
        obRpt.SetParameterValue("pCredito3", "0");
    }
    public void cargarBasicParametros()
    {
        DataRow[] foundRowTmp;
        //Parametros del Reporte                
        //EFECTIVO (E)
        foundRowTmp = dt_consulta.Select("Tip_Operacion = 'E' AND Tip_Vuelo = 'N' AND Tip_Pasajero = 'A'");
        obRpt.SetParameterValue("pEfectivo0", (foundRowTmp != null && foundRowTmp.Length > 0) ? foundRowTmp[0]["Num_Total_Ticket"].ToString() : "0");
        foundRowTmp = dt_consulta.Select("Tip_Operacion = 'E' AND Tip_Vuelo = 'N' AND Tip_Pasajero = 'I'");
        obRpt.SetParameterValue("pEfectivo1", (foundRowTmp != null && foundRowTmp.Length > 0) ? foundRowTmp[0]["Num_Total_Ticket"].ToString() : "0");
        foundRowTmp = dt_consulta.Select("Tip_Operacion = 'E' AND Tip_Vuelo = 'I' AND Tip_Pasajero = 'A'");
        obRpt.SetParameterValue("pEfectivo2", (foundRowTmp != null && foundRowTmp.Length > 0) ? foundRowTmp[0]["Num_Total_Ticket"].ToString() : "0");
        foundRowTmp = dt_consulta.Select("Tip_Operacion = 'E' AND Tip_Vuelo = 'I' AND Tip_Pasajero = 'I'");
        obRpt.SetParameterValue("pEfectivo3", (foundRowTmp != null && foundRowTmp.Length > 0) ? foundRowTmp[0]["Num_Total_Ticket"].ToString() : "0");
        //TRANSFERENCIA (T)
        foundRowTmp = dt_consulta.Select("Tip_Operacion = 'T' AND Tip_Vuelo = 'N' AND Tip_Pasajero = 'A'");
        obRpt.SetParameterValue("pTransferencia0", (foundRowTmp != null && foundRowTmp.Length > 0) ? foundRowTmp[0]["Num_Total_Ticket"].ToString() : "0");
        foundRowTmp = dt_consulta.Select("Tip_Operacion = 'T' AND Tip_Vuelo = 'N' AND Tip_Pasajero = 'I'");
        obRpt.SetParameterValue("pTransferencia1", (foundRowTmp != null && foundRowTmp.Length > 0) ? foundRowTmp[0]["Num_Total_Ticket"].ToString() : "0");
        foundRowTmp = dt_consulta.Select("Tip_Operacion = 'T' AND Tip_Vuelo = 'I' AND Tip_Pasajero = 'A'");
        obRpt.SetParameterValue("pTransferencia2", (foundRowTmp != null && foundRowTmp.Length > 0) ? foundRowTmp[0]["Num_Total_Ticket"].ToString() : "0");
        foundRowTmp = dt_consulta.Select("Tip_Operacion = 'T' AND Tip_Vuelo = 'I' AND Tip_Pasajero = 'I'");
        obRpt.SetParameterValue("pTransferencia3", (foundRowTmp != null && foundRowTmp.Length > 0) ? foundRowTmp[0]["Num_Total_Ticket"].ToString() : "0");
        //CHEQUE (Q)
        foundRowTmp = dt_consulta.Select("Tip_Operacion = 'Q' AND Tip_Vuelo = 'N' AND Tip_Pasajero = 'A'");
        obRpt.SetParameterValue("pCheque0", (foundRowTmp != null && foundRowTmp.Length > 0) ? foundRowTmp[0]["Num_Total_Ticket"].ToString() : "0");
        foundRowTmp = dt_consulta.Select("Tip_Operacion = 'Q' AND Tip_Vuelo = 'N' AND Tip_Pasajero = 'I'");
        obRpt.SetParameterValue("pCheque1", (foundRowTmp != null && foundRowTmp.Length > 0) ? foundRowTmp[0]["Num_Total_Ticket"].ToString() : "0");
        foundRowTmp = dt_consulta.Select("Tip_Operacion = 'Q' AND Tip_Vuelo = 'I' AND Tip_Pasajero = 'A'");
        obRpt.SetParameterValue("pCheque2", (foundRowTmp != null && foundRowTmp.Length > 0) ? foundRowTmp[0]["Num_Total_Ticket"].ToString() : "0");
        foundRowTmp = dt_consulta.Select("Tip_Operacion = 'Q' AND Tip_Vuelo = 'I' AND Tip_Pasajero = 'I'");
        obRpt.SetParameterValue("pCheque3", (foundRowTmp != null && foundRowTmp.Length > 0) ? foundRowTmp[0]["Num_Total_Ticket"].ToString() : "0");
        //TARJETA CREDITO (C)
        foundRowTmp = dt_consulta.Select("Tip_Operacion = 'C' AND Tip_Vuelo = 'N' AND Tip_Pasajero = 'A'");
        obRpt.SetParameterValue("pTarjetaCredito0", (foundRowTmp != null && foundRowTmp.Length > 0) ? foundRowTmp[0]["Num_Total_Ticket"].ToString() : "0");
        foundRowTmp = dt_consulta.Select("Tip_Operacion = 'C' AND Tip_Vuelo = 'N' AND Tip_Pasajero = 'I'");
        obRpt.SetParameterValue("pTarjetaCredito1", (foundRowTmp != null && foundRowTmp.Length > 0) ? foundRowTmp[0]["Num_Total_Ticket"].ToString() : "0");
        foundRowTmp = dt_consulta.Select("Tip_Operacion = 'C' AND Tip_Vuelo = 'I' AND Tip_Pasajero = 'A'");
        obRpt.SetParameterValue("pTarjetaCredito2", (foundRowTmp != null && foundRowTmp.Length > 0) ? foundRowTmp[0]["Num_Total_Ticket"].ToString() : "0");
        foundRowTmp = dt_consulta.Select("Tip_Operacion = 'C' AND Tip_Vuelo = 'I' AND Tip_Pasajero = 'I'");
        obRpt.SetParameterValue("pTarjetaCredito3", (foundRowTmp != null && foundRowTmp.Length > 0) ? foundRowTmp[0]["Num_Total_Ticket"].ToString() : "0");
        //TARJETA DEBITO (D)
        foundRowTmp = dt_consulta.Select("Tip_Operacion = 'D' AND Tip_Vuelo = 'N' AND Tip_Pasajero = 'A'");
        obRpt.SetParameterValue("pTarjetaDebito0", (foundRowTmp != null && foundRowTmp.Length > 0) ? foundRowTmp[0]["Num_Total_Ticket"].ToString() : "0");
        foundRowTmp = dt_consulta.Select("Tip_Operacion = 'D' AND Tip_Vuelo = 'N' AND Tip_Pasajero = 'I'");
        obRpt.SetParameterValue("pTarjetaDebito1", (foundRowTmp != null && foundRowTmp.Length > 0) ? foundRowTmp[0]["Num_Total_Ticket"].ToString() : "0");
        foundRowTmp = dt_consulta.Select("Tip_Operacion = 'D' AND Tip_Vuelo = 'I' AND Tip_Pasajero = 'A'");
        obRpt.SetParameterValue("pTarjetaDebito2", (foundRowTmp != null && foundRowTmp.Length > 0) ? foundRowTmp[0]["Num_Total_Ticket"].ToString() : "0");
        foundRowTmp = dt_consulta.Select("Tip_Operacion = 'D' AND Tip_Vuelo = 'I' AND Tip_Pasajero = 'I'");
        obRpt.SetParameterValue("pTarjetaDebito3", (foundRowTmp != null && foundRowTmp.Length > 0) ? foundRowTmp[0]["Num_Total_Ticket"].ToString() : "0");
        //CREDITO (X)
        foundRowTmp = dt_consulta.Select("Tip_Operacion = 'X' AND Tip_Vuelo = 'N' AND Tip_Pasajero = 'A'");
        obRpt.SetParameterValue("pCredito0", (foundRowTmp != null && foundRowTmp.Length > 0) ? foundRowTmp[0]["Num_Total_Ticket"].ToString() : "0");
        foundRowTmp = dt_consulta.Select("Tip_Operacion = 'X' AND Tip_Vuelo = 'N' AND Tip_Pasajero = 'I'");
        obRpt.SetParameterValue("pCredito1", (foundRowTmp != null && foundRowTmp.Length > 0) ? foundRowTmp[0]["Num_Total_Ticket"].ToString() : "0");
        foundRowTmp = dt_consulta.Select("Tip_Operacion = 'X' AND Tip_Vuelo = 'I' AND Tip_Pasajero = 'A'");
        obRpt.SetParameterValue("pCredito2", (foundRowTmp != null && foundRowTmp.Length > 0) ? foundRowTmp[0]["Num_Total_Ticket"].ToString() : "0");
        foundRowTmp = dt_consulta.Select("Tip_Operacion = 'X' AND Tip_Vuelo = 'I' AND Tip_Pasajero = 'I'");
        obRpt.SetParameterValue("pCredito3", (foundRowTmp != null && foundRowTmp.Length > 0) ? foundRowTmp[0]["Num_Total_Ticket"].ToString() : "0");
    }
    public void cargarResumenParametros(string sTipo)
    {
        DataRow[] foundRowTmp;
        decimal decPagoLAP, decRemesaSolesLAP, decRemesaDolaresLAP, decRemesaEurosLAP;
        decRemesaSolesLAP = 0.00m;
        decRemesaDolaresLAP = 0.00m;
        decRemesaEurosLAP = 0.00m;
        decPagoLAP = 0.00m;
        //PAGO LAP
        object objsum = null;
        if (dt_consulta.Rows.Count > 0)
        {
            objsum = dt_consulta.Compute("Sum(Imp_Monto_Dol)", "Tip_Operacion <> 'X'");
        }
        //object objsum = dt_consulta.Compute("Sum(Imp_Monto_Dol)", "");
        try
        {
            if (objsum != null)
            {
                if (objsum.GetType().ToString() != "System.DBNull")
                    decPagoLAP = Function.FormatDecimal(Convert.ToDecimal(objsum),Define.NUM_DECIMAL);
                else
                    decPagoLAP = 0.00m;
            }
        }
        catch (Exception ex)
        {
            decPagoLAP = 0.00m;
        }
        obRpt.SetParameterValue("pPago0", "0.00");
        obRpt.SetParameterValue("pPago1", Convert.ToString(decPagoLAP));
        //REMESA
        //if (sTipo == "2")
        //{
        //    //object objsum = dt_consulta_operacion.Compute("Sum(Imp_Monto_Dol)", "Cod_Tip_Operacion = 'CM' AND Cod_Moneda = 'DOL'");
        //    foundRowTmp = dt_consulta_operacion.Select("Cod_Tipo_Operacion = 'CM' AND Cod_Moneda = 'DOL'");
        //    if (foundRowTmp != null && foundRowTmp.Count() > 0)
        //    {
        //        decRemesaSolesLAP = -1 * Convert.ToDecimal(foundRowTmp[0]["Monto_Moneda_Soles"]);
        //        decRemesaDolaresLAP = Convert.ToDecimal(foundRowTmp[0]["Monto_Moneda_Extranjera"]);
        //    }
        //    foundRowTmp = dt_consulta_operacion.Select("Cod_Tipo_Operacion = 'VM' AND Cod_Moneda = 'DOL'");
        //    if (foundRowTmp != null && foundRowTmp.Count() > 0)
        //    {
        //        //decRemesaSolesLAP += Convert.ToDecimal(foundRowTmp[0]["Monto_Moneda_Soles"]);
        //        //decRemesaDolaresLAP -= Convert.ToDecimal(foundRowTmp[0]["Monto_Moneda_Extranjera"]);
        //        decRemesaSolesLAP += Convert.ToDecimal(foundRowTmp[0]["Monto_Moneda_Extranjera"]);
        //        decRemesaDolaresLAP -= Convert.ToDecimal(foundRowTmp[0]["Monto_Moneda_Soles"]);
        //    }
        //    //adicional - kinzi
        //    foundRowTmp = dt_consulta_operacion.Select("Cod_Tipo_Operacion = 'CM' AND Cod_Moneda = 'EUR'");
        //    if (foundRowTmp != null && foundRowTmp.Count() > 0)
        //    {
        //        decRemesaSolesLAP += -1 * Convert.ToDecimal(foundRowTmp[0]["Monto_Moneda_Soles"]);
        //        decRemesaEurosLAP = Convert.ToDecimal(foundRowTmp[0]["Monto_Moneda_Extranjera"]);
        //    }
        //    foundRowTmp = dt_consulta_operacion.Select("Cod_Tipo_Operacion = 'VM' AND Cod_Moneda = 'EUR'");
        //    if (foundRowTmp != null && foundRowTmp.Count() > 0)
        //    {
        //        //decRemesaSolesLAP += Convert.ToDecimal(foundRowTmp[0]["Monto_Moneda_Soles"]);
        //        //decRemesaDolaresLAP -= Convert.ToDecimal(foundRowTmp[0]["Monto_Moneda_Extranjera"]);
        //        decRemesaSolesLAP += Convert.ToDecimal(foundRowTmp[0]["Monto_Moneda_Extranjera"]);
        //        decRemesaEurosLAP -= Convert.ToDecimal(foundRowTmp[0]["Monto_Moneda_Soles"]);
        //    }
        //    decRemesaDolaresLAP += decPagoLAP;
        //    obRpt.SetParameterValue("pRemesa0", Convert.ToString(decRemesaSolesLAP));
        //    obRpt.SetParameterValue("pRemesa1", Convert.ToString(decRemesaDolaresLAP));
        //    obRpt.SetParameterValue("pRemesa2", Convert.ToString(decRemesaEurosLAP));
        //}
        //else
        //{
        if (dtRemesa.Rows.Count == 1 ){
            if (dtRemesa.Rows[0].ItemArray.GetValue(0).ToString() == "DOL")
            {
                decRemesaDolaresLAP = Convert.ToDecimal(dtRemesa.Rows[0].ItemArray.GetValue(1).ToString());
            }
            else if (dtRemesa.Rows[0].ItemArray.GetValue(0).ToString() == "SOL")
            {
                decRemesaSolesLAP = Convert.ToDecimal(dtRemesa.Rows[0].ItemArray.GetValue(1).ToString());
            }
        }
        else if (dtRemesa.Rows.Count >= 2) {
            if (dtRemesa.Select("Cod_Moneda='SOL'").Count() > 0)
            {
                decRemesaSolesLAP = Convert.ToDecimal(dtRemesa.Select("Cod_Moneda='SOL'")[0].ItemArray.GetValue(1).ToString());
            }
            if (dtRemesa.Select("Cod_Moneda='DOL'").Count() > 0)
            {
                decRemesaDolaresLAP = Convert.ToDecimal(dtRemesa.Select("Cod_Moneda='DOL'")[0].ItemArray.GetValue(1).ToString());
            }  
        }

        obRpt.SetParameterValue("pRemesa0", Convert.ToString(decRemesaSolesLAP));
        obRpt.SetParameterValue("pRemesa1", Convert.ToString(decRemesaDolaresLAP));
        obRpt.SetParameterValue("pRemesa2", Convert.ToString(decRemesaEurosLAP));
        //}
    }

    public void consultarDatos(String sTipo, String sFechaDesde, String sHoraDesde, String sFechaHasta, String sHoraHasta, String sTurnoDesde, String sTurnoHasta)
    {
        // RESUMEN DIARIO POR FECHA
        if (sTipo == "1" || sTipo == "3")
        {
            dt_consulta = objConsulta.ListarTicketVendido("1", sFechaDesde, sHoraDesde, sFechaHasta, sHoraHasta, null, null);
            dt_consulta_operacion = objConsulta.ListarResumenCompraVenta("1", sFechaDesde, sHoraDesde, sFechaHasta, sHoraHasta, null, null);
            dt_consulta_tasacambio = objConsulta.ListarResumenTasaCambio("1", sFechaDesde, sHoraDesde, sFechaHasta, sHoraHasta, null, null);
            dtRemesa = objConsulta.ListarResumenCompraVenta("3", sFechaDesde, sHoraDesde, sFechaHasta, sHoraHasta, null, null);
        }
        else if (sTipo == "2")
        {
            //RESUMEN DIARIO POR TURNO
            dt_consulta = objConsulta.ListarTicketVendido("2", null, null, null, null, sTurnoDesde, sTurnoHasta);
            dt_consulta_operacion = objConsulta.ListarResumenCompraVenta("2", null, null, null, null, sTurnoDesde, sTurnoHasta);
            dt_consulta_tasacambio = objConsulta.ListarResumenTasaCambio("2", null, null, null, null, sTurnoDesde, sTurnoHasta);
            dtRemesa = objConsulta.ListarResumenCompraVenta("3", null, null, null, null, sTurnoDesde, sTurnoHasta);
        }
    }
}
