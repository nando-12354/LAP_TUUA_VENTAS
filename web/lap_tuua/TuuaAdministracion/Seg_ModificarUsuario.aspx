<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Seg_ModificarUsuario.aspx.cs"
    Inherits="Modulo_Mantenimiento_MantenimientoUsuario" %>

<%@ Register Src="UserControl/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="uc1" %>
<%@ Register Src="UserControl/PiePagina.ascx" TagName="PiePagina" TagPrefix="uc2" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Src="UserControl/OKMessageBox.ascx" TagName="OKMessageBox" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>LAP - Modificar Usuario</title>
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
    <link href="css/Style.css" rel="stylesheet" type="text/css" />
    <!-- #INCLUDE file="javascript/KeyPress.js" -->
    <!-- #INCLUDE file="javascript/Functions.js" -->
    <!-- #INCLUDE file="javascript/validarteclaF5.js" -->
    <!-- #INCLUDE file="javascript/MantenerSesion.js" -->

    <script type="text/javascript">
        function numero() {
            if ((event.keyCode < 48) || (event.keyCode > 57)) event.keyCode = 0;
        }
        function val_int(o) {
            o.value = o.value.toString().replace(/([^0-9])/g, "");
        }
    </script>

    <link href="../css/Style.css" rel="stylesheet" type="text/css" />
    <link href="../css/Style.css" rel="stylesheet" type="text/css" />
    <link href="../css/Style.css" rel="stylesheet" type="text/css" />
    <link href="../css/Style.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            text-align: left;
            width: 244px;
        }
        .style2
        {
            text-align: left;
            height: 21px;
            width: 117px;
        }
        .style3
        {
            text-align: left;
            height: 26px;
            width: 117px;
        }
        .style4
        {
            height: 205px;
            width: 117px;
        }
        .style5
        {
            text-align: left;
        }
        .style6
        {
            text-align: left;
            width: 183px;
        }
        .style9
        {
            height: 205px;
            width: 183px;
        }
    </style>
    
    <script type="text/javascript">

        function updateVisDes() {
            var showStyle = (document.form1.chkDestrabe.checked) ? "block" : "none";
            document.getElementById("divDestrabe").style.display = showStyle;
        }

        function updateVisMol() {
            var showStyle = (document.form1.chkMolinete.checked) ? "block" : "none";
            document.getElementById("divMolinete").style.display = showStyle;
        } 

    </script>
</head>
<body onload="updateVisDes();updateVisMol();">
    <form id="form1" runat="server" defaultbutton="btnActualizar" defaultfocus="txtNombre">
    <div style="background-image: url(Imagenes/back.gif)">
        <table align="center" border="0" cellpadding="0" cellspacing="0" class="TamanoTabla">
            <tr>
                <td colspan="2" style="height: 11px">
                    <uc1:CabeceraMenu ID="CabeceraMenu2" runat="server" />
                </td>
            </tr>
            <tr class="formularioTitulo">
                <td align="right" class="style1" style="text-align: left">
                    <img alt="" src="Imagenes/flecha_back.png" onclick="JavaScript: FnVolver();" />
                </td>
                <td align="right">
                    <asp:Button ID="btnActualizar" runat="server" OnClick="btnAceptar_Click" CssClass="Boton"
                        Height="18px" Width="109px" />
                    <cc2:ConfirmButtonExtender ID="btnActualizar_ConfirmButtonExtender" runat="server"
                        ConfirmText="" Enabled="True" TargetControlID="btnActualizar">
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
                    <div class="EspacioSubTablaPrincipal">
                        &nbsp;<table cellpadding="0" cellspacing="0" class="EspacioSubTablaPrincipal">
                            <tr>
                                <td class="SpacingGrid" style="height: 115px">
                                </td>
                                <td class="CenterGrid" style="height: 115px">
                                    &nbsp;<table style="width: 100%">
                                        <tr>
                                            
                                            <td align="right" width="65%">
                                                <asp:ScriptManager ID="ScriptManager1" runat="server">
                                                </asp:ScriptManager>
                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                    <ContentTemplate>
                                                        <table style="width: 60%">
                                                            <tr>
                                                                <td class="style5" width="15%">
                                                                    <asp:Label ID="lblCodigo" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                                                </td>
                                                                <td style="text-align: left" width="35%">
                                                                    &nbsp;<asp:Label ID="txtCodigo" runat="server" CssClass="TextoCampo" Width="70px"></asp:Label>
                                                                </td>
                                                                
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td class="style2">
                                                                    <asp:Label ID="lblNombre" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                                                </td>
                                                                <td style="height: 21px; text-align: left;">
                                                                    <asp:TextBox ID="txtNombre" runat="server" MaxLength="50" Width="193px" CssClass="textbox"
                                                                        onkeypress="JavaScript: Tecla('Character');" onblur="gDescripcionNombre(this)"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ControlToValidate="txtNombre"
                                                                        Display="None" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                                                    <cc2:ValidatorCalloutExtender ID="vceNombre" runat="server" TargetControlID="rfvNombre">
                                                                    </cc2:ValidatorCalloutExtender>
                                                                </td>
                                                                
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td class="style5">
                                                                    <asp:Label ID="lblApellido" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                                                </td>
                                                                <td style="text-align: left">
                                                                    <asp:TextBox ID="txtApellido" runat="server" MaxLength="50" Width="192px" CssClass="textbox"
                                                                        onkeypress="JavaScript: Tecla('Character');" onblur="gDescripcionNombre(this)"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rfvApellido" runat="server" ControlToValidate="txtApellido"
                                                                        Display="None" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                                                    <cc2:ValidatorCalloutExtender ID="vceApellido" runat="server" TargetControlID="rfvApellido">
                                                                    </cc2:ValidatorCalloutExtender>
                                                                </td>
                                                                                                                                                                                                
                                                            </tr>
                                                            <tr>
                                                                <td class="style5">
                                                                    <asp:Label ID="lblCuenta" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                                                </td>
                                                                <td style="text-align: left">
                                                                    <asp:TextBox ID="txtCuenta" runat="server" MaxLength="30" Width="158px" CssClass="textbox"
                                                                        onkeypress="JavaScript: Tecla('Alphanumeric');" onblur="abc(this)"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rfvCuenta" runat="server" ControlToValidate="txtCuenta"
                                                                        Display="None" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                                                    <cc2:ValidatorCalloutExtender ID="vceCuenta" runat="server" TargetControlID="rfvCuenta">
                                                                    </cc2:ValidatorCalloutExtender>
                                                                </td>
                                                                
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td class="style3">
                                                                    <asp:Label ID="lblClave" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                                                </td>
                                                                <td style="height: 26px; text-align: left; vertical-align:middle; ">
                                                                    <asp:TextBox ID="txtClave" runat="server" MaxLength="8" TextMode="Password" Width="77px"
                                                                        CssClass="textbox" onkeypress="JavaScript: Tecla('Alphanumeric');" onblur="abcSinEspacio(this)"></asp:TextBox>
                                                                    &nbsp;
                                                                        <a href="" id="lnkHabilitar" runat="server" onserverclick="imbPassword_Click"><b><asp:Label ID="lblHabilitar" runat="server">Habilitar</asp:Label></b></a>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style3">
                                                                    <asp:Label ID="lblConfirmaClave" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                                                </td>
                                                                <td style="height: 26px; text-align: left">
                                                                    <asp:TextBox ID="txtConfirmaClave" runat="server" MaxLength="8" TextMode="Password"
                                                                        Width="77px" CssClass="textbox" onkeypress="JavaScript: Tecla('Alphanumeric');"
                                                                        onblur="abcSinEspacio(this)"></asp:TextBox>
                                                                    &nbsp;
                                                                    <asp:CompareValidator ID="cvdConfirmaClave" runat="server" ControlToCompare="txtClave"
                                                                        ControlToValidate="txtConfirmaClave" Display="None">*</asp:CompareValidator>
                                                                    <cc2:ValidatorCalloutExtender ID="vceConfirmaClaveC" runat="server" TargetControlID="cvdConfirmaClave">
                                                                    </cc2:ValidatorCalloutExtender>
                                                                    <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" Width="228px"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style5">
                                                                    <asp:Label ID="lblFechaVigencia" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                                                </td>
                                                                <td style="text-align: left">
                                                                      <table cellpadding="0" cellspacing="0">
                                                                        <tr>
                                                                            <td>
                                                                                <asp:TextBox ID="txtFechaVigencia" runat="server" MaxLength="10" Width="88px" CssClass="textboxFecha" Height="16px" onkeypress="JavaScript: Tecla('Time');" onfocus="this.blur();"></asp:TextBox>
                                                                                <cc2:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFechaVigencia" PopupButtonID="imgbtnCalendarVigencia" >
                                                                                </cc2:CalendarExtender>
                                                                                <asp:RegularExpressionValidator ID="revFechaVigencia" runat="server" ControlToValidate="txtFechaVigencia"
                                                                                    ValidationExpression="^\d{1,2}\/\d{1,2}\/\d{4}$" Display="None" SetFocusOnError="True">*</asp:RegularExpressionValidator>
                                                                                <cc2:ValidatorCalloutExtender ID="vceFechaVigencia" runat="server" TargetControlID="revFechaVigencia">
                                                                                </cc2:ValidatorCalloutExtender>
                                                                                <asp:RequiredFieldValidator ID="rfvFechaVigencia" runat="server" ControlToValidate="txtFechaVigencia"
                                                                                    Display="None">*</asp:RequiredFieldValidator>
                                                                                <cc2:ValidatorCalloutExtender ID="vceFechaVigenciaR" runat="server" TargetControlID="rfvFechaVigencia">
                                                                                </cc2:ValidatorCalloutExtender>
                                                                            </td>
                                                                            <td>
                                                                                <asp:ImageButton ID="imgbtnCalendarVigencia" ImageUrl="~/Imagenes/Calendar.bmp" runat="server" Width="22px" Height="22px" OnClientClick="return false" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="center">
                                                                                <asp:Label ID="lblFechaDesde0" runat="server" CssClass="TextoEtiqueta" Text="( dd/mm/yyyy )"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style5">
                                                                    <asp:Label ID="lblEstado" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                                                </td>
                                                                <td style="text-align: left">
                                                                    <asp:DropDownList ID="ddlEstado" runat="server" CssClass="combo" 
                                                                        >
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style5">
                                                                    <asp:Label ID="lblGrupo" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                                                </td>
                                                                <td style="text-align: left">
                                                                    <asp:DropDownList ID="ddlGrupo" runat="server" CssClass="combo">
                                                                    </asp:DropDownList>
                                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                    
                                                                </td>
                                                                
                                                            </tr>
                                                            <cc2:ValidatorCalloutExtender ID="vceNombre0" runat="server" TargetControlID="rfvNombre">
                                                            </cc2:ValidatorCalloutExtender>
                                                            <cc2:ValidatorCalloutExtender ID="vceApellido0" runat="server" TargetControlID="rfvApellido">
                                                            </cc2:ValidatorCalloutExtender>
                                                            <cc2:ValidatorCalloutExtender ID="vceCuenta0" runat="server" TargetControlID="rfvCuenta">
                                                            </cc2:ValidatorCalloutExtender>
                                                            <cc2:ValidatorCalloutExtender ID="vceFechaVigenciaR0" runat="server" TargetControlID="rfvFechaVigencia">
                                                            </cc2:ValidatorCalloutExtender>
                                                            <cc2:ValidatorCalloutExtender ID="vceFechaVigencia0" runat="server" TargetControlID="revFechaVigencia">
                                                            </cc2:ValidatorCalloutExtender>
                                                            <cc2:ValidatorCalloutExtender ID="vceConfirmaClaveC0" runat="server" TargetControlID="cvdConfirmaClave">
                                                            </cc2:ValidatorCalloutExtender>
                                                        </table>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                            <td align="left" width="35%">
                                                
                                                <table width="150px" border="0">
                                                    <tr>
                                                        <td>
                                                        <table style="width: 85%" border="1">
                                                        <tr>
                                                        <td>
                                                        <table>
							                            <tr>
                                                            <td class="style5" width="15%">
                                                                <asp:CheckBox ID="chkDestrabe" runat="server" Text="Destrabe" CssClass="TextoEtiqueta" onclick="updateVisDes();"/>
                                                            </td>
                                                            <td style="text-align: left" width="35%">
                                                                <div id="divDestrabe" style="display:none;">
                                                                <asp:Label ID="lblDestrabe" runat="server" CssClass="TextoCampo" Width="70px">DESU0001</asp:Label>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>  
                                                            <td class="style5">
                                                                <asp:CheckBox ID="chkMolinete" runat="server" Text="Molinete" CssClass="TextoEtiqueta" onclick="updateVisMol();" />
                                                            </td>
                                                            <td style="text-align: left">
                                                                <div id="divMolinete" style="display:none;">
                                                                <asp:Label ID="lblMolinete" runat="server" CssClass="TextoCampo" Width="70px">MOLU0001</asp:Label>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>  
                                                            <td class="style5" width="15%" colspan="2">
                                                                <asp:Button ID="btnGenerarCodeBar" runat="server" 
                                                                    Text="Generar código de barras" onclick="btnGenerarCodeBar_Click" />
                                                            </td>
                                                        </tr>
                                                        </table>
                                                        </td>
                                                        </tr>
                                                        </table>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 115px">
                                                        <td style="height: 115px">
                                                        </td>
                                                    </tr>
                                                    </tr>
                                                        <td class="style5">
                                                            <asp:CheckBox ID="chkFlagCambioClave" runat="server" CssClass="TextoEtiqueta" />
                                                        </td>
                                                    </tr>
                                                </table>  
                                            </td>
                                        </tr>
                                        <tr>

                                            <td align="center" colspan="2">
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                    <ContentTemplate>
                                                        <table style="width: 332px">
                                                            <tr>
                                                                <td style="width: 103px; text-align: center;">
                                                                    <asp:Label ID="lblRoles" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                                                </td>
                                                                <td style="width: 18px">
                                                                </td>
                                                                <td style="width: 124px; text-align: center;">
                                                                    <asp:Label ID="lblRolesAsignados" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 103px">
                                                                    <asp:ListBox ID="lstRoles" runat="server" Width="159px" AppendDataBoundItems="True"
                                                                        Height="166px"></asp:ListBox>
                                                                </td>
                                                                <td style="width: 18px">
                                                                    <asp:Button ID="btnAsignar" runat="server" OnClick="btnAsignar_Click" CausesValidation="False"
                                                                        Width="53px" CssClass="Boton" />
                                                                    <br />
                                                                    <asp:Button ID="btnDesasignar" runat="server" OnClick="btnDesasignar_Click" CausesValidation="False"
                                                                        Width="53px" CssClass="Boton" />
                                                                </td>
                                                                <td style="width: 124px">
                                                                    <asp:ListBox ID="lsRolesAsignados" runat="server" Width="153px" AppendDataBoundItems="True"
                                                                        Height="160px"></asp:ListBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <uc3:OKMessageBox ID="omb" runat="server" />
                                                        <asp:Label ID="lblMensajeError" runat="server" CssClass="mensaje" Width="406px"></asp:Label>
                                                        <asp:ListBox ID="lstCodRolSinAsignar" runat="server" DataSourceID="odsListarRolesNoAsignados"
                                                            DataTextField="SCodRol" DataValueField="SCodRol" Height="1px" Width="1px" Style="display: none">
                                                        </asp:ListBox>
                                                        <asp:ListBox ID="lstCodRolesAsignados" runat="server" DataSourceID="odsListarRolesAsignados"
                                                            DataTextField="SCodRol" DataValueField="SCodRol" Height="1px" Width="1px" Style="display: none">
                                                        </asp:ListBox>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="btnActualizar" EventName="Click" />                                                        
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </td>

                                        </tr>
                                    </table>
                                </td>
                                <td class="SpacingGrid" style="height: 115px">
                                </td>
                            </tr>
                        </table>
                    </div>
                    <uc2:PiePagina ID="PiePagina2" runat="server" />
                </td>
            </tr>
        </table>
        <br />
        <asp:ObjectDataSource ID="odsListarRolesNoAsignados" runat="server" SelectMethod="listarRolesSinAsignar"
            TypeName="LAP.TUUA.CONTROL.BO_Seguridad" OldValuesParameterFormatString="original_{0}">
            <SelectParameters>
                <asp:QueryStringParameter Name="sCodUsuario" QueryStringField="Cod_Usuario" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="odsListarRolesAsignados" runat="server" SelectMethod="listarRolesAsignados"
            TypeName="LAP.TUUA.CONTROL.BO_Seguridad" OldValuesParameterFormatString="original_{0}">
            <SelectParameters>
                <asp:QueryStringParameter Name="sCodUsuario" QueryStringField="Cod_Usuario" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="odsListarEstado" runat="server" SelectMethod="obtenerListadeCampo"
            TypeName="LAP.TUUA.CONTROL.BO_Administracion" OldValuesParameterFormatString="original_{0}">
            <SelectParameters>
                <asp:Parameter DefaultValue="EstadoUsuario" Name="sNomCampo" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="odsListarGrupos" runat="server" SelectMethod="obtenerListadeCampo"
            TypeName="LAP.TUUA.CONTROL.BO_Administracion">
            <SelectParameters>
                <asp:Parameter DefaultValue="GrupoUsuario" Name="sNomCampo" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
        &nbsp;&nbsp;
        <asp:Label ID="lblPassword" runat="server" Visible="False"></asp:Label>
        &nbsp;&nbsp;
        <asp:Label ID="lblEstadoNuevo" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lblCuentaReg" runat="server" Visible="False"></asp:Label>
        <br />
    </div>
    </form>
</body>
</html>
