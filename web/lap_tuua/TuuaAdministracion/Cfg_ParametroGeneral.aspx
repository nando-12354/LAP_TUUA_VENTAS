<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Cfg_ParametroGeneral.aspx.cs"
    Inherits="Cfg_ParametroGeneral" %>

<%@ Register Src="UserControl/PiePagina.ascx" TagName="PiePagina" TagPrefix="uc2" %>
<%@ Register Src="UserControl/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="uc1" %>
<%@ Register Src="UserControl/OKMessageBox.ascx" TagName="OKMessageBox" TagPrefix="uc3" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Configuración de parametro general</title>
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
    <link href="css/Style.css" rel="stylesheet" type="text/css" />
    <!-- #INCLUDE file="javascript/KeyPress.js" -->
    <!-- #INCLUDE file="javascript/Functions.js" -->
    <!-- INCLUDE file="javascript/validarteclaF5.js" -->
    <!-- #INCLUDE file="javascript/MantenerSesion.js" -->
    <link href="../css/Style.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">


        function comprobar() {

            Total = 190;
            var ConError = "0";

            for (v = 0; v < Total; v++) {
                for (n = 0; n < Total; n++) {

                    if (document.getElementById("HiddenTipoGrilla" + n) != null) {
                        TipoMin = document.getElementById("HiddenTipoGrilla" + n).value + n;
                        if (document.getElementById("idControlTextGrilla" + TipoMin) != null) {
                            var TipoLimiteN = document.getElementById("HiddenCordGrilla" + TipoMin).value;
                            var TipoLimiteMin = TipoLimiteN.split("=");

                            if (TipoLimiteMin[0] == "LN") {
                                if (document.getElementById("HiddenTipoGrilla" + v) != null) {
                                    Tipo = document.getElementById("HiddenTipoGrilla" + v).value + v;
                                    if (document.getElementById("idControlTextGrilla" + Tipo) != null) {
                                        var TipoLimite = document.getElementById("HiddenCordGrilla" + Tipo).value;
                                        var TipoLimiteMax = TipoLimite.split("=");

                                        if (TipoLimiteMax[0] == "LM") {
                                            /*
                                            alert(document.getElementById("HiddenCordGrilla"+Tipo).value +" - "+ document.getElementById("idControlTextGrilla"+Tipo).value + "<" + document.getElementById("HiddenCordGrilla"+TipoMin).value +"="+document.getElementById("idControlTextGrilla"+TipoMin).value+"-------"+Tipo+"::::"+TipoMin+";;;;"+v);
                                            */
                                            if (parseFloat(document.getElementById("idControlTextGrilla" + Tipo).value) < parseFloat(document.getElementById("idControlTextGrilla" + TipoMin).value)) {

                                                document.getElementById("lblMensaje").innerHTML = "Limite Maximo de " + TipoLimiteMax[1] + " " + TipoLimiteMax[2] + "=" + parseFloat(document.getElementById("idControlTextGrilla" + Tipo).value) + "   es menor que   Limite Minimo   " + TipoLimiteMin[1] + " " + TipoLimiteMin[2] + "=" + parseFloat(document.getElementById("idControlTextGrilla" + TipoMin).value);
                                                return false;
                                                TotPagina = 0;
                                            }

                                            if (n == Total - 1) {
                                                v = Total;
                                            }
                                            v++;
                                        }
                                    }
                                }

                            }
                        }
                    }
                }
            }
            document.getElementById("lblMensaje").innerHTML = "";
            return true;
        }




        function metodoClick() {

            TotPagina = 244;

            CadenaFinal = "";
            SoloDatosGrilla = "";
            for (i = 0; i < TotPagina; i++) {


                if (document.getElementById('idControlLista' + i) != null) {
                    CadenaFinal = CadenaFinal + document.getElementById("HiddenLista" + i).value + '~' + document.getElementById('idControlLista' + i).options[document.getElementById('idControlLista' + i).selectedIndex].value + '|';
                }


                if (document.getElementById("idControlTextAnidado" + i) != null) {
                    CadenaFinal = CadenaFinal + document.getElementById("HiddenTextAnidado" + i).value + '~' + document.getElementById("idControlTextAnidado" + i).value + '|';
                }

                if (document.getElementById("idControlText" + i) != null) {
                    CadenaFinal = CadenaFinal + document.getElementById("HiddenText" + i).value + '~' + document.getElementById("idControlText" + i).value + '|';
                }


                if (document.getElementById("HiddenTipoGrilla" + i) != null) {
                    Tipo = document.getElementById("HiddenTipoGrilla" + i).value + i;
                    if (document.getElementById("idControlTextGrilla" + Tipo) != null) {
                        valor = document.getElementById("HiddenCordGrilla" + Tipo).value + '=' + document.getElementById("idControlTextGrilla" + Tipo).value;
                        SoloDatosGrilla = SoloDatosGrilla + '|' + valor;
                    }
                }



                if (document.getElementById("CheckboxAnidado" + i) != null) {
                    if (document.getElementById("CheckboxAnidado" + i).checked == true) {
                        valorCheck = 1;
                    }
                    else {
                        valorCheck = 0;
                    }
                    CadenaFinal = CadenaFinal + document.getElementById("HiddenCheckAnidado" + i).value + '~' + valorCheck + '|';
                }

                if (document.getElementById("Checkbox" + i) != null) {
                    if (document.getElementById("Checkbox" + i).checked == true) {
                        valorCheck = 1;
                    }
                    else {
                        valorCheck = 0;
                    }
                    CadenaFinal = CadenaFinal + document.getElementById("HiddenCheck" + i).value + '~' + valorCheck + '|';
                }
            }
            document.getElementById("hfCadenaTotal").value = CadenaFinal;
            document.getElementById("hfCadenaGrilla").value = SoloDatosGrilla;
        }


        function validar(e) {
            obj = e.srcElement || e.target;
            tecla_codigo = (document.all) ? e.keyCode : e.which;
            if (tecla_codigo == 8) return true;
            patron = /[\d.]/;
            tecla_valor = String.fromCharCode(tecla_codigo);
            control = (tecla_codigo == 46 && (/[.]/).test(obj.value)) ? false : true
            return patron.test(tecla_valor) && control;
        }

        function ValidarRehabilitacion(obj, strLimite) 
        {
            var iCantidadLimite = parseInt(strLimite);
            var ivalor = parseInt(obj.value);

            if (ivalor > iCantidadLimite) {
                alert("Ingrese un valor menor o igual: " + strLimite);
                obj.focus();
                obj.select();
                return false;
            }
            else {
                return true;
            }
        }
         function ValidarEstadistico(obj, strLimite) 
        {
            var iCantidadLimite = parseInt(strLimite);
            var ivalor = parseInt(obj.value);

            if (ivalor < iCantidadLimite) {
                alert("Ingrese un valor mayor o igual: " + strLimite);
                obj.focus();
                obj.select();
                return false;
            }
            else {
                return true;
            }
        }
        

function comprobarSiBisisesto(anio){

     if ( ( anio % 100 != 0) && ((anio % 4 == 0) || (anio % 400 == 0))) {
     return true;
     }  else {
     return false;
     }
 }




        function ValidarEnteros(e) {
            obj = e.srcElement || e.target;
            tecla_codigo = (document.all) ? e.keyCode : e.which;
            if (tecla_codigo == 8) return true;
            patron = /[\d]/;
            tecla_valor = String.fromCharCode(tecla_codigo);
            control = (tecla_codigo == 46) ? false : true
            return patron.test(tecla_valor) && control;
        }



        function Evaluar() {

            for (j = 0; j < 80; j++) { //begin for
                if (document.getElementById("Checkbox" + j) != null) { //begin if
                    //alert(document.getElementById("Checkbox"+j).checked);
                    if (document.getElementById("Checkbox" + j).checked == true) {
                        for (q = 0; q < 20; q++) {
                            if (document.getElementById("idControlTextAnidado" + q) != null) {
                                document.getElementById("idControlTextAnidado" + q).disabled = false;
                            }
                        }
                    }

                    if (document.getElementById("Checkbox" + j).checked == false) {
                        //alert(document.getElementById("Checkbox"+j).checked);
                        for (q = 0; q < 20; q++) {
                            if (document.getElementById("idControlTextAnidado" + q) != null) {
                                document.getElementById("idControlTextAnidado" + q).disabled = true;
                            }
                        }
                    }
                }
            }
        }   
    
    </script>

</head>
<body>
    <form id="form1" runat="server" name="fomulario" onsubmit="return comprobar()">
    <div>
    
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
										
        <table align="center" border="0" cellpadding="0" cellspacing="0" class="TamanoTabla">
            <tr>
                <!-- HEADER -->
                <td class="Espacio1FilaTabla" style="height: 11px">
                    <uc1:CabeceraMenu ID="CabeceraMenu1" runat="server" />
                </td>
            </tr>
            <tr class="formularioTitulo">
                <!-- WORK MENU -->
                <td align="right">
                    <asp:Button ID="btnGrabar" runat="server" CssClass="Boton" Width="100px" OnClick="btnGrabar_Click"
                        OnClientClick="javascript:metodoClick();" CausesValidation="False" />&nbsp;&nbsp;&nbsp;
                </td>
            </tr>
            <tr>
                <!-- SPACE -->
                <td>
                    <hr class="EspacioLinea" color="#0099cc" />
                </td>
            </tr>
            <tr>
                <!-- GRILLA -->
                <td>
                    <div>
                        <table cellpadding="0" cellspacing="0" class="EspacioSubTablaPrincipal">
                            <tr>
                                <td class="SpacingGrid">
                                </td>
                                <td class="CenterGrid">
                              
                                            <div style="overflow: auto; width: 100%; height: 420px; border-left: 0px gray solid;
                                                border-bottom: 0px gray solid; padding: 0px; margin: 0px">
                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                    <asp:Label ID="lblcontroles" runat="server" Text=""></asp:Label>
                                                </table>
                                            </div>
                                            <uc3:OKMessageBox ID="omb" runat="server" />
                                      
                                    <asp:Label ID="lblMensaje" runat="server" CssClass="mensaje" Width="686px" Height="16px"></asp:Label>
                                </td>
                                <td class="SpacingGrid" style="height: 115px">
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <!-- FOOTER -->
                <td class="Espacio1FilaTabla" style="height: 11px">
                    <uc2:PiePagina ID="PiePagina1" runat="server" />
                </td>
            </tr>
        </table>
        
         </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnGrabar" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
    		
		    <asp:UpdateProgress ID="UpdateProgress1" 
             AssociatedUpdatePanelID="UpdatePanel1" runat="server">
        <ProgressTemplate>
                <DIV id="processMessage">
                    &nbsp;&nbsp;&nbsp;Procesando...<br />
                            <br />
                    <img alt="Loading" src="Imagenes/ajax-loader.gif" />                                   

                </DIV>

        </ProgressTemplate>
        </asp:UpdateProgress>
        
        
        <asp:HiddenField ID="hfCadenaTotal" runat="server" />
        <asp:HiddenField ID="hfCadenaGrilla" runat="server" />
    </div>
    </form>
</body>
</html>
