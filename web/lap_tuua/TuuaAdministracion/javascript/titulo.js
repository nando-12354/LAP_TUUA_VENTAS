//Hemi: Me deben 2 días enteros y 1 amanecida para hacer que funque esta función...
//¡¡No me vuelvan a cambiar el nombre de los Frames!! >_<
//Probado en Firefox y Exploider
function CambiarTitulo(nuevoTitulo)
{
	//window.parent.frames['mainFrame'].document.getElementById('hemi').innerHTML = 'hemito';	
	if (window.name == 'leftFrame')
	{
		window.parent.frames['mainFrame'].document.getElementById('titulo').innerHTML = nuevoTitulo;
	}
	else if (window.name == 'contenido')
	{
		window.parent.document.getElementById('titulo').innerHTML = nuevoTitulo;
	}
}

function CambiarTituloPrincipal(nuevoTitulo)
{
	window.parent.document.getElementById('titulo').innerHTML = nuevoTitulo;
}

function checkWin()
{
  	document.write(window.name)
}

