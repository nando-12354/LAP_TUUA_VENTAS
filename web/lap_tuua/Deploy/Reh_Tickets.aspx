<%@ page language="C#" autoeventwireup="true" inherits="Reh_Tickets, App_Web_7ctknflu" responseencoding="utf-8" %>

<%@ Register Src="UserControl/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="uc1" %>
<%@ Register Src="UserControl/PiePagina.ascx" TagName="PiePagina" TagPrefix="uc2" %>
<%@ Register Src="UserControl/ConsRepresentante.ascx" TagName="ConsRepresentante"
    TagPrefix="uc3" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="UserControl/ConsDetTicket.ascx" TagName="ConsDetTicket" TagPrefix="uc5" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script language="javascript" type="text/javascript">
    function sleep(naptime){
        naptime = naptime * 1000; 
        var sleeping = true; 
        var now = new Date(); 
        var alarm; 
        var startingMSeconds = now.getTime(); 
        //alert("starting nap at timestamp: " + startingMSeconds + "\nWill sleep for: " + naptime + " ms"); 
        while(sleeping){ 
            alarm = new Date(); 
            alarmMSeconds = alarm.getTime(); 
            if(alarmMSeconds - startingMSeconds > naptime){ sleeping = false; } 
        }         
        //alert("Wakeup!"); 
    }    
    
    function mostrarExcelBtn(){
        if(document.getElementById('pnlExcel') != null){
            document.getElementById('pnlExcel').style.display = "block";        
        }
    }
</script>

<script language="javascript" type="text/javascript">
function createXMLHttpRequest() {     
    if (window.ActiveXObject) {      
        return new ActiveXObject("Microsoft.XMLHTTP");   
    } else if (window.XMLHttpRequest) {      
        return new XMLHttpRequest();     
    }    
} 
var xmlHttp;
function EnviaLectura() {
    var url = document.location.href;
    //alert(url);
    xmlHttp = createXMLHttpRequest();
    xmlHttp.open("POST", url);
    xmlHttp.onreadystatechange = onCompleteData;
    xmlHttp.send(null);
}
function onCompleteData() {
    if (xmlHttp.readyState == 4) {
        //alert(xmlHttp.responseText);
    }
}


</script>

<script language="javascript" type="text/javascript">
    var comEvReceive;
    var strCad = "";
    var ioPort = 0;

    function OpenPort(){
        if(document.forms.item(0).MSCommObj==null)
            alert("No se puede conectar el lector.");
         else{
            //msgbox("cargado")
            try{
              document.forms.item(0).MSCommObj.PortOpen = true;
              //alert(document.forms.item(0).hdPortSerialLector.value);
              //alert(document.forms.item(0).MSCommObj.CommPort);
              //alert(document.forms.item(0).MSCommObj.PortOpen);
            }
            catch(err){
              //alert('<//%=replacePort(htLabels["msgLectora.IncorrectPort"].ToString()) %//>'); ***
              //alert("Error en la configuracion del puerto serial\nal que esta conectado la lectora.\nConectelo al COM1.");
            }
        }
    }

    function CargaPropiedades(){
        comEvReceive = 2;
        try{
          document.forms.item(0).MSCommObj.CommPort = document.forms.item(0).hdPortSerialLector.value;
          document.forms.item(0).MSCommObj.Settings = "9600,N,8,1";
          document.forms.item(0).MSCommObj.DTREnable = true;
          document.forms.item(0).MSCommObj.InputLen = 512;
          OpenPort();
        }
        catch(err){
          //alert("Configure su IExplorer para soportar lectura de lectora.");
          alert('<%=htLabels["msgLectora.NotAllowActiveXBrowser"] %>');
        }
    }
    
    function OnComm(){
      switch(document.forms.item(0).MSCommObj.CommEvent){
        case comEvReceive:
          sleep(1);
          while(document.forms.item(0).MSCommObj.InbufferCount>0){
            strCad = strCad + document.forms.item(0).MSCommObj.Input;
          }
          document.forms.item(0).DataInput.value = strCad;
          if(strCad.indexOf(String.fromCharCode(3))>=0){
            //var i = strCad.indexOf(String.fromCharCode(3));
            document.forms.item(0).MSCommObj.PortOpen = false;
            ioPort = 1;
            document.getElementById("btnAgregar").click();
          }
          break;
      }
    }
</script>

<script language="javascript" for="MSCommObj" event="OnComm">OnComm()</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>LAP - Rehabilitacion de Tickets</title>
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
    <link href="css/Style.css" rel="stylesheet" type="text/css" />
    <!-- #INCLUDE file="javascript/KeyPress.js" -->
    <!-- #INCLUDE file="javascript/MantenerSesion.js" -->
    <!-- #INCLUDE file="javascript/validarteclaF5.js" -->
    <link href="css/Style.css" rel="stylesheet" type="text/css" />
</head>

<script language="javascript" type="text/javascript">
    function openPopup(popurl) {

        winpops = window.open(popurl, "", "width:800,height:200, status: No,toolbar=no, left=45, top=15, scrollbars=no, menubar=no,resizable=no,directories=no,location=no,modal=yes")
        /*
        if(window.showModalDialog){
        winpops = window.showModalDialog(popurl,"","dialogWidth:800px;dialogHeight:200px; status: No;toolbar=no, left=45, top=15, scrollbars=no, menubar=no,resizable=no,directories=no,location=no")
        }
        else{
        winpops = window.open(popurl,"","width:800,height:200, status: No,toolbar=no, left=45, top=15, scrollbars=no, menubar=no,resizable=no,directories=no,location=no,modal=yes")
        }
        */
    }
</script>

<script type="text/javascript">
    // We need to set the ReadOnly property here, since in VS 2.0+, setting it in the markup does not persist client-side changes.
    function windowOnLoad() {
        document.getElementById('lblTxtSeleccionados').readOnly = true;
        document.getElementById('lblTxtIngresados').readOnly = true;
    }

    window.onload = windowOnLoad;
</script>

<body onload="CargaPropiedades()">
    <form id="form1" runat="server" defaultbutton="btnAgregar">
    <!--
        <textarea id="txtData" runat="server" style="width:10px;height:10px"></textarea>
        <input type="hidden" id="Data1"  runat="server"/>
        <input type="text" id="txtDatos" name="txtDatos" />
        -->      
    <input type="hidden" name="DataInput" style="height: 0px" />
    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="3600">
    </asp:ScriptManager>

    <script language="javascript" type="text/javascript">
        function SetCheckBoxHeaderGrilla(control) {
            var frm = document.forms[0];
            if (control.checked) {
                var j = 1;
                for (i = 0; i < frm.elements.length; i++) {
                    if (frm.elements[i].name.indexOf('chkSeleccionar') >= 0 && frm.elements[i].type == "checkbox") {
                        if (!frm.elements[i].checked) {
                            j = 0;
                            break;
                        }
                    }
                }
                if (j == 1) {
                    frm.chkAll.checked = true;
                }
            } else {
                frm.chkAll.checked = false;
            }

            //alert('llegue');
            //alert(control);
            //alert(control.parentNode);
            //alert(control.parentNode.previousSibling);
            //alert(control.parentNode.previousSibling.innerHTML);
            var observacion = control.parentNode.previousSibling.previousSibling.innerHTML; //ojo
            if (control.checked) {
                var numSelTotal = parseInt(frm.hdNumSelTotal.value);
                numSelTotal += 1;
                frm.hdNumSelTotal.value = numSelTotal;
                if (observacion != "-") {
                    var numSelConObs = parseInt(frm.hdNumSelConObs.value);
                    numSelConObs += 1;
                    frm.hdNumSelConObs.value = numSelConObs;
                }
            }
            else {
                var numSelTotal = parseInt(frm.hdNumSelTotal.value);
                numSelTotal -= 1;
                frm.hdNumSelTotal.value = numSelTotal;
                if (observacion != "-") {
                    var numSelConObs = parseInt(frm.hdNumSelConObs.value);
                    numSelConObs -= 1;
                    frm.hdNumSelConObs.value = numSelConObs;
                }
            }
            //alert("NumSelTotal:" + frm.hdNumSelTotal.value);
            //alert("NumSelConObs:" + frm.hdNumSelConObs.value);        

            var normales = parseInt(frm.hdNumSelTotal.value) - parseInt(frm.hdNumSelConObs.value);
            frm.lblTxtSeleccionados.value = frm.hdNumSelTotal.value + " (" + frm.hdNumSelConObs.value + " Observaciones / " + normales + " Normales)";
        }

        function CheckBoxHeaderGrilla() {
            var frm = document.forms[0];
            if (frm.chkAll != null) {//Esto para cuando oculte el pnlPrincipal
                var j = 1;
                var count = 0;
                for (i = 0; i < frm.elements.length; i++) {
                    if (frm.elements[i].name.indexOf('chkSeleccionar') >= 0 && frm.elements[i].type == "checkbox") {
                        count = count + 1;
                        if (!frm.elements[i].checked) {
                            j = 0;
                            break;
                        }
                    }
                }
                if (j == 1 && count > 0) {
                    frm.chkAll.checked = true;
                }
                else {
                    frm.chkAll.checked = false;
                }
                if (count > 0)
                    frm.chkAll.disabled = false;
                else
                    frm.chkAll.disabled = true;
            }
        }

        function SetCheck(control) 
        {
            var frm = document.forms[0];
            var checkbox;
            var observacion;
            var numSelTotal = parseInt(frm.hdNumSelTotal.value);
            var numSelConObs = parseInt(frm.hdNumSelConObs.value);
            if (control.checked) {
                for (i = 0; i < frm.elements.length; i++) {
                    if (frm.elements[i].type == "checkbox" && frm.elements[i].name != 'chkAll' && !frm.elements[i].checked) {//
                        checkbox = frm.elements[i];
                        observacion = checkbox.parentNode.previousSibling.previousSibling.innerHTML;
                        numSelTotal += 1;
                        frm.hdNumSelTotal.value = numSelTotal;
                        if (observacion != "-") {
                            numSelConObs += 1;
                        }
                        checkbox.checked = true;
                    }
                }
            }
            else
             {
                for (i = 0; i < frm.elements.length; i++) {
                    if (frm.elements[i].type == "checkbox" && frm.elements[i].name != 'chkAll' && frm.elements[i].checked) {
                        checkbox = frm.elements[i];
                        observacion = checkbox.parentNode.previousSibling.previousSibling.innerHTML;
                        numSelTotal -= 1;
                        if (observacion != "-") {
                            numSelConObs -= 1;
                        }
                        checkbox.checked = false;
                    }
                }
            }
            frm.hdNumSelTotal.value = numSelTotal;
            frm.hdNumSelConObs.value = numSelConObs;

            //alert("NumSelTotal:" + frm.hdNumSelTotal.value);  
            //alert("NumSelConObs:" + frm.hdNumSelConObs.value);         

            var normales = parseInt(frm.hdNumSelTotal.value) - parseInt(frm.hdNumSelConObs.value);
            frm.lblTxtSeleccionados.value = frm.hdNumSelTotal.value + " (" + frm.hdNumSelConObs.value + " Observaciones / " + normales + " Normales)";


        }

        function onDeleteClientClick(control) {
            var frm = document.forms[0];
            if (control.parentNode.previousSibling.children[0].checked) {
                var observacion = control.parentNode.previousSibling.previousSibling.previousSibling.innerHTML;
                var numSelTotal = parseInt(frm.hdNumSelTotal.value);
                numSelTotal -= 1;
                frm.hdNumSelTotal.value = numSelTotal;
                if (observacion != "-") {
                    var numSelConObs = parseInt(frm.hdNumSelConObs.value);
                    numSelConObs -= 1;
                    frm.hdNumSelConObs.value = numSelConObs;
                }
            }
            else {
                //Total = Total - 1
            }
            //alert("NumSelTotal:" + frm.hdNumSelTotal.value);
            //alert("NumSelConObs:" + frm.hdNumSelConObs.value);        

            var normales = parseInt(frm.hdNumSelTotal.value) - parseInt(frm.hdNumSelConObs.value);
            frm.lblTxtSeleccionados.value = frm.hdNumSelTotal.value + " (" + frm.hdNumSelConObs.value + " Observaciones / " + normales + " Normales)";

            frm.lblTxtIngresados.value = parseInt(frm.lblTxtIngresados.value) - 1;
        }
    </script>

    <script type="text/javascript" language="javascript">
        //Script para validar campos y condiciones antes del submit.
        function btn_Rehabilitar_clientClick() {
            document.getElementById('lblErrorMsg').innerHTML = "";
            document.getElementById('lblErrNroTicket').innerHTML = "";
            if (document.forms[0].hdNumSelTotal.value == 0) {
                document.getElementById('spnRehabilitar').innerHTML = "Debe de seleccionar al menos un ticket para rehabilitar.";
                return false;
            }
            else {
                document.getElementById('spnRehabilitar').innerHTML = "";
            }
            var ret = confirm("¿Desea Continuar con la Rehabilitacion?");
            return ret;
        }
        function btnAgregar_clientClick() {
            if (ioPort == 0) {
                document.getElementById('spnRehabilitar').innerHTML = "";
                if (document.forms[0].txtNroTicket.value.length < 16) {
                    document.getElementById('lblErrNroTicket').innerHTML = "*";
                    document.getElementById('lblErrorMsg').innerHTML = "El numero Ticket tiene un formato no válido, debe contener 16 caracteres.";
                    return false;
                }
                else {
                    document.getElementById('lblErrNroTicket').innerHTML = "";
                    return true;
                }
            }
        }
    </script>

    <script type="text/javascript" language="javascript">
        function txtNroTicket_onkeypress() {
            var sKey, sKeyAscii;
            //alert (window.event.keyCode);
            if (window.event.keyCode != 13) {
                sKey = String.fromCharCode(window.event.keyCode);
                sKeyAscii = window.event.keyCode;
                if (!(sKey >= "0" && sKey <= "9")) {
                    alert('hey');
                    window.event.keyCode = 0;
                }
            }
            else {
                alert('habla');
                document.getElementById("btnAgregar").click();
            }
        }
    </script>

    <div>
        <table align="center" border="0" cellpadding="0" cellspacing="0" class="TamanoTabla">
            <tr>
                <td style="height: 11px">
                    <uc1:CabeceraMenu ID="CabeceraMenu2" runat="server" />
                </td>
            </tr>
        </table>
        <table align="center" border="0" cellpadding="0" cellspacing="0" class="TamanoTabla">
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <table align="center" border="0" cellpadding="0" cellspacing="0" class="TamanoTabla"
                                id="tableRehabilitar" runat="server">
                                <tr class="formularioTitulo">
                                    <td align="left">
                                        <asp:Button ID="btnRehabilitar" runat="server" CssClass="Boton" OnClientClick="Javascript:return btn_Rehabilitar_clientClick();"
                                            OnClick="btnRehabilitar_Click" />
                                        &nbsp;<asp:Label ID="lblDescripcionLimite" runat="server" Text="" CssClass="msgMensaje" ></asp:Label>
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <hr class="EspacioLinea" color="#0099cc" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblMensajeError" runat="server" CssClass="mensaje"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                
            </tr>
        </table>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:Panel ID="pnlPrincipal" runat="server" Visible="true">
                    <table align="center" border="0" cellpadding="0" cellspacing="0" class="TamanoTabla">
                        <tr>
                            <td>
                                <div>
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                        <tr>
                                            <td style="width: 100%">
                                                <table width="100%" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td style="width: 0%">
                                                        </td>
                                                        <td style="width: 100%">
                                                            <!-- inicio -->
                                                            <table style="width: 100%;" cellpadding="0" cellspacing="0" border="0">
                                                                <tr>
                                                                    <td width="2%" height="30px">
                                                                    </td>
                                                                    <td width="8%">
                                                                        <asp:Label ID="lblNroTicket" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                                                    </td>
                                                                    <td width="17%">
                                                                        <asp:TextBox ID="txtNroTicket" runat="server" CssClass="textbox" Width="150px" onkeypress="JavaScript: Tecla('Integer');"></asp:TextBox>
                                                                        <span id="lblErrNroTicket" style="color: Red;"></span>
                                                                    </td>
                                                                    <td width="17%">
                                                                        <asp:ImageButton ID="btnAgregar" runat="server" OnClientClick="Javascript:return btnAgregar_clientClick();"
                                                                            OnClick="btnAgregar_Click" ImageUrl="~/Imagenes/Add.bmp" />
                                                                    </td>
                                                                    <td>
                                                                        <a href="" id="lnkRepresentante" runat="server" onserverclick="lnkRepresentante_Click">
                                                                            <b>
                                                                                <asp:Label ID="lblConsRepresentante" runat="server" /></b></a>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td colspan="3" height="11px">
                                                                        <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red"></asp:Label>
                                                                        <asp:Label ID="spnRehabilitar" runat="server" Style="color: Red;" />
                                                                    </td>
                                                                </tr>
                                                                <tr id="trVuelo" runat="server">
                                                                    <td height="30px">
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblNumVuelo" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtNumVuelo" runat="server" CssClass="textbox" Width="150px" MaxLength="10"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <br />
                                                            <table style="width: 100%;" cellpadding="0" cellspacing="0" border="0">
                                                                <tr>
                                                                    <td width="2%">
                                                                    </td>
                                                                    <td>
                                                                        <div id="divGrid" style="height: 520px; overflow: auto;">
                                                                            <asp:GridView ID="gwvRehabilitarPorTicket" name="gwvRehabilitarPorTicket" RowStyle-VerticalAlign="Middle"
                                                                                RowStyle-HorizontalAlign="Center" runat="server" AutoGenerateColumns="False"
                                                                                CssClass="grilla" Width="98%" OnRowDataBound="gwvRehabilitarPorTicket_RowDataBound"
                                                                                OnRowCommand="gwvRehabilitarPorTicket_RowCommand" AllowPaging="True" OnPageIndexChanging="gwvRehabilitarPorTicket_PageIndexChanging"
                                                                                AllowSorting="True" OnSorting="gwvRehabilitarPorTicket_Sorting">
                                                                                <SelectedRowStyle CssClass="grillaFila" />
                                                                                <PagerStyle CssClass="grillaPaginacion" />
                                                                                <HeaderStyle CssClass="grillaCabecera" />
                                                                                <AlternatingRowStyle CssClass="grillaFila" />
                                                                                <Columns>
                                                                                    <asp:BoundField DataField="Numero" HeaderText="Nro." SortExpression="Numero" ItemStyle-Width="5%" />
                                                                                    <asp:TemplateField HeaderText="Codigo Ticket" SortExpression="Cod_Numero_Ticket"
                                                                                        ItemStyle-Width="12%">
                                                                                        <ItemTemplate>
                                                                                            <asp:LinkButton ID="codTicket" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                                                                                CommandName="ShowTicket" Text='<%# Eval("Cod_Numero_Ticket") %>' />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:BoundField DataField="Observacion" HeaderText="Observaciones" SortExpression="Observacion"
                                                                                        ItemStyle-Width="28%" />
                                                                                    <asp:TemplateField HeaderText="Motivo" ItemStyle-Width="38%">
                                                                                        <ItemTemplate>
                                                                                            <asp:DropDownList ID="cboMotivo" runat="server" Width="340px">
                                                                                            </asp:DropDownList>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField SortExpression="Check" ItemStyle-Width="7%">
                                                                                        <ItemTemplate>
                                                                                            <asp:CheckBox ID="chkSeleccionar" runat="server" onclick="Javascript: SetCheckBoxHeaderGrilla(this); " />
                                                                                        </ItemTemplate>
                                                                                        <HeaderTemplate>
                                                                                            <input type="checkbox" name="chkAll" disabled="true" onclick="JavaScript:SetCheck(this);" />
                                                                                            <asp:ImageButton ID="imageButton1" ImageAlign="AbsMiddle" runat="server" CommandName="Sort"
                                                                                                CommandArgument="Check" ImageUrl="~/Imagenes/icon_check.jpg" Width="20" Height="20" />
                                                                                        </HeaderTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Eliminar" ItemStyle-Width="10%">
                                                                                        <ItemTemplate>
                                                                                            <asp:ImageButton ID="btnEliminar" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                                                                                CommandName="Eliminar" ImageUrl="~/Imagenes/Delete.bmp" Width="20" Height="18" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                                <EmptyDataTemplate>
                                                                                    &nbsp;
                                                                                </EmptyDataTemplate>
                                                                            </asp:GridView>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <br />
                                                            <table style="width: 100%;" cellpadding="0" cellspacing="0" border="0">
                                                                <tr>
                                                                    <td colspan="3">
                                                                        <hr class="EspacioLinea" color="#0099cc" />
                                                                    </td>
                                                                </tr>
                                                                <tr class="formularioTitulo">
                                                                    <td width="2%" height="25px">
                                                                    </td>
                                                                    <td width="15%" class="TextoEtiqueta">
                                                                        <asp:Label ID="lblTotalSeleccionados" runat="server" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="lblTxtSeleccionados" Width="500px" BackColor="#f5f5f5" CssClass="TextoEtiqueta"
                                                                            BorderColor="#f5f5f5" BorderWidth="0" BorderStyle="None" runat="server">0 (0 Observaciones / 0 Normales)</asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr class="formularioTitulo">
                                                                    <td height="25px">
                                                                    </td>
                                                                    <td class="TextoEtiqueta">
                                                                        <asp:Label ID="lblTotalIngresados" runat="server" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="lblTxtIngresados" Width="500px" BackColor="#f5f5f5" CssClass="TextoEtiqueta"
                                                                            BorderColor="#f5f5f5" BorderWidth="0" BorderStyle="None" runat="server">0</asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="3">
                                                                        <hr class="EspacioLinea" color="#0099cc" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <input type="hidden" id="hdNumSelTotal" runat="server" value="0" />
                                                            <input type="hidden" id="hdNumSelConObs" runat="server" value="0" />
                                                            <!--FIN-->
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnlConformidad" runat="server" Visible="false">
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
                            <td width="35%" height="30px" valign="bottom" >
                               
                            </td>
                            <td >
                                <table cellpadding="0" cellspacing="0" style="width:100%;">
                                    <tr>
                                        <td><br>
                                            <asp:Label ID="lblConformidad" runat="server" CssClass="titulosecundario" />
                                            <br>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td><br><span class="msgMensaje">Resumen</span><br>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td><br>&nbsp;&nbsp;<span class="msgMensaje">Total de Tickets Rehabilitados : </span><asp:Label ID="lblTotalRehab" runat="server" CssClass="msgMensaje" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;&nbsp;<span class="msgMensaje">Total Tickets No Rehabilitados : </span><asp:Label ID="lblTotalNoRehab" runat="server" CssClass="msgMensaje" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td><br><span class="msgMensaje">Para conocer el detalle haga click en Imprimir</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:ImageButton ID="btnReporte" runat="server" ImageUrl="~/Imagenes/print.jpg" class="BotonImprimir"
                                    OnClick="btnReporte_click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="35%" height="30px">
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
        <table align="center" border="0" cellpadding="0" cellspacing="0" class="TamanoTabla">
            <td style="height: 11px" width="35%" >
                
            </td>
            <td>    
            <asp:Panel ID="pnlExcel" runat="server" Style="display:none" >
            <span class="msgMensaje">Exportar a Excel&nbsp;&nbsp;&nbsp;&nbsp; </span>
                
                &nbsp;<asp:ImageButton ID="imgExportarExcel" runat="server" 
                    ImageUrl="~/Imagenes/excel.jpg" onclick="imgExportarExcel_Click" Width="32px" 
                    Height="29px" />
            </asp:Panel>                        
            </td>
            <td width="35%" >&nbsp;</td>
            
        </table>
    </div>

    <script type="text/javascript">
        function Atras() {
            window.location.href = "./Reh_Tickets.aspx";
        }
        function fnClickCerrarPopup(sender, e) {
            __doPostBack(sender, e);
        }
    </script>

    <object id="MSCommObj" width="0px" height="0px" classid="clsid:648a5600-2c6e-101b-82b6-000000000014"
        codebase="http://activex.microsoft.com/controls/vb6/mscomm32.cab">
        <param name="_extentx" value="1005">
        <param name="_extenty" value="1005">
        <param name="_version" value="327681">
        <param name="commport" value="1">
        <param name="rthreshold" value="1">
    </object>
    <uc3:ConsRepresentante ID="consRepre" runat="server" />
    <uc5:ConsDetTicket ID="ConsDetTicket" runat="server" />
    
    <asp:UpdateProgress AssociatedUpdatePanelID="UpdatePanel2" DisplayAfter="0" ID="updateProgress"
        runat="server">
        <ProgressTemplate>
            <div id="processMessage">
                &nbsp;&nbsp;&nbsp;Procesando...<br />
                <br />
                <img alt="Loading" src="Imagenes/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    
    <input type="hidden" id="hdPortSerialLector" runat="server" />
    </form>
</body>
</html>
