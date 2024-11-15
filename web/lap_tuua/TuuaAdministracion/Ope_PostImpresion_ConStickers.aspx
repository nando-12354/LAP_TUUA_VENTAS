<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Ope_PostImpresion_ConStickers.aspx.cs" Inherits="Ope_PostImpresion_ConStickers" %>

<%@ Register Src="UserControl/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="uc1" %>
<%@ Register Src="UserControl/PiePagina.ascx" TagName="PiePagina" TagPrefix="uc2" %>
<%@ Register Src="UserControl/OKMessageBox.ascx" TagName="OKMessageBox" TagPrefix="uc3" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="System.Web.Extensions" Namespace="System.Web.UI" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
  <title>Impresión</title>
  <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
  <link href="css/Style.css" rel="stylesheet" type="text/css" />
  <!-- #INCLUDE file="javascript/KeyPress.js" -->
  <!-- #INCLUDE file="javascript/Functions.js" -->
  <!-- #INCLUDE file="javascript/validarteclaF5.js" -->
  <!-- #INCLUDE file="javascript/MantenerSesion.js" -->
</head>
<body>
  <form id="form1" runat="server">
  <div style="background-image: url(Imagenes/back.gif)">
    <table align="center" border="0" cellpadding="0" cellspacing="0" class="TamanoTabla">
      <tr>
        <td style="height: 11px">
          <uc1:CabeceraMenu ID="CabeceraMenu1" runat="server" />
        </td>
      </tr>
    </table>
    <table align="center" border="0" cellpadding="0" cellspacing="0" class="TamanoTabla">
      <tr class="formularioTitulo">
        <td align="left">
          <img alt="" src="Imagenes/flecha_back.png" onclick="Atras();" />
        </td>
      </tr>
      <tr>
        <td>
          <hr class="EspacioLinea" color="#0099cc" />
        </td>
      </tr>
    </table>
    <table align="center" border="0" cellpadding="0" cellspacing="0" class="TamanoTabla">
      <tr>
        <td>
          &nbsp;
          <table align="center" cellpadding="0" cellspacing="0"  class="TamanoTabla" border="0">
            <tr>
              <td style="width: 2%">
              </td>
              <td>
                <table style="width: 96%;" align="center" cellpadding="0" cellspacing="0" border="0">
                  <tr>
                    <td align="center">
                      <asp:Label ID="lblMensaje" runat="server" CssClass="msgMensaje"></asp:Label>
                    </td>
                  </tr>
                </table>
              </td>
              <td style="width: 2%">
              </td>
            </tr>
          </table>
          <uc2:PiePagina ID="PiePagina2" runat="server" />
        </td>
      </tr>
    </table>
  </div>

  <script type="text/javascript">
    function Atras() {
      window.location.href = './<%=Request.QueryString["Pagina_PreImpresion"] %>';
    }
    
  </script>

  </form>
</body>
</html>
