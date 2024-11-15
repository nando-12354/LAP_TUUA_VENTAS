<%@ Page Language="C#" ContentType="text/xml" %> 
<%@ Import Namespace="CarlosAg.ExcelXmlWriter"  %>
<%@ Import Namespace="System.Data" %>

<%@ Import Namespace="LAP.EXTRANET.UTIL" %>

<%
    LAP.Tuua.Web.WSDAOConsulta.WSConsultas objWS = new LAP.Tuua.Web.WSDAOConsulta.WSConsultas();
    
        DataTable dt_consulta_u = new DataTable();
        DataTable dt_consulta_r = new DataTable();
        DataTable dt_consulta_u_res = new DataTable();
        DataTable dt_consulta_r_res = new DataTable();

        //Set Paramaters values
        string sFechaDesde = Fecha.convertToFechaSQL2(Request.QueryString["sFechaDesde"]);
        string sFechaHasta = Fecha.convertToFechaSQL2(Request.QueryString["sFechaHasta"]);
        string sHoraDesde = Fecha.convertToHoraSQL(Request.QueryString["sHoraDesde"]);
        string sHoraHasta = Fecha.convertToHoraSQL(Request.QueryString["sHoraHasta"]);
        string sFechaVuelo = Fecha.convertToFechaSQL2(Request.QueryString["sFechaVuelo"]);

        string sNumVuelo = Request.QueryString["sNumVuelo"];
        string sNumAsiento = Request.QueryString["sNumAsiento"];
        string sNomPasajero = Request.QueryString["sNomPasajero"];

        string sTipoVuelo = Request.QueryString["sTipoVuelo"];
        string sTipoPasajero = Request.QueryString["sTipoPersona"];
        string sTipoTrasbordo = Request.QueryString["sTipoTrasbordo"];

        string sCodCompania = "";// Request.QueryString["sCompania"];
        //string sMotivo = Request.QueryString["sMotivo"];
        //string sTipoVuelo = Request.QueryString["sTipoVuelo"];
        //string sTipoPersona = Request.QueryString["sTipoPersona"];
        //string sNumVuelo = Request.QueryString["sNumVuelo"];

        //string sCodIata = Request.QueryString["sCodIata"];
        string sCodIata = (String)HttpContext.Current.Session["Iata"];
        string sPassword = Request.QueryString["sPassword"];

        dt_consulta_u = objWS.ListarBoardingDiario(sFechaDesde,
                                                sFechaHasta,
                                                sHoraDesde,
                                                sHoraHasta,
                                                sCodCompania,
                                                sTipoPasajero,
                                                sTipoVuelo,
                                                sTipoTrasbordo,
                                                sFechaVuelo,
                                                sNumVuelo,
                                                sNomPasajero,
                                                sNumAsiento,
                                                sCodIata,
                                                sPassword, "0", null, 0, 0, "0").Tables[0];

        dt_consulta_r = objWS.ListarBoardingDiario(sFechaDesde,
                                                sFechaHasta,
                                                sHoraDesde,
                                                sHoraHasta,
                                                sCodCompania,
                                                sTipoPasajero,
                                                sTipoVuelo,
                                                sTipoTrasbordo,
                                                sFechaVuelo,
                                                sNumVuelo,
                                                sNomPasajero,
                                                sNumAsiento,
                                                sCodIata,
                                                sPassword, "1", null, 0, 0, "0").Tables[0];

        dt_consulta_u_res = objWS.ListarBoardingDiario(sFechaDesde,
                                               sFechaHasta,
                                               sHoraDesde,
                                               sHoraHasta,
                                               sCodCompania,
                                               sTipoPasajero,
                                               sTipoVuelo,
                                               sTipoTrasbordo,
                                               sFechaVuelo,
                                               sNumVuelo,
                                               sNomPasajero,
                                               sNumAsiento,
                                               sCodIata,
                                               sPassword, "2", null, 0, 0, "0").Tables[0];

        dt_consulta_r_res = objWS.ListarBoardingDiario(sFechaDesde,
                                                sFechaHasta,
                                                sHoraDesde,
                                                sHoraHasta,
                                                sCodCompania,
                                                sTipoPasajero,
                                                sTipoVuelo,
                                                sTipoTrasbordo,
                                                sFechaVuelo,
                                                sNumVuelo,
                                                sNomPasajero,
                                                sNumAsiento,
                                                sCodIata,
                                                sPassword, "4", null, 0, 0, "0").Tables[0];    
            
    
        Workbook book = new Workbook();
        

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
        Worksheet sheet = book.Worksheets.Add("Reporte BP Diario USADO");
    
        sheet.Table.Columns.Add(new WorksheetColumn());
        sheet.Table.Columns.Add(new WorksheetColumn());
        sheet.Table.Columns.Add(new WorksheetColumn());
        sheet.Table.Columns.Add(new WorksheetColumn());
        sheet.Table.Columns.Add(new WorksheetColumn());
        sheet.Table.Columns.Add(new WorksheetColumn());
        sheet.Table.Columns.Add(new WorksheetColumn());
        sheet.Table.Columns.Add(new WorksheetColumn());
        sheet.Table.Columns.Add(new WorksheetColumn());
        sheet.Table.Columns.Add(new WorksheetColumn());
        sheet.Table.Columns.Add(new WorksheetColumn());
        sheet.Table.Columns.Add(new WorksheetColumn());
        sheet.Table.Columns.Add(new WorksheetColumn());
        sheet.Table.Columns.Add(new WorksheetColumn());
        sheet.Table.Columns.Add(new WorksheetColumn());

        WorksheetRow row = sheet.Table.Rows.Add();
        row.Index = 0;
        row.Height = 27;
        row.AutoFitHeight = false;

        WorksheetCell wcHeader;
        wcHeader = new WorksheetCell("Nro. Correlativo", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Secuencial", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Tipo Documento", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Aerolinea", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Pasajero", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Nro. Asiento", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Fecha Vuelo", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Nro. Vuelo", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Fecha Uso", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Tipo Vuelo", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Tipo Pasajero", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Tipo Trasbordo", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Estado Actual", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Nro. Boarding", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("ETicket", "HeaderStyle"); row.Cells.Add(wcHeader);

        int iCant = 0;
        string estilo = "usado";
        foreach (DataRow dr in dt_consulta_u.Rows)
        {
            row.AutoFitHeight = true;
            WorksheetRow rowD = sheet.Table.Rows.Add();

            rowD.Cells.Add(new WorksheetCell(dt_consulta_u.Rows[iCant]["Num_Secuencial_Bcbp"].ToString(), DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta_u.Rows[iCant]["Correlativo"].ToString(), DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta_u.Rows[iCant]["Tip_Documento"].ToString(), DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta_u.Rows[iCant]["Dsc_Compania"].ToString(), DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta_u.Rows[iCant]["Nom_Pasajero"].ToString(), DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta_u.Rows[iCant]["Num_Asiento"].ToString(), DataType.String, estilo));
			string strField = dt_consulta_u.Rows[iCant]["Fch_Vuelo"].ToString();
			strField = LAP.EXTRANET.UTIL.Fecha.convertSQLToFecha(strField,null);			
            rowD.Cells.Add(new WorksheetCell(strField, DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta_u.Rows[iCant]["Num_Vuelo"].ToString(), DataType.String, estilo));
			strField = dt_consulta_u.Rows[iCant]["Fch_Uso"].ToString();
			strField = LAP.EXTRANET.UTIL.Fecha.convertSQLToFecha2(strField);
            rowD.Cells.Add(new WorksheetCell(strField, DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta_u.Rows[iCant]["Dsc_Vuelo"].ToString(), DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta_u.Rows[iCant]["Tip_Pasajero"].ToString(), DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta_u.Rows[iCant]["Dsc_Trasbordo"].ToString(), DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta_u.Rows[iCant]["Dsc_Tip_Estado"].ToString(), DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta_u.Rows[iCant]["Nro_Boarding"].ToString(), DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta_u.Rows[iCant]["Cod_ETicket"].ToString(), DataType.String, estilo));
      
            iCant++;
        }

        Worksheet sheet1 = book.Worksheets.Add("BP Diario USADO RESUMEN");

        sheet1.Table.Columns.Add(new WorksheetColumn());
        sheet1.Table.Columns.Add(new WorksheetColumn());
        sheet1.Table.Columns.Add(new WorksheetColumn());
        sheet1.Table.Columns.Add(new WorksheetColumn());
        sheet1.Table.Columns.Add(new WorksheetColumn());
        sheet1.Table.Columns.Add(new WorksheetColumn());
        

        row = sheet1.Table.Rows.Add();
        row.Index = 0;
        row.Height = 27;
        row.AutoFitHeight = false;

        //WorksheetCell wcHeader;
		wcHeader = new WorksheetCell("Fecha Vuelo", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Nro. Vuelo", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Tipo Vuelo", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Tipo Pasajero", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Tipo Trasbordo", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Cantidad", "HeaderStyle"); row.Cells.Add(wcHeader);
       
        iCant = 0;
        estilo = "usado";
        foreach (DataRow dr in dt_consulta_u_res.Rows)
        {
            row.AutoFitHeight = true;
            WorksheetRow rowD = sheet1.Table.Rows.Add();

			/*string strField = dt_consulta_u_res.Rows[iCant]["Fch_Vuelo"].ToString();
			strField = LAP.EXTRANET.UTIL.Fecha.convertSQLToFecha(strField,null);			
			rowD.Cells.Add(new WorksheetCell(strField, DataType.String, estilo));\
            */
            rowD.Cells.Add(new WorksheetCell(dt_consulta_u_res.Rows[iCant]["Fch_Vuelo"].ToString(), DataType.String, estilo));    		    
            rowD.Cells.Add(new WorksheetCell(dt_consulta_u_res.Rows[iCant]["Num_Vuelo"].ToString(), DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta_u_res.Rows[iCant]["Dsc_Vuelo"].ToString(), DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta_u_res.Rows[iCant]["Tip_Pasajero"].ToString(), DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta_u_res.Rows[iCant]["Dsc_Trasbordo"].ToString(), DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta_u_res.Rows[iCant]["Cantidad"].ToString(), DataType.String, estilo));
            
            iCant++;
        }


        Worksheet sheet2 = book.Worksheets.Add("Reporte BP Diario REHAB");

        sheet2.Table.Columns.Add(new WorksheetColumn());
        sheet2.Table.Columns.Add(new WorksheetColumn());
        sheet2.Table.Columns.Add(new WorksheetColumn());
        sheet2.Table.Columns.Add(new WorksheetColumn());
        sheet2.Table.Columns.Add(new WorksheetColumn());
        sheet2.Table.Columns.Add(new WorksheetColumn());
        sheet2.Table.Columns.Add(new WorksheetColumn());
        sheet2.Table.Columns.Add(new WorksheetColumn());
        sheet2.Table.Columns.Add(new WorksheetColumn());
        sheet2.Table.Columns.Add(new WorksheetColumn());
        sheet2.Table.Columns.Add(new WorksheetColumn());
        sheet2.Table.Columns.Add(new WorksheetColumn());
        sheet2.Table.Columns.Add(new WorksheetColumn());
        sheet2.Table.Columns.Add(new WorksheetColumn());
        sheet2.Table.Columns.Add(new WorksheetColumn());
        sheet2.Table.Columns.Add(new WorksheetColumn());
        sheet2.Table.Columns.Add(new WorksheetColumn());
        sheet2.Table.Columns.Add(new WorksheetColumn());

        row = sheet2.Table.Rows.Add();
        row.Index = 0;
        row.Height = 27;
        row.AutoFitHeight = false;

        //WorksheetCell wcHeader;
        wcHeader = new WorksheetCell("Nro. Correlativo", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Secuencial", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Tipo Documento", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Aerolinea", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Pasajero", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Nro. Asiento", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Fecha Vuelo", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Nro. Vuelo", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Fecha Uso", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Fecha Rehabilitación", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Motivo", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Nro. Rehabilitación", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Tipo Vuelo", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Tipo Pasajero", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Tipo Trasbordo", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Estado Actual", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Nro. Boarding", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("ETicket", "HeaderStyle"); row.Cells.Add(wcHeader);
    
        iCant = 0;
        estilo = "usado";
        foreach (DataRow dr in dt_consulta_r.Rows)
        {
            row.AutoFitHeight = true;
            WorksheetRow rowD = sheet2.Table.Rows.Add();

            rowD.Cells.Add(new WorksheetCell(dt_consulta_r.Rows[iCant]["Num_Secuencial_Bcbp"].ToString(), DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta_r.Rows[iCant]["Correlativo"].ToString(), DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta_r.Rows[iCant]["Tip_Documento"].ToString(), DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta_r.Rows[iCant]["Dsc_Compania"].ToString(), DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta_r.Rows[iCant]["Nom_Pasajero"].ToString(), DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta_r.Rows[iCant]["Num_Asiento"].ToString(), DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta_r.Rows[iCant]["Fch_Vuelo"].ToString(), DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta_r.Rows[iCant]["Num_Vuelo"].ToString(), DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta_r.Rows[iCant]["Fch_Uso"].ToString(), DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta_r.Rows[iCant]["Fch_Rehab"].ToString(), DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta_r.Rows[iCant]["Dsc_Causal_Rehab"].ToString(), DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta_r.Rows[iCant]["Num_Proceso_Rehab"].ToString(), DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta_r.Rows[iCant]["Dsc_Vuelo"].ToString(), DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta_r.Rows[iCant]["Tip_Pasajero"].ToString(), DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta_r.Rows[iCant]["Dsc_Trasbordo"].ToString(), DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta_r.Rows[iCant]["Dsc_Tip_Estado"].ToString(), DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta_u.Rows[iCant]["Nro_Boarding"].ToString(), DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta_u.Rows[iCant]["Cod_ETicket"].ToString(), DataType.String, estilo));

            iCant++;
        }

        Worksheet sheet3 = book.Worksheets.Add("BP Diario REHAB RESUMEN");

        sheet3.Table.Columns.Add(new WorksheetColumn());
        sheet3.Table.Columns.Add(new WorksheetColumn());
        sheet3.Table.Columns.Add(new WorksheetColumn());
        sheet3.Table.Columns.Add(new WorksheetColumn());
        sheet3.Table.Columns.Add(new WorksheetColumn());
        sheet3.Table.Columns.Add(new WorksheetColumn());


        row = sheet3.Table.Rows.Add();
        row.Index = 0;
        row.Height = 27;
        row.AutoFitHeight = false;

        
        wcHeader = new WorksheetCell("Fecha Vuelo", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Nro. Vuelo", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Tipo Vuelo", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Tipo Pasajero", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Tipo Trasbordo", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Cantidad", "HeaderStyle"); row.Cells.Add(wcHeader);

        iCant = 0;
        estilo = "usado";
        foreach (DataRow dr in dt_consulta_r_res.Rows)
        {
            row.AutoFitHeight = true;
            WorksheetRow rowD = sheet3.Table.Rows.Add();

            rowD.Cells.Add(new WorksheetCell(dt_consulta_r_res.Rows[iCant]["Fch_Vuelo"].ToString(), DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta_r_res.Rows[iCant]["Num_Vuelo"].ToString(), DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta_r_res.Rows[iCant]["Dsc_Vuelo"].ToString(), DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta_r_res.Rows[iCant]["Tip_Pasajero"].ToString(), DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta_r_res.Rows[iCant]["Dsc_Trasbordo"].ToString(), DataType.String, estilo));
            rowD.Cells.Add(new WorksheetCell(dt_consulta_r_res.Rows[iCant]["Cantidad"].ToString(), DataType.String, estilo));

            iCant++;
        }
    
    

        Response.AppendHeader("content-disposition", "filename=BPDiario.xls");

        book.Save(Response.OutputStream);
%>
