<%@ Page Language="C#" ContentType="text/xml" %> 
<%@ Import Namespace="CarlosAg.ExcelXmlWriter"  %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="LAP.TUUA.CONTROL" %>
<%@ Import Namespace="LAP.TUUA.UTIL" %>

<%
        BO_Reportes objListarTicketContingenciaxFecha = new BO_Reportes();
        DataTable dt_consulta = new DataTable();
        DataTable dt_resumen = new DataTable();

        //Set Paramaters values
        string sTipoTicket = Request.QueryString["codtpticket"];
        string sAerolinea = Request.QueryString["codaerolinea"];
        string sHoraDesde = Fecha.convertToHoraSQL(Request.QueryString["horainicial"]);
        string sHoraHasta = Fecha.convertToHoraSQL(Request.QueryString["horafinal"]);
        string sFechaDesde = Fecha.convertToFechaSQL2(Request.QueryString["fechainicial"]);
        string sFechaHasta = Fecha.convertToFechaSQL2(Request.QueryString["fechafinal"]);
        string sMotivo = Request.QueryString["codMotivo"];
        string sNumVuelo = Request.QueryString["numvuelo"];
        string sDocumento = Request.QueryString["documento"];

        dt_consulta = objListarTicketContingenciaxFecha.obtenerTicketBoardingRehabilitados(sFechaDesde, 
                                                                                            sFechaHasta, 
                                                                                            sHoraDesde, 
                                                                                            sHoraHasta, 
                                                                                            sDocumento, 
                                                                                            sTipoTicket, 
                                                                                            sAerolinea, 
                                                                                            sNumVuelo, 
                                                                                            sMotivo);
    
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
        sheet.Table.Columns.Add(new WorksheetColumn(80));
        sheet.Table.Columns.Add(new WorksheetColumn(70));
        sheet.Table.Columns.Add(new WorksheetColumn(220));
        sheet.Table.Columns.Add(new WorksheetColumn(80));
        sheet.Table.Columns.Add(new WorksheetColumn(210));
        sheet.Table.Columns.Add(new WorksheetColumn(80));
        sheet.Table.Columns.Add(new WorksheetColumn(110));
        sheet.Table.Columns.Add(new WorksheetColumn(85));

        WorksheetRow row = sheet.Table.Rows.Add();
        row.Index = 0;
        row.Height = 27;
        row.AutoFitHeight = false;

        WorksheetCell wcHeader;
        wcHeader = new WorksheetCell("Fecha Venta", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Fecha Rehab", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Hora Rehab", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Compañía", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Nro Vuelo", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Motivo", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Documento", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Nro. Documento", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Secuencial", "HeaderStyle"); row.Cells.Add(wcHeader);

        int iCant = 0;
        string estilo = "usado";
        foreach (DataRow dr in dt_consulta.Rows)
        {
            row.AutoFitHeight = true;
            WorksheetRow rowD = sheet.Table.Rows.Add();

            rowD.Cells.Add(new WorksheetCell(Fecha.convertSQLToFecha(dt_consulta.Rows[iCant]["FechaVenta"].ToString(),null), CarlosAg.ExcelXmlWriter.DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(Fecha.convertSQLToFecha(dt_consulta.Rows[iCant]["FechaRehab"].ToString(),null), CarlosAg.ExcelXmlWriter.DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(Fecha.convertSQLToHora(dt_consulta.Rows[iCant]["HoraRehab"].ToString()), CarlosAg.ExcelXmlWriter.DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Dsc_Compania"].ToString(), CarlosAg.ExcelXmlWriter.DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["NumVuelo"].ToString(), CarlosAg.ExcelXmlWriter.DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["DesMotivo"].ToString(), CarlosAg.ExcelXmlWriter.DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["DesDocument"].ToString(), CarlosAg.ExcelXmlWriter.DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Cod_Numero_Ticket"].ToString(), CarlosAg.ExcelXmlWriter.DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Secuencial"].ToString(), CarlosAg.ExcelXmlWriter.DataType.String, estilo));

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
    
        DataRow[] dtTicket = dt_consulta.Select("DesDocument = 'TICKET'");
        int iCantTicket = dtTicket.Length;
        DataRow[] dtBoarding = dt_consulta.Select("DesDocument = 'BOARDING'");
        int iCantBoarding = dtBoarding.Length;

            WorksheetRow rowR = sheetResumen.Table.Rows.Add();
            rowR.Cells.Add(new WorksheetCell("TICKET", CarlosAg.ExcelXmlWriter.DataType.String, estilo));
            rowR.Cells.Add(new WorksheetCell(iCantTicket.ToString(), CarlosAg.ExcelXmlWriter.DataType.String, estilo));

            rowR = sheetResumen.Table.Rows.Add();
            rowR.Cells.Add(new WorksheetCell("BOARDING", CarlosAg.ExcelXmlWriter.DataType.String, estilo));
            rowR.Cells.Add(new WorksheetCell(iCantBoarding.ToString(), CarlosAg.ExcelXmlWriter.DataType.String, estilo));

        WorksheetRow rowT = sheetResumen.Table.Rows.Add();
        rowT.Cells.Add(new WorksheetCell("TOTAL", "resumen"));
        rowT.Cells.Add(new WorksheetCell((iCantTicket + iCantBoarding).ToString(), "resumen"));

        Response.AppendHeader("content-disposition", "filename=TickeBPRehabilitados.xls");

        book.Save(Response.OutputStream);
%>
