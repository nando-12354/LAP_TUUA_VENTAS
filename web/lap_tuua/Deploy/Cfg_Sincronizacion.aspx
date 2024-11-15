<%@ Register src="UserControl/OKMessageBox.ascx" tagname="OKMessageBox" tagprefix="uc3" %>
<%--<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Cns_Sincronizacion_Prueba.aspx.cs" Inherits="Cns_Sincronizacion_Prueba" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
--%>

<%@ page language="C#" autoeventwireup="true" inherits="Cfg_Sincronizacion, App_Web_7ctknflu" responseencoding="utf-8" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Src="UserControl/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="uc1" %>
<%@ Register Src="UserControl/PiePagina.ascx" TagName="PiePagina" TagPrefix="uc2" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="UserControl/ConsDetTicket.ascx" TagName="Cfg_Sincronizacion1" TagPrefix="uc3" %>
<%@ Register Assembly="PaginGridView" Namespace="Pagin.Web.Control" TagPrefix="cc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LAP - Configuración Sincronización</title>
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
    <meta name="keypage" content="cns_boardingusados" />
    <!-- #INCLUDE file="javascript/ValidarFecha.js" -->
    <!-- #INCLUDE file="javascript/KeyPress.js" -->
    <!-- #INCLUDE file="javascript/validarteclaF5.js" -->
    <!-- #INCLUDE file="javascript/MantenerSesion.js" -->
    <link href="css/Style.css" rel="stylesheet" type="text/css" />
    <link href="../css/Style.css" rel="stylesheet" type="text/css" />
    
    <script language="JavaScript" type="text/javascript">
        
        function imgPrint_onclick() {

            var sDesde = document.getElementById("txtDesde").value;
            var sHasta = document.getElementById("txtHasta").value;
            var idHoraDesde = document.getElementById("txtHoraDesde").value;
            var idHoraHasta = document.getElementById("txtHoraHasta").value;
            var idCompania = document.getElementById("ddlCompania").value;
            var idTipoVuelo = document.getElementById("ddlTipoVuelo").value;
            var idNumVuelo = document.getElementById("txtNumVuelo").value;
            var idTipoPasajero = document.getElementById("ddlTipoPasajero").value;
            var idTipoDocumento = document.getElementById("ddlTipoDocumento").value;
            var idTipoTrasbordo = document.getElementById("ddlTipoTrasbordo").value;
            var idFechaVuelo = document.getElementById("txtFechaVuelo").value;
            var idEstado = document.getElementById("ddlEstado").value;

            //Descripciones
            var idDscD = (idTipoDocumento != "0") ? document.getElementById("ddlTipoDocumento").options[document.getElementById("ddlTipoDocumento").selectedIndex].text : "";
            var idDscC = (idCompania != "0") ? document.getElementById("ddlCompania").options[document.getElementById("ddlCompania").selectedIndex].text : "";
            var idDscE = (idEstado != "0") ? document.getElementById("ddlEstado").options[document.getElementById("ddlEstado").selectedIndex].text : "";
            var idDscP = (idTipoPasajero != "0") ? document.getElementById("ddlTipoPasajero").options[document.getElementById("ddlTipoPasajero").selectedIndex].text : "";
            var idDscV = (idTipoVuelo != "0") ? document.getElementById("ddlTipoVuelo").options[document.getElementById("ddlTipoVuelo").selectedIndex].text : "";
            var idDscT = (idTipoTrasbordo != "0") ? document.getElementById("ddlTipoTrasbordo").options[document.getElementById("ddlTipoTrasbordo").selectedIndex].text : "";

            var w = 900 + 32;
            var h = 500 + 96;
            var wleft = (screen.width - w) / 2;
            var wtop = (screen.height - h) / 2;

            var ventimp = window.open("ReporteCNS/rptTicketBoardingUsados.aspx" + "?sDesde=" + sDesde + "&sHasta=" + sHasta + "&idCompania=" + idCompania + "&idTipoVuelo=" + idTipoVuelo + "&idNumVuelo=" + idNumVuelo
                                            + "&idTipoPasajero=" + idTipoPasajero + "&idTipoDocumento=" + idTipoDocumento + "&idTipoTrasbordo=" + idTipoTrasbordo
                                            + "&idFechaVuelo=" + idFechaVuelo + "&idEstado=" + idEstado
                                            + "&idHoraDesde=" + idHoraDesde
                                            + "&idHoraHasta=" + idHoraHasta
                                            + "&idDscD=" + idDscD
                                            + "&idDscC=" + idDscC
                                            + "&idDscE=" + idDscE
                                            + "&idDscP=" + idDscP
                                            + "&idDscV=" + idDscV
                                            + "&idDscT=" + idDscT
                                            , "mywindow", "location=0,status=0,scrollbars=1,menubar=0,width=900,height=500,left=" + wleft + ",top=" + wtop);
            ventimp.moveTo(wleft, wtop);
            ventimp.focus();
        }

        function validarExcel() {
            var numRegistros = document.getElementById("lblTotalRows").value;
            var maxRegistros = document.getElementById("lblMaxRegistros").value;
            if (!isNaN(parseInt(numRegistros))) {
                if (parseInt(numRegistros) <= parseInt(maxRegistros)) {
                    return true;
                }
                else {
                    alert("La exportación a excel solo permite " + maxRegistros + " registros");
                    return false;
                }
            }
            else {
                alert("No existen registros para exportar \nSeleccione otros filtros");
                return false;
            }
        }
        
        function validarCampos() {
            //Clean screen
            document.getElementById('lblMensajeError').innerHTML = "";
            document.getElementById('lblMensajeErrorData').innerHTML = "";
            document.getElementById('lblTotal').innerHTML = "";

            document.getElementById("hlinkResumen").style.display = "none";
            cleanGrilla();

            if (isValidoRangoFecha(document.getElementById('txtDesde').value,
                                   document.getElementById('txtHoraDesde').value,
                                   document.getElementById('txtHasta').value,
                                   document.getElementById('txtHoraHasta').value
                                   )) {
                //document.getElementById('lblMensajeError').style.visibility = 'hidden';
                //document.getElementById('lblMensajeError').innerHTML = "";
                return true;
            } else {
                //document.getElementById('divData').style.visibility = 'hidden';
                //document.getElementById('lblMensajeError').style.visibility = 'visible';
                document.getElementById('lblMensajeError').innerHTML = "Error. Rango de Fechas ingresado es inválido";
                return false;
            }

        }
        
        function ControlarDropDownList(obj) {


            if (obj[obj.selectedIndex].value == "T") {
                 document.getElementById("ddlEstado").disabled = false;
            }
            if (obj[obj.selectedIndex].value == "0") {
                 document.getElementById("ddlEstado").disabled = false;
            }
        }

        function cleanGrilla() {
            if (document.getElementById("grvTicketUsados") != null) {
                document.getElementById("grvTicketUsados").style.display = "none";
            }
        } 
         function prueba(control) 
      {  // alert ("prueba");      
               
            if (control.checked)
            {
               document.getElementById('CheckPrueba1').checked=true;
               document.getElementById('CheckPrueba2').checked=true;
               document.getElementById('CheckPrueba3').checked=true;
               document.getElementById('CheckPrueba4').checked=true;
               document.getElementById('CheckPrueba5').checked=true;
               document.getElementById('CheckPrueba6').checked=true;
               document.getElementById('CheckPrueba7').checked=true;
               document.getElementById('CheckPrueba8').checked=true;
               document.getElementById('CheckPrueba9').checked=true;
               document.getElementById('CheckPrueba10').checked=true;
               document.getElementById('CheckPrueba11').checked=true;
               document.getElementById('CheckPrueba12').checked=true;
               document.getElementById('CheckPrueba13').checked=true;
               document.getElementById('CheckPrueba14').checked=true;
               document.getElementById('CheckPrueba15').checked=true;
               document.getElementById('CheckPrueba16').checked=true;
               document.getElementById('CheckPrueba17').checked=true;
               document.getElementById('CheckPrueba18').checked=true;
               document.getElementById('CheckPrueba19').checked=true;
               document.getElementById('CheckPrueba20').checked=true;
               document.getElementById('CheckPrueba21').checked=true;
               document.getElementById('CheckPrueba22').checked=true;
               document.getElementById('CheckPrueba23').checked=true;
               document.getElementById('CheckPrueba24').checked=true;
            }
            else
            { 
              document.getElementById('CheckPrueba1').checked=false;
               document.getElementById('CheckPrueba2').checked=false;
               document.getElementById('CheckPrueba3').checked=false;
               document.getElementById('CheckPrueba4').checked=false;
               document.getElementById('CheckPrueba5').checked=false;
               document.getElementById('CheckPrueba6').checked=false;
               document.getElementById('CheckPrueba7').checked=false;
               document.getElementById('CheckPrueba8').checked=false;
               document.getElementById('CheckPrueba9').checked=false;
               document.getElementById('CheckPrueba10').checked=false;
               document.getElementById('CheckPrueba11').checked=false;
               document.getElementById('CheckPrueba12').checked=false;
               document.getElementById('CheckPrueba13').checked=false;
               document.getElementById('CheckPrueba14').checked=false;
               document.getElementById('CheckPrueba15').checked=false;
               document.getElementById('CheckPrueba16').checked=false;
               document.getElementById('CheckPrueba17').checked=false;
               document.getElementById('CheckPrueba18').checked=false;
               document.getElementById('CheckPrueba19').checked=false;
               document.getElementById('CheckPrueba20').checked=false;
               document.getElementById('CheckPrueba21').checked=false;
               document.getElementById('CheckPrueba22').checked=false;
               document.getElementById('CheckPrueba23').checked=false;
               document.getElementById('CheckPrueba24').checked=false;
            
            }
           
         }
         
         function validar()
         {   
         //alert ("dd" + document.getElementById("ddlestado").value);
            if (document.getElementById("ddlestado").value == "CL")
             {
                        if(document.getElementById("CheckPrueba1").checked == true && 
                           document.getElementById("CheckPrueba2").checked == true &&
                           document.getElementById('CheckPrueba3').checked == true &&
                           document.getElementById('CheckPrueba4').checked == true &&
                           document.getElementById('CheckPrueba5').checked == true &&
                           document.getElementById('CheckPrueba6').checked == true &&
                           document.getElementById('CheckPrueba7').checked == true &&
                           document.getElementById('CheckPrueba8').checked == true &&
                           document.getElementById('CheckPrueba9').checked == true &&
                           document.getElementById('CheckPrueba10').checked == true &&
                           document.getElementById('CheckPrueba11').checked == true &&
                           document.getElementById('CheckPrueba12').checked == true&&
                           document.getElementById('CheckPrueba13').checked == true&&
                           document.getElementById('CheckPrueba14').checked == true&&
                           document.getElementById('CheckPrueba15').checked == true&&
                           document.getElementById('CheckPrueba16').checked == true&&
                           document.getElementById('CheckPrueba17').checked == true&&
                           document.getElementById('CheckPrueba18').checked == true&&
                           document.getElementById('CheckPrueba19').checked == true&&
                           document.getElementById('CheckPrueba20').checked == true&&
                           document.getElementById('CheckPrueba21').checked == true&&
                           document.getElementById('CheckPrueba22').checked == true&&
                           document.getElementById('CheckPrueba23').checked == true&&
                           document.getElementById('CheckPrueba24').checked == true)
                        {
                          document.getElementById("checkselec").checked = true;
                        }
                        else
                        {
                         document.getElementById("checkselec").checked = false;
                        }
              }
              else
              {
                           if(document.getElementById("CheckPrueba1").checked == true && 
                           document.getElementById("CheckPrueba2").checked == true &&
                           document.getElementById('CheckPrueba3').checked == true &&
                           document.getElementById('CheckPrueba4').checked == true &&
                           document.getElementById('CheckPrueba5').checked == true &&
                           document.getElementById('CheckPrueba6').checked == true &&
                           document.getElementById('CheckPrueba7').checked == true &&
                           document.getElementById('CheckPrueba8').checked == true &&
                           document.getElementById('CheckPrueba9').checked == true &&
                           document.getElementById('CheckPrueba10').checked == true &&
                           document.getElementById('CheckPrueba11').checked == true &&
                           document.getElementById('CheckPrueba12').checked == true&&
                           document.getElementById('CheckPrueba13').checked == true&&
                           document.getElementById('CheckPrueba14').checked == true&&
                           document.getElementById('CheckPrueba15').checked == true&&
                           document.getElementById('CheckPrueba16').checked == true&&
                           document.getElementById('CheckPrueba17').checked == true&&
                           document.getElementById('CheckPrueba18').checked == true&&
                           document.getElementById('CheckPrueba19').checked == true&&
                           document.getElementById('CheckPrueba20').checked == true&&
                           document.getElementById('CheckPrueba21').checked == true&&
                           document.getElementById('CheckPrueba22').checked == true&&
                           document.getElementById('CheckPrueba23').checked == true&&
                           document.getElementById('CheckPrueba24').checked == true)
                        {
                          document.getElementById("checkselec").checked = true;
                        }
                        else
                        {
                         document.getElementById("checkselec").checked = false;
                        }
              
              
              }
             }
         
         
        
      function selectall(control)
      {
       
           var activo = 'true';
          
           if(control.checked)
           {
                for(i = 1; i<=24 ; i++)
                {
                    var idItem = 'CheckPrueba'+ i;
                    //alert(idItem);
                    if(!document.getElementById(idItem).checked)
                    {
                        activo = 'false';
                    }                                      
                   
                }
                
                if(activo == 'true')
                {
                     document.getElementById('checkselec').checked=true;
                }               
                
          }
          else
          {          
            
            for(i = 1; i<=24 ; i++)
                {
                    var idItem = 'CheckPrueba'+ i;
                    //alert(idItem);
                    if(!document.getElementById(idItem).checked)
                    {
                        activo = 'false';
                    }                                      
                   
                }
               if(activo == 'false')
                {    
                     document.getElementById('checkselec').checked=false;
                }    
            
          
          }
       }

      
                       

    </script>

    <style type="text/css">
        .style1
        {
            height: 59px;
        }
        .style2
        {
            width: 135px;
            height: 32px;
        }
        .style3
        {
            width: 187px;
            height: 32px;
        }
        .style4
        {
            width: 134px;
            height: 32px;
        }
        .style5
        {
            height: 32px;
        }
        .style6
        {
            font-size: x-small;
        }
        .style7
        {
            font-weight: normal;
        }
        .style8
        {
            font-size: small;
        }
        .style9
        {
            width: 100%;
            height: 30px;
        }
        .style10
        {
            width: 1168px;
        }
        </style>

    </head>
<body >
    <form id="form1" runat="server" >
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="360">
        </asp:ScriptManager>
        <table cellpadding="0" cellspacing="0" align="center" class="TamanoTabla">
            <tr>
                <!-- HEADER -->
                <td align="center" class="style10">
                    <uc1:CabeceraMenu ID="CabeceraMenu1" runat="server" />                    
                </td>
            </tr>
            <tr class="formularioTitulo2">
                <td class="style10">

                    <br />
                    <!-- FILTER ZONE -->
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <table cellpadding="0" cellspacing="0" 
    class="EspacioSubTablaPrincipal">
                                <tr>
                                    <td align="left" class="style9">
                                        &nbsp;<asp:Label ID="Label20" runat="server" 
                            Text="   SINCRONIZACIÓN:" meta:resourcekey="Label20Resource1" 
                                    style="font-weight: 700" BorderColor="Navy"></asp:Label>
                                        &nbsp;
                                        <br />
                                    </td>
                                    <td align="left" class="style9">
                                        </td>
                                    <td align="left" class="style9">
                                        </td>
                                </tr>
                                <tr>
                                    <td class="style1" colspan="4">
                                        <table style="width:100%;">
                                            <tr>
                                                <td class="style2">
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Tabla de Sincronización:</td>
                                                <td class="style3">
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:DropDownList ID="ddlestado" runat="server" Height="27px" Width="107px" 
                                                                onselectedindexchanged="ddlestado_SelectedIndexChanged" AutoPostBack="True" 
                                                                >
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="style4">
                                                    <asp:CheckBox ID="checkselec" runat="server" Text="Seleccionar Todos" 
                                         AutoPostBack="True"  onclick="JavaScript:prueba(this);" />
                                                </td>
                                                <td class="style5">
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                     <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                                       <ContentTemplate>
                                                           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                       <asp:Button ID="btnAceptar" runat="server" CssClass="Button_BMATIC" 
                                                        meta:resourcekey="btnAceptarResource1" onclick="btnAceptar_Click1" 
                                                        OnClientClick="MostrarMarcados(this.form)" Text="Aceptar" 
                                                        ValidationGroup="grupo" />
                                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                                        <asp:Button ID="btnCancelar" runat="server" CssClass="Button_BMATIC" 
                                                            meta:resourcekey="btnCancelarResource1" onclick="btnCancelar_Click" 
                                                        Text="Cancelar" />
                                                        </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                                                                
                                                    
                                                    
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="CellHeader" colspan="4">
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Seleccionar las Horas:</td>
                                </tr>
                                <tr>
                                    <td class="CellHeader" colspan="4">
                                        <br />
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span 
                            class="style6">&nbsp;&nbsp; <span class="style7"><b>&nbsp;&nbsp;&nbsp;</b></span></span><span 
                            class="style7"><b><span 
                            class="style8">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span>
                                        <span 
                            class="style6"><span class="style8">
                                        <asp:CheckBox ID="CheckPrueba1" runat="server" Text="00:00" Font-Bold="False" onclick="JavaScript:selectall(this);"
                            style="font-size: small"  />
                                        &nbsp;&nbsp; </span></span></b></span>
                                        <span 
                            class="style6"><span 
                            class="style8">
                          
                                        <asp:CheckBox ID="CheckPrueba2" runat="server" Text="01:00" onclick="JavaScript:selectall(this);" />
                                        &nbsp;&nbsp;
                                        <asp:CheckBox ID="CheckPrueba3" runat="server" Text="02:00" onclick="JavaScript:selectall(this);"/>
                                        &nbsp;&nbsp;
                                        <asp:CheckBox ID="CheckPrueba4" runat="server" Text="03:00" onclick="JavaScript:selectall(this);"/>
                                        &nbsp;
                                        <asp:CheckBox ID="CheckPrueba5" runat="server" Text="04:00" onclick="JavaScript:selectall(this);"/>
                                        &nbsp;
                                        <asp:CheckBox ID="CheckPrueba6" runat="server" Text="05:00" onclick="JavaScript:selectall(this);"/>
                                        &nbsp;
                                        <asp:CheckBox ID="CheckPrueba7" runat="server" Text="06:00" onclick="JavaScript:selectall(this);"/>
                                        &nbsp;
                                        <asp:CheckBox ID="CheckPrueba8" runat="server" Text="07:00" onclick="JavaScript:selectall(this);"/>
                                        &nbsp;
                                        <asp:CheckBox ID="CheckPrueba9" runat="server" Text="08:00" onclick="JavaScript:selectall(this);"/>
                                        &nbsp;
                                        <asp:CheckBox ID="CheckPrueba10" runat="server" Text="09:00" onclick="JavaScript:selectall(this);"/>
                                        &nbsp;
                                        <asp:CheckBox ID="CheckPrueba11" runat="server" Text="10:00" onclick="JavaScript:selectall(this);"/>
                                        &nbsp;
                                        <asp:CheckBox ID="CheckPrueba12" runat="server" Text="11:00" onclick="JavaScript:selectall(this);"/>
                                        <br />
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox 
                            ID="CheckPrueba13" runat="server" 
                            Text="12:00" onclick="JavaScript:selectall(this);"/>
                                        &nbsp;&nbsp;
                                        <asp:CheckBox ID="CheckPrueba14" runat="server" Text="13:00" onclick="JavaScript:selectall(this);"/>
                                        &nbsp;&nbsp;
                                        <asp:CheckBox ID="CheckPrueba15" runat="server" Text="14:00" onclick="JavaScript:selectall(this);"/>
                                        &nbsp;&nbsp;
                                        <asp:CheckBox ID="CheckPrueba16" runat="server" Text="15:00" onclick="JavaScript:selectall(this);"/>
                                        &nbsp;
                                        <asp:CheckBox ID="CheckPrueba17" runat="server" Text="16:00" onclick="JavaScript:selectall(this);"/>
                                        &nbsp;
                                        <asp:CheckBox ID="CheckPrueba18" runat="server" Text="17:00" onclick="JavaScript:selectall(this);"/>
                                        &nbsp;
                                        <asp:CheckBox ID="CheckPrueba19" runat="server" Text="18:00" onclick="JavaScript:selectall(this);"/>
                                        &nbsp;
                                        <asp:CheckBox ID="CheckPrueba20" runat="server" Text="19:00" onclick="JavaScript:selectall(this);"/>
                                        &nbsp;
                                        <asp:CheckBox ID="CheckPrueba21" runat="server" Text="20:00" onclick="JavaScript:selectall(this);"/>
                                        &nbsp;
                                        <asp:CheckBox ID="CheckPrueba22" runat="server" Text="21:00" onclick="JavaScript:selectall(this);"/>
                                        &nbsp;
                                        <asp:CheckBox ID="CheckPrueba23" runat="server" Text="22:00" onclick="JavaScript:selectall(this);"/>
                                        &nbsp;
                                        <asp:CheckBox ID="CheckPrueba24" runat="server" Text="23:00" onclick="JavaScript:selectall(this);"/>
                                        </span></span><span class="style8">
                                        <br />
                                        <br />
                                        </span>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="CellControl" width="10%">
                                        <asp:Label ID="lblestado" runat="server" Enabled="False" Visible="False"></asp:Label>
                                    </td>
                                    <td class="CellControl" width="10%">
                                        &nbsp;</td>
                                    <td class="CellControl" width="10%">
                                        &nbsp;</td>
                                    <td width="90%">
                                        &nbsp;</td>
                                </tr>
                                <tr class="txtAyuda" valign="top">
                                    <td colspan="4">
                                        <asp:Label ID="lblcodrelativo" runat="server" Enabled="False" Visible="False"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <table border="0" width="100%">
                                            <tr>
                                            
                                                <td align="right">
                                                    <uc3:OKMessageBox ID="omb" runat="server" />
                                                    <asp:Label ID="lblMensajeError" runat="server" CssClass="mensaje" Width="502px"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr class="txtAyuda">
                                    <td colspan="4">
                                        <asp:Label ID="Label30" runat="server" 
                            Text="Son las horas que estará activa la sincronización." 
                            meta:resourcekey="Label30Resource1"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <!-- SPACE -->
                <td class="style10">
                    <hr color="#0099cc" style="width: 100%; height: 0px" />
                </td>
            </tr>
            <tr>
                <td class="style10">
                
                        <tr>
                            <td class="style10">
                                    <uc3:OKMessageBox ID="OKMessageBox1" runat="server" />
                             
                            
                                <div id="divData" class="divSizeCustom">
                                    <asp:UpdateProgress ID="UpdateProgress2" AssociatedUpdatePanelID="UpdatePanel3" runat="server"
                                        DisplayAfter="10">
                                        <ProgressTemplate>
                                        <div id="progressBackgroundFilter">
                                            </div>
                                            <div id="processMessage">
                                                &nbsp;&nbsp;&nbsp;Procesando...<br />
                                                <br />
                                                <img alt="Loading" src="Imagenes/ajax-loader.gif" />
                                            </div>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </div>
                                                      
                            
                            </td>
                         
                          
                        </tr>
                     
                        
                    <uc2:PiePagina ID="PiePagina1" runat="server" />
                    <br />
                </td>
            </tr>
        </table>
    </div>

    </form>
</body>
</html>
