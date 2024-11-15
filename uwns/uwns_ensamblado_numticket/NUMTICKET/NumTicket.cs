using System;
using System.Collections.Generic;
using System.Text;

namespace NUMTICKET
{
    public class NumTicket
    {
        public const int NUMBER_SIZE = 16;

        public static string CreateNumber(string strSerie, string strSecuencial, string strNumAnt, string strPosiciones)
        {
            string strNum = "";
            int length1 = strSecuencial.Length;
            try
            {
                string[] arrPosicion = strPosiciones.Split(';');
                int length2 = arrPosicion.Length;
                do
                {
                    strNum = DateTime.Now.Ticks.ToString();
                    strNum = strNum.Substring(0, length2);
                    NumTicket.AlternarDigitos(ref strNum, strSecuencial, strSerie, arrPosicion);
                }
                while (strNum.CompareTo(strNumAnt) <= 0);
            }
            catch
            {
            }
            return strNum;
        }

        public static void AlternarDigitos(ref string strNum, string strSecuencial, string strSerie, string[] arrPosicion)
        {
            string strNumSeg = strNum;
            strNum = strSerie + strSecuencial;
            NumTicket.DigitosSeguridad(ref strNum, strNumSeg, arrPosicion);
        }

        public static void DigitosSeguridad(ref string strNum, string strNumSeg, string[] arrPosicion)
        {
            string[] arrNumeroFinal = new string[16];
            string str = "";
            NumTicket.LLenarBlanco(arrNumeroFinal);
            for (int startIndex = 0; startIndex < arrPosicion.Length; ++startIndex)
            {
                int index = int.Parse(arrPosicion[startIndex]);
                arrNumeroFinal[index] = strNumSeg.Substring(startIndex, 1);
            }
            for (int startIndex = 0; startIndex < strNum.Length; ++startIndex)
            {
                int index = startIndex;
                while (true)
                {
                    if (!(arrNumeroFinal[index] == ""))
                        ++index;
                    else
                        break;
                }
                arrNumeroFinal[index] = strNum.Substring(startIndex, 1);
            }
            for (int index = 0; index < arrNumeroFinal.Length; ++index)
                str += arrNumeroFinal[index];
            strNum = str;
        }

        private static void LLenarBlanco(string[] arrNumeroFinal)
        {
            for (int index = 0; index < arrNumeroFinal.Length; ++index)
                arrNumeroFinal[index] = "";
        }

        public static string ObtainRealTicket(string strNumTicket, string strDigSeg)
        {
            if (strNumTicket == null || strNumTicket == "" || strDigSeg == null || strDigSeg == "")
                return "";
            string[] strArray = strDigSeg.Trim().Split(';');
            char[] chArray = strNumTicket.Trim().ToCharArray();
            string str = "";
            for (int index1 = 0; index1 < strArray.Length; ++index1)
            {
                int index2 = int.Parse(strArray[index1]);
                chArray[index2] = '.';
            }
            for (int index = 0; index < chArray.Length; ++index)
            {
                if ((int)chArray[index] != 46)
                    str += chArray[index].ToString();
            }
            return str;
        }
    }
}
