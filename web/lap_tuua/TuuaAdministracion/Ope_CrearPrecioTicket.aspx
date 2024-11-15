<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Ope_CrearPrecioTicket.aspx.cs" Inherits="Ope_CrearPrecioTicket" ResponseEncoding="utf-8" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register Src="UserControl/CabeceraMenu.ascx" TagName="CabeceraMenu"  TagPrefix="uc1" %>
<%@ Register Src="UserControl/PiePagina.ascx" TagName="PiePagina" TagPrefix="uc2" %>

<%@ Register src="UserControl/OKMessageBoxHuge.ascx" tagname="OKMessageBox" tagprefix="uc3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>LAP - Crear Precio Ticket</title>
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
    <!-- #INCLUDE file="javascript/ValidarFecha.js" -->    
    <!-- #INCLUDE file="javascript/validarteclaF5.js" -->
    <!-- #INCLUDE file="javascript/MantenerSesion.js" -->
    <!-- #INCLUDE file="javascript/Functions.js" -->
    <!-- #INCLUDE file="javascript/KeyPress.js" -->
    
	<link href="css/Style.css" rel="stylesheet" type="text/css" />
    <link href="../css/Style.css" rel="stylesheet" type="text/css" />
	<script language="javascript" type="text/javascript">
	    function controlaHoraProg() {
	        if (window.event.keyCode == 8 || window.event.keyCode == 46) {
	            //form1.txtHoraProg.value = form1.txtHoraProg.value.substring(0, form1.txtHoraProg.value.length - 2);
	        }
	        else {

	            if (form1.txtHoraProg.value.length == 2) {
	                form1.txtHoraProg.value = form1.txtHoraProg.value + ':';
	            }
	            if (form1.txtHoraProg.value.length == 5) {
	                form1.txtHoraProg.value = form1.txtHoraProg.value + ':';
	            }
	        }
	    } 
        function validar(e)
        {
            obj=e.srcElement || e.target;
            tecla_codigo = (document.all) ? e.keyCode : e.which;
            if(tecla_codigo==8)return true;
            patron =/[\d.]/;
            tecla_valor = String.fromCharCode(tecla_codigo);
            control=(tecla_codigo==46 && (/[.]/).test(obj.value))?false:true
            return patron.test(tecla_valor) &&  control;
        }

        function validarRB() {
            if (document.getElementById("rbtnFchAct").checked == true) {
                document.getElementById("txtFechaProg").disabled = true;
                document.getElementById("imgbtnCalendar").disabled = true;
                document.getElementById("txtHoraProg").disabled = true;
                document.getElementById("txtFechaProg").value = '';
                document.getElementById("imgbtnCalendar").value = '';
                document.getElementById("txtHoraProg").value = '';
                document.getElementById("txtFechaProg").style.backgroundColor = '#CCCCCC';
                document.getElementById("txtHoraProg").style.backgroundColor = '#CCCCCC';
                
            }
            if (document.getElementById("rbtnFchProg").checked == true) {
                document.getElementById("txtFechaProg").disabled = false;
                document.getElementById("imgbtnCalendar").disabled = false;
                document.getElementById("txtHoraProg").disabled = false;
                //document.getElementById("txtFechaProg").style.backgroundColor = '#FFFFFF';
                document.getElementById("txtHoraProg").style.backgroundColor = '#FFFFFF';
            }
        }


        function confirmacionLlamada()
        {

            if (document.getElementById("txtFechaProg").value == '' && document.getElementById("rbtnFchProg").checked == true) {
                document.getElementById("lblMensajeError").innerHTML = "Ingrese la Fecha a Programar";
                return false;
            } else if ((document.getElementById("txtHoraProg").value == '__:__:__' || document.getElementById("txtHoraProg").value == '') && document.getElementById("rbtnFchProg").checked == true) {
                document.getElementById("lblMensajeError").innerHTML = "Ingrese la Hora a Programar";
                return false;
            } else {
                if (confirm("Está seguro de registrar los precios de Ticket")) {
                    accionSave = true;
                    return true;
                }
                else {
                    accionSave = false;
                    return false;
                }
            }
        }


        var accionSave = false;
        function beginRequest(sender, args)
        {
            if (!sender.get_isInAsyncPostBack()) {
                if (accionSave) {
                    document.getElementById('btnGrabar').disabled = true;
                    document.body.style.cursor = 'wait';

                }
            }
        }


        function endRequest(sender, args)
        {
            if (!sender.get_isInAsyncPostBack()) {
                if (accionSave) {
                    document.getElementById('btnGrabar').disabled = false;
                    document.body.style.cursor = 'default'

                    accionSave = false;
                }
            }
        }        	

	</script>
</head>
<body onload="validarRB()">
    <form id="form1" runat="server" >

        <table align="center" border="0" cellpadding="0" cellspacing="0" class="TamanoTabla">
            <tr>
                <td class="Espacio1FilaTabla" colspan="2">
                    <uc1:CabeceraMenu ID="CabeceraMenu3" runat="server"/>
                </td>
            </tr>
            <tr class="formularioTitulo">
                    <td align="right"  style="text-align: left">&nbsp;&nbsp;&nbsp;<img alt="" src="Imagenes/flecha_back.png" onclick="JavaScript: FnVolver();" /></td>
                    <td align="right"  style="text-align: right">
                    <asp:Button ID="btnGrabar" runat="server" CssClass="Boton" Text="Grabar >>" OnClick="btnGrabar_Click" OnClientClick="return confirmacionLlamada()"/>&nbsp;&nbsp;&nbsp;
                    </td>                
                    <td colspan="2">                    
                    </td>
            </tr>
            <tr>
                <td colspan="2">
                    <hr class="EspacioLinea" color="#0099cc" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <div>
                        <table cellpadding="0" cellspacing="0" class="EspacioSubTablaPrincipal">
                            <tr>
                                <td></td>
                                <td colspan="2">
                                    <span class="titulosecundario">Registrar Nueva Precio de Ticket</span><br>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td colspan="2">
                                 <asp:ScriptManager ID="smgCrearPrecioTicket" runat="server">
                                     <Scripts>
                                        <asp:ScriptReference Path="~/javascript/jSManager.js" />
                                     </Scripts>
                                    </asp:ScriptManager>
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <asp:Panel ID="pnlPrincipal" runat="server" Visible="true">
                                    <table>
                                        <tr><td>
                                            <asp:RadioButton ID="rbtnFchAct" runat="server" onClick="validarRB();" 
                                                GroupName="TipoProg" CssClass="TextoFiltro" Text="Fecha Actual" 
                                                AutoPostBack="True"/>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            <asp:RadioButton ID="rbtnFchProg" runat="server" onClick="validarRB();" 
                                                    GroupName="TipoProg" CssClass="TextoFiltro" Text="Fecha Programada" 
                                                    AutoPostBack="True"/>
                                            </td>
                                            <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                            <td rowspan="2">
                                                <table cellpadding="0" cellspacing="0">
                                                    <tr>
                                                    <td><asp:TextBox ID="txtFechaProg" runat="server" CssClass="textboxFecha" Width="88px" onfocus="this.blur();" onkeypress="JavaScript: window.event.keyCode = 0;" BackColor="#E4E2DC" Height="16px" MaxLength="10"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" TargetControlID="txtFechaProg" PopupButtonID="imgbtnCalendar" Format="dd/MM/yyyy">
                                                        </cc1:CalendarExtender>
                                                    </td>
                                                    </tr>
                                                    <tr>
                                                    <td align="center"><asp:Label ID="lblFechaVuelo1" runat="server" CssClass="TextoEtiqueta" Text="( dd/mm/yyyy )"></asp:Label></td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td><asp:ImageButton ID="imgbtnCalendar" ImageUrl="~/Imagenes/Calendar.bmp" runat="server" Width="22px" Height="22px" TabIndex="3" />
                                            </td>
                                            <td>&nbsp;</td>
                                            <td rowspan="2">
                                                <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                <td><asp:TextBox ID="txtHoraProg" runat="server" CssClass="textbox" MaxLength="8" onkeypress="JavaScript: Tecla('Time');" onBlur="JavaScript:CheckTime(this)" ReadOnly="false" Width="56px"></asp:TextBox>
                                                    <cc1:MaskedEditExtender ID="valHoraProg" runat="server" Mask="99:99:99" TargetControlID="txtHoraProg" ClearMaskOnLostFocus="False" MaskType="Time" UserTimeFormat="TwentyFourHour" CultureName="es-ES" PromptCharacter="_" AutoComplete="False">
                                                    </cc1:MaskedEditExtender>
                                                </td>
                                                </tr>
                                                <tr>
                                                <td align="center"><asp:Label ID="lblFechaVuelo2" runat="server" CssClass="TextoEtiqueta" Text="( hh:mm:ss )"></asp:Label></td>
                                                </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr style="vertical-align: top;">
                                            <td></td>
                                            <td>&nbsp;</td>                            
                                        </tr>
                                    </table>
                                    <br>
                                    </asp:Panel> 
                            </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="rbtnFchAct" EventName="CheckedChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="rbtnFchProg" EventName="CheckedChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                                </td>                                    
                            </tr>
                            <tr>
                                <td class="SpacingGrid" style="height: 115px">
                                </td>
                                <td class="CenterGrid" style="height: 115px">
                                    <hr class="EspacioLinea" color="#0099cc" />
                                   
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <asp:GridView ID="grvPrecioTicket" runat="server" CssClass="grilla" 
                                                CellPadding="3" CellSpacing="0" BorderStyle="None" BorderWidth="1" 
                                                GridLines="Vertical" AutoGenerateColumns="false" Width="100%" 
                                                AllowSorting="true" >
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Codigo" Visible="False">
                                                        <ItemTemplate >
                                                            <asp:Label ID="lblITCodigo" runat="server" Text='<%# Eval("Cod_Precio_Ticket") %>' ></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Tipo de Ticket">
                                                        <ItemTemplate >
                                                            <asp:Label ID="lblITTipoTicket" runat="server" Text='<%# String.Format("{0} ( {1} )", Eval("Cod_Tipo_Ticket"), Eval("Dsc_Tipo_Ticket")) %>' ></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Moneda" >
                                                        <ItemTemplate >
                                                            <asp:DropDownList ID="ddlMoneda" runat="server" DataSourceID="ObjectDataSource1" DataTextField="Dsc_Moneda" DataValueField="Cod_Moneda" SelectedValue='<%#Bind("Cod_Moneda") %>' Width="200px">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Precio"  >
                                                        <ItemTemplate >
                                                            <asp:TextBox ID="idPrecio" runat="server" Text='<%# Bind("Imp_Precio") %>' 
                                                                BorderStyle="Inset" MaxLength="13" 
                                                                Font-Bold="true" ForeColor="Green" Width="200px"
                                                                onkeypress='return validar(event);' 
                                                                onblur="valdecimal_int(this)" ReadOnly = "true"></asp:TextBox>                         
                                                            <asp:RequiredFieldValidator ID="rfvPrecio" runat="server" ErrorMessage="Ingrese precio" ControlToValidate="idPrecio" Display="none" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                            <cc1:ValidatorCalloutExtender ID="vcePrecio" runat="server" TargetControlID="rfvPrecio">
                                                            </cc1:ValidatorCalloutExtender>
                                                            <asp:CheckBox ID="chkPrecio" runat="server" AutoPostBack="true" OnCheckedChanged="CheckP_Changed"/>
                                                            <asp:HiddenField ID="idTipoTicket" runat="server" Value='<%# Eval("Cod_Tipo_Ticket") %>' />
                                                            <asp:HiddenField ID="idMoneda" runat="server" Value='<%# Eval("Cod_Moneda") %>' />                                                            
                                                        </ItemTemplate>                                                        
                                                    </asp:TemplateField>
                                                </Columns>
                                                <SelectedRowStyle  CssClass="grillaFila"  />
                                                <PagerStyle  CssClass="grillaPaginacion"/>
                                                <HeaderStyle CssClass="grillaCabecera" />
                                                <AlternatingRowStyle CssClass="grillaFila" />
                                                <FooterStyle BackColor="Black" ForeColor="Black" />
                                            </asp:GridView>
                                            <asp:Label ID="lblMensajeError" runat="server" CssClass="mensaje"></asp:Label>
                                            <uc3:OKMessageBox ID="omb" runat="server" />
                                        </ContentTemplate>
                                        <triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnGrabar" EventName="Click" />
                                        </triggers>
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
        <asp:UpdateProgress AssociatedUpdatePanelID="UpdatePanel2" DisplayAfter="0" ID="updateProgress2" runat="server">
        <ProgressTemplate>
            <div id="progressBackgroundFilter">
            </div>
            <div id="processMessage">
                &nbsp;&nbsp;&nbsp;Procesando...<br />
                <br />
                <img alt="Loading" src="Imagenes/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
        <asp:ObjectDataSource 
            ID="ObjectDataSource1" 
            runat="server" 
            SelectMethod="listarAllMonedas"  
            TypeName="LAP.TUUA.CONTROL.BO_Administracion"
            FilterExpression="Tip_Estado='Vigente'">
        </asp:ObjectDataSource>
    </form>
</body>
</html>
