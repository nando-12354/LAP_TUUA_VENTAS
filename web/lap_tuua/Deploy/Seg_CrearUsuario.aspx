<%@ page language="C#" autoeventwireup="true" inherits="Modulo_Mantenimiento_MantenimientoUsuario, App_Web_tx1el90t" %>

<%@ Register Src="UserControl/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="uc1" %>
<%@ Register Src="UserControl/PiePagina.ascx" TagName="PiePagina" TagPrefix="uc2" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Src="UserControl/OKMessageBox.ascx" TagName="OKMessageBox" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>LAP - Creación de Usuario</title>
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
    <link href="css/Style.css" rel="stylesheet" type="text/css" />
    <!-- #INCLUDE file="javascript/KeyPress.js" -->
    <!-- #INCLUDE file="javascript/Functions.js" -->
    <!-- #INCLUDE file="javascript/validarteclaF5.js" -->
    <!-- #INCLUDE file="javascript/MantenerSesion.js" -->
    <style type="text/css">
        .style1
        {
            text-align: left;
            width: 132px;
        }
        .style2
        {
            text-align: left;
        }
        .style3
        {
            text-align: left;
            width: 99px;
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
    </style>
    
    
</head>
<body>
    <form id="form1" runat="server">
    <div style="background-image: url(Imagenes/back.gif)">
        <table align="center" border="0" cellpadding="0" cellspacing="0" class="TamanoTabla">
            <tr>
                <!-- HEADER -->
                <td style="height: 11px">
                    <uc1:CabeceraMenu ID="CabeceraMenu2" runat="server" />
                </td>
            </tr>
            <tr class="formularioTitulo">
                <!-- WORK MENU -->
                <td>
                    <table cellpadding="0" cellspacing="0" class="TamanoTabla">
                        <tr>
                            <td align="right" class="style1" style="text-align: left">
                                &nbsp;&nbsp;&nbsp;<img alt="" src="Imagenes/flecha_back.png" onclick="JavaScript: FnVolver();" />
                            </td>
                            <td align="right">
                                <asp:Button ID="btnAceptar" runat="server" OnClick="btnAceptar_Click" Width="100px"
                                    CssClass="Boton" />&nbsp;&nbsp;&nbsp;
                                <cc2:ConfirmButtonExtender ID="btnAceptar_ConfirmButtonExtender" runat="server" BehaviorID="cbeAceptar"
                                    ConfirmText="" Enabled="True" TargetControlID="btnAceptar">
                                </cc2:ConfirmButtonExtender>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <hr class="EspacioLinea" color="#0099cc" />
                </td>
            </tr>
			<tr>
                <td align="center">
                   <div class="EspacioSubTablaPrincipal">
						<table cellpadding="0" cellspacing="0" class="EspacioSubTablaPrincipal">
						<tr>
                            <td class="SpacingGrid" style="height: 20px" colspan="2">
                            </td>
                        </tr>
						<tr>
                            <td style="height: 115px" width="65%" align="right">
								<asp:ScriptManager ID="ScriptManager1" runat="server">
								</asp:ScriptManager>
								<asp:UpdatePanel ID="UpdatePanel2" runat="server">
								<ContentTemplate>
								<table style="width: 60%">
									<tr>
										<td class="style3" width="30%">
											<asp:Label ID="lblNombre" runat="server" CssClass="TextoEtiqueta"></asp:Label>
										</td>
										<td style="height: 21px; width: 70%; text-align: left;">
											<asp:TextBox ID="txtNombre" runat="server" MaxLength="50" Width="193px" CssClass="textbox"
												onkeypress="JavaScript: Tecla('Character');" onblur="gDescripcionNombre(this)"></asp:TextBox>
											<asp:RequiredFieldValidator ID="rfvNombre" runat="server" ControlToValidate="txtNombre"
												Display="None" SetFocusOnError="True">*</asp:RequiredFieldValidator>
											<cc2:ValidatorCalloutExtender ID="vceNombre" runat="server" TargetControlID="rfvNombre">
											</cc2:ValidatorCalloutExtender>
										</td>
									</tr>
									<tr>
										<td class="style3">
											<asp:Label ID="lblApellido" runat="server" CssClass="TextoEtiqueta"></asp:Label>
										</td>
										<td style="text-align: left;">
											<asp:TextBox ID="txtApellido" runat="server" MaxLength="50" Width="192px" CssClass="textbox"
												onkeypress="JavaScript: Tecla('Character');" onblur="gDescripcionNombre(this)"></asp:TextBox>
											<asp:RequiredFieldValidator ID="rfvApellido" runat="server" ControlToValidate="txtApellido"
												Display="None" SetFocusOnError="True">*</asp:RequiredFieldValidator>
											<cc2:ValidatorCalloutExtender ID="vceApellido" runat="server" TargetControlID="rfvApellido">
											</cc2:ValidatorCalloutExtender>
										</td>
										
									   
										
									</tr>
									<tr>
										<td class="style3">
											<asp:Label ID="lblCuenta" runat="server" CssClass="TextoEtiqueta"></asp:Label>
										</td>
										<td style="text-align: left;">
											<asp:TextBox ID="txtCuenta" runat="server" MaxLength="30" Width="158px" CssClass="textbox"
												onkeypress="JavaScript: Tecla('Alphanumeric');"></asp:TextBox>
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
										<td style="height: 26px; text-align: left;">
											<asp:TextBox ID="txtClave" runat="server" MaxLength="8" TextMode="Password" Width="77px"
												CssClass="textbox" onkeypress="JavaScript: Tecla('Password');" onblur="abcSinEspacio(this)"></asp:TextBox>
											<asp:RequiredFieldValidator ID="rfvClave" runat="server" ControlToValidate="txtClave"
												Display="None" SetFocusOnError="True">*</asp:RequiredFieldValidator>
											<cc2:ValidatorCalloutExtender ID="vceClave" runat="server" TargetControlID="rfvClave">
											</cc2:ValidatorCalloutExtender>
										</td>
									</tr>
									<tr>
										<td class="style3">
											<asp:Label ID="lblConfirmaClave" runat="server" CssClass="TextoEtiqueta"></asp:Label>
										</td>
										<td style="text-align: left;">
											<asp:TextBox ID="txtConfirmaClave" runat="server" MaxLength="8" TextMode="Password"
												Width="77px" CssClass="textbox" onkeypress="JavaScript: Tecla('Password');" onblur="abcSinEspacio(this)"></asp:TextBox>
											<asp:RequiredFieldValidator ID="rfvConfirmaClave" runat="server" ControlToValidate="txtConfirmaClave"
												Display="None" SetFocusOnError="True">*</asp:RequiredFieldValidator>
											<cc2:ValidatorCalloutExtender ID="vceConfirmarClaveR" runat="server" TargetControlID="rfvConfirmaClave">
											</cc2:ValidatorCalloutExtender>
											<asp:CompareValidator ID="cvdConfirmaClave" runat="server" ControlToCompare="txtClave"
												ControlToValidate="txtConfirmaClave" Display="None">*</asp:CompareValidator>
											<cc2:ValidatorCalloutExtender ID="vceConfirmaClaveC" runat="server" TargetControlID="cvdConfirmaClave">
											</cc2:ValidatorCalloutExtender>
										</td>
									</tr>
									<tr><!-- FECHA VIGENCIA -->
										<td class="style3">
											<asp:Label ID="lblFechaVigencia" runat="server" CssClass="TextoEtiqueta" onkeypress="JavaScript: Tecla('Date');"></asp:Label>
										</td>
										<td style="text-align: left;">
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
										<td class="style3">
											<asp:Label ID="lblGrupo" runat="server" CssClass="TextoEtiqueta"></asp:Label>
										</td>
										<td style="text-align: left;">
											<asp:DropDownList ID="ddlGrupo" runat="server" CssClass="combo">
											</asp:DropDownList>                                                                    
										</td>
									</tr>
								</table>
								</ContentTemplate>
								<Triggers>
									<asp:AsyncPostBackTrigger ControlID="btnAceptar" EventName="Click" />
									<asp:AsyncPostBackTrigger ControlID="btnAsignar" EventName="Click" />
									<asp:AsyncPostBackTrigger ControlID="btnDesasignar" EventName="Click" />
								</Triggers>
								</asp:UpdatePanel>
							</td>
							<td width="35%" align="left">
							    <table>
							    <tr>
							    <td width="10px">
							    </td>
							    <td>
								<table style="width: 85%" border="1">
								<tr>
								<td>
								    <table width="150px" border="0">
									<tr>
										<td class="style5" >
											<asp:CheckBox ID="chkDestrabe" runat="server" Text="Llave Destrabe" CssClass="TextoEtiqueta" />
										</td>
											
									</tr>
									<tr>
										<td class="style5" >
											<asp:CheckBox ID="chkMolinete" runat="server" Text="Llave Tipo Molinete" CssClass="TextoEtiqueta" />
										</td>
									</tr>
									</table>
								</td>
								</tr>
								</table>
								</td>
								</tr>
								</table>
							</td>
						</tr>
						<tr>        
				            <td align="center" colspan="2">
					            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
						            <ContentTemplate>
							            <table style="width: 332px" class="CenterGrid">
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
									            <td style="width: 103px; height: 112px;">
										            <asp:ListBox ID="lstRoles" runat="server" Width="159px" AppendDataBoundItems="True"
											            Height="173px" SelectionMode="Multiple"></asp:ListBox>
									            </td>
									            <td style="width: 18px; height: 112px; text-align: center;">
										            <asp:Button ID="btnAsignar" runat="server" OnClick="btnAsignar_Click" CausesValidation="False"
											            Width="57px" />
										            <br />
										            <asp:Button ID="btnDesasignar" runat="server" OnClick="btnDesasignar_Click" CausesValidation="False"
											            Width="57px" />
									            </td>
									            <td style="width: 124px; height: 112px;">
										            <asp:ListBox ID="lsRolesAsignados" runat="server" Width="169px" AppendDataBoundItems="True"
											            Height="171px" SelectionMode="Multiple"></asp:ListBox>
									            </td>
								            </tr>
							            </table>
							            <uc3:OKMessageBox ID="omb" runat="server" />
							            <asp:Label ID="lblMensajeError" runat="server" CssClass="mensaje" Width="427px"></asp:Label>
						            </ContentTemplate>
						            <Triggers>
							            <asp:AsyncPostBackTrigger ControlID="btnAceptar" EventName="Click" />
						            </Triggers>
					            </asp:UpdatePanel>
					            <br />
				            </td>
			            </tr>
						</table>
					</div>
				</td>
			</tr>			
			<tr>
                <!-- FOOTER -->
                <td class="Espacio1FilaTabla" style="height: 11px">
                    <uc2:PiePagina ID="PiePagina2" runat="server" />
                </td>
            </tr>
		</table>
		<asp:ObjectDataSource ID="odsListarGrupos" runat="server" SelectMethod="obtenerListadeCampo"
            TypeName="LAP.TUUA.CONTROL.BO_Administracion">
            <SelectParameters>
                <asp:Parameter DefaultValue="GrupoUsuario" Name="sNomCampo" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="odsListarRoles" runat="server" SelectMethod="listaDeRoles"
            TypeName="LAP.TUUA.CONTROL.BO_Seguridad"></asp:ObjectDataSource>
        <asp:Label ID="lblRConfiPassword" runat="server" Visible="False"></asp:Label>
        
        <asp:ListBox ID="lstCodRolesAsignados" runat="server" Height="10px" Visible="False"
            Width="1px"></asp:ListBox>
        
        <asp:Label ID="lblRpassword" runat="server" Visible="False"></asp:Label>
		<br />
        <br />
	</div>
	</form>
</body>	
			
</html>
