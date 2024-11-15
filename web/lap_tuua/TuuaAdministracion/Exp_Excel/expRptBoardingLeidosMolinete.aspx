<%@ Page Language="C#" ContentType="text/xml" %> 
<%@ Import Namespace="CarlosAg.ExcelXmlWriter"  %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="LAP.TUUA.CONTROL" %>
<%@ Import Namespace="LAP.TUUA.UTIL" %>

<%
        BO_Reportes objReporte = new BO_Reportes();
        DataTable dt_consulta = new DataTable();
        DataTable dt_resumen = new DataTable();

        string idCompania, sFechaVuelo, sNumVuelo, sFechaLecturaIni, sFechaLecturaFin, idEstado, sNumBoarding;
    
        //Set Paramaters values
        idCompania = Request.QueryString["idCompania"];
        sFechaVuelo = Fecha.convertToFechaSQL2(Request.QueryString["sFechaVuelo"]);
        sNumVuelo = Request.QueryString["sNumVuelo"];
        sFechaLecturaIni = Fecha.convertToFechaSQL2(Request.QueryString["sFechaLecturaIni"]);
        sFechaLecturaFin = Fecha.convertToFechaSQL2(Request.QueryString["sFechaLecturaFin"]);
        idEstado = Request.QueryString["idEstado"];
        sNumBoarding = Request.QueryString["sNumBoarding"];

        dt_consulta = objReporte.ListarBoardingLeidosMolinetePagin(idCompania,
                                                                     sFechaVuelo,
                                                                     sNumVuelo,
                                                                     sFechaLecturaIni,
                                                                     sFechaLecturaFin,
                                                                     idEstado,
                                                                     sNumBoarding,
                                                                     "0", null, 0, 0, "0");

    
        Workbook book = new Workbook();
        Worksheet sheet = book.Worksheets.Add("Reporte BP Leidos Molinete");
        
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

        WorksheetStyle sTotal = book.Styles.Add("resumen");
        sTotal.Interior.Color = "#A8A8A8";
        sTotal.Alignment.Horizontal = StyleHorizontalAlignment.Center;
        sTotal.Interior.Pattern = StyleInteriorPattern.Solid;
        sTotal.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1, "#EEEEEE");
        sTotal.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1, "#EEEEEE");
        sTotal.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1, "#EEEEEE");
        sTotal.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1, "#EEEEEE");
    
        /*************************** FIN ESTILOS ****************************/

        sheet.Table.Columns.Add(new WorksheetColumn(60));
        sheet.Table.Columns.Add(new WorksheetColumn(95));
        sheet.Table.Columns.Add(new WorksheetColumn(150));
        sheet.Table.Columns.Add(new WorksheetColumn(60));
        sheet.Table.Columns.Add(new WorksheetColumn(70));
        sheet.Table.Columns.Add(new WorksheetColumn(60));
        sheet.Table.Columns.Add(new WorksheetColumn(130));
        sheet.Table.Columns.Add(new WorksheetColumn(170));
        sheet.Table.Columns.Add(new WorksheetColumn(100));
        sheet.Table.Columns.Add(new WorksheetColumn(170));
        sheet.Table.Columns.Add(new WorksheetColumn(85));

        WorksheetRow row = sheet.Table.Rows.Add();
        row.Index = 0;
        row.Height = 27;
        row.AutoFitHeight = false;

        WorksheetCell wcHeader;
        wcHeader = new WorksheetCell("Nro. Correlativo", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Nro. Boarding", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Aerolínea", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Nro. Vuelo", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Fecha Vuelo", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Nro. Asiento", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Nro. Molinete", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Fecha Uso", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Fecha Anulación", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Fecha Rehabilitación", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Estado", "HeaderStyle"); row.Cells.Add(wcHeader);


        int iCant = 0;
        string estilo = "";
        foreach (DataRow dr in dt_consulta.Rows)
        {
            row.AutoFitHeight = true;
            WorksheetRow rowD = sheet.Table.Rows.Add();
            if (dt_consulta.Rows[iCant]["Tip_Estado"].ToString() == "R")
                estilo = "rehabilitado";
            else
            {
                if (dt_consulta.Rows[iCant]["Tip_Estado"].ToString() == "X")
                    estilo = "anulado";
                else
                    estilo = "usado";
            }

            rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Num_Secuencial_Bcbp"].ToString(), CarlosAg.ExcelXmlWriter.DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Cod_Numero_Bcbp"].ToString(), CarlosAg.ExcelXmlWriter.DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Dsc_Compania"].ToString(), CarlosAg.ExcelXmlWriter.DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Num_Vuelo"].ToString(), CarlosAg.ExcelXmlWriter.DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Fch_Vuelo"].ToString(), CarlosAg.ExcelXmlWriter.DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Num_Asiento"].ToString(), CarlosAg.ExcelXmlWriter.DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["NroMolinete"].ToString(), CarlosAg.ExcelXmlWriter.DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["FechaUso"].ToString(), CarlosAg.ExcelXmlWriter.DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["FechaAnulacion"].ToString(), CarlosAg.ExcelXmlWriter.DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["FechaRehabilitacion"].ToString(), CarlosAg.ExcelXmlWriter.DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Dsc_Campo"].ToString(), CarlosAg.ExcelXmlWriter.DataType.String, estilo));

            iCant++;
        }

        //RESUMEN DEL REPORTE
        Worksheet sheetResumen = book.Worksheets.Add("Resumen");

        sheetResumen.Table.Columns.Add(new WorksheetColumn(100));
        sheetResumen.Table.Columns.Add(new WorksheetColumn(150));
        sheetResumen.Table.Columns.Add(new WorksheetColumn(70));
        sheetResumen.Table.Columns.Add(new WorksheetColumn(70));

        //Add row with some properties            
        row = sheetResumen.Table.Rows.Add();
        row.Index = 0;
        row.Height = 23;
        row.AutoFitHeight = false;

        //Add header text for the columns	            
        WorksheetCell
        wcHeaderR = new WorksheetCell("Fecha Lectura", "HeaderStyle"); row.Cells.Add(wcHeaderR);
        wcHeaderR = new WorksheetCell("Aerolínea", "HeaderStyle"); row.Cells.Add(wcHeaderR);
        wcHeaderR = new WorksheetCell("Nro Vuelo", "HeaderStyle"); row.Cells.Add(wcHeaderR);
        wcHeaderR = new WorksheetCell("Cantidad", "HeaderStyle"); row.Cells.Add(wcHeaderR);

        dt_resumen = objReporte.ListarBoardingLeidosMolinetePagin(idCompania, 
                                                                    sFechaVuelo, 
                                                                    sNumVuelo, 
                                                                    sFechaLecturaIni, 
                                                                    sFechaLecturaFin, 
                                                                    idEstado, 
                                                                    sNumBoarding, 
                                                                    "1", "", 0, 0, "0");
        
        iCant = 0;
        int total = 0;
        estilo = "usado";
        foreach (DataRow dr in dt_resumen.Rows)
        {
            WorksheetRow rowD = sheetResumen.Table.Rows.Add();
            rowD.Cells.Add(new WorksheetCell(dt_resumen.Rows[iCant]["Fecha_Uso_Interno"].ToString(), CarlosAg.ExcelXmlWriter.DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_resumen.Rows[iCant]["Dsc_Compania"].ToString(), CarlosAg.ExcelXmlWriter.DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_resumen.Rows[iCant]["Num_Vuelo"].ToString(), CarlosAg.ExcelXmlWriter.DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_resumen.Rows[iCant]["Cantidad"].ToString(), CarlosAg.ExcelXmlWriter.DataType.String, estilo));
            
            total += (int)dt_resumen.Rows[iCant]["Cantidad"];
            iCant++;
        }

        WorksheetRow rowT = sheetResumen.Table.Rows.Add();
        rowT.Cells.Add(new WorksheetCell(""));
        rowT.Cells.Add(new WorksheetCell(""));
        rowT.Cells.Add(new WorksheetCell("TOTAL", "resumen"));
        rowT.Cells.Add(new WorksheetCell(total.ToString(), "resumen"));
    
        
        Response.AppendHeader("content-disposition", "filename=BPMolinete.xls");

        book.Save(Response.OutputStream);
%>
