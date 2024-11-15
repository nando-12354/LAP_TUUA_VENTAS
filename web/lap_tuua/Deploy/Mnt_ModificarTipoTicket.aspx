<%@ page language="C#" autoeventwireup="true" inherits="Mnt_ModificarTipoTicket, App_Web_ehzg6gwo" %>
<%@ Register Src="UserControl/CabeceraMenu.ascx" TagName="CabeceraMenu"
    TagPrefix="uc1" %>
<%@ Register Src="UserControl/PiePagina.ascx" TagName="PiePagina" TagPrefix="uc2" %>

<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>



<%@ Register src="UserControl/OKMessageBox.ascx" tagname="OKMessageBox" tagprefix="uc3" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>LAP- Modificar Tipo de Ticket</title>
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
    <link href="css/Style.css" rel="stylesheet" type="text/css" />
    <!-- #INCLUDE file="javascript/KeyPress.js" -->
    <!-- #INCLUDE file="javascript/validarteclaF5.js" -->
    <!-- #INCLUDE file="javascript/Functions.js" -->
   <!-- #INCLUDE file="javascript/MantenerSesion.js" -->

    
    <style type="text/css">
        .style1
        {
            width: 198px;
        }
        .style2
        {
            height: 24px;
            width: 234px;
        }
        .style3
        {
            width: 234px;
        }
        .style4
        {
            width: 353px;
        }
        .style5
        {
            height: 24px;
            width: 353px;
        }
        .style6
        {
            width: 11px;
        }
        .style7
        {
            height: 24px;
            width: 11px;
        }
    </style>
    
   <script language ="javascript" type="text/javascript">

       var accionSave = false;

       function validarAccion()
       {
           var mConfirmacion = document.forms[0].hConfirmacion.value;

           var mNombre = document.forms[0].txtNombre.value;
           mNombre = mNombre.replace(/^\s*|\s*$/g, "");

           if (mNombre == "") {
               document.getElementById("lblMensajeError").innerHTML = "Ingrese el nombre de Tipo Ticket";
               return false;
           }

           if (confirm(mConfirmacion)) {
               accionSave = true;
               return true;
           }
           else {
               accionSave = false;
               return false;
           }
       }
       
       
       function beginRequest(sender, args)
       {
           if (!sender.get_isInAsyncPostBack()) {
               if (accionSave) {
                   document.getElementById('btnActualizar').disabled = true;
                   document.body.style.cursor = 'wait';

               }
           }
       }


       function endRequest(sender, args)
       {
           if (!sender.get_isInAsyncPostBack()) {
               if (accionSave) {
                   document.getElementById('btnActualizar').disabled = false;
                   document.body.style.cursor = 'default'

                   accionSave = false;
               }
           }
       }
       
       
       function comprobarFormulario()
       {
           if (document.getElementById("txtNombre").value=="")
           {
                document.getElementById("lblMensajeError").innerHTML="Ingrese nombre de tipo ticket";
                return false;
           }
           
           if (document.getElementById("txtNombre").value!="")
           {
                document.getElementById("lblMensajeError").innerHTML="";
                return true;
           }          
          return true;
       }
       
  </script>    
    
</head>
<body>
    <form id="form1" runat="server" onsubmit="return comprobarFormulario()">
    <asp:HiddenField ID="hConfirmacion" runat="server" Value="Confirmar Accion" />
    <div>
        <div>
            <div style="background-image: url(Imagenes/back.gif)">
                <table align="center" border="0" cellpadding="0" cellspacing="0" class="TamanoTabla">
                    <tr>
                        <td class="Espacio1FilaTabla" colspan="2" style="height: 11px">
                            <uc1:cabeceramenu id="CabeceraMenu2" runat="server"></uc1:cabeceramenu>
                        </td>
                    </tr>
                    <tr class="formularioTitulo">
                    <td align="right" class="style1" style="text-align: left">
                        <img alt="" src="Imagenes/flecha_back.png" onclick="JavaScript: FnVolver();" /></td>
                    <td align="right" >
                        <asp:Button ID="btnActualizar" runat="server" CssClass="Boton" OnClick="btnActualizar_Click" OnClientClick="return validarAccion()"/>
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
                                <table cellpadding="0" cellspacing="0" class="EspacioSubTablaPrincipal">
                                    <tr>
                                        <td class="SpacingGrid" style="height: 115px">
                                        </td>
                                        <td class="CenterGrid" style="height: 115px">
                                        <asp:ScriptManager id="ScriptManager1" runat="server">
                                        </asp:ScriptManager>
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <table style="width: 100%">
                                                        <tr>
                                                            <td style="text-align: left" class="style3">
                                                                &nbsp;</td>
                                                            <td style="text-align: left" class="style6">
                                                                <asp:Label ID="lblCodigo" runat="server" CssClass="TextoEtiqueta" Width="47px"></asp:Label>
                                                            </td>
                                                            <td style="text-align: left" class="style4">
                                                                &nbsp;<asp:Label ID="txtCodigo" runat="server" CssClass="TextoCampo" ForeColor="#008FD5" Font-Names="Verdana"></asp:Label>
                                                            </td>
                                                            <td style="width: 20%;">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align: left" class="style2">&nbsp;</td>
                                                            <td style="text-align: left" class="style7">
                                                                <asp:Label ID="lblTipoPasajero" runat="server" CssClass="TextoEtiqueta" Width="95px"></asp:Label>
                                                            </td>
                                                            <td style="text-align: left" class="style5">
                                                                <asp:Label ID="txtTipoPasajero" runat="server" Width="151px" CssClass="TextoCampo" ForeColor="#008FD5" Font-Names="Verdana"></asp:Label>
                                                            </td>
                                                            <td style="height: 24px; text-align: left">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align: left" class="style2">
                                                                &nbsp;</td>
                                                            <td style="text-align: left" class="style7">
                                                                <asp:Label ID="lblTipoVuelo" runat="server" CssClass="TextoEtiqueta" Width="94px"></asp:Label>
                                                            </td>
                                                            <td style="text-align: left" class="style5">
                                                                <asp:Label ID="txtTipoVuelo" runat="server" Width="153px" CssClass="TextoCampo" ForeColor="#008FD5" Font-Names="Verdana"></asp:Label>
                                                            </td>
                                                            <td style="height: 24px; text-align: left">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align: left" class="style2">
                                                                &nbsp;</td>
                                                            <td style="text-align: left" class="style7">
                                                                <asp:Label ID="lblTipoTransbordo" runat="server" CssClass="TextoEtiqueta" Width="96px"></asp:Label>
                                                            </td>
                                                            <td style="text-align: left" class="style5">
                                                            <asp:Label ID="txtTipoTransbordo" runat="server" Width="152px" CssClass="TextoCampo" ForeColor="#008FD5" Font-Names="Verdana"></asp:Label>
                                                            </td>
                                                            <td style="height: 24px; text-align: left">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align: left" class="style2">
                                                                &nbsp;</td>
                                                            <td style="text-align: left" class="style7">
                                                                <asp:Label ID="lblNombre" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                                            </td>
                                                            <td style="text-align: left" class="style5">
                                                                <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox" MaxLength="50" onkeypress="JavaScript: Tecla('Alphanumeric');" onblur="abc(this)" Width="227px"></asp:TextBox>
                                                            </td>
                                                            <td style="height: 24px; text-align: left">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align: left" class="style2">
                                                                <uc3:OKMessageBox ID="omb" runat="server" />
                                                            </td>
                                                            <td style="text-align: left" class="style7">
                                                                <asp:Label ID="lblEstado" runat="server" CssClass="TextoEtiqueta" Width="47px"></asp:Label>
                                                            </td>
                                                            <td style="text-align: left" class="style5">
                                                                <asp:DropDownList ID="ddlEstado" runat="server" CssClass="combo" Width="175px">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td style="height: 24px; text-align: left">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <asp:Label ID="lblMensajeError" runat="server" CssClass="mensaje" Width="355px"></asp:Label>
                                                </ContentTemplate>
                                                <triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="btnActualizar" EventName="Click" />
                                                </triggers>
                                            </asp:UpdatePanel>
                                            
                                            
                                        
                                        </td>
                                        <td class="SpacingGrid" style="height: 115px">
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <uc2:piepagina id="PiePagina2" runat="server"></uc2:piepagina>
                        </td>
                    </tr>
                </table>
                
                    <asp:DropDownList ID="ddlTipoPasajero" runat="server" CssClass="combo" Visible="False" Width="13px">
                    </asp:DropDownList><asp:DropDownList ID="ddlTipoVuelo" runat="server" CssClass="combo" Visible="False" Width="11px">
                    </asp:DropDownList><asp:DropDownList ID="ddlTipoTransbordo" runat="server" CssClass="combo" Visible="False" Width="6px">
                    </asp:DropDownList><br />
            </div>
        </div>
    
    </div>
    </form>
</body>
</html>
