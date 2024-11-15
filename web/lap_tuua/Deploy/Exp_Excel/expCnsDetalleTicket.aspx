<%@ Page Language="C#" ContentType="text/xml" %> 
<%@ Import Namespace="CarlosAg.ExcelXmlWriter"  %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="LAP.TUUA.CONTROL" %>
<%@ Import Namespace="LAP.TUUA.UTIL" %>

<%
        BO_Consultas objConsultaDetalleTicket = new BO_Consultas();
        DataTable dt_consulta = new DataTable();

        string sTipoReporte, idNumTicket, idNumTicketDesde, idNumTicketHasta, idCompania, idNumVuelo, idFechaVuelo, idNumAsiento, idPasajero, idDscCompania;

        sTipoReporte = Request.QueryString[0].ToString();

        if (sTipoReporte == "1")
        {
            idNumTicket = Request.QueryString[1].ToString();
            dt_consulta = objConsultaDetalleTicket.ConsultaDetalleTicketPagin(idNumTicket, null, null, null, 0, 0, "0");
        }
        else
        {
            if (sTipoReporte == "2")
            {
                idNumTicketDesde = Request.QueryString[1].ToString();
                idNumTicketHasta = Request.QueryString[2].ToString();
                dt_consulta =  objConsultaDetalleTicket.ConsultaDetalleTicketPagin("", idNumTicketDesde, idNumTicketHasta, null, 0, 0, "0");
            }
            else
            {
                idCompania = Request.QueryString[1].ToString();
                idNumVuelo = Request.QueryString[2].ToString();
                idFechaVuelo = Request.QueryString[3].ToString();
                idNumAsiento = Request.QueryString[4].ToString();
                idPasajero = Request.QueryString[5].ToString();
                idDscCompania = Request.QueryString[6].ToString();
                dt_consulta = objConsultaDetalleTicket.DetalleBoardingPagin(idCompania, idNumVuelo,  Fecha.convertToFechaSQL(idFechaVuelo), idNumAsiento, idPasajero, null, null, null, null, 0, 0, "0");
            }
        }
        

        Workbook book = new Workbook();
        Worksheet sheet = book.Worksheets.Add("Consulta Detalle Ticket BP");

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

        if ((sTipoReporte == "1") || (sTipoReporte == "2"))
        {
            sheet.Table.Columns.Add(new WorksheetColumn(98));
            sheet.Table.Columns.Add(new WorksheetColumn(80));
            sheet.Table.Columns.Add(new WorksheetColumn(165));
            sheet.Table.Columns.Add(new WorksheetColumn(205));
            sheet.Table.Columns.Add(new WorksheetColumn(80));
            sheet.Table.Columns.Add(new WorksheetColumn(60));
            sheet.Table.Columns.Add(new WorksheetColumn(100));
            sheet.Table.Columns.Add(new WorksheetColumn(80));
            sheet.Table.Columns.Add(new WorksheetColumn(100));
            sheet.Table.Columns.Add(new WorksheetColumn(50));
            sheet.Table.Columns.Add(new WorksheetColumn(90));
            sheet.Table.Columns.Add(new WorksheetColumn(60));
            sheet.Table.Columns.Add(new WorksheetColumn(100));

            WorksheetRow row = sheet.Table.Rows.Add();
            row.Index = 0;
            row.Height = 27;
            row.AutoFitHeight = false;

            WorksheetCell wcHeader;
            wcHeader = new WorksheetCell("Nro. Ticket", "HeaderStyle"); row.Cells.Add(wcHeader);
            wcHeader = new WorksheetCell("Secuencial", "HeaderStyle"); row.Cells.Add(wcHeader);
            wcHeader = new WorksheetCell("Tipo Ticket", "HeaderStyle"); row.Cells.Add(wcHeader);
            wcHeader = new WorksheetCell("Compañia", "HeaderStyle"); row.Cells.Add(wcHeader);
            wcHeader = new WorksheetCell("Fecha Vuelo", "HeaderStyle"); row.Cells.Add(wcHeader);
            wcHeader = new WorksheetCell("Nro. Vuelo", "HeaderStyle"); row.Cells.Add(wcHeader);
            wcHeader = new WorksheetCell("Fecha Ultimo Proceso", "HeaderStyle"); row.Cells.Add(wcHeader);
            wcHeader = new WorksheetCell("Estado Actual", "HeaderStyle"); row.Cells.Add(wcHeader);
            wcHeader = new WorksheetCell("Fecha Vencimiento", "HeaderStyle"); row.Cells.Add(wcHeader);
            wcHeader = new WorksheetCell("Turno", "HeaderStyle"); row.Cells.Add(wcHeader);
            wcHeader = new WorksheetCell("Usuario Ultimo Proceso", "HeaderStyle"); row.Cells.Add(wcHeader);
            wcHeader = new WorksheetCell("Precio", "HeaderStyle"); row.Cells.Add(wcHeader);
            wcHeader = new WorksheetCell("Nro. Rehabilitaciones", "HeaderStyle"); row.Cells.Add(wcHeader);


            int iCant = 0;
            string estilo = "";
            foreach (DataRow dr in dt_consulta.Rows)
            {
                WorksheetRow rowD = sheet.Table.Rows.Add();
                if (dt_consulta.Rows[iCant]["Tip_Estado_Actual"].ToString() == "R")
                    estilo = "rehabilitado";
                else
                {
                    if (dt_consulta.Rows[iCant]["Tip_Estado_Actual"].ToString() == "X")
                        estilo = "anulado";
                    else
                        estilo = "usado";
                }
                        
                    rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Cod_Numero_Ticket"].ToString(), DataType.String,estilo));
                    rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Correlativo"].ToString(), DataType.String, estilo));
                    rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Dsc_Tipo_Ticket"].ToString(), DataType.String, estilo));
                    rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Dsc_Compania"].ToString(), DataType.String, estilo));
                    rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Fch_Vuelo"].ToString(), DataType.String, estilo));
                    rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Dsc_Num_Vuelo"].ToString(), DataType.String, estilo));
                    rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["FechaModificacion"].ToString(), DataType.String, estilo));
                    rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Dsc_Campo"].ToString(), DataType.String, estilo));
                    rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Fch_Vencimiento"].ToString(), DataType.String, estilo));
                    rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Cod_Turno"].ToString(), DataType.String, estilo));
                    rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Cta_Usuario"].ToString(), DataType.String, estilo));
                    rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Cod_Moneda"].ToString() + " (" + dt_consulta.Rows[iCant]["Imp_Precio"].ToString() + ")", DataType.String, estilo));
                    rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Num_Rehabilitaciones"].ToString(), DataType.String, estilo));

                iCant++;
            }
        }
        else
        {
            sheet.Table.Columns.Add(new WorksheetColumn(98));
            sheet.Table.Columns.Add(new WorksheetColumn(80));
            sheet.Table.Columns.Add(new WorksheetColumn(205));
            sheet.Table.Columns.Add(new WorksheetColumn(90));
            sheet.Table.Columns.Add(new WorksheetColumn(80));
            sheet.Table.Columns.Add(new WorksheetColumn(80));
            sheet.Table.Columns.Add(new WorksheetColumn(180));
            sheet.Table.Columns.Add(new WorksheetColumn(80));
            sheet.Table.Columns.Add(new WorksheetColumn(100));
            sheet.Table.Columns.Add(new WorksheetColumn(100));
            sheet.Table.Columns.Add(new WorksheetColumn(80));
            sheet.Table.Columns.Add(new WorksheetColumn(80));


            WorksheetRow row = sheet.Table.Rows.Add();
            row.Index = 0;
            row.Height = 27;
            row.AutoFitHeight = false;

            WorksheetCell wcHeader;
            wcHeader = new WorksheetCell("Nro. Boarding", "HeaderStyle"); row.Cells.Add(wcHeader);
            wcHeader = new WorksheetCell("Secuencial", "HeaderStyle"); row.Cells.Add(wcHeader);
            wcHeader = new WorksheetCell("Compañía", "HeaderStyle"); row.Cells.Add(wcHeader);
            wcHeader = new WorksheetCell("Fecha Vuelo", "HeaderStyle"); row.Cells.Add(wcHeader);
            wcHeader = new WorksheetCell("Nro. Vuelo", "HeaderStyle"); row.Cells.Add(wcHeader);
            wcHeader = new WorksheetCell("Nro. Asiento", "HeaderStyle"); row.Cells.Add(wcHeader);
            wcHeader = new WorksheetCell("Pasajero", "HeaderStyle"); row.Cells.Add(wcHeader);
            wcHeader = new WorksheetCell("Tipo Ingreso", "HeaderStyle"); row.Cells.Add(wcHeader);
            wcHeader = new WorksheetCell("Usuario Último Proceso", "HeaderStyle"); row.Cells.Add(wcHeader);
            wcHeader = new WorksheetCell("Fecha Último Proceso", "HeaderStyle"); row.Cells.Add(wcHeader);
            wcHeader = new WorksheetCell("Estado Actual", "HeaderStyle"); row.Cells.Add(wcHeader);
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
                
                rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Cod_Numero_Bcbp"].ToString(), DataType.String,estilo));
                rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Correlativo"].ToString(), DataType.String, estilo));
                rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Dsc_Compania"].ToString(), DataType.String, estilo));
                rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Fch_Vuelo"].ToString(), DataType.String, estilo));
                rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Num_Vuelo"].ToString(), DataType.String, estilo));
                rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Num_Asiento"].ToString(), DataType.String, estilo));
                rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Nom_Pasajero"].ToString(), DataType.String, estilo));
                rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Dsc_Tipo_Ingreso"].ToString(), DataType.String, estilo));
                rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Usuario"].ToString(), DataType.String, estilo));
                rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["FechaModificacion"].ToString(), DataType.String, estilo));
                rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Dsc_Campo"].ToString(), DataType.String, estilo));
                rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Flg_Tipo_Bcbp"].ToString(), DataType.String, estilo));

                iCant++;
            }

        }
            
        Response.AppendHeader("content-disposition", "filename=DetalleTicketBP.xls");

        book.Save(Response.OutputStream);
%>
