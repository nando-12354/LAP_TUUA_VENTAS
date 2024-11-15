<%@ page language="C#" autoeventwireup="true" inherits="ExpCodeBar_rptCodeBarDestrabe, App_Web_wabwkiii" %>

<%@ Register assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
    <link href="../css/Style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <CR:CrystalReportViewer ID="CrystalReportViewer2" runat="server" 
            AutoDataBind="true" />
    
    </div>
    
    <asp:Label ID="lblMensaje" runat="server" CssClass="msgMensaje"></asp:Label>
    </form>
</body>
</html>
