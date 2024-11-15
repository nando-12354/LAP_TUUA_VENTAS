<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Cfg_EditarListaCampo.aspx.cs" Inherits="Cfg_EditarListaCampo" %>

<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register Src="UserControl/CabeceraMenu.ascx" TagName="CabeceraMenu"  TagPrefix="uc1" %>
<%@ Register Src="UserControl/PiePagina.ascx" TagName="PiePagina" TagPrefix="uc2" %>

<%@ Register src="UserControl/OKMessageBox.ascx" tagname="OKMessageBox" tagprefix="uc3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>LAP - Configuración de Lista de Campos</title>
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
    <link href="css/Style.css" rel="stylesheet" type="text/css" />
    <link href="css/consolidated_common.css" rel="stylesheet" type="text/css" />
      <!-- #INCLUDE file="javascript/Functions.js" -->
      <!-- #INCLUDE file="javascript/validarteclaF5.js" -->
      <!-- #INCLUDE file="javascript/MantenerSesion.js" -->
    <script type="text/javascript" src="javascript/livevalidation_standalone.js"></script>
    <script type="text/javascript">
        function numero(){
          if((event.keyCode<48) || (event.keyCode>57))event.keyCode = 0;
        }
        function val_int(o){
          o.value=o.value.toString().replace(/([^0-9])/g,"");
        }
    </script>

    <link href="../css/Style.css" rel="stylesheet" type="text/css" />
    <link href="../css/Style.css" rel="stylesheet" type="text/css" />
    <link href="../css/Style.css" rel="stylesheet" type="text/css" />
    <link href="../css/Style.css" rel="stylesheet" type="text/css" />
    <link href="../css/Style.css" rel="stylesheet" type="text/css" />

    <style type="text/css">
        .style1
        {
            height: 32px;
        }
        .style2
        {
            height: 40px;
        }
        .style3
        {
            height: 44px;
        }
        .style4
        {
            height: 36px;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
            <table align="center" border="0" cellpadding="0" cellspacing="0" class="TamanoTabla">
                <tr>
                    <td colspan="2" style="height: 11px">
                        <uc1:CabeceraMenu ID="CabeceraMenu3" runat="server" />
                    </td>
                </tr>
                <tr class="formularioTitulo">
                    <td align="right" class="style1" style="text-align: left">
                        <img alt="" src="Imagenes/flecha_back.png" onclick="JavaScript: FnVolver();" /></td>
                    <td align="right">
                            <asp:Button ID="btnEliminar" runat="server" OnClick="btnEliminar_Click"
                                Width="96px" CssClass="Boton" Text="Eliminar" />
                            &nbsp;&nbsp;
                            <asp:Button ID="btnGrabar" runat="server" OnClick="btnGrabar_Click" OnClientClick="return IniEnvio(this.id)"
                                Width="96px" CssClass="Boton" Text="Grabar" />
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
        <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td >
                                                            &nbsp;</td>
                                                        <td >
                                                            <asp:Label ID="lblNomCampo" 
                            runat="server" Text="Nombre Campo:" CssClass="TextoFiltro"></asp:Label>
                                                        </td>
                                                        <td  style="width: 50%">
                                                            <asp:Label ID="lblNomCampoDesc" runat="server" Text="Label" Font-Bold="True" 
                            Font-Names="Verdana" Font-Size="X-Small" ForeColor="#008FD5" ></asp:Label>
                                                        </td>
                                                        <td style="width: 20%">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style4" >
                                                            </td>
                                                        <td class="style4">
                                                            <asp:Label ID="lblCodCampo" runat="server" CssClass="TextoFiltro" 
                                                                Text="Código Campo:"></asp:Label>
                                                        </td>
                                                        <td class="style4">
                                                            <asp:TextBox ID="txtCodCampo" runat="server" CssClass="textbox" Width="66px"></asp:TextBox>
                                                        </td>
                                                        <td class="style4">
                                                            </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style3">
                                                            </td>
                                                        <td class="style3">
                                                            <asp:Label ID="lblDescripcion" runat="server" Text="Descrioción del Valor:" 
                            CssClass="TextoFiltro"></asp:Label>
                                                        </td>
                                                        <td class="style3">
                                                            <asp:TextBox ID="txtDescripcion" runat="server" MaxLength="80" Width="262px" 
                            onkeypress="soloDescripcion()" onblur="gDescripcion(this);" CssClass="textbox"></asp:TextBox>
                                                        </td>
                                                        <td class="style3">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style2">
                                                            <uc3:OKMessageBox ID="omb" runat="server" />
                                                        </td>
                                                        <td class="style1">
                                                            &nbsp;</td>
                                                        <td>
                                                            <asp:Label ID="lblMensajeError" runat="server" CssClass="mensaje"></asp:Label>
                                                        </td>
                                                        <td>
                                                            &nbsp;</td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td class="SpacingGrid" style="height: 115px">
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <uc2:PiePagina ID="PiePagina3" runat="server" />
                    </td>
                </tr>
            </table>

        <asp:RequiredFieldValidator ID="rfvDescripcion" runat="server" ErrorMessage="Ingrese la descripción del valor" ControlToValidate="txtDescripcion" Display="None"></asp:RequiredFieldValidator>
        <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" TargetControlID="rfvDescripcion">
        </cc1:ValidatorCalloutExtender>
                <br />
    </form>
    
</body>
</html>
