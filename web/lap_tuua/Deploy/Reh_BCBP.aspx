<%@ page language="C#" autoeventwireup="true" inherits="Reh_BCBP, App_Web_jlql8yfo" %>

<%@ Register Src="UserControl/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="uc1" %>
<%@ Register Src="UserControl/PiePagina.ascx" TagName="PiePagina" TagPrefix="uc2" %>
<%@ Register Src="UserControl/ConsRepresentante.ascx" TagName="ConsRepresentante" TagPrefix="uc3" %>
<%@ Register Src="UserControl/CnsDetBoarding.ascx" TagName="CnsDetBoarding" TagPrefix="uc4" %>
<%@ Register Src="UserControl/IngBCBPAsociado.ascx" TagName="IngBoarding" TagPrefix="uc5" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script language="javascript" type="text/javascript">
    function sleep(naptime) {
        naptime = naptime * 1000;
        var sleeping = true;
        var now = new Date();
        var alarm;
        var startingMSeconds = now.getTime();
        //alert("starting nap at timestamp: " + startingMSeconds + "\nWill sleep for: " + naptime + " ms"); 
        while (sleeping) {
            alarm = new Date();
            alarmMSeconds = alarm.getTime();
            if (alarmMSeconds - startingMSeconds > naptime) { sleeping = false; }
        }
        //alert("Wakeup!");
    }

    function mostrarExcelBtn() {
        if (document.getElementById('pnlExcel') != null) {
            document.getElementById('pnlExcel').style.display = "block";
        }        
    }


    function mostrarTodosBtn() {

        if (document.getElementById('pnlExcel') != null) {
            document.getElementById('pnlExcel').style.display = "block";
        }  
    
        if (document.getElementById('pnlBoucher') != null) {
            document.getElementById('pnlBoucher').style.display = "block";
        }
    }
    
    

    function seleccionarFocoOpcion() {
        if (sControl != "") {
            window.location.href = "#" + sControl;
        }
    }
    
</script>


<script language="javascript" type="text/javascript">

var IdResultante = "";
var IdControl = "";
var IdResultante2 = "";
var IdSeleccion = "";
var hBanderaLlegadaValores = "0";
var IdhCompania ="";
var IdHSecuencialBCBP = "";
var hPersona ="";
var hDCompania = "";
var sControl = "";

//Pop Pup 

function obtenerID(control){
    IdResultante = control.id;  
    IdResultante2 = control.id;  
    IdhCompania =  control.id;
    IdHSecuencialBCBP =  control.id;
    
    hPersona = control.id;
    hDCompania = control.id;
    sControl = control.id;  
    
    IdResultante = IdResultante.split('btnAdicionarManual').join('hTramaPopPup');
    IdResultante2 = IdResultante2.split('btnAdicionarManual').join('hTramaPistola');
    IdhCompania = IdhCompania.split('btnAdicionarManual').join('hCompania');
    IdHSecuencialBCBP = IdHSecuencialBCBP.split('btnAdicionarManual').join('hNumSecuencialBCBP');

    hPersona = hPersona.split('btnAdicionarManual').join('hNomPasajero');
    hDCompania = hDCompania.split('btnAdicionarManual').join('hDscCompania');

    sControl = sControl.split('btnAdicionarManual').join('descBCBPX');
    
     //Reiniciar el Indice
     if(IdSeleccion != ""){
        if(document.getElementById(IdSeleccion) != null){
            document.getElementById(IdSeleccion).value = "";
        }
     }
     IdSeleccion = control.id;     
     IdSeleccion = IdSeleccion.split('btnAdicionarManual').join('hSeleccion');
     document.getElementById(IdSeleccion).value = "1";
     
     
     //Setear Valores de Compañia y NumSecuencialBCBP
     document.forms.item(0).hCompaniaBCBP.value = document.getElementById(IdhCompania).value;
     document.forms.item(0).hSecuencialBCBP.value = document.getElementById(IdHSecuencialBCBP).value;
     
     document.forms.item(0).hPasajero.value = document.getElementById(hPersona).value;
     document.forms.item(0).hDscCompania.value = document.getElementById(hDCompania).value;

     seleccionarFocoOpcion();
    
}

function mandarValores(){
    
    document.getElementById('IngBoarding1_lblError').innerHTML = "";
    if(hBanderaLlegadaValores == "0")
    {
        
        var nroVuelo = document.getElementById('IngBoarding1_txtNumVuelo').value;
        nroVuelo = nroVuelo.replace(/^\s*|\s*$/g,"");
        var fechaVuelo = document.getElementById('IngBoarding1_txtFechaVuelo').value;
        fechaVuelo = fechaVuelo.replace(/^\s*|\s*$/g,"");
        var asiento = document.getElementById('IngBoarding1_txtAsiento').value;
        asiento = asiento.replace(/^\s*|\s*$/g,"");
        var pasajero = document.getElementById('IngBoarding1_txtPasajero').value;
        pasajero = pasajero.replace(/^\s*|\s*$/g,"");
        
        if(nroVuelo=="" || fechaVuelo=="")
        {
            document.getElementById('IngBoarding1_lblError').innerHTML = "Ingrese los campos necesarios."
            return false;
        }
        else
        {
        
            if(pasajero==""){
                pasajero = document.forms.item(0).hPasajero.value;
            }
            
            hBanderaLlegadaValores = "1";
            document.forms.item(0).DataInput.value = nroVuelo + ";" +  fechaVuelo + ";" + asiento + ";" + pasajero + ";" + document.getElementById(IdhCompania).value;
            document.getElementById(IdResultante).value = nroVuelo + ";" +  fechaVuelo + ";" + asiento + ";" + pasajero + ";" + document.getElementById(IdhCompania).value ;
            document.getElementById(IdResultante2).value = "";
            //alert(document.getElementById(IdResultante).value);     
            
            IdhCompania = "";
            reinicarControlPopup();  
            document.getElementById("IngBoarding1_btnCerrarPopup").click(); 
                                      
            //Ejecutar Ajax 
            document.getElementById("btnRecargaPopPup").click(); 
            
            return false
        }
    }
    
}

function reinicarControlPopup(){

        document.getElementById('IngBoarding1_txtNumVuelo').value = "";
        document.getElementById('IngBoarding1_txtFechaVuelo').value = "";
        document.getElementById('IngBoarding1_txtAsiento').value = "";
        document.getElementById('IngBoarding1_txtPasajero').value = "";
        document.getElementById('IngBoarding1_lblError').innerHTML = "";

}

//Fin Pop Pup

//Lector

function setearFocoLector(control){
     IdControl = control.id;
     IdResultante2 = control.id;     
     IdResultante2 = IdResultante2.split('btnAdicionarPistola').join('hTramaPistola');
     IdResultante = control.id;   
     IdResultante = IdResultante.split('btnAdicionarPistola').join('hTramaPopPup');
     sControl = control.id;  
     
     IdhCompania =  control.id;
     IdHSecuencialBCBP =  control.id;
     IdhCompania = IdhCompania.split('btnAdicionarPistola').join('hCompania');
     IdHSecuencialBCBP = IdHSecuencialBCBP.split('btnAdicionarPistola').join('hNumSecuencialBCBP');

     sControl = sControl.split('btnAdicionarPistola').join('descBCBPX');
     
     //Setear Valores de Compañia y NumSecuencialBCBP
     document.forms.item(0).hCompaniaBCBP.value = document.getElementById(IdhCompania).value;
     document.forms.item(0).hSecuencialBCBP.value = document.getElementById(IdHSecuencialBCBP).value;
     
     hPersona = control.id;
     hDCompania = control.id;
     hPersona = hPersona.split('btnAdicionarPistola').join('hNomPasajero');
     hDCompania = hDCompania.split('btnAdicionarPistola').join('hDscCompania');     
     
     document.forms.item(0).hPasajero.value = document.getElementById(hPersona).value;
     document.forms.item(0).hDscCompania.value = document.getElementById(hDCompania).value;
     
     
     
     //Reiniciar el Indice
     if(IdSeleccion != ""){
        if(document.getElementById(IdSeleccion) != null){
            document.getElementById(IdSeleccion).value = "";
        }
     }
     IdSeleccion = control.id;     
     IdSeleccion = IdSeleccion.split('btnAdicionarPistola').join('hSeleccion');
     document.getElementById(IdSeleccion).value = "1";

     seleccionarFocoOpcion();
  
}

//Fin Lector

//Eliminar Asociado

function eliminarAsociadoBCBP(control){
     
     //Reiniciar el Indice
     if(IdSeleccion != ""){
        if(document.getElementById(IdSeleccion) != null){
            document.getElementById(IdSeleccion).value = "";
        }
     }
     IdSeleccion = control.id;     
     IdSeleccion = IdSeleccion.split('btnEliminarAsociado').join('hSeleccion');
     document.getElementById(IdSeleccion).value = "1";
     //Ejecutar Ajax 
     document.getElementById("btnEliminarAsociadoBCBP").click();    
     return false;     
}


//Fin Eliminar

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
    
    function resetearSelecControl()
    {
        IdControl = "";    
    }    
    
    
    function OnComm(){
        switch (document.forms.item(0).MSCommObj.CommEvent) {
            case comEvReceive:
                sleep(1);
                while (document.forms.item(0).MSCommObj.InbufferCount > 0) {
                    strCad = strCad + document.forms.item(0).MSCommObj.Input;
                }

                var iq = strCad.indexOf(String.fromCharCode(3));
                document.forms.item(0).DataInput.value = escape(strCad.substring(0, iq));

                //document.forms.item(0).DataInput.value = strCad;
                if (strCad.indexOf(String.fromCharCode(3)) >= 0) {
                    //var i = strCad.indexOf(String.fromCharCode(3));
                    document.forms.item(0).MSCommObj.PortOpen = false;
                    ioPort = 1;

                    if (IdControl != "") {

                        document.getElementById(IdResultante2).value = document.forms.item(0).DataInput.value;
                        document.getElementById(IdResultante).value = "";
                        IdControl = "";
                        StopAndStartTime();
                        document.getElementById("btnRecargarPistola").click();
                    }
                    else {
                        StopAndStartTime();
                        document.getElementById("btnAgregar").click();
                    }

                }
                break;
        }
  }

  function beginRequest(sender, args) {
//      if (!sender.get_isInAsyncPostBack()) {
//          if (accionSave) {
//              document.getElementById('btnAceptar').disabled = true;
//              document.body.style.cursor = 'wait';
//          }
//      }
  }

  function endRequest(sender, args) {
//      if (!sender.get_isInAsyncPostBack()) {
//          seleccionarFocoOpcion()
//      }
  }    
    
</script>

<script language="javascript" type="text/javascript" for="MSCommObj" event="OnComm">OnComm()</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title> LAP - Rehabilitacion de BCBP</title>
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
    <form id="form1" runat="server">
    <input type="hidden" name="DataInput" />
    <input type="hidden" name="hSecuencialBCBP" />
    <input type="hidden" name="hCompaniaBCBP" />
    
    <input type="hidden" name="hPasajero" />
    <input type="hidden" name="hDscCompania" />
    
<asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="3600">
    <Scripts>
        <asp:ScriptReference Path="~/javascript/jSManager.js" />
    </Scripts>
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

      //Si se muestra campos de asociacion
      var numColumnas = parseInt(frm.hdMostrarAsociacion.value);
      var observacion = "";
      if (numColumnas == 0)
          observacion = control.parentNode.previousSibling.previousSibling.previousSibling.innerHTML; //ojo
      else
          observacion = control.parentNode.previousSibling.previousSibling.previousSibling.previousSibling.previousSibling.innerHTML; 
          
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

      var normales = parseInt(frm.hdNumSelTotal.value) - parseInt(frm.hdNumSelConObs.value);
      frm.lblTxtSeleccionados.value = frm.hdNumSelTotal.value + " (" + frm.hdNumSelConObs.value + " Observaciones / " + normales + " Normales)";
  }

  function SetCheckBoxHeaderGrillaBloq(control) {
      var frm = document.forms[0];
      if (control.checked) {
          var j = 1;
          for (i = 0; i < frm.elements.length; i++) {
              if (frm.elements[i].name.indexOf('chkBloquear') >= 0 && frm.elements[i].type == "checkbox") {
                  if (!frm.elements[i].checked) {
                      j = 0;
                      break;
                  }
              }
          }
          if (j == 1) {
              frm.chkBloq.checked = true;
          }
      } else {
          frm.chkBloq.checked = false;
      }
  }

    function CheckBoxHeaderGrilla() {
        var frm = document.forms[0];
        if (frm.chkAll != null) {//Esto para cuando oculte el pnlPrincipal
            var j = 1;
            var countSel = 0;
            var k = 1;
            var countBloq = 0;

            for (i = 0; i < frm.elements.length; i++) {
                if (frm.elements[i].name.indexOf('chkSeleccionar') >= 0 && frm.elements[i].type == "checkbox") {
                    countSel = countSel + 1;
                    if (!frm.elements[i].checked) {
                        j = 0;
                        break;
                    }
                }
            }
            for (i = 0; i < frm.elements.length; i++) {
                if (frm.elements[i].name.indexOf('chkBloquear') >= 0 && frm.elements[i].type == "checkbox") {
                    countBloq = countBloq + 1;
                    if (!frm.elements[i].checked) {
                        k = 0;
                        break;
                    }
                }
            }
            if (j == 1 && countSel > 0) { frm.chkAll.checked = true; }
            else { frm.chkAll.checked = false; }
            if (countSel > 0) { frm.chkAll.disabled = false; }
            else { frm.chkAll.disabled = true; }

            if (k == 1 && countBloq > 0) { frm.chkBloq.checked = true; }
            else { frm.chkBloq.checked = false; }
            if (countBloq > 0) { frm.chkBloq.disabled = false; }
            else { frm.chkBloq.disabled = true; }
        }
    }
    
    function SetCheck(control) {
      var frm = document.forms[0];
      var checkbox;
      var observacion;
      var numSelTotal = parseInt(frm.hdNumSelTotal.value);
      var numSelConObs = parseInt(frm.hdNumSelConObs.value);
      var chkBloq = "";
      j = 2;

      var numColumnas = parseInt(frm.hdMostrarAsociacion.value);
      var observacion = "";
      
      if (control.checked) {
          for (i = 0; i < frm.elements.length; i++) {

              //Valida que bloqueados no se hagan check
              if (j < 10) chkBloq = "gwvRehabilitarPorBCBP$ctl0" + j + "$chkBloquear";
              else chkBloq = "gwvRehabilitarPorBCBP$ctl" + j + "$chkBloquear";
              if (frm.elements[i].name == chkBloq) j++;
              //Fin

              if (frm.elements[i].type == "checkbox" && frm.elements[i].name != 'chkAll' && frm.elements[i].name != 'chkBloq' && frm.elements[i].name != chkBloq && !frm.elements[i].checked) {//
                  checkbox = frm.elements[i];
                  //observacion = checkbox.parentNode.previousSibling.previousSibling.previousSibling.innerHTML;
                  if (numColumnas == 0)
                      observacion = checkbox.parentNode.previousSibling.previousSibling.previousSibling.innerHTML; //ojo
                  else
                      observacion = checkbox.parentNode.previousSibling.previousSibling.previousSibling.previousSibling.previousSibling.innerHTML; 

                  numSelTotal += 1;
                  frm.hdNumSelTotal.value = numSelTotal;
                  if (observacion != "-") {
                      numSelConObs += 1;
                  }
                  checkbox.checked = true;
              }
          }
      }
      else {
          for (i = 0; i < frm.elements.length; i++) {
              //Valida que bloqueados no se hagan check
              if (j < 10) chkBloq = "gwvRehabilitarPorBCBP$ctl0" + j + "$chkBloquear";
              else chkBloq = "gwvRehabilitarPorBCBP$ctl" + j + "$chkBloquear";
              if (frm.elements[i].name == chkBloq) j++;
              //Fin
              if (frm.elements[i].type == "checkbox" && frm.elements[i].name != 'chkAll' && frm.elements[i].name != 'chkBloq' && frm.elements[i].name != chkBloq && frm.elements[i].checked) {
                  checkbox = frm.elements[i];
                  //observacion = checkbox.parentNode.previousSibling.previousSibling.previousSibling.innerHTML;
                  if (numColumnas == 0)
                      observacion = checkbox.parentNode.previousSibling.previousSibling.previousSibling.innerHTML; //ojo
                  else
                      observacion = checkbox.parentNode.previousSibling.previousSibling.previousSibling.previousSibling.previousSibling.innerHTML; 

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

      var normales = parseInt(frm.hdNumSelTotal.value) - parseInt(frm.hdNumSelConObs.value);
      frm.lblTxtSeleccionados.value = frm.hdNumSelTotal.value + " (" + frm.hdNumSelConObs.value + " Observaciones / " + normales + " Normales)";
    }

    function SetCheckBloq(control) {
        var frm = document.forms[0];
        var checkbox;
        var chkSeleccionar = "";
        j = 2;
        if (control.checked) {
            for (i = 0; i < frm.elements.length; i++) {

                //Valida que seleccionados no se hagan check
                if (j < 10) chkSeleccionar = "gwvRehabilitarPorBCBP$ctl0" + j + "$chkSeleccionar";
                else chkSeleccionar = "gwvRehabilitarPorBCBP$ctl" + j + "$chkSeleccionar";
                if (frm.elements[i].name == chkSeleccionar) j++;
                //Fin

                if (frm.elements[i].type == "checkbox" && frm.elements[i].name != 'chkAll' && frm.elements[i].name != 'chkBloq' && frm.elements[i].name != chkSeleccionar && !frm.elements[i].checked) {//
                    checkbox = frm.elements[i];
                    checkbox.checked = true;
                }
            }
        }
        else {
            for (i = 0; i < frm.elements.length; i++) {
                //Valida que seleccionados no se hagan check
                if (j < 10) chkSeleccionar = "gwvRehabilitarPorBCBP$ctl0" + j + "$chkSeleccionar";
                else chkSeleccionar = "gwvRehabilitarPorBCBP$ctl" + j + "$chkSeleccionar";
                if (frm.elements[i].name == chkSeleccionar) j++;
                //Fin
                if (frm.elements[i].type == "checkbox" && frm.elements[i].name != 'chkAll' && frm.elements[i].name != 'chkBloq' && frm.elements[i].name != chkSeleccionar && frm.elements[i].checked) {
                    checkbox = frm.elements[i];
                    checkbox.checked = false;
                }
            }
        }
    }
    
    function onDeleteClientClick(control) {
        var frm = document.forms[0];
        var numColumnas = parseInt(frm.hdMostrarAsociacion.value);
        var observacion = "";

        if (control.parentNode.previousSibling.children[0].checked) {
            if (numColumnas == 0)
                observacion = checkbox.parentNode.previousSibling.previousSibling.previousSibling.innerHTML; //ojo
            else
                observacion = checkbox.parentNode.previousSibling.previousSibling.previousSibling.previousSibling.previousSibling.innerHTML; 

        //var observacion = control.parentNode.previousSibling.previousSibling.previousSibling.innerHTML;
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
      document.getElementById('lblErr_cboCompanias').innerHTML = "";
      document.getElementById('lblErr_txtFechaVuelo').innerHTML = "";
      document.getElementById('lblErr_cboVuelo').innerHTML = "";
      document.getElementById('lblErr_txtAsiento').innerHTML = "";
      document.getElementById('lblErr_txtPersona').innerHTML = "";
      document.getElementById('lblErrorMsg').innerHTML = "";
      if (document.forms[0].hdNumSelTotal.value == 0) {
        document.getElementById('spnRehabilitar').innerHTML = "Debe de seleccionar al menos un boarding para rehabilitar.";
        return false;
      }
      else {
        document.getElementById('spnRehabilitar').innerHTML = "";
      }
      var ret = confirm("¿Desea Continuar con la Rehabilitacion?");
      return ret;
    }
    
    function btnAgregar_clientClick() {
      if(ioPort == 0){
        document.getElementById('spnRehabilitar').innerHTML = "";
        var ret = true;
        if (document.forms[0].cboCompanias.selectedIndex == 0) {
          document.getElementById('lblErr_cboCompanias').innerHTML = "*";
          ret = false;
        }
        else
          document.getElementById('lblErr_cboCompanias').innerHTML = "";
        if (document.forms[0].txtFechaVuelo.value.length < 10) {
          document.getElementById('lblErr_txtFechaVuelo').innerHTML = "*";
          ret = false;
        }
        else
          document.getElementById('lblErr_txtFechaVuelo').innerHTML = "";
        if (document.forms[0].cboVuelo.selectedIndex == 0) {
          document.getElementById('lblErr_cboVuelo').innerHTML = "*";
          ret = false;
        }
        else
          document.getElementById('lblErr_cboVuelo').innerHTML = "";
        if (document.forms[0].txtAsiento.value == "") {
          document.getElementById('lblErr_txtAsiento').innerHTML = "*";
          ret = false;
        }
        else
          document.getElementById('lblErr_txtAsiento').innerHTML = "";
        if (document.forms[0].txtPersona.value == "") {
          document.getElementById('lblErr_txtPersona').innerHTML = "*";
          ret = false;
        }
        else
          document.getElementById('lblErr_txtPersona').innerHTML = "";

        if (!ret) {
          document.getElementById('lblErrorMsg').innerHTML = "Se debe completar los campos requeridos";
          return false;
        }
        else
          return true;      
      }
    }    
    
    
    function btnAgregar_clientClick2() {
    
    
      var compania = document.forms[0].cboCompanias.selectedIndex;
      var fechavuelo = document.forms[0].txtFechaVuelo.value;
      var nrovuelo = document.forms[0].txtNroVuelo.value;
      var asiento = document.forms[0].txtAsiento.value;
      var persona = document.forms[0].txtPersona.value;
      
      document.getElementById('lblErr_cboCompanias').innerHTML = "";
      document.getElementById('lblErr_txtFechaVuelo').innerHTML = "";
      document.getElementById('lblErr_cboVuelo').innerHTML = "";
  
      if(ioPort == 0)
      {
        document.getElementById('spnRehabilitar').innerHTML = "";       
        var ret = true; 
        if (compania == 0) {
          document.getElementById('lblErr_cboCompanias').innerHTML = "*";
          ret = false;
        }
        else if (fechavuelo.length < 10 && (nrovuelo == "" && asiento == "" && persona == "")) 
        {
          document.getElementById('lblErr_txtFechaVuelo').innerHTML = "*";
          ret = false;
        }
        else if (nrovuelo == ""  && (fechavuelo.length < 10 && asiento == "" && persona == "")) 
        {
          document.getElementById('lblErr_cboVuelo').innerHTML = "*";
          ret = false;
        }
        
        if (!ret) {
          document.getElementById('lblErrorMsg').innerHTML = "Se debe completar los campos requeridos";
          return false;
        }
        else{
          return true;                
        }  
      }
    }  
    
    
    </script>

    <div>
        <table align="center" border="0" cellpadding="0" cellspacing="0" class="TamanoTabla">
            <tr>
                <td style="height: 11px">
                    <uc1:CabeceraMenu ID="CabeceraMenu1" runat="server" />
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
                                            OnClick="btnRehabilitarExpancion_Click" />
                                        &nbsp;
                                        <asp:Label ID="lblDescripcionLimite" runat="server" Text="" CssClass="msgMensaje" ></asp:Label>

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
                                                                    <td width="5%" height="30px">
                                                                    </td>
                                                                    <td width="8%">
                                                                        <asp:Label ID="lblCia" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <table cellpadding="0" cellspacing="0">
                                                                        <tr>
                                                                        <td>
                                                                        <asp:DropDownList ID="cboCompanias" runat="server" AutoPostBack="False" OnSelectedIndexChanged="cboCompanias_SelectedIndexChanged"
                                                                            Width="150px" CssClass="combo">
                                                                        </asp:DropDownList>                                                                        
                                                                        </td>
                                                                        <td>
                                                                        <span id="lblErr_cboCompanias" style="color: Red;"></span>
                                                                        </td>
                                                                        </tr>
                                                                        </table>                                                                        
                                                                    </td>
                                                                    <td width="5%" colspan="2">
                                                                    </td>
                                                                    <td width="12%" colspan="2">
                                                                    </td>
                                                                    <td colspan="3">
                                                                        <a href="" id="lnkRepresentante" runat="server" onserverclick="lnkRepresentante_Click">
                                                                            <b>
                                                                                <asp:Label ID="lblConsRepresentante" runat="server" /></b></a>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td height="30px">
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblFechaVuelo" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                    
                                                                        <table cellpadding="0" cellspacing="0">
                                                                        <tr>
                                                                        <td>
                                                                        <asp:TextBox ID="txtFechaVuelo" runat="server" CssClass="textbox" Width="150px" 
                                                                            OnTextChanged="txtFechaVuelo_TextChanged" onfocus="this.blur();" onkeypress="JavaScript: window.event.keyCode = 0;"
                                                                            BackColor="#E4E2DC"></asp:TextBox>                                                                        
                                                                        </td>
                                                                        <td>
                                                                        <asp:ImageButton ID="imgbtnCalendar" ImageUrl="~/Imagenes/Calendar.bmp" runat="server"
                                                                            Width="22px" Height="22px" />
                                                                        </td>
                                                                        <td>
                                                                            <span id="lblErr_txtFechaVuelo" style="color: Red;"></span>
                                                                        </td>                                                                        
                                                                        </tr>                                                                        
                                                                        </table>                                                                        
                                                                        <cc1:CalendarExtender ID="txtFechaVuelo_CalendarExtender" runat="server" Enabled="True"
                                                                            TargetControlID="txtFechaVuelo" PopupButtonID="imgbtnCalendar" Format="dd/MM/yyyy"
                                                                            PopupPosition="BottomLeft"></cc1:CalendarExtender>
                                                                    </td>
                                                                    <td colspan="2">
                                                                        
                                                                    </td>
                                                                    <td colspan="2">
                                                                    <td>
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td height="30px">
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblVuelo" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                                                    </td>
                                                                    <td class="style1">
                                                                        <table cellpadding="0" cellspacing="0">
                                                                        <tr>
                                                                        <td>
                                                                            <asp:TextBox ID="txtNroVuelo" runat="server" CssClass="textbox" Width="150px" ></asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                            <span id="lblErr_cboVuelo" style="color: Red;"></span>
                                                                        </td>
                                                                        </tr>
                                                                        </table>                                                                
                                                                        <asp:DropDownList ID="cboVuelo" runat="server" Width="150px"
                                                                            CssClass="combo" OnSelectedIndexChanged="cboVuelo_OnSelectedIndexChanged" 
                                                                            Visible="False">
                                                                        </asp:DropDownList>
                                                                        
                                                                    </td>
                                                                    <td colspan="2">
                                                                    </td>
                                                                    <td colspan="5">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td height="30px">
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblAsiento" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                                                    </td>
                                                                    <td class="style1">
                                                                        <asp:TextBox ID="txtAsiento" runat="server" CssClass="textbox" Width="150px" MaxLength="4"></asp:TextBox>
                                                                        <span id="lblErr_txtAsiento" style="color: Red;"></span>
                                                                    </td>
                                                                    <td width="2%">
                                                                    </td>
                                                                    <td colspan="2">
                                                                        <asp:Label ID="lblPersona" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                                                    </td>
                                                                    <td width="25%" colspan="2">
                                                                        <asp:TextBox ID="txtPersona" runat="server" CssClass="textbox" Width="220px" MaxLength="20"></asp:TextBox>
                                                                        <span id="lblErr_txtPersona" style="color: Red;"></span>
                                                                    </td>
                                                                    <td width="5%">
                                                                        <asp:ImageButton ID="btnAgregar" runat="server" OnClientClick="Javascript:return btnAgregar_clientClick2();"
                                                                            OnClick="btnAgregar_Click2" ImageUrl="~/Imagenes/Add.bmp" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red"></asp:Label>
                                                                        <asp:Label ID="spnRehabilitar" Style="color: Red;" runat="server" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <br />
                                                            <table style="width: 100%;" cellpadding="0" cellspacing="0" border="0">
                                                                <tr>
                                                                    <td width="0%">
                                                                    </td>
                                                                    <td>
                                                                        <div id="divGrid" style="height: 505px; overflow: auto;">
                                                                            <asp:GridView ID="gwvRehabilitarPorBCBP" runat="server" AllowPaging="True" 
                                                                                AllowSorting="True" AutoGenerateColumns="False" CssClass="grilla" 
                                                                                 OnPageIndexChanging="gwvRehabilitarPorBCBP_PageIndexChanging"
                                                                                OnRowCommand="gwvRehabilitarPorBCBP_RowCommand" 
                                                                                OnRowDataBound="gwvRehabilitarPorBCBP_RowDataBound" 
                                                                                OnSorting="gwvRehabilitarPorBCBP_Sorting" RowStyle-HorizontalAlign="Center" 
                                                                                RowStyle-VerticalAlign="Middle" Width="100%">
                                                                                <SelectedRowStyle CssClass="grillaFila" />
                                                                                <PagerStyle CssClass="grillaPaginacion" />
                                                                                <HeaderStyle CssClass="grillaCabecera" />
                                                                                <AlternatingRowStyle CssClass="grillaFila" />
                                                                                <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                <Columns>
                                                                                    <asp:BoundField DataField="Numero" HeaderText="Nro." ItemStyle-Width="4%" 
                                                                                        SortExpression="Numero">
                                                                                        <ItemStyle Width="4%" />
                                                                                    </asp:BoundField>
                                                                                    <asp:TemplateField HeaderText="Descripcion BCBP" ItemStyle-Width="39%">
                                                                                        <ItemTemplate>                                                                                             
                                                                                            <asp:LinkButton ID="descBCBP" runat="server" 
                                                                                                CommandArgument="<%# Container.DataItemIndex %>" CommandName="ShowBCBP" 
                                                                                                Text='<%# rowDesc_Cia.Value + " " + Eval("Dsc_Compania") + "<br>" + rowDesc_NumVuelo.Value + " " + Eval("Num_Vuelo") + "&nbsp;&nbsp;-&nbsp;&nbsp;" + new StringBuilder().Append(rowDesc_FechaVuelo.Value).Append(" ").Append(LAP.TUUA.UTIL.Fecha.convertSQLToFecha(Convert.ToString(Eval("Fch_Vuelo")),null)).Append("&nbsp;&nbsp;-&nbsp;&nbsp;").Append(rowDesc_Asiento.Value).Append(" ").Append(Eval("Num_Asiento")).Append("<br>").Append(rowDesc_Pasajero.Value).Append(" ").Append(Eval("Nom_Pasajero")) %>' />
                                                                                            <asp:HiddenField ID="hCompania" runat="server" 
                                                                                                Value='<%# Eval("Cod_Compania") %>' />
                                                                                            <asp:HiddenField ID="hNumSecuencialBCBP" runat="server" 
                                                                                                Value='<%# Eval("Num_Secuencial_Bcbp") %>' />
                                                                                            <asp:HiddenField ID="hDscCompania" runat="server" 
                                                                                                Value='<%# Eval("Dsc_Compania") %>' />                                                                                               
                                                                                            <asp:HiddenField ID="hNomPasajero" runat="server" 
                                                                                                Value='<%# Eval("Nom_Pasajero") %>' />                                                                                             
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle Width="39%" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:BoundField DataField="Observacion" HeaderText="Observaciones" 
                                                                                        ItemStyle-Width="17%" SortExpression="Observacion">
                                                                                        <ItemStyle Width="17%" />
                                                                                    </asp:BoundField>
                                                                                    <asp:TemplateField HeaderText="Motivo" ItemStyle-Width="29%">
                                                                                        <ItemTemplate>
                                                                                            <asp:DropDownList ID="cboMotivo" runat="server" Width="270px">
                                                                                            </asp:DropDownList>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle Width="20%" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Asociar" Visible="False">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="descBCBPX" runat="server" Text=""></asp:Label>   
                                                                                            <asp:ImageButton ID="btnAdicionarPistola" runat="server" Height="18px" 
                                                                                                ImageUrl="~/Imagenes/Add.bmp" OnClientClick="setearFocoLector(this)" 
                                                                                                Width="18px"  Enabled="true" />
                                                                                            <br />
                                                                                            <br />
                                                                                            <asp:ImageButton ID="btnAdicionarManual" runat="server" 
                                                                                                CommandArgument="<%# Container.DataItemIndex  %>" 
                                                                                                CommandName="btnAdicionarManual" Height="18px" ImageUrl="~/Imagenes/file.gif" 
                                                                                                OnClientClick="obtenerID(this)" Width="18px" Enabled="true" />
                                                                                            <br />
                                                                                            <br />
                                                                                            <asp:ImageButton ID="btnEliminarAsociado" runat="server" Height="18px" 
                                                                                                ImageUrl="~/Imagenes/Delete.bmp" 
                                                                                                OnClientClick="return eliminarAsociadoBCBP(this)" Width="18px" Enabled="true" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="BCBP Asociado" Visible="False">
                                                                                        <ItemTemplate>
                                                                                            <table ID="tblDatosAsociados" runat="server">
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lblDCompania" runat="server" Text="Compañia:"></asp:Label>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lblCompania" runat="server" 
                                                                                                            Text='<%# Eval("Dsc_Compania_Asoc") %>'></asp:Label>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lblDNumeroVuelo" runat="server" Text="Numero Vuelo:"></asp:Label>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lblNumeroVuelo" runat="server" 
                                                                                                            Text='<%# Eval("Nro_Vuelo_Asoc") %>'></asp:Label>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lblDFechaVuelo" runat="server" Text="Fecha Vuelo:"></asp:Label>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lblFechaVuelo0" runat="server" 
                                                                                                            Text='<%# LAP.TUUA.UTIL.Fecha.convertSQLToFecha(Convert.ToString(Eval("Fch_Vuelo_Asoc")),null) %>'></asp:Label>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lblDAsiento" runat="server" EnableTheming="False" 
                                                                                                            Text="Asiento:"></asp:Label>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lblAsiento0" runat="server" 
                                                                                                            Text='<%# Eval("Nro_Asiento_Asoc") %>'></asp:Label>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lblDPasajero" runat="server" Text="Pasajero:"></asp:Label>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lblPasajero" runat="server" Text='<%# Eval("Pasajero_Asoc") %>'></asp:Label>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                            <asp:HiddenField ID="hTramaPistola" runat="server" 
                                                                                                Value='<%# Eval("TramaPistola_Asoc") %>' />
                                                                                            <asp:HiddenField ID="hTramaPopPup" runat="server" 
                                                                                                Value='<%# Eval("TramaPoppup_Asoc") %>' />
                                                                                            <asp:HiddenField ID="hSeleccion" runat="server" />
                                                                                            <br />
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle HorizontalAlign="Left" Width="20%" />
                                                                                    </asp:TemplateField>
                                                                                     <asp:TemplateField HeaderText="Bloquear">
                                                                                        <ItemTemplate>
                                                                                            <asp:CheckBox ID="chkBloquear"  runat="server" 
                                                                                                Checked='<%# ( /* Eval("Bloquear")!=DBNull.Value && */ Int32.Parse(Eval("Bloquear").ToString())==1) ? true : false  %>' 
                                                                                                onclick="Javascript: SetCheckBoxHeaderGrillaBloq(this);" />
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle Width="5%" />
                                                                                        <HeaderTemplate>
                                                                                            Bloquear<br />
                                                                                            <input disabled="true" name="chkBloq" onclick="JavaScript:SetCheckBloq(this);" 
                                                                                                type="checkbox" />
                                                                                        </HeaderTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField SortExpression="Check">
                                                                                        <ItemTemplate>
                                                                                            <asp:CheckBox ID="chkSeleccionar" runat="server" 
                                                                                                Checked='<%# ( /* Eval("Check")!=DBNull.Value && */ Int32.Parse(Eval("Check").ToString())==1) ? true : false  %>' 
                                                                                                onclick="Javascript: SetCheckBoxHeaderGrilla(this); " />
                                                                                        </ItemTemplate>
                                                                                        <HeaderTemplate>
                                                                                            Seleccionar<br />
                                                                                            <input disabled="true" name="chkAll" onclick="JavaScript:SetCheck(this);" type="checkbox" />
                                                                                            <%--<asp:ImageButton ID="imageButton1" runat="server" CommandArgument="Check" 
                                                                                                CommandName="Sort" Height="20" ImageAlign="AbsMiddle" 
                                                                                                ImageUrl="~/Imagenes/icon_check.jpg" Width="20" />--%>
                                                                                        </HeaderTemplate>
                                                                                        <ItemStyle Width="5%" />
                                                                                    </asp:TemplateField>
                                                                                    
                                                                                    <asp:TemplateField HeaderText="Eliminar" ItemStyle-Width="6%">
                                                                                        <ItemTemplate>
                                                                                            <asp:ImageButton ID="btnEliminar" runat="server" 
                                                                                                CommandArgument="<%# Container.DataItemIndex %>" CommandName="Eliminar" 
                                                                                                Height="18" ImageUrl="~/Imagenes/Delete.bmp" Width="20" OnClientClick="resetearSelecControl()" />
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle Width="6%" />
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
                                                                        <asp:TextBox ID="lblTxtSeleccionados"  Width="500px" BackColor="#f5f5f5" CssClass="TextoEtiqueta"
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
                                                            <input type="hidden" id="hdMostrarAsociacion" runat="server" value="0" />
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
                            <td width="35%" height="30px">
                            </td>
                            <td>
                                <table cellpadding="0" cellspacing="0" style="width: 100%;">
                                    <tr>
                                        <td>
                                            <br>
                                            <asp:Label ID="lblConformidad" runat="server" CssClass="titulosecundario" />
                                            <br>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <br>
                                            <span class="msgMensaje">Resumen</span><br>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <br>
                                            &nbsp;&nbsp;<span class="msgMensaje">Total de Boarding Pass Rehabilitados : </span>
                                            <asp:Label ID="lblTotalRehab" runat="server" CssClass="msgMensaje" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;&nbsp;<span class="msgMensaje">Total Boarding Pass No Rehabilitados : </span>
                                            <asp:Label ID="lblTotalNoRehab" runat="server" CssClass="msgMensaje" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <br>
                                            <span class="msgMensaje">Para conocer el detalle haga click en Imprimir</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:ImageButton
                                                ID="btnReporte" runat="server" ImageUrl="~/Imagenes/print.jpg" class="BotonImprimir"
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
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnRecargaPopPup" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnRecargarPistola" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnEliminarAsociadoBCBP" 
                    EventName="Click" /> 
            </Triggers>
        </asp:UpdatePanel>
        <table align="center" border="0" cellpadding="0" cellspacing="0" class="TamanoTabla">
            <tr>
             <td style="height: 11px" width="35%" >
                
            </td>
            <td>    
            <asp:Panel ID="pnlExcel" runat="server" Style="display:none">
            <span class="msgMensaje">Exportar a Excel&nbsp;&nbsp;&nbsp;&nbsp; </span>
                
                &nbsp;<asp:ImageButton ID="imgExportarExcel" runat="server" 
                    ImageUrl="~/Imagenes/excel.jpg" onclick="imgExportarExcel_Click" Width="32px" 
                    Height="29px" />
            </asp:Panel>                        
            </td>
            <td width="35%" >&nbsp;</td>
            </tr>

            <tr>
             <td style="height: 11px" width="35%" >
                
            </td>
            <td>    
            <asp:Panel ID="pnlBoucher" runat="server" Style="display:none">
            <span class="msgMensaje">Imprimir Voucher&nbsp;&nbsp;&nbsp;&nbsp; </span>
                
                &nbsp;<asp:ImageButton ID="btnImprimirBoucher" runat="server" 
                    ImageUrl="~/Imagenes/print.jpg" 
                    class="BotonImprimir" onclick="btnImprimirBoucher_Click" />
            </asp:Panel>                        
            </td>
            <td width="35%" >&nbsp;</td>
            </tr>            
            
        </table>
    </div>

    <script type="text/javascript">
    function Atras() {
      window.location.href = "./Reh_BCBP.aspx";
    }
    function fnClickCerrarPopup(sender, e) {
      __doPostBack(sender, e);
      alert('llego');
    }
    
    function fnClickCerrarPopup() {
      alert('llego');
    }    
    </script>

    <object id="MSCommObj" width="0px" height="0px" classid="clsid:648a5600-2c6e-101b-82b6-000000000014"
        codebase="http://activex.microsoft.com/controls/vb6/mscomm32.cab">
        <param name="_extentx" value="1005">
        <param name="_extenty" value="1005">
        <param name="_version" value="327681">
        <param name="commport" value="3">
        <param name="rthreshold" value="1">
    </object>
    <uc3:ConsRepresentante ID="consRepre" runat="server" />
    <uc4:CnsDetBoarding ID="CnsDetBoarding1" runat="server" />
    <uc5:IngBoarding ID="IngBoarding1" runat="server" />


    <asp:UpdateProgress AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="0" ID="updateProgress1" runat="server">
        <ProgressTemplate>
            <div id="processMessage">
                &nbsp;&nbsp;&nbsp;Procesando...<br />
                <br />
                <img alt="Loading" src="Imagenes/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    
    <asp:UpdateProgress AssociatedUpdatePanelID="UpdatePanel2" DisplayAfter="0" ID="updateProgress2" runat="server">
        <ProgressTemplate>
            <div id="processMessage">
                &nbsp;&nbsp;&nbsp;Procesando...<br />
                <br />
                <img alt="Loading" src="Imagenes/ajax-loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    
    <asp:HiddenField ID="rowDesc_Cia" runat="server" />
    <asp:HiddenField ID="rowDesc_NumVuelo" runat="server" />
    <asp:HiddenField ID="rowDesc_FechaVuelo" runat="server" />
    <asp:HiddenField ID="rowDesc_Asiento" runat="server" />
    <asp:HiddenField ID="rowDesc_Pasajero" runat="server" />
    <br />
    <asp:Button ID="btnRecargarPistola" runat="server" 
        Onclick="btnCargarDatosPistola_Click" Text="Refresh Lector" style="display:none" />
    <br />
    <asp:Button ID="btnRecargaPopPup" runat="server" 
        OnClick="btnCargarDatosPopPup_Click" Text="Refresh PopPup" style="display:none" />
        <br />
    <input type="hidden" id="hdPortSerialLector" runat="server" />
        <asp:Button ID="btnEliminarAsociadoBCBP" runat="server" Text="Eliminar Asociado"
         OnClick="btnEliminarDatosAsociadoBCBP_Click" style="display:none" />
    </form>
    

    
</body>
</html>
