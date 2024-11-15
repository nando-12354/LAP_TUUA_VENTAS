<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FormularioConsulta.aspx.cs"
    Inherits="FormularioConsulta" Culture="Auto" UICulture="Auto" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Consulta Web</title>
    <link href="css/Style.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">
        function consultar() {
            var sIdCompania = document.forms[0].cboCompanias[document.forms[0].cboCompanias.selectedIndex].value;
            var sAsiento = document.forms[0].txtAsiento.value;
            var sNumVuelo = document.forms[0].txtNroVuelo.value;
            var sPasajero = document.forms[0].txtPasajero.value;
            var sFechaVuelo = document.forms[0].txtFechaVuelo.value;
            document.frames.IframeReporte.location.href = "rptConsulta.aspx?sIdCompania=" + sIdCompania + "&sAsiento=" + sAsiento + "&sNumVuelo=" + sNumVuelo + "&sPasajero=" + sPasajero + "&sFechaVuelo=" + sFechaVuelo;
        }
        //        function validar(){
        //            if (document.getElementById("rbtn1D").checked == true) {
        //                document.getElementById("lblFormato").innerHTML='Nro. Boarding:'
        //            }else{
        //                document.getElementById("lblFormato").innerHTML='Nombre Pasajero:'
        //            }
        //        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True">
    </asp:ScriptManager>
    <br />
    <br />
    <br />
    <br />
    <table style="width: 70%" align="center">
        <tr>
            <td style="text-align: center">
                <span class="TituloBienvenida">CONSULTA TUUA</span>
                <br />
            </td>
        </tr>
        <tr>
            <td>
                <hr color="#0099cc" style="width: 100%; height: 0px" />
            </td>
        </tr>
    </table>
    <table style="width: 70%" align="center">
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <table cellpadding="0" cellspacing="1" style="width: 100%">
                            <tr>
                                <td>
                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                        <tr>
                                            <td style="width: 30%">
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                        <tr>
                                            <td style="width: 30%">
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                        <tr>
                                            <td style="width: 30%">
                                                Compañia:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="cboCompanias" runat="server" Width="150px" CssClass="combo">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="cboCompanias"
                                                    ErrorMessage="Seleccione una compañia" Display="Dynamic">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                        <tr>
                                            <td style="width: 30%">
                                                Nro Asiento:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtAsiento" runat="server" CssClass="textbox"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtAsiento"
                                                    ErrorMessage="Ingrese su numero de asiento" Display="Dynamic">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                        <tr>
                                            <td style="width: 30%">
                                                Nro Vuelo:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtNroVuelo" runat="server" CssClass="textbox"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtNroVuelo"
                                                    ErrorMessage="Ingrese numero vuelo" Display="Dynamic">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                        <tr>
                                            <td style="width: 30%">
                                                <asp:Label ID="lblFormato" runat="server" Text="Nombre Pasajero:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtPasajero" runat="server" CssClass="textbox"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtPasajero"
                                                    ErrorMessage="Ingrese el nombre de pasajero o nro. Boarding" Display="Dynamic">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                        <tr>
                                            <td style="width: 30%">
                                                Fecha Vuelo:
                                            </td>
                                            <td>
                                                <table cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="txtFechaVuelo" runat="server" CssClass="textbox" BackColor="#E4E2DC"
                                                                onfocus="this.blur();" onkeypress="JavaScript: window.event.keyCode = 0;" Width="102px"></asp:TextBox>
                                                            <cc1:CalendarExtender ID="txtFechaVuelo_CalendarExtender0" runat="server" Enabled="True"
                                                                Format="dd/MM/yyyy" PopupButtonID="imbCalendar" TargetControlID="txtFechaVuelo"
                                                                PopupPosition="TopRight">
                                                            </cc1:CalendarExtender>
                                                        </td>
                                                        <td>
                                                            <asp:ImageButton ID="imbCalendar" runat="server" Height="22px" ImageUrl="~/Imagenes/Calendar.bmp"
                                                                Width="22px" OnClientClick="return false" />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtFechaVuelo"
                                                                ErrorMessage="Ingrese la fecha de vuelo" Display="Dynamic">*</asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <asp:Button ID="btnConsultar" runat="server" Text="Consultar" CssClass="Boton" OnClick="btnConsultar_Click" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                <hr color="#0099cc" style="width: 100%; height: 0px" />
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                <asp:Label ID="lblErrorMsg" runat="server" CssClass="mensaje"></asp:Label>
                <div style="overflow: auto; width: 100%; height: 420px; border-left: 0px gray solid;
                    border-bottom: 0px gray solid; padding: 0px; margin: 0px; z-index: 10000000;
                    float: none;">
                    <iframe id="IframeReporte" src="#" width="100%" height="100%" align="center" frameborder="0"
                        name="I1" style="z-index: 50000000;"></iframe>
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                        ShowSummary="False" />
                </div>
            </td>
        </tr>
    </table>
    <div>
    </div>
    </form>
</body>
</html>
