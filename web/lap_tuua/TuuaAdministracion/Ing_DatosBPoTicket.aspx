<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Ing_DatosBPoTicket.aspx.cs" Inherits="Mnt_PuntoVenta" %>

<%@ Register Src="UserControl/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="uc1" %>
<%@ Register Src="UserControl/PiePagina.ascx" TagName="PiePagina" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<script>
    function enterPressed(e)
    {
        if (e.keyCode == 13) 
        {
            document.getElementById("btnProcesarDatos").click();
            
            //PageMethods.ProcesarDatos();
            //alert("hola");
        }
    }
    
    function clickPressed(e)
    {
        document.getElementById("txtResultado").value = "";
    }
    
    function changePressed(e)
    {
        document.getElementById("txtResultado").value = "";
        document.getElementById("txtDatoIngresado").focus();
    }
</script>
<head runat="server">
    <title>LAP-Ingreso de Datos de Tickets/BP por salida</title>
    <link href="css/Style.css" rel="stylesheet" type="text/css" />
    <link href="../css/Style.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            width: 156px;
        }
        .style6
        {
            width: 89px;
        }
        .style7
        {
            width: 298px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table align="center" border="0" cellpadding="0" cellspacing="0" class="TamanoTabla">
            <tr>
                <td class="Espacio1FilaTabla" style="height: 11px">
                    <uc1:CabeceraMenu ID="CabeceraMenu1" runat="server" />
                </td>
            </tr>
            <tr class="formularioTitulo">
                <!-- WORK MENU -->
                <td align="left">
                    <asp:Label ID="lblDatoIngresado" Text="Escanear Boardingpass o Sticker:" runat="server" CssClass="TextoEtiqueta" style="margin-top:10px"></asp:Label><br />
                    <asp:TextBox ID="txtDatoIngresado" runat="server" style="width: 350px;" onkeypress="return enterPressed(event)" onclick="clickPressed(event)" onchange="changePressed(event)" clientidmode="static" ></asp:TextBox>
                    <br /><br />
                    <asp:TextBox ID="txtResultado" runat="server"  style="width: 350px; font-size:12px; margin-bottom:10px"></asp:TextBox>
                    <asp:Button ID="btnProcesarDatos" runat="server" 
                        onclick="btnProcesarDatos_Click" style="display:none"  />
                </td>
            </tr>
            <tr>
                <td>
                    <!-- SPACE -->
                    <hr class="EspacioLinea" color="#0099cc" />
                </td>
            </tr>
            <tr>
                <td>
                <!-- INICIO GRILLA -->
                <div>
                    <table cellpadding="0" cellspacing="0" class="EspacioSubTablaPrincipal">
                        <tr>
                            <td class="SpacingGrid">
                            </td>
                            <td class="CenterGrid">
                                <asp:ScriptManager ID="ScriptManager1" runat="server">
                                </asp:ScriptManager>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="gwvTickets" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                            CellPadding="3" Width="100%" BackColor="White" BorderStyle="None" BorderWidth="1px"
                                            CssClass="grilla" AllowSorting="True"
                                            OnSorting="gwvTickets_Sorting">
                                            <Columns>
                                                <asp:BoundField DataField="Num_Ticket" HeaderText="#" SortExpression="Num_Ticket" ControlStyle-Width="100%" />
                                                <asp:BoundField DataField="Fch_Registro" HeaderText="Fecha Registro" SortExpression="Fch_Registro" ControlStyle-Width="100px" />
                                            </Columns>
                                            <SelectedRowStyle CssClass="grillaFila" />
                                            <PagerStyle CssClass="grillaPaginacion" />
                                            <HeaderStyle CssClass="grillaCabecera" />
                                            <AlternatingRowStyle CssClass="grillaFila" />
                                        </asp:GridView>
                                        <asp:Label ID="lblMensajeError" runat="server" CssClass="msgMensaje"></asp:Label>
                                        <asp:Label ID="lblTotal" runat="server" CssClass="TextoCampo"></asp:Label>
                                        <asp:UpdateProgress ID="upgUsuario" runat="server">
                                            <ProgressTemplate>
                                                <asp:Label ID="lblProcesando" runat="server" Style="text-align: left" Text="Procesando"></asp:Label>
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td class="SpacingGrid">
                            </td>
                        </tr>
                    </table>
                </div>
                <!-- FIN GRILLA -->
            </td>
            </tr>
            <tr>
            <!-- FOOTER -->
            <td class="Espacio1FilaTabla" style="height: 11px">
                <uc2:PiePagina ID="PiePagina2" runat="server" />
            </td>
        </tr>
        </table>
    </div>
    <asp:Label ID="txtOrdenacion" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="txtColumna" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="txtValorMaximoGrilla" runat="server" Visible="False"></asp:Label>
    </form>
</body>
</html>
