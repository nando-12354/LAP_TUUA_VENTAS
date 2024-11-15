using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.IO;
using System.Collections;

namespace tuua_api.Util
{
    public class Reader
    {
        //segmento opcional de boarding multiple
        private string NO_OPTIONAL_SEGMENT = "00";

        //Rehabilitación 
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
                    return Parse_Boarding_Multiple(strTrama);
                }
                else if (cabecera.Equals(Define.Boarding_Single))
                {
                    return Parse_Boarding_Single(strTrama);
                }
                else if (strTrama.Length > (Define.LongBcbpLan1D + 4))
                {
                    return Parse_Boarding_LAN1D(strTrama);
                }
            }
            return null;
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

            XmlNode nodoOptInfo = nodoFormato.SelectSingleNode("child::optional_information");
            XmlNodeList lstCampo3 = nodoOptInfo.ChildNodes;

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
            if (htBoarding["field_size_following_variable_size_field"].ToString().Trim() != NO_OPTIONAL_SEGMENT)
            {
                foreach (XmlNode xmlCampo in lstCampo3)
                {
                    string nombreKey = xmlCampo.Attributes["nombre"].Value;
                    string longitud = xmlCampo.Attributes["tam"].Value;

                    String valorKey = strTrama.Substring(0, Int32.Parse(longitud));
                    htBoarding.Add(nombreKey, valorKey);
                    strTrama = strTrama.Substring(Int32.Parse(longitud));
                }
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
            persona = persona.Replace('&', ' ');

            ht[Define.Persona] = persona;

            String companiaAux = ((String)ht[Define.Compania]).Trim();
            String nroVuelo = ((String)ht[Define.NroVuelo]).Trim();
            if (nroVuelo.Substring(0, 1).Equals("0"))
                nroVuelo = nroVuelo.Substring(1);
            ht[Define.NroVuelo] = companiaAux + nroVuelo;

            //INI SOLO POR LAN 1D
            //if (((String)ht[Define.Format_Code]).Trim().Equals(Define.FORM_BCBP_LAN1D))
            string strAsiento = ((String)ht[Define.Asiento]).Trim();
            if (strAsiento == "")
            {
                if (companiaAux.Equals(Define.COD_AERO_LA) || companiaAux.Equals(Define.COD_AERO_LP))
                    strAsiento = Define.COD_EMPTY_SEAT_NUMBER;
            }
            else
            {
                while (strAsiento.Trim().Length < Define.SIZE_SEAT_NUMBER) { strAsiento = "0" + strAsiento; }
            }
            ht[Define.Asiento] = strAsiento;
            if (nroVuelo.Length == Define.SIZE_FLIGHT_NUMBER && nroVuelo.Substring(0, 1).Equals("2") && companiaAux.Equals(Define.COD_AERO_LA))
            {
                ht[Define.Compania] = Define.COD_AERO_LP;
            }
            //}
            //FIN SOLO POR LAN 1D
            if (ht[Define.CHECKIN_SEQUENCE_NUMBER] != null && ((String)ht[Define.CHECKIN_SEQUENCE_NUMBER]).Trim() != "")
            {
                string strCheckinNumber = ((String)ht[Define.CHECKIN_SEQUENCE_NUMBER]).Trim();
                while (strCheckinNumber.Trim().Length < Define.SIZE_CHECKIN_NUMBER) { strCheckinNumber = "0" + strCheckinNumber; }
                ht[Define.CHECKIN_SEQUENCE_NUMBER] = strCheckinNumber;
            }
        }
        //-------- EAG 21/01/2010

        //Accesos
        public Hashtable ParseString_ACS(String strTrama, String strPrefijo, String strPreDestrabe, String strCambioMolinete)
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
                else if (strTrama.Length == (Define.LongBcbpLan1D + 4) || strTrama.Length == 27 || strTrama.Length == 24)
                {
                    return Parse_Boarding_LAN1D(strTrama);
                }
                else if (strTrama.Length >= (Define.LongTicket + 4))
                {
                    strTrama = strTrama.Substring(4, Define.LongTicket);
                    if (isNumeric(strTrama))
                        return Parse_Ticket_ACS(strTrama);
                    else
                        if (strTrama.Substring(0, strPrefijo.Length).Equals(strPrefijo))
                        return Parse_Supervisor_ACS(strTrama);
                    else
                        return null;
                }
                else if (strTrama.Length >= (Define.LongSupervisor + 4) &&
                        !(strTrama.Length >= (Define.LongDestrabe + 4) && strTrama.Substring(4, strPreDestrabe.Trim().Length).Equals(strPreDestrabe)) &&
                        !(!strCambioMolinete.Equals(String.Empty) && strTrama.Length >= (Define.LongCambioMolinete + 4) && strTrama.Substring(4, strCambioMolinete.Trim().Length).Equals(strCambioMolinete)))
                {
                    strTrama = strTrama.Substring(4, Define.LongSupervisor);
                    if (strTrama.Substring(0, strPrefijo.Trim().Length).Equals(strPrefijo))
                        return Parse_Supervisor_ACS(strTrama);
                    else
                        return null;

                }
                //Utilizado para el destrabe y el cierre de la puerta de discapacitado
                else if (strTrama.Length >= (Define.LongDestrabe + 4) &&
                       !(!strCambioMolinete.Equals(String.Empty) && strTrama.Length >= (Define.LongCambioMolinete + 4) && strTrama.Substring(4, strCambioMolinete.Trim().Length).Equals(strCambioMolinete)))
                {
                    strTrama = strTrama.Substring(4, Define.LongDestrabe);
                    if (strTrama.Substring(0, strPreDestrabe.Trim().Length).Equals(strPreDestrabe))
                        return Parse_Supervisor_ACS_Destrabe(strTrama);
                    else
                        return null;

                }
                else if (strTrama.Length >= (Define.LongCambioMolinete + 4) && !strCambioMolinete.Equals(String.Empty))
                {
                    strTrama = strTrama.Substring(4, Define.LongCambioMolinete);
                    if (strTrama.Substring(0, strCambioMolinete.Trim().Length).Equals(strCambioMolinete))
                        return Parse_Supervisor_ACS_CambioMolinete(strTrama);
                    else
                        return null;

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
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(AppDomain.CurrentDomain.BaseDirectory + "/" + Define.FolderResources + Define.FileName_FormatoBoarding_Single);//+ "/boarding/FormatoBoardingSingle.xml");
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
            catch { return null; }
        }
        
        public Hashtable Parse_Boarding_Multiple_ACS(String strTrama)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(AppDomain.CurrentDomain.BaseDirectory + "/" + Define.FolderResources + Define.FileName_FormatoBoarding_Multiple);
            XmlNode nodoRaiz = xmlDoc.DocumentElement;
            XmlNode nodoFormato = nodoRaiz.SelectSingleNode("child::formato");

            Hashtable htBoarding = new Hashtable();

            String tipo = nodoFormato.Attributes.GetNamedItem("tipo").Value;

            XmlNode nodoGralInfo = nodoFormato.SelectSingleNode("child::general_information");
            XmlNodeList lstCampo1 = nodoGralInfo.ChildNodes;
            XmlNode nodoFlightInfo = nodoFormato.SelectSingleNode("child::mandatory_items");
            XmlNodeList lstCampo2 = nodoFlightInfo.ChildNodes;

            XmlNode nodoOptInfo = nodoFormato.SelectSingleNode("child::optional_items");
            XmlNodeList lstCampo3 = nodoOptInfo.ChildNodes;

            XmlNode nodoCondItems = nodoFormato.SelectSingleNode("child::conditional_items");
            XmlNodeList lstCampo4 = nodoCondItems.ChildNodes;

            strTrama = strTrama.Substring(4);

            try
            {
                foreach (XmlNode xmlCampo in lstCampo1)
                {
                    string nombreKey = xmlCampo.Attributes["nombre"].Value;
                    string longitud = xmlCampo.Attributes["tam"].Value;
                    int intLongitud = Int32.Parse(longitud);

                    String valorKey = strTrama.Substring(0, intLongitud);
                    htBoarding.Add(nombreKey, valorKey);
                    strTrama = strTrama.Substring(intLongitud);
                }

                //procesa "flight information"
                int i = 0;
                Hashtable htOptionalItems = null;
                ArrayList ArrLst = new ArrayList();
                int intVariableSizeField = 0;
                int intSegments = Int32.Parse((string)htBoarding["number_segments_encoded"]);
                while (i < intSegments)
                {
                    Hashtable htMandatoryItems = new Hashtable();
                    foreach (XmlNode xmlCampo in lstCampo2)
                    {
                        string nombreKey = xmlCampo.Attributes["nombre"].Value;
                        string longitud = xmlCampo.Attributes["tam"].Value;
                        int intLongitud = Int32.Parse(longitud);

                        String valorKey = strTrama.Substring(0, intLongitud);
                        htMandatoryItems.Add(nombreKey, valorKey);
                        strTrama = strTrama.Substring(intLongitud);

                        if (nombreKey.Equals("field_size_following_variable_size_field"))
                        {
                            intVariableSizeField = Convert.ToInt32(valorKey.Trim(), 16);
                            htMandatoryItems[nombreKey] = intVariableSizeField.ToString();
                        }
                    }
                    if (intVariableSizeField > 0)
                    {
                        int intConditSize = intVariableSizeField;
                        string strTramaCondit = "";
                        if (intSegments > 1)
                        {
                            strTramaCondit = strTrama.Substring(0, intVariableSizeField);
                            try
                            {
                                if (i == 0)
                                {
                                    int intOptionalSize = intVariableSizeField;
                                    intConditSize = 0;
                                    foreach (XmlNode xmlCampo in lstCampo3)
                                    {
                                        string nombreKey = xmlCampo.Attributes["nombre"].Value;
                                        string longitud = xmlCampo.Attributes["tam"].Value;
                                        int intLongitud = Int32.Parse(longitud);
                                        if (intOptionalSize >= intLongitud)
                                        {
                                            String valorKey = strTramaCondit.Substring(0, intLongitud);
                                            htMandatoryItems.Add(nombreKey, valorKey);
                                            strTramaCondit = strTramaCondit.Substring(intLongitud);
                                            //intVariableSizeField -= intLongitud;
                                            intOptionalSize -= intLongitud;
                                            if (nombreKey.Equals("field_size_following_structure"))
                                            {
                                                int intFollowStructure = Convert.ToInt32(valorKey.Trim(), 16);
                                                intConditSize = intOptionalSize - intFollowStructure;
                                                intOptionalSize = intFollowStructure;
                                                if (intFollowStructure == 0)
                                                {
                                                    break;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            strTramaCondit = strTramaCondit.Substring(intOptionalSize);
                                            break;
                                        }
                                    }
                                }
                                if (intConditSize > 0)
                                {
                                    foreach (XmlNode xmlCampo in lstCampo4)
                                    {
                                        string nombreKey = xmlCampo.Attributes["nombre"].Value;
                                        string longitud = xmlCampo.Attributes["tam"].Value;
                                        int intLongitud = Int32.Parse(longitud);
                                        intLongitud = intLongitud < 0 ? intConditSize : intLongitud;

                                        if (intConditSize >= intLongitud)
                                        {
                                            String valorKey = strTramaCondit.Substring(0, intLongitud);
                                            htMandatoryItems.Add(nombreKey, valorKey);
                                            strTramaCondit = strTramaCondit.Substring(intLongitud);
                                            intConditSize -= intLongitud;
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                }
                            }
                            catch { }
                            if (i + 1 < intSegments)
                            {
                                strTrama = strTrama.Substring(intVariableSizeField);
                            }
                        }



                    }

                    ArrLst.Add(htMandatoryItems);
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

        public Hashtable Parse_Supervisor_ACS(String strTrama)
        {
            Hashtable ht = new Hashtable();
            ht.Add(Define.Format_Code, Define.PinPad);
            ht.Add(Define.NroTicket, strTrama.Substring(0, Define.LongSupervisor));
            return ht;
        }

        public Hashtable Parse_Supervisor_ACS_Destrabe(String strTrama)
        {
            Hashtable ht = new Hashtable();
            ht.Add(Define.Format_Code, Define.Destrabe);
            ht.Add(Define.NroTicket, strTrama.Substring(0, Define.LongDestrabe));
            return ht;
        }

        public Hashtable Parse_Supervisor_ACS_CambioMolinete(String strTrama)
        {
            Hashtable ht = new Hashtable();
            ht.Add(Define.Format_Code, Define.CambioMolinete);
            ht.Add(Define.NroTicket, strTrama.Substring(0, Define.LongDestrabe));
            return ht;
        }

        public Hashtable Parse_Ticket_ACS(String strTrama)
        {
            Hashtable ht = new Hashtable();
            ht.Add(Define.Format_Code, Define.Ticket);
            ht.Add(Define.NroTicket, strTrama.Substring(0, Define.LongTicket));
            return ht;
        }

        public Hashtable Parse_Airline_ACS(string strAirline, string strForAirlineUse)
        {
            XmlDocument xmlDoc = new XmlDocument();
            string strFile = AppDomain.CurrentDomain.BaseDirectory + "/" + Define.FolderResources + strAirline.Trim().ToUpper() + ".xml";
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
                            if (strForAirlineUse.Length > Int32.Parse(longitud))
                                strForAirlineUse = strForAirlineUse.Substring(Int32.Parse(longitud));
                        }
                        catch (ArgumentOutOfRangeException)
                        {
                            //htBoarding.Add(nombreKey, "");
                            return null;
                        }
                        catch (IndexOutOfRangeException)
                        {
                            //htBoarding.Add(nombreKey, "");
                            return null;
                        }
                        catch (ArgumentException)
                        {
                            //htBoarding.Add(nombreKey, "");
                            return null;
                        }
                        catch (FormatException)
                        {
                            //htBoarding.Add(nombreKey, "");
                            return null;
                        }
                    }
                    return htBoarding;
                }
            }
            return null;
        }

        public Hashtable Parse_Boarding_LAN1D(String strTrama)
        {
            XmlDocument xmlDoc = new XmlDocument();
            string strNumVuelo = "";
            int s = 0;
            try
            {
                xmlDoc.Load(AppDomain.CurrentDomain.BaseDirectory + "/" + Define.FolderResources + "LAN1D.xml");
                XmlNode nodoRaiz = xmlDoc.DocumentElement;
                XmlNode nodoFormato = nodoRaiz.SelectSingleNode("child::individual_airline");

                Hashtable htBoarding = new Hashtable();
                //String tipo = nodoFormato.Attributes.GetNamedItem("tipo").Value;

                XmlNodeList lstCampo1 = nodoFormato.ChildNodes;


                strTrama = strTrama.Substring(4);
                foreach (XmlNode xmlCampo in lstCampo1)
                {
                    string nombreKey = xmlCampo.Attributes["nombre"].Value;
                    string longitud = xmlCampo.Attributes["tam"].Value;
                    s = s + Int32.Parse(longitud);
                    String valorKey = strTrama.Substring(0, Int32.Parse(longitud));
                    htBoarding.Add(nombreKey, valorKey.Trim());
                    strTrama = strTrama.Substring(Int32.Parse(longitud));
                }
                nodoFormato = nodoRaiz.SelectSingleNode("child::airline_designator" + (string)htBoarding["canal"]);
                htBoarding.Add("airline_designator", nodoFormato.Attributes.GetNamedItem("valor").Value);

                strNumVuelo = ((string)htBoarding["flight_number"]).Trim();

                //if (strNumVuelo.Substring(0, 1).Equals("0") && strNumVuelo.Length > 3) strNumVuelo = strNumVuelo.Substring(1);

                htBoarding["flight_number"] = strNumVuelo;//((string)htBoarding["airline_designator"]).Trim() + strNumVuelo;
                htBoarding.Add("format_code", Define.FORM_BCBP_LAN1D);
                return htBoarding;
            }
            catch { return null; }
        }

    }
}