<%@ page language="C#" autoeventwireup="true" inherits="Rpt_BoardingLeidosMolinete, App_Web_ehzg6gwo" responseencoding="utf-8" %>

<%@ Register Src="UserControl/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="uc1" %>
<%@ Register Src="UserControl/PiePagina.ascx" TagName="PiePagina" TagPrefix="uc2" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Assembly="PaginGridView" Namespace="Pagin.Web.Control" TagPrefix="cc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LAP - Reporte - Boarding Pass Leídos en el Molinete</title>
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
    <!-- #INCLUDE file="javascript/ValidarFecha.js" -->
    <!-- #INCLUDE file="javascript/KeyPress.js" -->
    <!-- #INCLUDE file="javascript/Functions.js" -->
    <!-- #INCLUDE file="javascript/validarteclaF5.js" -->
    <!-- #INCLUDE file="javascript/MantenerSesion.js" -->
    <link href="css/Style.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .ajax__calendar_container
        {
            z-index: 1000;
        }
    </style>

    <script language="JavaScript" type="text/javascript">


        /*function controlaFechaLectura() {

            if (window.event.keyCode == 8 || window.event.keyCode == 46) {
        //form1.txtDesde.value = form1.txtDesde.value.substring(0, form1.txtDesde.value.length - 2);
        }
        else {

                if (form1.txtFechaLectura.value.length == 2) {
        form1.txtFechaLectura.value = form1.txtFechaLectura.value + '/';
        }
        if (form1.txtFechaLectura.value.length == 5) {
        form1.txtFechaLectura.value = form1.txtFechaLectura.value + '/';
        }
        }
        }




        function controlaFechaVuelo() {

            if (window.event.keyCode == 8 || window.event.keyCode == 46) {
        //form1.txtDesde.value = form1.txtDesde.value.substring(0, form1.txtDesde.value.length - 2);
        }
        else {

                if (form1.txtFechaVuelo.value.length == 2) {
        form1.txtFechaVuelo.value = form1.txtFechaVuelo.value + '/';
        }
        if (form1.txtFechaVuelo.value.length == 5) {
        form1.txtFechaVuelo.value = form1.txtFechaVuelo.value + '/';
        }
        }
        }


        function ConsultarDetalleBoarding() {
        var selectedIdx = document.getElementById('ddlCompania').selectedIndex;
        var selectedIdxEst = document.getElementById('ddlEstado').selectedIndex;

            var idCompania = document.getElementById("ddlCompania").value;
        var sFechaVuelo = document.getElementById("txtFechaVuelo").value;
        var sNumVuelo = document.getElementById("txtNumVuelo").value;
        var sFechaLecturaIni = document.getElementById("txtFechaLecturaIni").value;
        var sFechaLecturaFin = document.getElementById("txtFechaLecturaFin").value;
        var idEstado = document.getElementById("ddlEstado").value;
        var sNumBoarding = document.getElementById("txtNumBoarding").value;

            if (selectedIdx != '0') {
        var pCompaniaTexto = document.getElementById("ddlCompania").options[selectedIdx].text;
        } else { pCompaniaTexto = '0'; }


            if (selectedIdxEst != '0') {
        var pEstadoTexto = document.getElementById("ddlEstado").options[selectedIdxEst].text;
        } else { pEstadoTexto = '0'; }


            document.frames.iRptBoardingMolinete.location.href = "ReporteRPT/rptBoardingMolinete.aspx?idCompania=" + idCompania + "&sFechaVuelo=" + sFechaVuelo + "&sNumVuelo=" + sNumVuelo + "&sFechaLecturaIni=" + sFechaLecturaIni + "&sFechaLecturaFin=" + sFechaLecturaFin + "&idEstado=" + idEstado + "&sNumBoarding=" + sNumBoarding + "&pCompaniaTexto=" + pCompaniaTexto + "&pEstadoTexto=" + pEstadoTexto;


            return false;
        }*/

        function imgPrint_onclick() {
            //Filtros
            var idCompania = document.getElementById("ddlCompania").value;
            var sFechaVuelo = document.getElementById("txtFechaVuelo").value;
            var sNumVuelo = document.getElementById("txtNumVuelo").value;
            var sFechaLecturaIni = document.getElementById("txtFechaLecturaIni").value;
            var sFechaLecturaFin = document.getElementById("txtFechaLecturaFin").value;
            var sNumBoarding = document.getElementById("txtNumBoarding").value;

            //Descripciones
            var idDscC = (idCompania != "0") ? document.getElementById("ddlCompania").options[document.getElementById("ddlCompania").selectedIndex].text : "";

            var w = 900 + 32;
            var h = 500 + 96;
            var wleft = (screen.width - w) / 2;
            var wtop = (screen.height - h) / 2;

            var ventimp = window.open("ReporteRPT/rptBoardingMolinete.aspx?idCompania=" + idCompania + "&sFechaVuelo=" + sFechaVuelo + "&sNumVuelo=" + sNumVuelo + "&sFechaLecturaIni=" + sFechaLecturaIni + "&sFechaLecturaFin=" + sFechaLecturaFin + "&sNumBoarding=" + sNumBoarding + "&pCompaniaTexto=" + idDscC
                                                        , "mywindow", "location=0,status=0,scrollbars=1,menubar=1,width=900,height=500,left=" + wleft + ",top=" + wtop);
            //ventimp.moveTo(wleft, wtop);
            ventimp.focus();
        }

        function validarExcel() {
            var numRegistros = document.getElementById("lblTotalRows").value;
            var maxRegistros = document.getElementById("lblMaxRegistros").value;
            if (!isNaN(parseInt(numRegistros))) {
                if (parseInt(numRegistros) <= parseInt(maxRegistros)) {
                    return true;
                }
                else {
                    alert("La exportación a excel solo permite " + maxRegistros + " registros");
                    return false;
                }
            }
            else {
                alert("No existen registros para exportar \nSeleccione otros filtros");
                return false;
            }
        }
    </script>

    <link href="../css/Style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="3600">
        </asp:ScriptManager>
        <table cellpadding="0" cellspacing="0" align="center" class="TamanoTabla">
            <tr>
                <!-- HEADER -->
                <td align="center">
                    <uc1:CabeceraMenu ID="CabeceraMenu1" runat="server" />
                </td>
            </tr>
            <tr class="formularioTitulo2">
                <!-- FILTER -->
                <td>
                    <table border="0" cellpadding="0" cellspacing="0" class="EspacioSubTablaPrincipal">
                        <tr>
                            <td align="left" style="height: 30px; width: 100%;">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <!-- TITLE -->
                                        <td style="width: 20px;" rowspan="5">
                                        </td>
                                        <td colspan="12" style="height: 20px;">
                                            <asp:Label ID="lblFechaTitulo" runat="server" CssClass="TextoEtiquetaBold">Fecha de Uso:</asp:Label>
                                        </td>
                                        <td style="width: 20px;" rowspan="5">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 5%;">
                                            &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblFechaLecturaIni" runat="server" CssClass="TextoFiltro"></asp:Label>
                                        </td>
                                        <td rowspan="2" style="width: 80px;">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtFechaLecturaIni" runat="server" CssClass="textbox" Height="16px"
                                                            MaxLength="10" onfocus="this.blur();" onkeypress="JavaScript: window.event.keyCode = 0;"
                                                            BackColor="#E4E2DC" Width="72px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <asp:Label ID="lblFechaDesde0" runat="server" CssClass="TextoEtiqueta" Text="( dd/mm/yyyy )"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="width: 25px;">
                                            <asp:ImageButton ID="imgbtnCalendar1" ImageUrl="~/Imagenes/Calendar.bmp" runat="server"
                                                Width="22px" Height="22px" />
                                        </td>
                                        <td style="width: 3%;" rowspan="2">
                                        </td>
                                        <td style="width: 6%;">
                                            <asp:Label ID="lblFechaVuelo" runat="server" CssClass="TextoFiltro" Width="100%"></asp:Label>
                                        </td>
                                        <td rowspan="2" style="width: 80px;">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtFechaVuelo" runat="server" Width="72px" CssClass="textbox" Height="16px"
                                                            MaxLength="10" onfocus="this.blur();" onkeypress="JavaScript: window.event.keyCode = 0;"
                                                            BackColor="#E4E2DC"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <asp:Label ID="lblFormaFchVuelo" runat="server" Text="(dd/mm/yyyy)" CssClass="TextoFiltro"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="width: 25px;">
                                            <asp:ImageButton ID="imgbtnFechaVuelo" ImageUrl="~/Imagenes/Calendar.bmp" runat="server"
                                                Width="22px" Height="22px" />
                                        </td>
                                        <td style="width: 3%;" rowspan="2">
                                        </td>
                                        <td style="width: 7%;">
                                            <asp:Label ID="lblCompania" runat="server" CssClass="TextoFiltro" Width="100%"></asp:Label>
                                        </td>
                                        <td style="width: 28%;">
                                            <asp:DropDownList ID="ddlCompania" runat="server" Width="100%" CssClass="combo2">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 3%;">
                                        </td>
                                        <td rowspan="3" align="right">
                                            <b>
                                                <asp:LinkButton ID="lbExportar" runat="server" OnClientClick="return validarExcel();"
                                                    OnClick="lbExportar_Click">[Exportar a Excel]</asp:LinkButton></b>
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <br />
                                                    <br />
                                                    &nbsp;<a href="" id="lnkHabilitar" runat="server" onclick="javascript:imgPrint_onclick();"
                                                        style="cursor: hand;"><b>
                                                            <asp:Label ID="lblImprimir" runat="server">Imprimir</asp:Label>
                                                        </b></a>&nbsp;&nbsp;&nbsp;
                                                    <asp:Button ID="btnConsultar" runat="server" CssClass="Boton" Style="cursor: hand;"
                                                        OnClick="btnConsultar_Click" />
                                                    &nbsp;&nbsp;&nbsp;
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="UpdatePanel3" runat="server"
                                                DisplayAfter="10">
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
                                        <td>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td colspan="2">
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td colspan="2">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblFechaLecturaFin" runat="server" CssClass="TextoFiltro"></asp:Label>
                                        </td>
                                        <td rowspan="2">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtFechaLecturaFin" runat="server" Width="72px" CssClass="textbox"
                                                            Height="16px" MaxLength="10" onfocus="this.blur();" onkeypress="JavaScript: window.event.keyCode = 0;"
                                                            BackColor="#E4E2DC"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <asp:Label ID="lblFechaFin0" runat="server" CssClass="TextoFiltro" Text="(dd/mm/yyyy)"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="imgbtnCalendar2" ImageUrl="~/Imagenes/Calendar.bmp" runat="server"
                                                Width="22px" Height="22px" />
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblNumVuelo" runat="server" CssClass="TextoFiltro"></asp:Label>
                                        </td>
                                        <td colspan="2">
                                            <asp:TextBox ID="txtNumVuelo" runat="server" Width="88px" CssClass="textbox" onkeypress="JavaScript: Tecla('Alphanumeric');"
                                                onblur="gDescripcion(this)" Height="16px" MaxLength="10"></asp:TextBox>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblNumBoarding" runat="server" CssClass="TextoFiltro"></asp:Label>
                                        </td>
                                        <td style="width: 100px;">
                                            <asp:TextBox ID="txtNumBoarding" runat="server" Width="123px" CssClass="textbox"
                                                Height="16px" MaxLength="16" onkeypress="JavaScript: Tecla('Alphanumeric');"
                                                onblur="gDescripcion(this)"></asp:TextBox>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr style="vertical-align: top;">
                                        <td>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td colspan="3">
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
                <!-- DATA -->
                <td>
                    <table border="0" cellpadding="0" cellspacing="0" class="EspacioSubTablaPrincipal">
                        <tr>
                            <td class="SpacingGrid">
                            </td>
                            <td class="CenterGrid">
                                <div id="divData" class="divSizeCustom">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <cc2:PagingGridView ID="grvBoardingMolinete" runat="server" AutoGenerateColumns="False"
                                                BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
                                                CellPadding="3" Width="100%" AllowPaging="True" CssClass="grilla" AllowSorting="True"
                                                VirtualItemCount="-1" OnPageIndexChanging="grvBoardingMolinete_PageIndexChanging">
                                                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                <PagerSettings Mode="NumericFirstLast" />
                                                <Columns>
                                                    <asp:BoundField HeaderText="Nro. Correlativo" DataField="Num_Secuencial_Bcbp" />
                                                    <asp:BoundField HeaderText="Nro. Boarding" DataField="Cod_Numero_Bcbp" />
                                                    <asp:BoundField HeaderText="Aerolínea" DataField="Dsc_Compania" />
                                                    <asp:BoundField HeaderText="Nro. Vuelo" DataField="Num_Vuelo" />
                                                    <asp:BoundField HeaderText="Fecha Vuelo" DataField="Fch_Vuelo" />
                                                    <asp:BoundField HeaderText="Nro. Asiento" DataField="Num_Asiento" />
                                                    <asp:BoundField HeaderText="Nro. Molinete" DataField="NroMolinete" />
                                                    <asp:BoundField HeaderText="Fecha Uso" DataField="FechaUso" />
                                                    <asp:BoundField HeaderText="Estado" DataField="Dsc_Campo" />
                                                </Columns>
                                                <SelectedRowStyle CssClass="grillaFila" />
                                                <PagerStyle CssClass="grillaPaginacion" />
                                                <HeaderStyle CssClass="grillaCabecera" />
                                            </cc2:PagingGridView>
                                            <br />
                                            <div>
                                                <cc2:PagingGridView ID="grvDataResumen" runat="server" BorderColor="#999999" BorderStyle="None"
                                                    BorderWidth="1px" CellPadding="3" GridLines="Both" AutoGenerateColumns="False" 
                                                    HorizontalAlign="Center" CssClass="grillaShort" AllowPaging="False" AllowSorting="False"
                                                    OnRowCreated="grvDataResumen_RowCreated" OnRowDataBound="grvDataResumen_RowDataBound"
                                                    GroupingDepth="4" ShowFooter="true">
                                                    <PagerSettings Mode="NumericFirstLast" />
                                                    <Columns>
                                                        <asp:BoundField HeaderText="Fecha Lectura" DataField="FechaUso" />
                                                        <asp:BoundField HeaderText="Aerolínea" DataField="Dsc_Compania" />
                                                        <asp:BoundField HeaderText="Nro. Vuelo" DataField="Num_Vuelo" />
                                                        <asp:BoundField HeaderText="Tipo Vuelo" DataField="Tip_Vuelo" />
                                                        <asp:BoundField HeaderText="Tipo Pasajero" DataField="Tip_Pasajero" />
                                                        <asp:BoundField HeaderText="Tipo Trasbordo" DataField="Tip_Trasbordo" />
                                                        <asp:BoundField HeaderText="Cantidad" DataField="Cantidad" NullDisplayText="0" ItemStyle-HorizontalAlign="Right">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                    </Columns>
                                                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                    <SelectedRowStyle CssClass="grillaFila" />
                                                    <PagerStyle CssClass="grillaPaginacion" />
                                                    <HeaderStyle CssClass="grillaCabecera" />
                                                    <AlternatingRowStyle CssClass="grillaFila" />
                                                </cc2:PagingGridView>
                                                <asp:HiddenField ID="lblTotalRows" runat="server" />
                                                <asp:HiddenField ID="lblMaxRegistros" runat="server" />
                                            </div>
                                            <asp:Label ID="lblMensajeErrorData" runat="server" CssClass="msgMensaje"></asp:Label>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnConsultar" EventName="Click" />
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
                    <uc2:PiePagina ID="PiePagina1" runat="server" />
                    <br />
                </td>
            </tr>
        </table>
    </div>
    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" PopupButtonID="imgbtnCalendar1"
        TargetControlID="txtFechaLecturaIni">
    </cc1:CalendarExtender>
    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" PopupButtonID="imgbtnCalendar2"
        TargetControlID="txtFechaLecturaFin">
    </cc1:CalendarExtender>
    <cc1:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="True" PopupButtonID="imgbtnFechaVuelo"
        TargetControlID="txtFechaVuelo" Format="dd/MM/yyyy">
    </cc1:CalendarExtender>
    <asp:CompareValidator ID="cvFiltroFechas" runat="server" ControlToCompare="txtFechaLecturaFin"
        ControlToValidate="txtFechaLecturaIni" Display="None" ErrorMessage="Filtro de fechas invalido"
        Operator="LessThanEqual" Type="Date"></asp:CompareValidator>
    <cc1:ValidatorCalloutExtender ID="vceFiltroFechas" runat="server" TargetControlID="cvFiltroFechas">
    </cc1:ValidatorCalloutExtender>
    <asp:CompareValidator ID="cvFiltroFechaHasta" runat="server" ControlToCompare="txtFechaLecturaIni"
        ControlToValidate="txtFechaLecturaFin" Display="None" ErrorMessage="Filtro de fechas invalido"
        Operator="GreaterThanEqual" Type="Date"></asp:CompareValidator>
    <cc1:ValidatorCalloutExtender ID="vceFiltroFechaHasta" runat="server" TargetControlID="cvFiltroFechaHasta">
    </cc1:ValidatorCalloutExtender>
    <asp:Label ID="txtOrdenacion" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="txtColumna" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="txtValorMaximoGrilla" runat="server" Visible="False"></asp:Label>
    </form>
</body>
</html>
