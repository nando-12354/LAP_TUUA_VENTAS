using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using LAP.TUUA.UTIL;
using LAP.TUUA.CONTROL;

public partial class ReporteRPT_rptMovimientoTicketContingencia : System.Web.UI.Page
{

    protected bool Flg_Error;
    DataTable dt_consulta = new DataTable();
    BO_Reportes objListarTicketContingenciaxFecha = new BO_Reportes();

    protected void Page_Load(object sender, EventArgs e)
    {

        string ls_CodTipoTicket = Request.QueryString["codtpticket"];
        string ls_DesTipoTicket = Request.QueryString["destpticket"];
        string ls_CodEstadoTicket = Request.QueryString["codesticket"];
        string ls_DesEstadoTicket = Request.QueryString["desesticket"];

        string ls_RangoInicial = Request.QueryString["raninicial"];
        string ls_RangoFinal = Request.QueryString["ranfinal"];

        string ls_FechaInicial = Request.QueryString["fechainicial"];
        string ls_FechaFinal = Request.QueryString["fechafinal"];
        string ls_CodEstadoInicial = Request.QueryString["codesiniticket"];
        string ls_DesEstadoInicial = Request.QueryString["desesiniticket"];

        if (Session["ConsultaTicketConti"] != null)
        {
            dt_consulta = (DataTable)Session["ConsultaTicketConti"];
            generarReporte(ls_FechaInicial, ls_FechaFinal, 
                           ls_DesEstadoInicial, ls_DesTipoTicket, 
                           ls_DesEstadoTicket, ls_RangoInicial, ls_RangoFinal);
        }
        else
        {
            consultarDatos(ls_FechaInicial, ls_FechaFinal, 
                           ls_CodEstadoInicial, ls_CodTipoTicket, 
                           ls_CodEstadoTicket, ls_RangoInicial, ls_RangoFinal);
            
            generarReporte(ls_FechaInicial, ls_FechaFinal, 
                           ls_DesEstadoInicial, ls_DesTipoTicket, 
                           ls_DesEstadoTicket, ls_RangoInicial, ls_RangoFinal);
        }
    }

    public void generarReporte(string ls_FechaDesde, string ls_FechaHasta, string ls_Estado, string ls_TipoTicket, string ls_EstadoTicket, string ls_RangoInicial, string ls_RangoFinal)
    {
        try
        {
            if (dt_consulta == null || dt_consulta.Rows.Count == 0)
            {
                invocarMensaje(0);
                lblmensaje.Text = "La busqueda no retorna resultado, regrese a la consulta para un nuevo filtro";
            }
            else
            {
                DataTable dtResumen = ObtenerDataResumen();

                DataSet dtset = new DataSet();

                dt_consulta.TableName = "dtFiltroTicketContingencia";
                dtset.Tables.Add(dt_consulta.Copy());
                dtResumen.TableName = "dtResumenContingencia";
                dtset.Tables.Add(dtResumen.Copy());

                //Pintar el reporte                 
                CrystalDecisions.CrystalReports.Engine.ReportDocument obRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                obRpt.Load(Server.MapPath("").ToString() + @"\rptMovimientoTicketContingencia.rpt");
                obRpt.SetDataSource(dtset);
                obRpt.SetParameterValue("pFechaInicial", ls_FechaDesde);
                obRpt.SetParameterValue("pFechaFinal", ls_FechaHasta);
                obRpt.SetParameterValue("pEstadoInicial", ls_Estado);
                obRpt.SetParameterValue("pTipoTicket", ls_TipoTicket);
                obRpt.SetParameterValue("pEstadoTicket", ls_EstadoTicket);

                if (ls_RangoInicial == "0")
                {
                    ls_RangoInicial = "-";
                }

                if (ls_RangoFinal == "0")
                {
                    ls_RangoFinal = "-";
                }

                obRpt.SetParameterValue("pRangoInicial", ls_RangoInicial);
                obRpt.SetParameterValue("pRangoFinal", ls_RangoFinal);


                crptvTicketContingencia.ReportSource = obRpt;
                invocarMensaje(1);
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

    public void consultarDatos(string ls_FechaDesde, string ls_FechaHasta, 
                                string ls_Estado, string ls_TipoTicket, 
                                string ls_EstadoTicket, string ls_RangoInicial, 
                                string ls_RangoFinal)
    {
        try
        {

            if (ls_FechaDesde != "")
            {
                string[] wordsFechaDesde = ls_FechaDesde.Split('/');
                ls_FechaDesde = wordsFechaDesde[2] + "" + wordsFechaDesde[1] + "" + wordsFechaDesde[0];
            }
            else { ls_FechaDesde = ""; }

            if (ls_FechaHasta != "")
            {
                string[] wordsFechaHasta = ls_FechaHasta.Split('/');
                ls_FechaHasta = wordsFechaHasta[2] + "" + wordsFechaHasta[1] + "" + wordsFechaHasta[0];
            }
            else { ls_FechaHasta = ""; }
            
            dt_consulta = objListarTicketContingenciaxFecha.obtenerMovimientoTicketContingencia(ls_FechaDesde, ls_FechaHasta, 
                                                                                                ls_Estado, ls_TipoTicket, 
                                                                                                ls_EstadoTicket, ls_RangoInicial, 
                                                                                                ls_RangoFinal);

            if (!(ls_RangoInicial == "0" && ls_RangoFinal == "0"))
            {
                dt_consulta = EvaluarRangos(dt_consulta, ls_RangoInicial, ls_RangoFinal);
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

    public DataTable EvaluarRangos(DataTable dt_consulta,string sRangoInicial,string sRangofinal)
    {
        var query2 = from dtRpt in dt_consulta.AsEnumerable()
                     where (dtRpt.Field<String>("Cod_Numero_Ticket").CompareTo(sRangoInicial) >= 0 && dtRpt.Field<String>("Cod_Numero_Ticket").CompareTo(sRangofinal) <= 0)
                     select dtRpt;

        //Create a table from the query.
        if (query2.Count() > 0)
            dt_consulta = query2.CopyToDataTable();
        else
            dt_consulta = null;

        return dt_consulta;
    }

    public void invocarMensaje(int li_tipo)
    {
        hbandera.Value = Convert.ToString(li_tipo);
    }

    public DataTable obtenerResumenStock()
    {
        DataTable dtResumen = new DataTable();
        //Cargar Estados
        BO_Consultas objListaCampos = new BO_Consultas();
        DataTable dt_estado = new DataTable();
        dt_estado = objListaCampos.ListaCamposxNombre("EstadoTicket");

        //Cargar Tipo ticket
        BO_Administracion objListaTipoTicket = new BO_Administracion();
        DataTable dt_tipoticket = new DataTable();
        dt_tipoticket = objListaTipoTicket.ListaTipoTicket();
        Hashtable totTipoEstado = new Hashtable();

        foreach (DataRow dtRowTipo in dt_tipoticket.Rows)
        {
            //double totMontoTipo = 0.0;
            decimal totMontoTipo = 0.0M;
            String codTipoTicket = (String)dtRowTipo["Cod_Tipo_Ticket"];
            String nomTipoTicket = (String)dtRowTipo["Dsc_Tipo_Ticket"];
            bool isBanderaOcult = true;

            foreach (DataRow dtRowEstado in dt_estado.Rows)
            {
                //double totMonto = 0.0;
                decimal totMonto = 0.00M;
                int total = 0;
                String codEstadoTicket = (String)dtRowEstado["Cod_Campo"];
                String nomEstadoTicket = (String)dtRowEstado["Dsc_Campo"];

                DataRow[] dtfiltro = dt_consulta.Select("Cod_Tipo_Ticket = '" + codTipoTicket + "' and Tip_Estado_Actual ='" + codEstadoTicket + "'");
                int tam = dtfiltro.Count();
                if (tam > 0)
                {
                    foreach (DataRow dtrow in dtfiltro)
                    {
                        total++;
                        //totMonto = totMonto + (Decimal)dtrow["Monto_Ticket"];
                    }
                    ArrayList rsultotal = new ArrayList();
                    rsultotal.Add(total);
                    rsultotal.Add(totMonto);
                    totMontoTipo = totMontoTipo + totMonto;
                    totTipoEstado.Add(codTipoTicket + "*" + codEstadoTicket, rsultotal);
                }
            }


            //PARA EL ESTADO FANTASMA DE VENDIDO
            //double totMonto = 0.0;
            decimal totMonto1 = 0.00M;
            int total1 = 0;
            String codEstadoTicket1 = "Z";
            String nomEstadoTicket1 = "VENDIDO";

            DataRow[] dtfiltro1 = dt_consulta.Select("Cod_Tipo_Ticket = '" + codTipoTicket + "' and Cod_Estado_Ticket ='Z'");
            int tam1 = dtfiltro1.Count();
            if (tam1 > 0)
            {
                foreach (DataRow dtrow1 in dtfiltro1)
                {
                    try
                    {
                        totMonto1 = totMonto1 + (Decimal)dtrow1["Monto_Ticket"];

                        //kinzi
                        decimal tmp_Amount = 0.00M;
                        tmp_Amount = (Decimal)dtrow1["Monto_Ticket"];
                        if (tmp_Amount > 0)
                            total1++;
                    }
                    catch (Exception ex)
                    {

                    }
                }
                ArrayList rsultotal = new ArrayList();
                rsultotal.Add(total1);
                rsultotal.Add(totMonto1);
                totMontoTipo = totMontoTipo + totMonto1;
                totTipoEstado.Add(codTipoTicket + "*" + codEstadoTicket1, rsultotal);
            }

            totTipoEstado.Add(codTipoTicket, decimal.Round(totMontoTipo, 2));
        }

        //
        //DataTable dtResumen = new DataTable();
        DataColumn dtColumnaTipo = new DataColumn();
        dtColumnaTipo.DataType = System.Type.GetType("System.String");
        dtColumnaTipo.ColumnName = "TIPO TICKET";
        dtResumen.Columns.Add(dtColumnaTipo);
        ArrayList lstStadosCod = new ArrayList();
        ArrayList lstStadosNom = new ArrayList();

        bool isbanderaOculta = true;

        foreach (DataRow dtRow in dt_estado.Rows)
        {
            string nomEstado = (String)dtRow["Dsc_Campo"];
            string codEstado = (String)dtRow["Cod_Campo"];
            bool isbandera = true;

            foreach (DataRow dtRowConsult in dt_consulta.Rows)
            {
                String codEstadoConsult = (String)dtRowConsult["Tip_Estado_Actual"];
                if (codEstado.Equals(codEstadoConsult))
                {
                    if (isbandera)
                    {
                        DataColumn dtColumna = new DataColumn();
                        dtColumna.DataType = System.Type.GetType("System.String");
                        lstStadosCod.Add(codEstado);
                        lstStadosNom.Add(nomEstado);
                        dtColumna.ColumnName = nomEstado;
                        dtResumen.Columns.Add(dtColumna);
                        isbandera = false;
                        break;
                    }
                }
            }
        }

        foreach (DataRow dtRowConsult in dt_consulta.Rows)
        {
            String codEstadoConsult = "Z";
            //String codEstadoConsult = (String)dtRowConsult["Cod_Estado_Ticket"];
            //Para el Estado Fatasma de Ventas
            if (codEstadoConsult.Equals("Z"))
            {
                if (isbanderaOculta)
                {
                    DataColumn dtColumna = new DataColumn();
                    dtColumna.DataType = System.Type.GetType("System.String");
                    lstStadosCod.Add("Z");
                    lstStadosNom.Add("VENDIDO");
                    dtColumna.ColumnName = "VENDIDO";
                    dtResumen.Columns.Add(dtColumna);
                    isbanderaOculta = false;
                    break;
                }
            }
        }

        DataColumn dtColumnaTipoFin = new DataColumn();
        dtColumnaTipoFin.DataType = System.Type.GetType("System.String");
        dtColumnaTipoFin.ColumnName = "MONTO($)";
        dtResumen.Columns.Add(dtColumnaTipoFin);

        foreach (DataRow dtRowTipo in dt_tipoticket.Rows)
        {
            bool isVale = false;
            String codTipoTicket = (String)dtRowTipo["Cod_Tipo_Ticket"];
            String nomTipoTicket = (String)dtRowTipo["Dsc_Tipo_Ticket"];
            DataRow nRow;
            nRow = dtResumen.NewRow();
            nRow["TIPO TICKET"] = nomTipoTicket;
            for (int i = 0; i < lstStadosCod.Count; i++)
            {
                String nomCampo = (String)lstStadosNom[i];
                String codCampo = (String)lstStadosCod[i];
                ArrayList lstDatos = new ArrayList();
                lstDatos = (ArrayList)totTipoEstado[codTipoTicket + '*' + codCampo];
                if (lstDatos == null)
                {
                    nRow[nomCampo] = "0";
                }
                else
                {
                    nRow[nomCampo] = Convert.ToString((Int32)lstDatos[0]);
                    isVale = true;
                }
            }
            if (isVale)
            {
                decimal totMontoTipo = (Decimal)totTipoEstado[codTipoTicket];
                nRow["MONTO($)"] = Convert.ToString(totMontoTipo);
                dtResumen.Rows.Add(nRow);
            }
        }
        DataTable dtValor = formatearResumenRpt(dtResumen);

        return dtValor;

    }


    public DataTable formatearResumenRpt(DataTable dtValor)
    {
        DataTable dtResumenRpt = new DataTable();
        dtResumenRpt.Columns.Add(new DataColumn("Indice", System.Type.GetType("System.String")));
        dtResumenRpt.Columns.Add(new DataColumn("Cabecera", System.Type.GetType("System.String")));
        dtResumenRpt.Columns.Add(new DataColumn("Valor", System.Type.GetType("System.String")));

        int iContR = 0;
        foreach (DataRow dtRow in dtValor.Rows)
        {
            int iContC = 0;

            foreach (DataColumn dtColum in dtValor.Columns)
            {
                DataRow[] ArrData = dtValor.Select();
                string sValor = ArrData[iContR][iContC].ToString(); ;
                
                DataRow dtNRow = dtResumenRpt.NewRow();
                dtNRow["Indice"] = iContR.ToString();
                string sColumna = dtColum.ColumnName;

                if ("TIPO TICKET" == dtColum.ColumnName)
                {
                    sColumna = "AAAAA;" + sColumna;
                }

                if ("MONTO($)" == dtColum.ColumnName)
                {
                    sColumna = "ZZZZZ;" + sColumna;
                }

                dtNRow["Cabecera"] = sColumna;
                dtNRow["Valor"] = sValor;
                dtResumenRpt.Rows.Add(dtNRow);
                iContC++;
            }
            iContR++;

        }
        return dtResumenRpt;

    }



    public DataTable ObtenerDataResumen()
    {
        BO_Consultas objListaCampos = new BO_Consultas();
        BO_Administracion objListaTipoTicket = new BO_Administracion();

        DataTable dtResumen = new DataTable();

        //Cargar Estados
        DataTable dt_estado = new DataTable();
        dt_estado = objListaCampos.ListaCamposxNombre("EstadoTicket");

        //Agregamos a la tabla estado el tipo VENCIDO
        DataRow newEstado = dt_estado.NewRow();
        newEstado["Cod_Campo"] = "V";
        newEstado["Dsc_Campo"] = "VENCIDO";
        dt_estado.Rows.Add(newEstado);

        //Cargar Tipo ticket
        DataTable dt_tipoticket = new DataTable();
        dt_tipoticket = objListaTipoTicket.ListaTipoTicket();

        Hashtable totTipoEstado = new Hashtable();

        foreach (DataRow dtRowTipo in dt_tipoticket.Rows)
        {
            decimal totMontoTipo = 0.0M;
            String codTipoTicket = (String)dtRowTipo["Cod_Tipo_Ticket"];

            foreach (DataRow dtRowEstado in dt_estado.Rows)
            {
                decimal totMonto = 0.00M;
                int total = 0;

                String codEstadoTicket = (String)dtRowEstado["Cod_Campo"];

                DataRow[] dtfiltro = dt_consulta.Select("Cod_Tipo_Ticket = '" + codTipoTicket + "' AND Tip_Estado_Actual ='" + codEstadoTicket + "'");
                int tam = dtfiltro.Count();
                if (tam > 0)
                {
                    foreach (DataRow dtrow in dtfiltro)
                    {
                        total++;
                        //totMonto = totMonto + (Decimal)dtrow["Monto_Ticket"];
                    }
                    ArrayList rsultotal = new ArrayList();
                    rsultotal.Add(total);
                    rsultotal.Add(totMonto);
                    totTipoEstado.Add(codTipoTicket + "*" + codEstadoTicket, rsultotal);
                }
            }

            //PARA EL ESTADO FANTASMA DE VENDIDO
            decimal totMontoVenta = 0.00M;
            int total1 = 0;

            if (dt_consulta.Rows.Count > 0)
            {
                var lqList = from rowConsulta in dt_consulta.AsEnumerable()
                             where rowConsulta.Field<String>("Cod_Tipo_Ticket") == codTipoTicket && rowConsulta.Field<String>("Cod_Estado_Ticket") == "Z"
                             group rowConsulta by rowConsulta.Field<String>("Cod_Numero_Ticket") into g
                             select new { Category = g.Key, Monto = g.Min(rowConsulta => rowConsulta.Field<Decimal>("Monto_Ticket")) };

                if (lqList.Count() > 0)
                {
                    foreach (var cTicket in lqList)
                    {
                        try
                        {
                            totMontoVenta = totMontoVenta + (Decimal)cTicket.Monto;

                            //kinzi
                            decimal tmp_Amount = 0.00M;
                            tmp_Amount = (Decimal)cTicket.Monto;
                            if (tmp_Amount > 0)
                                total1++;
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                    ArrayList rsultotal = new ArrayList();
                    rsultotal.Add(total1);
                    rsultotal.Add(totMontoVenta);
                    totMontoTipo = totMontoTipo + totMontoVenta;
                    totTipoEstado.Add(codTipoTicket + "*" + "Z", rsultotal);
                }
            }
            totTipoEstado.Add(codTipoTicket, decimal.Round(totMontoTipo, 2));
        }

        //Crea las columnas Tipo Ticket en la tabla Resumen
        DataColumn dtColumnaTipo = new DataColumn();
        dtColumnaTipo.DataType = System.Type.GetType("System.String");
        dtColumnaTipo.ColumnName = "TIPO TICKET";
        dtResumen.Columns.Add(dtColumnaTipo);

        ArrayList lstStadosCod = new ArrayList();
        ArrayList lstStadosNom = new ArrayList();

        bool isbanderaOculta = true;

        DataRow[] ArrRow = dt_estado.Select("", "Dsc_Campo asc");

        //Crea las columnas Dinámicas de Estados en la tabla Resumen
        foreach (DataRow dtRow in ArrRow)
        {
            string nomEstado = (String)dtRow["Dsc_Campo"];
            string codEstado = (String)dtRow["Cod_Campo"];
            bool isbandera = true;

            foreach (DataRow dtRowConsult in dt_consulta.Rows)
            {
                String codEstadoConsult = (String)dtRowConsult["Tip_Estado_Actual"];
                if (codEstado.Equals(codEstadoConsult))
                {
                    if (isbandera)
                    {
                        DataColumn dtColumna = new DataColumn();
                        dtColumna.DataType = System.Type.GetType("System.String");
                        lstStadosCod.Add(codEstado);
                        lstStadosNom.Add(nomEstado);
                        dtColumna.ColumnName = nomEstado;
                        dtResumen.Columns.Add(dtColumna);
                        isbandera = false;
                        break;
                    }
                }
            }
        }

        //Crea la columna Vendidos en la tabla Resumen
        foreach (DataRow dtRowConsult in dt_consulta.Rows)
        {
            String codEstadoConsult = "Z";
            //Para el Estado Fatasma de Ventas
            if (codEstadoConsult.Equals("Z"))
            {
                if (isbanderaOculta)
                {
                    DataColumn dtColumna = new DataColumn();
                    dtColumna.DataType = System.Type.GetType("System.String");
                    lstStadosCod.Add("Z");
                    lstStadosNom.Add("VENDIDO");
                    dtColumna.ColumnName = "VENDIDO";
                    dtResumen.Columns.Add(dtColumna);
                    isbanderaOculta = false;
                    break;
                }
            }
        }

        //Crea la columna Montos en la tabla Resumen
        DataColumn dtColumnaTipoFin = new DataColumn();
        dtColumnaTipoFin.DataType = System.Type.GetType("System.String");
        dtColumnaTipoFin.ColumnName = "MONTO($)";
        dtResumen.Columns.Add(dtColumnaTipoFin);

        //Llena los registros en la tabla Resumen
        foreach (DataRow dtRowTipo in dt_tipoticket.Rows)
        {
            bool isVale = false;
            String codTipoTicket = (String)dtRowTipo["Cod_Tipo_Ticket"];
            String nomTipoTicket = (String)dtRowTipo["Dsc_Tipo_Ticket"];
            DataRow nRow;
            nRow = dtResumen.NewRow();
            nRow["TIPO TICKET"] = nomTipoTicket;
            for (int i = 0; i < lstStadosCod.Count; i++)
            {
                String nomCampo = (String)lstStadosNom[i];
                String codCampo = (String)lstStadosCod[i];
                ArrayList lstDatos = new ArrayList();
                lstDatos = (ArrayList)totTipoEstado[codTipoTicket + '*' + codCampo];
                if (lstDatos == null)
                {
                    nRow[nomCampo] = "0";
                }
                else
                {
                    nRow[nomCampo] = Convert.ToString((Int32)lstDatos[0]);
                    isVale = true;
                }
            }
            if (isVale)
            {
                decimal totMontoTipo = (Decimal)totTipoEstado[codTipoTicket];
                nRow["MONTO($)"] = Convert.ToString(totMontoTipo);
                dtResumen.Rows.Add(nRow);
            }
        }
        DataTable dtValor = formatearResumenRpt(dtResumen);
        return dtValor;        
    }

}
