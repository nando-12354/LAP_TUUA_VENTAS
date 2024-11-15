<%@ control language="C#" autoeventwireup="true" inherits="UserControl_ResumenTicketBPUsados, App_Web_i29mqlr7" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Button ID="BtnInvisible" Text="Invisible" runat="server" Style="display: none" />
<cc1:ModalPopupExtender ID="mpext" runat="server" BackgroundCssClass="modalBackground"
    TargetControlID="BtnInvisible" PopupControlID="pnlPopup" OkControlID="btnCerrarPopup"
    OnOkScript="onOk()" DropShadow="true">
</cc1:ModalPopupExtender>
<asp:Panel ID="pnlPopup" CssClass="modalDetalleTurno" Style="overflow: auto; display: none"
    runat="server" Width="700px" Height="460px">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <br />
            <table border="0" cellpadding="0" cellspacing="0" class="TamanoTabla" align="center">
                <tr>
                    <td align="center">
                        <asp:Label ID="lblResumen" runat="server" CssClass="titulosecundario"></asp:Label>
                    </td>
                    
                </tr>
                <tr>
                    <td>
                        <hr color="#0099cc" style="width: 95%; height: 0px" />
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
<%--                        <CR:CrystalReportViewer ID="crvResTipoDocumento" runat="server" AutoDataBind="true"
                            DisplayGroupTree="False" EnableDatabaseLogonPrompt="False" EnableParameterPrompt="False"
                            HasCrystalLogo="False" PrintMode="ActiveX" EnableTheming="False" HasGotoPageButton="False"
                            HasSearchButton="False" HasToggleGroupTreeButton="False" HasViewList="False"
                            SeparatePages="False" HasRefreshButton="False" HasPrintButton="False" HasPageNavigationButtons="False"
                            Width="100%" Height="100%" HasExportButton="False" HasDrillUpButton="False" HasZoomFactorList="False"
                            DisplayToolbar="False"/>--%>
                            
                        <CR:CrystalReportViewer ID="crvResTipoDocumento" runat="server" AutoDataBind="true" 
                        DisplayGroupTree="False" EnableDatabaseLogonPrompt="False" EnableParameterPrompt="False"
                        Width="100%" Height="100%" HasExportButton="False" HasDrillUpButton="False" HasZoomFactorList="False"
                        DisplayToolbar="False"/>
                        
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <table border="0" cellpadding="0" cellspacing="0" class="TamanoTabla" align="center">
        <tr>
            <td align="center">
                <asp:Button ID="btnCerrarPopup" runat="server" Text="Cerrar" CssClass="Boton" />
                <br>&nbsp;
            </td>
        </tr>
    </table>
</asp:Panel>

<script language="javascript" type="text/javascript">
    function onOk() {
    }
</script>

