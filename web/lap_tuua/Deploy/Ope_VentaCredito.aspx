<%@ page language="C#" autoeventwireup="true" inherits="Ope_VentaCredito, App_Web_tx1el90t" responseencoding="utf-8" %>

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
    <title>LAP - Venta Credito</title>
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
    <link href="css/Style.css" rel="stylesheet" type="text/css" />
    <!-- #INCLUDE file="javascript/KeyPress.js" -->
    <!-- #INCLUDE file="javascript/Functions.js" -->
    <!-- #INCLUDE file="javascript/validarteclaF5.js" -->
    <!-- #INCLUDE file="javascript/MantenerSesion.js" -->
    <script language="JavaScript" type="text/javascript">
        function ToUpperCase(){
        var numVuelo=document.getElementById("txtNumVuelo").value;
        document.getElementById("txtNumVuelo").value=numVuelo.toUpperCase();
    }
    
    var accionSave = false;

    function confirmacionLlamada() {
        var objCheckBock = document.getElementById("chkCierreTurno");
        var strMensaje = "";

        if (objCheckBock.checked) {
            strMensaje = "¿Está seguro de realizar esta operacion con cierre de turno?";
        }
        else {
            strMensaje = "¿Está seguro de realizar esta operacion sin cierre de turno?";
        }

        if (confirm(strMensaje)) {
            accionSave = true;
            return true;
        }
        else {
            accionSave = false;
            return false;
        }
    }    

    </script>
    <style type="text/css">
        .style1
        {}
    </style>
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
                    <td align="right" style="text-align: left"></td>
                    <td align="right">
                           <asp:Button ID="btnAceptar" runat="server" OnClick="btnAceptar_Click" CssClass="Boton" OnClientClick="return confirmacionLlamada()" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <hr class="EspacioLinea" color="#0099cc" />
                    </td>
                </tr>
                <tr>
                    <td colspan="9">
                        <div class="EspacioSubTablaPrincipal">
                            &nbsp;<table cellpadding="0" cellspacing="0" class="EspacioSubTablaPrincipal">
                                <tr>
                                    <td class="SpacingGrid" style="height: 115px">
                                    </td>
                                    <td class="CenterGrid" style="height: 115px">
                                    
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>   
                                    <table class="alineaderecha"> 
                                        <tr>
                                            <td colspan="3">
                                            <table width="100%" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label1" runat="server" Text="DATOS DE TURNO" CssClass="msgMensaje">                                                                
                                                    </asp:Label>                                                                
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <hr class="EspacioLinea" color="#0099cc" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                <table width="100%" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td width="10%">
                                                            <asp:Label ID="lblDesTurno" runat="server" Text="Turno:" CssClass="msgMensaje"></asp:Label>
                                                        </td>
                                                        <td width="90%">
                                                            <asp:Label ID="lblTurno" runat="server" Text="" CssClass="msgMensajeBlack"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="10%">
                                                            <asp:Label ID="lblDesRegistro" runat="server" Text="Registros:" CssClass="msgMensaje"></asp:Label>
                                                        </td>
                                                        <td width="90%">
                                                            <asp:Label ID="lblRegistro" runat="server" Text="" CssClass="msgMensajeBlack"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" height="40px">
                                                             <asp:CheckBox ID="chkCierreTurno" runat="server" Text="Cierre de Turno"  CssClass="msgMensajeNegrita"  />
                                                        </td>
                                                    </tr>                                                                 
                                                </table>                                                                
                                                </td>
                                            </tr> 
                                            <tr>
                                                <td style="height:20px">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label2" runat="server" Text="DATOS DE VENTA" CssClass="msgMensaje">                                                                
                                                    </asp:Label>                                                                
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <hr class="EspacioLinea" color="#0099cc" />
                                                </td>
                                            </tr>                                                                                                   
                                            </table>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td>
                                                <fieldset>
                                                <legend><asp:Label ID="lblTipoTicket" runat="server" CssClass="msgMensaje"></asp:Label></legend>

                                                <table>
                                                    <tr>
                                                        <td style="width: 100px">
                                                            <asp:Panel ID="pnlTipvuelo" runat="server" Height="50px" Width="125px">
                                                                <asp:RadioButton ID="rbNacional" runat="server" Checked="True" 
                                                                    GroupName="TipVuelo" oncheckedchanged="rbNacional_CheckedChanged" 
                                                                    Text="Nacional" AutoPostBack="True" CssClass="msgMensajeBlack" />
                                                                <br />
                                                                <asp:RadioButton ID="rbInter" runat="server" GroupName="TipVuelo" 
                                                                    oncheckedchanged="rbInter_CheckedChanged" Text="Internacional" 
                                                                    AutoPostBack="True" CssClass="msgMensajeBlack" />
                                                            </asp:Panel>
                                                        </td>
                                                        <td style="width: 100px">
                                                            <asp:Panel ID="pnlTiptrasbordo" runat="server" Height="50px" Width="125px">
                                                                <asp:RadioButton ID="rbNormal" runat="server" Checked="True" 
                                                                    GroupName="TipTrans" oncheckedchanged="rbNormal_CheckedChanged" 
                                                                    Text="Normal" AutoPostBack="True" CssClass="msgMensajeBlack" />
                                                                <br />
                                                                <asp:RadioButton ID="rbTrans" runat="server" GroupName="TipTrans" 
                                                                    oncheckedchanged="rbTrans_CheckedChanged" Text="Transferencia" 
                                                                    AutoPostBack="True" CssClass="msgMensajeBlack" />
                                                            </asp:Panel>
                                                        </td>
                                                        <td style="width: 100px">
                                                            <asp:Panel ID="pnl1" runat="server" Height="50px" Width="125px">
                                                                <asp:RadioButton ID="rbAdulto" runat="server" Checked="True" GroupName="TipPas" 
                                                                    oncheckedchanged="rbAdulto_CheckedChanged" Text="Adulto" 
                                                                    AutoPostBack="True" CssClass="msgMensajeBlack" />
                                                                <br />
                                                                <asp:RadioButton ID="rbInfante" runat="server" GroupName="TipPas" 
                                                                    oncheckedchanged="rbInfante_CheckedChanged" Text="Infante" 
                                                                    AutoPostBack="True" CssClass="msgMensajeBlack" />
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                </table>
                                                
                                                </fieldset>
                                            </td>
                                            <td style="height: 75px; width: 331px; text-align: left;" align="right" valign="bottom" 
                                                        colspan="2">
                                                     
                              
                                                
                                            </td>
                                        </tr>
                                        
                                        <tr>
                                            <td>
                                                &nbsp;</td>
                                            <td style="width: 331px; text-align: left; height: 13px;" colspan="2">
                                                &nbsp;</td>
                                        </tr>

                                        <tr>
                                        <td colspan="3">
                                        <table>
                                        <tr>
                                            <td style="height: 20px;">
                                                <asp:Label ID="lblFechaActual" runat="server" CssClass="msgMensaje"></asp:Label>
                                                </td>
                                            <td style="width: 331px; text-align: left; height: 13px;" colspan="3">
                                                <asp:Label ID="lblFecActVal" runat="server" Text="Label" CssClass="msgMensajeBlack"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 20px;" >
                                                <asp:Label ID="lblCantidad" runat="server" CssClass="msgMensaje"></asp:Label></td>
                                            <td style="width: 331px; text-align: left;">
                                                <asp:TextBox ID="txtCantidad" runat="server" MaxLength="9" Width="158px" 
                                                    CssClass="textbox" ontextchanged="txtCantidad_TextChanged"></asp:TextBox>
                                                <cc2:FilteredTextBoxExtender ID="txtCantidad_FilteredTextBoxExtender" 
                                                    runat="server" Enabled="True" FilterType="Numbers" 
                                                    TargetControlID="txtCantidad">
                                                </cc2:FilteredTextBoxExtender>
                                                <asp:RequiredFieldValidator ID="rfvCantidad" runat="server" 
                                                    ControlToValidate="txtCantidad"></asp:RequiredFieldValidator>
                                                </td>
                                            <td class="alineaderecha">
                                                <asp:Label ID="lblPrecioTicket" runat="server" CssClass="msgMensaje"></asp:Label>
                                                </td>
                                            <td style="width: 331px; text-align: left;">
                                                <asp:Label ID="lblPrecTicketVal" runat="server" CssClass="msgMensajeBlack"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 20px;">
                                                <asp:Label ID="lblCompania" runat="server" CssClass="msgMensaje"></asp:Label></td>
                                            <td style="height: 28px; width: 331px; text-align: left;">
                                                <asp:DropDownList ID="ddlCompania" runat="server" AutoPostBack="True" 
                                                    onselectedindexchanged="ddlCompania_SelectedIndexChanged" CssClass="combo">
                                                </asp:DropDownList>
                                            </td>
                                            <td class="alineaderecha">
                                                &nbsp;</td>
                                            <td style="height: 28px; width: 331px; text-align: left;">
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td style="height: 20px;">
                                                <asp:Label ID="lblRepteCia" runat="server" CssClass="msgMensaje"></asp:Label></td>
                                            <td style="width: 331px; text-align: left; height: 21px;" colspan="3">                                               
                                                <asp:DropDownList ID="ddlRepteCia" runat="server" 
                                                    DataTextField="SNomRepresentante" CssClass="combo">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvRteCia" runat="server" 
                                                    ControlToValidate="ddlRepteCia"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 20px;">
                                                <asp:Label ID="lblNumVuelo" runat="server" CssClass="msgMensaje"></asp:Label>
                                                </td>
                                            <td style="width: 331px; text-align: left; height: 20px;" colspan="3">
                                                
                                                <asp:TextBox ID="txtNumVuelo" runat="server" CssClass="textbox" MaxLength="10" 
                                                    Width="152px" onkeyup="JavaScript:ToUpperCase();"></asp:TextBox>
                                                <cc2:FilteredTextBoxExtender ID="txtNumVuelo_FilteredTextBoxExtender" 
                                                    runat="server" Enabled="True" 
                                                    TargetControlID="txtNumVuelo" 
                                                    
                                                    ValidChars="abcdefghijklmnopqrstuvwxyzQWERTYUIOPASDFGHJKLZXCVBNM0123456789">
                                                </cc2:FilteredTextBoxExtender>
                                                <asp:RequiredFieldValidator ID="rfvNumVuelo" runat="server" 
                                                    ControlToValidate="txtNumVuelo"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>                                            
                                        </table>
                                        </td>
                                        </tr>
                                        <tr>
                                            <td class="style10" colspan="3">
                                                
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style4">
                                                <asp:Label ID="lblMensajeError" runat="server" CssClass="mensaje" Width="427px"></asp:Label>
                                            </td>
                                            <td style="text-align: left; " colspan="2" class="style9">
                                                &nbsp;</td>
                                        </tr>
                                        
                                        <tr>
                                            <td class="style4">
                                                &nbsp;</td>
                                            <td align="center">
                                                <uc3:OKMessageBox ID="omb" runat="server" />
                                            </td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        
                                    </table>
                                    </ContentTemplate>

                                    </asp:UpdatePanel>
                                    
                                    &nbsp;</td>
                                    <td class="SpacingGrid" style="height: 115px; width: 2%;" valign="bottom">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="8" align="center">
                                    <asp:Label ID="lblInfo" runat="server" CssClass="msgMensaje"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <uc2:PiePagina ID="PiePagina2" runat="server" />
                    </td>
                </tr>
            </table>
            
            <br />
            <br />
        </div>
        <p></p>
    </form>
</body>
</html>

