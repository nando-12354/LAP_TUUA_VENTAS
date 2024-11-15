<%@ page language="C#" autoeventwireup="true" inherits="Mnt_ModalidadVentaDetalle, App_Web_ehzg6gwo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Atributos Generales </title>
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
    <!-- #INCLUDE file="javascript/KeyPress.js" -->
     <!-- #INCLUDE file="javascript/Functions.js" -->
     <!-- #INCLUDE file="javascript/validarteclaF5.js" -->
    <meta http-equiv="pragma" content="no-cache">
    <base target="_self" />
    <link href="css/Style.css" rel="stylesheet" type="text/css" />
    
    <script language="javascript" type="text/javascript">
    function CloseFormOK()
    {
        window.returnValue = true;
        //window.opener.location.reload();
        self.close();
    }
     function CloseFormCancel()
    {
        window.returnValue = false;
        self.close();
    }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    
    <asp:Panel ID="pnlDetalle" runat="server" >
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        </asp:UpdatePanel>
        <div style="text-align: right">
                    <asp:Button ID="btnAceptar" runat="server" CssClass="Boton" 
                        onclick="btnAceptar_Click" Text="Aceptar" />
            </div>
    </asp:Panel>
    
    
    
    </form>
</body>
</html>
