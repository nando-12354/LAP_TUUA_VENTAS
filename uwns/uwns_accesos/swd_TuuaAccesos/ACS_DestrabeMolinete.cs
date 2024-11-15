using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAP.TUUA.ACCESOS
{
      class ACS_DestrabeMolinete
      {

            private ACS_Util Obj_Util;
            private ACS_Resolver Obj_Resolver;
            private LAP.TUUA.AccesoMolinete.MolineteDiscapacitados Obj_MolineteDiscapacitados;
            private LAP.TUUA.AccesoMolinete.Molinete Obj_Molinete;
            public string Cod_Acceso;
            public string Cod_Web;
            public string Cod_Molinete;

            public ACS_DestrabeMolinete(ACS_Util Obj_Util, LAP.TUUA.AccesoMolinete.Molinete Obj_Molinete, 
                                        LAP.TUUA.AccesoMolinete.MolineteDiscapacitados Obj_MolineteDiscapacitados,
                                        ACS_Resolver Obj_Resolver)
            {
                  this.Obj_Util = Obj_Util;
                  this.Obj_Molinete = Obj_Molinete;
                  this.Obj_MolineteDiscapacitados = Obj_MolineteDiscapacitados;
                  this.Obj_Resolver = Obj_Resolver;
            }

            public void DestrabeMolinete()
            {
                  //*****************ENCENDER LUZ SEMAFORO*******************//
                  bool bRptaMolinete = true;
                  if (ACS_Property.estadoMolinete)
                  {
                        if (((string)ACS_Property.shtMolinete["Tip_Molinete"]).ToUpper().Equals("NORMAL"))
                              bRptaMolinete = Obj_Molinete.MolineteAprobadoNormal();
                        if (((string)ACS_Property.shtMolinete["Tip_Molinete"]).ToUpper().Equals("DISCAPACITADO"))
                        {
                            //Obj_MolineteDiscapacitados.IsEstadoOpen = true;
                            //LAP.TUUA.UTIL.Define.FLG_GATE_OPEN = "1";
                            //Obj_Util.EscribirLog(this, "Valores Seteados; Bool: " + Obj_MolineteDiscapacitados.IsEstadoOpen.ToString() + ", Cadena:" + LAP.TUUA.UTIL.Define.FLG_GATE_OPEN);
                            Obj_MolineteDiscapacitados.MolineteDiscapacitadosAprobadoNormal();
                            bRptaMolinete = true;
                        }
                  }
                  //estado = 1;
                  Obj_Util.EscribirLog(this, "Abrir Molinete");
                  Obj_Util.EscribirLog(this, "Cerrar Molinete");
            }


            public void CierreForzadoMolinete()
            {
                Obj_Util.EscribirLog(this, "Inicia Cierre de Molinete Forzado.");
                String sLoger = Obj_MolineteDiscapacitados.CierreMolineteDiscapacitado();
                //Obj_Util.EscribirLog(this, "CLOSE MOLINETE - RESULTADO: " + sLoger);
                Obj_Util.EscribirLog(this, "Finaliza Cierre de Molinete Forzado.");
            }
      }
}



