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
        string sFechaDesde = Fecha.convertToFechaSQL2(Request.QueryString["sFechaDesde"]);
        string sFechaHasta = Fecha.convertToFechaSQL2(Request.QueryString["sFechaHasta"]);
        string sHoraDesde = Request.QueryString["sHoraDesde"];
        if (sHoraDesde != null && sHoraDesde.Length == 5 && sHoraDesde != "__:__")
        {
            sHoraDesde = sHoraDesde.Substring(0, 2) + sHoraDesde.Substring(3, 2) + "00";
        }
        string sHoraHasta = Request.QueryString["sHoraHasta"];
        if (sHoraHasta != null && sHoraHasta.Length == 5 && sHoraHasta != "__:__")
        {
            sHoraHasta = sHoraHasta.Substring(0, 2) + sHoraHasta.Substring(3, 2) + "00";
        }
        string sTipoRango = Request.QueryString["sTipoRango"];
        string sAerolinea = Request.QueryString["sAerolinea"];
        string sTipoTicket = Request.QueryString["sTipoTicket"];
        string sNumVuelo = Request.QueryString["sNumVuelo"];
        string sTDocumento = Request.QueryString["sTDocumento"];
        string sDestino = Request.QueryString["sDestino"];


        dt_consulta = objReporte.consultarTicketBoardingUsados(sAerolinea,
                                                                            sNumVuelo,
                                                                            sTDocumento,
                                                                            sTipoTicket,
                                                                            sTipoRango,
                                                                            sFechaDesde,
                                                                            sFechaHasta,
                                                                            sHoraDesde,
                                                                            sHoraHasta,
                                                                            sDestino,
                                                                            null, 0, 0, "1", "0", "0");

    
        Workbook book = new Workbook();
        Worksheet sheet = book.Worksheets.Add("Reporte Tickets o BP Usados");
           
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

        sheet.Table.Columns.Add(new WorksheetColumn(70));
        sheet.Table.Columns.Add(new WorksheetColumn(70));
        sheet.Table.Columns.Add(new WorksheetColumn(135));
        sheet.Table.Columns.Add(new WorksheetColumn(245));
        sheet.Table.Columns.Add(new WorksheetColumn(70));
        sheet.Table.Columns.Add(new WorksheetColumn(70));
        sheet.Table.Columns.Add(new WorksheetColumn(70));
        sheet.Table.Columns.Add(new WorksheetColumn(70));
        sheet.Table.Columns.Add(new WorksheetColumn(70));
        sheet.Table.Columns.Add(new WorksheetColumn(70));

        WorksheetRow row = sheet.Table.Rows.Add();
        row.Index = 0;
        row.Height = 27;
        row.AutoFitHeight = false;

        WorksheetCell wcHeader;
        wcHeader = new WorksheetCell("Fecha Uso", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Tipo Documento", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Tipo Ticket", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Aerolínea", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Nº Vuelo", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Hora Inicio", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Hora Fin", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("BP", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Ticket", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Total", "HeaderStyle"); row.Cells.Add(wcHeader);


        int iCant = 0;
        string estilo = "usado";
        foreach (DataRow dr in dt_consulta.Rows)
        {
            row.AutoFitHeight = true;
            WorksheetRow rowD = sheet.Table.Rows.Add();
            rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Log_Fecha_Mod"].ToString(), CarlosAg.ExcelXmlWriter.DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Tipo_Documento"].ToString(), CarlosAg.ExcelXmlWriter.DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Dsc_Tipo_Ticket"].ToString(), CarlosAg.ExcelXmlWriter.DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Dsc_Compania"].ToString(), CarlosAg.ExcelXmlWriter.DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Num_Vuelo"].ToString(), CarlosAg.ExcelXmlWriter.DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["HoraInicio"].ToString(), CarlosAg.ExcelXmlWriter.DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["HoraFin"].ToString(), CarlosAg.ExcelXmlWriter.DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Total_BCBP"].ToString(), CarlosAg.ExcelXmlWriter.DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Total_Ticket"].ToString(), CarlosAg.ExcelXmlWriter.DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell((Convert.ToInt32(dt_consulta.Rows[iCant]["Total_BCBP"].ToString()) + Convert.ToInt32(dt_consulta.Rows[iCant]["Total_Ticket"].ToString())).ToString(), CarlosAg.ExcelXmlWriter.DataType.String, estilo));

            iCant++;
        }

        //RESUMEN DEL REPORTE
        Worksheet sheetResumen = book.Worksheets.Add("Resumen");

        sheetResumen.Table.Columns.Add(new WorksheetColumn(100));
        sheetResumen.Table.Columns.Add(new WorksheetColumn(100));

        //Add row with some properties            
        row = sheetResumen.Table.Rows.Add();
        row.Index = 0;
        row.Height = 23;
        row.AutoFitHeight = false;

        //Add header text for the columns	            
        WorksheetCell
        wcHeaderR = new WorksheetCell("Tipo Documento", "HeaderStyle"); row.Cells.Add(wcHeaderR);
        wcHeaderR = new WorksheetCell("Cantidad", "HeaderStyle"); row.Cells.Add(wcHeaderR);

        dt_resumen = objReporte.consultarTicketBoardingUsados(sAerolinea,
                                                                            sNumVuelo,
                                                                            sTDocumento,
                                                                            sTipoTicket,
                                                                            sTipoRango,
                                                                            sFechaDesde,
                                                                            sFechaHasta,
                                                                            sHoraDesde,
                                                                            sHoraHasta,
                                                                            sDestino,
                                                                            null, 0, 0, "0", "1", "0");
        
        iCant = 0;
        int total = 0;
        estilo = "usado";
        foreach (DataRow dr in dt_resumen.Rows)
        {
            WorksheetRow rowD = sheetResumen.Table.Rows.Add();
            rowD.Cells.Add(new WorksheetCell(dt_resumen.Rows[iCant]["Tipo_Documento"].ToString(), CarlosAg.ExcelXmlWriter.DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_resumen.Rows[iCant]["Total"].ToString(), CarlosAg.ExcelXmlWriter.DataType.String, estilo));

            total += (int)dt_resumen.Rows[iCant]["Total"];
            iCant++;
        }

        if (sTDocumento == "T")
        {
            WorksheetRow rowD = sheetResumen.Table.Rows.Add();
            rowD.Cells.Add(new WorksheetCell("BP", CarlosAg.ExcelXmlWriter.DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell("0", CarlosAg.ExcelXmlWriter.DataType.String, estilo));
        }
        else
        {
            if (sTDocumento == "B")
            {
                WorksheetRow rowD = sheetResumen.Table.Rows.Add();
                rowD.Cells.Add(new WorksheetCell("Ticket", CarlosAg.ExcelXmlWriter.DataType.String, estilo));
                rowD.Cells.Add(new WorksheetCell("0", CarlosAg.ExcelXmlWriter.DataType.String, estilo));
            }   
        }
    
        WorksheetRow rowT = sheetResumen.Table.Rows.Add();
        rowT.Cells.Add(new WorksheetCell("TOTAL", "resumen"));
        rowT.Cells.Add(new WorksheetCell(total.ToString(), "resumen"));
        
        
        Response.AppendHeader("content-disposition", "filename=TicketBBUsados.xls");

        book.Save(Response.OutputStream);
%>
