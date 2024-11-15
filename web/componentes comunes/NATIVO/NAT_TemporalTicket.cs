using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using LAP.TUUA.ENTIDADES;
using LAP.TUUA.DAO;
using LAP.TUUA.UTIL;
using LAP.TUUA.CONEXION;

namespace LAP.TUUA.NATIVO
{
    public class NAT_TemporalTicket : ITemporalTicket
    {
        protected string Dsc_PathSPConfig;
        DAO_TemporalTicket objDAOTemporalTicket;

        public NAT_TemporalTicket()
        {
            Dsc_PathSPConfig = (string)Property.htProperty["PATHRECURSOS"];
            objDAOTemporalTicket = new DAO_TemporalTicket(Dsc_PathSPConfig);
        }

        public override int Ingresar(TemporalTicket objTemporalTicket)
        {
            try
            {
                return objDAOTemporalTicket.Ingresar(objTemporalTicket);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }

        public override void Eliminar(TemporalTicket objTemporalTicket)
        {
            try
            {
                objDAOTemporalTicket.Eliminar(objTemporalTicket);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }

        public override DataTable ListarAll(TemporalTicket objTemporalTicket)
        {
            try
            {
                return objDAOTemporalTicket.ListarAll(objTemporalTicket);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }

        public override bool insertarRehabilitacionTicket(TicketEstHist objTicketEstHist, int flag, int sizeOutput)
        {
            try
            {
                return objDAOTemporalTicket.insertarRehabilitacionTicket(objTicketEstHist, flag, sizeOutput);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }
    }
}
