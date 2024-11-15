<%@ Page Language="C#" ContentType="text/xml" %> 
<%@ Import Namespace="CarlosAg.ExcelXmlWriter"  %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="LAP.TUUA.CONTROL" %>
<%@ Import Namespace="LAP.TUUA.UTIL" %>

<%
    BO_Consultas objBOConsulta = new BO_Consultas();
        DataTable dt_consulta = new DataTable();

        //Set Paramaters values
        string idFechaDesde;
        string idFechaHasta;
        string idCajero;
        string idTurno;
    
        idFechaDesde = Fecha.convertToFechaSQL(Request.QueryString["idFechaDesde"]);
        idFechaHasta = Fecha.convertToFechaSQL(Request.QueryString["idFechaHasta"]);
        idCajero = Request.QueryString["idCajero"];
        idTurno = Request.QueryString["idTurno"];

        dt_consulta = objBOConsulta.ConsultaTurnoxFiltro2(idFechaDesde, idFechaHasta, idCajero, idTurno);

    
        Workbook book = new Workbook();
        Worksheet sheet = book.Worksheets.Add("Consulta Ticket Procesados");

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
           
        /*************************** FIN ESTILOS ****************************/

        sheet.Table.Columns.Add(new WorksheetColumn(60));
        sheet.Table.Columns.Add(new WorksheetColumn(250));
        sheet.Table.Columns.Add(new WorksheetColumn(125));
        sheet.Table.Columns.Add(new WorksheetColumn(110));
        sheet.Table.Columns.Add(new WorksheetColumn(80));

        WorksheetRow row = sheet.Table.Rows.Add();
        row.Index = 0;
        row.Height = 27;
        row.AutoFitHeight = false;

        WorksheetCell wcHeader;
        wcHeader = new WorksheetCell("Turno", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Cajero", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Equipo", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Fecha de Apertura", "HeaderStyle"); row.Cells.Add(wcHeader);
        wcHeader = new WorksheetCell("Nro. Ticket Vendidos", "HeaderStyle"); row.Cells.Add(wcHeader);


        int iCant = 0;
        foreach (DataRow dr in dt_consulta.Rows)
        {
            WorksheetRow rowD = sheet.Table.Rows.Add();

            rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Cod_Turno"].ToString(), DataType.String));
            rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Ape_Usuario"].ToString() + " " + dt_consulta.Rows[iCant]["Nom_Usuario"].ToString() + " (" + dt_consulta.Rows[iCant]["Cod_Usuario"].ToString() + ")", DataType.String));
            rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Dsc_Estacion"].ToString(), DataType.String));
            rowD.Cells.Add(new WorksheetCell(Fecha.convertSQLToFecha(dt_consulta.Rows[iCant]["Fch_Inicio"].ToString(), dt_consulta.Rows[iCant]["Hor_Inicio"].ToString()), DataType.String));
            rowD.Cells.Add(new WorksheetCell(dt_consulta.Rows[iCant]["Ticket_Proc"].ToString(), DataType.String));

            iCant++;
        }
            
        Response.AppendHeader("content-disposition", "filename=TicketsProcesados.xls");

        book.Save(Response.OutputStream);
%>
