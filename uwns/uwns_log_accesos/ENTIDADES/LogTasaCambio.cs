using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAP.TUUA.ENTIDADES
{
      public class LogTasaCambio
      {
            #region Fields
            private int iNumSecuencial;
            private string sCodMoneda;
            private decimal dImpCompra;
            private decimal dImpVenta;
            private string sLogUsuarioMod;
            private string sLogFechaMod;
            private string sLogHoraMod;
            #endregion

            #region Constructors
            public LogTasaCambio() 
            {

            }
            #endregion

            #region Properties
            public string SCodMoneda
            {
                  get { return sCodMoneda; }
                  set { sCodMoneda = value; }
            }

            public int INumSecuencial
            {
                  get { return iNumSecuencial; }
                  set { iNumSecuencial = value; }
            }

            public decimal DImpCompra
            {
                  get { return dImpCompra; }
                  set { dImpCompra = value; }
            }
            public decimal DImpVenta
            {
                  get { return dImpVenta; }
                  set { dImpVenta = value; }
            }

            public string SLogUsuarioMod
            {
                  get { return sLogUsuarioMod; }
                  set { sLogUsuarioMod = value; }
            }

            public string SLogFechaMod
            {
                  get { return sLogFechaMod; }
                  set { sLogFechaMod = value; }
            }

            public string SLogHoraMod
            {
                  get { return sLogHoraMod; }
                  set { sLogHoraMod = value; }
            }
            #endregion
      }
}
