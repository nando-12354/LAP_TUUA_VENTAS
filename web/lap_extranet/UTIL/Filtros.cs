using System;
using System.Collections.Generic;
using System.Text;

namespace LAP.EXTRANET.UTIL
{
    [Serializable]
    public class Filtros
    {
        private string _nombre;
        private string _valor;

        public Filtros(string sNombre, string sValor)
        {
            this.Nombre = sNombre;
            this.Valor = sValor;
        }

        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        public string Valor
        {
            get { return _valor; }
            set { _valor = value; }
        }

    }
}
