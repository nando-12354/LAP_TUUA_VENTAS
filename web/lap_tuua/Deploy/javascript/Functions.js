<script type="text/javascript"> 
/* 
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

function DoSubmit(strTarget)
{
    //alert(strTarget);
    document.forms[0].action = strTarget;
	document.forms[0].submit();
	return true;
}

function verFecha(frm){
    if (isValidDate(frm.fechaInicial.value)){
        //alert("es valido");
    }else{
        alert("Formato de fecha no v�lido");
    }
}

function isValidForm(frm, strAction, strParams){
    if( frm.empresa.value == "" ){
        alert("Debe seleccionar una empresa");
        frm.empresa.focus();
    }
    else if ( frm.servicio.value == "" ){
        alert("Debe seleccionar un servicio");
        frm.servicio.focus();
        
    }
    else if( strAction =="cargarorden" && frm.cuenta.value == ""){
        alert("Debe seleccionar una cuenta");
        frm.cuenta.focus();
    }
    else{
        //alert(frm.tipoingreso.value);
        //es valido
        frm.action = "mantenerOrdenes.do?do="+ strAction + strParams;
        frm.submit();
    }
}
function isValidForm2(frm, from, strTarget){

    var response = false;
    if ( from == "ModOrden"){
        //modOrden.jsp
        //alert("ModOrden");
        response = isValidModOrden(frm, strTarget);
    }
    else if (from == "EliminarOrden"){
        //eliminarOrden.jsp
        //alert("EliminarOrden");
        response = isValidEliminarOrden(frm, strTarget);
    }
    else if (from == "CancelarOrden"){
        //eliminarOrden.jsp
        //alert("EliminarOrden");
        response = isValidCancelarOrden(frm, strTarget);
    }
    else if (from == "NvoOrden"){
        response = isValidNuevoOrden(frm);
    }
    else if (from == "TransfCtaPropia"){
        response = isValidTransfCtaPropia(frm);
    }
    else if (from == "TransfCtaTerceros"){
        response = isValidTransfCtaTerceros(frm);
    }
    else if (from == "TransfCtaInterBco"){
        response = isValidTransfCtaInterBco(frm);
    }
    else if ( from == ""){
        //....
    }

    if(response){
        //alert(frm.tipoingreso.value);
        //es valido
        frm.action = strTarget;
        frm.submit();
    }
}

//Formulario Ingresar Orden
function isValidNuevoOrden(frm){
    if( frm.empresa.value == "" ){
        alert("Debe seleccionar una empresa");
        frm.empresa.focus();
        return false;
    }
    else if ( frm.control.value == "" ){
        alert("Debe seleccionar un servicio");
        frm.servicio.focus();
        return false;
    }
    else if ( frm.cuenta.value == ""){
        alert("Debe seleccionar una cuenta");
        frm.cuenta.focus();
        return false;
    }
    
    else if (!(validarFechasRelacionadas2(frm.fechaInicial.value, frm.fechaFinal.value))){
        return false;   
    }
    
    else if (!validarFechaConReferencia(frm.fechaFinal.value, frm.fechaActualMax.value, "La Fecha de Vencimiento no puede ser mayor a un a�o de la Fecha Actual")){
        return false;
    }
    //alert(frm.fechaActualComp.value);
    /*if (!(validarFechasRelacionadas(frm.fechaInicial, frm.fechaFinal, frm.fechaActualComp.value))){
        return false;   
    }*/
    //
    //alert(frm.control[0].value);
    //alert(frm.control[0].checked);
    //alert(frm.control.length);

    return true;
}
function isValidModOrden(frm, strTarget){
    return true;
}
function isValidEliminarOrden(frm, strTarget){
    return true;
}
function isValidCancelarOrden(frm, strTarget){
    return true;
}
function isValidTransfCtaPropia(frm){
    return true;
}
function isValidTransfCtaTerceros(frm){
    return true;
}
function isValidTransfCtaInterBco(frm){
    return true;
}

function isValidDate(dateStr)
{
    var slash1 = dateStr.indexOf("/");

    if (slash1 == -1) { slash1 = dateStr.indexOf("-"); }
    // if no slashes or dashes, invalid date

    if (slash1 == -1) { return false; }

    var dateDay = dateStr.substring(0, slash1)

    var dateMonthAndYear = dateStr.substring(slash1+1, dateStr.length);

    var slash2 = dateMonthAndYear.indexOf("/");

    if (slash2 == -1) { slash2 = dateMonthAndYear.indexOf("-"); }
    // if not a second slash or dash, invalid date

    if (slash2 == -1) { return false; }

    var dateMonth = dateMonthAndYear.substring(0, slash2);

    var dateYear = dateMonthAndYear.substring(slash2+1, dateMonthAndYear.length);

    if ( (dateMonth == "") || (dateDay == "") || (dateYear == "") ) { return false; }
    // if any non-digits in the month, invalid date

    for (var x=0; x < dateMonth.length; x++) {
        var digit = dateMonth.substring(x, x+1);
        if ((digit < "0") || (digit > "9")) { return false; }
    }

    // convert the text month to a number
    var numMonth = 0;

    for (var x=0; x < dateMonth.length; x++)
    {
        digit = dateMonth.substring(x, x+1);
        numMonth *= 10;
        numMonth += parseInt(digit);
    }

    if ((numMonth <= 0) || (numMonth > 12)) { return false; }
    // if any non-digits in the day, invalid date

    for (var x=0; x < dateDay.length; x++) {
        digit = dateDay.substring(x, x+1);
        if ((digit < "0") || (digit > "9")) { return false; }
    }
    // convert the text day to a number

    var numDay = 0;
    for (var x=0; x < dateDay.length; x++) {
        digit = dateDay.substring(x, x+1);
        numDay *= 10;
        numDay += parseInt(digit);
    }

    if ((numDay <= 0) || (numDay > 31)) { return false; }
    // February can't be greater than 29 (leap year calculation comes later)

    if ((numMonth == 2) && (numDay > 29)) { return false; }
    // check for months with only 30 days

    if ((numMonth == 4) || (numMonth == 6) || (numMonth == 9) || (numMonth == 11)) {
        if (numDay > 30) { return false; }
    }
    // if any non-digits in the year, invalid date

    for (var x=0; x < dateYear.length; x++)
    {
        digit = dateYear.substring(x, x+1);
        if ((digit < "0") || (digit > "9")) { return false; }
    }
    // convert the text year to a number

    var numYear = 0;
    for (var x=0; x < dateYear.length; x++)
    {
        digit = dateYear.substring(x, x+1);
        numYear *= 10;
        numYear += parseInt(digit);
    }

    // Year must be a 2-digit year or a 4-digit year
    if ( (dateYear.length != 2) && (dateYear.length != 4) ) { return false; }

    // if 2-digit year, use 50 as a pivot date
    if ( (numYear < 50) && (dateYear.length == 2) ) { numYear += 2000; }

    if ( (numYear < 100) && (dateYear.length == 2) ) { numYear += 1900; }

    if ((numYear <= 0) || (numYear > 9999)) { return false; }
    // check for leap year if the month and day is Feb 29

    if ((numMonth == 2) && (numDay == 29))
    {
        var div4 = numYear % 4;
        var div100 = numYear % 100;
        var div400 = numYear % 400;
        // if not divisible by 4, then not a leap year so Feb 29 is invalid

        if (div4 != 0) { return false; }
        // at this point, year is divisible by 4. So if year is divisible by
        // 100 and not 400, then it's not a leap year so Feb 29 is invalid

        if ((div100 == 0) && (div400 != 0)) { return false; }
    }

    // date is valid
    return true;
}

//jwong 29/01/2009 para validar fechas relacionadas(inicio/fin)
/**
 * fecIni, fecha inicio (dd/MM/yyyy)
 * fecFin, fecha fin (dd/MM/yyyy)
 * fecActual, fecha actual (yyyyMMdd)
 */
function validarFechasRelacionadas(fecIni, fecFin, fecActual){
    //alert("Entra 1 =>>" + fecIni + " ; fecFin=>>" + fecFin + " ; fecActual=>>" + fecActual);
    var compIni = fecIni.substr(6, 4) + fecIni.substr(3, 2) + fecIni.substr(0, 2);
    var compFin = fecFin.substr(6, 4) + fecFin.substr(3, 2) + fecFin.substr(0, 2);

    //alert("Entra 2");
    
    //comparamos las fechas
    if(compFin < compIni){
        alert("Fecha desde no puede ser mayor que fecha hasta");
        return false;
    }
    else if(compFin > fecActual){
        alert("Fecha hasta no debe ser mayor que la fecha actual");
        return false;
    }
    else if(compIni > fecActual){
        alert("Fecha desde no debe ser mayor que la fecha actual");
        return false;
    }
    return true;
}

//jwong para validar que la fecha inicial no sea mayor que una fecha final
function validarFechaConReferencia(fecIni, fecFinFormateada, mensajeError){
    var compIni = fecIni.substr(6, 4) + fecIni.substr(3, 2) + fecIni.substr(0, 2);
    var compFin = fecFinFormateada;

    //comparamos las fechas
    if(compFin < compIni){
        alert(mensajeError);
        return false;
    }
    return true;
}
//jwong para validar que la fecha inicial no sea mayor que una fecha final
function validarFechaMenorConReferencia(fecIni, fecIniRefFormateada, mensajeError){
    var compIni = fecIni.substr(6, 4) + fecIni.substr(3, 2) + fecIni.substr(0, 2);
    var compFin = fecIniRefFormateada;

    //comparamos las fechas
    if(compIni < compFin){
        alert(mensajeError);
        return false;
    }
    return true;
}
function validarFechasRelacionadas2(fecIni, fecFin){
    //alert("Entra 1 =>>" + fecIni + " ; fecFin=>>" + fecFin + " ; fecActual=>>" + fecActual);
    var compIni = fecIni.substr(6, 4) + fecIni.substr(3, 2) + fecIni.substr(0, 2);
    var compFin = fecFin.substr(6, 4) + fecFin.substr(3, 2) + fecFin.substr(0, 2);

    //alert("Entra 2");

    //comparamos las fechas
    if(compFin < compIni){
        alert("Fecha desde no puede ser mayor que fecha hasta");
        return false;
    }
    return true;
}

//jwong 30/01/2009 para manejo de impresion/exportacion
/**
 * fecIni, fecha inicio (dd/MM/yyyy)
 * fecFin, fecha fin (dd/MM/yyyy)
 * fecActual, fecha actual (yyyyMMdd)
 */
function exportar(url, accion, formato){
    //alert("entra antes de enviar a descarga ==>> " + url + "; accion ==>> " + accion + "; formato ==>> " + formato);
    if( accion == 'print'){
        var ventimp = window.open(url + "&accion=" + accion, "export", "");
        ventimp.focus();
    }
    else if( accion == 'save' ){
        var frm = document.forms[0];
        frm.action = url + "&accion=" + accion + "&formato="+formato;
        frm.submit();
    }
}

//jwong 20/02/2009 para validar fechas relacionadas(inicio/fin)
/**
 * fecFin, fecha fin (dd/MM/yyyy)
 * fecActual, fecha actual (yyyyMMdd)
 */
function validarFechaConActual(fecFin, fecActual){
    var compFin = fecFin.substr(6, 4) + fecFin.substr(3, 2) + fecFin.substr(0, 2);
    if(compFin > fecActual){
        alert("Fecha hasta no debe ser mayor que la fecha actual");
        return false;
    }
    return true;
}


function validarFechaConActual2(fecFin, fecActual){
    var compFin = fecFin.substr(6, 4) + fecFin.substr(3, 2) + fecFin.substr(0, 2);
    if(compFin < fecActual){
        alert("La Fecha de Vencimiento no debe ser menor que la fecha actual");
        return false;
    }
    return true;
}


//jwong 24/02/2009 validacion de formato numerico
function numero(){
  if((event.keyCode<48) || (event.keyCode>57))event.keyCode = 0;
}
function val_int(o){
  o.value=o.value.toString().replace(/([^0-9])/g,"");
}

function valdecimal_int(o){
  o.value=o.value.toString().replace(/([^0-9.])/g,"");
}

//jwong 24/02/2009 validacion de caracteres alfanumericos ingresados
function abc(campo){
  campo.value=campo.value.toString().replace(/([^A-Z a-z0-9_])/g,"");
}
function alphabetic(e) {
  tecla = (document.all) ? e.keyCode : e.which;

  //Tecla de retroceso para borrar, siempre la permite
  if (tecla==8) return true;

  // Patron de entrada, en este caso solo acepta letras
  patron = /[A-Z a-z0-9_]/;

  tecla_final = String.fromCharCode(tecla);
  return patron.test(tecla_final);
}

//jwong 25/02/2009 validacion de numeros con 2 decimales
function val_decimal(e) {
  tecla = (document.all) ? e.keyCode : e.which;

  //Tecla de retroceso para borrar, siempre la permite
  if (tecla==8) return true;

  // Patron de entrada, en este caso solo acepta letras
  patron = /[0-9.]/;

  tecla_final = String.fromCharCode(tecla);
  return patron.test(tecla_final);
}

function valida_dec(str){
    str = alltrim(str);
    return /^[0-9]+(\.[0-9]+)?$/.test(str);
}
function alltrim(str) {
  return str.replace(/^\s+|\s+$/g, '');
}

//kinzi - adicionales
//Alfanumerico & '_' & '.' & '_'
function soloDescripcion() {
    var code = window.event.keyCode;
    patron = /[0-9A-Z a-z��������������\_.-]/;
    te = String.fromCharCode(code);
    //alert(patron.test(te));
    if (patron.test(te) == false) {
        window.event.keyCode = 0;
    }            
}
//Alfanumerico & '_' & '.' & '_'
function gDescripcion(campo){
    campo.value=campo.value.toString().replace(/^\s+|\s+$/g,"");//jmoreno 02-07-09
    campo.value=campo.value.toString().replace(/([^0-9A-Z a-z��������������_.\-])/g,"");
}
function gIATA(campo) {
        //alert( '' + campo.value.toString());
        //campo.value = campo.value.toString().replace(/^\s+|\s+$/g, ""); //jmoreno 02-07-09
        campo.value = campo.value.toString().replace(/([^A-Za-z0-9])/g, "");
}
function soloIATA(campo, event) {
    var code = window.event.keyCode;
    if (code == 8) return true;
    patron = /[0-9A-Za-z]/;
    te = String.fromCharCode(code);
    //alert(patron.test(te));
    if (patron.test(te) == false) {
        window.event.keyCode = 0;
    }
}

function gOACI(campo) {
    //alert( '' + campo.value.toString());
    //campo.value = campo.value.toString().replace(/^\s+|\s+$/g, ""); //jmoreno 02-07-09
    campo.value = campo.value.toString().replace(/([^A-Za-z])/g, "");
}
function soloOACI(campo, event) {
    var code = window.event.keyCode;
    if (code == 8) return true;
    patron = /[A-Za-z]/;
    te = String.fromCharCode(code);
    //alert(patron.test(te));
    if (patron.test(te) == false) {
        window.event.keyCode = 0;
    }
}


//Todos menos caracteres especiales, - . _
function gDescripcionNombre(campo){ 
    campo.value=campo.value.toString().replace(/^\s+|\s+$/g,"");//jmoreno 02-07-09
    campo.value=campo.value.toString().replace(/([^A-Z a-z��������������])/g,"");    
}
function soloDescripcionNombre(campo, event) {
    
    var code = window.event.keyCode;
    patron =/[A-Z a-z��������������]/;    
    te = String.fromCharCode(code);
    return patron.test(te);
}



//Numeros Decimales
function gDecimal(campo){
    campo.value=campo.value.toString().replace(/([^0-9.])/g,"");
}
function soloDecimal(campo, event) {
    var code = window.event.keyCode
    patron =/[0-9.]/;
    te = String.fromCharCode(code);
    return patron.test(te);
}
//Hora
function gHora(campo){
    campo.value=campo.value.toString().replace(/([^0-9:])/g,"");
}
function soloHora(campo, event) {
    var code = window.event.keyCode
    patron =/[0-9:]/;
    te = String.fromCharCode(code);
    return patron.test(te);
}

//email
function val_email(campo){
  campo.value=campo.value.toString().replace(/([^A-Za-z0-9_@])/g,"");
}

//jwong 24/02/2009 validacion de caracteres alfanumericos ingresados
function abcSinEspacio(campo){
  campo.value=campo.value.toString().replace(/([^A-Za-z0-9_])/g,"");
}
function alphabeticSinEspacio(e) {
  tecla = (document.all) ? e.keyCode : e.which;

  //Tecla de retroceso para borrar, siempre la permite
  if (tecla==8) return true;

  // Patron de entrada, en este caso solo acepta letras
  patron = /[A-Za-z0-9_]/;

  tecla_final = String.fromCharCode(tecla);
  return patron.test(tecla_final);
}

//jwong 08/05/2009 para validar fecha actual
/**
 * fecFin, fecha fin (dd/MM/yyyy)
 * fecActual, fecha actual (yyyyMMdd)
 */
function validarFechaConActual2(fecFin, fecActual){
    var compFin = fecFin.substr(6, 4) + fecFin.substr(3, 2) + fecFin.substr(0, 2);
    if(compFin > fecActual){
        alert("La fecha a consultar no debe ser mayor que la fecha actual");
        return false;
    }
    return true;
}

//jwong 12/05/2009 para validar el formato decimal de dos digitos
function valida_decimal2digitos(campo){
    if(campo.value!=""){
        if(!valida_dec(campo.value)){
            alert("Formato de monto incorrecto");
            campo.select();
            campo.focus();
        }
        else{
            var  indic = campo.value.indexOf(".", 0);
            var indfin = campo.value.length;
            if(indic>-1){ //si encuentra el punto decima valida qe solo tenga dos decimales
                var cadena = campo.value.substring(indic+1, indfin);
                if(cadena.length>2){
                    alert("El monto solo puede tener 2 decimales");
                    campo.select();
                    campo.focus();
                }
            }
        }
    }
}

//jwong 12/05/2009 para validar el formato decimal de dos digitos
function isdecimal2digitos(campo){
    if(campo.value!=""){
        if(!valida_dec(campo.value)){
            alert("Formato de monto incorrecto");
            campo.select();
            campo.focus();
            return false;
        }
        else{
            var  indic = campo.value.indexOf(".", 0);
            var indfin = campo.value.length;
            if(indic>-1){ //si encuentra el punto decima valida qe solo tenga dos decimales
                var cadena = campo.value.substring(indic+1, indfin);
                if(cadena.length>2){
                    alert("El monto solo puede tener 2 decimales");
                    campo.select();
                    campo.focus();
                    return false;
                }
            }
        }
    }
    return true;
}


//gchavez Funcion para Treeview

function OnCheckBoxCheckChanged(evt) {
		var src = window.event != window.undefined ? window.event.srcElement : evt.target;               
		var isChkBoxClick = (src.tagName.toLowerCase() == "input" && src.type == "checkbox");         
		if (isChkBoxClick) {   
			var parentTable = GetParentByTagName("table", src);         
			var nxtSibling = parentTable.nextSibling;
 
 if (nxtSibling && nxtSibling.nodeType == 1)//check if nxtsibling is not null & is an element node       
	{ 
		if (nxtSibling.tagName.toLowerCase() == "div") //if node has children       
		{   
			//check or uncheck children at all levels     
			CheckUncheckChildren(parentTable.nextSibling, src.checked);     
		}       
	} //check or uncheck parents at all levels    
		CheckUncheckParents(src, src.checked);   
	} 
 }    
 
 
 function CheckUncheckChildren(childContainer, check) {       
	var childChkBoxes = childContainer.getElementsByTagName("input");      
	var childChkBoxCount = childChkBoxes.length;   
	for (var i = 0; i < childChkBoxCount; i++) {  
		childChkBoxes[i].checked = check;   
	}  
  }       

  function CheckUncheckParents(srcChild, check) {      
	var parentDiv = GetParentByTagName("div", srcChild);    
	var parentNodeTable = parentDiv.previousSibling;      

	if (parentNodeTable) {
		var checkUncheckSwitch;   

	//	if (check) //checkbox checked    
	//	{   
	//		var isAllSiblingsChecked = AreAllSiblingsChecked(srcChild);   
	//		if (isAllSiblingsChecked)   
	//			checkUncheckSwitch = true; 
	//		else  
	//			return; //do not need to check parent if any(one or more) child not checked    
	//	}
	//	else //checkbox unchecked  
	//	{     
	//		//gerardo 
	//		//checkUncheckSwitch = false; 
	//		checkUncheckSwitch = true; 
	//	} 
        checkUncheckSwitch = true; 

		var inpElemsInParentTable = parentNodeTable.getElementsByTagName("input");   
		if (inpElemsInParentTable.length > 0) { 
			var parentNodeChkBox = inpElemsInParentTable[0]; 
			parentNodeChkBox.checked = checkUncheckSwitch;      
			//do the same recursively    
			CheckUncheckParents(parentNodeChkBox, checkUncheckSwitch);   
		}   
	}
 }                
 
 function AreAllSiblingsChecked(chkBox) {
	var parentDiv = GetParentByTagName("div", chkBox);     
	var childCount = parentDiv.childNodes.length;     
	for (var i = 0; i < childCount; i++) {   
		if (parentDiv.childNodes[i].nodeType == 1) //check if the child node is an element node                   
		{  
			if (parentDiv.childNodes[i].tagName.toLowerCase() == "table") {
				var prevChkBox = parentDiv.childNodes[i].getElementsByTagName("input")[0];     
				//if any of sibling nodes are not checked, return false
				if (!prevChkBox.checked) {  
				return false;  
				}
			}
		}
	} 
	return true; 
 }      
   
  //utility function to get the container of an element by tagname       
  function GetParentByTagName(parentTagName, childElementObj) {   
	var parent = childElementObj.parentNode;       
	while (parent.tagName.toLowerCase() != parentTagName.toLowerCase()) {     
		parent = parent.parentNode;     
		} 
			return parent;  
		}  


  function FnVolver(){
    history.back(-1);}


   var banderita= true;    
   var etiqueta="";
    
    function IniEnvio(id){
        if (id!=null)
        {
            if(banderita)
            {
                etiqueta=document.getElementById(id).value;
                document.getElementById(id).value="Procesando";
                banderita=false;
                return true;
            }
            return false;
        }
    }
    
    function FinEnvio(id){
        if(id!=null)
        {
            banderita=true;
           document.getElementById(id).value=etiqueta;
            return true;
        }
    }
    
    function valLength(control){
    
		if(control.value.length >= 3){
			window.event.keyCode = 0;
		}
    
    }
	//xxxxxxx
	function SetCheckBoxBoarding(control, ctrlTarget, ctrlList) {
        if (control.checked) {
            var cbk_Ticket = document.getElementById(ctrlTarget);
            if (!(cbk_Ticket.checked)) {
                document.getElementById(ctrlList).disabled = true;
            }
        } else {
            document.getElementById(ctrlList).disabled = false;
        }
    }
	function SetCheckBoxTicket(control, ctrlTarget, ctrlList) {
        if (control.checked) {
            document.getElementById(ctrlList).disabled = false;
        } else {
            var cbk_Boarding = document.getElementById(ctrlTarget);
            if (cbk_Boarding.checked) {
                document.getElementById(ctrlList).disabled = true;
            } else {
                document.getElementById(ctrlList).disabled = false;
            }
        }
    } 
	
 
</script> 