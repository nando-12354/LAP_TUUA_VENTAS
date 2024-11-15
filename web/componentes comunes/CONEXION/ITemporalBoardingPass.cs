using System;
using System.Collections.Generic;
using System.Text;
using LAP.TUUA.ENTIDADES;
using System.Data;

namespace LAP.TUUA.CONEXION
{
    public abstract class ITemporalBoardingPass
    {
        #region G&S
        public abstract int Ingresar(TemporalBoardingPass objTemporalBoardingPass);
        public abstract void Eliminar(TemporalBoardingPass objTemporalBoardingPass);
        public abstract DataTable ListarAll(TemporalBoardingPass objTemporalBoardingPass);
        public abstract bool insertarRehabilitacionBCBP(BoardingBcbpEstHist boardingBcbpEstHist, int flag, int sizeOutput);
        #endregion
    }
}
