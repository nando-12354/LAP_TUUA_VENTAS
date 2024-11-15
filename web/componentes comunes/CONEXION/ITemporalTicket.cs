using System;
using System.Collections.Generic;
using System.Text;
using LAP.TUUA.ENTIDADES;
using System.Data;

namespace LAP.TUUA.CONEXION
{
    public abstract class ITemporalTicket
    {
        #region G&S
        public abstract int Ingresar(TemporalTicket objTemporalTicket);
        public abstract void Eliminar(TemporalTicket objTemporalTicket);
        public abstract DataTable ListarAll(TemporalTicket objTemporalTicket);
        public abstract bool insertarRehabilitacionTicket(TicketEstHist objTicketEstHist, int flag, int sizeOutput);
        #endregion
    }
}
