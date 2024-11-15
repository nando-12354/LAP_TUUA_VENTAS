<%@ page language="C#" autoeventwireup="true" inherits="Modulo_Consultas_ConsultaCompanias, App_Web_tx1el90t" %>

<%@ Register Src="UserControl/PiePagina.ascx" TagName="PiePagina" TagPrefix="uc2" %>
<%@ Register Src="UserControl/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>LAP - Consulta - Compañía</title>
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
    <link href="css/Style.css" rel="stylesheet" type="text/css" />
    <!-- #INCLUDE file="javascript/validarteclaF5.js" -->
    <!-- #INCLUDE file="javascript/MantenerSesion.js" -->
    
    <script language="javascript" type="text/javascript">
        function imgPrint_onclick() {
            var idEstado = document.getElementById("ddlEstado").value;
            var idTipo = document.getElementById("ddlTipo").value;

            //Descripciones
            var idDscE = (idEstado != "0") ? document.getElementById("ddlEstado").options[document.getElementById("ddlEstado").selectedIndex].text : "";
            var idDscT = (idTipo != "0") ? document.getElementById("ddlTipo").options[document.getElementById("ddlTipo").selectedIndex].text : "";

            var idOrdenacion = null;
            var idColumna = null;
            var idPaginacion = null;

            var w = 900 + 32;
            var h = 500 + 96;
            var wleft = (screen.width - w) / 2;
            var wtop = (screen.height - h) / 2;

            var ventimp = window.open("ReporteCNS/rptCompania.aspx" + "?idEstado=" + idEstado + "&idTipo=" + idTipo + "&idOrdenacion=" + idOrdenacion + "&idColumna=" + idColumna + "&idPaginacion=" + idPaginacion + "&idDscE=" + idDscE + "&idDscT=" + idDscT, "mywindow",
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
                                &nbsp;&nbsp;&nbsp;<asp:Label ID="lblEstado" runat="server" CssClass="TextoFiltro"></asp:Label>
                                &nbsp;&nbsp;&nbsp;<asp:DropDownList ID="ddlEstado" runat="server" CssClass="combo2"
                                    Width="200px">
                                </asp:DropDownList>
                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                <asp:Label ID="lblTipo" runat="server" CssClass="TextoFiltro"></asp:Label>
                                &nbsp;&nbsp;&nbsp;<asp:DropDownList ID="ddlTipo" runat="server" CssClass="combo2"
                                    Width="200px">
                                </asp:DropDownList>
                            &nbsp;
                                <asp:Label ID="lblGrupo" runat="server" CssClass="TextoFiltro"></asp:Label>
                                &nbsp; <asp:DropDownList ID="ddlGrupo" runat="server" CssClass="combo2"
                                    Width="200px">
                                </asp:DropDownList>
                            </td>
                            <td align="right">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <a href="" id="lnkHabilitar" runat="server" onclick="javascript:imgPrint_onclick();"
                                            style="cursor: hand;"><b>
                                                <asp:Label ID="lblImprimir" runat="server">Imprimir</asp:Label>
                                            </b></a>&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btnConsultar" runat="server" OnClick="btnConsultar_Click" CssClass="Boton"
                                            Style="cursor: hand;" />&nbsp;&nbsp;&nbsp;
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <asp:UpdateProgress ID="UpdateProgress2" AssociatedUpdatePanelID="UpdatePanel2" runat="server">
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
                    <table cellpadding="0" cellspacing="0" class="EspacioSubTablaPrincipal">
                        <tr>
                            <td class="SpacingGrid">
                            </td>
                            <td class="CenterGrid">
                            
                            
                                <div id="divData" class="divSizeCustom">
                                    <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="UpdatePanel1" runat="server"
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
                </td>
            </tr>
            <tr>
                <!-- FOOTER -->
                <td>
                <<asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <asp:Label ID="lblMensajeError" runat="server" CssClass="mensaje"></asp:Label>
                                            <asp:Label ID="lblMensajeErrorData" runat="server" CssClass="msgMensaje"></asp:Label>
                                            <asp:Label ID="lblTotal" runat="server" CssClass="TextoCampo"></asp:Label>
                                            <br />
                                            <asp:GridView ID="grvCompania" runat="server" AllowSorting="True" 
                                                AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
                                                BorderStyle="None" BorderWidth="1px" CellPadding="3" CssClass="grilla" 
                                                GridLines="Vertical" OnPageIndexChanging="grvCompania_PageIndexChanging" 
                                                OnRowDataBound="grvCompania_RowDataBound" OnSorting="grvCompania_Sorting" 
                                                PageSize="3" Width="100%">
                                                <Columns>
                                                    <asp:BoundField DataField="Cod_Usuario" HeaderText="Código" ReadOnly="True" 
                                                        SortExpression="Cod_Usuario">
                                                        <HeaderStyle Width="10%" />
                                                        <ItemStyle Width="10%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Cta_Usuario" HeaderText="Cuenta" 
                                                        SortExpression="Cta_Usuario" />
                                                    <asp:BoundField DataField="Nom_Usuario" HeaderText="Nombre" 
                                                        SortExpression="Nom_Usuario">
                                                        <HeaderStyle Width="20%" />
                                                        <ItemStyle Width="20%" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="Fecha Creación" 
                                                        SortExpression="Fch_Creacion,Hor_Creacion">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblITFecha" runat="server" 
                                                                Text='<%# LAP.TUUA.UTIL.Fecha.convertSQLToFecha(Convert.ToString(Eval("Fch_Creacion")),Convert.ToString(Eval("Hor_Creacion"))) %> '></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Nom_Grupo" HeaderText="Grupo" 
                                                        SortExpression="Nom_Grupo" />
                                                    <asp:BoundField DataField="Nom_Rol" HeaderText="Roles Asociados" 
                                                        SortExpression="Nom_Rol">
                                                        <HeaderStyle Width="15%" />
                                                        <ItemStyle Width="15%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Nom_Estado" HeaderText="Estado" 
                                                        SortExpression="Nom_Estado">
                                                        <ItemStyle Width="15%" />
                                                        <HeaderStyle Width="15%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Dias_xVencer" 
                                                        HeaderText="Dias por Vencer Cuenta Usuario" SortExpression="Dias_xVencer">
                                                        <ItemStyle Width="20%" />
                                                        <HeaderStyle Width="20%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Dias_xVenceClave" HeaderText="Dias por Vencer Clave" 
                                                        SortExpression="Dias_xVenceClave">
                                                        <ItemStyle Width="20%" />
                                                        <HeaderStyle Width="20%" />
                                                    </asp:BoundField>
                                                </Columns>
                                                <SelectedRowStyle CssClass="grillaFila" />
                                                <PagerStyle CssClass="grillaPaginacion" />
                                                <HeaderStyle CssClass="grillaCabecera" />
                                                <AlternatingRowStyle CssClass="grillaFila" />
                                            </asp:GridView>
                                            <br />
                                            <br />
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnConsultar" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                    &nbsp;<uc2:PiePagina ID="PiePagina1" runat="server" />
                    <br />                    
                </td>
            </tr>
        </table>
    </div>
    &nbsp; &nbsp;&nbsp;
    <asp:Label ID="txtOrdenacion" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="txtColumna" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="txtValorMaximoGrilla" runat="server" Visible="False"></asp:Label>
    </form>
</body>
</html>
