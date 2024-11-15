<%@ page language="C#" autoeventwireup="true" inherits="Inf_Session, App_Web_jlql8yfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Informacion Session</title>
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
    <link href="css/Style.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>
</head>
<body onload="window.focus()"  onblur="JavaScript:setInterval('window.focus()',250);" >
    <form id="form1" runat="server">
    <div style="text-align: center">
    

    <div style="text-align: center">

        <br />
        <asp:Label ID="Label1" runat="server" 
            Text="Periodo de Vigencia de su Contraseña" CssClass="Titulo"></asp:Label>
        <table class="style1">
            <tr>
                <td>
                    <asp:Label ID="lblPeriodo" runat="server" CssClass="Titulo"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblPeriodo0" runat="server" CssClass="Titulo"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnCerrar" runat="server" Text="Cerrar" CssClass="Boton" 
                        OnClientClick="JavaScript:window.close(); " TabIndex="1" />
                </td>
            </tr>
        </table>
    
    </div>

    
    </div>
    <p>
        &nbsp;</p>
    </form>
</body>
</html>
