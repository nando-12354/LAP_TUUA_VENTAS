<%@ page language="C#" autoeventwireup="true" inherits="Modulo_Consultas_ConsultaUsuarios, App_Web_tx1el90t" %>

<%@ Register Src="UserControl/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="uc1" %>
<%@ Register Src="UserControl/PiePagina.ascx" TagName="PiePagina" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LAP - Consulta - Usuario</title>
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
    <link href="css/Style.css" rel="stylesheet" type="text/css" />
    <!-- #INCLUDE file="javascript/validarteclaF5.js" -->
    <!-- #INCLUDE file="javascript/MantenerSesion.js" -->
    
    <script language="javascript" type="text/javascript">
        function imgPrint_onclick() {
            var idRol = document.getElementById("ddlRol").value;
            var idEstado = document.getElementById("ddlEstado").value;
            var idGrupo = document.getElementById("ddlGrupo").value;

            //Descripciones
            var idDscR = (idRol != "0") ? document.getElementById("ddlRol").options[document.getElementById("ddlRol").selectedIndex].text : "";
            var idDscE = (idEstado != "0") ? document.getElementById("ddlEstado").options[document.getElementById("ddlEstado").selectedIndex].text : "";
            var idDscG = (idGrupo != "0") ? document.getElementById("ddlGrupo").options[document.getElementById("ddlGrupo").selectedIndex].text : "";

            var idOrdenacion = null;
            var idColumna = null;
            var idPaginacion = null;

            var w = 900 + 32;
            var h = 500 + 96;
            var wleft = (screen.width - w) / 2;
            var wtop = (screen.height - h) / 2;

            var ventimp = window.open("ReporteCNS/rptUsuarios.aspx" + "?idRol=" + idRol + "&idEstado=" + idEstado + "&idGrupo=" + idGrupo + "&idOrdenacion=" + idOrdenacion + "&idColumna=" + idColumna + "&idPaginacion=" + idPaginacion + "&idDscR=" + idDscR + "&idDscE=" + idDscE + "&idDscG=" + idDscG, "mywindow",
                                      "location=0,status=0,scrollbars=1,menubar=0,width=900,height=500,left=" + wleft + ",top=" + wtop);
            //ventimp.moveTo(wleft, wtop);
            ventimp.focus();
        }

    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <table border="0" cellpadding="0" cellspacing="0" class="TamanoTabla" align="center">
            <tr>
                <!-- HEADER -->
                <td align="center">
                    <uc1:CabeceraMenu ID="CabeceraMenu1" runat="server" />
                </td>
            </tr>
            <tr class="formularioTitulo2">
                <!-- FILTER -->
                <td>
                    <table border="0" cellpadding="0" cellspacing="0" class="EspacioSubTablaPrincipal">
                        <tr>
                            <td align="left" style="height: 30px; width: 80%">
                                &nbsp;&nbsp;&nbsp;<asp:Label ID="lblRol" runat="server" CssClass="TextoFiltro"></asp:Label>
                                &nbsp;&nbsp;&nbsp;<asp:DropDownList ID="ddlRol" runat="server" CssClass="combo2"
                                    Width="200px">
                                    <asp:ListItem Value="0">Todos</asp:ListItem>
                                </asp:DropDownList>
                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                <asp:Label ID="lblEstado" runat="server" CssClass="TextoFiltro"></asp:Label>
                                &nbsp;&nbsp;&nbsp;<asp:DropDownList ID="ddlEstado" runat="server" CssClass="combo2"
                                    Width="200px">
                                </asp:DropDownList>
                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                <asp:Label ID="lblGrupo" runat="server" CssClass="TextoFiltro"></asp:Label>
                                &nbsp;&nbsp;&nbsp;<asp:DropDownList ID="ddlGrupo" runat="server" CssClass="combo2"
                                    Width="200px">
                                </asp:DropDownList>
                            </td>
                            <td align="right">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <a href="" id="lnkHabilitar" runat="server" onclick="javascript:imgPrint_onclick();"
                                            style="cursor: hand;"><b>
                                                <asp:Label ID="lblImprimir" runat="server">Imprimir</asp:Label>
                                            </b></a>&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btnConsultar" runat="server" OnClick="cmdConsultar_Click" CssClass="Boton"
                                            Style="cursor: hand;" />&nbsp;&nbsp;&nbsp;
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
                                    <ProgressTemplate>
                                        <div id="processMessage">
                                            &nbsp;&nbsp;&nbsp;Procesando...<br />
                                            <br />
                                            <img alt="Loading" src="Imagenes/ajax-loader.gif" />
                                        </div>
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <!-- SPACE -->
                <td>
                    <hr color="#0099cc" style="width: 100%; height: 0px" />
                </td>
            </tr>
            <tr>
                <!-- CONTENT -->
                <td>
                    <table border="0" cellpadding="0" cellspacing="0" class="EspacioSubTablaPrincipal">
                        <tr>
                            <td class="SpacingGrid">
                            </td>
                            <td>
                                <div id="divData" class="divSizeCustom">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <asp:GridView ID="grvUsuarios" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical"
                                                OnPageIndexChanging="grvUsuarios_PageIndexChanging" PageSize="3" Width="100%"
                                                CssClass="grilla" AllowSorting="True" OnSorting="grvUsuarios_Sorting" OnRowDataBound="grvUsuarios_RowDataBound">
                                                <Columns>
                                                    <asp:BoundField DataField="Cod_Usuario" HeaderText="C&#243;digo" ReadOnly="True"
                                                        SortExpression="Cod_Usuario">
                                                        <HeaderStyle Width="10%" />
                                                        <ItemStyle Width="10%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Cta_Usuario" HeaderText="Cuenta" SortExpression="Cta_Usuario" />
                                                    <asp:BoundField HeaderText="Nombre" DataField="Nom_Usuario" SortExpression="Nom_Usuario">
                                                        <HeaderStyle Width="20%" />
                                                        <ItemStyle Width="20%" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="Fecha Creación" SortExpression="Fch_Creacion,Hor_Creacion">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblITFecha" runat="server" Text='<%# LAP.TUUA.UTIL.Fecha.convertSQLToFecha(Convert.ToString(Eval("Fch_Creacion")),Convert.ToString(Eval("Hor_Creacion"))) %> '></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Nom_Grupo" HeaderText="Grupo" SortExpression="Nom_Grupo" />
                                                    <asp:BoundField HeaderText="Roles Asociados" DataField="Nom_Rol" SortExpression="Nom_Rol">
                                                        <HeaderStyle Width="15%" />
                                                        <ItemStyle Width="15%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Estado" DataField="Nom_Estado" SortExpression="Nom_Estado">
                                                        <ItemStyle Width="15%" />
                                                        <HeaderStyle Width="15%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Dias por Vencer Cuenta Usuario" DataField="Dias_xVencer"
                                                        SortExpression="Dias_xVencer">
                                                        <ItemStyle Width="20%" />
                                                        <HeaderStyle Width="20%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Dias por Vencer Clave" DataField="Dias_xVenceClave" SortExpression="Dias_xVenceClave">
                                                        <ItemStyle Width="20%" />
                                                        <HeaderStyle Width="20%" />
                                                    </asp:BoundField>
                                                </Columns>
                                                <SelectedRowStyle CssClass="grillaFila" />
                                                <PagerStyle CssClass="grillaPaginacion" />
                                                <HeaderStyle CssClass="grillaCabecera" />
                                                <AlternatingRowStyle CssClass="grillaFila" />
                                            </asp:GridView>
                                            <asp:Label ID="lblMensajeErrorData" runat="server" CssClass="msgMensaje" Width="200px"></asp:Label>
                                            <br />
                                            <asp:Label ID="lblTotal" runat="server" CssClass="TextoCampo"></asp:Label>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnConsultar" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                    <asp:UpdateProgress ID="UpdateProgress2" AssociatedUpdatePanelID="UpdatePanel2" runat="server"
                                        DisplayAfter="10">
                                        <ProgressTemplate>
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
                    <uc2:PiePagina ID="PiePagina1" runat="server" />
                    <br />
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
