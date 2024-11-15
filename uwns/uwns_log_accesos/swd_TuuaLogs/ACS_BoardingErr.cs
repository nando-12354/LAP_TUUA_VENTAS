
using System;
using System.Collections.Generic;
using System.Text;
using LAP.TUUA.ENTIDADES;
using LAP.TUUA.DAO;
using LAP.TUUA.LOGS;
using System.Collections;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using System.Diagnostics;
using LAP.TUUA.CONVERSOR;
using LAP.TUUA.UTIL;
using System.Net;

namespace LAP.TUUA.LOGS
{
    class ACS_BoardingErr
    {
        #region Fields
        private Hashtable Ht_Boarding;
        private List<ListaDeCampo> Lst_ListaDeCampo;
        private List<Compania> Lst_Compania;
        private BoardingBcbpErr Obj_BoardingBcbpErr;
        //private ACS_InterfazPinPad Obj_IntzPinPad;
        private ACS_Util Obj_Util;
        public int estado = 0;
        private ACS_Resolver Obj_Resolver;
        public Usuario Obj_Usuario;
        public Hashtable HT_AirlineUse;
        //private LAP.TUUA.AccesoMolinete.Molinete Obj_Molinete;
        //private LAP.TUUA.AccesoMolinete.MolineteDiscapacitados Obj_MolineteDiscapacitados;

        //private ACS_FormLogs frmLogs;
        //public Hashtable Lst_WSBcbp;
        //public TimeSpan Time_Start;
        #endregion


        public ACS_BoardingErr()
        {
        }



        /// <summary>
        /// Insertar registro de error
        /// </summary>
        /// <param name="Obj_BoardingBcbpErr"></param>
        public void InsertarError(BoardingBcbpErr Obj_BoardingBcbpErr)
        {
            try
            {
                Obj_Util.EscribirLog(this, "insert");
                Obj_Resolver.CrearDAOBoardingBcbpErr();
                Obj_Resolver.InsertarBoardingBcbpErr(Obj_BoardingBcbpErr);

                //Obj_Util.EscribirLog(this, "insert");
            }
            catch
            { }

        }
    }
}
