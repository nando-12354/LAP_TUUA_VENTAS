/**
* COMMON DHTML FUNCTIONS
* These are handy functions I use all the time.
*
* By Seth Banks (webmaster at subimage dot com)
* http://www.subimage.com/
*
* Up to date code can be found at http://www.subimage.com/dhtml/
*
* This code is free for you to use anywhere, just keep this comment block.
*/

/**
* X-browser event handler attachment and detachment
* TH: Switched first true to false per http://www.onlinetools.org/articles/unobtrusivejavascript/chapter4.html
*
* @argument obj - the object to attach event to
* @argument evType - name of the event - DONT ADD "on", pass only "mouseover", etc
* @argument fn - function to call
*/

function addEvent(obj, evType, fn) {
    if (obj.addEventListener) {
        obj.addEventListener(evType, fn, false);
        return true;
    } else if (obj.attachEvent) {
        var r = obj.attachEvent("on" + evType, fn);
        return r;
    } else {
        return false;
    }
}
function removeEvent(obj, evType, fn, useCapture) {
    if (obj.removeEventListener) {
        obj.removeEventListener(evType, fn, useCapture);
        return true;
    } else if (obj.detachEvent) {
        var r = obj.detachEvent("on" + evType, fn);
        return r;
    } else {
        alert("Handler could not be removed");
    }
}

/**
* Code below taken from - http://www.evolt.org/article/document_body_doctype_switching_and_more/17/30655/
*
* Modified 4/22/04 to work with Opera/Moz (by webmaster at subimage dot com)
*
* Gets the full width/height because it's different for most browsers.
*/
function getViewportHeight() {
    if (window.innerHeight != window.undefined) return window.innerHeight;
    if (document.compatMode == 'CSS1Compat') return document.documentElement.clientHeight;
    if (document.body) return document.body.clientHeight;

    return window.undefined;
}
function getViewportWidth() {
    var offset = 17;
    var width = null;
    if (window.innerWidth != window.undefined) return window.innerWidth;
    if (document.compatMode == 'CSS1Compat') return document.documentElement.clientWidth;
    if (document.body) return document.body.clientWidth;
}

/**
* Gets the real scroll top
*/
function getScrollTop() {
    if (self.pageYOffset) // all except Explorer
    {
        return self.pageYOffset;
    }
    else if (document.documentElement && document.documentElement.scrollTop)
    // Explorer 6 Strict
    {
        return document.documentElement.scrollTop;
    }
    else if (document.body) // all other Explorers
    {
        return document.body.scrollTop;
    }
}
function getScrollLeft() {
    if (self.pageXOffset) // all except Explorer
    {
        return self.pageXOffset;
    }
    else if (document.documentElement && document.documentElement.scrollLeft)
    // Explorer 6 Strict
    {
        return document.documentElement.scrollLeft;
    }
    else if (document.body) // all other Explorers
    {
        return document.body.scrollLeft;
    }
}


///////////////////////////////////

function setearHidden(valor, formId) {
    document.getElementById(formId).value = valor;
}

function esTeclaEntero(e) {
    var valid = "0123456789";
    var key = String.fromCharCode(event.keyCode);
    if (valid.indexOf("" + key) == "-1") return false;
}
function esTeclaDecimal(e) {
    var valid = "0123456789.";
    var key = String.fromCharCode(event.keyCode);
    if (valid.indexOf("" + key) == "-1") return false;
}
function esTeclaTexto(e) {
    var valid = "abcdefghijklmñnopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ ";
    var key = String.fromCharCode(event.keyCode);
    if (valid.indexOf("" + key) == "-1") return false;
}
//********************************************************************************************//
function esTeclasTodas(e) {
    var valid = "abcdefghijklmñnopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ :.-/()#&0123456789_";
    var key = String.fromCharCode(event.keyCode);
    if (valid.indexOf("" + key) == "-1") return false;
}
function isInteger(s) {
    valor = parseInt(s)
    if (isNaN(valor)) {
        return false;
    } else {
        return true;
    }
}
function toPopup(form) {
    var form = document.getElementById(form);
    //deshabilitar inputs, checkbox y combobox
    for (i = 0; i < form.elements.length; i++) {
        form.elements[i].disabled = true;
    }
    //deshabilitar botones
    for (i = 0; i < form.elements.length; i++) {
        if (form.elements[i].type == 'submit') {
            form.elements[i].style.display = 'none';
        }
    }

}
function mostrarRechazo() {
    document.getElementById('rechazo').style.display = 'block';
    document.getElementById('aprobacion').style.display = 'none';
    return false;
}
/**
*funcion para validar de que el dato que se ingresa es numérico
*
**/
function isNumeric(o) {
    o.value = o.value.replace(/([^\x2E^0-9])/g, "");
}
/**
*funcion para validar de que el dato que se ingresa es alfanumérico
*
**/
function isName(o) {
    o.value = o.value.replace(/([^\xE1^\xE9^\xED^\xF3^\xFA^\xC1^\xC9^\xCD^\xD3^\xDA^\x27^a-zA-Z_ ])/g, "");
}
function isCodigo(o) {
    o.value = o.value.replace(/([^0-9])/g, "");
}

function obtnId(o) {
    alert(o.value);
}
function crearPopUp(nombre) {
    window.open('" & ' + nombre + ' & "');
}
function WindowC() {
    window.showModalDialog("../M_CxC/BuscarChequeWeb.aspx", "WinC", " dialogWidth:800px;dialogHeight:600px,toolbar=no,location=no,directories=no,status=no,menubar=no, scrollbars=no,resizable=yes,copyhistory=no")
}
function alertSeleccioneSoloUno() {
    alert("Seleccione solo un documento");
}
function sumar(id1, id2, o, pago, pagoH) {
    //alert(id1);
    //alert(id2);
    //alert(o);
    //alert(pago);
    //alert(pagoH);
    var d = document.getElementById(id2);
    var j = document.getElementById(pagoH);
    //alert(j);
    var pa = parseFloat(pago);
    var g = document.getElementById(o);

    o = o.replace(/([\x24])/g, "_");

    var h = document.getElementById(o);

    if (d.value == '') {
        d.value = 0;
    }
    id1 = id1.replace(/([\x2C])/g, ".");
    d.value = d.value.replace(/([\x2C])/g, ".");
    if (h.checked == true) {
        var valor = parseFloat(d.value) + parseFloat(id1);
        d.value = valor;
        if (valor > pa) {
            alert('El monto a cancelar excede al saldo disponible');
        }
    } else {
        var k = parseFloat(d.value) - parseFloat(id1);
        if (k > 0) {
            d.value = k
        } else {
            d.value = 0;
        }
    }
    d.value = d.value.replace(/([\x2E])/g, ",");
    j.value = d.value;


}