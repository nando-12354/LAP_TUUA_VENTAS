function formatoFecha(fecha){
    var dia = fecha.getDate();
    var mes = fecha.getMonth()+1;
    var anio = fecha.getFullYear();
    var hora = fecha.getHours();
    var minuto = fecha.getMinutes();
    var segundo = fecha.getSeconds();

    var strFecha = ("0"+dia.toString()).slice(-2)+"/"+("0"+mes.toString()).slice(-2)+"/"+anio.toString()+" "+("0"+hora.toString()).slice(-2)+":"+("0"+minuto.toString()).slice(-2)+":"+("0"+segundo.toString()).slice(-2);

    return strFecha;
}