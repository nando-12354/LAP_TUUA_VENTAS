<%@ Page Language="C#" ContentType="text/xml" %> 
<%@ Import Namespace="CarlosAg.ExcelXmlWriter"  %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="LAP.TUUA.CONTROL" %>
<%@ Import Namespace="LAP.TUUA.UTIL" %>

<%
        BO_Reportes objListarBPRehab = new BO_Reportes();
        DataTable dt_consulta = new DataTable();

        //Set Paramaters values
        string sFechaDesde = Fecha.convertToFechaSQL2(Request.QueryString["sFechaDesde"]);
        string sFechaHasta = Fecha.convertToFechaSQL2(Request.QueryString["sFechaHasta"]);
        string sHoraDesde = Fecha.convertToHoraSQL(Request.QueryString["sHoraDesde"]);
        string sHoraHasta = Fecha.convertToHoraSQL(Request.QueryString["sHoraHasta"]);

        string sCompania = Request.QueryString["sCompania"];
        string sMotivo = Request.QueryString["sMotivo"];
        string sTipoVuelo = Request.QueryString["sTipoVuelo"];
        string sTipoPersona = Request.QueryString["sTipoPersona"];
        string sNumVuelo = Request.QueryString["sNumVuelo"];

        dt_consulta = objListarBPRehab.obtenerBoardingRehabilitados(sFechaDesde,
                                                                        sFechaHasta,
                                                                        sHoraDesde,
                                                                        sHoraHasta,
                                                                        sCompania,
                                                                        sMotivo,
                                                                        sTipoVuelo,
                                                                        sTipoPersona,
                                                                        sNumVuelo,
                                                                        null, 0, 0, "1", "0", "0");
    
        Workbook book = new Workbook();
        Worksheet sheet = book.Worksheets.Add("Reporte BP Rehabilitados");

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

        sheet.Table.Columns.Add(new WorksheetColumn(110));
        sheet.Table.Columns.Add(new WorksheetColumn(200));
        sheet.Table.Columns.Add(new WorksheetColumn(70));
        sheet.Table.Columns.Add(new WorksheetColumn(220));
        sheet.Table.Columns.Add(new WorksheetColumn(80));
        sheet.Table.Columns.Add(new WorksheetColumn(70));
        sheet.Table.Columns.Add(new WorksheetColumn(110));
        sheet.Table.Columns.Add(new WorksheetColumn(200));
        sheet.Table.Columns.Add(new WorksheetColumn(100));
        sheet.Table.Columns.Add(new WorksheetColumn(100));
        sheet.Table.Columns.Add(new WorksheetColumn(90));

        WorksheetRow row = sheet.Table.Rows.Add();
        row.Index = 0;
        row.Height = 27;
        row.AutoFitHeight = false;

        WorksheetCell wcHeader;
        wcHeader = new WorksheetCell("Fecha Ultimo Uso", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Nombre Pasajero", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Nro. Asiento", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Compañía", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Fecha Vuelo", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Nro. Vuelo", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Fecha Rehab.", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Motivo", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Nro. SEAE", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Codigo Rehab.", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Tipo Vuelo", "HeaderStyle"); row.Cells.Add(wcHeader);

        int iCant = 0;
        string estilo = "usado";
        foreach (DataRow dr in dt_consulta.Rows)
        {
            row.AutoFitHeight = true;
            WorksheetRow rowD = sheet.Table.Rows.Add();

            rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Fch_Uso"].ToString(), DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Nom_Pasajero"].ToString(), DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Num_Asiento"].ToString(), DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Dsc_Compania"].ToString(), DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Fch_Vuelo"].ToString(), DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Num_Vuelo"].ToString(), DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Fch_Rehabilitacion"].ToString(), DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["DesMotivo"].ToString(), DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Cod_Numero_Bcbp"].ToString(), DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Num_Proceso_Rehab"].ToString(), DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["TipoVuelo"].ToString(), DataType.String, estilo));

            iCant++;
        }

        Response.AppendHeader("content-disposition", "filename=BPRehabilitados.xls");

        book.Save(Response.OutputStream);
%>
