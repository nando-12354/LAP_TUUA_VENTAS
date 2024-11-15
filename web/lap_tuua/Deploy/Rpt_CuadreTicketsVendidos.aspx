﻿<%@ page language="C#" autoeventwireup="true" inherits="Cns_CuadreTicketsEmitidos, App_Web_ehzg6gwo" %>

<%@ Register Src="UserControl/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="uc1" %>
<%@ Register Src="UserControl/PiePagina.ascx" TagName="PiePagina" TagPrefix="uc2" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LAP - Reporte - Cuadre de Stickers Vendidos o BP Usados</title>
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
    <meta name="keypage" content="rpt_cuadreticketvend" />
    <!-- #INCLUDE file="javascript/ValidarFecha.js" -->
    <!-- #INCLUDE file="javascript/KeyPress.js" -->
    <!-- #INCLUDE file="javascript/validarteclaF5.js" -->
    <!-- #INCLUDE file="javascript/MantenerSesion.js" -->
    <link href="css/Style.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        @import url(css/calendar-system.css);
    </style>
    <style type="text/css">
        html, body
        {
            height: 100%;
            overflow: hidden;
        }
        .ajax__calendar_container
        {
            z-index: 1000;
        }
    </style>

    <script language="JavaScript" type="text/javascript">

        function validarFiltros() {
            var isValid = false;
            var tipError = 0;
            //Clean screen
            document.getElementById('lblMensajeError').innerHTML = "";
            document.getElementById('lblMensajeErrorData').innerHTML = "";

            cleanGrilla();
            
            var sTicket = document.getElementById('chkTicket').checked;
            var sBoarding = document.getElementById('chkBoarding').checked;

            //Validacion Rango de Fecha
            if (!(isValidoRangoFecha(document.getElementById('txtDesde').value,'',document.getElementById('txtHasta').value,''))) {
                tipError = 1;
            }
            //Validacion Tipo Documento
            if (sBoarding == false && sTicket == false) {
                tipError = 2;
            }

            switch (tipError) {
                case 1: document.getElementById('lblMensajeError').innerHTML = "Error. Rango de Fechas ingresado es inválido"; break;
                case 2: document.getElementById('lblMensajeError').innerHTML = "Ingrese el tipo de consulta"; break;
                case 0: isValid = true; break;
            }

            return isValid;
        }

        function cleanGrilla() {
            if (document.getElementById("crptvCuadreTicketEmitidosReport") != null) {
                document.getElementById("crptvCuadreTicketEmitidosReport").style.display = "none";
            }
        }     
    </script>

    <link href="../css/Style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <table cellpadding="0" cellspacing="0" align="center" class="TamanoTabla">
            <tr>
                <!-- HEADER -->
                <td>
                    <uc1:CabeceraMenu ID="CabeceraMenu1" runat="server" />
                </td>
            </tr>
            <tr class="formularioTitulo2">
                <!-- FILTER -->
                <td>
                    <table border="0" cellpadding="0" cellspacing="0" class="EspacioSubTablaPrincipal">
                        <tr>
                            <td align="left" style="height: 30px; width: 80%">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <!-- TITLE -->
                                        <td style="width: 20px;" rowspan="5">
                                        </td>
                                        <td colspan="4" style="height: 20px;">
                                            <asp:Label ID="lblFechaTitulo" runat="server" CssClass="TextoEtiquetaBold">Fecha de Venta:</asp:Label>
                                        </td>
                                        <td style="width: 100px;" colspan="3">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblDesde" runat="server" CssClass="TextoFiltro"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td rowspan="2">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtDesde" runat="server" Width="88px" CssClass="textbox" Height="16px"
                                                            MaxLength="10" onfocus="this.blur();" onkeypress="JavaScript: window.event.keyCode = 0;"
                                                            BackColor="#E4E2DC"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="txtDesde_CalendarExtender" runat="server" Enabled="True"
                                                            PopupButtonID="imgbtnCalendar1" TargetControlID="txtDesde" Format="dd/MM/yyyy">
                                                        </cc1:CalendarExtender>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtDesde"
                                                            Display="None" ErrorMessage="Ingrese una fecha correcta" ValidationExpression="^\d{1,2}\/\d{1,2}\/\d{4}$">*</asp:RegularExpressionValidator>
                                                        <asp:RegularExpressionValidator ID="rfvFchDesde" runat="server" ControlToValidate="txtDesde"
                                                            Display="None" ErrorMessage="Ingrese una fecha correcta" ValidationExpression="^\d{1,2}\/\d{1,2}\/\d{4}$">*</asp:RegularExpressionValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <asp:Label ID="lblFechaDesde0" runat="server" CssClass="TextoEtiqueta" Text="( dd/mm/yyyy )"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td>
                                            &nbsp;<asp:ImageButton ID="imgbtnCalendar1" ImageUrl="~/Imagenes/Calendar.bmp" runat="server"
                                                Width="22px" Height="22px" />&nbsp;
                                        </td>
                                        <td>
                                            &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblHasta" runat="server" CssClass="TextoFiltro"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td rowspan="2">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtHasta" runat="server" Width="88px" CssClass="textbox" Height="16px"
                                                            MaxLength="10" onfocus="this.blur();" onkeypress="JavaScript: window.event.keyCode = 0;"
                                                            BackColor="#E4E2DC"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="txtHasta_CalendarExtender" runat="server" Enabled="True"
                                                            PopupButtonID="imgbtnCalendar2" TargetControlID="txtHasta" Format="dd/MM/yyyy">
                                                        </cc1:CalendarExtender>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtHasta"
                                                            Display="None" ErrorMessage="Ingrese una fecha correcta" ValidationExpression="^\d{1,2}\/\d{1,2}\/\d{4}$">*</asp:RegularExpressionValidator>
                                                        <asp:RegularExpressionValidator ID="rfvTxtHasta" runat="server" ControlToValidate="txtHasta"
                                                            Display="None" ErrorMessage="Ingrese una fecha correcta" ValidationExpression="^\d{1,2}\/\d{1,2}\/\d{4}$">*</asp:RegularExpressionValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <asp:Label ID="lblFechaHasta0" runat="server" CssClass="TextoEtiqueta" Text="( dd/mm/yyyy )"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td>
                                            &nbsp;<asp:ImageButton ID="imgbtnCalendar2" ImageUrl="~/Imagenes/Calendar.bmp" runat="server"
                                                Width="22px" Height="22px" />&nbsp;
                                        </td>
                                        <td style="width: 40px;">
                                        </td>
                                        <td>
                                            &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblTipoDocumento" runat="server" CssClass="TextoFiltro"></asp:Label>
                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td style="width: 200px;">
                                            <asp:CheckBox ID="chkTicket" runat="server" Text="Ticket" CssClass="TextoFiltro"
                                                Checked="True" />
                                            <asp:CheckBox ID="chkBoarding" runat="server" Text="Boarding" CssClass="TextoFiltro" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td align="right">
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btnConsultar" runat="server" OnClick="btnConsultar_Click" CssClass="Boton"
                                            OnClientClick="return validarFiltros()" Style="cursor: hand;" />&nbsp;&nbsp;&nbsp;
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="UpdatePanel3" runat="server"
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
                <!-- DATA -->
                <td valign="top">
                    <table border="0" cellpadding="0" cellspacing="0" class="EspacioSubTablaPrincipal">
                        <tr>
                            <td class="SpacingGrid">
                            </td>
                            <td class="CenterGrid" valign="top">
                                <div id="divData" style="overflow: auto; position: relative;">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <CR:CrystalReportViewer ID="crptvCuadreTicketEmitidosReport" runat="server" runat="server"
                                                AutoDataBind="true" DisplayGroupTree="False" EnableDatabaseLogonPrompt="False"
                                                EnableParameterPrompt="False" HasCrystalLogo="False" PrintMode="ActiveX" EnableTheming="False"
                                                HasGotoPageButton="False" HasSearchButton="False" HasToggleGroupTreeButton="False"
                                                HasViewList="False" SeparatePages="False" HasPageNavigationButtons="False" />
                                            <asp:Label ID="lblMensajeError" runat="server" CssClass="mensaje"></asp:Label>
                                            <asp:Label ID="lblMensajeErrorData" runat="server" CssClass="msgMensaje"></asp:Label>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnConsultar" EventName="Click" />
                                            <asp:PostBackTrigger ControlID="crptvCuadreTicketEmitidosReport" />
                                        </Triggers>
                                    </asp:UpdatePanel>                                    
                                </div>
                            </td>
                            <td class="SpacingGrid">
                            </td>
                        </tr>
                    </table>
                    <uc2:PiePagina ID="PiePagina1" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDesde">
    </cc1:CalendarExtender>
    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="txtHasta">
    </cc1:CalendarExtender>
    <asp:Label ID="txtOrdenacion" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="txtColumna" runat="server" Visible="False"></asp:Label>
    <asp:HiddenField ID="txtPaginacion" runat="server" />
    <asp:HiddenField ID="txtFiltroTipoDocumento" runat="server" />
    </form>
    <script src="javascript/resolucion.js" type="text/javascript"></script>
</body>
</html>
