<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Ope_VentaATM.aspx.cs" Inherits="Ope_RegistroVentaATM" %>

<%@ Register Src="UserControl/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="uc1" %>
<%@ Register Src="UserControl/PiePagina.ascx" TagName="PiePagina" TagPrefix="uc2" %>
<%@ Register Src="UserControl/OKMessageBox.ascx" TagName="OKMessageBox" TagPrefix="uc3" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="System.Web.Extensions" Namespace="System.Web.UI" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Registro de Venta ATM</title>
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
    <link href="css/Style.css" rel="stylesheet" type="text/css" />
    <!-- #INCLUDE file="javascript/KeyPress.js" -->
    <!-- #INCLUDE file="javascript/Functions.js" -->
    <!-- #INCLUDE file="javascript/validarteclaF5.js" -->
    <!-- #INCLUDE file="javascript/MantenerSesion.js" -->

    <script type="text/javascript" language="JavaScript">
        function CalcularVenta() {
            var frm = document.forms[0];
            for (i = 0; i < frm.elements.length; i++) {
                if (frm.elements[i].type == "checkbox") {
                    if (frm.elements[i].checked)
                        frm.elements[i].checked = false;
                    else frm.elements[i].checked = true;
                }
            }
        }

        function confirmacionLlamada() {
            if (confirm("Está seguro de realizar esta operacion.")) {
                accionSave = true;
                return true;
            }
            else {
                accionSave = false;
                return false;
            }
        }

        var accionSave = false;
        function beginRequest(sender, args) {
            if (!sender.get_isInAsyncPostBack()) {
                if (accionSave) {
                    document.getElementById('btnAceptar').disabled = true;
                    document.body.style.cursor = 'wait';

                }
            }
        }

        function endRequest(sender, args) {
            if (!sender.get_isInAsyncPostBack()) {
                if (accionSave) {
                    document.getElementById('btnAceptar').disabled = false;
                    document.body.style.cursor = 'default'

                    accionSave = false;
                }
            }
        }

        function ToUpperCase() {
            var numVuelo = document.getElementById("txtNumVuelo").value;
            document.getElementById("txtNumVuelo").value = numVuelo.toUpperCase();
        }    	
    </script>

    <style type="text/css">
        .style1
        {
            height: 11px;
        }
        .style2
        {
            height: 3px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="600">
        <Scripts>
            <asp:ScriptReference Path="~/javascript/jSManager.js" />
        </Scripts>
    </asp:ScriptManager>
    <div style="background-image: url(Imagenes/back.gif)">
        <table align="center" border="0" cellpadding="0" cellspacing="0" class="TamanoTabla">
            <tr>
                <td class="Espacio1FilaTabla" colspan="9" style="height: 11px">
                    <uc1:CabeceraMenu ID="CabeceraMenu2" runat="server" />
                </td>
            </tr>
            <tr class="formularioTitulo">
                <td align="right" class="style1" style="text-align: left">
                    <td align="right">
                        &nbsp;&nbsp;&nbsp; &nbsp; &nbsp;
                        <asp:Button ID="btnAceptar" runat="server" OnClick="btnAceptar_Click" CssClass="Boton"
                            OnClientClick="return confirmacionLlamada()" />
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
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <table style="width: 100%; left: 0px; position: relative; top: 0px;" class="SpacingGrid">
                                                <tr>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                    <td class="SpacingGrid">
                                                        <asp:Label ID="lblUsuario" runat="server" CssClass="TextoEtiqueta" Text="Usuario:"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                        <asp:DropDownList ID="ddlUsuario" runat="server" CssClass="combo">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td colspan="6">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                    <td style="height: 75px; text-align: left;" align="right" valign="bottom" colspan="3">
                                                        <fieldset>
                                                            <!-- CONTROL DE RANGO DE TICKET -->
                                                            <legend>
                                                                <asp:Label ID="lblRangoTicket" runat="server" CssClass="TextoFiltro">Tipo de 
                                                                Ticket</asp:Label>
                                                            </legend>
                                                            <table>
                                                                <tr>
                                                                    <td style="width: 100px">
                                                                        <asp:Panel ID="pnlTipvuelo" runat="server" Height="50px" Width="125px">
                                                                            <asp:RadioButton ID="rbNacional" runat="server" AutoPostBack="True" Checked="True"
                                                                                GroupName="TipVuelo" OnCheckedChanged="rbNacional_CheckedChanged" Text="Nacional" />
                                                                            <br />
                                                                            <asp:RadioButton ID="rbInter" runat="server" AutoPostBack="True" GroupName="TipVuelo"
                                                                                OnCheckedChanged="rbInter_CheckedChanged" Text="Internacional" />
                                                                        </asp:Panel>
                                                                    </td>
                                                                    <td style="width: 100px">
                                                                        <asp:Panel ID="pnlTiptrasbordo" runat="server" Height="50px" Width="125px">
                                                                            <asp:RadioButton ID="rbNormal" runat="server" AutoPostBack="True" Checked="True"
                                                                                GroupName="TipTrans" OnCheckedChanged="rbNormal_CheckedChanged" Text="Normal" />
                                                                            <br />
                                                                            <asp:RadioButton ID="rbTrans" runat="server" AutoPostBack="True" GroupName="TipTrans"
                                                                                OnCheckedChanged="rbTrans_CheckedChanged" Text="Transferencia" />
                                                                        </asp:Panel>
                                                                    </td>
                                                                    <td style="width: 100px">
                                                                        <asp:Panel ID="pnl1" runat="server" Height="50px" Width="125px">
                                                                            <asp:RadioButton ID="rbAdulto" runat="server" AutoPostBack="True" Checked="True"
                                                                                GroupName="TipPas" OnCheckedChanged="rbAdulto_CheckedChanged" Text="Adulto" />
                                                                            <br />
                                                                            <asp:RadioButton ID="rbInfante" runat="server" AutoPostBack="True" GroupName="TipPas"
                                                                                OnCheckedChanged="rbInfante_CheckedChanged" Text="Infante" />
                                                                        </asp:Panel>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </fieldset>
                                                    </td>
                                                    <td align="right" class="SpacingGrid" colspan="2" valign="bottom">
                                                        &nbsp;
                                                    </td>
                                                    <td style="height: 75px; text-align: left;" align="right" valign="bottom">
                                                        <fieldset style="height: 70px">
                                                            <legend>
                                                                <asp:Label ID="Label2" runat="server" CssClass="TextoFiltro"> Rango de Tickets</asp:Label>
                                                            </legend>
                                                            <table>
                                                                <tr>
                                                                    <td style="width: 100px">
                                                                        <asp:Label ID="lblRango" runat="server" CssClass="TextoEtiqueta" Text="Del Nro:"></asp:Label>
                                                                    </td>
                                                                    <td style="width: 100px">
                                                                        <asp:TextBox ID="txtNroIni" runat="server" CssClass="textbox" MaxLength="12"></asp:TextBox>
                                                                        <cc2:FilteredTextBoxExtender ID="txtNroIni_FilteredTextBoxExtender" runat="server"
                                                                            Enabled="True" FilterType="Numbers" TargetControlID="txtNroIni">
                                                                        </cc2:FilteredTextBoxExtender>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 100px">
                                                                        <asp:Label ID="lblAlNro" runat="server" CssClass="TextoEtiqueta" Text="Al Nro:"></asp:Label>
                                                                    </td>
                                                                    <td style="width: 100px">
                                                                        <asp:TextBox ID="txtNroFin" runat="server" CssClass="textbox" MaxLength="12"></asp:TextBox>
                                                                        <cc2:FilteredTextBoxExtender ID="txtNroFin_FilteredTextBoxExtender" runat="server"
                                                                            Enabled="True" FilterType="Numbers" TargetControlID="txtNroFin">
                                                                        </cc2:FilteredTextBoxExtender>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </fieldset>
                                                    </td>
                                                    <td align="right" style="height: 75px; text-align: left;" valign="bottom">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="SpacingGrid">
                                                        &nbsp;</td>
                                                    <td colspan="7" style="width: 331px; text-align: left; height: 13px;">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td class="SpacingGrid">
                                                        &nbsp;</td>
                                                    <td colspan="7" style="text-align: left; height: 13px;">
                                                        <table cellpadding="0" cellspacing="0" width="100%">
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label1" runat="server" CssClass="msgMensaje" 
                                                                        Text="DATOS DE TURNO"> </asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <hr class="EspacioLinea" color="#0099cc" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <table cellpadding="0" cellspacing="0" width="100%">
                                                                        <tr>
                                                                            <td width="10%">
                                                                                <asp:Label ID="lblDesTurno" runat="server" CssClass="msgMensaje" Text="Turno:"></asp:Label>
                                                                            </td>
                                                                            <td width="90%">
                                                                                <asp:Label ID="lblTurno" runat="server" Text=""></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="10%">
                                                                                <asp:Label ID="lblDesRegistro" runat="server" CssClass="msgMensaje" 
                                                                                    Text="Registros:"></asp:Label>
                                                                            </td>
                                                                            <td width="90%">
                                                                                <asp:Label ID="lblRegistro" runat="server" Text=""></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="SpacingGrid">
                                                    </td>
                                                    <td colspan="7" style="text-align: left; height: 13px;">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="SpacingGrid">
                                                        &nbsp;
                                                    </td>
                                                    <td colspan="7" style="width: 331px; text-align: left; height: 13px;">
                                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                            <ContentTemplate>
                                                                <asp:Button ID="btnConsultar" runat="server" CssClass="Boton" 
                                                                    OnClick="btnConsultar_Click" Style="cursor: hand;" Text="Consultar" />
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                        <asp:UpdateProgress ID="UpdateProgress3" runat="server" 
                                                            AssociatedUpdatePanelID="UpdatePanel2">
                                                            <progresstemplate>
                                                               
                                                                <div id="progressBackgroundFilter">
                                                                </div>
                                                            
                                                                <div ID="processMessage">
                                                                    &nbsp;&nbsp;&nbsp;Procesando...<br />
                                                                    <br />
                                                                    <img alt="Loading" src="Imagenes/ajax-loader.gif" />
                                                                </div>
                                                            </progresstemplate>
                                                        </asp:UpdateProgress>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="SpacingGrid">
                                                        &nbsp;</td>
                                                    <td colspan="7" style="width: 331px; text-align: left; height: 13px;">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td style="text-align: left;" colspan="7">
                                                        <div style="vertical-align: top; height: 250px; overflow: auto;">
                                                            <asp:Label ID="lblGrilla" runat="server" CssClass="msgMensaje"></asp:Label>
                                                            <asp:GridView ID="grvContingencia" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                                BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
                                                                CellPadding="3" CssClass="grilla" GridLines="Vertical" OnSorting="grvPuntoVenta_Sorting"
                                                                Width="97%">
                                                                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="">
                                                                        <ItemTemplate>
                                                                            <%# Container.DataItemIndex+1 %>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="Cod_Numero_Ticket" HeaderText="Nro. Ticket" SortExpression="Cod_Numero_Ticket" />
                                                                    <asp:BoundField DataField="Fch_Creacion" HeaderText="Fecha Preemisión" SortExpression="Fch_Creacion">
                                                                    </asp:BoundField>
                                                                    <asp:TemplateField HeaderText="">
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="ckbRegistrar" runat="server" />
                                                                        </ItemTemplate>
                                                                        <HeaderTemplate>
                                                                            <asp:ImageButton ID="ibtnCheckAll" runat="server" ImageUrl="~/Imagenes/check.gif"
                                                                                OnClientClick="JavaScript:CalcularVenta();" />
                                                                        </HeaderTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <SelectedRowStyle CssClass="grillaFila" />
                                                                <PagerStyle CssClass="grillaPaginacion" />
                                                                <HeaderStyle CssClass="grillaCabecera" />
                                                                <AlternatingRowStyle CssClass="grillaFila" />
                                                            </asp:GridView>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style5">
                                                        &nbsp;
                                                    </td>
                                                    <td colspan="7" style="width: 331px; text-align: left; height: 13px;">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style5">
                                                        &nbsp;
                                                    </td>
                                                    <td colspan="2" style="width: 331px; text-align: left; height: 13px;">
                                                        <asp:Label ID="lblFechaActual" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                                    </td>
                                                    <td colspan="2" style="width: 331px; text-align: left; height: 13px;">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="txtFecha" runat="server" CssClass="textboxFecha" Height="16px" MaxLength="10"
                                                                        onfocus="this.blur();" Width="88px"></asp:TextBox>
                                                                    <cc2:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" PopupButtonID="imgbtnCalendar"
                                                                        TargetControlID="txtFecha">
                                                                    </cc2:CalendarExtender>
                                                                </td>
                                                                <td>
                                                                    <asp:ImageButton ID="imgbtnCalendar" runat="server" Height="22px" ImageUrl="~/Imagenes/Calendar.bmp"
                                                                        Width="22px" />
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlHour" runat="server">
                                                                        <asp:ListItem>00</asp:ListItem>
                                                                        <asp:ListItem>01</asp:ListItem>
                                                                        <asp:ListItem>02</asp:ListItem>
                                                                        <asp:ListItem>03</asp:ListItem>
                                                                        <asp:ListItem>04</asp:ListItem>
                                                                        <asp:ListItem>05</asp:ListItem>
                                                                        <asp:ListItem>06</asp:ListItem>
                                                                        <asp:ListItem>07</asp:ListItem>
                                                                        <asp:ListItem>08</asp:ListItem>
                                                                        <asp:ListItem>09</asp:ListItem>
                                                                        <asp:ListItem>10</asp:ListItem>
                                                                        <asp:ListItem>11</asp:ListItem>
                                                                        <asp:ListItem>12</asp:ListItem>
                                                                        <asp:ListItem>13</asp:ListItem>
                                                                        <asp:ListItem>14</asp:ListItem>
                                                                        <asp:ListItem>15</asp:ListItem>
                                                                        <asp:ListItem>16</asp:ListItem>
                                                                        <asp:ListItem>17</asp:ListItem>
                                                                        <asp:ListItem>18</asp:ListItem>
                                                                        <asp:ListItem>19</asp:ListItem>
                                                                        <asp:ListItem>20</asp:ListItem>
                                                                        <asp:ListItem>21</asp:ListItem>
                                                                        <asp:ListItem>22</asp:ListItem>
                                                                        <asp:ListItem>23</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label3" runat="server" Text=":"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlMinute" runat="server">
                                                                        <asp:ListItem>00</asp:ListItem>
                                                                        <asp:ListItem>01</asp:ListItem>
                                                                        <asp:ListItem>02</asp:ListItem>
                                                                        <asp:ListItem>03</asp:ListItem>
                                                                        <asp:ListItem>04</asp:ListItem>
                                                                        <asp:ListItem>05</asp:ListItem>
                                                                        <asp:ListItem>06</asp:ListItem>
                                                                        <asp:ListItem>07</asp:ListItem>
                                                                        <asp:ListItem>08</asp:ListItem>
                                                                        <asp:ListItem>09</asp:ListItem>
                                                                        <asp:ListItem>10</asp:ListItem>
                                                                        <asp:ListItem>11</asp:ListItem>
                                                                        <asp:ListItem>12</asp:ListItem>
                                                                        <asp:ListItem>13</asp:ListItem>
                                                                        <asp:ListItem>14</asp:ListItem>
                                                                        <asp:ListItem>15</asp:ListItem>
                                                                        <asp:ListItem>16</asp:ListItem>
                                                                        <asp:ListItem>17</asp:ListItem>
                                                                        <asp:ListItem>18</asp:ListItem>
                                                                        <asp:ListItem>19</asp:ListItem>
                                                                        <asp:ListItem>20</asp:ListItem>
                                                                        <asp:ListItem>21</asp:ListItem>
                                                                        <asp:ListItem>22</asp:ListItem>
                                                                        <asp:ListItem>23</asp:ListItem>
                                                                        <asp:ListItem>24</asp:ListItem>
                                                                        <asp:ListItem>25</asp:ListItem>
                                                                        <asp:ListItem>26</asp:ListItem>
                                                                        <asp:ListItem>27</asp:ListItem>
                                                                        <asp:ListItem>28</asp:ListItem>
                                                                        <asp:ListItem>29</asp:ListItem>
                                                                        <asp:ListItem>30</asp:ListItem>
                                                                        <asp:ListItem>31</asp:ListItem>
                                                                        <asp:ListItem>32</asp:ListItem>
                                                                        <asp:ListItem>33</asp:ListItem>
                                                                        <asp:ListItem>34</asp:ListItem>
                                                                        <asp:ListItem>35</asp:ListItem>
                                                                        <asp:ListItem>36</asp:ListItem>
                                                                        <asp:ListItem>37</asp:ListItem>
                                                                        <asp:ListItem>38</asp:ListItem>
                                                                        <asp:ListItem>39</asp:ListItem>
                                                                        <asp:ListItem>40</asp:ListItem>
                                                                        <asp:ListItem>41</asp:ListItem>
                                                                        <asp:ListItem>42</asp:ListItem>
                                                                        <asp:ListItem>43</asp:ListItem>
                                                                        <asp:ListItem>44</asp:ListItem>
                                                                        <asp:ListItem>45</asp:ListItem>
                                                                        <asp:ListItem>46</asp:ListItem>
                                                                        <asp:ListItem>47</asp:ListItem>
                                                                        <asp:ListItem>48</asp:ListItem>
                                                                        <asp:ListItem>49</asp:ListItem>
                                                                        <asp:ListItem>50</asp:ListItem>
                                                                        <asp:ListItem>51</asp:ListItem>
                                                                        <asp:ListItem>52</asp:ListItem>
                                                                        <asp:ListItem>53</asp:ListItem>
                                                                        <asp:ListItem>54</asp:ListItem>
                                                                        <asp:ListItem>55</asp:ListItem>
                                                                        <asp:ListItem>56</asp:ListItem>
                                                                        <asp:ListItem>57</asp:ListItem>
                                                                        <asp:ListItem>58</asp:ListItem>
                                                                        <asp:ListItem>59</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="rfvFecha" runat="server" ControlToValidate="txtFecha"
                                                                        ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td colspan="3" style="width: 331px; text-align: left; height: 13px;">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style5" colspan="8">
                                                        <hr class="EspacioLinea" color="#0099cc" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 331px">
                                                        &nbsp;
                                                    </td>
                                                    <td colspan="4" style="width: 331px; text-align: left; height: 13px;">
                                                        <asp:Label ID="lblDatosVenta" runat="server" CssClass="TextoEtiqueta">Datos de Venta</asp:Label>
                                                    </td>
                                                    <td colspan="3" style="text-align: left; height: 13px;">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="alineaderecha">
                                                        &nbsp;
                                                    </td>
                                                    <td style="height: 28px; width: 331px; text-align: left;">
                                                        <asp:Label ID="lblCompania" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                                    </td>
                                                    <td class="alineaderecha" colspan="4">
                                                        <asp:DropDownList ID="ddlCompania" runat="server" CssClass="combo" Enabled="False">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="height: 28px; width: 331px; text-align: left;" colspan="2">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="alineaderecha">
                                                        &nbsp;
                                                    </td>
                                                    <td class="style2" style="text-align: left;">
                                                        <asp:Label ID="lblNumVuelo" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                                    </td>
                                                    <td class="style3" colspan="4">
                                                        <asp:TextBox ID="txtNumVuelo" runat="server" CssClass="textbox" MaxLength="9" Width="158px"></asp:TextBox>
                                                        <cc2:FilteredTextBoxExtender ID="txtNumVuelo_FilteredTextBoxExtender" runat="server"
                                                            Enabled="True" TargetControlID="txtNumVuelo" ValidChars="abcdefghijklmnopqrstuvwxyzQWERTYUIOPASDFGHJKLZXCVBNM0123456789">
                                                        </cc2:FilteredTextBoxExtender>
                                                    </td>
                                                    <td class="style2" style="text-align: left;" colspan="2">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td style="text-align: left;" colspan="4">
                                                        <asp:CheckBox ID="chkCierreTurno" runat="server" Text="Cierre de Turno" />
                                                    </td>
                                                    <td colspan="3" style="text-align: left;">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td colspan="4" style="text-align: left;">
                                                        &nbsp;</td>
                                                    <td colspan="3" style="text-align: left;">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td colspan="4" style="text-align: left;">
                                                        <asp:Label ID="lblMensajeError" runat="server" CssClass="mensaje" Width="427px"></asp:Label>
                                                    </td>
                                                    <td colspan="3" style="text-align: left;">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td colspan="4" style="text-align: left;">
                                                    </td>
                                                    <td colspan="3" style="text-align: left;">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style4">                                                        
                                                    </td>
                                                    <td colspan="4" align="center">
                                                        <uc3:OKMessageBox ID="omb" runat="server" />
                                                    </td>
                                                    <td colspan="3" style="text-align: left;">                                                        
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnAceptar" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>                                    
                                </td>
                                <td class="SpacingGrid" style="height: 115px; width: 2%;" valign="bottom">
                                </td>
                            </tr>
                            <tr>
                                <td colspan="8" align="center">
                                    <asp:Label ID="lblInfo" runat="server" CssClass="msgMensaje"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <uc2:PiePagina ID="PiePagina2" runat="server" />
                </td>
            </tr>
        </table>
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
        <br />
        <asp:Label ID="txtOrdenacion" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="txtColumna" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="txtPaginacion" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="txtValorMaximoGrilla" runat="server" Visible="False"></asp:Label>
        <br />
    </div>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    </form>
</body>
</html>
