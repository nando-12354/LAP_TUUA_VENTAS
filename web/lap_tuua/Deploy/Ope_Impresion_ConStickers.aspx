<%@ page language="C#" autoeventwireup="true" inherits="Ope_Impresion_ConStickers, App_Web_nfcc8hqf" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
    
<%@ Register src="UserControl/PiePagina.ascx" tagname="PiePagina" tagprefix="uc2" %>
    
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Impresión</title>
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
    <link href="css/Style.css" rel="stylesheet" type="text/css" />    
    <!-- #INCLUDE file="javascript/Functions.js" -->
    <!-- #INCLUDE file="javascript/validarteclaF5.js" -->

    <script language="javascript" type="text/javascript">
//       function Imprimir(){
//           var dataVoucher = '<%=Session["dataVoucher"] %>';
//           var dataSticker = '<%=Session["dataSticker"] %>';
//           var retorno;
//           retorno = document.print.printer(dataVoucher.split("@"), dataSticker.split("@"));
//           window.document.location = 'Ope_PostImpresion_ConStickers.aspx?respuesta=' + retorno;
//        }
        
        function PostImpresion(id_mensaje, nroTicketsImpresos, puertos){
          var pagina_preImpresion = '<%=Request.QueryString["Pagina_PreImpresion"] %>';
          var operacion = '<%=Request.QueryString["operacion"] %>';
          window.document.location = 'Ope_PostImpresion_ConStickers.aspx?' + 
          'Pagina_PreImpresion=' + pagina_preImpresion + 
          '&id_mensaje=' + id_mensaje + 
          '&nroTicketsImpresos=' + nroTicketsImpresos + 
          '&operacion=' + operacion +
          '&puertos=' + puertos;
        }
        
        function PostImpresionNoImprimio(id_mensaje, nroTicketsImpresos){
          var pagina_preImpresion = '<%=Request.QueryString["Pagina_PreImpresion"] %>';
          var operacion = '<%=Request.QueryString["operacion"] %>';
          window.document.location = 'Ope_PostImpresion_ConStickers.aspx?' + 
          'Pagina_PreImpresion=' + pagina_preImpresion + 
          '&id_mensaje=' + id_mensaje + 
          '&nroTicketsImpresos=' + nroTicketsImpresos + 
          '&operacion=' + operacion;
        }
                
        function PostImpresionError(id_mensaje){
          var operacion = '<%=Request.QueryString["operacion"] %>';
          var pagina_preImpresion = '<%=Request.QueryString["Pagina_PreImpresion"] %>';
          window.document.location = 'Ope_PostImpresion_ConStickers.aspx?' + 
          'Pagina_PreImpresion=' + pagina_preImpresion + 
          '&id_mensaje=' + id_mensaje + 
          '&operacion=' + operacion;
        }
    </script>
    
</head>

<body>
    <form id="form1" runat="server">
        <div style="background-image: url(Imagenes/back.gif)">
        <table class="TamanoTabla" align="center">
            <tr>
                <td>
                    <table border="0" cellpadding="0" cellspacing="0" class="tamanocabecera">
                        <tr>
                            <td class="tamanocabecera">
                                <asp:Image ID="Image1" runat="server" CssClass="tamanocabecera" ImageUrl="~/Imagenes/head.gif" /></td>
                        </tr>
                        <tr>
                            <td class="tamanocabecera">
                            </td>
                        </tr>
                        <tr>
                            <td class="tamanocabecera" style="height: 28px">
                                <table class="menuDatos">
                                    <tr>
                                        <td>
                                        </td>
                                        <td style="width: 30px; text-align: left">
                                            <asp:Label ID="Label2" runat="server" Width="360px"></asp:Label></td>
                                        <td>
                                        </td>
                                        <td style="text-align: right">
                                            <asp:Label ID="lblFecha" runat="server" Text="Label" Width="358px"></asp:Label></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table> 
        <br />           
        <table align="center">    
            <tr>
            <td align="center">
                <applet code="com.hiper.lap.print.PrintWeb.class" 
                        name="print" 
                        codebase="Util/Print/"
                        archive="Print.jar"
                        height=450
                        width=450
                        align=middle> 
                    <param name="configImpSticker" value='<%=Request.QueryString["configImpSticker"] %>'/> 
                    <param name="configImpVoucher" value='<%=Request.QueryString["configImpVoucher"] %>'/>   
                    <param name="flagImpSticker" value='<%=Request.QueryString["flagImpSticker"] %>'/> 
                    <param name="flagImpVoucher" value='<%=Request.QueryString["flagImpVoucher"] %>'/> 
                    <param name="dataSticker" value='<%=Session["dataSticker"] %>'/>
                    <param name="dataVoucher" value='<%=Session["dataVoucher"] %>'/>                    
                </applet>                    
            </td>
            </tr>
            <tr>
            <td>
            <uc2:PiePagina ID="PiePagina2" runat="server" />
            </td>
            </tr>
        </table>
        </div>        
    </form>   
</body>
</html>
