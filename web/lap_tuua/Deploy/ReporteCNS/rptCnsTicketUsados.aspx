<%@ page language="C#" autoeventwireup="true" inherits="ReporteCNS_rptCnsTicketUsados, App_Web_qpqu9x_k" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<%@ Register src="../UserControl/ConsDetTicket.ascx" tagname="ConsDetTicket" tagprefix="uc1" %>

<%@ Register src="../UserControl/CnsDetBoarding.ascx" tagname="CnsDetBoarding" tagprefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>LAP - Reporte de consulta de ticket y boarding</title>
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
    <link href="../css/Style.css" rel="stylesheet" type="text/css" />
</head>
<body>

    <form id="form1" runat="server">
    <div>        
    <asp:ScriptManager ID="ScriptManager2" runat="server">
    </asp:ScriptManager>
          <asp:GridView ID="grvTicketUsadosRpt" runat="server" 
              AutoGenerateColumns="False" BackColor="White"
                                BorderColor="#999999" BorderStyle="None" 
              BorderWidth="1px" CellPadding="3" GridLines="Vertical"
                                Width="100%" AllowSorting="True"               
              onrowcommand="grvTicketUsadosRpt_RowCommand" 
              onsorting="grvTicketUsadosRpt_Sorting" onpageindexchanging="grvTicketUsadosRpt_PageIndexChanging" >
                                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Codigo" SortExpression="Codigo">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="codTipoDocumento" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                                CommandName="ShowTipoDocumento" Text='<%# Eval("Codigo") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Tipo Documento" DataField="Dsc_Documento" SortExpression="Dsc_Documento" />
                                    <asp:BoundField HeaderText="Modalidad Venta" DataField="Nom_Modalidad" SortExpression="Nom_Modalidad" />
                                    <asp:BoundField HeaderText="Aerolínea" DataField="Dsc_Compania" SortExpression="Dsc_Compania" />
                                    <asp:BoundField HeaderText="Nro. Vuelo" DataField="Dsc_Num_Vuelo" SortExpression="Dsc_Num_Vuelo" />
                                    <asp:TemplateField HeaderText="Fecha Vuelo" SortExpression="Fch_Vuelo">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFchVuelo" runat="server" Text='<%# LAP.TUUA.UTIL.Fecha.convertSQLToFecha(Convert.ToString(Eval("Fch_Vuelo")),null) %> '></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Fecha Uso" SortExpression="Log_Fecha_Mod">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFchUso" runat="server" Text='<%# LAP.TUUA.UTIL.Fecha.convertSQLToFecha(Convert.ToString(Eval("Log_Fecha_Mod")),Convert.ToString(Eval("Log_Hora_Mod"))) %> '></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Nro. Asiento" DataField="NroAsiento" SortExpression="NroAsiento" />
                                    <asp:BoundField HeaderText="Tipo Vuelo" DataField="Dsc_TipoVuelo" SortExpression="Dsc_TipoVuelo" />
                                    <asp:BoundField HeaderText="Tipo Persona" DataField="Dsc_TipoPasajero" SortExpression="Dsc_TipoPasajero" />
                                    <asp:BoundField HeaderText="Estado" DataField="Dsc_EstadoActual" SortExpression="Dsc_EstadoActual" />
                                    <asp:BoundField HeaderText="Tip. Trasbordo" DataField="Dsc_Trasbordo" SortExpression="Dsc_Trasbordo" />
                                    <asp:BoundField HeaderText="Secuencia" DataField="Num_Secuencial" SortExpression="Num_Secuencial" />
                                </Columns>
                                <SelectedRowStyle CssClass="grillaFila" />
                                <PagerStyle CssClass="grillaPaginacion" />
                                <HeaderStyle CssClass="grillaCabecera" />
                                <AlternatingRowStyle CssClass="grillaFila" />
           </asp:GridView>   
            
            
            
            
         <CR:CrystalReportViewer ID="crvResumenTipoDocumento" runat="server" 
        AutoDataBind="true"  DisplayGroupTree="False" EnableDatabaseLogonPrompt="False" 
             EnableParameterPrompt="False" HasCrystalLogo="False" PrintMode="ActiveX" 
             EnableTheming="False" HasGotoPageButton="False" HasSearchButton="False" 
             HasToggleGroupTreeButton="False" HasViewList="False" SeparatePages="False" HasRefreshButton="False" HasPrintButton="False" HasPageNavigationButtons="False" HasExportButton="False" HasDrillUpButton="False" HasZoomFactorList="False" DisplayToolbar="False" />
    </div>
    
      <asp:Label ID="txtOrdenacion" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="txtColumna" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="txtValorMaximoGrilla" runat="server" Visible="False"></asp:Label>    
        
    <uc1:ConsDetTicket ID="ConsDetTicket2" runat="server" />
    
        
    <uc2:CnsDetBoarding ID="CnsDetBoarding2" runat="server" />
    
        
    </form>
</body>
</html>
