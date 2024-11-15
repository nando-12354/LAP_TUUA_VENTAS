<%@ Page Language="C#" ContentType="text/xml" %> 
<%@ Import Namespace="CarlosAg.ExcelXmlWriter"  %>
<%@ Import Namespace="System.Data" %>
<%
 
    DataTable dt_consulta = (DataTable)Session["tablaBP"];
    
    DataTable dt_resumen = (DataTable)Session["tablaDetBoarding"];

    Workbook book = new Workbook();
    Worksheet sheet = book.Worksheets.Add("Listado Boarding Pass");

    /******************** ESTILOS ********************/
    WorksheetStyle style = book.Styles.Add("HeaderStyle");
    style.Font.FontName = "Verdana";
    style.Font.Size = 10;
    style.Font.Bold = true;
    style.Alignment.Horizontal = StyleHorizontalAlignment.Center;
    style.Alignment.Vertical = StyleVerticalAlignment.Center;
    style.Font.Color = "White";
    style.Interior.Color = "#003399";
    style.Interior.Pattern = StyleInteriorPattern.Solid;
    style.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1, "#EEEEEE");
    style.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1, "#EEEEEE");
    style.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1, "#EEEEEE");
    style.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1, "#EEEEEE");

    WorksheetStyle sTotal = book.Styles.Add("resumen");
    sTotal.Interior.Color = "#A8A8A8";
    sTotal.Alignment.Horizontal = StyleHorizontalAlignment.Center;
    sTotal.Interior.Pattern = StyleInteriorPattern.Solid;
    sTotal.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1, "#EEEEEE");
    sTotal.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1, "#EEEEEE");
    sTotal.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1, "#EEEEEE");
    sTotal.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1, "#EEEEEE");
    

    WorksheetStyle sUsado = book.Styles.Add("fila");
    sUsado.Font.Color = "#000000";
    sUsado.Alignment.Horizontal = StyleHorizontalAlignment.Center;
    
    /*************************** FIN ESTILOS ****************************/
    

    sheet.Table.Columns.Add(new WorksheetColumn(80));
    sheet.Table.Columns.Add(new WorksheetColumn(100));
    sheet.Table.Columns.Add(new WorksheetColumn(90));
    sheet.Table.Columns.Add(new WorksheetColumn(90));
    sheet.Table.Columns.Add(new WorksheetColumn(90));

    //Add row with some properties            
    WorksheetRow row = sheet.Table.Rows.Add();
    row.Index = 0;
    row.Height = 23;
    row.AutoFitHeight = false;

    //Add header text for the columns	            
    WorksheetCell
    wcHeader = new WorksheetCell("Fecha", "HeaderStyle"); row.Cells.Add(wcHeader);
    wcHeader = new WorksheetCell("Tipo Vuelo", "HeaderStyle"); row.Cells.Add(wcHeader);
    wcHeader = new WorksheetCell("Tipo Persona", "HeaderStyle"); row.Cells.Add(wcHeader);
    wcHeader = new WorksheetCell("Nro. Vuelo", "HeaderStyle"); row.Cells.Add(wcHeader);
    wcHeader = new WorksheetCell("Cantidad", "HeaderStyle"); row.Cells.Add(wcHeader);

    int iCant = 0;
    int intCol = 0;
    foreach (DataRow dr in dt_consulta.Rows)
    {
        WorksheetRow rowD = sheet.Table.Rows.Add();
        intCol = 0;
        foreach (DataColumn dc in dt_consulta.Columns)
        {
            if (dc.ColumnName == "Fch_Resumen")
                rowD.Cells.Add(new WorksheetCell(Fecha.convertSQLToFecha(dt_consulta.Rows[iCant][intCol].ToString(), null), DataType.String, "fila"));
            else
                rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant][intCol].ToString(), DataType.String, "fila"));
            
            intCol++;
        }
        iCant++;
    }

    Worksheet sheetResumen =  book.Worksheets.Add("Resumen");

    sheetResumen.Table.Columns.Add(new WorksheetColumn(80));
    sheetResumen.Table.Columns.Add(new WorksheetColumn(80));
    sheetResumen.Table.Columns.Add(new WorksheetColumn(80));
    
    //Add row with some properties            
    row = sheetResumen.Table.Rows.Add();
    row.Index = 0;
    row.Height = 23;
    row.AutoFitHeight = false;

    //Add header text for the columns	            
    WorksheetCell
    wcHeaderR = new WorksheetCell("Tipo Vuelo", "HeaderStyle"); row.Cells.Add(wcHeaderR);
    wcHeaderR = new WorksheetCell("Tipo Persona", "HeaderStyle"); row.Cells.Add(wcHeaderR);
    wcHeaderR = new WorksheetCell("Cantidad", "HeaderStyle"); row.Cells.Add(wcHeaderR);

    iCant = 0;
    intCol = 0;
    int total = 0;
    foreach (DataRow dr in dt_resumen.Rows)
    {
        WorksheetRow rowD = sheetResumen.Table.Rows.Add();
        intCol = 0;
        foreach (DataColumn dc in dt_resumen.Columns)
        {
            rowD.Cells.Add(new WorksheetCell(dt_resumen.Rows[iCant][intCol].ToString(), DataType.String, "fila"));
            intCol++;
        }
        total += (int)dt_resumen.Rows[iCant][2];
        iCant++;
    }

    WorksheetRow rowT = sheetResumen.Table.Rows.Add();
    rowT.Cells.Add(new WorksheetCell(""));
    rowT.Cells.Add(new WorksheetCell("TOTAL", "resumen"));
    rowT.Cells.Add(new WorksheetCell(total.ToString(), "resumen"));

    Response.AppendHeader("content-disposition", "filename=BP_MENSUAL-" + Request.QueryString[0] + "-" + Request.QueryString[1] + ".xls");
    
    book.Save(Response.OutputStream);
    
%>