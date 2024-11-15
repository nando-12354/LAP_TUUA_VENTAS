<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Alr_ConsultarAlarma.aspx.cs"
    Inherits="Alr_ConsultarAlarma" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="UserControl/ConsBCBP.ascx" TagName="consbcbp" TagPrefix="uc5" %>
<%@ Register Src="UserControl/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="uc1" %>
<%@ Register Src="UserControl/PiePagina.ascx" TagName="PiePagina" TagPrefix="uc2" %>
<%@ Register Src="UserControl/OKMessageBox.ascx" TagName="okmessagebox" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LAP - Alarmas - Alarmas Generadas</title>
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
    <link href="css/Style.css" rel="stylesheet" type="text/css" />
    <!-- #INCLUDE file="javascript/ValidarFecha.js" -->
    <!-- #INCLUDE file="javascript/KeyPress.js" -->
    <!-- #INCLUDE file="javascript/MantenerSesion.js" -->
    <!-- #INCLUDE file="javascript/validarteclaF5.js" -->
    <!-- #INCLUDE file="javascript/Functions.js" -->

    <script language="javascript" type="text/javascript">

        function controlaFechaDesde() {

            if (window.event.keyCode == 8 || window.event.keyCode == 46) {
                form1.txtDesde.value = form1.txtDesde.value.substring(0, form1.txtDesde.value.length - 2);
            }
            else {
                if (form1.txtDesde.value.length == 2) {
                    form1.txtDesde.value = form1.txtDesde.value + '/';
                }
                if (form1.txtDesde.value.length == 5) {
                    form1.txtDesde.value = form1.txtDesde.value + '/';
                }
            }
        }



        function controlaFechaHasta() {

            if (window.event.keyCode == 8 || window.event.keyCode == 46) {
                form1.txtHasta.value = form1.txtHasta.value.substring(0, form1.txtHasta.value.length - 2);
            }
            else {

                if (form1.txtHasta.value.length == 2) {
                    form1.txtHasta.value = form1.txtHasta.value + '/';
                }
                if (form1.txtHasta.value.length == 5) {
                    form1.txtHasta.value = form1.txtHasta.value + '/';
                }
            }
        }


        function controlaHoraDesde() {

            if (window.event.keyCode == 8 || window.event.keyCode == 46) {
                form1.txtHoraDesde.value = form1.txtHoraDesde.value.substring(0, form1.txtHoraDesde.value.length - 2);
            }
            else {

                if (form1.txtHoraDesde.value.length == 2) {
                    form1.txtHoraDesde.value = form1.txtHoraDesde.value + ':';
                }
                if (form1.txtHoraDesde.value.length == 5) {
                    form1.txtHoraDesde.value = form1.txtHoraDesde.value + ':';
                }
            }
        }

        function controlaHoraHasta() {

            if (window.event.keyCode == 8 || window.event.keyCode == 46) {
                form1.txtHoraHasta.value = form1.txtHoraHasta.value.substring(0, form1.txtHoraHasta.value.length - 2);
            }
            else {

                if (form1.txtHoraHasta.value.length == 2) {
                    form1.txtHoraHasta.value = form1.txtHoraHasta.value + ':';
                }
                if (form1.txtHoraHasta.value.length == 5) {
                    form1.txtHoraHasta.value = form1.txtHoraHasta.value + ':';
                }
            }

        }

        function validarParametros() {

            if (document.getElementById('txtDesde').value != "" && document.getElementById('txtHasta').value != "") {

                if (document.getElementById('txtHoraDesde').value == "__:__:__") {
                    //document.getElementById('lblErrorMsg').innerHtml = "Ingrese la hora desde";
                    document.getElementById('txtHoraDesde').value = "00:00:00";
                    //return false;
                }

                if (document.getElementById('txtHoraHasta').value == "__:__:__") {
                    //document.getElementById('lblErrorMsg').innerHtml = "Ingrese la hora hasta";
                    document.getElementById('txtHoraHasta').value = "23:59:59";
                    //return false;
                }

                //                if (document.getElementById('txtDesde').value == document.getElementById('txtHasta').value &&
                //                document.getElementById('txtHoraHasta').value == "00:00:00" &&
                //                document.getElementById('txtHoraDesde').value == "00:00:00" &&
                //                document.getElementById('txtDesde').value != "") {
                //                    document.getElementById('txtHoraHasta').value = "23:59:59";
                //                } 

            }
            accionConsult = true;
            return true;
        }

        var accionConsult = false;
        function beginRequest(sender, args) {
            if (!sender.get_isInAsyncPostBack()) {
                if (accionConsult) {
                    document.getElementById('btnBuscar').disabled = true;
                    document.body.style.cursor = 'wait';
                }
            }
        }

        function endRequest(sender, args) {
            if (!sender.get_isInAsyncPostBack()) {
                if (accionConsult) {
                    document.getElementById('btnBuscar').disabled = false;
                    document.body.style.cursor = 'default'
                    accionConsult = false;
                }
            }
        }  
        
  
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="600">
            <Scripts>
                <asp:ScriptReference Path="~/javascript/jSManager.js" />
            </Scripts>
        </asp:ScriptManager>
        <table align="center" border="0" cellpadding="0" cellspacing="0" class="TamanoTabla">
            <tr>
                <!-- HEADER -->
                <td align="center">
                    <uc1:CabeceraMenu ID="CabeceraMenu2" runat="server" />
                </td>
            </tr>
            <tr class="formularioTitulo2">
                <!-- FILTER -->
                <td align="right" style="text-align: left">
                    <table>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <table style="width: 100%; left: 4px; position: relative; top: 0px;" class="alineaderecha">
                                    <tr>
                                        <td>
                                            <fieldset>
                                                <legend>
                                                    <asp:Label ID="lblFechaGeneracion" runat="server" CssClass="TextoEtiqueta"></asp:Label></legend>
                                                <table>
                                                    <tr>
                                                        <td>
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblDesde" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtDesde" runat="server" BackColor="#E4E2DC" CssClass="textbox"
                                                                onfocus="this.blur();" onkeypress="JavaScript: window.event.keyCode = 0;" Width="72px"></asp:TextBox>
                                                            <cc1:CalendarExtender ID="txtDesde_CalendarExtender" runat="server" Enabled="True"
                                                                Format="dd/MM/yyyy" PopupButtonID="imbCalDesde" TargetControlID="txtDesde">
                                                            </cc1:CalendarExtender>
                                                        </td>
                                                        <td>
                                                            <asp:ImageButton ID="imbCalDesde" runat="server" Height="22px" ImageUrl="~/Imagenes/Calendar.bmp"
                                                                Width="22px" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblFechaDesde" runat="server" CssClass="TextoFiltro" Text="(dd/mm/yyyy)"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtHoraDesde" runat="server" CssClass="textbox" MaxLength="8" onBlur="JavaScript:CheckTime(this)"
                                                                onkeypress="JavaScript: Tecla('Time');" ReadOnly="false" Width="56px"></asp:TextBox>
                                                            <cc1:MaskedEditExtender ID="valHoraDesde" runat="server" Mask="99:99:99" TargetControlID="txtHoraDesde"
                                                                ClearMaskOnLostFocus="False" MaskType="Time" UserTimeFormat="TwentyFourHour"
                                                                CultureName="es-ES" PromptCharacter="_" AutoComplete="False">
                                                            </cc1:MaskedEditExtender>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblHoraDesde" runat="server" CssClass="TextoFiltro" Text="(hh:mm:ss)"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblHasta" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtHasta" runat="server" BackColor="#E4E2DC" CssClass="textbox"
                                                                onfocus="this.blur();" onkeypress="JavaScript: window.event.keyCode = 0;" Width="72px"></asp:TextBox>
                                                            <cc1:CalendarExtender ID="txtHasta_CalendarExtender" runat="server" Enabled="True"
                                                                Format="dd/MM/yyyy" PopupButtonID="imbCalHasta" TargetControlID="txtHasta">
                                                            </cc1:CalendarExtender>
                                                        </td>
                                                        <td>
                                                            <asp:ImageButton ID="imbCalHasta" runat="server" Height="22px" ImageUrl="~/Imagenes/Calendar.bmp"
                                                                Width="22px" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblFechaHasta" runat="server" CssClass="TextoFiltro" Text="(dd/mm/yyyy)"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtHoraHasta" runat="server" CssClass="textbox" MaxLength="8" onBlur="JavaScript:CheckTime(this)"
                                                                onkeypress="JavaScript: Tecla('Time');" ReadOnly="false" Width="56px"></asp:TextBox>
                                                            <cc1:MaskedEditExtender ID="valHoraHasta" runat="server" Mask="99:99:99" TargetControlID="txtHoraHasta"
                                                                ClearMaskOnLostFocus="False" MaskType="Time" UserTimeFormat="TwentyFourHour"
                                                                CultureName="es-ES" PromptCharacter="_" AutoComplete="False">
                                                            </cc1:MaskedEditExtender>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblHoraHasta" runat="server" CssClass="TextoFiltro" Text="(hh:mm:ss)"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </fieldset>
                                        </td>
                                        <td>
                                            <table>
                                                <tr>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblModulo" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlModulo" runat="server" CssClass="combo" Width="224px">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblTipoAlarma" runat="server" CssClass="TextoEtiqueta" Width="80px"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlTipoAlarma" runat="server" CssClass="combo" Width="224px">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblEstado" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlEstado" runat="server" CssClass="combo" Width="224px">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="10%">
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Button ID="btnBuscar" runat="server" CausesValidation="False" OnClick="btnBuscar_Click"
                                            OnClientClick="return validarParametros()" CssClass="Boton" Style="cursor: hand;"
                                            Text="Consultar" />&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="UpdatePanel3" runat="server"
                                    DisplayAfter="10">
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
                            </td>
                            <td width="10%">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                    <br />
                </td>
            </tr>
            <tr>
                <!-- SPACE -->
                <td>
                    <hr class="EspacioLinea" color="#0099cc" />
                </td>
            </tr>
            <tr>
                <!-- DATA -->
                <td>
                    <table cellpadding="0" cellspacing="0" class="EspacioSubTablaPrincipal">
                        <tr>
                            <td class="SpacingGrid">
                            </td>
                            <td class="CenterGrid">
                                <div id="divData" class="divSizeCustom">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <table class="alineaderecha" style="width: 100%; left: 0px;">
                                                <tr>
                                                    <td align="left" class="CenterGrid">
                                                        <asp:GridView ID="gwvAlarmasGeneradas" runat="server" AllowPaging="True" AllowSorting="True"
                                                            AutoGenerateColumns="False" CssClass="grilla" OnPageIndexChanging="gwvAlarmasGeneradas_PageIndexChanging"
                                                            OnSorting="gwvAlarmasGeneradas_Sorting" RowStyle-HorizontalAlign="Center" RowStyle-VerticalAlign="Middle">
                                                            <SelectedRowStyle CssClass="grillaFila" />
                                                            <PagerStyle CssClass="grillaPaginacion" />
                                                            <HeaderStyle CssClass="grillaCabecera" />
                                                            <AlternatingRowStyle CssClass="grillaFila" />
                                                            <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            <Columns>
                                                                <asp:BoundField DataField="Cod_AlarmaGenerada" HeaderText="AlarmaGenerada" SortExpression="Cod_AlarmaGenerada"
                                                                    Visible="False" />
                                                                <asp:BoundField DataField="Cod_Alarma" HeaderText="Alarma" SortExpression="Cod_Alarma" />
                                                                <asp:BoundField DataField="Dsc_Modulo" HeaderText="Módulo" SortExpression="Dsc_Modulo" />
                                                                <asp:BoundField DataField="Dsc_Alarma" HeaderText="Tipo Alarma" SortExpression="Dsc_Alarma" />
                                                                <asp:BoundField DataField="Dsc_Equipo" HeaderText="Equipo" SortExpression="Dsc_Equipo" />
                                                                <asp:BoundField DataField="Dsc_Body" HeaderText="Mensaje" SortExpression="Dsc_Body"
                                                                    HtmlEncode="False" HtmlEncodeFormatString="False" />
                                                                <asp:BoundField DataField="Fch_Generacion" HeaderText="Fecha Generación" SortExpression="Fch_Generacion2" />
                                                                <asp:BoundField DataField="Dsc_Tip_Importancia" HeaderText="Importancia" SortExpression="Dsc_Tip_Importancia" />
                                                                <asp:BoundField DataField="Fch_Actualizacion" HeaderText="Fecha Actualización" SortExpression="Fch_Actualizacion2" />
                                                                <asp:BoundField DataField="Dsc_Atencion" HeaderText="Descripción Actualización" SortExpression="Dsc_Atencion" />
                                                                <asp:BoundField DataField="Dsc_Tip_Estado" HeaderText="Estado" SortExpression="Dsc_Tip_Estado" />
                                                            </Columns>
                                                            <EmptyDataTemplate>
                                                            </EmptyDataTemplate>
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblTotalIngresados" runat="server" />
                                                        &nbsp;&nbsp;&nbsp;
                                                        <asp:Label ID="lblTxtIngresados" runat="server">0</asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left;">
                                                        <asp:Label ID="lblMensajeError" runat="server" CssClass="msgMensaje" Width="427px"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" Width="120px"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                    <asp:UpdateProgress ID="UpdateProgress2" AssociatedUpdatePanelID="UpdatePanel1" runat="server"
                                        DisplayAfter="100">
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
                                </div>
                            </td>
                            <td class="SpacingGrid">
                            </td>
                        </tr>
                    </table>
                    <uc2:PiePagina ID="PiePagina3" runat="server" />
                    <br />
                </td>
            </tr>
        </table>
    </div>
    <input type="hidden" id="hdNumSelTotal" runat="server" value="0" />
    <input type="hidden" id="hdNumSelConObs" runat="server" value="0" />
    </form>
</body>
</html>
