/*
Sistema		 :   TUUA
Aplicación	 :   Administracion
Objetivo		 :   Proceso de gestión de Administración.
Especificaciones:   Se considera aquellas marcaciones según el rango programado.
Fecha Creacion	 :   11/07/2009	
Programador	 :	GCHAVEZ
Observaciones:	
*/
using System;
using System.Collections.Generic;
using System.Text;
using LAP.TUUA.ENTIDADES;
using LAP.TUUA.CONEXION;
using LAP.TUUA.RESOLVER;
using LAP.TUUA.UTIL;
using System.Collections;
using System.Data;

namespace LAP.TUUA.CONTROL
{
    public class BO_TemporalTicket
    {

        private ITemporalTicket oTemporalTicket;

        public BO_TemporalTicket(string keyClass)
        {
            oTemporalTicket = (ITemporalTicket)Resolver.ObtenerConexionObject(keyClass);
        }

        public int Ingresar(TemporalTicket objTemporalTicket)
        {
            return oTemporalTicket.Ingresar(objTemporalTicket);
        }

        public DataTable ListarAll(TemporalTicket objTemporalTicket)
        {
            return oTemporalTicket.ListarAll(objTemporalTicket);
        }

        public bool Eliminar(TemporalTicket objTemporalTicket, int flag, int sizeOutput)
        {
            foreach (TemporalTicket item in objTemporalTicket.TemporalTicketLis)
            {
                oTemporalTicket.Eliminar(item);    
            }
            TicketEstHist objTicketEstHist = new TicketEstHist();
            objTicketEstHist.SCodNumeroTicket = objTemporalTicket.SCodNumeroTicket;
            objTicketEstHist.SCausalRehabilitacion = objTemporalTicket.SCausalRehabilitacion;
            objTicketEstHist.SDscNumVuelo = objTemporalTicket.SDscNumVuelo;
            objTicketEstHist.SLogUsuarioMod = objTemporalTicket.SLogUsuarioMod;
            oTemporalTicket.insertarRehabilitacionTicket(objTicketEstHist, flag, sizeOutput);
            return true;
        }
    }
}
