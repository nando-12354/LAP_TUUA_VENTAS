<%@ page language="C#" autoeventwireup="true" inherits="index, App_Web_tx1el90t" %>

<%@ Register Src="UserControl/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="uc1" %>
<%@ Register Src="UserControl/PiePagina.ascx" TagName="PiePagina" TagPrefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>LAP - Sistema TUUA</title>
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
    <link href="css/Style.css" rel="stylesheet" type="text/css" />
    <!-- #INCLUDE file="javascript/validarteclaF5.js" -->
    <!-- #INCLUDE file="javascript/MantenerSesion.js" -->
    <link href="../css/Style.css" rel="stylesheet" type="text/css" />
    <link href="../css/Style.css" rel="stylesheet" type="text/css" />
    <link href="../css/Style.css" rel="stylesheet" type="text/css" />
    
    <script language="javascript" type="text/javascript">
        function paginaVida()
        {
            if (document.getElementById('hvida').value == "yes") {
                window.open('Inf_Session.aspx', '_blank', 'top=' + screen.width / 2 + ',left=' + (screen.height - 60) / 2 + ',toolbar=no,status=no,location=no,menubar=no,resizable=no,width=200,height=120');
            }
        }      
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table class="TamanoTabla" align="center">
            <tr>
                <td colspan="3">
                    <uc1:CabeceraMenu ID="CabeceraMenu1" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:HiddenField ID="hvida" runat="server" 
                         />
                    <asp:Image ID="Image1" runat="server" CssClass="tamanoimagenbienvenida" ImageUrl="~/Imagenes/Bienvenida.jpg" Width="100%" />
                </td>
            </tr>
            <tr>
                <td colspan="3">                    
                    <uc2:PiePagina ID="PiePagina1" runat="server" />
                </td>
            </tr>
        </table>
    
    </div>
    </form>
    <script language="javascript" type="text/javascript">
        paginaVida();
    </script>    
</body>
</html>
