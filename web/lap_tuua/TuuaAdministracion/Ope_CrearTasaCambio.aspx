<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Ope_CrearTasaCambio.aspx.cs" Inherits="Ope_CrearTasaCambio" ResponseEncoding="utf-8" %>

<%@ Register Src="UserControl/CabeceraMenu.ascx" TagName="CabeceraMenu"  TagPrefix="uc1" %>
<%@ Register Src="UserControl/PiePagina.ascx" TagName="PiePagina" TagPrefix="uc2" %>

<%@ Register src="UserControl/OKMessageBoxHuge.ascx" tagname="OKMessageBox" tagprefix="uc3" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>LAP - Crear Tasa de Cambio</title>
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
    <!-- #INCLUDE file="javascript/ValidarFecha.js" -->
    <!-- #INCLUDE file="javascript/validarteclaF5.js" -->
    <!-- #INCLUDE file="javascript/MantenerSesion.js" -->
    <!-- #INCLUDE file="javascript/Functions.js" -->
    <!-- #INCLUDE file="javascript/KeyPress.js" -->
    <link href="css/Style.css" rel="stylesheet" type="text/css" />
    <link href="../css/Style.css" rel="stylesheet" type="text/css" />
    
    <script language="JavaScript" type="text/javascript">
        function valida_decimal2digitosa(campo)
        {
            /*if(campo.value!=""){
            if(!valida_dec(campo.value)){
            alert("Formato de monto incorrecto");
            campo.select();
            campo.focus();
            }
            else{
            var  indic = campo.value.indexOf(".", 0);
            var indfin = campo.value.length;
            if(indic>-1){ //si encuentra el punto decima valida qe solo tenga dos decimales
            var cadena = campo.value.substring(indic+1, indfin);
            if(cadena.length>2){
            alert("El monto solo puede tener 2 decimales");
            campo.select();
            campo.focus();
            }
            }
            }    
            }*/
        }

        function soloDecimalTest(campo, event)
        {
            /*var code = window.event.keyCode
            patron =/^\d{0,4}\.?\d{0,4}$/;            
            strCadena = campo.value + String.fromCharCode(code);
            //alert(strCadena);
            //te = String.fromCharCode(code);
            te = strCadena;
            var match = patron.test(te)
            //alert(match);
            return patron.test(te);*/
        }        
        
        function validarDecimal(e) {

            obj = e.srcElement || e.target;
            tecla_codigo = (document.all) ? e.keyCode : e.which;
            if (tecla_codigo == 8) return true;
            patron = /[\d.]/;
            tecla_valor = String.fromCharCode(tecla_codigo);
            control = (tecla_codigo == 46 && (/[.]/).test(obj.value)) ? false : true
            return patron.test(tecla_valor) && control;
        }
        
	
        function controlaHoraProg() {

            if (window.event.keyCode == 8 || window.event.keyCode == 46) {
                //form1.txtHoraHasta.value = form1.txtHoraHasta.value.substring(0, form1.txtHoraHasta.value.length - 2);
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

		function validar() {
		    if (document.getElementById("rbtnFchAct").checked == true) {
		        document.getElementById("txtFechaProg").disabled = true;
		        document.getElementById("imgbtnCalendar").disabled = true;
		        document.getElementById("txtHoraProg").disabled = true;
		        document.getElementById("txtFechaProg").value = '';
		        document.getElementById("imgbtnCalendar").value = '';
		        document.getElementById("txtHoraProg").value = '';
		        document.getElementById("txtFechaProg").style.backgroundColor = '#CCCCCC';
		        document.getElementById("txtHoraProg").style.backgroundColor = '#CCCCCC';
		        document.getElementById("lblMensajeError").innerHTML = '';
		    }
		    if (document.getElementById("rbtnFchProg").checked == true) {
		        document.getElementById("txtFechaProg").disabled = false;
		        document.getElementById("imgbtnCalendar").disabled = false;
		        document.getElementById("txtHoraProg").disabled = false;		        
		        document.getElementById("txtHoraProg").style.backgroundColor = '#FFFFFF';		        
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

		function confirmacionLlamada()
		{
		    if (document.getElementById("txtFechaProg").value == '' && document.getElementById("rbtnFchProg").checked == true) {
		        document.getElementById("lblMensajeError").innerHTML = "Ingrese la Fecha a Programar";
		        return false;
		    } else if ((document.getElementById("txtHoraProg").value == '__:__:__' || document.getElementById("txtHoraProg").value == '') && document.getElementById("rbtnFchProg").checked == true) {
		        document.getElementById("lblMensajeError").innerHTML = "Ingrese la Hora a Programar";
		        return false;
		    } else {
		        if (confirm("Está seguro de registrar la Tasa de Cambio")) {
		            accionSave = true;
		            return true;
		        }
		        else {
		            accionSave = false;
		            return false;
		        }
		    }
		}	
    </script>
    
</head>
<body onload="validar()">
<form id="form1" runat="server">
    <table align="center" border="0" cellpadding="0" cellspacing="0" class="TamanoTabla">
        <tr>
            <td class="Espacio1FilaTabla" colspan="2" style="height: 11px">
                <uc1:CabeceraMenu ID="CabeceraMenu3" runat="server" />
            </td>
        </tr>
        <tr class="formularioTitulo">
            <td align="right"  style="text-align: left">
                &nbsp;&nbsp;&nbsp;<img alt="" src="Imagenes/flecha_back.png" onclick="JavaScript: FnVolver();" /></td>
            <td align="right">
                <asp:Button ID="btnGrabar" runat="server" CssClass="Boton" Text="Grabar >>" 
                    OnClick="btnGrabar_Click" OnClientClick="return confirmacionLlamada()" 
                    TabIndex="6" />&nbsp;&nbsp;&nbsp;
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
                            <td>
                            </td>
                            <td colspan="2">
                                <span class="titulosecundario">Registrar Nueva Tasa de Cambio</span>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td colspan="2">
                                <table style="width: 100%; left: 0px; top: 0px;" class="alineaderecha">
                                <tr>
                                <td>
                                <table>
                                <tr><td>
                                    <asp:RadioButton ID="rbtnFchAct" runat="server" onClick="validar();" 
                                        GroupName="TipoProg" CssClass="TextoFiltro" Text="Fecha Actual" TabIndex="1"/>
                                    </td>
                                </tr>
                                <tr style="vertical-align: top;"><td>
                                    <asp:RadioButton ID="rbtnFchProg" runat="server" onClick="validar();" GroupName="TipoProg" CssClass="TextoFiltro" Text="Fecha Programada" TabIndex="2"/>
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
                                </td>
                                </tr>
                                </table>
                                <br/>
                                
                            </td>                                    
                        </tr>
                        <tr>
                            <td class="SpacingGrid" style="height: 115px; width: 2%;">
                            </td>
                            <td class="CenterGrid" style="height: 115px">
                            <hr class="EspacioLinea" color="#0099cc" />
                            <asp:ScriptManager ID="ScriptManager1" runat="server">
                             <Scripts>
                                <asp:ScriptReference Path="~/javascript/jSManager.js" />
                             </Scripts>
                            </asp:ScriptManager>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="grvTasaCambio" runat="server" 
                                            CssClass="grilla" CellPadding="3" BorderStyle="None" BorderWidth="1px" GridLines="Vertical" AutoGenerateColumns="False" Width="100%" AllowPaging="True" AllowSorting="True" >
                                            <Columns>
                                                <asp:TemplateField HeaderText="Moneda">
                                                    <ItemTemplate >
                                                        <asp:Label ID="lblITMoneda" runat="server" Text='<%# String.Format("{0} ( {1} )", Eval("Dsc_Moneda"), Eval("Dsc_Simbolo")) %>' ></asp:Label>
                                                        <asp:HiddenField ID="idMoneda" runat="server" Value='<%# Eval("Cod_Moneda") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Compra (C)" >
                                                    <ItemTemplate >
                                                        <asp:TextBox ID="idCompra" runat="server" Text='<%# Bind("Imp_Cambio_Actual_Compra") %>' 
                                                        BorderStyle="Inset" MaxLength="9" 
                                                        Font-Bold="true" ForeColor="Green" Width="200px"
                                                        onkeypress="return validarDecimal(event)"
                                                        onblur="valdecimal_int(this)"  
                                                        ReadOnly="true"></asp:TextBox>
                                                        <asp:CheckBox ID="chkCompra" runat="server" AutoPostBack="true" OnCheckedChanged="CheckC_Changed"/>
                                                        <asp:HiddenField ID="idTasaCambioCompra" runat="server" Value='<%# Eval("Cod_Tasa_Cambio_Compra") %>' />                                                                
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Venta (V)" >
                                                    <ItemTemplate >
                                                        <asp:TextBox ID="idVenta" runat="server" Text='<%# Bind("Imp_Cambio_Actual_Venta") %>' 
                                                        BorderStyle="Inset" MaxLength="9" 
                                                        Font-Bold="true" ForeColor="Green" Width="200px"                             
                                                        onkeypress="return validarDecimal(event)"
                                                        onblur="valdecimal_int(this)"
                                                        ReadOnly="true"></asp:TextBox>
                                                        <asp:CheckBox ID="chkVenta" runat="server" AutoPostBack="true" OnCheckedChanged="CheckV_Changed"/>
                                                        <asp:HiddenField ID="idTasaCambioVenta" runat="server" Value='<%# Eval("Cod_Tasa_Cambio_Venta") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <SelectedRowStyle  CssClass="grillaFila"  />
                                            <PagerStyle  CssClass="grillaPaginacion"/>
                                            <HeaderStyle CssClass="grillaCabecera" />
                                            <AlternatingRowStyle CssClass="grillaFila" />
                                            <FooterStyle BackColor="Black" ForeColor="Black" />
                                        </asp:GridView>
                                        <asp:Label ID="lblMensajeError" runat="server" CssClass="mensaje" Width="450px"></asp:Label>
                                        <uc3:OKMessageBox ID="omb" runat="server" />
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnGrabar" EventName="Click" />
                                    </Triggers>
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
</form>
</body>
</html>
