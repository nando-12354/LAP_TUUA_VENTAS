<%@ page language="C#" autoeventwireup="true" inherits="Mnt_ModificarModalidadVenta, App_Web_tx1el90t" %>
<%@ Register Src="UserControl/CabeceraMenu.ascx" TagName="CabeceraMenu"
    TagPrefix="uc1" %>
<%@ Register Src="UserControl/PiePagina.ascx" TagName="PiePagina" TagPrefix="uc2" %>

<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>

<%@ Register src="UserControl/OKMessageBox.ascx" tagname="OKMessageBox" tagprefix="uc3" %>

<%@ Register src="UserControl/ModVentaDetalle.ascx" tagname="ModVentaDetalle" tagprefix="uc4" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>LAP- Modificar Modalidad de Venta</title>
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
    <link href="css/Style.css" rel="stylesheet" type="text/css" />
    <!-- #INCLUDE file="javascript/KeyPress.js" -->
    <!-- #INCLUDE file="javascript/Functions.js" -->
    <!-- #INCLUDE file="javascript/validarteclaF5.js" -->
    <!-- #INCLUDE file="javascript/MantenerSesion.js" -->

   
    <style type="text/css">
        .style1
        {
            width: 308px;
            height: 22px;
        }
        .style2
        {
            height: 22px;
            width: 90px;
        }
        .style3
        {
            width: 260px;
            height: 22px;
        }
        .style4
        {
            width: 280px;
            height: 22px;
        }
        .style5
        {
            width: 90px;
        }
    </style>

    <script language="javascript" type="text/javascript">
        var accionSave = false;
        function beginRequest(sender, args)
        {
            if (!sender.get_isInAsyncPostBack()) {
                if (accionSave) {
                    document.getElementById('btnActualizar').disabled = true;
                    document.body.style.cursor = 'wait';
                    //                var xc = 0;
                    //                for (xc = 0; xc < document.forms[0].length; xc++) {
                    //                    document.forms[0].elements[xc].disabled = true;
                    //                }
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
                    //                var xc = 0;
                    //                for (xc = 0; xc < document.forms[0].length; xc++) {
                    //                    document.forms[0].elements[xc].disabled = false;
                    //                }
                }
            }
        }

        function confirmacionLlamada()
        {
            var mConfirmacion = document.forms[0].hConfirmacion.value;

            var mNombre = document.forms[0].txtDescripcion.value;
            mNombre = mNombre.replace(/^\s*|\s*$/g, "");

            if (mNombre == "") {
                document.getElementById("lblMensajeError").innerHTML = "Ingrese la descripcion de la modalidad";
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
    </script> 
</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="hConfirmacion" runat="server" Value="Confirmar Accion" />
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
                    <td align="right">
                            <asp:Button ID="btnActualizar" runat="server" Width="96px" CssClass="Boton" onclick="btnAceptar_Click" OnClientClick="return confirmacionLlamada()" />
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
                            &nbsp;<table cellpadding="0" cellspacing="0" class="EspacioSubTablaPrincipal">
                                <tr>
                                    <td class="SpacingGrid" style="height: 115px">
                                    </td>
                                    <td class="CenterGrid" style="height: 115px">
                                    <asp:ScriptManager id="ScriptManager1" runat="server">
                                        <Scripts>
                                            <asp:ScriptReference Path="~/javascript/jSManager.js" />
                                        </Scripts>                                    
                                    </asp:ScriptManager>
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td style="width: 308px; height: 21px; text-align: left">
                                                            &nbsp;</td>
                                                        <td style="text-align: left" class="style5">
                                                            <asp:Label ID="lblCodigo" runat="server" CssClass="TextoEtiqueta" 
                                                        Width="82px"></asp:Label>
                                                        </td>
                                                        <td style="width: 260px; height: 21px; text-align: left">
                                                    <asp:Label ID="txtCodigo" runat="server" Width="149px" CssClass="TextoCampo"></asp:Label>
                                                        </td>
                                                        <td style="width: 280px; height: 21px; text-align: left">
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 308px; height: 21px; text-align: right">
                                                        </td>
                                                        <td style="text-align: left" class="style5">
                                                            <asp:Label ID="lblDescripcion" runat="server" CssClass="TextoEtiqueta" 
                                                        Width="82px"></asp:Label>
                                                        </td>
                                                        <td style="width: 260px; height: 21px; text-align: left">
                                                            <asp:TextBox ID="txtDescripcion" runat="server" Width="300px" MaxLength="50" 
                                                        CssClass="textbox" onkeypress="soloDescripcion()" onblur="gDescripcion(this)" ></asp:TextBox>
                                                        </td>
                                                        <td style="width: 280px; height: 21px; text-align: left">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: right" class="style1">
                                                        </td>
                                                        <td style="text-align: left" class="style2">
                                                            <asp:Label ID="lblTipoModalidad" runat="server" CssClass="TextoEtiqueta" 
                                                        Width="96px"></asp:Label>
                                                        </td>
                                                        <td style="text-align: left" class="style3">
                                                            <asp:Label ID="lblTipoModalidadDesc" runat="server" Width="200px" 
                                                                CssClass="TextoCampo" Visible="False"></asp:Label>
                                                            <asp:DropDownList ID="ddlTipoModalidad" runat="server" CssClass="combo2" 
                                                                Width="300px" Enabled="False">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="text-align: left" class="style4">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 308px; text-align: right">
                                                            &nbsp;</td>
                                                        <td style="text-align: left" class="style5">
                                                            <asp:Label ID="lblTipoEstado" runat="server" CssClass="TextoEtiqueta" 
                                                        Width="96px"></asp:Label>
                                                        </td>
                                                        <td style="width: 260px; text-align: left">
                                                            <asp:DropDownList ID="ddlTipoEstado" runat="server" CssClass="combo2" 
                                                        Width="150px" >
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="width: 280px; text-align: left">
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 308px; height: 17px; text-align: right">
                                                        </td>
                                                        <td style="text-align: left" class="style21" colspan="2">
        
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <uc3:OKMessageBox ID="omb" runat="server" />
            <asp:Label ID="lblMensajeError" runat="server" CssClass="mensaje" Width="428px" 
                Height="16px"></asp:Label>
        </ContentTemplate>
        <triggers>
            <asp:AsyncPostBackTrigger ControlID="btnActualizar" EventName="Click" />
        </triggers>
</asp:UpdatePanel>
                                                        </td>
                                                        <td style="width: 280px; height: 17px; text-align: left">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 308px; height: 17px; text-align: right">
                                                            &nbsp;</td>
                                                        <td style="text-align: right" colspan="2">
                                                            &nbsp;</td>
                                                        <td style="width: 280px; height: 17px; text-align: left">
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 308px; text-align: right">
                                                        </td>
                                                        <td colspan="2" style="text-align: left" rowspan="9">
                                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                <ContentTemplate>
                                                                    <div class="divModVenta">
                                                                        <table>
                                                                            <tr>
                                                                                <td style="text-align: left" >
                                                                                    <asp:Label ID="lblTituloAtributosModalidadVenta" runat="server" 
                                                                                CssClass="Titulo" Height="17px" Width="287px"></asp:Label>
                                                                                    &nbsp;&nbsp;&nbsp;
                                                                                </td>
                                                                                <td style="text-align: left">
                                                                                    <asp:ImageButton ID="btnAtributosMV" runat="server" CausesValidation="False" 
                                                                                        ImageUrl="~/Imagenes/Add.bmp" onclick="btnAtributosMV_Click" />
                                                                                </td>
                                                                                <td style="text-align: left">
                                                                                    &nbsp;</td>
                                                                                <td style="text-align: left">
                                                                                    &nbsp;</td>
                                                                                <td style="text-align: left">
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="5" style="height: 16px; text-align: left">
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="5" style="text-align: left">
                                                                                    <asp:Panel ID="pnlAtributosAsignadosMV" runat="server" 
                                                                                CssClass="TablaModVenta">
                                                                                    </asp:Panel>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="5" style="text-align: left">
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="5" style="text-align: left">
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="5" style="text-align: left; height: 19px;">
                                                                                    <asp:Label ID="lblTituloAtributosxTipoTicket" runat="server" CssClass="Titulo" 
                                                                                Width="251px"></asp:Label>
                                                                                    &nbsp; &nbsp; &nbsp;&nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="5" style="height: 19px; text-align: left">
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="5">
                                                                                <table>
                                                                                <tr>
                                                                                <td style="text-align: left" class="style12">
                                                                                    <asp:Label ID="lblTipoTicket" runat="server" CssClass="TextoEtiqueta" 
                                                                                Width="70px"></asp:Label>
                                                                                </td>
                                                                                <td class="style12" style="text-align: left">
                                                                                    <asp:DropDownList ID="ddlTipoTicket" runat="server" CssClass="combo2"
                                                                                        Width="300px">
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                                <td class="style12" style="text-align: left">
                                                                                    <asp:ImageButton ID="btnAtributosTT" runat="server" CausesValidation="False" 
                                                                                        ImageUrl="~/Imagenes/Add.bmp" onclick="btnAtributosTT_Click" />
                                                                                </td>
                                                                                </tr>
                                                                                </table>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="5" style="height: 24px; text-align: left">
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="5" style="text-align: left">
                                                                                    <asp:Panel ID="pnlAtributosAsignadosTT" runat="server">
                                                                                    </asp:Panel>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </div>
                                                                </ContentTemplate>
                                                                <triggers>
                                                                    <asp:AsyncPostBackTrigger ControlID="btnAtributosMV" EventName="Click" />
                                                                    <asp:AsyncPostBackTrigger ControlID="btnAtributosTT" EventName="Click" />
                                                                </triggers>
                                                            </asp:UpdatePanel>
                                                        </td>
                                                        <td style="width: 280px; text-align: left">
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 308px; height: 16px; text-align: right">
                                                        </td>
                                                        <td style="width: 280px; height: 16px; text-align: left">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 308px; text-align: right">
                                                        </td>
                                                        <td style="width: 280px; text-align: left">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 308px; text-align: right">
                                                        </td>
                                                        <td style="width: 280px; text-align: left">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 308px; text-align: right; height: 19px;">
                                                        </td>
                                                        <td style="width: 280px; text-align: left; height: 19px;">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 308px; height: 19px; text-align: right">
                                                        </td>
                                                        <td style="width: 280px; height: 19px; text-align: left">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 308px; height: 24px; text-align: right">
                                                        </td>
                                                        <td style="width: 280px; height: 24px; text-align: left">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 308px; height: 24px; text-align: right">
                                                            &nbsp;</td>
                                                        <td style="width: 280px; height: 24px; text-align: left">
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 308px; height: 24px; text-align: right">
                                                            &nbsp;</td>
                                                        <td style="width: 280px; height: 24px; text-align: left">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4" style="height: 24px; text-align: center">
                                                            &nbsp;</td>
                                                    </tr>
                                                </table>
                                    
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
            &nbsp;&nbsp;
            &nbsp;&nbsp;
            &nbsp;&nbsp;&nbsp;
            <br />
        </div>
    
    </div>
        
    <uc4:ModVentaDetalle ID="ModVentaDetalle1" runat="server" />
        
</form>
</body>
</html>