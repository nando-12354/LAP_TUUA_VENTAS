<script type="text/javascript"> 

var TimeOut=0;
var Time=0;

//Empieza a correr el tiempo....
function StartTime(tiempo)
{
Time = tiempo;

TimeOut = window.setTimeout('window.location = "login.aspx";',Time);
//alert('StarTime2='+Time);
}

//Detenemos el contador y empezamos a contar de nuevo,,,,
function StopAndStartTime()
{
//alert('Tiempo inicial:'+TimeOut);
//alert('El contador ha sido detenido y empieza a contar de nuevo');
window.clearTimeout(TimeOut);
StartTime(Time);
}
document.attachEvent('onclick', StopAndStartTime);
document.attachEvent('onkeypress', StopAndStartTime);
//document.attachEvent('onmousemove', StopAndStartTime);

</script> 