<%@ page language="C#" autoeventwireup="true" inherits="Reh_TicketsPorVuelo, App_Web_jlql8yfo" responseencoding="utf-8" %>

<%@ Register Src="UserControl/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="uc1" %>
<%@ Register Src="UserControl/PiePagina.ascx" TagName="PiePagina" TagPrefix="uc2" %>
<%@ Register Src="UserControl/ConsRepresentante.ascx" TagName="ConsRepresentante"
    TagPrefix="uc3" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="UserControl/ConsDetTicket.ascx" TagName="ConsDetTicket" TagPrefix="uc5" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LAP - Rehabilitacion de Tickets por Vuelo</title>
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
    <link href="css/Style.css" rel="stylesheet" type="text/css" />
    <!-- #INCLUDE file="javascript/KeyPress.js" -->
    <!-- #INCLUDE file="javascript/MantenerSesion.js" -->
    <!-- #INCLUDE file="javascript/validarteclaF5.js" -->
    <link href="css/Style.css" rel="stylesheet" type="text/css" />
</head>

<script language="javascript" type="text/javascript">
  function openPopup(popurl) {

    winpops = window.open(popurl, "", "width:800,height:200, status: No,toolbar=no, left=45, top=15, scrollbars=no, menubar=no,resizable=no,directories=no,location=no,modal=yes")
    /*
    if(window.showModalDialog){
    winpops = window.showModalDialog(popurl,"","dialogWidth:800px;dialogHeight:200px; status: No;toolbar=no, left=45, top=15, scrollbars=no, menubar=no,resizable=no,directories=no,location=no")
    }
    else{
    winpops = window.open(popurl,"","width:800,height:200, status: No,toolbar=no, left=45, top=15, scrollbars=no, menubar=no,resizable=no,directories=no,location=no,modal=yes")
    }
    */
  }
</script>

<script type="text/javascript">
  // We need to set the ReadOnly property here, since in VS 2.0+, setting it in the markup does not persist client-side changes.
  function windowOnLoad() {
    document.getElementById('lblTxtSeleccionados').readOnly = true;
    document.getElementById('lblTxtIngresados').readOnly = true;
  }

  window.onload = windowOnLoad;
</script>

<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="3600">
    </asp:ScriptManager>

    <script language="javascript" type="text/javascript">
    function SetCheckBoxHeaderGrilla(control) {
      var frm = document.forms[0];
      if (control.checked) {
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

      //alert('llegue');
      //alert(control);
      //alert(control.parentNode);
      //alert(control.parentNode.previousSibling);
      //alert(control.parentNode.previousSibling.innerHTML);
      var observacion = control.parentNode.previousSibling.innerHTML;
      if (control.checked) {
        var numSelTotal = parseInt(frm.hdNumSelTotal.value);
        numSelTotal += 1;
        frm.hdNumSelTotal.value = numSelTotal;
        if (observacion != "-") {
          var numSelConObs = parseInt(frm.hdNumSelConObs.value);
          numSelConObs += 1;
          frm.hdNumSelConObs.value = numSelConObs;
        }
      }
      else {
        var numSelTotal = parseInt(frm.hdNumSelTotal.value);
        numSelTotal -= 1;
        frm.hdNumSelTotal.value = numSelTotal;
        if (observacion != "-") {
          var numSelConObs = parseInt(frm.hdNumSelConObs.value);
          numSelConObs -= 1;
          frm.hdNumSelConObs.value = numSelConObs;
        }
      }
      //alert("NumSelTotal:" + frm.hdNumSelTotal.value);
      //alert("NumSelConObs:" + frm.hdNumSelConObs.value);        

      var normales = parseInt(frm.hdNumSelTotal.value) - parseInt(frm.hdNumSelConObs.value);
      frm.lblTxtSeleccionados.value = frm.hdNumSelTotal.value + " (" + frm.hdNumSelConObs.value + " Observaciones / " + normales + " Normales)";
    }

    function CheckBoxHeaderGrilla() {
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
    }

    function SetCheck(control) {
      var frm = document.forms[0];
      var checkbox;
      var observacion;
      var numSelTotal = parseInt(frm.hdNumSelTotal.value);
      var numSelConObs = parseInt(frm.hdNumSelConObs.value);
      if (control.checked) {
        for (i = 0; i < frm.elements.length; i++) {
          if (frm.elements[i].type == "checkbox" && frm.elements[i].name != 'chkAll' && !frm.elements[i].checked) {//
            checkbox = frm.elements[i];
            observacion = checkbox.parentNode.previousSibling.innerHTML;
            numSelTotal += 1;
            frm.hdNumSelTotal.value = numSelTotal;
            if (observacion != "-") {
              numSelConObs += 1;
            }
            checkbox.checked = true;
          }
        }
      }
      else {
        for (i = 0; i < frm.elements.length; i++) {
          if (frm.elements[i].type == "checkbox" && frm.elements[i].name != 'chkAll' && frm.elements[i].checked) {
            checkbox = frm.elements[i];
            observacion = checkbox.parentNode.previousSibling.innerHTML;
            numSelTotal -= 1;
            if (observacion != "-") {
              numSelConObs -= 1;
            }
            checkbox.checked = false;
          }
        }
      }
      frm.hdNumSelTotal.value = numSelTotal;
      frm.hdNumSelConObs.value = numSelConObs;

      //alert("NumSelTotal:" + frm.hdNumSelTotal.value);  
      //alert("NumSelConObs:" + frm.hdNumSelConObs.value);         

      var normales = parseInt(frm.hdNumSelTotal.value) - parseInt(frm.hdNumSelConObs.value);
      frm.lblTxtSeleccionados.value = frm.hdNumSelTotal.value + " (" + frm.hdNumSelConObs.value + " Observaciones / " + normales + " Normales)";


    }

    function onDeleteClientClick(control) {
      var frm = document.forms[0];
      if (control.parentNode.previousSibling.children[0].checked) {
        var observacion = control.parentNode.previousSibling.previousSibling.innerHTML;
        var numSelTotal = parseInt(frm.hdNumSelTotal.value);
        numSelTotal -= 1;
        frm.hdNumSelTotal.value = numSelTotal;
        if (observacion != "-") {
          var numSelConObs = parseInt(frm.hdNumSelConObs.value);
          numSelConObs -= 1;
          frm.hdNumSelConObs.value = numSelConObs;
        }
      }
      else {
        //Total = Total - 1
      }
      //alert("NumSelTotal:" + frm.hdNumSelTotal.value);
      //alert("NumSelConObs:" + frm.hdNumSelConObs.value);        

      var normales = parseInt(frm.hdNumSelTotal.value) - parseInt(frm.hdNumSelConObs.value);
      frm.lblTxtSeleccionados.value = frm.hdNumSelTotal.value + " (" + frm.hdNumSelConObs.value + " Observaciones / " + normales + " Normales)";

      frm.lblTxtIngresados.value = parseInt(frm.lblTxtIngresados.value) - 1;
    }
    </script>

    <script type="text/javascript" language="javascript">
    //Script para validar campos y condiciones antes del submit.
    function btn_Rehabilitar_clientClick() {
      if(document.forms[0].cboMotivo.value==""){
        document.getElementById('spnRehabilitar').innerHTML = "Debe seleccionar el motivo de rehabilitación";
        return false;
      }
      
      if (document.forms[0].hdNumSelTotal.value == 0) {
        document.getElementById('spnRehabilitar').innerHTML = "Debe de seleccionar al menos un ticket para rehabilitar.";
        return false;
      }
      else {
        document.getElementById('spnRehabilitar').innerHTML = "";
      }
      var ret = confirm("¿Desea Continuar con la Rehabilitacion?");
      return ret;
    }
    </script>

    <div>
        <table align="center" border="0" cellpadding="0" cellspacing="0" class="TamanoTabla">
            <tr>
                <td style="height: 11px">
                    <uc1:CabeceraMenu ID="CabeceraMenu1" runat="server" />
                </td>
            </tr>
        </table>
        <table align="center" border="0" cellpadding="0" cellspacing="0" class="TamanoTabla">
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <table align="center" border="0" cellpadding="0" cellspacing="0" class="TamanoTabla"
                                id="tableRehabilitar" runat="server">
                                <tr class="formularioTitulo">
                                    <td align="left">
                                        <asp:Button ID="btnRehabilitar" runat="server" CssClass="Boton" OnClientClick="Javascript:return btn_Rehabilitar_clientClick();"
                                            OnClick="btnRehabilitar_Click" />
                                        &nbsp;<asp:Label ID="lblDescripcionLimite" runat="server" Text="" CssClass="msgMensaje" ></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <hr class="EspacioLinea" color="#0099cc" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblMensajeError" runat="server" CssClass="mensaje"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:Panel ID="pnlPrincipal" runat="server" Visible="true">
                    <table align="center" border="0" cellpadding="0" cellspacing="0" class="TamanoTabla">
                        <tr>
                            <td>
                                <div>
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                        <tr>
                                            <td style="width: 100%">
                                                <table width="100%" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td style="width: 0%">
                                                        </td>
                                                        <td style="width: 100%">
                                                            <!-- inicio -->
                                                            <table style="width: 100%;" cellpadding="0" cellspacing="0" border="0">
                                                                <tr class="formularioTitulo">
                                                                    <td width="5%" height="30px">
                                                                    </td>
                                                                    <td width="8%">
                                                                        <asp:Label ID="lblMotivo" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="cboMotivo" runat="server" Width="340px" CssClass="combo" AppendDataBoundItems="True">
                                                                        <asp:ListItem Value="">Seleccionar</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="5">
                                                                        <hr class="EspacioLinea" color="#0099cc" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <table style="width: 100%;" cellpadding="0" cellspacing="0" border="0">
                                                                <tr>
                                                                    <td width="5%" height="30px">
                                                                    </td>
                                                                    <td width="8%">
                                                                        <asp:Label ID="lblCia" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                                                    </td>
                                                                    <td width="17%">
                                                                        <asp:DropDownList ID="cboCompanias" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboCompanias_SelectedIndexChanged"
                                                                            Width="150px" CssClass="combo">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td width="5%">
                                                                    </td>
                                                                    <td width="12%">
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <a href="" id="lnkRepresentante" runat="server"
                                                                            onserverclick="lnkRepresentante_Click"><b>
                                                                                <asp:Label ID="lblConsRepresentante" runat="server" /></b></a>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td height="30px">
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblFechaVuelo" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtFechaVuelo" runat="server" CssClass="textbox" Width="150px" OnTextChanged="txtFechaVuelo_TextChanged"
                                                                            AutoPostBack="true" onfocus="this.blur();" onkeypress="JavaScript: window.event.keyCode = 0;"
                                                                            BackColor="#E4E2DC"></asp:TextBox>
                                                                        <cc1:CalendarExtender ID="txtFechaVuelo_CalendarExtender" runat="server" Enabled="True"
                                                                            TargetControlID="txtFechaVuelo" PopupButtonID="imgbtnCalendar" Format="dd/MM/yyyy">
                                                                        </cc1:CalendarExtender>
                                                                    </td>
                                                                    <td>
                                                                        <asp:ImageButton ID="imgbtnCalendar" ImageUrl="~/Imagenes/Calendar.bmp" runat="server"
                                                                            Width="22px" Height="22px" />
                                                                    </td>
                                                                    <td colspan="2">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td height="30px">
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblVuelo" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="cboVuelo" runat="server" AutoPostBack="True" Width="150px"
                                                                            CssClass="combo" OnSelectedIndexChanged="cboVuelo_OnSelectedIndexChanged">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td>
                                                                        <asp:ImageButton ID="btnRefresh" runat="server" OnClientClick="processEndRequest=0;"
                                                                            OnClick="btnRefresh_Click" ImageUrl="~/Imagenes/Refresh.bmp" />
                                                                    </td>
                                                                    <td colspan="2">
                                                                        <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red"></asp:Label>
                                                                        <asp:Label ID="spnRehabilitar" Style="color: Red;" runat="server" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <br />
                                                            <table style="width: 100%;" cellpadding="0" cellspacing="0" border="0">
                                                                <tr>
                                                                    <td width="5%">
                                                                    </td>
                                                                    <td>
                                                                        <div id="divGrid" style="height: 520px; overflow: auto;">
                                                                            <asp:GridView ID="gwvRehabilitarPorTicket" runat="server" RowStyle-VerticalAlign="Middle"
                                                                                RowStyle-HorizontalAlign="Center" AutoGenerateColumns="False" GridLines="Both"
                                                                                CssClass="grilla" Width="80%" OnRowDataBound="gwvRehabilitarPorTicket_RowDataBound"
                                                                                OnRowCommand="gwvRehabilitarPorTicket_RowCommand" AllowPaging="True" OnPageIndexChanging="gwvRehabilitarPorTicket_PageIndexChanging"
                                                                                AllowSorting="True" OnSorting="gwvRehabilitarPorTicket_Sorting">
                                                                                <SelectedRowStyle CssClass="grillaFila" />
                                                                                <PagerStyle CssClass="grillaPaginacion" />
                                                                                <HeaderStyle CssClass="grillaCabecera" />
                                                                                <AlternatingRowStyle CssClass="grillaFila" />
                                                                                <Columns>
                                                                                    <asp:TemplateField HeaderText="Nro." ItemStyle-Width="8%">
                                                                                        <ItemTemplate>
                                                                                            <%# Container.DataItemIndex+1 %>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Codigo Ticket" SortExpression="Cod_Numero_Ticket"
                                                                                        ItemStyle-Width="20%">
                                                                                        <ItemTemplate>
                                                                                            <asp:LinkButton ID="codTicket" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                                                                                CommandName="ShowTicket" Text='<%# Eval("Cod_Numero_Ticket") %>' />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:BoundField DataField="Observacion" HeaderText="Observaciones" SortExpression="Observacion"
                                                                                        ItemStyle-Width="45%" />
                                                                                    <asp:TemplateField SortExpression="Check" ItemStyle-Width="18%">
                                                                                        <ItemTemplate>
                                                                                            <asp:CheckBox ID="chkSeleccionar" runat="server" onclick="Javascript: SetCheckBoxHeaderGrilla(this); " />
                                                                                        </ItemTemplate>
                                                                                        <HeaderTemplate>
                                                                                            <input type="checkbox" name="chkAll" disabled="true" onclick="JavaScript:SetCheck(this);" />
                                                                                            &nbsp;<asp:LinkButton ID="LinkButton1" runat="server" CommandName="Sort" CommandArgument="Check"
                                                                                                Text="Seleccionar"></asp:LinkButton>
                                                                                        </HeaderTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Eliminar" ItemStyle-Width="8%">
                                                                                        <ItemTemplate>
                                                                                            <asp:ImageButton ID="btnEliminar" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                                                                                CommandName="Eliminar" ImageUrl="~/Imagenes/Delete.bmp" Width="20" Height="18" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                                <EmptyDataTemplate>
                                                                                    &nbsp;
                                                                                </EmptyDataTemplate>
                                                                            </asp:GridView>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <br />
                                                            <table style="width: 100%;" cellpadding="0" cellspacing="0" border="0">
                                                                <tr>
                                                                    <td colspan="3">
                                                                        <hr class="EspacioLinea" color="#0099cc" />
                                                                    </td>
                                                                </tr>
                                                                <tr class="formularioTitulo">
                                                                    <td width="5%" height="25px">
                                                                    </td>
                                                                    <td width="15%" class="TextoEtiqueta">
                                                                        <asp:Label ID="lblTotalSeleccionados" runat="server" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="lblTxtSeleccionados" Width="500px" BackColor="#f5f5f5" CssClass="TextoEtiqueta"
                                                                            BorderColor="#f5f5f5" BorderWidth="0" BorderStyle="None" runat="server">0 (0 Observaciones / 0 Normales)</asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr class="formularioTitulo">
                                                                    <td height="25px">
                                                                    </td>
                                                                    <td class="TextoEtiqueta">
                                                                        <asp:Label ID="lblTotalIngresados" runat="server" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="lblTxtIngresados" Width="500px" BackColor="#f5f5f5" CssClass="TextoEtiqueta"
                                                                            BorderColor="#f5f5f5" BorderWidth="0" BorderStyle="None" runat="server">0</asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="3">
                                                                        <hr class="EspacioLinea" color="#0099cc" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <input type="hidden" id="hdNumSelTotal" runat="server" value="0" />
                                                            <input type="hidden" id="hdNumSelConObs" runat="server" value="0" />
                                                            <!--FIN-->
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnlConformidad" runat="server" Visible="false">
                    <table align="center" border="0" cellpadding="0" cellspacing="0" class="TamanoTabla">
                        <tr class="formularioTitulo">
                            <td align="left">
                                <img alt="" src="Imagenes/flecha_back.png" onclick="Atras();" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <hr class="EspacioLinea" color="#0099cc" />
                            </td>
                        </tr>
                    </table>
                    <table align="center" border="0" cellpadding="0" cellspacing="0" class="TamanoTabla">
                        <tr>
                            <td width="35%" height="30px">
                            </td>
                            <td>
                                <table cellpadding="0" cellspacing="0" style="width: 100%;">
                                    <tr>
                                        <td>
                                            <br>
                                            <asp:Label ID="lblConformidad" runat="server" CssClass="titulosecundario" />
                                            <br>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <br>
                                            <span class="msgMensaje">Resumen</span><br>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <br>
                                            &nbsp;&nbsp;<span class="msgMensaje">Total de Tickets Rehabilitados : </span>
                                            <asp:Label ID="lblTotalRehab" runat="server" CssClass="msgMensaje" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;&nbsp;<span class="msgMensaje">Total Tickets No Rehabilitados : </span>
                                            <asp:Label ID="lblTotalNoRehab" runat="server" CssClass="msgMensaje" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <br>
                                            <span class="msgMensaje">Para conocer el detalle haga click en Imprimir</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:ImageButton
                                                ID="btnReporte" runat="server" ImageUrl="~/Imagenes/print.jpg" class="BotonImprimir"
                                                OnClick="btnReporte_click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="35%" height="30px">
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
        <table align="center" border="0" cellpadding="0" cellspacing="0" class="TamanoTabla">
            <tr>
                <td style="height: 11px">
                    <uc2:PiePagina ID="PiePagina1" runat="server" />
                </td>
            </tr>
        </table>
    </div>

    <script type="text/javascript">
    function Atras() {
      window.location.href = "./Reh_TicketsPorVuelo.aspx";
    }
    function fnClickCerrarPopup(sender, e) {
      __doPostBack(sender, e);
    }
    </script>

    <script language="javascript" type="text/javascript">
    // This Script is used to maintain Grid Scroll on Partial Postback
    var scrollTop;
    var processEndRequest = 1;
    //Register Begin Request and End Request
    if (document.forms[0].chkAll != null) {//Esto para cuando oculte el pnlPrincipal
      Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
      Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
    }
    //Get The Div Scroll Position
    function BeginRequestHandler(sender, args) {
      if (document.forms[0].chkAll != null) {//Esto para cuando oculte el pnlPrincipal
        var m = document.getElementById('divGrid');
        scrollTop = m.scrollTop;
      }
    }
    //Set The Div Scroll Position
    function EndRequestHandler(sender, args) {
      if (document.forms[0].chkAll != null) {//Esto para cuando oculte el pnlPrincipal
        if (processEndRequest == 0) {
          processEndRequest = 1;
        }
        else {
          var m = document.getElementById('divGrid');
          m.scrollTop = scrollTop;
        }
      }
    }   
    </script>

    <uc3:ConsRepresentante ID="consRepre" runat="server" />
    <uc5:ConsDetTicket ID="ConsDetTicket" runat="server" />
    <asp:UpdateProgress AssociatedUpdatePanelID="UpdatePanel2" DisplayAfter="0" ID="updateProgress"
        runat="server">
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
    </form>
</body>
</html>
