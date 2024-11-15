﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Ope_TicketContingencia.aspx.cs" Inherits="Ope_TicketContingencia" ResponseEncoding="utf-8" %>

<%@ Register Src="UserControl/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="uc1" %>
<%@ Register Src="UserControl/PiePagina.ascx" TagName="PiePagina" TagPrefix="uc2" %>
<%@ Register src="UserControl/OKMessageBox.ascx" tagname="OKMessageBox" tagprefix="uc3" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register assembly="System.Web.Extensions" namespace="System.Web.UI" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Emitir Tickets de Contingencia</title>
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
    <link href="css/Style.css" rel="stylesheet" type="text/css" />
    <!-- #INCLUDE file="javascript/KeyPress.js" -->
    <!-- #INCLUDE file="javascript/Functions.js" -->
    <!-- #INCLUDE file="javascript/validarteclaF5.js" -->
    <!-- #INCLUDE file="javascript/MantenerSesion.js" -->
    <script language="JavaScript" type="text/javascript">
        function CalcularVenta() {
            var cant= document.getElementById("txtCantidad").value;
            var precTicket=document.getElementById("lblPrecTicketVal").innerHTML+" meters";
            var total=cant*precTicket;
            //document.getElementById('lblPrecTotVal').innerHTML=total.toFixed(2);
        }
        
    </script>
</head>
<body>
    <form id="form1" runat="server" >
        <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeOut="600">
        </asp:ScriptManager>  
        <div style="background-image: url(Imagenes/back.gif)">
            <table align="center" border="0" cellpadding="0" cellspacing="0" class="TamanoTabla">
                <tr>
                    <td class="Espacio1FilaTabla" colspan="9" style="height: 11px">
                                                
                        <uc1:CabeceraMenu ID="CabeceraMenu2" runat="server" />
                    </td>
                </tr>
                <tr class="formularioTitulo">
                    <td align="right" style="text-align: left">&nbsp;</td>
                    <td align="right">
                        <asp:Button ID="btnGenerar" runat="server" OnClick="btnAceptar_Click" CssClass="Boton" />
                        <cc2:ConfirmButtonExtender ID="btnGenerar_ConfirmButtonExtender" runat="server" ConfirmText="" Enabled="True" TargetControlID="btnGenerar" BehaviorID="btnGenerar_ConfirmButtonExtender">
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
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate> 
                        <div class="EspacioSubTablaPrincipal">
                            <table cellpadding="0" cellspacing="0" class="EspacioSubTablaPrincipal">
                                <tr>
                                    <td class="SpacingGrid" style="height: 115px;width:5%">
                                    </td>
                                    <td class="CenterGrid" style="height: 115px;width:95%">
                                    <table style="width: 100%; left: 0px; position: relative; top: 0px;" class="alineaderecha">
                                        <tr>
                                            <td colspan="3" >  
                                            <table cellpadding="0" cellspacing="0">
                                            <tr>
                                            <td>
                                               <fieldset>
                                               <legend><asp:Label ID="lblTipoTicket" runat="server" CssClass="msgMensajeNegrita"></asp:Label></legend>
                                               <table cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td style="width: 100px">
                                                            <asp:Panel ID="pnlTipvuelo" runat="server" Height="50px" Width="125px">
                                                                <asp:RadioButton ID="rbNacional" runat="server" Checked="True" CssClass="msgMensajeBlack"
                                                                    GroupName="TipVuelo" oncheckedchanged="rbNacional_CheckedChanged" 
                                                                    Text="Nacional" AutoPostBack="True" />
                                                                <br />
                                                                <asp:RadioButton ID="rbInter" runat="server" GroupName="TipVuelo" CssClass="msgMensajeBlack"
                                                                    oncheckedchanged="rbInter_CheckedChanged" Text="Internacional" 
                                                                    AutoPostBack="True" />
                                                            </asp:Panel>
                                                        </td>
                                                        <td style="width: 100px">
                                                            <asp:Panel ID="pnlTiptrasbordo" runat="server" Height="50px" Width="125px">
                                                                <asp:RadioButton ID="rbNormal" runat="server" Checked="True" CssClass="msgMensajeBlack"
                                                                    GroupName="TipTrans" oncheckedchanged="rbNormal_CheckedChanged" 
                                                                    Text="Normal" AutoPostBack="True" />
                                                                <br />
                                                                <asp:RadioButton ID="rbTrans" runat="server" GroupName="TipTrans" CssClass="msgMensajeBlack" 
                                                                    oncheckedchanged="rbTrans_CheckedChanged" Text="Transferencia" 
                                                                    AutoPostBack="True" />
                                                            </asp:Panel>
                                                        </td>
                                                        <td style="width: 100px">
                                                            <asp:Panel ID="pnl1" runat="server" Height="50px" Width="125px">
                                                                <asp:RadioButton ID="rbAdulto" runat="server" Checked="True" GroupName="TipPas" CssClass="msgMensajeBlack" 
                                                                    oncheckedchanged="rbAdulto_CheckedChanged" Text="Adulto" 
                                                                    AutoPostBack="True" />
                                                                <br />
                                                                <asp:RadioButton ID="rbInfante" runat="server" GroupName="TipPas" CssClass="msgMensajeBlack"
                                                                    oncheckedchanged="rbInfante_CheckedChanged" Text="Infante" 
                                                                    AutoPostBack="True" />
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                </table>                                                  
                                               </fieldset>                                             
                                            </td>
                                            </tr>
                                            </table>                                                                                                   
                                            </td>
                                        </tr>                                        
                                        <tr>
                                        <td>
                                        <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td style="height:20px; width: 120px">&nbsp;</td>
                                            <td style="width: 331px; text-align: left; height: 13px;" colspan="3">
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td style="height:20px">
                                                <asp:Label ID="lblFechaActual" runat="server" CssClass="msgMensaje"></asp:Label>
                                                </td>
                                            <td style="text-align: left; " colspan="3">
                                                <asp:Label ID="lblFecActVal" runat="server" Text="Label" CssClass="TextoCampo"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height:20px">
                                                <asp:Label ID="lblPrecioTicket" runat="server" CssClass="msgMensaje"></asp:Label>
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:DropDownList ID="cmbPrecioTicket" runat="server"  
                                                    Width="200px" CssClass="combo" AutoPostBack="True" 
                                                    onselectedindexchanged="cmbPrecioTicket_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:Label ID="lblPrecTicketVal" runat="server" CssClass="TextoCampo" Visible="False"></asp:Label>
                                            </td>
                                            <td>
                                            </td>
                                            <td style="text-align: left;">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height:20px">
                                                <asp:Label ID="lblCantidad" runat="server" CssClass="msgMensaje"></asp:Label>
                                            </td>
                                            <td style="width: 331px; text-align: left;">
                                                <asp:TextBox ID="txtCantidad" runat="server" CssClass="textbox" MaxLength="3" Width="158px"></asp:TextBox>
                                                <cc2:FilteredTextBoxExtender ID="txtCantidad_FilteredTextBoxExtender" 
                                                    runat="server" Enabled="True" FilterType="Numbers" 
                                                    TargetControlID="txtCantidad">
                                                </cc2:FilteredTextBoxExtender>
                                                <asp:RequiredFieldValidator ID="rfvCantidad" runat="server" 
                                                    ControlToValidate="txtCantidad" 
                                                    ErrorMessage="Ingresar cantidad de tickets válido.">
                                                </asp:RequiredFieldValidator>
                                            </td>
                                            <td class="alineaderecha">
                                                &nbsp;
                                            </td>
                                            <td style="width: 331px; text-align: left;">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        </table>
                                        
                                        </td>
                                        
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td style="text-align: left; " colspan="2">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td colspan="2" style="text-align: left; ">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                               <asp:Label ID="lblMensajeError" runat="server" CssClass="mensaje" Width="427px"></asp:Label>
                                            </td>
                                            <td style="text-align: left; " colspan="2" >
                                                
                                            </td>
                                        </tr>                                        
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td align="center">
                                                <uc3:OKMessageBox ID="omb" runat="server" />
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>                                        
                                    </table>
                                    </td>
                                    <td class="SpacingGrid" style="height: 310px; width: 2%;" valign="bottom">
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <uc2:PiePagina ID="PiePagina2" runat="server" />
                    </ContentTemplate>
                        <triggers>
                            <asp:AsyncPostBackTrigger ControlID="txtCantidad" EventName="TextChanged" />
                        </triggers>
                    </asp:UpdatePanel>
                    <asp:UpdateProgress AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="0" ID="updateProgress" runat="server">
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
             &nbsp;
            <br />
            <br />
        </div>
        <p>&nbsp;</p>
    </form>
</body>
</html>