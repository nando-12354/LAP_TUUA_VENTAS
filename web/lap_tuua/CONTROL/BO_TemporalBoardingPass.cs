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
    public class BO_TemporalBoardingPass
    {

        private ITemporalBoardingPass oTemporalBoardingPass;

        public BO_TemporalBoardingPass(string keyClass)
        {
            oTemporalBoardingPass = (ITemporalBoardingPass)Resolver.ObtenerConexionObject(keyClass);
        }

        public int Ingresar(TemporalBoardingPass objTemporalBoardingPass)
        {
            return oTemporalBoardingPass.Ingresar(objTemporalBoardingPass);
        }

        public DataTable ListarAll(TemporalBoardingPass objTemporalBoardingPass)
        {
            return oTemporalBoardingPass.ListarAll(objTemporalBoardingPass);
        }

        public bool Eliminar(TemporalBoardingPass objTemporalBoardingPass, int flag, int sizeOutput)
        {
            foreach (TemporalBoardingPass item in objTemporalBoardingPass.TemporalBoardingPassLis)
            {
                oTemporalBoardingPass.Eliminar(item);
            }

            BoardingBcbpEstHist objBoardingBcbpEstHist = new BoardingBcbpEstHist();
            objBoardingBcbpEstHist.SListaBcbPs = objTemporalBoardingPass.SListaBCBPs;
            objBoardingBcbpEstHist.SCausalRehabilitacion = objTemporalBoardingPass.SCausalRehabilitacion;
            objBoardingBcbpEstHist.SLogUsuarioMod = objTemporalBoardingPass.SLogUsuarioMod;
            objBoardingBcbpEstHist.SCompaniaAsoc = objTemporalBoardingPass.SDesCompania;
            objBoardingBcbpEstHist.Lst_Bloqueados = objTemporalBoardingPass.LstBloqueados;
            objBoardingBcbpEstHist.Cod_Modulo_Mod = objTemporalBoardingPass.CodModulo;
            objBoardingBcbpEstHist.Cod_SubModulo_Mod = objTemporalBoardingPass.CodSubModulo;
            oTemporalBoardingPass.insertarRehabilitacionBCBP(objBoardingBcbpEstHist, flag, sizeOutput);
            return true;
        }
    }
}
