﻿using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using LAP.TUUA.UTIL;
using LAP.TUUA.CONTROL;
using LAP.TUUA.CONVERSOR;

public partial class ReporteRPT_rptTicketBPUsadosDiaMes : System.Web.UI.Page
{
    protected bool Flg_Error;
    BO_Reportes objBOReportes = new BO_Reportes();
    BO_Consultas objConsulta = new BO_Consultas();
    protected DataTable dtReporteDetalle = new DataTable();
    DataSetHelper dsHelper = new DataSetHelper();

    protected DataTable dt_resumen = new DataTable();
    protected DataTable dt_reporte = new DataTable();

    //Filtros
    string sFechaDesde, sFechaHasta;
    string sMes, sAnnio;
    string sTDocumento;
    string sIdCompania;
    string sDesCompania;
    string sDestino;
    string sTipoTicket;
    string sNumVuelo;
    string sTipReporte;
    string sDesTipoTicket;

    //label
    string sFechaEstadistico;

    protected void Page_Load(object sender, EventArgs e)
    {

        sFechaDesde = Request.QueryString["sFechaDesde"];
        sFechaHasta = Request.QueryString["sFechaHasta"];
        sMes = Request.QueryString["sMes"];
        sAnnio = Request.QueryString["sAnnio"];
        sTDocumento = Request.QueryString["sTDocumento"];
        sIdCompania = Request.QueryString["sIdCompania"];
        sDesCompania = Request.QueryString["sDesCompania"];
        sDestino = Request.QueryString["sDestino"];
        sDestino = sDestino.ToUpper();
        sTipoTicket = Request.QueryString["sTipoTicket"];
        sDesTipoTicket = Request.QueryString["sDesTipoTicket"];
        sNumVuelo = Request.QueryString["sNumVuelo"];
        sFechaEstadistico = Request.QueryString["sFechaEstadistico"];

        if (sMes == "")
        {
            sTipReporte = null;
        }
        else
        {
            sTipReporte = "1";
        }

        //if (ViewState["dtReporte"] == null)
        //{
            //dtReporteDetalle = CrossTab(FiltraFechas(FiltrarFechaUso(objBOReportes.consultarTicketBoardingUsados(sIdCompania, sNumVuelo, sTDocumento, sTipoTicket, sTipoFiltro, sFechaDesde, sFechaHasta, "", "", sAño + sMes)), sFechaDesde, sFechaHasta, sMes, sAño));
            //ViewState["dtReporte"] = dtReporteDetalle;
        //}

        //if (dtReporteDetalle != null)
        //{
        //    if (sFechaDesde != "")
        //        sFechaDesde = Fecha.convertSQLToFecha(sFechaDesde, "");


        //    if (sFechaHasta != "")
        //        sFechaHasta = Fecha.convertSQLToFecha(sFechaHasta, "");

        //    if (sMes == "")
        //        sAño = "";

        DataTable dt_parametro = objConsulta.ListarParametrosDefaultValue("LR");
        if (dt_parametro.Rows.Count > 0)
        {
            bool reporteFechas = false;
            DateTime fechaF;
            DateTime fechaI;
            TimeSpan ts = new TimeSpan();
            if (sFechaDesde != "" && sFechaHasta != "")
            {
                fechaF = Convert.ToDateTime(Fecha.convertSQLToFecha(sFechaHasta, null));
                fechaI = Convert.ToDateTime(Fecha.convertSQLToFecha(sFechaDesde, null));
                ts = fechaF.Subtract(fechaI);
                reporteFechas=true;
            }
                
            int dias = ts.Days;
            int parametro = Convert.ToInt32(dt_parametro.Rows[0].ItemArray.GetValue(4).ToString());

            if (dias > parametro && reporteFechas)
            {
                lblmensaje.Text = "El periodo máximo de días a imprimir el reporte es" + " " + parametro.ToString() + " " + "días.";
            }
            else
            {
                generarReporte(sFechaDesde, sFechaHasta, sMes, sAnnio, sTDocumento,
                         sIdCompania, sDesCompania, sDestino, sTipoTicket,
                         sDesTipoTicket, sNumVuelo);
            }
        }

          
        //}
    }

    public void generarReporte(string sFechaDesde, string sFechaHasta, string sMes, string sAño, string sTDocumento, string sIdCompania, string sDesCompania, string sDestino, string sTipoTicket, string sDesTipoTicket, string sNumVuelo)
    {
        try
        {

            DataTable dtReporte = new DataTable();
            dtReporte = GetDataPage();

            if (dtReporte == null || dtReporte.Rows.Count == 0)
            {
                //Response.Write("La busqueda no retorna resultado, regrese a la consulta para un nuevo filtro");
                lblmensaje.Text = "La busqueda no retorna resultado, regrese a la consulta para un nuevo filtro";
            }
            else
            {
               //cargar resumen
                DataTable dtResumen = GetResumen();

                //Pintar el reporte                 
                CrystalDecisions.CrystalReports.Engine.ReportDocument obRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                obRpt.Load(Server.MapPath("").ToString() + @"\rptTicketBPUsadosDiaMes.rpt");
                obRpt.SetDataSource(dtReporte);
                obRpt.Subreports[0].SetDataSource(dtResumen);
               
                
                obRpt.SetParameterValue("pDesde", Fecha.convertSQLToFecha(sFechaDesde, ""));
                obRpt.SetParameterValue("pHasta", Fecha.convertSQLToFecha(sFechaHasta, ""));
                if (sMes != "")
                    obRpt.SetParameterValue("pMes", this.ddlMes.Items[Convert.ToInt32(sMes) - 1].Text);
                else
                    obRpt.SetParameterValue("pMes", sMes);
                obRpt.SetParameterValue("pAño", sAño);
                string sTiDoc = "";
                if (sTDocumento == "O")
                    sTiDoc = "Ticket - BP";
                if (sTDocumento == "T")
                    sTiDoc = "Ticket";
                if (sTDocumento == "B")
                    sTiDoc = "Boarding Pass";

                obRpt.SetParameterValue("pTipoDocumento", sTiDoc);
                obRpt.SetParameterValue("pAerolinea", sDesCompania);
                obRpt.SetParameterValue("pNroVuelo", sNumVuelo);
                obRpt.SetParameterValue("pDestino", sDestino);
                obRpt.SetParameterValue("pTipoTicket", sDesTipoTicket);
                obRpt.SetParameterValue("pFechaEstadistico", sFechaEstadistico);

                this.crvrptTicketBPUsadosDiaMes.ReportSource = obRpt;
                dtReporte.Dispose();
            }
        }
        catch (Exception ex)
        {
            Flg_Error = true;
            ErrorHandler.Trace(ErrorHandler.Desc_Mensaje, ex.Message);
        }
        finally
        {
            dtReporteDetalle.Dispose();
            if (Flg_Error)
            {
                Response.Redirect("PaginaError.aspx");
            }
        }
    }

    private DataTable GetDataPage()
    {

        //int iTamanio = GetRowCount();

        //DataTable dt_consulta = objBOReportes.ListarTicketBoardingUsadosDiaMesPagin(sFechaDesde, sFechaHasta, sAnnio + sMes, 
        //                                                                            sTDocumento, sIdCompania, sNumVuelo, 
        //                                                                            sTipoTicket, sDestino, sTipReporte, 
        //                                                                            sortExpression, 1,
        //                                                                            iTamanio, "0");

        DataTable dt_consulta = objBOReportes.ListarTicketBoardingUsadosDiaMesPagin(sFechaDesde,
                                                                                    sFechaHasta, 
                                                                                    sAnnio + sMes,
                                                                                    sTDocumento, sIdCompania, 
                                                                                    sNumVuelo, sTipoTicket,
                                                                                    sDestino, sTipReporte, null, 0, 0, "0");


        //ViewState["tablaTicket"] = dt_consulta;
        return dt_consulta;
    }


    private DataTable GetResumen()
    {
        //int count = 0;

        /*dt_resumen = objBOReportes.ListarTicketBoardingUsadosDiaMesPagin(sFechaDesde, sFechaHasta, sAnnio + sMes, 
                                                                         sTDocumento, sIdCompania, sNumVuelo, sTipoTicket, 
                                                                         sDestino, sTipReporte, null, 0, 0, "1");*/
        dt_resumen = objBOReportes.ListarTicketBoardingUsadosDiaMesPagin(sFechaDesde, sFechaHasta, sAnnio + sMes
                            , sTDocumento, sIdCompania, sNumVuelo, sTipoTicket
                            , sDestino, sTipReporte, null, 0, 0, "2");

        //if (dt_resumen.Columns.Contains("TotRows"))
        //    count = Convert.ToInt32(dt_resumen.Rows[0]["TotRows"].ToString());
        //else
        //    dt_resumen = null;
            //lblMensajeError.Text = dt_resumen.Rows[0].ItemArray.GetValue(1).ToString();
        return dt_resumen;
    }


    #region FUENTES GCHAVEZ
    private DataTable FiltraFechas(DataTable dtReporteDetalle, string sFechaDesde, string sFechaHasta, string sMes, string sAño)
    {

        DataTable dest = new DataTable("Result" + dtReporteDetalle.TableName);
        DataColumn dc;

        dc = new DataColumn();
        dc.ColumnName = "Log_Fecha_Mod";
        dest.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Log_Hora_Mod";
        dest.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Tipo_Documento";
        dest.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Dsc_Tipo_Ticket";
        dest.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Dsc_Compania";
        dest.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Num_Vuelo";
        dest.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Destino";
        dest.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Total_Ticket";
        dest.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Total_BCP";
        dest.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Total";
        dest.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Dsc_Trama_Bcbp";
        dest.Columns.Add(dc);


        string sSeleccion = "";

        if (sFechaDesde != "")
        {
            sSeleccion = "Log_Fecha_Mod >= '" + sFechaDesde + "'";
            if (sFechaHasta != "")
                sSeleccion = sSeleccion + " AND Log_Fecha_Mod <= '" + sFechaHasta + "'";
        }
        else
        {
            if (sFechaHasta != "")
                sSeleccion = "Log_Fecha_Mod <= '" + sFechaHasta + "'";
        }

        if (sMes != "")
            sSeleccion = "SUBSTRING(Log_Fecha_Mod,5,2) = '" + sMes + "' AND SUBSTRING(Log_Fecha_Mod,1,4)= '" + sAño + "' ";

        if (sSeleccion != "")
        {
            if (dtReporteDetalle.Rows.Count > 0)
            {
                DataRow[] foundRowFiltroFecha = dtReporteDetalle.Select(sSeleccion);

                if (foundRowFiltroFecha != null && foundRowFiltroFecha.Length > 0)
                {
                    for (int i = 0; i < foundRowFiltroFecha.Length; i++)
                    {
                        dest.Rows.Add(dest.NewRow());
                        if (sMes == "")
                            dest.Rows[i][0] = Fecha.convertSQLToFecha(foundRowFiltroFecha[i]["Log_Fecha_Mod"].ToString(), "");
                        else
                            dest.Rows[i][0] = ddlMes.Items[Convert.ToInt32(sMes) - 1].Text + " - " + sAño;
                        dest.Rows[i][1] = foundRowFiltroFecha[i]["Log_Hora_Mod"].ToString();
                        dest.Rows[i][2] = foundRowFiltroFecha[i]["Tipo_Documento"].ToString();
                        dest.Rows[i][3] = foundRowFiltroFecha[i]["Dsc_Tipo_Ticket"].ToString();
                        dest.Rows[i][4] = foundRowFiltroFecha[i]["Dsc_Compania"].ToString();
                        dest.Rows[i][5] = foundRowFiltroFecha[i]["Num_Vuelo"].ToString();
                        dest.Rows[i][6] = foundRowFiltroFecha[i]["Destino"].ToString();
                        dest.Rows[i][7] = foundRowFiltroFecha[i]["Total_Ticket"].ToString();
                        dest.Rows[i][8] = foundRowFiltroFecha[i]["Total_BCP"].ToString();
                        dest.Rows[i][9] = foundRowFiltroFecha[i]["Total"].ToString();
                        dest.Rows[i][10] = foundRowFiltroFecha[i]["Dsc_Trama_Bcbp"].ToString();
                    }
                }
            }
            dest.AcceptChanges();

            dest = dsHelper.SelectGroupByInto("Reporte", dest, "Log_Fecha_Mod,Log_Hora_Mod,Tipo_Documento,Dsc_Tipo_Ticket,Dsc_Compania,Num_Vuelo,Destino,sum(Total_Ticket) Total_Ticket,sum(Total_BCP) Total_BCP,sum(Total) Total,Dsc_Trama_Bcbp", "", "Log_Fecha_Mod");
            dest = dsHelper.SelectGroupByInto("Reporte", dest, "Log_Fecha_Mod,Log_Hora_Mod,Tipo_Documento,Dsc_Tipo_Ticket,Dsc_Compania,Num_Vuelo,Destino,sum(Total_Ticket) Total_Ticket,sum(Total_BCP) Total_BCP,sum(Total) Total,Dsc_Trama_Bcbp", "", "Tipo_Documento");
            dest = dsHelper.SelectGroupByInto("Reporte", dest, "Log_Fecha_Mod,Log_Hora_Mod,Tipo_Documento,Dsc_Tipo_Ticket,Dsc_Compania,Num_Vuelo,Destino,sum(Total_Ticket) Total_Ticket,sum(Total_BCP) Total_BCP,sum(Total) Total,Dsc_Trama_Bcbp", "", "Dsc_Compania");
            dest = dsHelper.SelectGroupByInto("Reporte", dest, "Log_Fecha_Mod,Log_Hora_Mod,Tipo_Documento,Dsc_Tipo_Ticket,Dsc_Compania,Num_Vuelo,Destino,sum(Total_Ticket) Total_Ticket,sum(Total_BCP) Total_BCP,sum(Total) Total,Dsc_Trama_Bcbp", "", "Dsc_Tipo_Ticket");
            dest = dsHelper.SelectGroupByInto("Reporte", dest, "Log_Fecha_Mod,Log_Hora_Mod,Tipo_Documento,Dsc_Tipo_Ticket,Dsc_Compania,Num_Vuelo,Destino,sum(Total_Ticket) Total_Ticket,sum(Total_BCP) Total_BCP,sum(Total) Total,Dsc_Trama_Bcbp", "", "Num_Vuelo");


            return dest;
        }
        else
            return dtReporteDetalle;

    }


    private DataTable FiltrarFechaUso(DataTable dtReporteDetalle)
    {
        DataTable dest = new DataTable("Result" + dtReporteDetalle.TableName);
        DataColumn dc;

        dc = new DataColumn();
        dc.ColumnName = "Log_Fecha_Mod";
        dest.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Log_Hora_Mod";
        dest.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Tipo_Documento";
        dest.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Dsc_Tipo_Ticket";
        dest.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Dsc_Compania";
        dest.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Num_Vuelo";
        dest.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Destino";
        dest.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Total_Ticket";
        dest.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Total_BCP";
        dest.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Total";
        dest.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Dsc_Trama_Bcbp";
        dest.Columns.Add(dc);


        DataTable dtTickets = new DataTable();

        dc = new DataColumn();
        dc.ColumnName = "Log_Fecha_Mod";
        dtTickets.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Log_Hora_Mod";
        dtTickets.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Tipo_Documento";
        dtTickets.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Dsc_Tipo_Ticket";
        dtTickets.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Dsc_Compania";
        dtTickets.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Num_Vuelo";
        dtTickets.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Destino";
        dtTickets.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Total_Ticket";
        dtTickets.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Codigo";
        dtTickets.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "FlagCobro";
        dtTickets.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Secuencial";
        dtTickets.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Dsc_Trama_Bcbp";
        dtTickets.Columns.Add(dc);


        if (dtReporteDetalle.Rows.Count > 0)
        {

            DataRow[] foundRowTicket = dtReporteDetalle.Select("Tipo_Documento = 'Ticket'");

            if (foundRowTicket != null && foundRowTicket.Length > 0)
            {
                for (int i = 0; i < foundRowTicket.Length; i++)
                {
                    dtTickets.Rows.Add(dtTickets.NewRow());
                    dtTickets.Rows[i][0] = foundRowTicket[i]["Log_Fecha_Mod"].ToString();
                    dtTickets.Rows[i][1] = foundRowTicket[i]["Log_Hora_Mod"].ToString();
                    dtTickets.Rows[i][2] = foundRowTicket[i]["Tipo_Documento"].ToString();
                    dtTickets.Rows[i][3] = foundRowTicket[i]["Dsc_Tipo_Ticket"].ToString();
                    dtTickets.Rows[i][4] = foundRowTicket[i]["Dsc_Compania"].ToString();
                    dtTickets.Rows[i][5] = foundRowTicket[i]["Num_Vuelo"].ToString();
                    dtTickets.Rows[i][6] = foundRowTicket[i]["Destino"].ToString();
                    dtTickets.Rows[i][7] = foundRowTicket[i]["Total_Ticket"].ToString();
                    dtTickets.Rows[i][8] = foundRowTicket[i]["Codigo"].ToString();
                    dtTickets.Rows[i][9] = foundRowTicket[i]["FlagCobro"].ToString();
                    dtTickets.Rows[i][10] = foundRowTicket[i]["Secuencial"].ToString();
                    dtTickets.Rows[i][11] = foundRowTicket[i]["Dsc_Trama_Bcbp"].ToString();
                }
            }


            DataTable dtCodTicket = new DataTable();
            dtCodTicket = dtTickets;
//          dtCodTicket = SelectDistinct(dtTickets, "Codigo");

            for (int i = 0; i < dtCodTicket.Rows.Count; i++)
            {
                DataRow[] foundRowCodTicket = dtTickets.Select("Codigo = '" + dtCodTicket.Rows[i][8] + "' AND Secuencial = '" + dtCodTicket.Rows[i][10] + "'");
                DataTable dtFechaTickets = new DataTable();
                dc = new DataColumn();
                dc.ColumnName = "Secuencial";
                dtFechaTickets.Columns.Add(dc);
                dc = new DataColumn();
                dc.ColumnName = "Log_Fecha_Mod";
                dtFechaTickets.Columns.Add(dc);
                dc = new DataColumn();
                dc.ColumnName = "Log_Hora_Mod";
                dtFechaTickets.Columns.Add(dc);

                if (foundRowCodTicket != null && foundRowCodTicket.Length > 0)
                {
                    for (int j = 0; j < foundRowCodTicket.Length; j++)
                    {
                        dtFechaTickets.Rows.Add(dtFechaTickets.NewRow());
                        dtFechaTickets.Rows[j][0] = foundRowCodTicket[j]["Secuencial"].ToString();
                        dtFechaTickets.Rows[j][1] = foundRowCodTicket[j]["Log_Fecha_Mod"].ToString();
                        dtFechaTickets.Rows[j][2] = foundRowCodTicket[j]["Log_Hora_Mod"].ToString();
                    }
                }

                DataRow[] foundRowFechTicket = null;

                if (foundRowCodTicket[0]["FlagCobro"].ToString() == "0")
                {
                    foundRowFechTicket = dtFechaTickets.Select("Secuencial = Max(Secuencial)");
                }
                else
                {
                    if (foundRowCodTicket[0]["FlagCobro"].ToString() == "1")
                    {
                        foundRowFechTicket = dtFechaTickets.Select("Secuencial = Min(Secuencial)");
                    }
                }

                if (foundRowFechTicket != null)
                {
                    dest.Rows.Add(dest.NewRow());
                    dest.Rows[i][0] = foundRowFechTicket[0]["Log_Fecha_Mod"].ToString();
                    dest.Rows[i][1] = foundRowFechTicket[0]["Log_Hora_Mod"].ToString();
                    dest.Rows[i][2] = foundRowCodTicket[0]["Tipo_Documento"].ToString();
                    dest.Rows[i][3] = foundRowCodTicket[0]["Dsc_Tipo_Ticket"].ToString();
                    dest.Rows[i][4] = foundRowCodTicket[0]["Dsc_Compania"].ToString();
                    dest.Rows[i][5] = foundRowCodTicket[0]["Num_Vuelo"].ToString();
                    dest.Rows[i][6] = foundRowCodTicket[0]["Destino"].ToString();
                    dest.Rows[i][7] = foundRowCodTicket[0]["Total_Ticket"].ToString();
                    dest.Rows[i][8] = "0";
                    dest.Rows[i][9] = foundRowCodTicket[0]["Total_Ticket"].ToString();
                    dest.Rows[i][10] = foundRowCodTicket[0]["Dsc_Trama_Bcbp"].ToString();
                }
            }

            DataRow[] foundRowBP = dtReporteDetalle.Select("Tipo_Documento = 'BCBP'");


            if (foundRowBP != null && foundRowBP.Length > 0)
            {
                int iTotalRows = dest.Rows.Count;

                if (iTotalRows > 0)
                {
                    for (int i = iTotalRows; i < iTotalRows + foundRowBP.Length; i++)
                    {
                        dest.Rows.Add(dest.NewRow());
                        dest.Rows[i][0] = foundRowBP[i - iTotalRows]["Log_Fecha_Mod"].ToString();
                        dest.Rows[i][1] = foundRowBP[i - iTotalRows]["Log_Hora_Mod"].ToString();
                        dest.Rows[i][2] = foundRowBP[i - iTotalRows]["Tipo_Documento"].ToString();
                        dest.Rows[i][3] = foundRowBP[i - iTotalRows]["Dsc_Tipo_Ticket"].ToString();
                        dest.Rows[i][4] = foundRowBP[i - iTotalRows]["Dsc_Compania"].ToString();
                        dest.Rows[i][5] = foundRowBP[i - iTotalRows]["Num_Vuelo"].ToString();
                        dest.Rows[i][6] = foundRowBP[i - iTotalRows]["Destino"].ToString();
                        dest.Rows[i][7] = "0";
                        dest.Rows[i][8] = foundRowBP[i - iTotalRows]["Total_BCP"].ToString();
                        dest.Rows[i][9] = foundRowBP[i - iTotalRows]["Total_BCP"].ToString();
                        dest.Rows[i][10] = foundRowBP[i - iTotalRows]["Dsc_Trama_Bcbp"].ToString();
                    }
                }
                else
                {
                    for (int i = 0; i < foundRowBP.Length; i++)
                    {
                        dest.Rows.Add(dest.NewRow());
                        dest.Rows[i][0] = foundRowBP[i]["Log_Fecha_Mod"].ToString();
                        dest.Rows[i][1] = foundRowBP[i]["Log_Hora_Mod"].ToString();
                        dest.Rows[i][2] = foundRowBP[i]["Tipo_Documento"].ToString();
                        dest.Rows[i][3] = foundRowBP[i]["Dsc_Tipo_Ticket"].ToString();
                        dest.Rows[i][4] = foundRowBP[i]["Dsc_Compania"].ToString();
                        dest.Rows[i][5] = foundRowBP[i]["Num_Vuelo"].ToString();
                        dest.Rows[i][6] = foundRowBP[i]["Destino"].ToString();
                        dest.Rows[i][7] = "0";
                        dest.Rows[i][8] = foundRowBP[i]["Total_BCP"].ToString();
                        dest.Rows[i][9] = foundRowBP[i]["Total_BCP"].ToString();
                        dest.Rows[i][10] = foundRowBP[i]["Dsc_Trama_Bcbp"].ToString();
                    }

                }
            }
        }
        dest.AcceptChanges();
        return dest;
    }


    private DataTable CrossTab(DataTable dtSourceTable)
    {

        DataTable dest = new DataTable("Result" + dtReporteDetalle.TableName);
        DataColumn dc;

        dc = new DataColumn();
        dc.ColumnName = "Log_Fecha_Mod";
        dest.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Tipo_Documento";
        dest.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Dsc_Tipo_Ticket";
        dest.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Dsc_Compania";
        dest.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Num_Vuelo";
        dest.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Destino";
        dest.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "TipoValor";
        dest.Columns.Add(dc);
        dest.Columns.Add("Valor", System.Type.GetType("System.Int32"));

        int i = 0;
        int j = 0;
        while (i < dtSourceTable.Rows.Count)
        {

            string sTramaBcbp = dtSourceTable.Rows[i]["Dsc_Trama_Bcbp"].ToString();
            Reader reader = new Reader();
            Hashtable ht;
            string sDestinoList;
            bool isAceptar = false;
            if (this.sDestino == string.Empty)
            {
                isAceptar = true;
            }
            else if (dtSourceTable.Rows[i]["Tipo_Documento"].ToString() == "Ticket")
            {
                isAceptar = false;
            }
            else
            {
                ht = reader.ParseString_Boarding(sTramaBcbp);
                sDestinoList = (String)ht["to_city_airport_code"];

                if (this.sDestino.Contains(sDestinoList))
                {
                    isAceptar = true;
                }
            }

            if (isAceptar)
            {
                dest.Rows.Add(dest.NewRow());
                dest.Rows[j]["Log_Fecha_Mod"] = dtSourceTable.Rows[i]["Log_Fecha_Mod"].ToString();
                dest.Rows[j]["Tipo_Documento"] = dtSourceTable.Rows[i]["Tipo_Documento"].ToString();
                dest.Rows[j]["Dsc_Tipo_Ticket"] = dtSourceTable.Rows[i]["Dsc_Tipo_Ticket"].ToString();
                dest.Rows[j]["Dsc_Compania"] = dtSourceTable.Rows[i]["Dsc_Compania"].ToString();
                dest.Rows[j]["Num_Vuelo"] = dtSourceTable.Rows[i]["Num_Vuelo"].ToString();
                dest.Rows[j]["Destino"] = dtSourceTable.Rows[i]["Destino"].ToString();
                dest.Rows[j]["TipoValor"] = "Ticket";
                dest.Rows[j]["Valor"] = Convert.ToInt32(dtSourceTable.Rows[i]["Total_Ticket"].ToString());

                dest.Rows.Add(dest.NewRow());
                dest.Rows[j + 1]["Log_Fecha_Mod"] = dtSourceTable.Rows[i]["Log_Fecha_Mod"].ToString();
                dest.Rows[j + 1]["Tipo_Documento"] = dtSourceTable.Rows[i]["Tipo_Documento"].ToString();
                dest.Rows[j + 1]["Dsc_Tipo_Ticket"] = dtSourceTable.Rows[i]["Dsc_Tipo_Ticket"].ToString();
                dest.Rows[j + 1]["Dsc_Compania"] = dtSourceTable.Rows[i]["Dsc_Compania"].ToString();
                dest.Rows[j + 1]["Num_Vuelo"] = dtSourceTable.Rows[i]["Num_Vuelo"].ToString();
                dest.Rows[j + 1]["Destino"] = dtSourceTable.Rows[i]["Destino"].ToString();
                dest.Rows[j + 1]["TipoValor"] = "BP";
                dest.Rows[j + 1]["Valor"] = Convert.ToInt32(dtSourceTable.Rows[i]["Total_BCP"].ToString());
                j = j + 2;
            }

            i++;
        }

        dest.AcceptChanges();
        return dest;

    }



    private DataTable SelectDistinct(DataTable SourceTable, params string[] FieldNames)
    {
        object[] lastValues;
        DataTable newTable;
        DataRow[] orderedRows;

        if (FieldNames == null || FieldNames.Length == 0)
            throw new ArgumentNullException("FieldNames");

        lastValues = new object[FieldNames.Length];
        newTable = new DataTable();

        foreach (string fieldName in FieldNames)
            newTable.Columns.Add(fieldName, SourceTable.Columns[fieldName].DataType);

        orderedRows = SourceTable.Select("", string.Join(", ", FieldNames));

        foreach (DataRow row in orderedRows)
        {
            if (!fieldValuesAreEqual(lastValues, row, FieldNames))
            {
                newTable.Rows.Add(createRowClone(row, newTable.NewRow(), FieldNames));

                setLastValues(lastValues, row, FieldNames);
            }
        }

        return newTable;
    }

    private bool fieldValuesAreEqual(object[] lastValues, DataRow currentRow, string[] fieldNames)
    {
        bool areEqual = true;

        for (int i = 0; i < fieldNames.Length; i++)
        {
            if (lastValues[i] == null || !lastValues[i].Equals(currentRow[fieldNames[i]]))
            {
                areEqual = false;
                break;
            }
        }

        return areEqual;
    }

    private DataRow createRowClone(DataRow sourceRow, DataRow newRow, string[] fieldNames)
    {
        foreach (string field in fieldNames)
            newRow[field] = sourceRow[field];

        return newRow;
    }

    private void setLastValues(object[] lastValues, DataRow sourceRow, string[] fieldNames)
    {
        for (int i = 0; i < fieldNames.Length; i++)
            lastValues[i] = sourceRow[fieldNames[i]];
    }

    #endregion


}
