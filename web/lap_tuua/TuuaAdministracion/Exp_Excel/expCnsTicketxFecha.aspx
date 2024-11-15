<%@ Page Language="C#" ContentType="text/xml" %> 
<%@ Import Namespace="CarlosAg.ExcelXmlWriter"  %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="LAP.TUUA.CONTROL" %>
<%@ Import Namespace="LAP.TUUA.UTIL" %>

<%
        BO_Consultas objListarTicketxFecha = new BO_Consultas();
        DataTable dt_consulta = new DataTable();

        //Set Paramaters values
        string sDesde = Fecha.convertToFechaSQL2(Request.QueryString["sDesde"]);
        string sHasta = Fecha.convertToFechaSQL2(Request.QueryString["sHasta"]);
        string idHoraDesde = Fecha.convertToHoraSQL(Request.QueryString["idHoraDesde"]);
        string idHoraHasta = Fecha.convertToHoraSQL(Request.QueryString["idHoraHasta"]);
        string idCompania = Request.QueryString["idCompania"];
        string idEstadoTicket = Request.QueryString["idEstadoTicket"];
        string idPersona = Request.QueryString["idPersona"];
        string idVuelo = Request.QueryString["idVuelo"];
        string idTipoTicket = Request.QueryString["idTipoTicket"];
        string idFlgCobro = Request.QueryString["idFlgCobro"];
        string idTipoDocumento = Request.QueryString["idTipoDoc"];
        string idMasiva = Request.QueryString["idChkMasiva"];
        string idEstadoTurno = Request.QueryString["idEstadoTurno"];
        string idCajero = Request.QueryString["idCajero"];
        string idMedioAnulacion = Request.QueryString["idMedioAnulacion"];

        dt_consulta = objListarTicketxFecha.ListarTicketxFechaPagin(idTipoDocumento,
                                                    sDesde,
                                                    sHasta,
                                                    idHoraDesde,
                                                    idHoraHasta,
                                                    idCompania,
                                                    idEstadoTicket,
                                                    idTipoTicket,
                                                    idPersona,
                                                    idVuelo,
                                                    null,
                                                    null,
                                                    idFlgCobro,
                                                    idMasiva,
                                                    idEstadoTurno,
                                                    idCajero,
                                                    idMedioAnulacion,
                                                    null,
                                                    0,
                                                    0,
                                                    "0");
    
    
        Workbook book = new Workbook();
        Worksheet sheet = book.Worksheets.Add("Consulta Tickets por Fecha");
        
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

        if (idTipoDocumento == "T")
        {
            sheet.Table.Columns.Add(new WorksheetColumn(98));
            sheet.Table.Columns.Add(new WorksheetColumn(80));
            sheet.Table.Columns.Add(new WorksheetColumn(165));
            sheet.Table.Columns.Add(new WorksheetColumn(205));
            sheet.Table.Columns.Add(new WorksheetColumn(100));
            sheet.Table.Columns.Add(new WorksheetColumn(70));
            sheet.Table.Columns.Add(new WorksheetColumn(70));
            sheet.Table.Columns.Add(new WorksheetColumn(100));
            sheet.Table.Columns.Add(new WorksheetColumn(100));
            sheet.Table.Columns.Add(new WorksheetColumn(100));
            sheet.Table.Columns.Add(new WorksheetColumn(90));
            sheet.Table.Columns.Add(new WorksheetColumn(70));

            WorksheetRow row = sheet.Table.Rows.Add();
            row.Index = 0;
            row.Height = 27;
            row.AutoFitHeight = false;

            WorksheetCell wcHeader;
            wcHeader = new WorksheetCell("Nro. Ticket", "HeaderStyle"); row.Cells.Add(wcHeader);
            wcHeader = new WorksheetCell("Secuencial", "HeaderStyle"); row.Cells.Add(wcHeader);
            wcHeader = new WorksheetCell("Tipo Ticket", "HeaderStyle"); row.Cells.Add(wcHeader);
            wcHeader = new WorksheetCell("Compañia", "HeaderStyle"); row.Cells.Add(wcHeader);
            wcHeader = new WorksheetCell("Fch. Creación", "HeaderStyle"); row.Cells.Add(wcHeader);
            wcHeader = new WorksheetCell("Fch. Vuelo", "HeaderStyle"); row.Cells.Add(wcHeader);
            wcHeader = new WorksheetCell("Nro. Vuelo", "HeaderStyle"); row.Cells.Add(wcHeader);
            wcHeader = new WorksheetCell("Fch. Emisión", "HeaderStyle"); row.Cells.Add(wcHeader);
            wcHeader = new WorksheetCell("Fch. Uso", "HeaderStyle"); row.Cells.Add(wcHeader);
            wcHeader = new WorksheetCell("Fch. Rehabilitacion", "HeaderStyle"); row.Cells.Add(wcHeader);
            wcHeader = new WorksheetCell("Cajero Emisión", "HeaderStyle"); row.Cells.Add(wcHeader);
            wcHeader = new WorksheetCell("Estado Actual", "HeaderStyle"); row.Cells.Add(wcHeader);


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

                    rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Cod_Numero_Ticket"].ToString(), CarlosAg.ExcelXmlWriter.DataType.String, estilo));
                    rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Correlativo"].ToString(), CarlosAg.ExcelXmlWriter.DataType.String, estilo));
                    rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Dsc_Tipo_Ticket"].ToString(), CarlosAg.ExcelXmlWriter.DataType.String, estilo));
                    rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Dsc_Compania"].ToString(), CarlosAg.ExcelXmlWriter.DataType.String, estilo));
                    rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Fch_Creacion"].ToString(), CarlosAg.ExcelXmlWriter.DataType.String, estilo));
                    rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Fch_Vuelo"].ToString(), CarlosAg.ExcelXmlWriter.DataType.String, estilo));
                    rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Dsc_Num_Vuelo"].ToString(), CarlosAg.ExcelXmlWriter.DataType.String, estilo));
                    rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["FHEmision"].ToString(), CarlosAg.ExcelXmlWriter.DataType.String, estilo));
                    rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["FHUso"].ToString(), CarlosAg.ExcelXmlWriter.DataType.String, estilo));
                    rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["FHReh"].ToString(), CarlosAg.ExcelXmlWriter.DataType.String, estilo));
                    rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Cta_Usuario"].ToString(), CarlosAg.ExcelXmlWriter.DataType.String, estilo));
                    rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Dsc_Campo"].ToString(), CarlosAg.ExcelXmlWriter.DataType.String, estilo));

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
            wcHeader = new WorksheetCell("Usuario Proceso", "HeaderStyle"); row.Cells.Add(wcHeader);
            wcHeader = new WorksheetCell("Fch. Creación", "HeaderStyle"); row.Cells.Add(wcHeader);
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

                rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Cod_Numero_Bcbp"].ToString(), CarlosAg.ExcelXmlWriter.DataType.String, estilo));
                rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Correlativo"].ToString(), CarlosAg.ExcelXmlWriter.DataType.String, estilo));
                rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Dsc_Compania"].ToString(), CarlosAg.ExcelXmlWriter.DataType.String, estilo));
                rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Fch_Vuelo"].ToString(), CarlosAg.ExcelXmlWriter.DataType.String, estilo));
                rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Num_Vuelo"].ToString(), CarlosAg.ExcelXmlWriter.DataType.String, estilo));
                rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Num_Asiento"].ToString(), CarlosAg.ExcelXmlWriter.DataType.String, estilo));
                rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Nom_Pasajero"].ToString(), CarlosAg.ExcelXmlWriter.DataType.String, estilo));
                rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Dsc_Tip_Ingreso"].ToString(), CarlosAg.ExcelXmlWriter.DataType.String, estilo));
                rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Cta_Usuario"].ToString(), CarlosAg.ExcelXmlWriter.DataType.String, estilo));
                rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Fch_Creacion"].ToString(), CarlosAg.ExcelXmlWriter.DataType.String, estilo));
                rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Dsc_Tip_Estado"].ToString(), CarlosAg.ExcelXmlWriter.DataType.String, estilo));
                rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Flg_Tipo_Bcbp"].ToString()=="1"? "Si" : "No", CarlosAg.ExcelXmlWriter.DataType.String, estilo));

                iCant++;
            }

        }
            
        Response.AppendHeader("content-disposition", "filename=TicketxFecha.xls");

        book.Save(Response.OutputStream);
%>
