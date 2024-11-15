<%@ page language="C#" autoeventwireup="true" inherits="PaginaError, App_Web_fmojukce" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

</script>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>LAP - Pagina Error</title>
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
    <link href="../css/Style.css" rel="stylesheet" type="text/css" />
    <!-- #INCLUDE file="../javascript/validarteclaF5.js" -->
    <!-- #INCLUDE file="../javascript/Functions.js" -->
    <link href="../css/Style.css" rel="stylesheet" type="text/css" />

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table align="center" border="0" cellpadding="0" cellspacing="0" class="TamanoTabla">
            <tr>
                <td class="Espacio1FilaTabla" style="height: 11px">
                    &nbsp;</td>
            </tr>
            <tr>
                <td  style="height: 11px; text-align: right">
                    <table cellpadding="0" cellspacing="0" class="EspacioSubTablaPrincipal">
                            <tr>
                                <td class="SpacingGrid" style="height: 115px">
                                </td>
                                <td class="CenterGrid" style="height: 115px; text-align: center;">
                    <asp:Label ID="lblMensajeError" runat="server" CssClass="mensaje"></asp:Label>&nbsp;</td>
                                <td class="SpacingGrid" style="height: 115px">
                                </td>
                            </tr>
                        </table>
                    </td>
            </tr>
            <tr class="header">
                <td class="header">
                    &nbsp;</td>
            </tr>
            <tr><td align="right" style="text-align: left">&nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <div class="EspacioSubTablaPrincipal">&nbsp;</div>
                    </td>
            </tr>
            <tr>
                <td style="text-align: center">
                    &nbsp;</td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
