using System;
using System.Collections.Generic;
using System.Text;

namespace Hiper.Net.Utilidades.Arreglos
{
    public class ArrStrComparer : IComparer<string[]>
    {
        private int nCampo;

        public ArrStrComparer(int nCampo)
        {
            this.nCampo = nCampo;
        }

        public int Compare(string[] a, string[] b)
        {
            string strA = a[nCampo];
            string strB = b[nCampo];
            return strA.CompareTo(strB);
        }
    }
}
