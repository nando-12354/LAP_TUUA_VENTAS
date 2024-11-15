<%@ page language="C#" autoeventwireup="true" inherits="Cns_ResumenDiarioTurno, App_Web_tx1el90t" %>

<%@ Register Src="UserControl/PiePagina.ascx" TagName="PiePagina" TagPrefix="uc2" %>
<%@ Register Src="UserControl/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="uc1" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>LAP - Modulo consulta Resumen diario de turnos</title>
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
    <!-- #INCLUDE file="javascript/ValidarFecha.js" -->
    <!-- #INCLUDE file="javascript/KeyPress.js" -->
    <!-- #INCLUDE file="javascript/validarteclaF5.js" -->
    <!-- #INCLUDE file="javascript/MantenerSesion.js" -->
    <link href="css/Style.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        function validar() {

            if (document.getElementById("rbtnNumTurno").checked == true) {

                document.getElementById("txtNumTurno").disabled = false;
                document.getElementById("txtTurnoDesde").disabled = true;
                document.getElementById("txtTurnoHasta").disabled = true;                

                document.getElementById("txtNumTurno").style.backgroundColor = '#FFFFFF';
                document.getElementById("txtTurnoDesde").style.backgroundColor = '#CCCCCC';
                document.getElementById("txtTurnoHasta").style.backgroundColor = '#CCCCCC';    

                document.getElementById("txtTurnoDesde").value = '';
                document.getElementById("txtTurnoHasta").value = '';
                
                document.getElementById("lblNumTicket").disabled = false;
                document.getElementById("lblTicketDesde").disabled = true;
                document.getElementById("lblTicketHasta").disabled = true;

            }

            if (document.getElementById("rbtnRangoTurno").checked == true) {

                document.getElementById("txtNumTurno").disabled = true;
                document.getElementById("txtTurnoDesde").disabled = false;
                document.getElementById("txtTurnoHasta").disabled = false;
                
                document.getElementById("txtNumTurno").style.backgroundColor = '#CCCCCC';
                document.getElementById("txtTurnoDesde").style.backgroundColor = '#FFFFFF';
                document.getElementById("txtTurnoHasta").style.backgroundColor = '#FFFFFF';

                document.getElementById("txtNumTurno").value = '';
                
                document.getElementById("lblNumTicket").disabled = true;
                document.getElementById("lblTicketDesde").disabled = false;
                document.getElementById("lblTicketHasta").disabled = false;
                
            }
          

        }

        function imgPrint_onclick() {
            var NumTicket = document.getElementById("txtNumTurno").value;

            var NumTicketDesde = document.getElementById("txtTurnoDesde").value;
            var NumTicketHasta = document.getElementById("txtTurnoHasta").value;

           

            if (document.getElementById("rbtnNumTurno").checked == true) {
                if (NumTicket == "") {
                    alert("Ingrese número de ticket a imprimir");
                }
                else {
                    var ventimp = window.open("ReporteCNS/rptDetalleTicket.aspx" + "?idNumTicket=" + NumTicket, "mywindow", "location=0,status=0,scrollbars=0,menubar=0,width=900,height=800");
                    ventimp.focus();
                }
            }


            if (document.getElementById("rbtnRangoTurno").checked == true) {
                if ((NumTicketDesde != "" && NumTicketHasta != "") || (NumTicketDesde == "" && NumTicketHasta != "") || (NumTicketDesde != "" && NumTicketHasta == "")) {
                    var ventimp = window.open("ReporteCNS/rptDetalleTicket.aspx" + "?idNumTicketDesde=" + NumTicketDesde + "&idNumTicketHasta=" + NumTicketHasta, "mywindow", "location=0,status=0,scrollbars=0,menubar=0,width=900,height=800");
                    ventimp.focus();
                }
                else { alert("Ingrese los números de ticket a imprimir"); }
            }


        }



    </script>


    <link href="../css/Style.css" rel="stylesheet" type="text/css" />

</head>
<body>

    <form id="form1" runat="server">
    <div>
        <table align="center" border="0" cellpadding="0" cellspacing="0" class="TamanoTabla">
            <tr>
                <td class="Espacio1FilaTabla" colspan="3" >
                    <uc1:CabeceraMenu ID="CabeceraMenu1" runat="server" />
                </td>
            </tr>
            <tr class="formularioTitulo">
                <td width="8%" class="style5">
                    
                    <asp:RadioButton ID="rbtnNumTurno" runat="server" GroupName="TipoConsulta" 
                        onClick="validar();" CssClass="TextoFiltro" 
                        />
                    </td>
                <td align="left" class="style1">
                    
                        <asp:Label ID="lblNumTurno" runat="server" 
    CssClass="TextoFiltro" Width="85px" ></asp:Label>
                        <asp:TextBox ID="txtNumTurno" runat="server" CssClass="textbox" MaxLength="10" 
                            onkeypress="JavaScript: Tecla('Integer');"></asp:TextBox>
                    
                </td>
                <td align="right" class="style1">
                    
                    <img src="Imagenes/print.jpg" class="BotonImprimir" id="imgPrint" language="javascript" onclick="return imgPrint_onclick()" width="0"/>&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnConsultar" runat="server"  OnClick="btnConsultar_Click" CssClass="Boton" />
                    
                </td>
            </tr>
            <tr class="formularioTitulo">
                <td width="8%" class="style5">

                    
                    <asp:RadioButton ID="rbtnRangoTurnos" runat="server" GroupName="TipoConsulta" 
                        onClick="validar();" CssClass="TextoFiltro" />
                    </td>
                <td align="left" class="style1">

                    
                        <asp:Label ID="lblTurnoDesde" runat="server" 
    CssClass="TextoFiltro" Width="85px" ></asp:Label>
                        <asp:TextBox ID="txtTurnoDesde" runat="server" CssClass="textbox" 
                            MaxLength="10"></asp:TextBox>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="lblTurnoHasta" runat="server" CssClass="TextoFiltro"></asp:Label>
                        &nbsp;
                        <asp:TextBox ID="txtTurnoHasta" runat="server" CssClass="textbox" 
                            MaxLength="10"></asp:TextBox>
                    

                </td>
                <td align="left" class="style1">

                    
                        &nbsp;</td>
            </tr>
            <tr>
                <td colspan="3">
                    <hr class="EspacioLinea" color="#0099cc" />
                    
                </td>
            </tr>
            <tr>
                <td colspan="3" >
                    <div>
                        <table cellpadding="0" cellspacing="0" class="EspacioSubTablaPrincipal">
                            <tr>
                                <td class="SpacingGrid" style="height: 20%">
                                </td>
                                <td class="CenterGrid">
                                 <div class="divSizeOneRowFilter">
                                <asp:ScriptManager ID="ScriptManager1" runat="server"> </asp:ScriptManager>
                                
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                        <asp:Label ID="lblMensajeError" runat="server" CssClass="mensaje"  Visible="False"></asp:Label>
                                        <asp:Label ID="lblMensajeErrorData" runat="server" CssClass="msgMensaje"  Visible="False"></asp:Label>    
                                                
                                      
                                            
                                        </ContentTemplate>
                                        <triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnConsultar" EventName="Click" />
                                        </triggers>
                                    </asp:UpdatePanel>
                                    </div>
                                </td>
                                <td class="SpacingGrid">
                                </td>
                            </tr>
                        </table>
                    </div>
                    <uc2:PiePagina ID="PiePagina1" runat="server" />
                </td>
            </tr>
        </table>
    
    </div>        
        <asp:Label ID="txtOrdenacion" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="txtColumna" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="txtValorMaximoGrilla" runat="server" Visible="False"></asp:Label>
                
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
        
    </form>
</body>
</html>