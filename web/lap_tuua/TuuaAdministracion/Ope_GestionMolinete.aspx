<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Ope_GestionMolinete.aspx.cs"
    Inherits="Ope_GestionMolinete" %>

<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="UserControl/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="uc1" %>
<%@ Register Src="UserControl/PiePagina.ascx" TagName="PiePagina" TagPrefix="uc2" %>
<%@ Register Src="UserControl/OKMessageBox.ascx" TagName="OKMessageBox" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LAP - Operaciones - Gestión de Molinete</title>
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
    <link href="css/Style.css" rel="stylesheet" type="text/css" />
    <!-- #INCLUDE file="javascript/KeyPress.js" -->
    <!-- #INCLUDE file="javascript/Functions.js" -->
    <!-- #INCLUDE file="javascript/validarteclaF5.js" -->
    <!-- #INCLUDE file="javascript/MantenerSesion.js" -->
</head>
<body>
    <form id="form1" runat="server">

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

            for (var i = 1; i <= 6; i++) {
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

    <div>
        <table align="center" border="0" cellpadding="0" cellspacing="0" class="TamanoTabla">
            <tr>
                <td class="Espacio1FilaTabla" colspan="2" style="height: 11px">
                    <uc1:CabeceraMenu ID="CabeceraMenu1" runat="server"></uc1:CabeceraMenu>
                </td>
            </tr>
            <tr class="formularioTitulo">
                <td align="right" style="text-align: left">
                    &nbsp;
                </td>
                <td align="right" style="text-align: right">
                    <asp:Button ID="btnGrabar" runat="server" CssClass="Boton" OnClientClick="return validarCampos()" OnClick="btnGrabar_Click" />
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
                                    <table class="EspacioSubTablaPrincipal" cellspacing="1">
                                        <tr>
                                            <td align="center">
                                                <asp:Image ID="Image8" runat="server" ImageUrl="~/Imagenes/molinete.jpg" />
                                            </td>
                                            <td align="center">
                                                <asp:Image ID="Image9" runat="server" ImageUrl="~/Imagenes/molinete.jpg" />
                                            </td>
                                            <td align="center">
                                                <asp:Image ID="Image10" runat="server" ImageUrl="~/Imagenes/molinete.jpg" />
                                            </td>
                                            <td align="center">
                                                <asp:Image ID="Image11" runat="server" ImageUrl="~/Imagenes/molinete.jpg" />
                                            </td>
                                            <td align="center">
                                                <asp:Image ID="Image12" runat="server" ImageUrl="~/Imagenes/molinete.jpg" />
                                            </td>
                                            <td align="center">
                                                <asp:Image ID="Image13" runat="server" ImageUrl="~/Imagenes/molinete.jpg" />
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
                                                &nbsp;
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblCodigo1" runat="server" CssClass="TextoFiltro"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCodigo2" runat="server" CssClass="TextoFiltro"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCodigo3" runat="server" CssClass="TextoFiltro"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCodigo4" runat="server" CssClass="TextoFiltro"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCodigo5" runat="server" CssClass="TextoFiltro"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCodigo6" runat="server" CssClass="TextoFiltro"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblCodMolinete1" runat="server" CssClass="TextoFiltro" Font-Bold="True"
                                                    Font-Names="Arial" Font-Size="Small" ForeColor="#008FD5"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCodMolinete2" runat="server" CssClass="TextoFiltro" Font-Bold="True"
                                                    Font-Names="Arial" Font-Size="Small" ForeColor="#008FD5"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCodMolinete3" runat="server" CssClass="TextoFiltro" Font-Bold="True"
                                                    Font-Names="Arial" Font-Size="Small" ForeColor="#008FD5"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCodMolinete4" runat="server" CssClass="TextoFiltro" Font-Bold="True"
                                                    Font-Names="Arial" Font-Size="Small" ForeColor="#008FD5"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCodMolinete5" runat="server" CssClass="TextoFiltro" Font-Bold="True"
                                                    Font-Names="Arial" Font-Size="Small" ForeColor="#008FD5"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCodMolinete6" runat="server" CssClass="TextoFiltro" Font-Bold="True"
                                                    Font-Names="Arial" Font-Size="Small" ForeColor="#008FD5"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblDescripcion1" runat="server" CssClass="TextoFiltro"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblDescripcion2" runat="server" CssClass="TextoFiltro"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblDescripcion3" runat="server" CssClass="TextoFiltro"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblDescripcion4" runat="server" CssClass="TextoFiltro"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblDescripcion5" runat="server" CssClass="TextoFiltro"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblDescripcion6" runat="server" CssClass="TextoFiltro"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtDescripcion1" runat="server" onkeypress="JavaScript: Tecla('Alphanumeric');"
                                                    MaxLength="60" CssClass="textbox"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDescripcion2" runat="server" onkeypress="JavaScript: Tecla('Alphanumeric');"
                                                    MaxLength="60" CssClass="textbox"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDescripcion3" runat="server" onkeypress="JavaScript: Tecla('Alphanumeric');"
                                                    MaxLength="60" CssClass="textbox"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDescripcion4" runat="server" onkeypress="JavaScript: Tecla('Alphanumeric');"
                                                    MaxLength="60" CssClass="textbox"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDescripcion5" runat="server" onkeypress="JavaScript: Tecla('Alphanumeric');"
                                                    MaxLength="60" CssClass="textbox"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDescripcion6" runat="server" onkeypress="JavaScript: Tecla('Alphanumeric');"
                                                    MaxLength="60" CssClass="textbox"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblDscIp1" runat="server" CssClass="TextoFiltro"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblDscIp2" runat="server" CssClass="TextoFiltro"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblDscIp3" runat="server" CssClass="TextoFiltro"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblDscIp4" runat="server" CssClass="TextoFiltro"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblDscIp5" runat="server" CssClass="TextoFiltro"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblDscIp6" runat="server" CssClass="TextoFiltro"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtDscIP11" runat="server" CssClass="textbox" Width="30px" onkeypress="JavaScript: Tecla('DireccionIP');"
                                                    MaxLength="3"></asp:TextBox>
                                                .<asp:TextBox ID="txtDscIP12" runat="server" CssClass="textbox" Width="30px" onkeypress="JavaScript: Tecla('DireccionIP');"
                                                    MaxLength="3"></asp:TextBox>
                                                .<asp:TextBox ID="txtDscIP13" runat="server" CssClass="textbox" Width="30px" onkeypress="JavaScript: Tecla('DireccionIP');"
                                                    MaxLength="3"></asp:TextBox>
                                                .<asp:TextBox ID="txtDscIP14" runat="server" CssClass="textbox" Width="30px" onkeypress="JavaScript: Tecla('DireccionIP');"
                                                    MaxLength="3"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDscIP21" runat="server" CssClass="textbox" Width="30px" onkeypress="JavaScript: Tecla('DireccionIP');"
                                                    MaxLength="3"></asp:TextBox>
                                                .<asp:TextBox ID="txtDscIP22" runat="server" CssClass="textbox" Width="30px" onkeypress="JavaScript: Tecla('DireccionIP');"
                                                    MaxLength="3"></asp:TextBox>
                                                .<asp:TextBox ID="txtDscIP23" runat="server" CssClass="textbox" Width="30px" onkeypress="JavaScript: Tecla('DireccionIP');"
                                                    MaxLength="3"></asp:TextBox>
                                                .<asp:TextBox ID="txtDscIP24" runat="server" CssClass="textbox" Width="30px" onkeypress="JavaScript: Tecla('DireccionIP');"
                                                    MaxLength="3"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDscIP31" runat="server" CssClass="textbox" Width="30px" onkeypress="JavaScript: Tecla('DireccionIP');"
                                                    MaxLength="3"></asp:TextBox>
                                                .<asp:TextBox ID="txtDscIP32" runat="server" CssClass="textbox" Width="30px" onkeypress="JavaScript: Tecla('DireccionIP');"
                                                    MaxLength="3"></asp:TextBox>
                                                .<asp:TextBox ID="txtDscIP33" runat="server" CssClass="textbox" Width="30px" onkeypress="JavaScript: Tecla('DireccionIP');"
                                                    MaxLength="3"></asp:TextBox>
                                                .<asp:TextBox ID="txtDscIP34" runat="server" CssClass="textbox" Width="30px" onkeypress="JavaScript: Tecla('DireccionIP');"
                                                    MaxLength="3"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDscIP41" runat="server" CssClass="textbox" Width="30px" onkeypress="JavaScript: Tecla('DireccionIP');"
                                                    MaxLength="3"></asp:TextBox>
                                                .<asp:TextBox ID="txtDscIP42" runat="server" CssClass="textbox" Width="30px" onkeypress="JavaScript: Tecla('DireccionIP');"
                                                    MaxLength="3"></asp:TextBox>
                                                .<asp:TextBox ID="txtDscIP43" runat="server" CssClass="textbox" Width="30px" onkeypress="JavaScript: Tecla('DireccionIP');"
                                                    MaxLength="3"></asp:TextBox>
                                                .<asp:TextBox ID="txtDscIP44" runat="server" CssClass="textbox" Width="30px" onkeypress="JavaScript: Tecla('DireccionIP');"
                                                    MaxLength="3"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDscIP51" runat="server" CssClass="textbox" Width="30px" onkeypress="JavaScript: Tecla('DireccionIP');"
                                                    MaxLength="3"></asp:TextBox>
                                                .<asp:TextBox ID="txtDscIP52" runat="server" CssClass="textbox" Width="30px" onkeypress="JavaScript: Tecla('DireccionIP');"
                                                    MaxLength="3"></asp:TextBox>
                                                .<asp:TextBox ID="txtDscIP53" runat="server" CssClass="textbox" Width="30px" onkeypress="JavaScript: Tecla('DireccionIP');"
                                                    MaxLength="3"></asp:TextBox>
                                                .<asp:TextBox ID="txtDscIP54" runat="server" CssClass="textbox" Width="30px" onkeypress="JavaScript: Tecla('DireccionIP');"
                                                    MaxLength="3"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDscIP61" runat="server" CssClass="textbox" Width="30px" onkeypress="JavaScript: Tecla('DireccionIP');"
                                                    MaxLength="3"></asp:TextBox>
                                                .<asp:TextBox ID="txtDscIP62" runat="server" CssClass="textbox" Width="30px" onkeypress="JavaScript: Tecla('DireccionIP');"
                                                    MaxLength="3"></asp:TextBox>
                                                .<asp:TextBox ID="txtDscIP63" runat="server" CssClass="textbox" Width="30px" onkeypress="JavaScript: Tecla('DireccionIP');"
                                                    MaxLength="3"></asp:TextBox>
                                                .<asp:TextBox ID="txtDscIP64" runat="server" CssClass="textbox" Width="30px" onkeypress="JavaScript: Tecla('DireccionIP');"
                                                    MaxLength="3"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblTipoDocumento1" runat="server" CssClass="TextoFiltro"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblTipoDocumento2" runat="server" CssClass="TextoFiltro"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblTipoDocumento3" runat="server" CssClass="TextoFiltro"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblTipoDocumento4" runat="server" CssClass="TextoFiltro"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblTipoDocumento5" runat="server" CssClass="TextoFiltro"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblTipoDocumento6" runat="server" CssClass="TextoFiltro"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style1">
                                                <asp:DropDownList ID="ddlTipoDocumento1" runat="server" CssClass="combo" Width="120px">
                                                </asp:DropDownList>
                                            </td>
                                            <td class="style1">
                                                <asp:DropDownList ID="ddlTipoDocumento2" runat="server" CssClass="combo" Width="120px">
                                                </asp:DropDownList>
                                            </td>
                                            <td class="style1">
                                                <asp:DropDownList ID="ddlTipoDocumento3" runat="server" CssClass="combo" Width="120px">
                                                </asp:DropDownList>
                                            </td>
                                            <td class="style1">
                                                <asp:DropDownList ID="ddlTipoDocumento4" runat="server" CssClass="combo" Width="120px">
                                                </asp:DropDownList>
                                            </td>
                                            <td class="style1">
                                                <asp:DropDownList ID="ddlTipoDocumento5" runat="server" CssClass="combo" Width="120px">
                                                </asp:DropDownList>
                                            </td>
                                            <td class="style1">
                                                <asp:DropDownList ID="ddlTipoDocumento6" runat="server" CssClass="combo" Width="120px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblTipoVuelo1" runat="server" CssClass="TextoFiltro"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblTipoVuelo2" runat="server" CssClass="TextoFiltro"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblTipoVuelo3" runat="server" CssClass="TextoFiltro"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblTipoVuelo4" runat="server" CssClass="TextoFiltro"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblTipoVuelo5" runat="server" CssClass="TextoFiltro"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblTipoVuelo6" runat="server" CssClass="TextoFiltro"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="ddlTipoVuelo1" runat="server" CssClass="combo" Width="120px">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlTipoVuelo2" runat="server" CssClass="combo" Width="120px">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlTipoVuelo3" runat="server" CssClass="combo" Width="120px">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlTipoVuelo4" runat="server" CssClass="combo" Width="120px">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlTipoVuelo5" runat="server" CssClass="combo" Width="120px">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlTipoVuelo6" runat="server" CssClass="combo" Width="120px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblTipoAcceso1" runat="server" CssClass="TextoFiltro"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblTipoAcceso2" runat="server" CssClass="TextoFiltro"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblTipoAcceso3" runat="server" CssClass="TextoFiltro"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblTipoAcceso4" runat="server" CssClass="TextoFiltro"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblTipoAcceso5" runat="server" CssClass="TextoFiltro"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblTipoAcceso6" runat="server" CssClass="TextoFiltro"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="ddlTipoAcceso1" runat="server" CssClass="combo" Width="120px">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlTipoAcceso2" runat="server" CssClass="combo" Width="120px">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlTipoAcceso3" runat="server" CssClass="combo" Width="120px">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlTipoAcceso4" runat="server" CssClass="combo" Width="120px">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlTipoAcceso5" runat="server" CssClass="combo" Width="120px">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlTipoAcceso6" runat="server" CssClass="combo" Width="120px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblEstado1" runat="server" CssClass="TextoFiltro"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblEstado2" runat="server" CssClass="TextoFiltro"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblEstado3" runat="server" CssClass="TextoFiltro"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblEstado4" runat="server" CssClass="TextoFiltro"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblEstado5" runat="server" CssClass="TextoFiltro"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblEstado6" runat="server" CssClass="TextoFiltro"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="ddlEstado1" runat="server" CssClass="combo" Width="120px">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlEstado2" runat="server" CssClass="combo" Width="120px">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlEstado3" runat="server" CssClass="combo" Width="120px">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlEstado4" runat="server" CssClass="combo" Width="120px">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlEstado5" runat="server" CssClass="combo" Width="120px">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlEstado6" runat="server" CssClass="combo" Width="120px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblDBName1" runat="server" CssClass="TextoFiltro"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblDBName2" runat="server" CssClass="TextoFiltro"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblDBName3" runat="server" CssClass="TextoFiltro"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblDBName4" runat="server" CssClass="TextoFiltro"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblDBName5" runat="server" CssClass="TextoFiltro"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblDBName6" runat="server" CssClass="TextoFiltro"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtDBName1" runat="server" 
                                                    MaxLength="50" CssClass="textbox"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDBName2" runat="server" 
                                                    MaxLength="50" CssClass="textbox"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDBName3" runat="server" 
                                                    MaxLength="50" CssClass="textbox"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDBName4" runat="server" 
                                                    MaxLength="50" CssClass="textbox"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDBName5" runat="server" 
                                                    MaxLength="50" CssClass="textbox"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDBName6" runat="server" 
                                                    MaxLength="50" CssClass="textbox"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblDBUser1" runat="server" CssClass="TextoFiltro"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblDBUser2" runat="server" CssClass="TextoFiltro"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblDBUser3" runat="server" CssClass="TextoFiltro"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblDBUser4" runat="server" CssClass="TextoFiltro"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblDBUser5" runat="server" CssClass="TextoFiltro"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblDBUser6" runat="server" CssClass="TextoFiltro"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtDBUser1" runat="server" MaxLength="50" CssClass="textbox" onkeypress="JavaScript: Tecla('Alphanumeric');"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDBUser2" runat="server" MaxLength="50" onkeypress="JavaScript: Tecla('Alphanumeric');" CssClass="textbox"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDBUser3" runat="server" MaxLength="50" onkeypress="JavaScript: Tecla('Alphanumeric');" CssClass="textbox"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDBUser4" runat="server" MaxLength="50" onkeypress="JavaScript: Tecla('Alphanumeric');" CssClass="textbox"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDBUser5" runat="server" MaxLength="50" onkeypress="JavaScript: Tecla('Alphanumeric');" CssClass="textbox"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDBUser6" runat="server" MaxLength="50" onkeypress="JavaScript: Tecla('Alphanumeric');" CssClass="textbox"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblDBPassword1" runat="server" CssClass="TextoFiltro"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblDBPassword2" runat="server" CssClass="TextoFiltro"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblDBPassword3" runat="server" CssClass="TextoFiltro"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblDBPassword4" runat="server" CssClass="TextoFiltro"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblDBPassword5" runat="server" CssClass="TextoFiltro"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblDBPassword6" runat="server" CssClass="TextoFiltro"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtDBPassword1" runat="server" onkeypress="JavaScript: Tecla('Alphanumeric');"
                                                    MaxLength="50" CssClass="textbox" TextMode="Password"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDBPassword2" runat="server" onkeypress="JavaScript: Tecla('Alphanumeric');"
                                                    MaxLength="50" CssClass="textbox" TextMode="Password"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDBPassword3" runat="server" onkeypress="JavaScript: Tecla('Alphanumeric');"
                                                    MaxLength="50" CssClass="textbox" TextMode="Password"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDBPassword4" runat="server" onkeypress="JavaScript: Tecla('Alphanumeric');"
                                                    MaxLength="50" CssClass="textbox" TextMode="Password"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDBPassword5" runat="server" onkeypress="JavaScript: Tecla('Alphanumeric');"
                                                    MaxLength="50" CssClass="textbox" TextMode="Password"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDBPassword6" runat="server" onkeypress="JavaScript: Tecla('Alphanumeric');"
                                                    MaxLength="50" CssClass="textbox" TextMode="Password"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblEstMaster1" runat="server" CssClass="TextoFiltro"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblEstMaster2" runat="server" CssClass="TextoFiltro"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblEstMaster3" runat="server" CssClass="TextoFiltro"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblEstMaster4" runat="server" CssClass="TextoFiltro"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblEstMaster5" runat="server" CssClass="TextoFiltro"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblEstMaster6" runat="server" CssClass="TextoFiltro"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                <asp:RadioButton ID="rbtEstMaster1" runat="server" GroupName="EstadosMaster" />
                                            </td>
                                            <td align="center">
                                                <asp:RadioButton ID="rbtEstMaster2" runat="server" GroupName="EstadosMaster" />
                                            </td>
                                            <td align="center">
                                                <asp:RadioButton ID="rbtEstMaster3" runat="server" GroupName="EstadosMaster" />
                                            </td>
                                            <td align="center">
                                                <asp:RadioButton ID="rbtEstMaster4" runat="server" GroupName="EstadosMaster" />
                                            </td>
                                            <td align="center">
                                                <asp:RadioButton ID="rbtEstMaster5" runat="server" GroupName="EstadosMaster" />
                                            </td>
                                            <td align="center">
                                                <asp:RadioButton ID="rbtEstMaster6" runat="server" GroupName="EstadosMaster" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblMensajeError1" runat="server" CssClass="mensaje" Width="180px"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblMensajeError2" runat="server" CssClass="mensaje" Width="180px"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblMensajeError3" runat="server" CssClass="mensaje" Width="180px"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblMensajeError4" runat="server" CssClass="mensaje" Width="180px"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblMensajeError5" runat="server" CssClass="mensaje" Width="180px"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblMensajeError6" runat="server" CssClass="mensaje" Width="180px"></asp:Label>
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
