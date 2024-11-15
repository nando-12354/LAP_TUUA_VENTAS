<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Ope_Impresion.aspx.cs" Inherits="Ope_Impresion" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Impresión</title>
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
    <link href="css/Style.css" rel="stylesheet" type="text/css" />    
    <!-- #INCLUDE file="javascript/Functions.js" -->
    <!-- #INCLUDE file="javascript/validarteclaF5.js" -->

   
    
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
            <td>
                <div id="contenidoImprimir">
                    <%=((string)Session["dataVoucher"]).Replace("@","<br>") %>;
                    <%=((string)Session["dataSticker"]).Replace("@","<br>")  %>;

                </div>
               <%-- <applet code="com.hiper.lap.print.PrintWeb.class" 
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
                </applet>    --%>                
            </td>
            </tr>
            </table>
        </div>
        
    </form>

 <script language="javascript" type="text/javascript">
     function Imprimir() {
        var dataVoucher = '<%=Session["dataVoucher"] %>';
        var dataSticker = '<%=Session["dataSticker"] %>';
        var pagina_preImpresion = '<%=Request.QueryString["Pagina_PreImpresion"] %>';
        var id_mensaje = '<%=Request.QueryString["id_mensaje"] %>';
        var retorno;
          
         retorno = PrintElem("contenidoImprimir"); 
         console.log(retorno);
         if (retorno === true) {
             window.document.location = 'Ope_PostImpresion.aspx?id_mensaje=' + id_mensaje + '&Pagina_PreImpresion=' + pagina_preImpresion;
         } 

        }

        function PrintElem(elem)
        {
            var mywindow = window.open('', 'PRINT', 'height=400,width=600');
        
            mywindow.document.write(document.getElementById(elem).innerHTML);
           
        
            mywindow.document.close(); // necessary for IE >= 10
            mywindow.focus(); // necessary for IE >= 10*/
        
            mywindow.print();
            mywindow.close();
        
            return true;
        }

        function PostImpresion(id_mensaje, puertos){
          var pagina_preImpresion = '<%=Request.QueryString["Pagina_PreImpresion"] %>';
          window.document.location = 'Ope_PostImpresion.aspx?Pagina_PreImpresion=' + pagina_preImpresion + '&id_mensaje=' + id_mensaje + '&puertos=' + puertos;
        }
        
        function PostImpresionNoImprimio(id_mensaje){
          var pagina_preImpresion = '<%=Request.QueryString["Pagina_PreImpresion"] %>';
          window.document.location = 'Ope_PostImpresion.aspx?Pagina_PreImpresion=' + pagina_preImpresion + '&id_mensaje=' + id_mensaje;
        }
        
        function PostImpresionError(id_mensaje){
          var pagina_preImpresion = '<%=Request.QueryString["Pagina_PreImpresion"] %>';
         window.document.location = 'Ope_PostImpresion.aspx?Pagina_PreImpresion=' + pagina_preImpresion + '&id_mensaje=' + id_mensaje;
     }


     Imprimir();
 </script>



</body>
</html>
