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
        string sFechaDesde = Fecha.convertToFechaSQL2(Request.QueryString["sDesde"]);
        string sFechaHasta = Fecha.convertToFechaSQL2(Request.QueryString["sHasta"]);
        string sHoraDesde = Fecha.convertToHoraSQL(Request.QueryString["idHoraDesde"]);
        string sHoraHasta = Fecha.convertToHoraSQL(Request.QueryString["idHoraHasta"]);

        dt_consulta = objReporte.ListarResumenCompaniaPagin(sFechaDesde, sFechaHasta, sHoraDesde, sHoraHasta, null, 0, 0, "0");
    
        Workbook book = new Workbook();
        Worksheet sheet = book.Worksheets.Add("Reporte Resumen Compania");

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

        WorksheetStyle sUsado = book.Styles.Add("usado");
        sUsado.Font.Color = "#000000";
        style.Alignment.WrapText = true;

        
        /*************************** FIN ESTILOS ****************************/

        sheet.Table.Columns.Add(new WorksheetColumn(70));
        sheet.Table.Columns.Add(new WorksheetColumn(220));
        sheet.Table.Columns.Add(new WorksheetColumn(70));
        sheet.Table.Columns.Add(new WorksheetColumn(70));
        sheet.Table.Columns.Add(new WorksheetColumn(70));
        sheet.Table.Columns.Add(new WorksheetColumn(70));
        sheet.Table.Columns.Add(new WorksheetColumn(70));
        sheet.Table.Columns.Add(new WorksheetColumn(80));
        sheet.Table.Columns.Add(new WorksheetColumn(70));
        sheet.Table.Columns.Add(new WorksheetColumn(70));

        WorksheetRow row = sheet.Table.Rows.Add();
        row.Index = 0;
        row.Height = 27;
        row.AutoFitHeight = false;

        WorksheetCell wcHeader;
        wcHeader = new WorksheetCell("Fecha Venta", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Compañía", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Nro. Vuelo", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Tipo Documento", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Vendido", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Usado", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Emitido", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Rehabilitado", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Anulado", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Vencido", "HeaderStyle"); row.Cells.Add(wcHeader);

        int iCant = 0;
        string estilo = "usado";
        foreach (DataRow dr in dt_consulta.Rows)
        {
            row.AutoFitHeight = true;
            WorksheetRow rowD = sheet.Table.Rows.Add();

            rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Fecha_Venta"].ToString(), CarlosAg.ExcelXmlWriter.DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Dsc_Compania"].ToString(), CarlosAg.ExcelXmlWriter.DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Num_Vuelo"].ToString(), CarlosAg.ExcelXmlWriter.DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Tipo_Documento"].ToString(), CarlosAg.ExcelXmlWriter.DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Vendido"].ToString(), CarlosAg.ExcelXmlWriter.DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Usado"].ToString(), CarlosAg.ExcelXmlWriter.DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Emitido"].ToString(), CarlosAg.ExcelXmlWriter.DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Rehabilitado"].ToString(), CarlosAg.ExcelXmlWriter.DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Anulado"].ToString(), CarlosAg.ExcelXmlWriter.DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Vencido"].ToString(), CarlosAg.ExcelXmlWriter.DataType.String, estilo));

            iCant++;
        }

        //RESUMEN DEL REPORTE
        Worksheet sheetResumen = book.Worksheets.Add("Resumen");

        sheetResumen.Table.Columns.Add(new WorksheetColumn(100));
        sheetResumen.Table.Columns.Add(new WorksheetColumn(80));

        //Add row with some properties            
        row = sheetResumen.Table.Rows.Add();
        row.Index = 0;
        row.Height = 23;
        row.AutoFitHeight = false;

        //Add header text for the columns	            
        WorksheetCell
        wcHeaderR = new WorksheetCell("Tipo Documento", "HeaderStyle"); row.Cells.Add(wcHeaderR);
        wcHeaderR = new WorksheetCell("Cantidad", "HeaderStyle"); row.Cells.Add(wcHeaderR);

        dt_resumen = objReporte.ListarResumenCompaniaPagin(sFechaDesde, sFechaHasta, sHoraDesde, sHoraHasta, null, 0, 0, "2");   
        
        iCant = 0;
        estilo = "usado";
        foreach (DataRow dr in dt_resumen.Rows)
        {
            WorksheetRow rowD = sheetResumen.Table.Rows.Add();
            rowD.Cells.Add(new WorksheetCell(dt_resumen.Rows[iCant]["Dsc_Campo"].ToString(), CarlosAg.ExcelXmlWriter.DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_resumen.Rows[iCant]["Cantidad"].ToString(), CarlosAg.ExcelXmlWriter.DataType.String, estilo));

            iCant++;
        }

        Response.AppendHeader("content-disposition", "filename=ResumenCompania.xls");

        book.Save(Response.OutputStream);
%>
