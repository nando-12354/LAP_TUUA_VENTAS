<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Ope_Conciliacion.aspx.cs" Inherits="Ope_Conciliacion" %>
<%@ Register Src="UserControl/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="uc1" %>
<%@ Register Src="UserControl/PiePagina.ascx" TagName="PiePagina" TagPrefix="uc2" %>
<%@ Register Src="UserControl/ConsRepresentante.ascx" TagName="ConsRepresentante" TagPrefix="uc3" %>
<%@ Register Src="UserControl/CnsDetBoarding.ascx" TagName="CnsDetBoarding" TagPrefix="uc4" %>
<%@ Register Src="UserControl/IngBCBPAsociado.ascx" TagName="IngBoarding" TagPrefix="uc5" %>

<%@ Register src="UserControl/PiePagina.ascx" tagname="PiePagina" tagprefix="uc6" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script language="javascript" type="text/javascript">

</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
    <LAP - Conciliacion de BCBP</title>
    <link href="css/Style.css" rel="stylesheet" type="text/css" />
    <!-- #INCLUDE file="javascript/KeyPress.js" -->
    <!-- #INCLUDE file="javascript/MantenerSesion.js" -->
    <!-- #INCLUDE file="javascript/validarteclaF5.js" -->
    <link href="css/Style.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            width: 15%;
        }
        .style2
        {
            height: 31px;
        }
        </style>
</head>
<script type="text/javascript" language="javascript">

    function LimpiarPersona() {
        document.getElementById('txtPersona').value = "";
    }
    
    
    function FocusPistola() {
        if (alertTimeOut == 0) {
            document.getElementById("txtPersona").focus();
            setTimeout("FocusPistola()", 3000);
        }
    }
      
    function validarEnter(key_event)
    {
            var k;
            if (document.all)  k = key_event.keyCode;
            else k = key_event.which;
            if (k == 13) 
            return false;
            else return true;     
    }  
      
    function Checkear(key_event)
    {              
//       if(alertTimeOut==0 && ingTrama==1) {
//           ingTrama = 0;
//            
//           document.getElementById("btnBuscar").click();
//        return false;
//       }
//       else
//       {
        return true;
 //      }
        
    }
      
    function CapturarTrama(key_event)
    {
        if (alertTimeOut == 0) {
            var k;
            ingTrama = 1;
            if (document.all) k = key_event.keyCode;
            else k = key_event.which;
            if (k == 13) {
                document.getElementById("txtPersona").focus();
                document.getElementById('hCodigoBarra').value = document.getElementById('txtPersona').value;
                document.getElementById('txtPersona').value = "";
                document.getElementById("btnBuscar").click();
                document.getElementById("txtPersona").focus();
            }
        }
        else {
            var k;            
            if (document.all) k = key_event.keyCode;
            else k = key_event.which;
            if (k == 13) {
                event.keyCode = 9;
            }
        }
    }

    function fTrim(valor) {

        return valor.replace(/^\s*|\s*$/g, "");
    }


    function validarFechas() {

        var controlHabil = document.getElementById('chkFechaUso');
        var controlActual = document.getElementById('hFechaActual');

        if (!controlHabil.checked) {
            document.getElementById('imgbtnCalendar1').disabled = true;
            document.getElementById('imgbtnCalendar2').disabled = true;
            document.getElementById('txtDesde').value = "";
            document.getElementById('txtHasta').value = "";
        }
        else {
            document.getElementById('imgbtnCalendar1').disabled = false;
            document.getElementById('imgbtnCalendar2').disabled = false;
            document.getElementById('txtDesde').value = controlActual.value;
            document.getElementById('txtHasta').value = controlActual.value;            
        }

    }
    
    
    function Validaciones() {


        if (alertTimeOut == 1) {

            var controlUso = document.forms[0].chkFechaUso;
            var controlMensaje = document.getElementById('lblErrorMsg');
            var controlCompania = document.getElementById('cboCompanias');
            var controlPasajero = document.getElementById('txtPersona');

            if (fTrim(controlCompania.value) == "") {
                controlMensaje.innerHTML = "Seleccione una compañía.";
                return false;
            }

            if (fTrim(controlPasajero.value) == "") {
                controlMensaje.innerHTML = "Ingrese el nombre del pasajero.";
                return false;
            }

            controlMensaje.innerHTML = "";

            if (controlUso.checked) {
                var sDesde = document.getElementById('txtDesde').value;
                var sHasta = document.getElementById('txtHasta').value;

                if (fTrim(sDesde) == "" || fTrim(sHasta) == "") {
                    controlMensaje.innerHTML = "Rango de fecha de uso incorrecto.";
                    return false;
                }

                sDesde = sDesde.split('/')[2] + sDesde.split('/')[1] + sDesde.split('/')[0];
                sHasta = sHasta.split('/')[2] + sHasta.split('/')[1] + sHasta.split('/')[0];

                if (sDesde > sHasta) {
                    controlMensaje.innerHTML = "Rango de fecha de uso incorrecto.";
                    return false;
                }
            }
        }
        return true;
    }

    function confirmarConciliacion() {

//        if (ingTrama == 0) {
            if (confirm("Esta seguro de realizar el proceso de Conciliación.")) {
                return true;
            }
            else {
                return false;
            }
//        }
//        else {
//            ingTrama = 0
//            document.getElementById("btnBuscar").click();
//            return false;

//        }
    }
    var ingTrama=0;
    var alertTimeOut = 0;
    function ValidarhabilitarPistola() {
        var controlM = document.getElementById('rbManual');
        var controlP = document.getElementById('rbPistola');

        if (controlM.checked) {
            alertTimeOut = 1;
            document.getElementById('hBandera').value = "1";

            document.getElementById('cboCompanias').disabled = false;
            document.getElementById('txtAsiento').disabled = false;
            document.getElementById('cboCompanias').disabled = false;
            document.getElementById('imgbtnCalendar').disabled = false;
            document.getElementById('txtNroVuelo').disabled = false;
            document.getElementById('chkFechaUso').disabled = false;            
            
        }
        else if (controlP.checked) {
            alertTimeOut = 0;
            document.getElementById('hBandera').value = "0";
            setTimeout("FocusPistola()", 3000);

            document.getElementById('cboCompanias').disabled = true;
            document.getElementById('txtAsiento').disabled = true;
            document.getElementById('cboCompanias').disabled = true;
            document.getElementById('imgbtnCalendar').disabled = true;
            document.getElementById('txtNroVuelo').disabled = true;
            document.getElementById('chkFechaUso').disabled = true;   
                     
            document.getElementById('chkFechaUso').checked = false;
            document.getElementById('imgbtnCalendar1').disabled = true;
            document.getElementById('imgbtnCalendar2').disabled = true;
            document.getElementById('txtDesde').value = "";
            document.getElementById('txtHasta').value = "";
            document.getElementById('txtPersona').value = ""; 
                     
                 
        }
    }

</script>

<body>
<script type="text/javascript" language="javascript">
    setTimeout("FocusPistola()", 3000);
</script>
<form id="form1" runat="server" onKeyDown="if(event.keyCode==13) event.keyCode=9;">
    
<asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="3600">
    </asp:ScriptManager>

    <div>
        <table align="center" border="0" cellpadding="0" cellspacing="0" class="TamanoTabla">
            <tr>
                <td style="height: 11px">
                    <uc1:CabeceraMenu ID="CabeceraMenu1" runat="server" />
                </td>
            </tr>
        </table>
        <table align="center" border="0" cellpadding="0" cellspacing="0" class="TamanoTabla">
            <tr>
                <td>

                            <table align="center" border="0" cellpadding="0" cellspacing="0" class="TamanoTabla"
                                id="tableRehabilitar" runat="server">
                                <tr class="formularioTitulo">
                                    <td align="left">
                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                            <ContentTemplate>
                                                <asp:Button ID="btnConciliar" runat="server" CssClass="Boton" onKeyDown="if(event.keyCode==13) event.keyCode=9;"
                                                onclick="btnConciliar_Click" OnClientClick="return confirmarConciliacion()" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    
                                    
                                        &nbsp;<br />
                                        <table cellpadding="0" cellspacing="0" width="90%">
                                            <tr>
                                                <td class="style2">
                                                    <table cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td>
                                                                <asp:RadioButton ID="rbPistola" runat="server" Checked="True" 
                                                                    CssClass="titulopopupMV" GroupName="Busqueda" 
                                                                    OnClick="ValidarhabilitarPistola()" Text="Pistola" />
                                                            </td>
                                                            <td>
                                                                <asp:RadioButton ID="rbManual" runat="server" CssClass="titulopopupMV" 
                                                                    GroupName="Busqueda" OnClick="ValidarhabilitarPistola()" Text="Manual" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:CheckBox ID="chkFechaUso" runat="server" CssClass="titulopopupMV" 
                                                        OnClick="validarFechas()" Text="Fecha de Uso:" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 50%">
                                                    <table cellpadding="0" cellspacing="0" width="100%">
                                                        <tr>
                                                            <td>
                                                                <table>
                                                                    <tr>
                                                                        <td style="width: 20%">
                                                                            <asp:Label ID="lblDesde" runat="server" CssClass="TextoEtiqueta" Text="Desde:"></asp:Label>
                                                                        </td>
                                                                        <td style="width: 60%">
                                                                            <asp:TextBox ID="txtDesde" runat="server" BackColor="#E4E2DC" 
                                                                                CssClass="textbox" onfocus="this.blur();" 
                                                                                onkeypress="JavaScript: window.event.keyCode=0;"></asp:TextBox>
                                                                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" 
                                                                                Format="dd/MM/yyyy" PopupButtonID="imgbtnCalendar1" PopupPosition="BottomLeft" 
                                                                                TargetControlID="txtDesde">
                                                                            </cc1:CalendarExtender>
                                                                        </td>
                                                                        <td style="width: 20%">
                                                                            <asp:ImageButton ID="imgbtnCalendar1" runat="server" Height="22px" 
                                                                                ImageUrl="~/Imagenes/Calendar.bmp" Width="22px" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td>
                                                                <table>
                                                                    <tr>
                                                                        <td style="width: 30%">
                                                                            <asp:Label ID="lblCia" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                                                        </td>
                                                                        <td style="width: 70%">
                                                                            <asp:DropDownList ID="cboCompanias" runat="server" CssClass="combo" onKeyDown="if(event.keyCode==13) event.keyCode=9;" 
                                                                                Width="150px">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <table>
                                                                    <tr>
                                                                        <td style="width: 20%">
                                                                            <asp:Label ID="lblHasta" runat="server" CssClass="TextoEtiqueta" Text="Hasta:"></asp:Label>
                                                                        </td>
                                                                        <td style="width: 60%">
                                                                            <asp:TextBox ID="txtHasta" runat="server" BackColor="#E4E2DC" 
                                                                                CssClass="textbox" onfocus="this.blur();" 
                                                                                onkeypress="JavaScript: window.event.keyCode=0;"></asp:TextBox>
                                                                            <cc1:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="True" 
                                                                                Format="dd/MM/yyyy" PopupButtonID="imgbtnCalendar2" PopupPosition="BottomLeft" 
                                                                                TargetControlID="txtHasta">
                                                                            </cc1:CalendarExtender>
                                                                        </td>
                                                                        <td style="width: 20%">
                                                                            <asp:ImageButton ID="imgbtnCalendar2" runat="server" Height="22px" 
                                                                                ImageUrl="~/Imagenes/Calendar.bmp" Width="22px" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td>
                                                                <table>
                                                                    <tr>
                                                                        <td style="width: 30%">
                                                                            <asp:Label ID="lblAsiento" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                                                        </td>
                                                                        <td style="width: 70%">
                                                                            <asp:TextBox ID="txtAsiento" runat="server" CssClass="textbox" MaxLength="4" onKeyDown="if(event.keyCode==13) event.keyCode=9;"
                                                                                Width="150px"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 50%">
                                                    <table cellpadding="0" cellspacing="0" width="100%">
                                                        <tr>
                                                            <td>
                                                                <table>
                                                                    <tr>
                                                                        <td style="width: 30%">
                                                                            <asp:Label ID="lblFechaVuelo" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                                                        </td>
                                                                        <td style="width: 60%">
                                                                            <asp:TextBox ID="txtFechaVuelo" runat="server" BackColor="#E4E2DC" 
                                                                                CssClass="textbox" onfocus="this.blur();" 
                                                                                onkeypress="JavaScript: window.event.keyCode = 0;" Width="150px"></asp:TextBox>
                                                                            <cc1:CalendarExtender ID="txtFechaVuelo_CalendarExtender" runat="server" 
                                                                                Enabled="True" Format="dd/MM/yyyy" PopupButtonID="imgbtnCalendar" 
                                                                                PopupPosition="BottomLeft" TargetControlID="txtFechaVuelo">
                                                                            </cc1:CalendarExtender>
                                                                        </td>
                                                                        <td style="width: 10%">
                                                                            <asp:ImageButton ID="imgbtnCalendar" runat="server" Height="22px" 
                                                                                ImageUrl="~/Imagenes/Calendar.bmp" Width="22px" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td>
                                                                <table align="right">
                                                                    <tr>
                                                                        <td style="width: 30%">
                                                                            <asp:Label ID="lblVuelo" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                                                        </td>
                                                                        <td style="width: 70%">
                                                                            <asp:TextBox ID="txtNroVuelo" runat="server" CssClass="textbox" MaxLength="6" 
                                                                                onKeyDown="if(event.keyCode==13) event.keyCode=9;" Width="150px"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 50%">
                                                                <table>
                                                                    <tr>
                                                                        <td style="width: 30%">
                                                                            <asp:Label ID="lblPersona" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                                                        </td>
                                                                        <td style="width: 70%">
                                                                            <asp:TextBox ID="txtPersona" runat="server" CssClass="textbox" 
                                                                                onKeyDown="CapturarTrama(event)" Width="179px"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td style="width: 50%; text-align: right;">
                                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:Button ID="btnBuscar" runat="server" CssClass="Boton"                                                                     
                                                                                    onclick="btnBuscar_Click" OnClientClick="return Validaciones()" TabIndex="1" 
                                                                                    onKeyDown="if(event.keyCode==13) event.keyCode=9;"
                                                                                    Text="Agregar" />
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    &nbsp;</td>
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
                                    <td>
                                        &nbsp;</td>
                                </tr>
                            </table>
                        
                </td>
            </tr>
        </table>
                <asp:Panel ID="pnlPrincipal" runat="server" Visible="true">
                    <table align="center" border="0" cellpadding="0" cellspacing="0" class="TamanoTabla">
                        <tr>
                            <td>
                                <div>
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                        <tr>
                                            <td style="width: 100%">
                                                <table width="100%" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td style="width: 0%">
                                                        </td>
                                                        <td style="width: 100%">
                                                            <!-- inicio -->                                                           
                                                                                                                        
                                                            
                                                            <br />
                                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                            <ContentTemplate>
                                                            <asp:Label ID="lblErrorMsg" runat="server" CssClass="mensaje"></asp:Label>
                                                            <asp:Label ID="lblMensajeError" runat="server" CssClass="titulopopupMV"></asp:Label>
                                                            <asp:HiddenField ID="hFechaActual" runat="server" />
                                                                <asp:HiddenField ID="hCodigoBarra" runat="server" />
                                                            <br />
                                                            <table style="width: 100%;" cellpadding="0" cellspacing="0" border="0">
                                                                <tr>
                                                                    <td width="0%">
                                                                    </td>
                                                                    <td>

                                                                        <div id="divGrid" style="height: 505px; overflow: auto;">
                                                                            <asp:GridView ID="gvwConciliaBcbcp" runat="server" AllowPaging="True" 
                                                                                OnPageIndexChanging="gvwConciliaBcbcp_PageIndexChanging"
                                                                                AutoGenerateColumns="False" CssClass="grilla" 
                                                                                  RowStyle-HorizontalAlign="Center" 
                                                                                RowStyle-VerticalAlign="Middle" Width="100%">
                                                                                <SelectedRowStyle CssClass="grillaFila" />
                                                                                <PagerStyle CssClass="grillaPaginacion" />
                                                                                <HeaderStyle CssClass="grillaCabecera" />
                                                                                <AlternatingRowStyle CssClass="grillaFila" />
                                                                                <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                <Columns>
                                                                                    <asp:BoundField DataField="Numero" HeaderText="Nro." ItemStyle-Width="4%" 
                                                                                        SortExpression="Numero">
                                                                                        <ItemStyle Width="4%" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="compania" HeaderText="Compania" 
                                                                                        ItemStyle-Width="8%" SortExpression="compania">
                                                                                        <ItemStyle Width="8%" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="fecha_vuelo" HeaderText="Fecha Vuelo" 
                                                                                        ItemStyle-Width="8%" SortExpression="fecha_vuelo">
                                                                                        <ItemStyle Width="8%" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="num_vuelo" HeaderText="Nro. Vuelo" 
                                                                                        ItemStyle-Width="8%" SortExpression="num_vuelo">
                                                                                        <ItemStyle Width="8%" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="asiento" HeaderText="Asiento" 
                                                                                        ItemStyle-Width="8%" SortExpression="asiento">
                                                                                        <ItemStyle Width="8%" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="persona" HeaderText="Persona" 
                                                                                        ItemStyle-Width="17%" SortExpression="persona">
                                                                                        <ItemStyle Width="17%" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="fecha_uso" HeaderText="Fecha Uso" 
                                                                                        ItemStyle-Width="8%" SortExpression="fecha_uso">
                                                                                        <ItemStyle Width="8%" />
                                                                                    </asp:BoundField>
                                                                                    <asp:TemplateField HeaderText="Boarding Pass Rehabilitados">
                                                                                        <ItemTemplate>
                                                                                            <asp:CheckBoxList ID="CheckBoxList1" runat="server">
                                                                                                <asp:ListItem Value="Num_Secuencial_Bcbp">dsc_bcbp_reh</asp:ListItem>
                                                                                            </asp:CheckBoxList>
                                                                                            <asp:HiddenField ID="hSecuencia" runat="server" value = '<%# Eval("Num_Secuencial_Bcbp") %>' />
                                                                                        </ItemTemplate>
                                                                                        
                                                                                        <HeaderTemplate>                                                                                            
                                                                                            <asp:CheckBox ID="chkAll" runat="server" Enabled="false"  OnCheckedChanged = "btnCheck_Click"/>
                                                                                            <asp:ImageButton ID="imageButton1" runat="server" CommandArgument="Check" 
                                                                                                CommandName="Sort" Height="20" ImageAlign="AbsMiddle"  OnClientClick="return Checkear(event) " OnClick="btnCheck_Click"
                                                                                                ImageUrl="~/Imagenes/icon_check.jpg" Width="20" TabIndex="2" onKeyDown="if(event.keyCode==13) event.keyCode=9;" />
                                                                                        </HeaderTemplate>
                                                                                                                                                                                
                                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                                <EmptyDataTemplate>
                                                                                    &nbsp;
                                                                                </EmptyDataTemplate>
                                                                            </asp:GridView>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
                                                                <asp:AsyncPostBackTrigger ControlID="btnConciliar" EventName="Click" />
                                                            </Triggers>
                                                            </asp:UpdatePanel>
                                                            <asp:UpdateProgress AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="0" ID="updateProgress" runat="server">
                                                                <ProgressTemplate>            
                                                                <div id="processMessage">
                                                                    &nbsp;&nbsp;&nbsp;Procesando...<br />
                                                                     <br />
                                                                     <img alt="Loading" src="Imagenes/ajax-loader.gif" />
                                                                 </div>
                                                                </ProgressTemplate>
                                                             </asp:UpdateProgress>
                                                             
                                                            <asp:UpdateProgress AssociatedUpdatePanelID="UpdatePanel2" DisplayAfter="0" ID="updateProgress1" runat="server">
                                                                <ProgressTemplate>            
                                                                <div id="processMessage">
                                                                    &nbsp;&nbsp;&nbsp;Procesando...<br />
                                                                     <br />
                                                                     <img alt="Loading" src="Imagenes/ajax-loader.gif" />
                                                                 </div>
                                                                </ProgressTemplate>
                                                             </asp:UpdateProgress>     
                                                             
                                                            <asp:UpdateProgress AssociatedUpdatePanelID="UpdatePanel3" DisplayAfter="0" ID="updateProgress2" runat="server">
                                                                <ProgressTemplate>            
                                                                <div id="processMessage">
                                                                    &nbsp;&nbsp;&nbsp;Procesando...<br />
                                                                     <br />
                                                                     <img alt="Loading" src="Imagenes/ajax-loader.gif" />
                                                                 </div>
                                                                </ProgressTemplate>
                                                             </asp:UpdateProgress>                                                                                                                          
                                                             
                                                            <br />
                                                            <table style="width: 100%;" cellpadding="0" cellspacing="0" border="0">
                                                                <tr>
                                                                    <td colspan="3">
                                                                        <hr class="EspacioLinea" color="#0099cc" />
                                                                    </td>
                                                                </tr>
                                                                <tr class="formularioTitulo">
                                                                    <td width="2%" height="25px">
                                                                    </td>
                                                                    <td width="15%" class="TextoEtiqueta">
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr class="formularioTitulo">
                                                                    <td height="25px">
                                                                    </td>
                                                                    <td class="TextoEtiqueta">
                                                                    </td>
                                                                    <td>
                                                                       
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="3">
                                                                        <hr class="EspacioLinea" color="#0099cc" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <input type="hidden" id="hdNumSelTotal" runat="server" value="0" />
                                                            <input type="hidden" id="hdNumSelConObs" runat="server" value="0" />
                                                            <!--FIN-->
                                                            <uc6:PiePagina ID="PiePagina2" runat="server" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>

    </div>

    <script type="text/javascript">
    function Atras() {
      window.location.href = "./Reh_BCBP.aspx";
    }
    function fnClickCerrarPopup(sender, e) {
      __doPostBack(sender, e);
      alert('llego');
    }
    
    function fnClickCerrarPopup() {
      alert('llego');
    }    
    </script>

    <uc3:ConsRepresentante ID="consRepre" runat="server" />
    <uc4:CnsDetBoarding ID="CnsDetBoarding1" runat="server" />
    <uc5:IngBoarding ID="IngBoarding1" runat="server" />
    
    <asp:HiddenField ID="hBandera" runat="server" Value="1" />
    
    <br />
    </form>
    
<script language="javascript" type="text/javascript">    
    document.getElementById('imgbtnCalendar1').disabled = true;
    document.getElementById('imgbtnCalendar2').disabled = true;

    document.getElementById('cboCompanias').disabled = true;
    document.getElementById('txtAsiento').disabled = true;
    document.getElementById('cboCompanias').disabled = true;
    document.getElementById('imgbtnCalendar').disabled = true;
    document.getElementById('txtNroVuelo').disabled = true;
    document.getElementById('chkFechaUso').disabled = true;    
</script>
    
</body>
</html>
