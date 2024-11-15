<%@ page language="C#" autoeventwireup="true" inherits="_Default, App_Web_tx1el90t" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>LAP - Sistema TUUA</title>
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
    <script type="text/javascript"> 
        function OpenHomePage() { 
 //           window.open('login.aspx','_blank', 'top=0,left=0,toolbar=no,status=yes,location=no,menubar=no,resizable=no,width=' + screen.width + ',height=' + (screen.height-60));
 //           window.close(); 
	    var nuevaVentana = window.open('login.aspx','_blank','titlebar=0,scrollbars=1,status=1,toolbar=0,location=0,menubar=0,directories=0,resizable=1'); 
	    ventanaActual = window.self; 
	    ventanaActual.opener = window.self; 
	    ventanaActual.close(); 
		
	    nuevaVentana.moveTo(0,0);
	    nuevaVentana.resizeTo(screen.availWidth, screen.availHeight);
	    nuevaVentana.focus();     

        }  
     </script> 
</head>
<body onload="OpenHomePage()">
    

    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
</body>
</html>

