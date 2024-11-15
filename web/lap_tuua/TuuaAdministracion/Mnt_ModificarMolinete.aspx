<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Mnt_ModificarMolinete.aspx.cs" Inherits="Mnt_ModificarMolinete" %>

<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="UserControl/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="uc1" %>
<%@ Register Src="UserControl/PiePagina.ascx" TagName="PiePagina" TagPrefix="uc2" %>
<%@ Register Src="UserControl/OKMessageBox.ascx" TagName="OKMessageBox" TagPrefix="uc3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>LAP - Operaciones - Modificar de Molinete</title>
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
    <link href="css/Style.css" rel="stylesheet" type="text/css" />
    <!-- #INCLUDE file="javascript/KeyPress.js" -->
    <!-- #INCLUDE file="javascript/Functions.js" -->
    <!-- #INCLUDE file="javascript/validarteclaF5.js" -->
    <!-- #INCLUDE file="javascript/MantenerSesion.js" -->

    <script language="javascript" type="text/javascript">
        function validateIp(idForm) {

            //Creamos un objeto 
            object = document.getElementById(idForm);
            valueForm = object.value;

            // Patron para la ip
            var patronIp = new RegExp("^([0-9]{1,3}).([0-9]{1,3}).([0-9]{1,3}).([0-9]{1,3})$");
            //window.alert(valueForm.search(patronIp));
            // Si la ip consta de 4 pares de números de máximo 3 dígitos
            if (valueForm.search(patronIp) == 0) {
                // Validamos si los números no son superiores al valor 255
                valores = valueForm.split(".");
                if (valores[0] <= 255 && valores[1] <= 255 && valores[2] <= 255 && valores[3] <= 255) {
                    //Ip correcta
                    object.style.color = "#000";
                    return;
                }
            }
            //Ip incorrecta
            object.style.color = "#f00";
        }

        function validarCampos() {
            var bValido = true;

            var _errDescripcion = 'La Descripción es requerido.<br>';
            var _errDBName = 'Base Datos es requerido.<br>';
            var _errUsuario = 'El Usuario es requerido.';

            for (var i = 1; i <= 1; i++) {
                document.getElementById('lblMensajeError' + i).innerHTML = "";
                var _input = document.getElementById('txtDescripcion' + i).value;
                var _inputA = document.getElementById('txtDBName' + i).value;
                var _inputB = document.getElementById('txtDBUser' + i).value;

                if (_input == null || _input.length == 0) {
                    document.getElementById('lblMensajeError' + i).innerHTML = _errDescripcion;
                    bValido = false;
                }
                if (_inputA == null || _inputA.length == 0) {
                    document.getElementById('lblMensajeError' + i).innerHTML += _errDBName;
                    bValido = false;
                }
                if (_inputB == null || _inputB.length == 0) {
                    document.getElementById('lblMensajeError' + i).innerHTML += _errUsuario;
                    bValido = false;
                }
            }
            return bValido;
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table align="center" border="0" cellpadding="0" cellspacing="0" class="TamanoTabla">
            <tr>
                <td class="Espacio1FilaTabla" colspan="2" style="height: 11px">
                    <uc1:CabeceraMenu ID="CabeceraMenu1" runat="server"></uc1:CabeceraMenu>
                </td>
            </tr>
            <tr class="formularioTitulo">
                <td align="right" style="text-align: left;width:70%">
                    &nbsp;
                </td>
                <td align="right" style="text-align: right">
                    <table border="0" cellpadding="0" cellspacing="0" align="right">
                    <tr>
                    <td style="width:120px;text-align: center">
                     <asp:Button ID="btnGrabar" runat="server" CssClass="Boton" 
                        OnClientClick="return validarCampos()" onclick="btnGrabar_Click" />                   
                    </td>
                    <td style="width:120px;text-align: center">
                        <asp:Button ID="btnCancelar" runat="server" CssClass="Boton" onclick="btnCancelar_Click" />                    
                    </td>
                    </tr>
                    </table>
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
                                <td>
                                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                                    </asp:ScriptManager>
                                    <table cellspacing="1" align="center">
                                        <tr>
                                            <td align="center">
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td rowspan="25" valign="middle">
                                               &nbsp;
                                               <asp:Image ID="Image8" runat="server" ImageUrl="~/Imagenes/molinete.jpg" 
                                                    Height="157px" Width="123px" />
                                            </td>
                                            <td>
                                               &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblCodigo1" runat="server" CssClass="TextoFiltro"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblCodMolinete1" runat="server" CssClass="TextoFiltro" Font-Bold="True"
                                                    Font-Names="Arial" Font-Size="Small" ForeColor="#008FD5"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblDescripcion1" runat="server" CssClass="TextoFiltro"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtDescripcion1" runat="server" onkeypress="JavaScript: Tecla('Alphanumeric');"
                                                    MaxLength="60" CssClass="textbox" Width="180px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblDscIp1" runat="server" CssClass="TextoFiltro"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtDscIP11" runat="server" CssClass="textbox" Width="30px" onkeypress="JavaScript: Tecla('DireccionIP');"
                                                    MaxLength="3" Enabled="False"></asp:TextBox>
                                                .<asp:TextBox ID="txtDscIP12" runat="server" CssClass="textbox" Width="30px" onkeypress="JavaScript: Tecla('DireccionIP');"
                                                    MaxLength="3" Enabled="False"></asp:TextBox>
                                                .<asp:TextBox ID="txtDscIP13" runat="server" CssClass="textbox" Width="30px" onkeypress="JavaScript: Tecla('DireccionIP');"
                                                    MaxLength="3" Enabled="False"></asp:TextBox>
                                                .<asp:TextBox ID="txtDscIP14" runat="server" CssClass="textbox" Width="30px" onkeypress="JavaScript: Tecla('DireccionIP');"
                                                    MaxLength="3" Enabled="False"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblTipoDocumento1" runat="server" CssClass="TextoFiltro"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style1">
                                                <asp:DropDownList ID="ddlTipoDocumento1" runat="server" CssClass="combo" Width="160px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblTipoVuelo1" runat="server" CssClass="TextoFiltro"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="ddlTipoVuelo1" runat="server" CssClass="combo" Width="160px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblTipoAcceso1" runat="server" CssClass="TextoFiltro"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="ddlTipoAcceso1" runat="server" CssClass="combo" Width="160px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblTipoMolinete1" runat="server" CssClass="TextoFiltro"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="ddlTipoMolinete1" runat="server" CssClass="combo" Width="160px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>                                        
                                        
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblEstado1" runat="server" CssClass="TextoFiltro"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="ddlEstado1" runat="server" CssClass="combo" Width="160px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblDBName1" runat="server" CssClass="TextoFiltro"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtDBName1" runat="server" 
                                                    MaxLength="50" CssClass="textbox" Enabled="False"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblDBUser1" runat="server" CssClass="TextoFiltro"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtDBUser1" runat="server" MaxLength="50" CssClass="textbox" 
                                                    onkeypress="JavaScript: Tecla('Alphanumeric');" Enabled="False"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblDBPassword1" runat="server" CssClass="TextoFiltro"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtDBPassword1" runat="server" onkeypress="JavaScript: Tecla('Alphanumeric');"
                                                    MaxLength="50" CssClass="textbox" TextMode="Password" Enabled="False"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblEstMaster1" runat="server" CssClass="TextoFiltro"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                <asp:RadioButton ID="rbtEstMaster1" runat="server" GroupName="EstadosMaster" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" align="center">
                                                <asp:Label ID="lblMensajeError1" runat="server" CssClass="mensaje"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                    <asp:Label ID="lblMensaje" runat="server" CssClass="mensaje"></asp:Label>
                                </td>
                                <td class="SpacingGrid" style="height: 115px">
                                </td>
                            </tr>
                        </table>
                    </div>
                    <uc2:PiePagina ID="PiePagina2" runat="server"></uc2:PiePagina>
                </td>
            </tr>
        </table>
    </div>
    <uc3:OKMessageBox ID="omb" runat="server" />
    </form>
</body>
</html>
