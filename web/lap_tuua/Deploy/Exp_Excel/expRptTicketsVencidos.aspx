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
        string FechaDesde = Fecha.convertToFechaSQL2(Request.QueryString["sFechaDesde"]);
        string FechaHasta = Fecha.convertToFechaSQL2(Request.QueryString["sFechaHasta"]);
        string sTipoTicket = Request.QueryString["sTipoTicket"];

        dt_consulta = objReporte.ListarTicketVencidosPagin(FechaDesde, FechaHasta, sTipoTicket, null, 0, 0, "0");
    
        Workbook book = new Workbook();
        Worksheet sheet = book.Worksheets.Add("Reporte Tickets Vencidos");

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

        sheet.Table.Columns.Add(new WorksheetColumn(120));
        sheet.Table.Columns.Add(new WorksheetColumn(160));
        sheet.Table.Columns.Add(new WorksheetColumn(220));
        sheet.Table.Columns.Add(new WorksheetColumn(110));
        sheet.Table.Columns.Add(new WorksheetColumn(80));

        WorksheetRow row = sheet.Table.Rows.Add();
        row.Index = 0;
        row.Height = 27;
        row.AutoFitHeight = false;

        WorksheetCell wcHeader;
        wcHeader = new WorksheetCell("Nro. Ticket", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Tipo Ticket", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Compañía", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Fecha Vencimiento", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Dias Vencido", "HeaderStyle"); row.Cells.Add(wcHeader);

        int iCant = 0;
        string estilo = "usado";
        foreach (DataRow dr in dt_consulta.Rows)
        {
            row.AutoFitHeight = true;
            WorksheetRow rowD = sheet.Table.Rows.Add();

            rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Cod_Numero_Ticket"].ToString(), DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["TipoTicket"].ToString(), DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Dsc_Compania"].ToString(), DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Fch_Vencimiento"].ToString(), DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["DiasVencidos"].ToString(), DataType.String, estilo));

            iCant++;
        }

        Response.AppendHeader("content-disposition", "filename=TicktesVencidos.xls");

        book.Save(Response.OutputStream);
%>
