using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using LAP.TUUA.CONEXION;
using LAP.TUUA.RESOLVER;
using LAP.TUUA.UTIL;
using LAP.TUUA.ENTIDADES;

namespace LAP.TUUA.CONTROL
{
    public class BO_Rehabilitacion
    {
        protected Conexion objCnxRehabilitacion;

        public BO_Rehabilitacion()
        {
            objCnxRehabilitacion = Resolver.ObtenerConexion(Define.CNX_08);
        }

        public BO_Rehabilitacion(string strUsuario, string strModulo, string strSubModulo)
        {
            objCnxRehabilitacion = Resolver.ObtenerConexion(Define.CNX_08);
            objCnxRehabilitacion.Cod_Usuario = strUsuario;
            objCnxRehabilitacion.Cod_Modulo = strModulo;
            objCnxRehabilitacion.Cod_Sub_Modulo = strSubModulo;
        }

        public DataTable ConsultarRepresXRehabilitacionYCia(string strCia)
        {
            return objCnxRehabilitacion.ConsultarRepresXRehabilitacionYCia(strCia);
        }

        public bool registrarRehabilitacionTicket(TicketEstHist objTicketEstHist, int flag, int sizeOutput)
        {
            return objCnxRehabilitacion.registrarRehabilitacionTicket(objTicketEstHist, flag, sizeOutput);
        }

        public DataTable consultarVuelosTicketPorCiaFecha(string sCompania, string fechaVuelo)
        {
            return objCnxRehabilitacion.consultarVuelosTicketPorCiaFecha(sCompania, fechaVuelo);

        }

        public DataTable consultarTicketsPorVuelo(string sCompania, string fechaVuelo, string dsc_Num_Vuelo)
        {
            return objCnxRehabilitacion.consultarTicketsPorVuelo(sCompania, fechaVuelo, dsc_Num_Vuelo);

        }

        public DataTable consultarVuelosBCBPPorCiaFecha(string sCompania, string fechaVuelo)
        {
            return objCnxRehabilitacion.consultarVuelosBCBPPorCiaFecha(sCompania, fechaVuelo);

        }

        public bool registrarRehabilitacionBCBP(BoardingBcbpEstHist boardingBcbpEstHist, int flag, int sizeOutput)
        {
            return objCnxRehabilitacion.registrarRehabilitacionBCBP(boardingBcbpEstHist, flag, sizeOutput);
        }

        public bool registrarRehabilitacionBCBPAmpliacion(BoardingBcbpEstHist boardingBcbpEstHist, int flag, int sizeOutput)
        {
            return objCnxRehabilitacion.registrarRehabilitacionBCBPAmpliacion(boardingBcbpEstHist, flag, sizeOutput);
        }

        public DataTable listarCompania_xCodigoEspecial(String codigoEspecial)
        {
            return objCnxRehabilitacion.listarCompania_xCodigoEspecial(codigoEspecial);

        }

        public DataTable obteneterBoardingsByRangoFechas(string sCompania, string sFchDesde, string sFchHasta, string sHoraDesde, string sHoraHasta)
        {
            return objCnxRehabilitacion.obteneterBoardingsByRangoFechas(sCompania, sFchDesde, sFchHasta, sHoraDesde, sHoraHasta);

        }

    }
}
