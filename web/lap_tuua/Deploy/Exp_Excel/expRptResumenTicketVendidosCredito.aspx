<%@ Page Language="C#" ContentType="text/xml" %> 
<%@ Import Namespace="CarlosAg.ExcelXmlWriter"  %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="LAP.TUUA.CONTROL" %>
<%@ Import Namespace="LAP.TUUA.UTIL" %>

<%
        BO_Reportes objReporte = new BO_Reportes();
        DataTable dt_consulta = new DataTable();
        DataTable dt_resumen = new DataTable();

        //Set Paramaters values
        string ls_Fecha_Inicial = Fecha.convertToFechaSQL2(Request.QueryString["sDesde"]);
        string ls_Fecha_Final = Fecha.convertToFechaSQL2(Request.QueryString["sHasta"]);
        string ls_CodTipoTicket = Request.QueryString["STicket"];
        string ls_NumVuelo = Request.QueryString["sVuelo"];
        string ls_CodAerolinea = Request.QueryString["sCompania"];
        string ls_CodPago = Request.QueryString["sPago"];

        dt_consulta = objReporte.ListarResumenTicketVendidosCreditoPagin(ls_Fecha_Inicial,
                                                                           ls_Fecha_Final, 
                                                                           ls_CodTipoTicket, 
                                                                           ls_NumVuelo, 
                                                                           ls_CodAerolinea, 
                                                                           ls_CodPago, 
                                                                           "0", null, 0,0, "0");
    
        Workbook book = new Workbook();
        Worksheet sheet = book.Worksheets.Add("Rep. Tickets Vendidos Credito");

        /******************** ESTILOS ********************/
        WorksheetStyle style = book.Styles.Add("HeaderStyle");
        style.Font.FontName = "Arial";
        style.Font.Size = 10;
        style.Font.Bold = true;
        style.Alignment.Horizontal = StyleHorizontalAlignment.Center;
        style.Alignment.Vertical = StyleVerticalAlignment.Center;
        style.Alignment.WrapText = true;
        style.Font.Color = "#000000";
        style.Font.Underline = UnderlineStyle.Single;
        style.Interior.Color = "#E2E2E2";
        style.Interior.Pattern = StyleInteriorPattern.Solid;
        style.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1, "#575757");
        style.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1, "#575757");
        style.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1, "#575757");
        style.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1, "#575757");

        WorksheetStyle sAnulado = book.Styles.Add("anulado");
        sAnulado.Interior.Color = "#FFCECE";
        sAnulado.Interior.Pattern = StyleInteriorPattern.Solid;
        style.Alignment.WrapText = true;
        sAnulado.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1, "#EEEEEE");
        sAnulado.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1, "#EEEEEE");
        sAnulado.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1, "#EEEEEE");
        sAnulado.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1, "#EEEEEE");

        WorksheetStyle sRehabilitado = book.Styles.Add("rehabilitado");
        sRehabilitado.Interior.Color = "#FFE0C1";
        sRehabilitado.Interior.Pattern = StyleInteriorPattern.Solid;
        style.Alignment.WrapText = true;
        sRehabilitado.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1, "#EEEEEE");
        sRehabilitado.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1, "#EEEEEE");
        sRehabilitado.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1, "#EEEEEE");
        sRehabilitado.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1, "#EEEEEE");

        WorksheetStyle sUsado = book.Styles.Add("usado");
        sUsado.Font.Color = "#000000";
        style.Alignment.WrapText = true;

        WorksheetStyle sResumenN = book.Styles.Add("resumenNum");
        sResumenN.Font.Color = "#000000";
        
        sResumenN.Alignment.Horizontal = StyleHorizontalAlignment.Right;

        WorksheetStyle sTotal = book.Styles.Add("resumen");
        sTotal.Interior.Color = "#A8A8A8";
        sTotal.Alignment.Horizontal = StyleHorizontalAlignment.Center;
        sTotal.Interior.Pattern = StyleInteriorPattern.Solid;
        sTotal.Font.Bold = true;
        sTotal.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1, "#EEEEEE");
        sTotal.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1, "#EEEEEE");
        sTotal.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1, "#EEEEEE");
        sTotal.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1, "#EEEEEE");

        WorksheetStyle sTotalN = book.Styles.Add("resumenN");
        sTotalN.Interior.Color = "#A8A8A8";
        sTotalN.Font.Bold = true;
        sTotalN.Alignment.Horizontal = StyleHorizontalAlignment.Right;
        sTotalN.Interior.Pattern = StyleInteriorPattern.Solid;
        sTotalN.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1, "#EEEEEE");
        sTotalN.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1, "#EEEEEE");
        sTotalN.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1, "#EEEEEE");
        sTotalN.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1, "#EEEEEE");
    
        /*************************** FIN ESTILOS ****************************/

        sheet.Table.Columns.Add(new WorksheetColumn(70));
        sheet.Table.Columns.Add(new WorksheetColumn(220));
        sheet.Table.Columns.Add(new WorksheetColumn(70));
        sheet.Table.Columns.Add(new WorksheetColumn(125));
        sheet.Table.Columns.Add(new WorksheetColumn(60));
        sheet.Table.Columns.Add(new WorksheetColumn(140));

        WorksheetRow row = sheet.Table.Rows.Add();
        row.Index = 0;
        row.Height = 27;
        row.AutoFitHeight = false;

        WorksheetCell wcHeader;
        wcHeader = new WorksheetCell("Fecha Venta", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Aerolínea", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Nro. Vuelo", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Tipo Ticket", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Cantidad", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Representante", "HeaderStyle"); row.Cells.Add(wcHeader);

        int iCant = 0;
        string estilo = "usado";
        foreach (DataRow dr in dt_consulta.Rows)
        {
            row.AutoFitHeight = true;
            WorksheetRow rowD = sheet.Table.Rows.Add();

            rowD.Cells.Add(new WorksheetCell(Fecha.convertSQLToFecha(dt_consulta.Rows[iCant]["Fecha_Venta"].ToString(),null), DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Dsc_Compania"].ToString(), DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Dsc_Num_Vuelo"].ToString(), DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Dsc_Tipo_Ticket"].ToString(), DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Can_Venta"].ToString(), DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Dsc_Repte"].ToString(), DataType.String, estilo));

            iCant++;
        }

        //RESUMEN DEL REPORTE
        Worksheet sheetResumen = book.Worksheets.Add("Resumen");

        sheetResumen.Table.Columns.Add(new WorksheetColumn(235));
        sheetResumen.Table.Columns.Add(new WorksheetColumn(80));
        sheetResumen.Table.Columns.Add(new WorksheetColumn(95));

        //Add row with some properties            
        row = sheetResumen.Table.Rows.Add();
        row.Index = 0;
        row.Height = 23;
        row.AutoFitHeight = false;

        //Add header text for the columns	            
        WorksheetCell
        wcHeaderR = new WorksheetCell("Aerolínea", "HeaderStyle"); row.Cells.Add(wcHeaderR);
        wcHeaderR = new WorksheetCell("Cantidad", "HeaderStyle"); row.Cells.Add(wcHeaderR);
        wcHeaderR = new WorksheetCell("Importe Vendido", "HeaderStyle"); row.Cells.Add(wcHeaderR);

        dt_resumen = objReporte.ListarResumenTicketVendidosCreditoPagin(ls_Fecha_Inicial, 
                                                                        ls_Fecha_Final, 
                                                                        ls_CodTipoTicket,
                                                                        ls_NumVuelo, 
                                                                        ls_CodAerolinea, 
                                                                        ls_CodPago, 
                                                                        "1", null, 0, 0, "2");    
        
        iCant = 0;
        double monto = 0;
        int cantidad = 0;
        estilo = "usado";
        foreach (DataRow dr in dt_resumen.Rows)
        {
            WorksheetRow rowD = sheetResumen.Table.Rows.Add();
            rowD.Cells.Add(new WorksheetCell(dt_resumen.Rows[iCant]["Dsc_Compania"].ToString(), DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_resumen.Rows[iCant]["Cantidad"].ToString(), DataType.String, "resumenNum"));
            rowD.Cells.Add(new WorksheetCell(dt_resumen.Rows[iCant]["Monto"].ToString(), DataType.String, "resumenNum"));

            cantidad += (int)dt_resumen.Rows[iCant]["Cantidad"];
            monto += Convert.ToDouble(dt_resumen.Rows[iCant]["Monto"]);
            iCant++;
        }

        WorksheetRow rowT = sheetResumen.Table.Rows.Add();
        rowT.Cells.Add(new WorksheetCell("TOTAL","resumen"));
        rowT.Cells.Add(new WorksheetCell(cantidad.ToString(), "resumenN"));
        rowT.Cells.Add(new WorksheetCell("$" + monto.ToString(), "resumenN"));
    
        
        Response.AppendHeader("content-disposition", "filename=TicketsVendidosCredito.xls");

        book.Save(Response.OutputStream);
%>
