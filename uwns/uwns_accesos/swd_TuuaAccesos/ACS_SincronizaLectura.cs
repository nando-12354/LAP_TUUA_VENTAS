///File: ACS_SincronizaLectura.cs
///Proposito:Sincroniza el proceso de  Lector y PINPAD
///Metodos: 
///Version:1.0
///Creado por:Ramiro Salinas
///Fecha de Creación:15/07/2009
///Modificado por: Ramiro Salinas
///Fecha de Modificación:20/07/2009


using System;
using System.Collections.Generic;
using System.Text;

namespace LAP.TUUA.ACCESOS
{
      /// <summary>
      /// ACCESOS: Sincroniza Lectura 
      /// </summary>
      class ACS_SincronizaLectura
      {
            private Boolean flg_LecturaScaner;

            public ACS_SincronizaLectura(){
                  flg_LecturaScaner = true;
            }



            public Boolean Flg_LecturaScaner
            {
                  get
                  {
                        return flg_LecturaScaner;
                  }
                  set
                  {
                        flg_LecturaScaner = value;
                  }
            }

      }
}
