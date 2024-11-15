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
        string sFechaDesde="",sFechaHasta="";
        if (Request.QueryString["sFechaDesde"] != "")
        {
            sFechaDesde = Fecha.convertToFechaSQL(Request.QueryString["sFechaDesde"]);
            sFechaHasta = Fecha.convertToFechaSQL(Request.QueryString["sFechaHasta"]);
        }
        string sMes = Request.QueryString["sMes"];
        string sAnnio = Request.QueryString["sAnnio"];
        string sTDocumento = Request.QueryString["sTDocumento"];
        string sIdCompania = Request.QueryString["sIdCompania"];
        string sDestino = Request.QueryString["sDestino"].ToUpper();
        string sTipoTicket = Request.QueryString["sTipoTicket"];
        string sNumVuelo = Request.QueryString["sNumVuelo"];
        string sTipReporte = Request.QueryString["sTipReporte"].ToString() == "" ? null : Request.QueryString["sTipReporte"].ToString();

        //dt_consulta = objReporte.ListarResumenCompaniaPagin(sFechaDesde, sFechaHasta, sHoraDesde, sHoraHasta, null, 0, 0, "0");
        dt_consulta = objReporte.ListarTicketBoardingUsadosDiaMesPagin(sFechaDesde,
                                                                         sFechaHasta, 
                                                                         sAnnio + sMes, 
                                                                         sTDocumento, 
                                                                         sIdCompania, 
                                                                         sNumVuelo, 
                                                                         sTipoTicket, 
                                                                         sDestino, 
                                                                         sTipReporte, 
                                                                         null, 0, 0, "0");
    
        Workbook book = new Workbook();
        Worksheet sheet = book.Worksheets.Add("Reporte BP Usados");

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
        style.Alignment.WrapText = true;

        
        /*************************** FIN ESTILOS ****************************/

        sheet.Table.Columns.Add(new WorksheetColumn(90));
        sheet.Table.Columns.Add(new WorksheetColumn(100));
        sheet.Table.Columns.Add(new WorksheetColumn(130));
        sheet.Table.Columns.Add(new WorksheetColumn(220));
        sheet.Table.Columns.Add(new WorksheetColumn(80));
        sheet.Table.Columns.Add(new WorksheetColumn(70));
        sheet.Table.Columns.Add(new WorksheetColumn(70));
        sheet.Table.Columns.Add(new WorksheetColumn(70));

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
        wcHeader = new WorksheetCell("", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Cantidad", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("", "HeaderStyle"); row.Cells.Add(wcHeader);

        row = sheet.Table.Rows.Add();
        row.Index = 0;
        row.Height = 27;
        row.AutoFitHeight = false;
    
        wcHeader = new WorksheetCell("Fecha Uso", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Tipo Documento", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Tipo Ticket", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Aerolínea", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Nro. Vuelo", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("BP", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Ticket", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Total", "HeaderStyle"); row.Cells.Add(wcHeader);

        int iCant = 0;
        string estilo = "usado";
        foreach (DataRow dr in dt_consulta.Rows)
        {
            row.AutoFitHeight = true;
            WorksheetRow rowD = sheet.Table.Rows.Add();

            rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Fecha_Uso"].ToString(), DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Tipo_Documento"].ToString(), DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Dsc_Tipo_Ticket"].ToString(), DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Dsc_Compania"].ToString(), DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Num_Vuelo"].ToString(), DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["BP"].ToString(), DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Ticket"].ToString(), DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Total"].ToString(), DataType.String, estilo));

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

        iCant = 0;
        estilo = "usado";


        dt_resumen = objReporte.ListarTicketBoardingUsadosDiaMesPagin(sFechaDesde, 
                                                                        sFechaHasta, 
                                                                        sAnnio + sMes,
                                                                        sTDocumento, 
                                                                        sIdCompania, 
                                                                        sNumVuelo, 
                                                                        sTipoTicket,
                                                                        sDestino, 
                                                                        sTipReporte, 
                                                                        null, 0, 0, "1");

        iCant = 0;
        estilo = "usado";
        foreach (DataRow dr in dt_resumen.Rows)
        {
            WorksheetRow rowD = sheetResumen.Table.Rows.Add();
            rowD.Cells.Add(new WorksheetCell("BP", DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_resumen.Rows[iCant]["TotBoarding"].ToString(), DataType.String, estilo));
            
            rowD = sheetResumen.Table.Rows.Add();
            rowD.Cells.Add(new WorksheetCell("Ticket", DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_resumen.Rows[iCant]["TotTicket"].ToString(), DataType.String, estilo));

            iCant++;
        }
        
        WorksheetRow rowT = sheetResumen.Table.Rows.Add();
        rowT.Cells.Add(new WorksheetCell("TOTAL", "resumen"));
        rowT.Cells.Add(new WorksheetCell(dt_resumen.Rows[0]["Total"].ToString(), "resumen"));
    
    
        Response.AppendHeader("content-disposition", "filename=TicketBPUsados.xls");

        book.Save(Response.OutputStream);
%>
