using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using LAP.TUUA.CONTROL;
using LAP.TUUA.UTIL;

public partial class Rpt_DetalleLineaVuelo : System.Web.UI.Page
{
    protected Hashtable htLabels;
    protected bool Flg_Error;
    DataTable dt_consulta = new DataTable();
    DataTable dt_parametro = new DataTable();
    DataTable dt_allcompania = new DataTable();
    BO_Consultas objConsulta = new BO_Consultas();
    UIControles objCargaComboCompania = new UIControles();

    string sFechaDesde;
    string sFechaHasta;
    string sCompania;
    string idDscC;
    string sFechaEstadistico;
    string sTipoImp;
    string tipoDocumento = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            sFechaDesde = Fecha.convertToFechaSQL(Request.QueryString["sFechaDesde"]);
            sFechaHasta = Fecha.convertToFechaSQL(Request.QueryString["sFechaHasta"]);
            sCompania = Request.QueryString["sCompania"];
            idDscC = Request.QueryString["idDscC"];
            sFechaEstadistico = Request.QueryString["sFechaEstadistico"];
            sTipoImp = Request.QueryString["sTipoImp"];

            //Validacion Limite de Rango de Fechas en Reportes 
            DataTable dt_parametro = objConsulta.ListarParametrosDefaultValue("LR");

            if (dt_parametro.Rows.Count > 0)
            {
                //RecuperarFiltros();
                DateTime fechaF = Convert.ToDateTime(Request.QueryString["sFechaHasta"]);
                DateTime fechaI = Convert.ToDateTime(Request.QueryString["sFechaDesde"]);
                TimeSpan ts = fechaF.Subtract(fechaI);
                int dias = ts.Days;
                int parametro = Convert.ToInt32(dt_parametro.Rows[0].ItemArray.GetValue(4).ToString());

                if (dias > parametro)
                {
                    lblMensajeError.Text = "El periodo máximo de días a imprimir el reporte es" + " " + parametro.ToString() + " " + "días.";
                }
                else
                {
                    if (sTipoImp.Equals("D"))
                    {
                        //imprime detalle reportes
                        ImprimirDetalle(sFechaDesde, sFechaHasta, sCompania, null, 0, 0, "1", "0");
                    }
                    else { 
                        //imprime resumen
                        cargarSubTotalesResumen(sFechaDesde, sFechaHasta, sCompania);
                        DataTable dt_Resumen = cargarResumen(sFechaDesde, sFechaHasta, sCompania);

                        //Pintar el reporte 
                        CrystalDecisions.CrystalReports.Engine.ReportDocument obRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                        obRpt.Load(Server.MapPath("").ToString() + @"\DetallexLineaVueloResumen.rpt");
                        //Poblar el reporte con el datatable
                        obRpt.SetDataSource(dt_Resumen);
                        //obRpt.SetParameterValue("sFechaInicio", Fecha.convertSQLToFecha(sFechaDesde, null));
                        //obRpt.SetParameterValue("sFechaFinal", Fecha.convertSQLToFecha(sFechaHasta, null));
                        //obRpt.SetParameterValue("sCompania", idDscC);
                        //obRpt.SetParameterValue("pFechaEstadistico", sFechaEstadistico);
                        crvDetalleLineaVuelo.ReportSource = obRpt;
                    }
                    //dt_consulta = objConsulta.ObtenerDetallexLineaVuelo(sFechaDesde, sFechaHasta, sCompania, null, 0, 0, "1", "0");


                    //if (dt_consulta.Rows.Count < 1)
                    //{

                    //    htLabels = LabelConfig.htLabels;
                    //    try
                    //    {
                    //        this.lblMensajeError.Text = htLabels["mconsultaNoData.lblMensajeError.Text"].ToString();
                    //    }
                    //    catch (Exception ex)
                    //    {
                    //        Flg_Error = true;
                    //        ErrorHandler.Cod_Error = Define.ERR_008;
                    //        ErrorHandler.Trace(ErrorHandler.htErrorType[Define.ERR_008].ToString(), ex.Message);
                    //    }
                    //    finally
                    //    {
                    //        if (Flg_Error)
                    //        {
                    //            Response.Redirect("PaginaError.aspx");
                    //        }
                    //    }


                    //}
                    //else
                    //{
                    //    //Pintar el reporte 
                    //    CrystalDecisions.CrystalReports.Engine.ReportDocument obRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                    //    obRpt.Load(Server.MapPath("").ToString() + @"\DetallexLineaVueloImpeso.rpt");
                    //    //Poblar el reporte con el datatable
                    //    obRpt.SetDataSource(dt_consulta);
                    //    obRpt.SetParameterValue("sFechaInicio", Fecha.convertSQLToFecha(sFechaDesde, null));
                    //    obRpt.SetParameterValue("sFechaFinal", Fecha.convertSQLToFecha(sFechaHasta, null));
                    //    obRpt.SetParameterValue("sCompania", idDscC);
                    //    obRpt.SetParameterValue("pFechaEstadistico", sFechaEstadistico);
                    //    crvDetalleLineaVuelo.ReportSource = obRpt;

                    //}
                }
            }

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

    public void ImprimirDetalle(string sFechaDesde,string  sFechaHasta,string sCompania, string valor, int valor2, int valor3, string valor4, string valor5)
    {
        dt_consulta = objConsulta.ObtenerDetallexLineaVuelo(sFechaDesde, sFechaHasta, sCompania, valor, 0, 0, "1", "0");

        if (dt_consulta.Rows.Count < 1)
        {

            htLabels = LabelConfig.htLabels;
            try
            {
                this.lblMensajeError.Text = htLabels["mconsultaNoData.lblMensajeError.Text"].ToString();
            }
            catch (Exception ex)
            {
                Flg_Error = true;
                ErrorHandler.Cod_Error = Define.ERR_008;
                ErrorHandler.Trace(ErrorHandler.htErrorType[Define.ERR_008].ToString(), ex.Message);
            }
            finally
            {
                if (Flg_Error)
                {
                    Response.Redirect("PaginaError.aspx");
                }
            }

        }
        else
        {
            //Pintar el reporte 
            CrystalDecisions.CrystalReports.Engine.ReportDocument obRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            obRpt.Load(Server.MapPath("").ToString() + @"\DetallexLineaVueloImpeso.rpt");
            //Poblar el reporte con el datatable
            obRpt.SetDataSource(dt_consulta);
            obRpt.SetParameterValue("sFechaInicio", Fecha.convertSQLToFecha(sFechaDesde, null));
            obRpt.SetParameterValue("sFechaFinal", Fecha.convertSQLToFecha(sFechaHasta, null));
            obRpt.SetParameterValue("sCompania", idDscC);
            obRpt.SetParameterValue("pFechaEstadistico", sFechaEstadistico);
            crvDetalleLineaVuelo.ReportSource = obRpt;

        }
    }

    private void cargarSubTotalesResumen(string sFechaDesde, string sFechaHasta, string sCompania)
    {
        try
        {
            dt_consulta = objConsulta.ObtenerDetallexLineaVuelo(sFechaDesde,
                                                                    sFechaHasta,
                                                                    sCompania,
                                                                    null,
                                                                    0, 0, "2", "2");
        }
        catch (Exception ex)
        {
            Flg_Error = true;
            ErrorHandler.Cod_Error = Define.ERR_008;
            ErrorHandler.Trace(ErrorHandler.htErrorType[Define.ERR_008].ToString(), ex.Message);
        }
        finally
        {
            if (Flg_Error)
            {
                Response.Redirect("PaginaError.aspx");
            }
        }

        ViewState["SubTotalesResumen"] = dt_consulta;
    }

    private DataTable cargarResumen(string sFechaDesde, string sFechaHasta, string sCompania)
    {
        DataTable dtResumenTotal = new DataTable();
        try
        {
            DataTable dtReportResumen = new DataTable();
            dtReportResumen = objConsulta.ObtenerDetallexLineaVuelo(sFechaDesde,
                                                                       sFechaHasta,
                                                                       sCompania,
                                                                       null,
                                                                       0, 0, "0", "0");

           
            dtResumenTotal = dtReportResumen.Clone();
            int rows = dtReportResumen.Rows.Count;
            DataRow fila;

            // Calculamos los totales por documento
            DataTable dt_Total = (DataTable)ViewState["SubTotalesResumen"];
            tipoDocumento = dtReportResumen.Rows[0]["Tip_Documento"].ToString();
            int cont = 0;
            string documento = "";
            object totalUtil = null;
            object totalVend = null;

            for (int k = 0; k < rows; k++)
            {
                if (tipoDocumento != dtReportResumen.Rows[k]["Tip_Documento"].ToString())
                {
                    if (k == rows - 1) //es la ultima posicion
                    {
                        fila = dtReportResumen.Rows[k];
                        dtResumenTotal.ImportRow(fila);

                        documento = dtReportResumen.Rows[k]["Tip_Documento"].ToString();
                        totalUtil = dt_Total.Select("Tip_Documento = '" + documento + "'")[0]["Cnt_Utilizada"].ToString();
                        totalVend = dt_Total.Select("Tip_Documento = '" + documento + "'")[0]["Cnt_Vendida"].ToString();

                        dtResumenTotal.Rows.Add(dtResumenTotal.NewRow());
                        dtResumenTotal.Rows[k + 2]["Tip_Vuelo"] = "Total";
                        dtResumenTotal.Rows[k + 2]["Cnt_Utilizada"] = totalUtil.ToString();
                        dtResumenTotal.Rows[k + 2]["Cnt_Vendida"] = totalVend.ToString();
                    }
                    else
                    {
                        if (cont == 0) //si es por primera vez
                        {
                            //adicionando subtotal documento previo
                            totalUtil = dt_Total.Select("Tip_Documento = '" + tipoDocumento + "'")[0]["Cnt_Utilizada"].ToString();
                            totalVend = dt_Total.Select("Tip_Documento = '" + tipoDocumento + "'")[0]["Cnt_Vendida"].ToString();

                            dtResumenTotal.Rows.Add(dtResumenTotal.NewRow());
                            dtResumenTotal.Rows[k]["Tip_Vuelo"] = "Total";
                            dtResumenTotal.Rows[k]["Cnt_Utilizada"] = totalUtil.ToString();
                            dtResumenTotal.Rows[k]["Cnt_Vendida"] = totalVend.ToString();

                            //adicionando el documento actual
                            fila = dtReportResumen.Rows[k];
                            dtResumenTotal.ImportRow(fila);
                        }
                        else
                        {
                            //adicionando el documento actual
                            fila = dtReportResumen.Rows[k];
                            dtResumenTotal.ImportRow(fila);
                        }
                        cont++;
                    }
                }
                else
                {
                    if (k == rows - 1)//ultima posicion en caso haya solo un tipo de documento
                    {
                        fila = dtReportResumen.Rows[k];
                        dtResumenTotal.ImportRow(fila);

                        documento = dtReportResumen.Rows[k]["Tip_Documento"].ToString();
                        totalUtil = dt_Total.Select("Tip_Documento = '" + documento + "'")[0]["Cnt_Utilizada"].ToString();
                        totalVend = dt_Total.Select("Tip_Documento = '" + documento + "'")[0]["Cnt_Vendida"].ToString();

                        dtResumenTotal.Rows.Add(dtResumenTotal.NewRow());
                        dtResumenTotal.Rows[k + 1]["Tip_Vuelo"] = "Total";
                        dtResumenTotal.Rows[k + 1]["Cnt_Utilizada"] = totalUtil.ToString();
                        dtResumenTotal.Rows[k + 1]["Cnt_Vendida"] = totalVend.ToString();
                    }
                    else
                    {
                        fila = dtReportResumen.Rows[k];
                        dtResumenTotal.ImportRow(fila);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Flg_Error = true;
            ErrorHandler.Cod_Error = Define.ERR_008;
            ErrorHandler.Trace(ErrorHandler.htErrorType[Define.ERR_008].ToString(), ex.Message);
        }
        finally
        {
            if (Flg_Error)
            {
                Response.Redirect("PaginaError.aspx");
            }
        }

        return dtResumenTotal;

    }



}
