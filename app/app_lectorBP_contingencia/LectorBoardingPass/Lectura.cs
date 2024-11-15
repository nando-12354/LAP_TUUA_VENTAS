using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LectorBoardingPass
{
    public class Lectura
    {
        private int id;
        private string trama;
        private string fecha;
        private string tipoMolinete;
        private string tipoDocumento;
        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public string Trama
        {
            get
            {
                return trama;
            }

            set
            {
                trama = value;
            }
        }

        public String Fecha
        {
            get
            {
                return fecha;
            }

            set
            {
                fecha = value;
            }
        }

        public string TipoMolinete
        {
            get
            {
                return tipoMolinete;
            }

            set
            {
                tipoMolinete = value;
            }
        }

        public string TipoDocumento
        {
            get
            {
                return tipoDocumento;
            }

            set
            {
                tipoDocumento = value;
            }
        }
    }
}
