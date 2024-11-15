<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Ope_ComprobanteSEAE.aspx.cs"
    Inherits="Ope_ComprobanteSEAE" %>

<%@ Register Src="UserControl/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="uc1" %>
<%@ Register Src="UserControl/PiePagina.ascx" TagName="PiePagina" TagPrefix="uc2" %>
<%@ Register Src="UserControl/OKMessageBox.ascx" TagName="OKMessageBox" TagPrefix="uc3" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="System.Web.Extensions" Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Generacion Comprobante SEAE</title>
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
    <link href="css/Style.css" rel="stylesheet" type="text/css" />
    <link href="css/progress.css" rel="stylesheet" type="text/css" />
    <!-- #INCLUDE file="javascript/ValidarFecha.js" -->
    <!-- #INCLUDE file="javascript/KeyPress.js" -->
    <!-- #INCLUDE file="javascript/Functions.js" -->
    <!-- #INCLUDE file="javascript/validarteclaF5.js" -->
    <!-- #INCLUDE file="javascript/MantenerSesion.js" -->

    <script language="JavaScript" type="text/javascript">
    
    function confirmacionLlamada()
		{
		   if (confirm("Está seguro de realizar esta operacion.")) {
		      accionSave = true;
		      return true;
		   }
		   else {
		          accionSave = false;
		          return false;
		        }
		}
		
		 var accionSave = false;
        function beginRequest(sender, args)
        {
            if (!sender.get_isInAsyncPostBack()) {
                if (accionSave) {
                    document.getElementById('btnAceptar').disabled = true;
                    document.body.style.cursor = 'wait';

                }
            }
        }

        function endRequest(sender, args)
        {
            if (!sender.get_isInAsyncPostBack()) {
                if (accionSave) {
                    document.getElementById('btnAceptar').disabled = false;
                    document.body.style.cursor = 'default'

                    accionSave = false;
                }
            }
        }   
    
        
     function onCalendarShown() {
            var cal = $find("calendar1");     //Setting the default mode to month   
            cal._switchMode("months", true);      //Iterate every month Item and attach click event to it 
            if (cal._monthsBody) {
                for (var i = 0; i < cal._monthsBody.rows.length; i++) {
                    var row = cal._monthsBody.rows[i];
                    for (var j = 0; j < row.cells.length; j++) {
                        Sys.UI.DomEvent.addHandler(row.cells[j].firstChild, "click", call);
                    }
                }
            }
        }
        function onCalendarHidden() {
            var cal = $find("calendar1");     //Iterate every month Item and remove click event from it   
            if (cal._monthsBody) {
                for (var i = 0; i < cal._monthsBody.rows.length; i++) {
                    var row = cal._monthsBody.rows[i];
                    for (var j = 0; j < row.cells.length; j++) {
                        Sys.UI.DomEvent.removeHandler(row.cells[j].firstChild, "click", call);
                    }
                }
            }
        }
        
           function call(eventElement) {
            var target = eventElement.target;
            switch (target.mode) {
                case "month":
                    var cal = $find("calendar1");
                    cal._visibleDate = target.date;
                    cal.set_selectedDate(target.date);
                    cal._switchMonth(target.date);
                    cal._blur.post(true);
                    cal.raiseDateSelectionChanged();
                    break;
            }
        }
    
    function ActualizaMes() {
            var myString = document.getElementById("txtMes").value;
            var mySplitResult = myString.split("/");

            document.getElementById("hfMes").value = mySplitResult[0];
            document.getElementById("hfAnnio").value = mySplitResult[1];

            var index = mySplitResult[0] * 1;
            if (index > 0) {
                var SelValue = document.getElementById("ddlMes").options[index - 1].outerText;
                document.getElementById("txtMes").value = SelValue + " - " + mySplitResult[1];
            }
        }
        
      
    
    </script>

    <style type="text/css">
        .style1
        {
            width: 98px;
        }
        .style2
        {
            width: 62px;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="1200">
    </asp:ScriptManager>
    <div style="background-image: url(Imagenes/back.gif)">
        <table align="center" border="0" cellpadding="0" cellspacing="0" class="TamanoTabla">
            <tr>
                <td class="Espacio1FilaTabla" colspan="9" style="height: 11px">
                    <uc1:CabeceraMenu ID="CabeceraMenu2" runat="server" />
                </td>
            </tr>
            <tr class="formularioTitulo">
                <td align="right" style="text-align: left">
                    <img alt="" src="Imagenes/flecha_back.png" onclick="JavaScript: FnVolver();" />
                </td>
                <td align="right">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:Button ID="btnGenerar" runat="server" CssClass="Boton" OnClick="btnGenerar_Click"
                                OnClientClick="return confirmacionLlamada()" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="UpdatePanel1" runat="server"
                        DisplayAfter="500">
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
            <tr>
                <td colspan="2">
                    <hr class="EspacioLinea" color="#0099cc" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <div class="EspacioSubTablaPrincipal">
                        &nbsp;
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <table cellpadding="0" cellspacing="0" class="EspacioSubTablaPrincipal">
                                    <tr>
                                        <td class="SpacingGrid" style="height: 115px">
                                        </td>
                                        <td class="CenterGrid" style="height: 115px">
                                            <table style="width: 100%; left: 0px; top: 0px;" class="alineaderecha">
                                                <tr>
                                                    <td colspan="2">
                                                        &nbsp;
                                                    </td>
                                                    <td colspan="5">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style2">
                                                        <asp:Label ID="lblFecha" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                                    </td>
                                                    <td class="style7">
                                                        <asp:TextBox ID="txtMes" runat="server" BackColor="#E4E2DC" CssClass="textbox" onchange="ActualizaMes()"
                                                            onfocus="this.blur();" onkeypress="JavaScript: window.event.keyCode = 0;" Width="102px"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="txtMes_CalendarExtender" runat="server" BehaviorID="calendar1"
                                                            Enabled="True" Format="MM/yyyy" OnClientHidden="onCalendarHidden" OnClientShown="onCalendarShown"
                                                            PopupButtonID="imgbtnCalendarMes" TargetControlID="txtMes">
                                                        </cc1:CalendarExtender>
                                                        <asp:ImageButton ID="imgbtnCalendarMes" runat="server" Height="22px" ImageUrl="~/Imagenes/Calendar.bmp"
                                                            Width="22px" />
                                                    </td>
                                                    <td class="style1">
                                                        <asp:Label ID="lblTipoDocumento" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                                    </td>
                                                    <td colspan="2" style="text-align: left;" class="style9">
                                                        <asp:RadioButton ID="rbTicket" runat="server" Checked="True" GroupName="formato"
                                                            AutoPostBack="True" OnCheckedChanged="rbTicket_CheckedChanged" />
                                                    </td>
                                                    <td colspan="2" style="width: 331px; text-align: left; height: 13px;">
                                                        <asp:RadioButton ID="rbBP" runat="server" GroupName="formato" AutoPostBack="True"
                                                            OnCheckedChanged="rbBP_CheckedChanged" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style2">
                                                        <asp:Label ID="lblArchivo" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6" style="width: 331px; text-align: left; height: 13px;">
                                                        <asp:TreeView ID="tvwModalidad" runat="server" CssClass="arbol" ShowCheckBoxes="All"
                                                            ShowExpandCollapse="False" ShowLines="True" SkipLinkText="" Width="244px">
                                                            <NodeStyle ChildNodesPadding="10px" />
                                                            <LevelStyles>
                                                                <asp:TreeNodeStyle BackColor="AliceBlue" Font-Underline="False" />
                                                            </LevelStyles>
                                                        </asp:TreeView>
                                                        <asp:Label ID="lblMensajeError" runat="server" CssClass="mensaje" Width="427px"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style5">
                                            &nbsp;
                                        </td>
                                        <td align="center" colspan="6">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style5">
                                            &nbsp;
                                        </td>
                                        <td colspan="6" style="width: 331px; text-align: left; height: 13px;">
                                            <asp:Label ID="lblGrilla" runat="server" CssClass="msgMensaje"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style4">
                                            &nbsp;
                                        </td>
                                        <td class="style9" colspan="6" style="text-align: left;">
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                           
                    </div>
                     </ContentTemplate>
                        </asp:UpdatePanel>
                    <div>
                    </div>
                    <uc2:PiePagina ID="PiePagina2" runat="server" />
                </td>
            </tr>
        </table>
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
        <br />
        <br />
    </div>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <asp:DropDownList ID="ddlMes" runat="server" Style="display: none">
        <asp:ListItem Value="1">Enero</asp:ListItem>
        <asp:ListItem Value="2">Febrero</asp:ListItem>
        <asp:ListItem Value="3">Marzo</asp:ListItem>
        <asp:ListItem Value="4">Abril</asp:ListItem>
        <asp:ListItem Value="5">Mayo</asp:ListItem>
        <asp:ListItem Value="6">Junio</asp:ListItem>
        <asp:ListItem Value="7">Julio</asp:ListItem>
        <asp:ListItem Value="8">Agosto</asp:ListItem>
        <asp:ListItem Value="9">Setiembre</asp:ListItem>
        <asp:ListItem Value="10">Octubre</asp:ListItem>
        <asp:ListItem Value="11">Noviembre</asp:ListItem>
        <asp:ListItem Value="12">Diciembre</asp:ListItem>
    </asp:DropDownList>
    <asp:HiddenField ID="hfMes" runat="server" />
    <asp:HiddenField ID="hfAnnio" runat="server" />
    </form>
</body>
</html>
