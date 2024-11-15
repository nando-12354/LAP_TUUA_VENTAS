//Función que oculta el Frame del índice al presionar el botón de la flecha
function OcultarIndice()
{
	var oculto = '0px,*';
	var botonFlecha = document.getElementById('flechita');
	
	if (oculto == parent.document.getElementById('inferior').cols)
	{
		parent.document.getElementById('inferior').cols='221px,*';
		botonFlecha.src="./images/boton_contraer.gif";
		botonFlecha.alt="Ocultar Índice";
	} 
	else 
	{
		parent.document.getElementById('inferior').cols='0px,*';
		botonFlecha.src="./images/boton_expandir.gif";
		botonFlecha.alt="Mostrar Índice";
	}
} 

function CambiarTitulo(nuevoTitulo)
{
	parent.mainFrame.getElementById('titulo').innerHTML = nuevoTitulo
//	parent.document.getElementById('titulo').innerHTML = nuevoTitulo;
}

function resize_iframe()
{

	var height=window.innerWidth;//Firefox
	if (document.body.clientHeight)
	{
		height=document.body.clientHeight;//IE
	}
	//resize the iframe according to the size of the
	//window (all these should be on the same line)
	
	document.getElementById("contenido").style.height=parseInt(height-
	document.getElementById("contenido").offsetTop-38)+"px";
}

function resize_iframeIndice()
{

	var height=window.innerWidth;//Firefox
	if (document.body.clientHeight)
	{
		height=document.body.clientHeight;//IE
	}
	//resize the iframe according to the size of the
	//window (all these should be on the same line)
	
	document.getElementById("ind").style.height=parseInt(height-
	document.getElementById("ind").offsetTop-56)+"px";
	
}

// this will resize the iframe every
// time you change the size of the window.
window.onresize=resize_iframe; 