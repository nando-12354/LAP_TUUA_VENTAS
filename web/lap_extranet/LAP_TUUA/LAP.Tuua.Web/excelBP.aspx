<%@ Page Language="C#" ContentType="text/xml" %> 
<%@ Import Namespace="CarlosAg.ExcelXmlWriter"  %>
<%@ Import Namespace="System.Data" %>

<%
 
    DataTable dt_consulta = (DataTable)Session["tablaBP"];
    dt_consulta.Columns.Remove("Tip_Estado");
    
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

    WorksheetStyle sAnulado = book.Styles.Add("anulado");
    sAnulado.Interior.Color = "#FFCECE";
    sAnulado.Alignment.Horizontal = StyleHorizontalAlignment.Center;
    sAnulado.Interior.Pattern = StyleInteriorPattern.Solid;
    sAnulado.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1, "#EEEEEE");
    sAnulado.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1, "#EEEEEE");
    sAnulado.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1, "#EEEEEE");
    sAnulado.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1, "#EEEEEE");
    
    WorksheetStyle sRehabilitado = book.Styles.Add("rehabilitado");
    sRehabilitado.Interior.Color = "#FFE0C1";
    sRehabilitado.Alignment.Horizontal = StyleHorizontalAlignment.Center;
    sRehabilitado.Interior.Pattern = StyleInteriorPattern.Solid;
    sRehabilitado.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1, "#EEEEEE");
    sRehabilitado.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1, "#EEEEEE");
    sRehabilitado.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1, "#EEEEEE");
    sRehabilitado.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1, "#EEEEEE");

    WorksheetStyle sTotal = book.Styles.Add("resumen");
    sTotal.Interior.Color = "#A8A8A8";
    sTotal.Alignment.Horizontal = StyleHorizontalAlignment.Center;
    sTotal.Interior.Pattern = StyleInteriorPattern.Solid;
    sTotal.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1, "#EEEEEE");
    sTotal.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1, "#EEEEEE");
    sTotal.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1, "#EEEEEE");
    sTotal.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1, "#EEEEEE");
    

    WorksheetStyle sUsado = book.Styles.Add("usado");
    sUsado.Font.Color = "#000000";
    sUsado.Alignment.Horizontal = StyleHorizontalAlignment.Center;
    
    /*************************** FIN ESTILOS ****************************/
    

    sheet.Table.Columns.Add(new WorksheetColumn(80));
    sheet.Table.Columns.Add(new WorksheetColumn(60));
    sheet.Table.Columns.Add(new WorksheetColumn(70));
    sheet.Table.Columns.Add(new WorksheetColumn(70));
    sheet.Table.Columns.Add(new WorksheetColumn(120));
    sheet.Table.Columns.Add(new WorksheetColumn(110));
    sheet.Table.Columns.Add(new WorksheetColumn(80));
    sheet.Table.Columns.Add(new WorksheetColumn(105));
    sheet.Table.Columns.Add(new WorksheetColumn(105));
    sheet.Table.Columns.Add(new WorksheetColumn(60));
    sheet.Table.Columns.Add(new WorksheetColumn(60));
    sheet.Table.Columns.Add(new WorksheetColumn(170));
    sheet.Table.Columns.Add(new WorksheetColumn(80));

    //Add row with some properties            
    WorksheetRow row = sheet.Table.Rows.Add();
    row.Index = 0;
    row.Height = 23;
    row.AutoFitHeight = false;

    //Add header text for the columns	            
    WorksheetCell
    wcHeader = new WorksheetCell("Nro. Correlativo", "HeaderStyle"); row.Cells.Add(wcHeader);
    wcHeader = new WorksheetCell("Nro. Vuelo", "HeaderStyle"); row.Cells.Add(wcHeader);
    wcHeader = new WorksheetCell("Fech. Vuelo", "HeaderStyle"); row.Cells.Add(wcHeader);
    wcHeader = new WorksheetCell("Nro. Asiento", "HeaderStyle"); row.Cells.Add(wcHeader);
    wcHeader = new WorksheetCell("Molinete", "HeaderStyle"); row.Cells.Add(wcHeader);
    wcHeader = new WorksheetCell("Fech. Modificacion", "HeaderStyle"); row.Cells.Add(wcHeader);
    wcHeader = new WorksheetCell("Estado Actual", "HeaderStyle"); row.Cells.Add(wcHeader);
    wcHeader = new WorksheetCell("Fech. 1er Uso", "HeaderStyle"); row.Cells.Add(wcHeader);
    wcHeader = new WorksheetCell("Fech. Ultimo Uso", "HeaderStyle"); row.Cells.Add(wcHeader);
    wcHeader = new WorksheetCell("Nro. BP", "HeaderStyle"); row.Cells.Add(wcHeader);
    wcHeader = new WorksheetCell("Destino", "HeaderStyle"); row.Cells.Add(wcHeader);
    wcHeader = new WorksheetCell("Pasajero", "HeaderStyle"); row.Cells.Add(wcHeader);
    wcHeader = new WorksheetCell("eTicket", "HeaderStyle"); row.Cells.Add(wcHeader);

    int iCant = 0;
    int intCol = 0;
    foreach (DataRow dr in dt_consulta.Rows)
    {
        WorksheetRow rowD = sheet.Table.Rows.Add();
        intCol = 0;
        foreach (DataColumn dc in dt_consulta.Columns)
        {
            if (dt_consulta.Rows[iCant][6].ToString() == "REHABILITADO")
                rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant][intCol].ToString(), DataType.String, "rehabilitado"));
            else
            {
                if ((dt_consulta.Rows[iCant][6].ToString() == "VENCIDO") || (dt_consulta.Rows[iCant][intCol].ToString() == "ANULADO"))
                    rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant][intCol].ToString(), DataType.String, "anulado"));
                else
                    rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant][intCol].ToString(), DataType.String, "usado"));
            }
            
            intCol++;
        }
        iCant++;
    }

    Worksheet sheetResumen = book.Worksheets.Add("Resumen");

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
    wcHeaderR = new WorksheetCell("Fecha Lectura", "HeaderStyle"); row.Cells.Add(wcHeaderR);
    wcHeaderR = new WorksheetCell("Nro Vuelo", "HeaderStyle"); row.Cells.Add(wcHeaderR);
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
            rowD.Cells.Add(new WorksheetCell(dt_resumen.Rows[iCant][intCol].ToString(), DataType.String, "usado"));
            intCol++;
        }
        total += (int)dt_resumen.Rows[iCant][2];
        iCant++;
    }

    WorksheetRow rowT = sheetResumen.Table.Rows.Add();
    rowT.Cells.Add(new WorksheetCell(""));
    rowT.Cells.Add(new WorksheetCell("TOTAL", "resumen"));
    rowT.Cells.Add(new WorksheetCell(total.ToString(), "resumen"));

    Response.AppendHeader("content-disposition", "filename=BP-" + Request.QueryString[0] + "-" + Request.QueryString[1] + ".xls");
    
    book.Save(Response.OutputStream);
    
%>