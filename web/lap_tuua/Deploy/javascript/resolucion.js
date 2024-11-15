/**
* Verificacion de Resolucion de Pantalla
*
* Soporte de resoluciones 1280 x 1024 y 1024 x 768
*
*/
var docWidth = document.body.clientWidth;
var docHeight = document.body.clientHeight;
var keypage = document.getElementsByName('keypage')[0].getAttribute('content');

var docH = docHeight;

document.getElementById("divData").style.width = docWidth;

//Resolution 1280 x 1024
if (screen.height == 1024) {
    switch (keypage) {
        case 'cns_usuarios': docH -= 230; break;
        case 'cns_companias': docH -= 230; break;
        case 'cns_detalleticket': docH -= 310; break;
        case 'cns_turnos': docH -= 290; break;
        case 'cns_ticketxfecha': docH -= 290; break;
        case 'cns_cuadreticketemit': docH -= 255; break;
        case 'cns_compraventa': docH -= 255; break;
        case 'cns_boardingusados': docH -= 290; break;
        case 'cns_stickersanulados': docH -= 255; break;
        case 'cns_auditoria': docH -= 290; break;
        case 'cns_ticketproc': docH -= 300; break;
        case 'rpt_liquidticket': docH -= 250; break;
        //resumen diario 
        //mov_ticket_conti
        case 'rpt_stockticket': docH -= 230; break;
        case 'rpt_detlineavuelo': docH -= 250; break;
        case 'rpt_boardingmol': docH -= 280; break; 
        //resumen_masiva_credito
        case 'rpt_rescompania': docH -= 250; break;
        //ticket boarding media hora dia mes
        case 'rpt_ticketusadosmhh': docH -= 250; break;
        //ticket dia mes
        case 'rpt_ticketusadosddmm': docH -= 310; break;
        case 'rpt_recmensual': docH -= 230; break;
        case 'rpt_liquidventa': docH -= 250; break;
        //ticket vencidos
        case 'cns_ticketvenc': docH -= 250; break;            
        case 'rpt_cuadreticketvend': docH -= 250; break;
        case 'rpt_ticketrehab': docH -= 280; break;
        //resumen turno

        case 'hlp_ayuda': docH -= 190; break;
    }    
}
//Resolution 1024 x 768
if (screen.height == 768) {
    switch (keypage) {
        case 'cns_usuarios': docH -= 210; break;
        case 'cns_companias': docH -= 210; break;
        case 'cns_detalleticket': docH -= 305; break;
        case 'cns_turnos': docH -= 270; break;
        case 'cns_ticketxfecha': docH -= 270; break;
        case 'cns_cuadreticketemit': docH -= 230; break;
        case 'cns_compraventa': docH -= 230; break;
        case 'cns_boardingusados': docH -= 275; break;
        case 'cns_stickersanulados': docH -= 230; break;
        case 'cns_auditoria': docH -= 275; break;
        case 'cns_ticketproc': docH -= 275; break;
        case 'rpt_liquidticket': docH -= 230; break;
        //resumen diario  
        //mov_ticket_conti 
        case 'rpt_stockticket': docH -= 210; break;
        case 'rpt_detlineavuelo': docH -= 230; break;
        case 'rpt_boardingmol': docH -= 260; break;
        //resumen_masiva_credito 
        case 'rpt_rescompania': docH -= 230; break;
        //ticket boarding media hora dia mes
        case 'rpt_ticketusadosmhh': docH -= 250; break;
        //ticket dia mes
        case 'rpt_ticketusadosddmm': docH -= 250; break;
        case 'rpt_recmensual': docH -= 210; break;
        case 'rpt_liquidventa': docH -= 230; break;
        //ticket vencidos
        case 'cns_ticketvenc': docH -= 250; break; 
        case 'rpt_cuadreticketvend': docH -= 230; break;
        case 'rpt_ticketrehab': docH -= 255; break;
        //resumen turno

        case 'hlp_ayuda': docH -= 280; break;
    }    
}
document.getElementById("divData").style.height = docH;