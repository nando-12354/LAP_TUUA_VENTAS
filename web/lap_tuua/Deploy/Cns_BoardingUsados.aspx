<%@ page language="C#" autoeventwireup="true" inherits="Cns_BoardingUsados, App_Web_jlql8yfo" responseencoding="utf-8" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Src="UserControl/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="uc1" %>
<%@ Register Src="UserControl/PiePagina.ascx" TagName="PiePagina" TagPrefix="uc2" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="UserControl/ConsDetTicket.ascx" TagName="ConsDetTicket" TagPrefix="uc3" %>
<%@ Register Src="UserControl/CnsDetBoarding.ascx" TagName="CnsDetBoarding" TagPrefix="uc4" %>
<%@ Register Src="UserControl/ResumenTicketBPUsados.ascx" TagName="ResumenTicketBPUsados"
    TagPrefix="uc6" %>
<%@ Register Assembly="PaginGridView" Namespace="Pagin.Web.Control" TagPrefix="cc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LAP - Consulta - Ticket o Boarding Pass Usados</title>
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
    <meta name="keypage" content="cns_boardingusados" />
    <!-- #INCLUDE file="javascript/ValidarFecha.js" -->
    <!-- #INCLUDE file="javascript/KeyPress.js" -->
    <!-- #INCLUDE file="javascript/validarteclaF5.js" -->
    <!-- #INCLUDE file="javascript/MantenerSesion.js" -->
    <link href="css/Style.css" rel="stylesheet" type="text/css" />
    <link href="../css/Style.css" rel="stylesheet" type="text/css" />
    
    <script language="JavaScript" type="text/javascript">
        
        function imgPrint_onclick() {

            var sDesde = document.getElementById("txtDesde").value;
            var sHasta = document.getElementById("txtHasta").value;
            var idHoraDesde = document.getElementById("txtHoraDesde").value;
            var idHoraHasta = document.getElementById("txtHoraHasta").value;
            var idCompania = document.getElementById("ddlCompania").value;
            var idTipoVuelo = document.getElementById("ddlTipoVuelo").value;
            var idNumVuelo = document.getElementById("txtNumVuelo").value;
            var idTipoPasajero = document.getElementById("ddlTipoPasajero").value;
            var idTipoDocumento = document.getElementById("ddlTipoDocumento").value;
            var idTipoTrasbordo = document.getElementById("ddlTipoTrasbordo").value;
            var idFechaVuelo = document.getElementById("txtFechaVuelo").value;
            var idEstado = document.getElementById("ddlEstado").value;

            //Descripciones
            var idDscD = (idTipoDocumento != "0") ? document.getElementById("ddlTipoDocumento").options[document.getElementById("ddlTipoDocumento").selectedIndex].text : "";
            var idDscC = (idCompania != "0") ? document.getElementById("ddlCompania").options[document.getElementById("ddlCompania").selectedIndex].text : "";
            var idDscE = (idEstado != "0") ? document.getElementById("ddlEstado").options[document.getElementById("ddlEstado").selectedIndex].text : "";
            var idDscP = (idTipoPasajero != "0") ? document.getElementById("ddlTipoPasajero").options[document.getElementById("ddlTipoPasajero").selectedIndex].text : "";
            var idDscV = (idTipoVuelo != "0") ? document.getElementById("ddlTipoVuelo").options[document.getElementById("ddlTipoVuelo").selectedIndex].text : "";
            var idDscT = (idTipoTrasbordo != "0") ? document.getElementById("ddlTipoTrasbordo").options[document.getElementById("ddlTipoTrasbordo").selectedIndex].text : "";

            var w = 900 + 32;
            var h = 500 + 96;
            var wleft = (screen.width - w) / 2;
            var wtop = (screen.height - h) / 2;

            var ventimp = window.open("ReporteCNS/rptTicketBoardingUsados.aspx" + "?sDesde=" + sDesde + "&sHasta=" + sHasta + "&idCompania=" + idCompania + "&idTipoVuelo=" + idTipoVuelo + "&idNumVuelo=" + idNumVuelo
                                            + "&idTipoPasajero=" + idTipoPasajero + "&idTipoDocumento=" + idTipoDocumento + "&idTipoTrasbordo=" + idTipoTrasbordo
                                            + "&idFechaVuelo=" + idFechaVuelo + "&idEstado=" + idEstado
                                            + "&idHoraDesde=" + idHoraDesde
                                            + "&idHoraHasta=" + idHoraHasta
                                            + "&idDscD=" + idDscD
                                            + "&idDscC=" + idDscC
                                            + "&idDscE=" + idDscE
                                            + "&idDscP=" + idDscP
                                            + "&idDscV=" + idDscV
                                            + "&idDscT=" + idDscT
                                            , "mywindow", "location=0,status=0,scrollbars=1,menubar=0,width=900,height=500,left=" + wleft + ",top=" + wtop);
            ventimp.moveTo(wleft, wtop);
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
        
        function validarCampos() {
            //Clean screen
            document.getElementById('lblMensajeError').innerHTML = "";
            document.getElementById('lblMensajeErrorData').innerHTML = "";
            document.getElementById('lblTotal').innerHTML = "";

            document.getElementById("hlinkResumen").style.display = "none";
            cleanGrilla();

            if (isValidoRangoFecha(document.getElementById('txtDesde').value,
                                   document.getElementById('txtHoraDesde').value,
                                   document.getElementById('txtHasta').value,
                                   document.getElementById('txtHoraHasta').value
                                   )) {
                //document.getElementById('lblMensajeError').style.visibility = 'hidden';
                //document.getElementById('lblMensajeError').innerHTML = "";
                return true;
            } else {
                //document.getElementById('divData').style.visibility = 'hidden';
                //document.getElementById('lblMensajeError').style.visibility = 'visible';
                document.getElementById('lblMensajeError').innerHTML = "Error. Rango de Fechas ingresado es inválido";
                return false;
            }

        }
        
        function ControlarDropDownList(obj) {
//            if (obj[obj.selectedIndex].value == "B") {
//                //document.getElementById("ddlPersona").disabled = true;
////                document.getElementById("ddlTipoTicket").disabled = false;
//                document.getElementById("ddlEstado").disabled = false;
//                            
//            }

            if (obj[obj.selectedIndex].value == "T") {
                 document.getElementById("ddlEstado").disabled = false;
            }
            if (obj[obj.selectedIndex].value == "0") {
                 document.getElementById("ddlEstado").disabled = false;
            }
        }

        function cleanGrilla() {
            if (document.getElementById("grvTicketUsados") != null) {
                document.getElementById("grvTicketUsados").style.display = "none";
            }
        }        

    </script>

    <style type="text/css">
        .style1
        {
            width: 3%;
        }
    </style>

    </head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="360">
        </asp:ScriptManager>
        <table cellpadding="0" cellspacing="0" align="center" class="TamanoTabla">
            <tr>
                <!-- HEADER -->
                <td align="center">
                    <uc1:CabeceraMenu ID="CabeceraMenu1" runat="server" />                    
                </td>
            </tr>
            <tr class="formularioTitulo2">
                <td>
                    <!-- FILTER ZONE -->
                    <table cellpadding="0" cellspacing="0" class="EspacioSubTablaPrincipal">
                        <tr>
                            <td align="left" style="height: 30px; width: 100%">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <!-- TITLE -->
                                        
                                        <td colspan="17" style="height: 20px;">
                                            <asp:Label ID="lblDesde0" runat="server" CssClass="TextoFiltro" Font-Bold="True">Fecha de Uso:</asp:Label>
                                        </td>
                                        
                                    </tr>
                                    <tr>
                                        <!-- FIRST ROW FILTER -->
                                        <td style="width: 4%;">
                                            <asp:Label ID="lblDesde" runat="server" CssClass="TextoFiltro"></asp:Label>
                                        </td>
                                        <td rowspan="2" style="width: 75px;">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtDesde" runat="server" Width="72px" CssClass="textbox" Height="16px"
                                                            MaxLength="10" onfocus="this.blur();" onkeypress="JavaScript: window.event.keyCode = 0;"
                                                            BackColor="#E4E2DC">
                                                        </asp:TextBox>
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
                                        <td rowspan="2" style="width: 60px;">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtHoraDesde" runat="server" CssClass="textbox" MaxLength="8" onBlur="JavaScript:CheckTime(this)"
                                                            ReadOnly="false" Width="56px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <asp:Label ID="lblHoraDesde0" runat="server" CssClass="TextoEtiqueta" Text="( hh:mm:ss )"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="width: 1%;">
                                        </td>
                                        <td style="width: 5%;">
                                            <asp:Label ID="lblTipoDocumento" runat="server" CssClass="TextoFiltro" Width="100%"></asp:Label>
                                        </td>
                                        <%--<td style="width: 24%;" colspan="4">
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <table cellpadding="0" cellspacing="0" style="width: 100%;">
                                                        <tr>
                                                            <td style="width: 40%;">
                                                                <asp:DropDownList ID="ddlTipoDocumento" runat="server" Width="100%" CssClass="combo2" 
                                                                onChange="javascript:ControlarDropDownList(this);" OnSelectedIndexChanged="ddlTipoDocumento_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td style="width: 4%;">
                                                                &nbsp;
                                                            </td>
                                                            <td style="width: 16%;">
                                                                <asp:Label ID="lblEstado" runat="server" CssClass="TextoFiltro"></asp:Label>
                                                            </td>
                                                            <td style="width: 40%;">
                                                                <asp:DropDownList ID="ddlEstado" runat="server" Width="100%" CssClass="combo2">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ContentTemplate>
                                                
                                               <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="ddlTipoDocumento" EventName="SelectedIndexChanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </td>--%>
                                        <td style="width: 27%">
                                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                <table cellpadding="0" cellspacing="0">
                                                                    <tr>
                                                                        <td style="width: 50%;">
                                                                            <asp:DropDownList ID="ddlTipoDocumento" runat="server" Width="100%" CssClass="combo2"
                                                                             onChange="javascript:ControlarDropDownList(this);"  AutoPostBack="True" OnSelectedIndexChanged="ddlTipoDoc_SelectedIndexChanged">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td style="width: 15%;">
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblEstado" runat="server" CssClass="TextoFiltro" Width="100%" ></asp:Label>
                                                                        </td>
                                                                        <td style="width: 35%;">
                                                                            <asp:DropDownList ID="ddlEstado" runat="server" Width="100px" CssClass="combo2">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="ddlTipoDocumento" EventName="SelectedIndexChanged" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>
                                         </td>
                                        <td style="width: 1%;">
                                        </td>
                                        <td style="width: 5%;">
                                            <asp:Label ID="lblCompania" runat="server" CssClass="TextoFiltro"></asp:Label>
                                        </td>
                                        <td style="width: 20%;" colspan="5">
                                            <asp:DropDownList ID="ddlCompania" runat="server" Width="100%" CssClass="combo2">
                                            </asp:DropDownList>
                                        </td>
                                        
                                        <td class="style1">
                                            <asp:Label ID="lblTipoPasajero" runat="server" CssClass="TextoFiltro"></asp:Label>
                                        </td>
                                        <td style="width: 8%;">
                                            <asp:DropDownList ID="ddlTipoPasajero" runat="server" Width="100%" CssClass="combo2">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 20%;" >
                                            </td>
                                            <td>
                                             <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="return validarExcel();"
                                             OnClick="lbExportar_Click">[Exportar a Excel]</asp:LinkButton>
                                            </td>
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
                                        <!-- SECOND ROW FILTER -->
                                        <td>
                                            <asp:Label ID="lblHasta" runat="server" CssClass="TextoFiltro"></asp:Label>
                                        </td>
                                        <td rowspan="2">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtHasta" runat="server" Width="72px" CssClass="textbox" Height="16px"
                                                            MaxLength="10" onfocus="this.blur();" onkeypress="JavaScript: window.event.keyCode = 0;"
                                                            BackColor="#E4E2DC"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <asp:Label ID="Label1" runat="server" CssClass="TextoEtiqueta" Text="( dd/mm/yyyy )"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="imgbtnCalendar2" ImageUrl="~/Imagenes/Calendar.bmp" runat="server"
                                                Width="22px" Height="22px" />
                                        </td>
                                        <td rowspan="2">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtHoraHasta" runat="server" CssClass="textbox" MaxLength="8" onkeypress="JavaScript: Tecla('Time');"
                                                            onBlur="JavaScript:CheckTime(this)" ReadOnly="false" Width="56px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <asp:Label ID="Label2" runat="server" CssClass="TextoEtiqueta" Text="( hh:mm:ss )"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblTipoVuelo" runat="server" CssClass="TextoFiltro"></asp:Label>
                                        </td>
                                        <td style="width: 10%;">
                                            <asp:DropDownList ID="ddlTipoVuelo" runat="server" Width="100%" CssClass="combo2">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 1%;">
                                        </td>
                                        <td style="width: 4%;">
                                            <asp:Label ID="lblTipoTrasbordo" runat="server" CssClass="TextoFiltro"></asp:Label>
                                        </td>
                                        <td style="width: 10%;">
                                            <asp:DropDownList ID="ddlTipoTrasbordo" runat="server" Width="100%" CssClass="combo2">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblNumVuelo" runat="server" CssClass="TextoFiltro" Width="100%"></asp:Label>
                                        </td>
                                        <td width="80px">
                                            <asp:TextBox ID="txtNumVuelo" runat="server" Width="80px" CssClass="textbox" Height="16px"
                                                MaxLength="10" onkeypress="JavaScript: Tecla('Alphanumeric');"></asp:TextBox>
                                        </td>
                                        <td style="width: 1%;">
                                        </td>
                                        <td class="style1">
                                            <asp:Label ID="lblFechaVuelo" runat="server" CssClass="TextoFiltro"></asp:Label>
                                        </td>
                                        <td colspan="2" >
                                            
                                                  <asp:TextBox ID="txtFechaVuelo" runat="server" Width="72px" CssClass="textbox" Height="16px"
                                                            MaxLength="10" onfocus="this.blur();" onkeypress="JavaScript: window.event.keyCode = 0;"
                                                            BackColor="#E4E2DC"></asp:TextBox>
                                                  
                                                    
                                                  &nbsp;<asp:ImageButton ID="imgbtnCalendar3" ImageUrl="~/Imagenes/Calendar.bmp" runat="server"
                                                Width="22px" Height="22px" />
                                                  <br />
&nbsp;<asp:Label ID="Label3" runat="server" CssClass="TextoEtiqueta" Text="( dd/mm/yyyy )"></asp:Label>
                                                 
                                                 <cc1:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" PopupButtonID="imgbtnCalendar3"
                                                    TargetControlID="txtFechaVuelo">
                                                </cc1:CalendarExtender>
                                                                                             
                                        </td>
                                        <td align="right">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                <td>
                                                    &nbsp;</td>
                                                    <td>
                                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                            
                                                            <br />  
                                                                <a href="" id="lnkHabilitar" runat="server" onclick="javascript:imgPrint_onclick();"
                                                                    style="cursor: hand;"><b>
                                                                        <asp:Label ID="lblImprimir" runat="server">Imprimir</asp:Label>
                                                                    </b></a>&nbsp;&nbsp;&nbsp;
                                                                    <br>
                                                                    </br>
                                                                <asp:Button ID="btnConsultar" runat="server" OnClientClick="return validarCampos()"
                                                                    OnClick="btnConsultar_Click" CssClass="Boton" Style="cursor: hand;" />&nbsp;&nbsp;&nbsp;
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                        <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="UpdatePanel3" runat="server">
                                                            <ProgressTemplate>
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
                <td>
                    <!-- DATA RESULT ZONE -->
                    <table border="0" cellpadding="0" cellspacing="0" class="EspacioSubTablaPrincipal">
                        <tr>
                            <td class="SpacingGrid">
                            </td>
                            <td class="CenterGrid">
                                <div id="divData" class="divSizeCustom">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <cc2:PagingGridView ID="grvTicketUsados" runat="server" AutoGenerateColumns="False"
                                                BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
                                                CellPadding="3" GridLines="Vertical" Width="100%" AllowSorting="True" OnPageIndexChanging="grvTicketUsados_PageIndexChanging"
                                                OnSorting="grvTicketUsados_Sorting" CssClass="grilla" OnRowCommand="grvTicketUsados_RowCommand"
                                                OnRowDataBound="grvTicketUsados_RowDataBound">
                                                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Nro. Documento" SortExpression="Codigo">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="codTipoDocumento" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                                                CommandName="ShowTipoDocumento" Text='<%# Eval("Codigo") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="Secuencial" DataField="Correlativo" SortExpression="Num_Serie" />
                                                    <asp:BoundField HeaderText="Tipo Documento" DataField="Dsc_Documento" SortExpression="Dsc_Documento" />
                                                    <asp:BoundField HeaderText="Destino" DataField="Dsc_Destino" SortExpression="Dsc_Destino" />
                                                    <asp:BoundField HeaderText="Modalidad Venta" DataField="Nom_Modalidad" SortExpression="Nom_Modalidad" />
                                                    <asp:BoundField HeaderText="Aerolínea" DataField="Dsc_Compania" SortExpression="Dsc_Compania" />
                                                    <asp:BoundField HeaderText="Nro. Vuelo" DataField="Dsc_Num_Vuelo" SortExpression="Dsc_Num_Vuelo" />
                                                    <asp:TemplateField HeaderText="Fecha Vuelo" SortExpression="Fch_Vuelo">
                                                    <ItemTemplate>
                                                            <asp:Label ID="lblFchVuelo" runat="server" Text='<%# LAP.TUUA.UTIL.Fecha.convertSQLToFecha3(Convert.ToString(Eval("Fch_Vuelo"))) %> '></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Fecha Uso" SortExpression="Fch_Uso">
                                                    
                                                    <%--<asp:TemplateField HeaderText="Fecha Uso" SortExpression="Log_Fecha_Mod,Log_Hora_Mod">--%>
                                                        <ItemTemplate>
                                                        <asp:Label ID="lblFchUso" runat="server" Text='<%# LAP.TUUA.UTIL.Fecha.convertSQLToFecha2(Convert.ToString(Eval("Fch_Uso"))) %> '></asp:Label>
                                                            <%--<asp:Label ID="lblFchUso" runat="server" Text='<%# LAP.TUUA.UTIL.Fecha.convertSQLToFecha(Convert.ToString(Eval("Log_Fecha_Mod")),Convert.ToString(Eval("Log_Hora_Mod"))) %> '></asp:Label>--%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="Nro. Asiento" DataField="NroAsiento" SortExpression="NroAsiento" />
                                                    <asp:BoundField HeaderText="Tipo Vuelo" DataField="Dsc_TipoVuelo" SortExpression="Dsc_TipoVuelo"
                                                        NullDisplayText=" - " />
                                                    <asp:BoundField HeaderText="Tipo Persona" DataField="Dsc_TipoPasajero" SortExpression="Dsc_TipoPasajero"
                                                        NullDisplayText=" - " />
                                                    <asp:BoundField HeaderText="Tipo Trasbordo" DataField="Dsc_Trasbordo" SortExpression="Dsc_Trasbordo" />
                                                    <asp:BoundField HeaderText="Estado Actual" DataField="Dsc_Estado" SortExpression="Estado" />
                                                    <asp:BoundField DataField="Tip_Estado" Visible="false" />
                                                    <asp:BoundField HeaderText="Secuencia" DataField="Num_Secuencial" SortExpression="Num_Secuencial" />
                                                  
                                                    <asp:TemplateField HeaderText="Asociado" SortExpression="Flg_Tipo_Bcbp">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEstadoAsociacion" runat="server" Text='<%# ( /* Eval("Flg_Tipo_Bcbp")!=DBNull.Value && */ Int32.Parse(Eval("Flg_Tipo_Bcbp").ToString())==1) ? "Si" : "No"  %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField Visible="False">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Num_Secuencial_Bcbp" runat="server" Text='<%# Eval("Num_Secuencial_Bcbp") %>' />
                                                            <asp:Label ID="Num_Secuencial_Bcbp_Rel" runat="server" Text='<%# Eval("Num_Secuencial_Bcbp_Rel")%>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <SelectedRowStyle CssClass="grillaFila" />
                                                <PagerStyle CssClass="grillaPaginacion" />
                                                <HeaderStyle CssClass="grillaCabecera" />
                                                <AlternatingRowStyle CssClass="grillaFila" />
                                            </cc2:PagingGridView>
                                            <br />
                                            <!-- 
                                        <CR:CrystalReportViewer ID="crvResTipoDocumento" runat="server" AutoDataBind="true"
                                            DisplayGroupTree="False" EnableDatabaseLogonPrompt="False" EnableParameterPrompt="False"
                                            HasCrystalLogo="False" PrintMode="ActiveX" EnableTheming="False" HasGotoPageButton="False"
                                            HasSearchButton="False" HasToggleGroupTreeButton="False" HasViewList="False"
                                            SeparatePages="False" HasRefreshButton="False" HasPrintButton="False" HasPageNavigationButtons="False"
                                            HasExportButton="False" HasDrillUpButton="False" HasZoomFactorList="False" DisplayToolbar="False" />
                                        <br /> -->
                                            <asp:Label ID="lblMensajeError" runat="server" CssClass="mensaje"></asp:Label>
                                            <asp:Label ID="lblMensajeErrorData" runat="server" CssClass="msgMensaje"></asp:Label>
                                            <asp:Label ID="lblTotal" runat="server" CssClass="TextoCampo"></asp:Label>
                                            &nbsp;&nbsp;<a href="" id="hlinkResumen" runat="server" onserverclick="hlinkResumen_Click"><b>Ver
                                                Resumen</b></a>
                                                <asp:HiddenField ID="lblTotalRows" runat="server" />
                                        <asp:HiddenField ID="lblMaxRegistros" runat="server" />
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnConsultar" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                    <asp:UpdateProgress ID="UpdateProgress2" AssociatedUpdatePanelID="UpdatePanel1" runat="server"
                                        DisplayAfter="10">
                                        <ProgressTemplate>
                                            <div id="processMessage">
                                                &nbsp;&nbsp;&nbsp;Procesando...<br />
                                                <br />
                                                <img alt="Loading" src="Imagenes/ajax-loader.gif" />
                                            </div>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                    <uc3:ConsDetTicket ID="ConsDetTicket1" runat="server" />
                                    <uc4:CnsDetBoarding ID="CnsDetBoarding1" runat="server" />
                                </div>
                            </td>
                            <td class="SpacingGrid">
                            </td>
                        </tr>
                        <tr>
                            <td class="SpacingGrid">
                                &nbsp;
                            </td>
                            <td class="CenterGrid">
                            </td>
                            <td class="SpacingGrid">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                    <%--<asp:Panel ID="Panel1" runat="server">--%>
                    <uc6:ResumenTicketBPUsados ID="ResumenTicketBPUsados1" runat="server" />
                   <%-- </asp:Panel>--%>
                    <uc2:PiePagina ID="PiePagina1" runat="server" />
                    <br />
                </td>
            </tr>
        </table>
    </div>
    <!--Declaracion de Calendarios -->
    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" PopupButtonID="imgbtnCalendar1"
        TargetControlID="txtDesde">
    </cc1:CalendarExtender>
    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" PopupButtonID="imgbtnCalendar2"
        TargetControlID="txtHasta">
    </cc1:CalendarExtender>
    
    <!--Declaracion Control Hora -->
    <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99:99:99" TargetControlID="txtHoraDesde"
        ClearMaskOnLostFocus="False" MaskType="Time" UserTimeFormat="TwentyFourHour"
        CultureName="es-ES" PromptCharacter="_" AutoComplete="False">
    </cc1:MaskedEditExtender>
    <cc1:MaskedEditExtender ID="mee_txtHoraHasta" runat="server" Mask="99:99:99" TargetControlID="txtHoraHasta"
        ClearMaskOnLostFocus="False" MaskType="Time" UserTimeFormat="TwentyFourHour"
        CultureName="es-ES" PromptCharacter="_" AutoComplete="False">
    </cc1:MaskedEditExtender>
    <asp:Label ID="txtOrdenacion" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="txtColumna" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="txtValorMaximoGrilla" runat="server" Visible="False"></asp:Label>
    </form>
</body>
</html>
