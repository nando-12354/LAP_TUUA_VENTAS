<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Rpt_TicketBoardingUsadosMediaHoraDiaMes.aspx.cs"
    Inherits="Rpt_TicketBoardingUsadosMediaHoraDiaMes" ResponseEncoding="utf-8" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="UserControl/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="uc1" %>
<%@ Register Src="UserControl/PiePagina.ascx" TagName="PiePagina" TagPrefix="uc2" %>
<%@ Register Assembly="PaginGridView" Namespace="Pagin.Web.Control" TagPrefix="cc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>LAP - Reporte - Tickets o Boarding Pass Usados por Media Hora, Hora</title>
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
    <!-- #INCLUDE file="javascript/ValidarFecha.js" -->
    <!-- #INCLUDE file="javascript/KeyPress.js" -->
    <!-- #INCLUDE file="javascript/Functions.js" -->
    <!-- #INCLUDE file="javascript/validarteclaF5.js" -->
    <!-- #INCLUDE file="javascript/MantenerSesion.js" -->
    <link href="css/Style.css" rel="stylesheet" type="text/css" />
    
    <style type="text/css">
        @import url(css/calendar-system.css);
    </style>
    <style type="text/css">
        
        .ajax__calendar_container
        {
            z-index: 1000;
        }
        .style1
        {
            height: 15px;
        }
        .style2
        {
            height: 7px;
        }
    </style>

    <script language="javascript" type="text/javascript">
        
        function imgPrint_onclick() {

            var sFechaDesde = document.getElementById("txtDesde").value;
            var sFechaHasta = document.getElementById("txtHasta").value;
            var sHoraDesde = document.getElementById("txtHoraDesde").value;
            var sHoraHasta = document.getElementById("txtHoraHasta").value;
            var sTipoRango = document.getElementById("ddlTipoRango").value;
            var sAerolinea = document.getElementById("ddlAerolinea").value;
            var sTipoTicket = document.getElementById("ddlTipoTicket").value;
            var sNumVuelo = document.getElementById("txtNroVuelo").value;
            var sDestino = document.getElementById("txtDestino").value;

            if (document.forms[0].chkbTicket.checked)
                if (document.forms[0].chkbBP.checked) {
                sTDocumento = 'O';
            }
            else {  sTDocumento = 'T'; }
            else {
                if (document.forms[0].chkbBP.checked) {sTDocumento = 'B';}
                else {sTDocumento = 'N';}
            }

            //Descripciones
            var idDscR = document.getElementById("ddlTipoRango").options[document.getElementById("ddlTipoRango").selectedIndex].text;
            var idDscA = (sAerolinea != "0") ? document.getElementById("ddlAerolinea").options[document.getElementById("ddlAerolinea").selectedIndex].text : " -TODOS- ";
            var idDscT = (sTipoTicket != "0") ? document.getElementById("ddlTipoTicket").options[document.getElementById("ddlTipoTicket").selectedIndex].text : " -TODOS- ";

            var w = 900 + 32;
            var h = 500 + 96;
            var wleft = (screen.width - w) / 2;
            var wtop = (screen.height - h) / 2;

            var ventimp = window.open("ReporteRPT/rptTicketBPUsadosMediaHoraDiaMes.aspx" + "?sFechaDesde=" + sFechaDesde + "&sFechaHasta=" + sFechaHasta + "&sHoraDesde=" +
                                        sHoraDesde + "&sHoraHasta=" + sHoraHasta + "&sTipoRango=" + sTipoRango + "&sAerolinea=" +
                                        sAerolinea + "&sTipoTicket=" + sTipoTicket + "&sNumVuelo=" + sNumVuelo + "&sTDocumento=" + sTDocumento + "&sDestino=" + sDestino
                                        + "&idDscR=" + idDscR
                                        + "&idDscA=" + idDscA
                                        + "&idDscT=" + idDscT
                                        , "mywindow", "location=0,status=0,scrollbars=1,menubar=0,width=900,height=500,left=" + wleft + ",top=" + wtop);
            ventimp.focus();
        }

       function validarExcel() {
            var numRegistros = document.getElementById("lblTotalRows").value;
            var maxRegistros = document.getElementById("lblMaxRegistros").value;
            if (!isNaN(parseInt(numRegistros)) && numRegistros>0) {
                if (parseInt(numRegistros) <= parseInt(maxRegistros)) {
                    return true;
                }
                else {
                    alert("La exportación a excel solo permite " + maxRegistros + " registros");
                    return false;
                }
            }
            else {
                alert("No existen registros para exportar \n Primero debe presionar el botón consultar");
                return false;
            }
        }
        function validarCampos() {
            document.getElementById('lblMensajeError').innerHTML = "";
            document.getElementById('lblMensajeErrorData').innerHTML = "";

            cleanGrilla();

            //Validate Form
            var frm = window.document.forms[0];
            var ckTicket = frm.chkbTicket.checked;
            var ckBoarding = frm.chkbBP.checked;

            if (!(ckTicket || ckBoarding)) {
                document.getElementById('lblMensajeError').innerHTML = "Seleccione al menos un Tipo de Documento";
                return false;
            }

            return validaFecha();
        }

        function cleanGrilla() {
            if (document.getElementById("grvTicketBoardingMes") != null) {
                document.getElementById("grvTicketBoardingMes").style.display = "none";
            }
            if (document.getElementById("grvDataResumen") != null) {
                document.getElementById("grvDataResumen").style.display = "none";
            }
        }
        
        function validaFecha() {
            var compFin = form1.txtHasta.value.substr(6, 4) + form1.txtHasta.value.substr(3, 2) + form1.txtHasta.value.substr(0, 2);
            var compIni = form1.txtDesde.value.substr(6, 4) + form1.txtDesde.value.substr(3, 2) + form1.txtDesde.value.substr(0, 2);
            if (compFin < compIni) {
                document.getElementById('lblMensajeError').innerHTML = "Rango incorrecto de Fechas";
                return false;
            } else {
                if (compFin == compIni) {
                    var horIni = form1.txtDesde.value.substr(0, 2) + form1.txtDesde.value.substr(3, 2);
                    var horFin = form1.txtHasta.value.substr(0, 2) + form1.txtHasta.value.substr(3, 2);

                    if (horFin < horIni) {
                        document.getElementById('lblMensajeError').innerHTML = "Rango incorrecto de Horas";
                        return false;
                    }
                }
            }
            return true;
        }

        function controlaHoraDesde() {

            if (window.event.keyCode == 8 || window.event.keyCode == 46) {
                form1.txtHoraDesde.value = form1.txtHoraDesde.value.substring(0, form1.txtHoraDesde.value.length - 2);
            }
            else {

                if (form1.txtHoraDesde.value.length == 2) {
                    form1.txtHoraDesde.value = form1.txtHoraDesde.value + ':';
                }
            }
        }

        function controlaHoraHasta() {

            if (window.event.keyCode == 8 || window.event.keyCode == 46) {
                form1.txtHoraHasta.value = form1.txtHoraHasta.value.substring(0, form1.txtHoraHasta.value.length - 2);
            }
            else {

                if (form1.txtHoraHasta.value.length == 2) {
                    form1.txtHoraHasta.value = form1.txtHoraHasta.value + ':';
                }
            }

        }  

    </script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="600">
        </asp:ScriptManager>
    <table align="center" border="0" cellpadding="0" cellspacing="0" class="TamanoTabla">
        <tr>
            <!-- HEADER -->
            <td>
                <uc1:CabeceraMenu ID="CabeceraMenu2" runat="server" />
            </td>
        </tr>
        <tr class="formularioTitulo2">
            <td>
                <!-- FILTER ZONE -->
                <table border="0" cellpadding="0" cellspacing="0" class="EspacioSubTablaPrincipal">
                    <tr>
                        <td align="left" style="height: 30px; width: 100%">
                            <table cellpadding="1" cellspacing="0">
                                <tr>
                                    <!-- TITLE -->
                                    <td style="width: 20px;" rowspan="6">
                                    </td>
                                    <td colspan="13" style="height: 20px;">
                                        <asp:Label ID="Label1" Text="Fecha de Uso:" runat="server" CssClass="TextoEtiquetaBold"></asp:Label>
                                    </td>
                                    <td style="width: 20px;" rowspan="5">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width:4%;">
                                        <asp:Label ID="lblDesde" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                    </td>
                                    <td rowspan="2" style="width: 80px;">
                                        <table cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtDesde" runat="server" BackColor="#E4E2DC" CssClass="textbox"
                                                        onfocus="this.blur();" onkeypress="JavaScript: window.event.keyCode = 0;" Width="72px"></asp:TextBox>
                                                   <cc1:CalendarExtender ID="txtDesde_CalendarExtender" runat="server" Enabled="True"
                                                            PopupButtonID="imbCalDesde" TargetControlID="txtDesde" Format="dd/MM/yyyy">
                                                        </cc1:CalendarExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <asp:Label ID="lblFechaDesde0" runat="server" CssClass="TextoFiltro" Text="(dd/mm/yyyy)"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="width: 25px;">
                                        <asp:ImageButton ID="imbCalDesde" runat="server" Height="22px" ImageUrl="~/Imagenes/Calendar.bmp"
                                            Width="22px" />
                                    </td>
                                    <td rowspan="2" style="width: 60px;">
                                        <table cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtHoraDesde" runat="server" CssClass="textbox" MaxLength="5" onKeyDown="JavaScript:controlaHoraDesde();"
                                                        onkeypress="JavaScript: Tecla('Time');" ReadOnly="false" Width="56px">
                                                    </asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <asp:Label ID="lblHoraDesde" runat="server" CssClass="TextoFiltro" Text="(hh:mm)"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="width: 1%;">
                                    </td>
                                    <td style="width: 8%;">
                                        <asp:Label ID="lblTipoDocumento" runat="server" CssClass="TextoFiltro" Width="100%"></asp:Label>
                                    </td>
                                    <td style="width: 13%;">
                                        <asp:CheckBox ID="chkbTicket" runat="server" Checked="True" CssClass="TextoEtiqueta"/>
                                            <!--onclick="Javascript: SetCheckBoxTicket(this,'chkbBP','ddlTipoTicket');"--> 
                                        <asp:CheckBox ID="chkbBP" runat="server" CssClass="TextoEtiqueta" />
                                        <!--onclick="Javascript: SetCheckBoxBoarding(this,'chkbTicket','ddlTipoTicket');"--> 
                                    </td>
                                    <td class="style1">
                                    </td>
                                    <td style="width: 6%;">
                                        <asp:Label ID="lblAerolinea" runat="server" CssClass="TextoFiltro"></asp:Label>
                                    </td>
                                    <td style="width: 28%;">
                                        <asp:DropDownList ID="ddlAerolinea" runat="server" CssClass="combo2"
                                            Width="100%">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 1%;">
                                    </td>
                                    <td style="width: 6%;">
                                        &nbsp;</td>
                                    <td style="width: 8%;">
                                        &nbsp;</td>
                                </tr>
                                <tr style="vertical-align: top;">
                                    <td>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td colspan="3">
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblHasta" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                    </td>
                                    <td rowspan="2">
                                        <table cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtHasta" runat="server" BackColor="#E4E2DC" CssClass="textbox"
                                                        onfocus="this.blur();" onkeypress="JavaScript: window.event.keyCode = 0;" Width="72px"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True"
                                                            PopupButtonID="imbCalHasta" TargetControlID="txtHasta" Format="dd/MM/yyyy">
                                                        </cc1:CalendarExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <asp:Label ID="lblFechaHasta" runat="server" CssClass="TextoFiltro" Text="(dd/mm/yyyy)"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="imbCalHasta" runat="server" Height="22px" ImageUrl="~/Imagenes/Calendar.bmp"
                                            Width="22px" />
                                    </td>
                                    <td rowspan="2">
                                        <table cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtHoraHasta" runat="server" CssClass="textbox" MaxLength="5" onKeyDown="JavaScript:controlaHoraHasta();"
                                                        onkeypress="JavaScript: Tecla('Time');" ReadOnly="false" Width="56px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <asp:Label ID="lblHoraHasta" runat="server" CssClass="TextoFiltro" Text="(hh:mm)"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblTipoRango" runat="server" CssClass="TextoFiltro" Height="16px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlTipoRango" runat="server" CssClass="combo2"
                                            Width="100%">
                                            <asp:ListItem Value="HORA">Por cada Hora</asp:ListItem>
                                            <asp:ListItem Value="MEDIA">Por cada Media Hora</asp:ListItem>
                                        </asp:DropDownList>                                        
                                    </td>
                                    <td class="style1">
                                    </td>
                                    <td>
                                        <asp:Label ID="lblTipoTicket" runat="server" CssClass="TextoFiltro"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlTipoTicket" runat="server" CssClass="combo2"
                                            Width="80%">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                    </td>
                                    <td colspan="2" align="right" rowspan="3">
                                    <asp:LinkButton ID="lbExportar" runat="server" 
                                    onclick="lbExportar_Click" OnClientClick="return validarExcel();">[Exportar a Excel]</asp:LinkButton>
                                    <br /><br />
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <a href="#" id="lnkHabilitar" runat="server" onclick="javascript:imgPrint_onclick();" style="cursor: hand;">
                                                        <b>
                                                            <asp:Label ID="lblImprimir" runat="server">Imprimir</asp:Label>
                                                        </b></a>&nbsp;&nbsp;&nbsp;
                                                    <asp:Button ID="Button1" runat="server" OnClick="btnBuscar_Click" OnClientClick="return validarCampos()"
                                                        CssClass="Boton" Text="Consultar" ValidationGroup="grupito" Style="cursor: hand;" />&nbsp;&nbsp;&nbsp;
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <asp:UpdateProgress ID="UpdateProgress3" AssociatedUpdatePanelID="UpdatePanel2" runat="server">
                                                <ProgressTemplate>
                                                    <div id="progressBackgroundFilter">
                                                    </div>
                                                    <div id="processMessage">
                                                        &nbsp;&nbsp;&nbsp;Procesando...<br />
                                                        <br />
                                                        <img alt="Loading" src="Imagenes/ajax-loader.gif" />
                                                    </div>
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>
                                    </td>
                                </tr>
                                <tr style="vertical-align: top;">
                                    <td class="style2">
                                    </td>
                                    <td class="style2">
                                        &nbsp;
                                    </td>
                                    <td class="style2">
                                    </td>
                                    <td class="style2"></td>
                                    <td class="style2"></td>
                                    <td class="style2"></td>
                                    <td class="style2"></td>
                                    <td class="style2"></td>
                                    <td class="style2"></td>
                                </tr>
                                
                                <tr>
                                    <td>
                                        
                                    </td>
                                    <td rowspan="2" colspan="3">
                                        
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblDestino" runat="server" CssClass="TextoFiltro"></asp:Label>
                                    </td>
                                    <td><asp:TextBox ID="txtDestino" runat="server" CssClass="textbox" Height="16px" MaxLength="3"
                                            onkeypress="JavaScript: Tecla('Character');" onblur="gDescripcionNombre(this)"
                                            Width="141px"></asp:TextBox>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        
                                        <asp:Label ID="lblNumeroVuelo" runat="server" CssClass="TextoFiltro"></asp:Label>
                                        
                                    </td>
                                    <td>

                                        <asp:TextBox ID="txtNroVuelo" runat="server" CssClass="textbox" Height="16px" MaxLength="10"
                                            onkeypress="JavaScript: Tecla('NumeroyLetra');" Width="141px"></asp:TextBox>

                                    </td>
                                    <td>
                                    </td>
                                </tr>                                
                                
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <!-- SPACE -->
            <td>
                <hr color="#0099cc" style="width: 100%; height: 0px" />
            </td>
        </tr>
        <tr>
            <!-- CONTENT -->
            <td>
                <table border="0" cellpadding="0" cellspacing="0" class="EspacioSubTablaPrincipal">
                    <tr>
                        <td class="SpacingGrid">
                        </td>
                        <td class="CenterGrid">
                            
                            <div id="divData" class="divSizeCustom">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <cc2:PagingGridView ID="grvTicketBoardingMes" runat="server" AutoGenerateColumns="False"
                                                BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
                                                CellPadding="3" Width="100%" AllowPaging="True" CssClass="grilla" AllowSorting="True"
                                                  
                                                OnPageIndexChanging="grvTicketBoardingMes_PageIndexChanging" 
                                                GroupingDepth="5" onsorting="grvTicketBoardingMes_Sorting" 
                                                onrowcreated="grvTicketBoardingMes_RowCreated">
                                                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                <PagerSettings Mode="NumericFirstLast" />
                                                <Columns>
                                                    <asp:BoundField HeaderText="Fecha Uso" DataField="Log_Fecha_Mod" 
                                                        SortExpression="Log_Fecha_Mod" >
                                                        <ItemStyle VerticalAlign="Top" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Tipo Documento" DataField="Tipo_Documento" 
                                                        SortExpression="Tipo_Documento" >
                                                        <ItemStyle VerticalAlign="Top" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Tipo Ticket" DataField="Dsc_Tipo_Ticket" 
                                                        SortExpression="Dsc_Tipo_Ticket" >
                                                        <ItemStyle VerticalAlign="Top" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Aerolinea" DataField="Dsc_Compania" 
                                                        SortExpression="Dsc_Compania" >
                                                        <ItemStyle VerticalAlign="Top" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Nro. Vuelo" DataField="Num_Vuelo" 
                                                        SortExpression="Num_Vuelo" >
                                                        <ItemStyle VerticalAlign="Top" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Hora Inicio" DataField="HoraInicio" />
                                                    <asp:BoundField HeaderText="Hora Fin" DataField="HoraFin" />
                                                    <asp:BoundField HeaderText="BP" DataField="Total_BCBP" />
                                                    <asp:BoundField HeaderText="Ticket" DataField="Total_Ticket" />
                                                    <asp:TemplateField HeaderText="Total">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTotal" runat="server" Text='<%# Int32.Parse(Eval("Total_BCBP").ToString()) + Int32.Parse(Eval("Total_Ticket").ToString()) %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <SelectedRowStyle CssClass="grillaFila" />
                                                <PagerStyle CssClass="grillaPaginacion" />
                                                <HeaderStyle CssClass="grillaCabecera" />
                                                <AlternatingRowStyle CssClass="grillaFila" />
                                            </cc2:PagingGridView>
                                            <br />
                                            <div>
                                                <cc2:PagingGridView ID="grvDataResumen" runat="server" BorderColor="#999999" BorderStyle="None"
                                                    BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" AllowPaging="False"
                                                    HorizontalAlign="Center" CssClass="grillaShort"
                                                    GroupingDepth="1" ShowFooter="false"  
                                                    onrowcreated="grvDataResumen_RowCreated" OnRowDataBound="grvDataResumen_RowDataBound">
                                                
                                                    <PagerSettings Mode="NumericFirstLast" />
                                                    <Columns>
                                                        <asp:BoundField HeaderText="Tipo Documento" DataField="Tipo_Documento" />
                                                        <asp:BoundField HeaderText="Tipo Venta" DataField="Tip_Venta" />
                                                        <asp:BoundField HeaderText="Tipo Vuelo" DataField="Tip_Vuelo" />
                                                        <asp:BoundField HeaderText="Tipo Pasajero" DataField="Tip_Pasajero" />
                                                        <asp:BoundField HeaderText="Tipo Trasbordo" DataField="Tip_Trasbordo" />
                                                        <asp:BoundField HeaderText="Cantidad" DataField="Total" 
                                                            ItemStyle-HorizontalAlign="Right" NullDisplayText="0" >
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                    </Columns>
                                                    <FooterStyle BackColor="#CCCCCC" Font-Bold="true" ForeColor="Black" HorizontalAlign="Right" />
                                                    <SelectedRowStyle CssClass="grillaFila" />
                                                    <HeaderStyle CssClass="grillaCabecera" />
                                                    <AlternatingRowStyle CssClass="grillaFila" />
                                                </cc2:PagingGridView>
                                                <%--<asp:Label ID="lblTotalRows" runat="server" CssClass="TextoCampo" ForeColor="White"></asp:Label>
                                    <asp:Label ID="lblMaxRegistros" runat="server" CssClass="TextoCampo" ForeColor="White"></asp:Label>--%>
                                            <input type="hidden" id="lblTotalRows" runat="server" value="0" />
                                            <input type="hidden" id="lblMaxRegistros" runat="server" value="0" />
                                            </div>
                                            <asp:Label ID="lblMensajeErrorData" runat="server" CssClass="msgMensaje"></asp:Label>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                    <asp:UpdateProgress ID="UpdateProgress2" AssociatedUpdatePanelID="UpdatePanel1" runat="server"
                                        DisplayAfter="100">
                                        <ProgressTemplate>
                                            <div id="progressBackgroundFilter">
                                            </div>
                                            <div id="processMessage">
                                                &nbsp;&nbsp;&nbsp;Procesando...<br />
                                                <br />
                                                <img alt="Loading" src="Imagenes/ajax-loader.gif" />
                                            </div>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                    <asp:Label ID="lblMensajeError" runat="server" CssClass="mensaje"></asp:Label>
                                    
                                </div>
                        </td>
                        <td class="SpacingGrid">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table align="center" border="0" cellpadding="0" cellspacing="0" class="TamanoTabla">
                    <tr>
                        <td style="height: 11px">
                            <uc2:PiePagina ID="PiePagina1" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>

</body>
</html>
