<%@ page language="C#" autoeventwireup="true" inherits="Ope_AnulacionTicket, App_Web_ehzg6gwo" responseencoding="utf-8" %>

<%@ Register Src="UserControl/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="uc1" %>
<%@ Register Src="UserControl/PiePagina.ascx" TagName="PiePagina" TagPrefix="uc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="UserControl/OKMessageBox.ascx" TagName="OKMessageBox" TagPrefix="uc3" %>
<%@ Register Src="UserControl/ConsDetTicket.ascx" TagName="ConsDetTicket" TagPrefix="uc5" %>
<%@ Register Assembly="PaginGridView" Namespace="Pagin.Web.Control" TagPrefix="cc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LAP - Anulación de Tickets</title>
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
    <link href="css/Style.css" rel="stylesheet" type="text/css" />
    <!-- #INCLUDE file="javascript/KeyPress.js" -->
    <!-- #INCLUDE file="javascript/MantenerSesion.js" -->
    <!-- #INCLUDE file="javascript/validarteclaF5.js" -->
    <!-- #INCLUDE file="javascript/Functions.js" -->

    <script language="javascript" type="text/javascript">

        function HabilitaOpciones() {
            try {

                if (document.getElementById("rbtnNumTicket").checked == true) {

                    document.getElementById("txtNumTicket").disabled = false;
                    document.getElementById("txtDesde").disabled = true;
                    document.getElementById("txtHasta").disabled = true;
                    document.getElementById("txtNumTicket").style.backgroundColor = '#FFFFFF';
                    document.getElementById("txtDesde").style.backgroundColor = '#CCCCCC';
                    document.getElementById("txtHasta").style.backgroundColor = '#CCCCCC';
                    document.getElementById("txtDesde").value = '';
                    document.getElementById("txtHasta").value = '';
                }

                if (document.getElementById("rbtnRangoTicket").checked == true) {

                    document.getElementById("txtNumTicket").disabled = true;
                    document.getElementById("txtDesde").disabled = false;
                    document.getElementById("txtHasta").disabled = false;
                    document.getElementById("txtNumTicket").style.backgroundColor = '#CCCCCC';
                    document.getElementById("txtDesde").style.backgroundColor = '#FFFFFF';
                    document.getElementById("txtHasta").style.backgroundColor = '#FFFFFF';
                    document.getElementById("txtNumTicket").value = '';
                }

            } catch (Exception) {
            }
        }

        var NumTotal = 0;

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
                    if (count > 0)
                        frm.chkAll.disabled = false;
                    else
                        frm.chkAll.disabled = true;
                }

                //frm.hdNumSelTotal.value = "0";

            } catch (Exception) {
            }
        }

        function SetNumTotal() {
            try {
                var frm = document.forms[0];
                frm.hdNumSelTotal.value = "0";
            }
            catch (Exception) {
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


        function validaBusqueda() {
            try {
                document.getElementById('lblErrorMsg').innerHTML = "";
                document.getElementById('lblErrorHasta').innerHTML = "";
                document.getElementById('lblErrorDesde').innerHTML = "";
                document.getElementById('lblErrorNumTicket').innerHTML = "";
                document.getElementById('lblMensajeError').innerHTML = "";

                document.getElementById('hdNumSelTotal').value = "0";

                if (document.forms[0].rbtnNumTicket.checked) {
                    if (document.forms[0].txtNumTicket.value.length < 16) {

                        document.getElementById('lblErrorMsg').innerHTML = "El Número de Ticket debe contener 16 caracteres";
                        document.getElementById('lblErrorNumTicket').innerHTML = "*";
                        return false;
                    }
                    else {
                        document.getElementById('lblErrorDesde').innerHTML = "";
                        document.getElementById('lblErrorNumTicket').innerHTML = "";
                    }
                }
                else {
                    if (document.forms[0].rbtnRangoTicket.checked) {
                        if (document.forms[0].txtDesde.value.length < 16) {
                            document.getElementById('lblErrorMsg').innerHTML = "El Inicio del Rango debe contener 16 caracteres";
                            document.getElementById('lblErrorDesde').innerHTML = "*";
                            return false;
                        }
                        else {
                            document.getElementById('lblErrorMsg').innerHTML = "";
                            document.getElementById('lblErrorDesde').innerHTML = "";
                        }
                        if (document.forms[0].txtHasta.value.length < 16) {
                            document.getElementById('lblErrorMsg').innerHTML = "El Fin de Rango debe contener 16 caracteres";
                            document.getElementById('lblErrorHasta').innerHTML = "*";
                            return false;
                        }
                        else {
                            document.getElementById('lblErrorMsg').innerHTML = "";
                            document.getElementById('lblErrorHasta').innerHTML = "";
                        }
                    }
                    else {
                        document.getElementById('lblErrorMsg').innerHTML = "Seleccion una opción de consulta";
                    }
                }

                return true;
            } catch (Exception) {
                return false;
            }
        }



        var accionSave = false;

       function ValidarAnulacion() {
        /*if (document.forms[0].txtMotivo.value.length == 0) {
        document.getElementById('lblErrorMsg').innerHTML = "Motivo campo obligatorio";
        accionSave = false;
        return false;
        }*/
       // else{
        accionSave = true;
       // return true;
      //  }
            
       }

        function beginRequest(sender, args) {
            if (!sender.get_isInAsyncPostBack()) {
                if (accionSave) {
                    document.getElementById('btnAnular').disabled = true;
                    document.body.style.cursor = 'wait';

                }
            }
        }


        function endRequest(sender, args) {
            if (!sender.get_isInAsyncPostBack()) {
                if (accionSave) {
                    document.getElementById('btnAnular').disabled = false;
                    document.body.style.cursor = 'default'

                    accionSave = false;
                }
            }
        }


        function reinicioSeleccion() {
            NumTotal = 0;
        }
    
    </script>

    <style type="text/css">
        .style5
        {
            text-align: left;
            width: 262px;
        }
        .style7
        {
            text-align: right;
            width: 9px;
        }
        .style8
        {
            text-align: right;
            width: 253px;
        }
        .style9
        {
            width: 303px;
        }
        .style10
        {
            width: 302px;
        }
        .style11
        {
            width: 216px;
        }
        .style12
        {
            width: 498px;
        }
        .style13
        {
            text-align: left;
            width: 104px;
        }
        .style15
        {
            width: 262px;
        }
        .style16
        {
            width: 262px;
            height: 115px;
        }
        .style18
        {
            height: 21px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
        <Scripts>
            <asp:ScriptReference Path="~/javascript/jSManager.js" />
        </Scripts>
    </asp:ScriptManager>
    <table align="center" border="0" cellpadding="0" cellspacing="0" class="TamanoTabla">
        <tr>
            <td class="Espacio1FilaTabla" colspan="2" style="height: 11px">
                <uc1:CabeceraMenu ID="CabeceraMenu2" runat="server" />
            </td>
        </tr>
        <tr class="formularioTitulo">
            <td align="right" style="text-align: left">
                &nbsp;
            </td>
            <td align="right">
                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                    <ContentTemplate>
                        <asp:Button ID="btnAnular" runat="server" 
    OnClientClick="Javascript:return ValidarAnulacion();" CssClass="Boton"
                    OnClick="btnAnular_Click"  />
                        <cc1:ConfirmButtonExtender ID="cbeAnular" runat="server" 
    ConfirmText="" Enabled="True"
                    TargetControlID="btnAnular">
                        </cc1:ConfirmButtonExtender>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <hr class="EspacioLinea" color="#0099cc" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <div class="EspacioSubTablaPrincipal">
                    &nbsp;<table cellpadding="0" cellspacing="0" class="EspacioSubTablaPrincipal">
                        <tr>
                            <td class="SpacingGrid" style="height: 115px">
                            </td>
                            <td class="CenterGrid" style="height: 115px">
                                <table style="width: 100%; left: 0px; top: 0px;" >
                                    <tr>
                                        <%--<td class="style5">
                                            &nbsp;
                                        </td>
                                        <td style="text-align: left;" class="style11">
                                            &nbsp;
                                        </td>--%>
                                        <td style="text-align: left;" class="style12">
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                
                                                <table style="width: 90%; left: 0px; top: 0px;" class="alineaderecha">
                                                        <tr>
                                                            <td colspan="3">
                                                            <asp:Label ID="Label2" runat="server" Text="Estado de Turno" CssClass="msgMensaje">                                                                
                                                            </asp:Label>                                                                
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                                <td colspan="3"><hr class="EspacioLinea" color="#0099cc" /></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:RadioButton ID="rbAbierto" runat="server" CssClass="TextoFiltro"
                                                                    GroupName="EstadoTurno" Text="Abierto" />
                                                            </td>    
                                                            <td>
                                                                <asp:RadioButton ID="rbCerrado" runat="server" CssClass="TextoFiltro"
                                                                    GroupName="EstadoTurno" Text="Cerrado" />
                                                            </td>  
                                                            <td>
                                                                <asp:RadioButton ID="rbSinTurno" runat="server" CssClass="TextoFiltro"
                                                                    GroupName="EstadoTurno" Text="Sin Turno (Preem. Conti)" />
                                                            </td>    
                                                          </tr>
                                                          <tr>
                                                            <td colspan="3">
                                                            <asp:Label ID="Label1" runat="server" Text="Filtros de Ticket" CssClass="msgMensaje">                                                                
                                                            </asp:Label>                                                                
                                                            </td>
                                                        </tr>
                                                          <tr>
                                                                <td colspan="3"><hr class="EspacioLinea" color="#0099cc" /></td>
                                                            </tr>
                                                      </table>
                                                            
                                                    <table style="width: 70%; left: 0px; top: 0px;" class="alineaderecha">
                                                        <tr>
                                                            <td>
                                                                <asp:RadioButton ID="rbtnNumTicket" runat="server" CssClass="TextoFiltro" onClick="HabilitaOpciones();"
                                                                    GroupName="TipoConsulta" />
                                                                <table>
                                                                    <tr>
                                                                        <td class="style18">
                                                                            <asp:TextBox ID="txtNumTicket" runat="server" CssClass="textbox" MaxLength="16" onblur="JavaScript: val_int(this)"
                                                                                onkeypress="JavaScript: Tecla('Integer');" Width="123px"></asp:TextBox>
                                                                        </td>
                                                                        <td class="style18">
                                                                            <asp:Label ID="lblErrorNumTicket" runat="server" ForeColor="Red"></asp:Label>
                                                                        </td>
                                                            </td>       
                                                          </tr>
                                                      </table>
                                                           
                                                            <td>
                                                            
                                                                <asp:RadioButton ID="rbtnRangoTicket" runat="server" CssClass="TextoFiltro" onClick="HabilitaOpciones();"
                                                                    GroupName="TipoConsulta" />
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="lblDesde" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtDesde" runat="server" CssClass="textbox" MaxLength="16" onblur="JavaScript: val_int(this)"
                                                                                onkeypress="JavaScript: Tecla('Integer');" Width="123px"></asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblErrorDesde" runat="server" ForeColor="Red"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblHasta" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtHasta" runat="server" CssClass="textbox" MaxLength="16" onblur="JavaScript: val_int(this)"
                                                                                onkeypress="JavaScript: Tecla('Integer');" Width="123px"></asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblErrorHasta" runat="server" ForeColor="Red"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            
                                                            </td>
                                                            
                                                             <td>
                                                             &nbsp;
                                                                <table>
                                                                    <tr>
                                                                        <td class="style13" colspan="1">
                                                                            <asp:Label ID="lblTipoTicket" runat="server" CssClass="TextoEtiqueta" Width="70px"></asp:Label>
                                                                        </td>
                                                                        <%--<td class="style7">
                                                                            &nbsp;
                                                                        </td>--%>
                                                                        <td style="text-align: left;" class="style8">
                                                                            <asp:DropDownList ID="ddlTipoTicket" runat="server" CssClass="combo">
                                                                                <asp:ListItem Value="T">&lt;Todos&gt;</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td style="text-align: left;" class="style10">
                                                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                                                                <ContentTemplate>
                                                                                    <asp:ImageButton ID="btnBuscar" runat="server" OnClientClick="Javascript:return validaBusqueda();"
                                                                                        OnClick="btnBuscar_Click" ImageUrl="~/Imagenes/Search.bmp" Height="20px" Width="20px"
                                                                                        CausesValidation="False" />
                                                                                </ContentTemplate>
                                                                            </asp:UpdatePanel>
                                                                            
                                                                            <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="UpdatePanel4" runat="server">
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
                                                        <td>
                                                        <asp:Label ID="lblMotivo" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                        <asp:TextBox ID="txtMotivo" runat="server" CssClass="textbox" Width="323px"></asp:TextBox>
                                                        </td>
                                                        
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="btnAnular" EventName="Click" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </td>
                                       <%-- <td style="width: 200px; text-align: left; height: 13px;">
                                            &nbsp;
                                        </td>--%>
                                    </tr>
                                   <tr>
                                        <td class="style5">
                                            <br />
                                            <br />
                                            <br />
                                        </td>             
                                    </tr>
                                    
                                    <tr>
                                          <td class="CenterGrid" colspan="3" align="left">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <table class="alineaderecha" style="width: 100%; left: 0px; top: 0px;">
                                                        <tr>
                                                            <td align="left" class="CenterGrid">
                                                                <cc2:PagingGridView ID="gwvAnularTicket" runat="server" AutoGenerateColumns="False"
                                                                    BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
                                                                    CellPadding="3" Width="100%" AllowPaging="True" CssClass="grilla" AllowSorting="True"
                                                                    VirtualItemCount="-1" OnPageIndexChanging="gwvAnularTicket_PageIndexChanging"
                                                                    GroupingDepth="0" OnSorting="gwvAnularTicket_Sorting" RowStyle-HorizontalAlign="Center"
                                                                    RowStyle-VerticalAlign="Middle" OnRowCommand="gwvAnularTicket_RowCommand" OnRowDataBound="gwvAnularTicket_RowDataBound">
                                                                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                                    <PagerSettings Mode="NumericFirstLast" />
                                                                    <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Numero" ItemStyle-Width="8%">
                                                                            <ItemTemplate>
                                                                                <%# Container.DataItemIndex+1 %>
                                                                            </ItemTemplate>
                                                                            <ItemStyle Width="8%" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Nº de Ticket" ItemStyle-Width="20%" SortExpression="Cod_Numero_Ticket">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="codTicket" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                                                                    CommandName="ShowTicket" Text='<%# Eval("Cod_Numero_Ticket") %>' />
                                                                            </ItemTemplate>
                                                                            <ItemStyle Width="20%" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Tipo Ticket" SortExpression="Dsc_Tipo_Ticket">
                                                                        <ItemTemplate>
                                                                        <%# Eval("Dsc_Tipo_Ticket")%>
                                                                        </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Compañía" SortExpression="Dsc_Compania">
                                                                        <ItemTemplate>
                                                                        <%# Eval("Dsc_Compania")%>
                                                                        </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Fecha Vuelo" SortExpression="Fch_Vuelo">
                                                                        <ItemTemplate>
                                                                        <%# Eval("Fch_Vuelo")%>
                                                                        </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Nro. Vuelo" SortExpression="Dsc_Num_Vuelo">
                                                                        <ItemTemplate>
                                                                        <%# Eval("Dsc_Num_Vuelo")%>
                                                                        </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Estado Actual" SortExpression="Dsc_Campo">
                                                                        <ItemTemplate>
                                                                        <%# Eval("Dsc_Campo")%>
                                                                        </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <%--<asp:TemplateField HeaderText="Motivo">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtMotivo" runat="server" CssClass="textbox" Height="16px" CommandArgument="Motivo"
                                                                                    MaxLength="255" Width="358px"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>--%>
                                                                        <asp:TemplateField ItemStyle-Width="18%" SortExpression="Check">
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox ID="chkSeleccionar" runat="server" onclick="Javascript:SetCheckBoxHeaderGrilla(this); " />
                                                                            </ItemTemplate>
                                                                            <HeaderTemplate>
                                                                                <input disabled="true" name="chkAll" onclick="JavaScript:SetCheck(this);" type="checkbox" />
                                                                                &nbsp;<asp:LinkButton ID="LinkButton1" runat="server" CommandArgument="Check" CommandName="Sort"
                                                                                    Text="Seleccionar"></asp:LinkButton>
                                                                            </HeaderTemplate>
                                                                            <ItemStyle Width="18%" />
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <SelectedRowStyle CssClass="grillaFila" />
                                                                    <PagerStyle CssClass="grillaPaginacion" />
                                                                    <HeaderStyle CssClass="grillaCabecera" />
                                                                    <AlternatingRowStyle CssClass="grillaFila" />
                                                                </cc2:PagingGridView>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="formularioTitulo" style="width: 331px; text-align: left; height: 13px;">
                                                                <asp:Label ID="lblTotalIngresados" runat="server" />
                                                                &nbsp;&nbsp;&nbsp;
                                                                <asp:Label ID="lblTxtIngresados" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="formularioTitulo" style="width: 331px; text-align: left; height: 13px;">
                                                                <asp:Label ID="lblTotalSeleccionados" runat="server" />
                                                                &nbsp;<asp:Label ID="lblTxtSeleccionados" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style9" style="text-align: left;">
                                                                <asp:Label ID="lblMensajeError" runat="server" CssClass="mensaje" Width="427px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="btnAnular" EventName="Click" />
                                                    <asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                            
                                            <asp:UpdateProgress ID="UpdateProgress2" AssociatedUpdatePanelID="UpdatePanel1" runat="server"
                                                DisplayAfter="100">
                                                <ProgressTemplate>
                                                    <div id="progressBackgroundFilter"></div>
                                                    <div id="processMessage">
                                                        &nbsp;&nbsp;&nbsp;Procesando...<br />
                                                        <br />
                                                        <img alt="Loading" src="Imagenes/ajax-loader.gif" />
                                                    </div>
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>
                                        </td>
                                        <td class="SpacingGrid" colspan="1" style="height: 115px;">
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                                &nbsp;
                            </td>
                            <td class="SpacingGrid" style="height: 115px; width: 2%;" valign="bottom">
                            </td>
                        </tr>
                    </table>
                </div>
                <uc2:PiePagina ID="PiePagina3" runat="server" />
            </td>
        </tr>
    </table>
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <uc5:ConsDetTicket ID="ConsDetTicket" runat="server" />
            <uc3:OKMessageBox ID="omb" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:HiddenField ID="hdNumSelTotal" runat="server" />
    <asp:HiddenField ID="hdNumSelConObs" runat="server" />
    </form>
</body>
</html>
