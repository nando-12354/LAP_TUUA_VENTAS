<%@ Page Language="C#" ContentType="text/xml" %> 
<%@ Import Namespace="CarlosAg.ExcelXmlWriter"  %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="LAP.TUUA.CONTROL" %>
<%@ Import Namespace="LAP.TUUA.UTIL" %>

<%
        BO_Consultas objConsultaTurnoxFiltro = new BO_Consultas();
        DataTable dt_consulta = new DataTable();

        string sFechaDesde, sFechaHasta, sHoraDesde, sHoraHasta, sIdUsuario, sIdPtoVta, sIdDscU;

        sFechaDesde = Fecha.convertToFechaSQL(Request.QueryString[0].ToString());
        sFechaHasta = Fecha.convertToFechaSQL(Request.QueryString[1].ToString());
        sIdUsuario = Request.QueryString[2].ToString();
        sIdPtoVta = Request.QueryString[3].ToString();
        sHoraDesde = Fecha.convertToHoraSQL(Request.QueryString[4].ToString());
        sHoraHasta = Fecha.convertToHoraSQL(Request.QueryString[5].ToString());
        sIdDscU = Request.QueryString[6].ToString();

        dt_consulta = objConsultaTurnoxFiltro.ConsultaTurnoxFiltro(sFechaDesde, sFechaHasta, sIdUsuario, sIdPtoVta, sHoraDesde, sHoraHasta, "0");

        Workbook book = new Workbook();
        Worksheet sheet = book.Worksheets.Add("Consulta Turnos");

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

        /*************************** FIN ESTILOS ****************************/


        sheet.Table.Columns.Add(new WorksheetColumn(60));
        sheet.Table.Columns.Add(new WorksheetColumn(160));
        sheet.Table.Columns.Add(new WorksheetColumn(120));
        sheet.Table.Columns.Add(new WorksheetColumn(120));
        sheet.Table.Columns.Add(new WorksheetColumn(120));
        sheet.Table.Columns.Add(new WorksheetColumn(80));
        sheet.Table.Columns.Add(new WorksheetColumn(80));
        sheet.Table.Columns.Add(new WorksheetColumn(80));
        sheet.Table.Columns.Add(new WorksheetColumn(80));


        WorksheetRow row = sheet.Table.Rows.Add();
        row.Index = 0;
        row.Height = 27;
        row.AutoFitHeight = false;

        WorksheetCell wcHeader;
        wcHeader = new WorksheetCell(""); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell(""); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell(""); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell(""); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell(""); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Moneda Inicio", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Moneda Fin", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("", "HeaderStyle"); row.Cells.Add(wcHeader);

        row = sheet.Table.Rows.Add();
        row.Index = 0;
        row.Height = 23;
        row.AutoFitHeight = false;

        wcHeader = new WorksheetCell("Turno", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Usuario", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Nro. Caja", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Fecha Apertura", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Fecha Cierre", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("SOL", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("DOL", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("SOL", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("DOL", "HeaderStyle"); row.Cells.Add(wcHeader);


        int iCant = 0;
        foreach (DataRow dr in dt_consulta.Rows)
        {
            WorksheetRow rowD = sheet.Table.Rows.Add();
            rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Cod_Turno"].ToString(), DataType.String));
            rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Dsc_Usuario"].ToString(), DataType.String));
            rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Dsc_Estacion"].ToString(), DataType.String));
            rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Fch_Inicio"].ToString(), DataType.String));
            rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Fch_Fin"].ToString(), DataType.String));
            rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["SOL_ImporteIni"].ToString(), DataType.String));
            rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["DOL_ImporteIni"].ToString(), DataType.String));
            rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["SOL_ImporteFin"].ToString(), DataType.String));
            rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["DOL_ImporteFin"].ToString(), DataType.String));

            iCant++;
        }

        Response.AppendHeader("content-disposition", "filename=Turnos.xls");

        book.Save(Response.OutputStream);
%>
