<%@ page language="C#" autoeventwireup="true" inherits="Ope_ExtornarTicket, App_Web_tx1el90t" %>

<%@ Register Src="UserControl/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="uc1" %>
<%@ Register Src="UserControl/PiePagina.ascx" TagName="PiePagina" TagPrefix="uc2" %>
<%@ Register Src="UserControl/OKMessageBox.ascx" TagName="OKMessageBox" TagPrefix="uc3" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="System.Web.Extensions" Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="PaginGridView" Namespace="Pagin.Web.Control" TagPrefix="cc1" %>
<%@ Register Src="UserControl/ConsDetTicket.ascx" TagName="ConsDetTicket" TagPrefix="uc5" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Extornar Tickets</title>
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
    <link href="css/Style.css" rel="stylesheet" type="text/css" />
    <!-- #INCLUDE file="javascript/KeyPress.js" -->
    <!-- #INCLUDE file="javascript/Functions.js" -->
    <!-- #INCLUDE file="javascript/validarteclaF5.js" -->
    <!-- #INCLUDE file="javascript/MantenerSesion.js" -->

    <script type="text/javascript" language="JavaScript">
        var NumTotal = 0;

        function SetNumTotal() {
            try {
                var frm = document.forms[0];
                frm.hdNumSelTotal.value = "0";
            }
            catch (Exception) {
            }
        }
            
        function SetCheckBoxHeaderGrilla(control) {
            try {
                var frm = document.forms[0];
                if (control.checked) {
                    //var numSelTotal = parseInt(frm.hdNumSelTotal.value);
                    var numSelTotal = parseInt(document.getElementById("hdNumSelTotal").value);
                    //var numSelTotal = NumTotal;
                    var j = 1;
                    for (i = 0; i < frm.elements.length; i++) {
                        if (frm.elements[i].name.indexOf('chkSeleccionar') >= 0 && frm.elements[i].type == "checkbox") {
                            if (!frm.elements[i].checked) {
                                j = 0;
                                break;
                            }
                        }
                    }
                    if (j == 1) {
                        frm.chkAll.checked = true;
                    }
                } else {
                    frm.chkAll.checked = false;

                }

                if (control.checked) {
                    //var numSelTotal = parseInt(frm.hdNumSelTotal.value);
                    var numSelTotal = parseInt(document.getElementById("hdNumSelTotal").value);
                    // var numSelTotal = NumTotal;
                    numSelTotal += 1;
                    frm.hdNumSelTotal.value = numSelTotal;
                }
                else {
                    //var numSelTotal = parseInt(frm.hdNumSelTotal.value);
                    var numSelTotal = parseInt(document.getElementById("hdNumSelTotal").value);
                    //var numSelTotal = NumTotal;
                    numSelTotal -= 1;
                    frm.hdNumSelTotal.value = numSelTotal;
                }
                NumTotal = parseInt(document.getElementById("hdNumSelTotal").value); //parseInt(frm.hdNumSelTotal.value);
                document.getElementById('lblTxtSeleccionados').innerHTML = document.getElementById("hdNumSelTotal").value; //frm.hdNumSelTotal.value;

            } catch (Exception) {
            }

        }

        function CheckBoxHeaderGrilla() {
            try {
                var frm = document.forms[0];

                if (frm.chkAll != null) {//Esto para cuando oculte el pnlPrincipal
                    var j = 1;
                    var count = 0;
                    for (i = 0; i < frm.elements.length; i++) {
                        if (frm.elements[i].name.indexOf('chkSeleccionar') >= 0 && frm.elements[i].type == "checkbox") {
                            count = count + 1;
                            if (!frm.elements[i].checked) {
                                j = 0;
                                break;
                            }
                        }
                    }
                    if (j == 1 && count > 0) {
                        frm.chkAll.checked = true;
                    }
                    else {
                        frm.chkAll.checked = false;
                    }

                }

                //frm.hdNumSelTotal.value = "0";

            } catch (Exception) {
            }
        }
           
        function SetCheck(control) {
            try {

                var frm = document.forms[0];
                var checkbox;
                var observacion;
                //var numSelTotal = parseInt(frm.hdNumSelTotal.value);
                var numSelTotal = NumTotal;
                if (control.checked) {
                    for (i = 0; i < frm.elements.length; i++) {
                        if (frm.elements[i].type == "checkbox" && frm.elements[i].name != 'chkAll' && !frm.elements[i].checked) {//
                            checkbox = frm.elements[i];
                            numSelTotal += 1;
                            frm.hdNumSelTotal.value = numSelTotal;
                            checkbox.checked = true;
                        }
                    }
                }
                else {
                    for (i = 0; i < frm.elements.length; i++) {
                        if (frm.elements[i].type == "checkbox" && frm.elements[i].name != 'chkAll' && frm.elements[i].checked) {
                            checkbox = frm.elements[i];
                            numSelTotal -= 1;
                            checkbox.checked = false;
                        }
                    }
                }
                frm.hdNumSelTotal.value = numSelTotal;
                NumTotal = numSelTotal;
                document.getElementById('lblTxtSeleccionados').innerHTML = frm.hdNumSelTotal.value;
            } catch (Exception) {
            }

        }

        function habilitarOpciones() {
            //            if (document.getElementById("rbTicket").checked == true) {
            //                document.getElementById("rbRango").checked = false;
            //                document.getElementById("txtNroIni").disabled = false;
            //                document.getElementById("txtNroFin").disabled = false;
            //                document.getElementById("txtNroIni").style.backgroundColor = '#FFFFFF';
            //                document.getElementById("txtNroFin").style.backgroundColor = '#FFFFFF';
            //                document.getElementById("txtNumTicket").style.backgroundColor = '#CCCCCC';
            //                document.getElementById("txtNumTicket").value = '';
            //                document.getElementById("txtNumTicket").disabled = true;
            //            }

            if (document.getElementById("rbTicket").checked == true) {
                document.getElementById("rbRango").checked = false;
                document.getElementById("txtNumTicket").disabled = false;
                document.getElementById("txtNumTicket").style.backgroundColor = '#FFFFFF';
                document.getElementById("txtNroIni").style.backgroundColor = '#CCCCCC';
                document.getElementById("txtNroFin").style.backgroundColor = '#CCCCCC';
                document.getElementById("txtNroIni").disabled = true;
                document.getElementById("txtNroFin").disabled = true;
                document.getElementById("txtNroIni").value = '';
                document.getElementById("txtNroFin").value = '';
            }
            else {
                document.getElementById("rbTicket").checked = false;
                document.getElementById("txtNroIni").disabled = false;
                document.getElementById("txtNroFin").disabled = false;
                document.getElementById("txtNroIni").style.backgroundColor = '#FFFFFF';
                document.getElementById("txtNroFin").style.backgroundColor = '#FFFFFF';
                document.getElementById("txtNumTicket").style.backgroundColor = '#CCCCCC';
                document.getElementById("txtNumTicket").value = '';
                document.getElementById("txtNumTicket").disabled = true;
            }
        }
    </script>

    <style type="text/css">
        .style1
        {
            height: 12px;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="600">
    </asp:ScriptManager>
    <div style="background-image: url(Imagenes/back.gif)">
        <table align="center" border="0" cellpadding="0" cellspacing="0" class="TamanoTabla">
            <tr>
                <td class="Espacio1FilaTabla" colspan="2" style="height: 11px">
                    <uc1:CabeceraMenu ID="CabeceraMenu2" runat="server" />
                </td>
            </tr>
            <tr class="formularioTitulo">
                <td align="right" style="text-align: left">
                    <img alt="" src="Imagenes/flecha_back.png" onclick="JavaScript: FnVolver();" />
                </td>
                <td align="right">
                    <asp:Button ID="btnExtornar" runat="server" CssClass="Boton" OnClick="btnExtornar_Click" />
                    <cc2:ConfirmButtonExtender ID="btnExtornar_ConfirmButtonExtender" runat="server"
                        ConfirmText="" Enabled="True" TargetControlID="btnExtornar">
                    </cc2:ConfirmButtonExtender>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <hr class="EspacioLinea" color="#0099cc" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <table cellpadding="0" cellspacing="0" width="100%" class="EspacioSubTablaPrincipal">
                                <tr class="formularioTitulo">
                                    <td style="width: 4%">
                                        &nbsp;
                                    </td>
                                    <td style="width: 12%; height: 30px" valign="top">
                                        <asp:RadioButton ID="rbTicket" runat="server" CssClass="TextoEtiquetaBold" GroupName="rangos"
                                            onClick="habilitarOpciones();" Text="Por Numero de Ticket" />
                                    </td>
                                    <td style="width: 5%">
                                        &nbsp;
                                    </td>
                                    <td style="width: 15%">
                                        <asp:TextBox ID="txtNumTicket" runat="server" BackColor="#CCCCCC" CssClass="textbox"
                                            Enabled="False" MaxLength="16" onblur="gDecimal(this)" onkeypress="JavaScript: Tecla('Integer');"></asp:TextBox>
                                    </td>
                                    <td style="width: 5%">
                                        &nbsp;
                                    </td>
                                    <td style="width: 20%">
                                        &nbsp;
                                    </td>
                                    <td style="width: 29%">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr class="formularioTitulo">
                                    <td style="width: 4%">
                                        &nbsp;
                                    </td>
                                    <td style="width: 12%; height: 30px" valign="top">
                                        <asp:RadioButton ID="rbRango" runat="server" Checked="True" CssClass="TextoEtiquetaBold"
                                            GroupName="rangos" onClick="habilitarOpciones();" Text="Por Rango de Tickets" />
                                    </td>
                                    <td style="width: 5%" align="right">
                                        <asp:Label ID="lblTicketDesde" runat="server" CssClass="TextoFiltro">Del:&nbsp;&nbsp;</asp:Label>
                                    </td>
                                    <td style="width: 15%">
                                        <asp:TextBox ID="txtNroIni" runat="server" CssClass="textbox" MaxLength="16" onblur="gDecimal(this)"
                                            onkeypress="JavaScript: Tecla('Integer');"></asp:TextBox>
                                    </td>
                                    <td style="width: 5%" align="right">
                                        <asp:Label ID="lblTicketHasta" runat="server" CssClass="TextoFiltro">Al:&nbsp;&nbsp;</asp:Label>
                                    </td>
                                    <td style="width: 20%">
                                        <asp:TextBox ID="txtNroFin" runat="server" CssClass="textbox" MaxLength="16" onblur="gDecimal(this)"
                                            onkeypress="JavaScript: Tecla('Integer');"></asp:TextBox>
                                    </td>
                                    <td style="width: 29%">
                                        <asp:Button ID="btnConsultar" runat="server" CssClass="Boton" OnClick="btnConsultar_Click"
                                            Text="Consultar" />&nbsp;&nbsp;
                                        <asp:Label ID="lblError" runat="server" Font-Size="11px" ForeColor="Red"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="7">
                                        <hr color="#0099cc" style="width: 100%; height: 0px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style1">
                                        &nbsp;
                                    </td>
                                    <td class="style1">
                                        &nbsp;
                                    </td>
                                    <td class="style1">
                                        &nbsp;
                                    </td>
                                    <td class="style1">
                                        &nbsp;
                                    </td>
                                    <td class="style1">
                                        &nbsp;
                                    </td>
                                    <td class="style1">
                                        &nbsp;
                                    </td>
                                    <td class="style1">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td colspan="6">
                                        <asp:Label ID="lblMotivo" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:TextBox ID="txtMotivo" runat="server" CssClass="textbox" MaxLength="250" Width="272px"></asp:TextBox>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="lblMensajeError" runat="server" CssClass="mensaje" Width="427px"></asp:Label>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td colspan="6">
                                        &nbsp;
                                        <asp:Label ID="lblGrilla" runat="server" CssClass="msgMensaje"></asp:Label>
                                        <cc1:PagingGridView ID="grvTicket" runat="server" AutoGenerateColumns="False" BackColor="White"
                                            BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%"
                                            AllowPaging="True" CssClass="grilla" AllowSorting="True" VirtualItemCount="-1"
                                            RowStyle-HorizontalAlign="Center" RowStyle-VerticalAlign="Middle" OnPageIndexChanging="grvTicket_PageIndexChanging"
                                            OnSorting="grvTicket_Sorting" OnRowDataBound="grvTicket_RowDataBound" 
                                            onrowcommand="grvTicket_RowCommand" GroupingDepth="0" DataKeyNames="Imp_Precio_Dol">
                                            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                            <PagerSettings Mode="NumericFirstLast" />
                                            <RowStyle HorizontalAlign="Center" VerticalAlign="Middle"></RowStyle>
                                            <Columns>
                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <%# Eval("rownum")%></ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Dsc_Tip_Vuelo" HeaderText="Tipo Ticket" SortExpression="Dsc_Tip_Vuelo" />
                                                <asp:BoundField DataField="Dsc_Compania" HeaderText="Compania" SortExpression="Dsc_Compania" />
                                                <asp:BoundField DataField="Dsc_Num_Vuelo" HeaderText="Num. Vuelo" SortExpression="Dsc_Num_Vuelo" />
                                                <asp:TemplateField HeaderText="Fecha Vuelo" SortExpression="Fch_Vuelo">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFechaVuelo" runat="server" Text='<%# LAP.TUUA.UTIL.Fecha.convertSQLToFecha(Convert.ToString(Eval("Fch_Vuelo")),null) %> '></asp:Label></ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Num. Ticket" SortExpression="Cod_Numero_Ticket">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="codTicket" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                                            CommandName="ShowTicket" Text='<%# Eval("Cod_Numero_Ticket") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Fecha Proceso" SortExpression="FHCreacion">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFechaProcesp" runat="server" Text='<%# LAP.TUUA.UTIL.Fecha.formatoFechaExcel(Convert.ToString(Eval("FHCreacion"))) %> '></asp:Label></ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Estado" HeaderText="Estado" SortExpression="Estado" />
                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkSeleccionar" runat="server" onclick="Javascript:SetCheckBoxHeaderGrilla(this); " />
                                                    </ItemTemplate>
                                                    <HeaderTemplate>
                                                        <input name="chkAll" onclick="JavaScript:SetCheck(this);" type="checkbox" />
                                                        &nbsp;
                                                    </HeaderTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Imp_Precio_Dol" Visible="False" />
                                            </Columns>
                                            <SelectedRowStyle CssClass="grillaFila" />
                                            <PagerStyle CssClass="grillaPaginacion" />
                                            <HeaderStyle CssClass="grillaCabecera" />
                                            <AlternatingRowStyle CssClass="grillaFila" />
                                        </cc1:PagingGridView>
                                    </td>
                                    <td class="SpacingGrid" colspan="1" style="height: 115px;">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td colspan="6">
                                        <%--<asp:Label ID="lblTotalIngresados" runat="server" >Total Tickets Encontrados : </asp:Label>
                                        &nbsp;&nbsp;<asp:Label ID="lblTxtIngresados" runat="server" />--%>
                                        <div style="margin: 2px; background-color: #f5f5f5; height: 16px; padding-top: 4px;">
                                            <asp:Label ID="Label1" runat="server" Font-Bold="True">Total Tickets Encontrados: </asp:Label>
                                            &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblTxtIngresados" runat="server" Font-Bold="False"></asp:Label>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td colspan="6">
                                        <div style="margin: 2px; background-color: #f5f5f5; height: 16px; padding-top: 4px;">
                                            <asp:Label ID="lblTotalSeleccionados" runat="server" Font-Bold="True">Total Tickets Seleccionados: </asp:Label>
                                            &nbsp;<asp:Label ID="lblTxtSeleccionados" runat="server" Font-Bold="False"></asp:Label>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <uc5:ConsDetTicket ID="ConsDetTicket" runat="server" />
                                        <uc3:OKMessageBox ID="omb" runat="server" />
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <asp:HiddenField ID="hdNumSelTotal" runat="server" />
                                        <asp:HiddenField ID="hdNumSelConObs" runat="server" />
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnConsultar" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnExtornar" EventName="Click" />
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
                    <uc2:PiePagina ID="PiePagina2" runat="server" />
                </td>
            </tr>
        </table>
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
        <br />
        <asp:Label ID="txtOrdenacion" runat="server" Visible="False"></asp:Label><asp:Label
            ID="txtColumna" runat="server" Visible="False"></asp:Label><asp:Label ID="txtPaginacion"
                runat="server" Visible="False"></asp:Label><asp:Label ID="txtValorMaximoGrilla" runat="server"
                    Visible="False"></asp:Label><br />
    </div>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    </form>
</body>
</html>
