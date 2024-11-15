<%@ Page Language="C#" ContentType="text/xml" %> 
<%@ Import Namespace="CarlosAg.ExcelXmlWriter"  %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="LAP.TUUA.CONTROL" %>
<%@ Import Namespace="LAP.TUUA.UTIL" %>

<%
    BO_Consultas objBOConsultas = new BO_Consultas();
        DataTable dt_consulta = new DataTable();

        //Set Paramaters values
        string FechaDesde = Fecha.convertToFechaSQL2(Request.QueryString["sDesde"]);
        string FechaHasta = Fecha.convertToFechaSQL2(Request.QueryString["sHasta"]);
        string HoraDesde = Fecha.convertToHoraSQL(Request.QueryString["idHoraDesde"]);
        string HoraHasta = Fecha.convertToHoraSQL(Request.QueryString["idHoraHasta"]);

        string idCompania = Request.QueryString["idCompania"];
        string idTipoVuelo = Request.QueryString["idTipoVuelo"];
        string idNumVuelo = Request.QueryString["idNumVuelo"];
        string idTipoPasajero = Request.QueryString["idTipoPasajero"];
        string idTipoDocumento = Request.QueryString["idTipoDocumento"];
        string idTipoTrasbordo = Request.QueryString["idTipoTrasbordo"];
        string idFechaVuelo = Request.QueryString["idFechaVuelo"];
        string sEstado = Request.QueryString["idEstado"];


        dt_consulta = objBOConsultas.obtenerTicketBoardingUsados(FechaDesde, 
                                                                 FechaHasta, 
                                                                 HoraDesde, 
                                                                 HoraHasta,
                                                                 idCompania, 
                                                                 idTipoVuelo, 
                                                                 idNumVuelo, 
                                                                 idTipoPasajero,
                                                                 idTipoDocumento, 
                                                                 idTipoTrasbordo, 
                                                                 Fecha.convertToFechaSQL2(idFechaVuelo), 
                                                                 sEstado);

    
        Workbook book = new Workbook();
        Worksheet sheet = book.Worksheets.Add("Consulta Boarding Usados");

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
    
        /*************************** FIN ESTILOS ****************************/

        sheet.Table.Columns.Add(new WorksheetColumn(98));
        sheet.Table.Columns.Add(new WorksheetColumn(80));
        sheet.Table.Columns.Add(new WorksheetColumn(90));
        sheet.Table.Columns.Add(new WorksheetColumn(110));
        sheet.Table.Columns.Add(new WorksheetColumn(205));
        sheet.Table.Columns.Add(new WorksheetColumn(70));
        sheet.Table.Columns.Add(new WorksheetColumn(70));
        sheet.Table.Columns.Add(new WorksheetColumn(100));
        sheet.Table.Columns.Add(new WorksheetColumn(70));
        sheet.Table.Columns.Add(new WorksheetColumn(100));
        sheet.Table.Columns.Add(new WorksheetColumn(70));
        sheet.Table.Columns.Add(new WorksheetColumn(80));
        sheet.Table.Columns.Add(new WorksheetColumn(80));
        sheet.Table.Columns.Add(new WorksheetColumn(90));
        sheet.Table.Columns.Add(new WorksheetColumn(70));

        WorksheetRow row = sheet.Table.Rows.Add();
        row.Index = 0;
        row.Height = 27;
        row.AutoFitHeight = false;

        WorksheetCell wcHeader;
        wcHeader = new WorksheetCell("Nro. Documento", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Secuencial", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Tipo Documento", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Modalidad Venta", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Aerolínea", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Nro. Vuelo", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Fecha Vuelo", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Fecha Uso", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Nro. Asiento", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Tipo Vuelo", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Tipo Persona", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Tipo Trasbordo", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Estado Actual", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Secuencia", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Asociado", "HeaderStyle"); row.Cells.Add(wcHeader);


        int iCant = 0;
        string estilo = "";
        foreach (DataRow dr in dt_consulta.Rows)
        {
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

            rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Codigo"].ToString(), DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Correlativo"].ToString(), DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Dsc_Documento"].ToString(), DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Nom_Modalidad"].ToString(), DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Dsc_Compania"].ToString(), DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Dsc_Num_Vuelo"].ToString(), DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Fch_Vuelo"].ToString(), DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(Fecha.convertSQLToFecha(dt_consulta.Rows[iCant]["Log_Fecha_Mod"].ToString(), dt_consulta.Rows[iCant]["Log_Hora_Mod"].ToString()), DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["NroAsiento"].ToString(), DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Dsc_TipoVuelo"].ToString(), DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Dsc_TipoPasajero"].ToString(), DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Dsc_Trasbordo"].ToString(), DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Dsc_EstadoActual"].ToString(), DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Num_Secuencial"].ToString(), DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Flg_Tipo_Bcbp"].ToString() == "1" ? "Si" : "No", estilo));

            iCant++;
        }
            
        Response.AppendHeader("content-disposition", "filename=BoardingUsados.xls");

        book.Save(Response.OutputStream);
%>
