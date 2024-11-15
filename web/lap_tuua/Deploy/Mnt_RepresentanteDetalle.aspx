<%@ page language="C#" autoeventwireup="true" inherits="Mnt_RepresentanteDetalle, App_Web_ehzg6gwo" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>LAP- Representante</title>
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
    <!-- #INCLUDE file="javascript/KeyPress.js" -->
     <!-- #INCLUDE file="javascript/Functions.js" -->
     <!-- #INCLUDE file="javascript/validarteclaF5.js" -->
    <meta http-equiv="pragma" content="no-cache">
    <base target="_self" />
    <link href="css/Style.css" rel="stylesheet" type="text/css" />
    
    <script language="javascript" type="text/javascript">
    function CloseFormOK()
    {
        window.returnValue = true;
        //window.opener.location.reload();
        self.close();
    }
     function CloseFormCancel()
    {
        window.returnValue = false;
        self.close();
    }
    </script>
    
    
    <style type="text/css">
        .style1
        {
        }
        .style2
        {
        }
        .style6
        {
        }
        .style10
        {
            width: 664px;
        }
        .style11
        {
        }
        .style12
        {
            width: 7px;
        }
        .style13
        {
            width: 1199px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table style="width:386px;">
        <tr>
            <td class="style12">
                &nbsp;</td>
            <td colspan="3">
                <asp:Label ID="lblTituloRepresentante" runat="server" 
                    CssClass="titulosecundario"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style12">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
            <td colspan="3">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style12">
                &nbsp;</td>
            <td class="style6" colspan="2">
                <asp:Label ID="lblCodigo" runat="server" CssClass="TextoEtiqueta"></asp:Label>
            </td>
            <td class="style2">
                <asp:Label ID="lblDscCodigo" runat="server" CssClass="TextoCampo"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style12">
                &nbsp;</td>
            <td class="style6" colspan="2">
                <asp:Label ID="lblNombre" runat="server" CssClass="TextoEtiqueta"></asp:Label>
            </td>
            <td class="style2">
                <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox" Height="20px" onkeypress="JavaScript: Tecla('Character');" onblur="gDescripcionNombre(this)" 
                    Width="198px"></asp:TextBox>
                <br />
                <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ControlToValidate="txtNombre" 
                    Display="Dynamic"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="style12">
                &nbsp;</td>
            <td class="style6" colspan="2">
                <asp:Label ID="lblApellido" runat="server" CssClass="TextoEtiqueta"></asp:Label>
            </td>
            <td class="style2">
                <asp:TextBox ID="txtApellido" runat="server" CssClass="textbox" Height="20px" onkeypress="JavaScript: Tecla('Character');" onblur="gDescripcionNombre(this)"    Width="198px"></asp:TextBox>
                <br />
                <asp:RequiredFieldValidator ID="rfvApellido" runat="server" ControlToValidate="txtApellido" 
                    Display="Dynamic"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="style12">
                &nbsp;</td>
            <td class="style6" colspan="2">
                <asp:Label ID="lblCargo" runat="server" CssClass="TextoEtiqueta"></asp:Label>
            </td>
            <td class="style2">
                <asp:TextBox ID="txtCargo" runat="server" CssClass="textbox" Height="20px" onkeypress="JavaScript: Tecla('Character');" onblur="gDescripcionNombre(this)"
                    Width="153px"></asp:TextBox>
                <br />
                <asp:RequiredFieldValidator ID="rfvCargo" runat="server" ControlToValidate="txtCargo" 
                    Display="Dynamic"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="style12">
                &nbsp;</td>
            <td class="style6" colspan="2">
                <asp:Label ID="lblTipoDocumento" runat="server" CssClass="TextoEtiqueta"></asp:Label>
            </td>
            <td class="style2">
                <asp:DropDownList ID="ddlTipoDocumento" runat="server" CssClass="combo">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="style12">
                &nbsp;</td>
            <td class="style6" colspan="2">
                <asp:Label ID="lblNumDocumento" runat="server" CssClass="TextoEtiqueta"></asp:Label>
            </td>
            <td class="style2">
                <asp:TextBox ID="txtNumDocumento" runat="server" CssClass="textbox" 
                    onkeypress="JavaScript: Tecla('Double');" onblur="val_int(this)" MaxLength="11"></asp:TextBox>
                <br />
                <asp:RequiredFieldValidator ID="rfvNumdocumento" runat="server" 
                    ControlToValidate="txtNumDocumento" Display="Dynamic"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="style12">
                &nbsp;</td>
            <td class="style6" colspan="2">
                <asp:Label ID="lblEstado" runat="server" CssClass="TextoEtiqueta"></asp:Label>
            </td>
            <td class="style2">
                <asp:DropDownList ID="ddlEstado" runat="server" CssClass="combo">
                </asp:DropDownList>
                <br />
            </td>
        </tr>
        <tr>
            <td class="style12">
                &nbsp;</td>
            <td class="style6" colspan="2">
                &nbsp;</td>
            <td class="style2">
            <asp:Label ID="lblMensajeError" runat="server" CssClass="mensaje" Height="16px" 
                Width="225px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style12">
                &nbsp;</td>
            <td class="style6" colspan="2">
                &nbsp;</td>
            <td class="style2">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style12">
                &nbsp;</td>
            <td class="style1" colspan="3">
                <asp:Label ID="lblPermisosRepresentante" runat="server" 
                    CssClass="TextoEtiqueta"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style12">
                &nbsp;</td>
            <td class="style6">
                &nbsp;</td>
            <td class="style13">
                &nbsp;</td>
            <td class="style10">
                <asp:Panel ID="pnlPermisos" runat="server" Height="106px" Width="163px">
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td class="style12">
                &nbsp;</td>
            <td class="style6">
                &nbsp;</td>
            <td class="style13">
                &nbsp;</td>
            <td class="style10">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style12">
                &nbsp;</td>
            <td class="style6">
                &nbsp;</td>
            <td class="style13">
                &nbsp;</td>
            <td class="style11">
                    <asp:Button ID="btnAceptar" runat="server" CssClass="Boton" Height="20px" 
                        Width="78px" onclick="btnAceptar_Click" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnCancelar" runat="server" CssClass="Boton" Height="21px" 
                        Width="74px" onclick="btnCancelar_Click" CausesValidation="False" />
            </td>
        </tr>
        <tr>
            <td class="style12">
                &nbsp;</td>
            <td class="style6" colspan="3">
                &nbsp;</td>
        </tr>
        </table>
    </form>
</body>
</html>
