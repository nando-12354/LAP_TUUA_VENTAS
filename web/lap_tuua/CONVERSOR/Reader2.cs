using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace LAP.TUUA.CONVERSOR
{
    public class Reader2
    {
        public Hashtable ParseString(String strTrama)
        {
            if (strTrama != null && !strTrama.Trim().Equals("") && strTrama.Length > 4)
            {
                String cabecera = strTrama.Substring(4, 1);
                if (cabecera.Equals(Define.Boarding_Multiple))
                {
                    return Parse_Boarding_Multiple(strTrama);
                }
                else if (cabecera.Equals(Define.Boarding_Single))
                {
                    return Parse_Boarding_Single(strTrama);
                }
                else if (strTrama.Length >= (4 + Define.TamanoTicket))
                {
                    strTrama = strTrama.Substring(4, Define.TamanoTicket);
                    if (isNumeric(strTrama))
                        return Parse_Ticket(strTrama);
                }
                else
                {
                    return null;
                }
            }
            return null;
        }

        public Hashtable ParseString_Ticket(String strTrama)
        {
            if (strTrama != null && !strTrama.Trim().Equals("") && strTrama.Length >= (4 + Define.TamanoTicket))
            {
                strTrama = strTrama.Substring(4, Define.TamanoTicket);
                if (isNumeric(strTrama))
                    return Parse_Ticket(strTrama);
            }
            return null;
        }

        private static bool isNumeric(String str)
        {
            try
            {
                Int64.Parse(str);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public Hashtable Parse_Ticket(String strTrama)
        {
            Hashtable ht = new Hashtable();
            ht.Add(Define.Tipo, Define.Ticket);
            ht.Add(Define.NroTicket, strTrama.Substring(0, Define.TamanoTicket));
            return ht;
        }

        public Hashtable ParseString_Boarding(String strTrama)
        {
            if (strTrama != null && !strTrama.Trim().Equals("") && strTrama.Length > 4)
            {
                String cabecera = strTrama.Substring(4, 1);
                if (cabecera.Equals(Define.Boarding_Multiple))
                {
                    if (strTrama.Length >= 88)
                    {
                        return Parse_Boarding_Multiple2(strTrama);
                    }
                    else
                    {
                        return Parse_Boarding_Multiple(strTrama);
                    }
                }
                else if (cabecera.Equals(Define.Boarding_Single))
                { 
                    return Parse_Boarding_Single(strTrama);
                }
            }
            return null;
        }

        public Hashtable Parse_Boarding_Multiple2(String strTrama)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(Define.Dsc_SPConfig + "/" + Define.FolderResources + Define.FileName_FormatoBoarding_Multiple);
            XmlNode nodoRaiz = xmlDoc.DocumentElement;
            XmlNode nodoFormato = nodoRaiz.SelectSingleNode("child::formato");

            Hashtable htBoarding = new Hashtable();

            String tipo = nodoFormato.Attributes.GetNamedItem("tipo").Value;

            XmlNode nodoGralInfo = nodoFormato.SelectSingleNode("child::general_information");
            XmlNodeList lstCampo1 = nodoGralInfo.ChildNodes;
            XmlNode nodoFlightInfo = nodoFormato.SelectSingleNode("child::flight_information");
            XmlNodeList lstCampo2 = nodoFlightInfo.ChildNodes;

            strTrama = strTrama.Substring(4);
            foreach (XmlNode xmlCampo in lstCampo1)
            {
                string nombreKey = xmlCampo.Attributes["nombre"].Value;
                string longitud = xmlCampo.Attributes["tam"].Value;

                String valorKey = strTrama.Substring(0, Int32.Parse(longitud));
                htBoarding.Add(nombreKey, valorKey);
                strTrama = strTrama.Substring(Int32.Parse(longitud));
            }
            foreach (XmlNode xmlCampo in lstCampo2)
            {
                string nombreKey = xmlCampo.Attributes["nombre"].Value;
                string longitud = xmlCampo.Attributes["tam"].Value;

                String valorKey = strTrama.Substring(0, Int32.Parse(longitud));
                htBoarding.Add(nombreKey, valorKey);
                strTrama = strTrama.Substring(Int32.Parse(longitud));
            }
            return htBoarding;
        }

        public Hashtable Parse_Boarding_Multiple(String strTrama)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(Define.Dsc_SPConfig + "/" + Define.FolderResources + Define.FileName_FormatoBoarding_Multiple);
            XmlNode nodoRaiz = xmlDoc.DocumentElement;
            XmlNode nodoFormato = nodoRaiz.SelectSingleNode("child::formato");

            Hashtable htBoarding = new Hashtable();

            String tipo = nodoFormato.Attributes.GetNamedItem("tipo").Value;

            XmlNode nodoGralInfo = nodoFormato.SelectSingleNode("child::general_information");
            XmlNodeList lstCampo1 = nodoGralInfo.ChildNodes;
            XmlNode nodoFlightInfo = nodoFormato.SelectSingleNode("child::flight_information");
            XmlNodeList lstCampo2 = nodoFlightInfo.ChildNodes;

            strTrama = strTrama.Substring(4);
            foreach (XmlNode xmlCampo in lstCampo1)
            {
                string nombreKey = xmlCampo.Attributes["nombre"].Value;
                string longitud = xmlCampo.Attributes["tam"].Value;

                String valorKey = strTrama.Substring(0, Int32.Parse(longitud));
                htBoarding.Add(nombreKey, valorKey);
                strTrama = strTrama.Substring(Int32.Parse(longitud));
            }
            foreach (XmlNode xmlCampo in lstCampo2)
            {
                string nombreKey = xmlCampo.Attributes["nombre"].Value;
                string longitud = xmlCampo.Attributes["tam"].Value;

                String valorKey = strTrama.Substring(0, Int32.Parse(longitud));
                htBoarding.Add(nombreKey, valorKey);
                strTrama = strTrama.Substring(Int32.Parse(longitud));
            }
            return htBoarding;
        }

        public Hashtable Parse_Boarding_Single(String strTrama)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(Define.Dsc_SPConfig + "/" + Define.FolderResources + Define.FileName_FormatoBoarding_Single);
            XmlNode nodoRaiz = xmlDoc.DocumentElement;
            XmlNode nodoFormato = nodoRaiz.SelectSingleNode("child::formato");

            Hashtable htBoarding = new Hashtable();

            String tipo = nodoFormato.Attributes.GetNamedItem("tipo").Value;

            XmlNodeList lstCampo1 = nodoFormato.ChildNodes;

            strTrama = strTrama.Substring(4);
            foreach (XmlNode xmlCampo in lstCampo1)
            {
                string nombreKey = xmlCampo.Attributes["nombre"].Value;
                string longitud = xmlCampo.Attributes["tam"].Value;

                String valorKey = strTrama.Substring(0, Int32.Parse(longitud));
                htBoarding.Add(nombreKey, valorKey);
                strTrama = strTrama.Substring(Int32.Parse(longitud));
            }
            return htBoarding;
        }

        //-------- EAG 21/01/2010
        public void ParseHashtable(Hashtable ht)
        {
            String persona = (String)ht[Define.Persona];
            persona = persona.Replace('/', ' ');
            persona = persona.Replace('-', ' ');
            ht[Define.Persona] = persona;

            String companiaAux = ((String)ht[Define.Compania]).Trim();
            String nroVuelo = ((String)ht[Define.NroVuelo]).Trim();
            if (nroVuelo.Substring(0, 1).Equals("0"))
                nroVuelo = nroVuelo.Substring(1);
            ht[Define.NroVuelo] = companiaAux + nroVuelo;
        }
        //-------- EAG 21/01/2010

        //Accesos
        public Hashtable ParseString_ACS(String strTrama)
        {
            if (strTrama != null && !strTrama.Trim().Equals("") && strTrama.Length > 4)
            {
                String cabecera = strTrama.Substring(4, 1);
                if (cabecera.Equals(Define.Boarding_Multiple))
                {
                    return Parse_Boarding_Multiple_ACS(strTrama);
                }
                else if (cabecera.Equals(Define.Boarding_Single))
                {
                    return Parse_Boarding_Single_ACS(strTrama);
                }
                else if (strTrama.Length >= (4 + Define.TamanoTicket))
                {
                    strTrama = strTrama.Substring(4, Define.TamanoTicket);
                    if (isNumeric(strTrama))
                        return Parse_Ticket_ACS(strTrama);
                }
                else
                {
                    return null;
                }
            }
            return null;
        }


        public Hashtable Parse_Boarding_Single_ACS(String strTrama)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(AppDomain.CurrentDomain.BaseDirectory + "/boarding/FormatoBoardingSingle.xml");
            XmlNode nodoRaiz = xmlDoc.DocumentElement;
            XmlNode nodoFormato = nodoRaiz.SelectSingleNode("child::formato");

            Hashtable htBoarding = new Hashtable();

            String tipo = nodoFormato.Attributes.GetNamedItem("tipo").Value;

            XmlNodeList lstCampo1 = nodoFormato.ChildNodes;

            int s = 0;
            strTrama = strTrama.Substring(4);
            foreach (XmlNode xmlCampo in lstCampo1)
            {
                string nombreKey = xmlCampo.Attributes["nombre"].Value;
                string longitud = xmlCampo.Attributes["tam"].Value;
                s = s + Int32.Parse(longitud);
                String valorKey = strTrama.Substring(0, Int32.Parse(longitud));
                htBoarding.Add(nombreKey, valorKey);
                strTrama = strTrama.Substring(Int32.Parse(longitud));
            }

            return htBoarding;
        }

        public Hashtable Parse_Boarding_Multiple_ACS(String strTrama)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(AppDomain.CurrentDomain.BaseDirectory + "/boarding/FormatoBoardingMultiple.xml");
            XmlNode nodoRaiz = xmlDoc.DocumentElement;
            XmlNode nodoFormato = nodoRaiz.SelectSingleNode("child::formato");

            Hashtable htBoarding = new Hashtable();

            String tipo = nodoFormato.Attributes.GetNamedItem("tipo").Value;

            XmlNode nodoGralInfo = nodoFormato.SelectSingleNode("child::general_information");
            XmlNodeList lstCampo1 = nodoGralInfo.ChildNodes;
            XmlNode nodoFlightInfo = nodoFormato.SelectSingleNode("child::flight_information");
            XmlNodeList lstCampo2 = nodoFlightInfo.ChildNodes;

            strTrama = strTrama.Substring(4);

            try
            {
                foreach (XmlNode xmlCampo in lstCampo1)
                {
                    string nombreKey = xmlCampo.Attributes["nombre"].Value;
                    string longitud = xmlCampo.Attributes["tam"].Value;

                    String valorKey = strTrama.Substring(0, Int32.Parse(longitud));
                    htBoarding.Add(nombreKey, valorKey);
                    strTrama = strTrama.Substring(Int32.Parse(longitud));
                }

                //procesa "flight information"
                int i = 0;
                Hashtable htFlightInformation;
                ArrayList ArrLst = new ArrayList();
                int intSizeIndividualUse = 0;
                while (i < Int32.Parse((string)htBoarding["number_segments_encoded"]))
                {
                    htFlightInformation = new Hashtable();
                    foreach (XmlNode xmlCampo in lstCampo2)
                    {
                        string nombreKey = xmlCampo.Attributes["nombre"].Value;
                        string longitud = xmlCampo.Attributes["tam"].Value;

                        String valorKey = strTrama.Substring(0, Int32.Parse(longitud));
                        htFlightInformation.Add(nombreKey, valorKey);
                        strTrama = strTrama.Substring(Int32.Parse(longitud));

                        if (nombreKey.Equals("field_size_individual_use"))
                        {
                            intSizeIndividualUse = Convert.ToInt32(valorKey.Trim(), 16);
                            htFlightInformation["field_size_individual_use"] = intSizeIndividualUse.ToString();
                            if (intSizeIndividualUse > 0)
                            {
                                string strIndivualValue = strTrama.Substring(0, intSizeIndividualUse);
                                strTrama = strTrama.Substring(intSizeIndividualUse);
                                htFlightInformation.Add("individual_airline", strIndivualValue);
                            }
                        }
                    }
                    ArrLst.Add(htFlightInformation);
                    i++;
                }
                htBoarding.Add("flight_information", ArrLst);
                return htBoarding;
            }
            catch (IndexOutOfRangeException)
            {
                return null;
            }
            catch (ArgumentOutOfRangeException)
            {
                return null;
            }
            catch (ArgumentException)
            {
                return null;
            }
            catch (FormatException)
            {
                return null;
            }

        }

        public Hashtable Parse_Ticket_ACS(String strTrama)
        {
            Hashtable ht = new Hashtable();
            ht.Add(Define.Format_Code, Define.Ticket);
            ht.Add(Define.NroTicket, strTrama.Substring(0, Define.TamanoTicket));
            return ht;
        }

        public Hashtable Parse_Airline_ACS(string strAirline, string strForAirlineUse)
        {

            XmlDocument xmlDoc = new XmlDocument();
            string strFile = AppDomain.CurrentDomain.BaseDirectory + "/boarding/" + strAirline.Trim().ToUpper() + ".xml";
            if (File.Exists(strFile))
            {
                xmlDoc.Load(strFile);
                if (xmlDoc.HasChildNodes)
                {
                    XmlNode nodoRaiz = xmlDoc.DocumentElement;
                    XmlNode nodoFormato = nodoRaiz.SelectSingleNode("child::individual_airline");
                    Hashtable htBoarding = new Hashtable();
                    XmlNodeList lstCampo1 = nodoFormato.ChildNodes;
                    foreach (XmlNode xmlCampo in lstCampo1)
                    {
                        string nombreKey = xmlCampo.Attributes["nombre"].Value;
                        string longitud = xmlCampo.Attributes["tam"].Value;
                        try
                        {
                            String valorKey = strForAirlineUse.Substring(0, Int32.Parse(longitud));
                            htBoarding.Add(nombreKey, valorKey);
                            strForAirlineUse = strForAirlineUse.Substring(Int32.Parse(longitud));
                        }
                        catch (ArgumentOutOfRangeException)
                        {
                            htBoarding.Add(nombreKey, "");
                        }
                    }
                    return htBoarding;
                }
            }
            return null;
        } 

    }
}
