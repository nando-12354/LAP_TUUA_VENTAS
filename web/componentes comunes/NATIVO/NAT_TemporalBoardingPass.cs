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
    public class NAT_TemporalBoardingPass : ITemporalBoardingPass
    {
        protected string Dsc_PathSPConfig;
        DAO_TemporalBoardingPass objDAOTemporalBoardingPass;

        public NAT_TemporalBoardingPass()
        {
            Dsc_PathSPConfig = (string)Property.htProperty["PATHRECURSOS"];
            objDAOTemporalBoardingPass = new DAO_TemporalBoardingPass(Dsc_PathSPConfig);
        }

        public override int Ingresar(TemporalBoardingPass objTemporalBoardingPass)
        {
            try
            {
                return objDAOTemporalBoardingPass.Ingresar(objTemporalBoardingPass);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }

        public override void Eliminar(TemporalBoardingPass objTemporalBoardingPass)
        {
            try
            {
                objDAOTemporalBoardingPass.Eliminar(objTemporalBoardingPass);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }

        public override DataTable ListarAll(TemporalBoardingPass objTemporalBoardingPass)
        {
            try
            {
                return objDAOTemporalBoardingPass.ListarAll(objTemporalBoardingPass);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }
        public override bool insertarRehabilitacionBCBP(BoardingBcbpEstHist boardingBcbpEstHist, int flag, int sizeOutput)
        {
            try
            {
                return objDAOTemporalBoardingPass.insertarRehabilitacionBCBP(boardingBcbpEstHist, flag, sizeOutput);
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
