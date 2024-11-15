<%@ page language="C#" autoeventwireup="true" inherits="Ope_ArchivoVenta, App_Web_jlql8yfo" %>
<%@ Register Src="UserControl/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="uc1" %>
<%@ Register Src="UserControl/PiePagina.ascx" TagName="PiePagina" TagPrefix="uc2" %>
<%@ Register src="UserControl/OKMessageBox.ascx" tagname="OKMessageBox" tagprefix="uc3" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register assembly="System.Web.Extensions" namespace="System.Web.UI" tagprefix="asp" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Generacion Archivo Ventas</title>
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
</script>

    <style type="text/css">
        .style1
        {
            height: 16px;
        }
        .style2
        {
            height: 13px;
            width: 331px;
        }
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
                    <td align="right" style="text-align: left">
                        <img alt="" src="Imagenes/flecha_back.png" onclick="JavaScript: FnVolver();" /></td>
                    <td align="right">
                                    <asp:Button ID="btnGenerar" runat="server" CssClass="Boton" 
                                        OnClick="btnGenerar_Click" OnClientClick="return confirmacionLlamada()"/>
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
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>   
                            <table cellpadding="0" cellspacing="0" class="EspacioSubTablaPrincipal">
                                <tr>
                                    <td class="SpacingGrid" style="height: 115px">
                                    </td>
                                    <td class="CenterGrid" style="height: 115px">
                                    <table style="width: 100%; left: 0px; top: 0px;" 
                                            class="alineaderecha">
                                        <tr>
                                            <td colspan="2" class="style1">
                                                </td>
                                            <td colspan="5" class="style1">
                                                </td>
                                        </tr>
                                        
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblFecIni" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                            </td>
                                            <td colspan="3" style="width: 331px; text-align: left; height: 13px;">
                                                <asp:TextBox ID="txtFecIni" runat="server" CssClass="textbox" Width="150px" onfocus="this.blur();" onkeypress="JavaScript: window.event.keyCode = 0;" BackColor="#E4E2DC"></asp:TextBox>
                                                <cc2:CalendarExtender ID="txtFecIni_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtFecIni" PopupButtonID="imgbtnCalendar1" Format="dd/MM/yyyy">
                                                </cc2:CalendarExtender>
                                                <asp:ImageButton ID="imgbtnCalendar1" runat="server" Height="22px" 
                                                    ImageUrl="~/Imagenes/Calendar.bmp" Width="22px" />
                                            </td>
                                            <td colspan="2">
                                                <asp:Label ID="lblFecFin" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                            </td>
                                            <td class="alineaderecha" colspan="1">
                                                <asp:TextBox ID="txtFecFin" runat="server" CssClass="textbox" Width="150px" onfocus="this.blur();" onkeypress="JavaScript: window.event.keyCode = 0;" BackColor="#E4E2DC"></asp:TextBox>
                                                <cc2:CalendarExtender ID="txtFecFin_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtFecFin" PopupButtonID="imgbtnCalendar2" Format="dd/MM/yyyy">
                                                </cc2:CalendarExtender>
                                                <asp:ImageButton ID="imgbtnCalendar2" runat="server" Height="22px" 
                                                    ImageUrl="~/Imagenes/Calendar.bmp" Width="22px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style5">
                                                <asp:Label ID="lblFormato" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                            </td>
                                            <td colspan="2" style="text-align: left; " class="style2">
                                                <asp:RadioButton ID="rbExcel" runat="server" Checked="True" 
                                                    GroupName="formato" />
                                            </td>
                                            <td colspan="2" style="width: 331px; text-align: left; height: 13px;">
                                                <asp:RadioButton ID="rbTexto" runat="server" GroupName="formato" 
                                                    />
                                            </td>
                                            <td colspan="2" style="width: 331px; text-align: left; height: 13px;">
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblArchivo" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                            </td>
                                            <td style="width: 331px; text-align: left; height: 13px;" colspan="6">
                                                <asp:RequiredFieldValidator ID="rfvModa" runat="server" 
                                                    ControlToValidate="txtFecIni" ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style5">
                                                &nbsp;</td>
                                            <td colspan="6" style="width: 331px; text-align: left; height: 13px;">
                                                <asp:TreeView ID="tvwModalidad" runat="server" CssClass="arbol" 
                                                    ShowCheckBoxes="All" ShowExpandCollapse="False" ShowLines="True" 
                                                    SkipLinkText="" Width="244px">
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
                                        &nbsp;</td>
                                    <td align="center" colspan="6">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style5">
                                        &nbsp;</td>
                                    <td colspan="6" style="width: 331px; text-align: left; height: 13px;">
                                        <asp:Label ID="lblGrilla" runat="server" CssClass="msgMensaje"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style4">
                                        &nbsp;</td>
                                    <td class="style9" colspan="6" style="text-align: left; ">
                                        &nbsp;</td>
                                </tr>
                            </table>
                                &nbsp;
                                <td class="SpacingGrid" style="height: 115px; width: 2%;" valign="bottom">
                                </td>
                            </ContentTemplate>
                            </asp:UpdatePanel>
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
    </form>
</body>
</html>