using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Net;
using System.Xml;
using LAP.TUUA.ENTIDADES;
using LAP.TUUA.DAO;
using System.Data;
using System.Collections;


namespace LAP.TUUA.ALARMAS
{
    public static class GestionAlarma
    {
        private static DAO_AlarmaGenerada objDAOAlarmaGenerada = null;
        private static DAO_CnfgAlarma objDAOCnfgAlarma = null;
        private static DAO_ParameGeneral objDAOParameGeneral = null;
        private static DAO_Auditoria oDAO_Auditoria = null;
        private static string sPathConfig;
        private static bool flagError;

        public static bool Registrar(string sPath, string sCodAlarma, string sCodModulo, string sDscEquipo, string sTipImportancia, string sDscSubject, string sDscBody, string sLogUsuarioMod)
        {
            sPathConfig = sPath;
            objDAOAlarmaGenerada = new DAO_AlarmaGenerada(sPathConfig);
            AlarmaGenerada objAlarmaGenerada = new AlarmaGenerada();
            objAlarmaGenerada.SCodAlarma = sCodAlarma;
            objAlarmaGenerada.SCodModulo = sCodModulo;
            objAlarmaGenerada.SDscEquipo = sDscEquipo;
            objAlarmaGenerada.STipImportancia = sTipImportancia;
            objAlarmaGenerada.SDscSubject = sDscSubject;
            objAlarmaGenerada.SDscBody = sDscBody;
            objAlarmaGenerada.SLogUsuarioMod = sLogUsuarioMod;

            try
            {
                objDAOCnfgAlarma = new DAO_CnfgAlarma(sPathConfig);
                CnfgAlarma ObjCnfgAlarma = null;
                ObjCnfgAlarma = objDAOCnfgAlarma.obtener(objAlarmaGenerada.SCodAlarma, objAlarmaGenerada.SCodModulo);

                if (ObjCnfgAlarma != null)
                {
                    DateTime localNow = DateTime.Now;
                       

                    objAlarmaGenerada.SDscBody += "<br>Fecha Generada:"+localNow.ToString("G")+"<br><b>" + ObjCnfgAlarma.SDscFinMensaje + "</b>"; 

                    if (objDAOAlarmaGenerada.insertar(objAlarmaGenerada) == false)
                    {
                        return false;
                    }
                    //EnviarAlarma(objAlarmaGenerada);
                }
            }
            catch (Exception ex)
            {
                //throw;
            }
           
            return true;
        }

        public static bool RegistrarAlarmaCrit(string sPath, string sCodAlarma, string sCodModulo, string sDscEquipo, 
            string sTipImportancia, string sDscSubject, string sDscBody, string sLogUsuarioMod, Int64 idTxCritica, 
            string sModMtto, string sUri)
        {
            sPathConfig = sPath;
            objDAOAlarmaGenerada = new DAO_AlarmaGenerada(sPathConfig);
            AlarmaGenerada objAlarmaGenerada = new AlarmaGenerada();
            objAlarmaGenerada.SCodAlarma = sCodAlarma;
            objAlarmaGenerada.SCodModulo = sCodModulo;
            objAlarmaGenerada.SDscEquipo = sDscEquipo;
            objAlarmaGenerada.STipImportancia = sTipImportancia;
            objAlarmaGenerada.SDscSubject = sDscSubject;
            objAlarmaGenerada.SDscBody = sDscBody;
            objAlarmaGenerada.SLogUsuarioMod = sLogUsuarioMod;
            objAlarmaGenerada.IdTxCritica = idTxCritica;

            try
            {
                objDAOCnfgAlarma = new DAO_CnfgAlarma(sPathConfig);
                CnfgAlarma ObjCnfgAlarma = null;
                ObjCnfgAlarma = objDAOCnfgAlarma.obtener(objAlarmaGenerada.SCodAlarma, objAlarmaGenerada.SCodModulo);

                if (ObjCnfgAlarma != null)
                {
                    if (!string.IsNullOrEmpty(ObjCnfgAlarma.SDscAsunto))
                    {
                        objAlarmaGenerada.SDscSubject = ObjCnfgAlarma.SDscAsunto;
                    }

                    if (sModMtto.Equals("TasaCambio"))
                    {
                        if (sCodAlarma.Equals("W0000017"))
                        {
                            objAlarmaGenerada.SDscBody = ObtenerBodyTasaCambio(sPathConfig, idTxCritica, sLogUsuarioMod, sUri, 1);
                        }
                        else if (sCodAlarma.Equals("W0000016"))
                        {
                            objAlarmaGenerada.SDscBody = ObtenerBodyTasaCambio(sPathConfig, idTxCritica, sLogUsuarioMod, sUri, 2);
                        }
                        else if (sCodAlarma.Equals("W0000083"))
                        {
                            objAlarmaGenerada.SDscBody = ObtenerBodyTasaCambio(sPathConfig, idTxCritica, sLogUsuarioMod, sUri, 3);
                        }
                    }
                    else if (sModMtto.Equals("TipoTicket"))
                    {
                        if (sCodAlarma.Equals("W0000019"))
                        {
                            objAlarmaGenerada.SDscBody = ObtenerBodyTipoTicket(sPathConfig, idTxCritica, sLogUsuarioMod, sUri, 1);
                        }
                        else if (sCodAlarma.Equals("W0000018"))
                        {
                            objAlarmaGenerada.SDscBody = ObtenerBodyTipoTicket(sPathConfig, idTxCritica, sLogUsuarioMod, sUri, 2);
                        }
                        else if (sCodAlarma.Equals("W0000084"))
                        {
                            objAlarmaGenerada.SDscBody = ObtenerBodyTipoTicket(sPathConfig, idTxCritica, sLogUsuarioMod, sUri, 3);
                        }
                    }
                    else if (sModMtto.Equals("Compania"))
                    {
                        if (sCodAlarma.Equals("W0000085"))
                        {
                            objAlarmaGenerada.SDscBody = ObtenerBodyCampania(sPathConfig, idTxCritica, sLogUsuarioMod, sUri, 1);
                        }
                        else if (sCodAlarma.Equals("W0000086"))
                        {
                            objAlarmaGenerada.SDscBody = ObtenerBodyCampania(sPathConfig, idTxCritica, sLogUsuarioMod, sUri, 2);
                        }
                    }
                    else
                    {
                        DateTime localNow = DateTime.Now;
                        objAlarmaGenerada.SDscBody += "<br>Fecha Generada:" + localNow.ToString("G") + "<br><b>" + ObjCnfgAlarma.SDscFinMensaje + "</b>";
                    }

                    if (objDAOAlarmaGenerada.insertarCrit(objAlarmaGenerada) == false)
                    {
                        return false;
                    }
                    //EnviarAlarma(objAlarmaGenerada);
                }
            }
            catch (Exception ex)
            {
                //throw;
            }

            return true;
        }

        private static string DecimalToBase(int iDec, int numbase)
        {
            const int base10 = 10;
            char[] cHexa = new char[] { 'A', 'B', 'C', 'D', 'E', 'F' };

            string strBin = "";
            int[] result = new int[32];
            int MaxBit = 32;
            for (; iDec > 0; iDec /= numbase)
            {
                int rem = iDec % numbase;
                result[--MaxBit] = rem;
            }
            for (int i = 0; i < result.Length; i++)
                if ((int)result.GetValue(i) >= base10)
                    strBin += cHexa[(int)result.GetValue(i) % base10];
                else
                    strBin += result.GetValue(i);
            strBin = strBin.TrimStart(new char[] { '0' });
            return strBin;
        }

        private static DAO_Moneda oDAO_Moneda = null;
        private static DAO_Usuario oDAO_Usuario = null;
        private static DAO_TipoTicket oDAO_TipoTicket = null;
        private static DAO_ListaDeCampos oDAO_ListaDeCampos = null;
        private static DAO_ModalidadVenta oDAO_ModalidadVenta = null;
        private static DAO_ParameGeneral oDAO_ParameGeneral = null;

        private static string ObtenerBodyTasaCambio(string sPathConfig, Int64 idTxCritica, 
            string sCodUsuarioModificador, string sUri, int iTTC)
        {
            string sBody = string.Empty;
            oDAO_Auditoria = new DAO_Auditoria(sPathConfig);
            oDAO_Moneda = new DAO_Moneda(sPathConfig);
            oDAO_Usuario = new DAO_Usuario(sPathConfig);

            DataTable dt_AuditoriaTxCrit = oDAO_Auditoria.obtenerRegistrosTransaccionCritica(idTxCritica);

            string xTxRealizada = string.Empty;
            string xOpRealizada = string.Empty;
            string sTitleC = string.Empty;
            string sTitleV = string.Empty;
            //Concepto
            string[] lstConceptos = new string[5];
            lstConceptos[0] = "Tipo de Cambio:";
            lstConceptos[1] = "Moneda:";
            lstConceptos[2] = "Valor tasa:";
            lstConceptos[3] = string.Empty;
            lstConceptos[4] = "Usuario:";

            ArrayList lstValoresOld_C = new ArrayList();
            ArrayList lstValoresNew_C = new ArrayList();

            ArrayList lstValoresOld_V = new ArrayList();
            ArrayList lstValoresNew_V = new ArrayList();

            string sCampoLogRegistro = string.Empty;

            if (iTTC.Equals(1))
            {
                xTxRealizada = "Registro de nueva Tasa de Cambio Programada";
                xOpRealizada = "Inserci&oacute;n";
                sTitleC = "Datos insertados para tipo de cambio Compra";
                sTitleV = "Datos insertados para tipo de cambio Venta";
                lstConceptos[3] = "Fecha programada:";
                sCampoLogRegistro = "Log_Reg_Nuevo";
            }
            else if (iTTC.Equals(2))
            {
                xTxRealizada = "Registro de nueva Tasa de Cambio";
                xOpRealizada = "Inserci&oacute;n";
                sTitleC = "Datos afectados para tipo de cambio Compra";
                sTitleV = "Datos afectados para tipo de cambio Venta";
                lstConceptos[3] = "Fecha de inicio de vigencia:";
                sCampoLogRegistro = "Log_Reg_Nuevo";
            }
            else if (iTTC.Equals(3))
            {
                xTxRealizada = "Eliminación de una Tasa de Cambio Programada";
                xOpRealizada = "Eliminaci&oacute;n";
                sTitleC = "Datos eliminados para tipo de cambio Compra";
                sTitleV = "Datos eliminados para tipo de cambio Venta";
                lstConceptos[3] = "Fecha programada:";
                sCampoLogRegistro = "Log_Reg_Orig";
            }

            //Tipo de Cambio: Compra
            foreach (DataRow dr in dt_AuditoriaTxCrit.Select(sCampoLogRegistro + " LIKE '%ID4©C%'", "Cod_Contador ASC"))
            {
                if (dr["Log_Tabla_Mod"].ToString().Equals("TUA_TasaCambioHist"))
                {
                    string[] lstValues = dr[sCampoLogRegistro].ToString().Split('|');

                    lstValoresOld_C.Add("Compra ( C )");
                    lstValoresOld_C.Add(oDAO_Moneda.obtenerDescripcionMoneda(lstValues[4].Replace("ID5©", "")));
                    lstValoresOld_C.Add(lstValues[5].Replace("ID6©", ""));
                    lstValoresOld_C.Add(lstValues[8].Replace("ID9©", ""));
                    lstValoresOld_C.Add(oDAO_Usuario.obtenerNombreCompletoUsuario(lstValues[10].Replace("ID11©", "")));
                }
                else if (dr["Log_Tabla_Mod"].ToString().Equals("TUA_TasaCambio"))
                {
                    string[] lstValues = dr[sCampoLogRegistro].ToString().Split('|');

                    if (iTTC.Equals(1) || iTTC.Equals(2))
                    {
                        lstValoresNew_C.Add("Compra ( C )");
                        lstValoresNew_C.Add(oDAO_Moneda.obtenerDescripcionMoneda(lstValues[4].Replace("ID5©", "")));
                        lstValoresNew_C.Add(lstValues[5].Replace("ID6©", ""));
                        if (iTTC.Equals(1))
                        {
                            lstValoresNew_C.Add(lstValues[9].Replace("ID10©", ""));
                        }
                        else if (iTTC.Equals(2))
                        {
                            lstValoresNew_C.Add(lstValues[6].Replace("ID7©", ""));
                        }
                        lstValoresNew_C.Add(oDAO_Usuario.obtenerNombreCompletoUsuario(lstValues[10].Replace("ID11©", "")));
                    }
                    else if (iTTC.Equals(3))
                    {
                        lstValoresOld_C.Add("Compra ( C )");
                        lstValoresOld_C.Add(oDAO_Moneda.obtenerDescripcionMoneda(lstValues[4].Replace("ID5©", "")));
                        lstValoresOld_C.Add(lstValues[5].Replace("ID6©", ""));
                        
                        lstValoresOld_C.Add(lstValues[9].Replace("ID10©", ""));
                        
                        lstValoresOld_C.Add(oDAO_Usuario.obtenerNombreCompletoUsuario(lstValues[10].Replace("ID11©", "")));
                    }
                }
            }

            //Tipo de Cambio: Venta
            foreach (DataRow dr in dt_AuditoriaTxCrit.Select(sCampoLogRegistro + " LIKE '%ID4©V%'", "Cod_Contador ASC"))
            {
                if (dr["Log_Tabla_Mod"].ToString().Equals("TUA_TasaCambioHist"))
                {
                    string[] lstValues = dr[sCampoLogRegistro].ToString().Split('|');

                    lstValoresOld_V.Add("Venta ( V )");
                    lstValoresOld_V.Add(oDAO_Moneda.obtenerDescripcionMoneda(lstValues[4].Replace("ID5©", "")));
                    lstValoresOld_V.Add(lstValues[5].Replace("ID6©", ""));
                    lstValoresOld_V.Add(lstValues[8].Replace("ID9©", ""));
                    lstValoresOld_V.Add(oDAO_Usuario.obtenerNombreCompletoUsuario(lstValues[10].Replace("ID11©", "")));
                }
                else if (dr["Log_Tabla_Mod"].ToString().Equals("TUA_TasaCambio"))
                {
                    string[] lstValues = dr[sCampoLogRegistro].ToString().Split('|');

                    if (iTTC.Equals(1) || iTTC.Equals(2))
                    {
                        lstValoresNew_V.Add("Venta ( V )");
                        lstValoresNew_V.Add(oDAO_Moneda.obtenerDescripcionMoneda(lstValues[4].Replace("ID5©", "")));
                        lstValoresNew_V.Add(lstValues[5].Replace("ID6©", ""));
                        if (iTTC.Equals(1))
                        {
                            lstValoresNew_V.Add(lstValues[9].Replace("ID10©", ""));
                        }
                        else if (iTTC.Equals(2))
                        {
                            lstValoresNew_V.Add(lstValues[6].Replace("ID7©", ""));
                        }
                        lstValoresNew_V.Add(oDAO_Usuario.obtenerNombreCompletoUsuario(lstValues[10].Replace("ID11©", "")));
                    }
                    else if (iTTC.Equals(3))
                    {
                        lstValoresOld_V.Add("Venta ( V )");
                        lstValoresOld_V.Add(oDAO_Moneda.obtenerDescripcionMoneda(lstValues[4].Replace("ID5©", "")));
                        lstValoresOld_V.Add(lstValues[5].Replace("ID6©", ""));

                        lstValoresOld_V.Add(lstValues[9].Replace("ID10©", ""));

                        lstValoresOld_V.Add(oDAO_Usuario.obtenerNombreCompletoUsuario(lstValues[10].Replace("ID11©", "")));
                    }
                }
            }

            #region "Body"
            sBody = 
                    "<table border=\"1\" style=\"border-spacing:0\">" +
	                    "<tr>" +
		                    "<td>" +
			                    "<b>Transacci&oacute;n realizada:</b>" +
		                    "</td>" +
		                    "<td colspan=\"2\">" +
			                    xTxRealizada + "<br>( " + sUri + " )" +
		                    "</td>" +
	                    "</tr>" +
	                    "<tr>" +
		                    "<td>" +
			                    "<b>Operaci&oacute;n realizada:</b>" +
		                    "</td>" +
		                    "<td colspan=\"2\">" +
                                xOpRealizada +
		                    "</td>" +
	                    "</tr>" +
	                    "<tr>" +
		                    "<td>" +
			                    "<b>Usuario modificador:</b>" +
		                    "</td>" +
		                    "<td colspan=\"2\">" +
			                    oDAO_Usuario.obtenerNombreCompletoUsuario(sCodUsuarioModificador) +
		                    "</td>" +
	                    "</tr>" +
	                    "<tr>" +
		                    "<td>" +
			                    "<b>Fecha modificaci&oacute;n:</b>" +
		                    "</td>" +
		                    "<td colspan=\"2\">" +
			                    DateTime.Now.ToString("dd/MM/yyyy HH:mm") +
		                    "</td>" +
	                    "</tr>";
            #region Compra
            if (lstValoresOld_C.Count > 0 || lstValoresNew_C.Count > 0)
            {
                sBody = sBody + 
                        "<tr>" +
		                    "<td colspan=\"3\" align=\"center\">" +
                                "<b>" + sTitleC + "</b>" +
		                    "</td>" +
	                    "</tr>" +
                        "<tr>" +
		                    "<td align=\"center\">" +
			                    "<u><b>Concepto</b></u>" +
		                    "</td>" +
		                    "<td align=\"center\">" +
			                    "<u><b>Valor anterior</b></u>" +
		                    "</td>" +
		                    "<td align=\"center\">" +
			                    "<u><b>Valor nuevo</b></u>" +
		                    "</td>" +
	                    "</tr>" +                
	                    "<tr>" +
		                    "<td>" +
			                    "Tipo de cambio:" +
		                    "</td>";
                if(lstValoresOld_C.Count > 0 )
                {
                    sBody = sBody +
		                    "<td>" +
			                    "Compra ( C )" +
		                    "</td>";
                }
                else
                {
                    sBody = sBody +
		                    "<td>" +
			                    " " +
		                    "</td>";
                }
                if(lstValoresNew_C.Count > 0 )
                {
                    sBody = sBody +
		                    "<td>" +
			                    "Compra ( C )" +
		                    "</td>";
                }
                else
                {
                    sBody = sBody +
		                    "<td>" +
			                    " " +
		                    "</td>";
                }
                sBody = sBody +
	                    "</tr>" +
	                    "<tr>" +
		                    "<td>" +
			                    "Moneda:" +
		                    "</td>";
                if(lstValoresOld_C.Count > 0 )
                {
                    sBody = sBody +
		                    "<td>" +
			                    lstValoresOld_C[1] +
		                    "</td>";
                }
                else
                {
                    sBody = sBody +
		                    "<td>" +
			                    " " +
		                    "</td>";
                }
                if(lstValoresNew_C.Count > 0 )
                {
                    sBody = sBody +
		                    "<td>" +
			                    lstValoresNew_C[1] +
		                    "</td>";
                }
                else
                {
                    sBody = sBody +
		                    "<td>" +
			                    " " +
		                    "</td>";
                }
                sBody = sBody +
	                    "</tr>" +
	                    "<tr>" +
		                    "<td>" +
			                    "Valor tasa:" +
		                    "</td>";
                if(lstValoresOld_C.Count > 0 )
                {
                    sBody = sBody +
		                    "<td>" +
			                    lstValoresOld_C[2] +
		                    "</td>";
                }
                else
                {
                    sBody = sBody +
		                    "<td>" +
			                    " " +
		                    "</td>";
                }
                if(lstValoresNew_C.Count > 0 )
                {
                    sBody = sBody +
		                    "<td>" +
			                    lstValoresNew_C[2] +
		                    "</td>";
                }
                else
                {
                    sBody = sBody +
		                    "<td>" +
			                    " " +
		                    "</td>";
                }
                sBody = sBody +
	                    "</tr>" +
	                    "<tr>" +
		                    "<td>" +
			                    lstConceptos[3] +
		                    "</td>";
                if(lstValoresOld_C.Count > 0 )
                {
                    sBody = sBody +
		                    "<td>" +
			                    lstValoresOld_C[3] +
		                    "</td>";
                }
                else
                {
                    sBody = sBody +
		                    "<td>" +
			                    " " +
		                    "</td>";
                }
                if(lstValoresNew_C.Count > 0 )
                {
                    sBody = sBody +
		                    "<td>" +
			                    lstValoresNew_C[3] +
		                    "</td>";
                }
                else
                {
                    sBody = sBody +
		                    "<td>" +
			                    " " +
		                    "</td>";
                }
                sBody = sBody +
	                    "</tr>" +
	                    "<tr>" +
		                    "<td>" +
			                    "Usuario:" +
		                    "</td>";
                if(lstValoresOld_C.Count > 0 )
                {
                    sBody = sBody +
		                    "<td>" +
			                    lstValoresOld_C[4] +
		                    "</td>";
                }
                else
                {
                    sBody = sBody +
		                    "<td>" +
			                    " " +
		                    "</td>";
                }
                if(lstValoresNew_C.Count > 0 )
                {
                    sBody = sBody +
		                    "<td>" +
			                    lstValoresNew_C[4] +
		                    "</td>";
                }
                else
                {
                    sBody = sBody +
		                    "<td>" +
			                    " " +
		                    "</td>";
                }
                 sBody = sBody +
	                    "</tr>";

            }
            #endregion
            #region Venta
            if (lstValoresOld_V.Count > 0 || lstValoresNew_V.Count > 0)
            {
                sBody = sBody + 
                        "<tr>" +
		                    "<td colspan=\"3\" align=\"center\">" +
                                "<b>" + sTitleV + "</b>" +
		                    "</td>" +
	                    "</tr>" +
	                    "<tr>" +
		                    "<td align=\"center\">" +
			                    "<u><b>Concepto</b></u>" +
		                    "</td>" +
		                    "<td align=\"center\">" +
			                    "<u><b>Valor anterior</b></u>" +
		                    "</td>" +
		                    "<td align=\"center\">" +
			                    "<u><b>Valor nuevo</b></u>" +
		                    "</td>" +
	                    "</tr>" +
	                    "<tr>" +
		                    "<td>" +
			                    "Tipo de cambio:" +
		                    "</td>";
                if(lstValoresOld_V.Count > 0 )
                {
                    sBody = sBody +
		                    "<td>" +
			                    "Venta ( V )" +
		                    "</td>";
                }
                else
                {
                    sBody = sBody +
		                    "<td>" +
			                    " " +
		                    "</td>";
                }
                if(lstValoresNew_V.Count > 0 )
                {
                    sBody = sBody +
		                    "<td>" +
			                    "Venta ( V )" +
		                    "</td>";
                }
                else
                {
                    sBody = sBody +
		                    "<td>" +
			                    " " +
		                    "</td>";
                }
                sBody = sBody +
	                    "</tr>" +
	                    "<tr>" +
		                    "<td>" +
			                    "Moneda:" +
		                    "</td>";
                if(lstValoresOld_V.Count > 0 )
                {
                    sBody = sBody +
		                    "<td>" +
			                    lstValoresOld_V[1] +
		                    "</td>";
                }
                else
                {
                    sBody = sBody +
		                    "<td>" +
			                    " " +
		                    "</td>";
                }
                if(lstValoresNew_V.Count > 0 )
                {
                    sBody = sBody +
		                    "<td>" +
			                    lstValoresNew_V[1] +
		                    "</td>";
                }
                else
                {
                    sBody = sBody +
		                    "<td>" +
			                    " " +
		                    "</td>";
                }
                sBody = sBody +
	                    "</tr>" +
	                    "<tr>" +
		                    "<td>" +
			                    "Valor tasa:" +
		                    "</td>";
                if(lstValoresOld_V.Count > 0 )
                {
                    sBody = sBody +
		                    "<td>" +
			                    lstValoresOld_V[2] +
		                    "</td>";
                }
                else
                {
                    sBody = sBody +
		                    "<td>" +
			                    " " +
		                    "</td>";
                }
                if(lstValoresNew_V.Count > 0 )
                {
                    sBody = sBody +
		                    "<td>" +
			                    lstValoresNew_V[2] +
		                    "</td>";
                }
                else
                {
                    sBody = sBody +
		                    "<td>" +
			                    " " +
		                    "</td>";
                }
                sBody = sBody +
	                    "</tr>" +
	                    "<tr>" +
		                    "<td>" +
			                    lstConceptos[3] +
		                    "</td>";
                if(lstValoresOld_V.Count > 0 )
                {
                    sBody = sBody +
		                    "<td>" +
			                    lstValoresOld_V[3] +
		                    "</td>";
                }
                else
                {
                    sBody = sBody +
		                    "<td>" +
			                    " " +
		                    "</td>";
                }
                if(lstValoresNew_V.Count > 0 )
                {
                    sBody = sBody +
		                    "<td>" +
			                    lstValoresNew_V[3] +
		                    "</td>";
                }
                else
                {
                    sBody = sBody +
		                    "<td>" +
			                    " " +
		                    "</td>";
                }
                sBody = sBody +
	                    "</tr>" +
	                    "<tr>" +
		                    "<td>" +
			                    "Usuario:" +
		                    "</td>";
                if(lstValoresOld_V.Count > 0 )
                {
                    sBody = sBody +
		                    "<td>" +
			                    lstValoresOld_V[4] +
		                    "</td>";
                }
                else
                {
                    sBody = sBody +
		                    "<td>" +
			                    " " +
		                    "</td>";
                }
                if(lstValoresNew_V.Count > 0 )
                {
                    sBody = sBody +
		                    "<td>" +
			                    lstValoresNew_V[4] +
		                    "</td>";
                 }
                else
                {
                    sBody = sBody +
		                    "<td>" +
			                    " " +
		                    "</td>";
                }
                sBody = sBody +
                        "</tr>";
            }
            #endregion
            sBody = sBody +
                    "</table>";
            #endregion

            return sBody;
        }

        private static string ObtenerBodyTipoTicket(string sPathConfig, Int64 idTxCritica, 
            string sCodUsuarioModificador, string sUri, int iTTT)
        {
            string sBody = string.Empty;
            oDAO_Auditoria = new DAO_Auditoria(sPathConfig);
            oDAO_Moneda = new DAO_Moneda(sPathConfig);
            oDAO_Usuario = new DAO_Usuario(sPathConfig);
            oDAO_TipoTicket = new DAO_TipoTicket(sPathConfig);

            DataTable dt_AuditoriaTxCrit = oDAO_Auditoria.obtenerRegistrosTransaccionCritica(idTxCritica);

            string xTxRealizada = string.Empty;
            string xOpRealizada = string.Empty;
            //string sTitleV = string.Empty;
            //Concepto
            string[] lstConceptos = new string[5];
            lstConceptos[0] = "Tipo de Ticket:";
            lstConceptos[1] = "Moneda:";
            lstConceptos[2] = "Precio:";
            lstConceptos[3] = string.Empty;
            lstConceptos[4] = "Usuario:";

            ArrayList lstValoresOld = new ArrayList();
            ArrayList lstValoresNew = new ArrayList();

            ArrayList lstValoresOld_TT = new ArrayList();
            ArrayList lstValoresNew_TT = new ArrayList();

            //string sCampoLogRegistro = string.Empty;

            if (iTTT.Equals(1))
            {
                xTxRealizada = "Registro de nuevo Precio de Ticket Programado";
                xOpRealizada = "Inserci&oacute;n";
                //sTitleC = "Datos insertados para tipo de cambio Compra";
                //sTitleV = "Datos insertados para tipo de cambio Venta";
                lstConceptos[3] = "Fecha programada:";
                //sCampoLogRegistro = "Log_Reg_Nuevo";
            }
            else if (iTTT.Equals(2))
            {
                xTxRealizada = "Registro de nuevo Precio de Ticket";
                xOpRealizada = "Inserci&oacute;n";
                //sTitleC = "Datos afectados para tipo de cambio Compra";
                //sTitleV = "Datos afectados para tipo de cambio Venta";
                lstConceptos[3] = "Fecha de inicio de vigencia:";
                //sCampoLogRegistro = "Log_Reg_Nuevo";
            }
            else if (iTTT.Equals(3))
            {
                xTxRealizada = "Eliminación de un Precio de Ticket Programado";
                xOpRealizada = "Eliminaci&oacute;n";
                //sTitleC = "Datos eliminados para tipo de cambio Compra";
                //sTitleV = "Datos eliminados para tipo de cambio Venta";
                lstConceptos[3] = "Fecha programada:";
                //sCampoLogRegistro = "Log_Reg_Orig";
            }

            foreach (DataRow dr in dt_AuditoriaTxCrit.Select("Log_Tabla_Mod = 'TUA_PrecioTicket'", "Cod_Contador ASC"))
            {
                if (dr["Log_Reg_Orig"] != DBNull.Value && !string.IsNullOrEmpty(dr["Log_Reg_Orig"].ToString().Trim()))
                {
                    string[] lstValues = dr["Log_Reg_Orig"].ToString().Split('|');
                    lstValoresOld_TT = new ArrayList();
                    lstValoresOld_TT.Add(lstValues[3].Replace("ID4©", "") + " ( " + oDAO_TipoTicket.obtener(lstValues[3].Replace("ID4©", "")).SNomTipoTicket + " )");
                    lstValoresOld_TT.Add(oDAO_Moneda.obtenerDescripcionMoneda(lstValues[4].Replace("ID5©", "")));
                    lstValoresOld_TT.Add(lstValues[5].Replace("ID6©", ""));
                    lstValoresOld_TT.Add(lstValues[6].Replace("ID7©", ""));
                    lstValoresOld_TT.Add(oDAO_Usuario.obtenerNombreCompletoUsuario(lstValues[10].Replace("ID11©", "")));

                    lstValoresOld.Add(lstValoresOld_TT);

                    if (iTTT.Equals(3))
                    {
                        lstValoresNew_TT = new ArrayList();
                        lstValoresNew.Add(lstValoresNew_TT);
                    }
                }
                else if (dr["Log_Reg_Nuevo"] != DBNull.Value && !string.IsNullOrEmpty(dr["Log_Reg_Nuevo"].ToString().Trim()))
                {
                    if (iTTT.Equals(1))
                    {
                        lstValoresOld_TT = new ArrayList();
                        lstValoresOld.Add(lstValoresOld_TT);
                    }

                    string[] lstValues = dr["Log_Reg_Nuevo"].ToString().Split('|');
                    lstValoresNew_TT = new ArrayList();
                    lstValoresNew_TT.Add(lstValues[3].Replace("ID4©", "") + " ( " + oDAO_TipoTicket.obtener(lstValues[3].Replace("ID4©", "")).SNomTipoTicket + " )");
                    lstValoresNew_TT.Add(oDAO_Moneda.obtenerDescripcionMoneda(lstValues[4].Replace("ID5©", "")));
                    lstValoresNew_TT.Add(lstValues[5].Replace("ID6©", ""));
                    lstValoresNew_TT.Add(lstValues[6].Replace("ID7©", ""));
                    lstValoresNew_TT.Add(oDAO_Usuario.obtenerNombreCompletoUsuario(lstValues[10].Replace("ID11©", "")));

                    lstValoresNew.Add(lstValoresNew_TT);
                }
            }

            #region "Body"
            sBody =
                    "<table border=\"1\" style=\"border-spacing:0\">" +
                        "<tr>" +
                            "<td>" +
                                "<b>Transacci&oacute;n realizada:</b>" +
                            "</td>" +
                            "<td colspan=\"2\">" +
                                xTxRealizada + "<br>( " + sUri + " )" +
                            "</td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>" +
                                "<b>Operaci&oacute;n realizada:</b>" +
                            "</td>" +
                            "<td colspan=\"2\">" +
                                xOpRealizada +
                            "</td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>" +
                                "<b>Usuario modificador:</b>" +
                            "</td>" +
                            "<td colspan=\"2\">" +
                                oDAO_Usuario.obtenerNombreCompletoUsuario(sCodUsuarioModificador) +
                            "</td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>" +
                                "<b>Fecha modificaci&oacute;n:</b>" +
                            "</td>" +
                            "<td colspan=\"2\">" +
                                DateTime.Now.ToString("dd/MM/yyyy HH:mm") +
                            "</td>" +
                        "</tr>";
            #region Tipos de ticket
            for(int i = 0; i < lstValoresOld.Count; i++)
            {
                ArrayList lstValorOld = ((ArrayList)lstValoresOld[i]);
                ArrayList lstValorNew = ((ArrayList)lstValoresNew[i]);

                sBody = sBody +
                        "<tr>" +
                            "<td colspan=\"3\" align=\"center\">" +
                                "<b>" + (lstValorOld.Count > 0 ? lstValorOld[0] : lstValorNew[0]) + "</b>" +
                            "</td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td align=\"center\">" +
                                "<u><b>Concepto</b></u>" +
                            "</td>" +
                            "<td align=\"center\">" +
                                "<u><b>Valor anterior</b></u>" +
                            "</td>" +
                            "<td align=\"center\">" +
                                "<u><b>Valor nuevo</b></u>" +
                            "</td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>" +
                                "Tipo de Ticket:" +
                            "</td>";
                if (lstValorOld.Count > 0)
                {
                    sBody = sBody +
                            "<td>" +
                                lstValorOld[0] +
                            "</td>";
                }
                else
                {
                    sBody = sBody +
                            "<td>" +
                                " " +
                            "</td>";
                }
                if (lstValorNew.Count > 0)
                {
                    sBody = sBody +
                            "<td>" +
                                lstValorNew[0] +
                            "</td>";
                }
                else
                {
                    sBody = sBody +
                            "<td>" +
                                " " +
                            "</td>";
                }
                sBody = sBody +
                        "</tr>" +
                        "<tr>" +
                            "<td>" +
                                "Moneda:" +
                            "</td>";
                if (lstValorOld.Count > 0)
                {
                    sBody = sBody +
                            "<td>" +
                                lstValorOld[1] +
                            "</td>";
                }
                else
                {
                    sBody = sBody +
                            "<td>" +
                                " " +
                            "</td>";
                }
                if (lstValorNew.Count > 0)
                {
                    sBody = sBody +
                            "<td>" +
                                lstValorNew[1] +
                            "</td>";
                }
                else
                {
                    sBody = sBody +
                            "<td>" +
                                " " +
                            "</td>";
                }
                sBody = sBody +
                        "</tr>" +
                        "<tr>" +
                            "<td>" +
                                "Precio:" +
                            "</td>";
                if (lstValorOld.Count > 0)
                {
                    sBody = sBody +
                            "<td>" +
                                lstValorOld[2] +
                            "</td>";
                }
                else
                {
                    sBody = sBody +
                            "<td>" +
                                " " +
                            "</td>";
                }
                if (lstValorNew.Count > 0)
                {
                    sBody = sBody +
                            "<td>" +
                                lstValorNew[2] +
                            "</td>";
                }
                else
                {
                    sBody = sBody +
                            "<td>" +
                                " " +
                            "</td>";
                }
                sBody = sBody +
                        "</tr>" +
                        "<tr>" +
                            "<td>" +
                                lstConceptos[3] +
                            "</td>";
                if (lstValorOld.Count > 0)
                {
                    sBody = sBody +
                            "<td>" +
                                lstValorOld[3] +
                            "</td>";
                }
                else
                {
                    sBody = sBody +
                            "<td>" +
                                " " +
                            "</td>";
                }
                if (lstValorNew.Count > 0)
                {
                    sBody = sBody +
                            "<td>" +
                                lstValorNew[3] +
                            "</td>";
                }
                else
                {
                    sBody = sBody +
                            "<td>" +
                                " " +
                            "</td>";
                }
                sBody = sBody +
                        "</tr>" +
                        "<tr>" +
                            "<td>" +
                                "Usuario:" +
                            "</td>";
                if (lstValorOld.Count > 0)
                {
                    sBody = sBody +
                            "<td>" +
                                lstValorOld[4] +
                            "</td>";
                }
                else
                {
                    sBody = sBody +
                            "<td>" +
                                " " +
                            "</td>";
                }
                if (lstValorNew.Count > 0)
                {
                    sBody = sBody +
                            "<td>" +
                                lstValorNew[4] +
                            "</td>";
                }
                else
                {
                    sBody = sBody +
                            "<td>" +
                                " " +
                            "</td>";
                }
                sBody = sBody +
                       "</tr>";

            }
            #endregion
            
            sBody = sBody +
                    "</table>";
            #endregion

            return sBody;
        }

        private static string ObtenerBodyCampania(string sPathConfig, Int64 idTxCritica, 
            string sCodUsuarioModificador, string sUri, int iTTT)
        {
            string sBody = string.Empty;
            oDAO_Auditoria = new DAO_Auditoria(sPathConfig);
            oDAO_ListaDeCampos = new DAO_ListaDeCampos(sPathConfig);
            oDAO_Usuario = new DAO_Usuario(sPathConfig);
            oDAO_ModalidadVenta = new DAO_ModalidadVenta(sPathConfig);
            oDAO_ParameGeneral = new DAO_ParameGeneral(sPathConfig);
            
            DataTable dt_AuditoriaTxCrit = oDAO_Auditoria.obtenerRegistrosTransaccionCritica(idTxCritica);

            string xTxRealizada = string.Empty;
            string xOpRealizada = string.Empty;
            //string sTitleV = string.Empty;
            //Concepto
            /*string[] lstConceptos = new string[5];
            lstConceptos[0] = "Tipo de Ticket:";
            lstConceptos[1] = "Moneda:";
            lstConceptos[2] = "Precio:";
            lstConceptos[3] = string.Empty;
            lstConceptos[4] = "Usuario:";*/

            ArrayList lstValoresOld_Companias = new ArrayList();
            ArrayList lstValoresNew_Companias = new ArrayList();
            ArrayList lstValoresOld_Cia = new ArrayList();
            ArrayList lstValoresNew_Cia = new ArrayList();

            ArrayList lstValoresOld_Representantes = new ArrayList();
            ArrayList lstValoresNew_Representantes = new ArrayList();
            ArrayList lstValoresOld_Rptte = new ArrayList();
            ArrayList lstValoresNew_Rptte = new ArrayList();

            ArrayList lstPermRepresentante_Concepto = new ArrayList();
            ArrayList lstPermRepresentante_ValuesOld = new ArrayList();
            ArrayList lstPermRepresentante_ValuesNew = new ArrayList();
            ArrayList lstValoresOld_PermRepresentantes = new ArrayList();
            ArrayList lstValoresNew_PermRepresentantes = new ArrayList();

            ArrayList lstValoresOld_ModVentas = new ArrayList();
            ArrayList lstValoresNew_ModVentas = new ArrayList();
            ArrayList lstValoresOld_MVt = new ArrayList();
            ArrayList lstValoresNew_MVt = new ArrayList();

            ArrayList lstConcepto_MVtAtr = new ArrayList();
            ArrayList lstValuesOld_MVtAtr = new ArrayList();
            ArrayList lstValuesNew_MVtAtr = new ArrayList();
            ArrayList lstConcepto_ModVentaAtribs = new ArrayList();
            ArrayList lstValoresOld_ModVentaAtribs = new ArrayList();
            ArrayList lstValoresNew_ModVentaAtribs = new ArrayList();

            //string sCampoLogRegistro = string.Empty;

            if (iTTT.Equals(1))
            {
                xTxRealizada = "Registro de nueva Compañía";
                xOpRealizada = "Inserci&oacute;n";
                //sTitleC = "Datos insertados para tipo de cambio Compra";
                //sTitleV = "Datos insertados para tipo de cambio Venta";
                //lstConceptos[3] = "Fecha programada:";
                //sCampoLogRegistro = "Log_Reg_Nuevo";
            }
            else if (iTTT.Equals(2))
            {
                xTxRealizada = "Actualización de Compañía";
                xOpRealizada = "Actualizaci&oacute;n";
                //sTitleC = "Datos afectados para tipo de cambio Compra";
                //sTitleV = "Datos afectados para tipo de cambio Venta";
                //lstConceptos[3] = "Fecha de inicio de vigencia:";
                //sCampoLogRegistro = "Log_Reg_Nuevo";
            }
            #region Compania
            //Compañia
            foreach (DataRow dr in dt_AuditoriaTxCrit.Select("Log_Tabla_Mod = 'TUA_Compania'", "Cod_Contador ASC"))
            {
                if (dr["Log_Reg_Orig"] != DBNull.Value && !string.IsNullOrEmpty(dr["Log_Reg_Orig"].ToString().Trim()))
                {
                    string[] lstValues = dr["Log_Reg_Orig"].ToString().Split('|');
                    lstValoresOld_Cia = new ArrayList();
                    lstValoresOld_Cia.Add(lstValues[7].Replace("ID8©", ""));
                    lstValoresOld_Cia.Add(oDAO_ListaDeCampos.obtenerDescripcionTipo("TipoCompania", lstValues[6].Replace("ID7©", "")));
                    lstValoresOld_Cia.Add(lstValues[1].Replace("ID2©", ""));
                    lstValoresOld_Cia.Add(oDAO_ListaDeCampos.obtenerDescripcionTipo("EstadoCompania", lstValues[2].Replace("ID3©", "")));
                    lstValoresOld_Cia.Add(lstValues[9].Replace("ID10©", ""));
                    lstValoresOld_Cia.Add(lstValues[13].Replace("ID14©", ""));
                    lstValoresOld_Cia.Add(lstValues[12].Replace("ID13©", ""));
                    lstValoresOld_Cia.Add(lstValues[11].Replace("ID12©", ""));
                    lstValoresOld_Cia.Add(lstValues[10].Replace("ID11©", ""));
                    lstValoresOld_Cia.Add(oDAO_Usuario.obtenerNombreCompletoUsuario(lstValues[3].Replace("ID4©", "")));

                    lstValoresOld_Companias.Add(lstValoresOld_Cia);
                }
                if (dr["Log_Reg_Nuevo"] != DBNull.Value && !string.IsNullOrEmpty(dr["Log_Reg_Nuevo"].ToString().Trim()))
                {
                    if (iTTT.Equals(1))
                    {
                        lstValoresOld_Cia = new ArrayList();
                        lstValoresOld_Companias.Add(lstValoresOld_Cia);
                    }

                    string[] lstValues = dr["Log_Reg_Nuevo"].ToString().Split('|');
                    lstValoresNew_Cia = new ArrayList();
                    lstValoresNew_Cia.Add(lstValues[7].Replace("ID8©", ""));
                    lstValoresNew_Cia.Add(oDAO_ListaDeCampos.obtenerDescripcionTipo("TipoCompania", lstValues[6].Replace("ID7©", "")));
                    lstValoresNew_Cia.Add(lstValues[1].Replace("ID2©", ""));
                    lstValoresNew_Cia.Add(oDAO_ListaDeCampos.obtenerDescripcionTipo("EstadoCompania", lstValues[2].Replace("ID3©", "")));
                    lstValoresNew_Cia.Add(lstValues[9].Replace("ID10©", ""));
                    lstValoresNew_Cia.Add(lstValues[13].Replace("ID14©", ""));
                    lstValoresNew_Cia.Add(lstValues[12].Replace("ID13©", ""));
                    lstValoresNew_Cia.Add(lstValues[11].Replace("ID12©", ""));
                    lstValoresNew_Cia.Add(lstValues[10].Replace("ID11©", ""));
                    lstValoresNew_Cia.Add(oDAO_Usuario.obtenerNombreCompletoUsuario(lstValues[3].Replace("ID4©", "")));

                    lstValoresNew_Companias.Add(lstValoresNew_Cia);
                }
            }
            #endregion

            #region Representante
            //Representante
            List<ListaDeCampo> objListListadeCampo = new List<ListaDeCampo>();
            objListListadeCampo = oDAO_ListaDeCampos.obtenerListadeCampo("PermRepresentante");

            for (int i = 0; i < objListListadeCampo.Count; i++)
            {
                lstPermRepresentante_Concepto.Add(objListListadeCampo[i].SDscCampo);
            }

            foreach (DataRow dr in dt_AuditoriaTxCrit.Select("Log_Tabla_Mod = 'TUA_RepresentantCia'", "Cod_Contador ASC"))
            {
                if (dr["Log_Reg_Orig"] != DBNull.Value && !string.IsNullOrEmpty(dr["Log_Reg_Orig"].ToString().Trim()))
                {
                    string[] lstValues = dr["Log_Reg_Orig"].ToString().Split('|');
                    lstValoresOld_Rptte = new ArrayList();
                    lstValoresOld_Rptte.Add(lstValues[5].Replace("ID6©", ""));
                    lstValoresOld_Rptte.Add(lstValues[7].Replace("ID8©", ""));
                    lstValoresOld_Rptte.Add(lstValues[8].Replace("ID9©", ""));
                    lstValoresOld_Rptte.Add(lstValues[9].Replace("ID10©", ""));
                    lstValoresOld_Rptte.Add(oDAO_ListaDeCampos.obtenerDescripcionTipo("EstadoRegistro", lstValues[11].Replace("ID12©", "")));
                    string sPermRepresentante = lstValues[10].Replace("ID11©", "");
                    lstPermRepresentante_ValuesNew = new ArrayList();
                    string valor;
                    for (int j = 0; j < objListListadeCampo.Count; j++)
                    {
                        if (Convert.ToInt32(sPermRepresentante) == 0)
                        {
                            valor = "0";
                        }
                        else
                        {
                            string conversion = DecimalToBase(Convert.ToInt32(sPermRepresentante), 2);
                            conversion = conversion.PadLeft(objListListadeCampo.Count, '0');
                            valor = conversion.Substring(j, 1);
                        }

                        if (valor == "1")
                            lstPermRepresentante_ValuesOld.Add("Si");
                        else
                            lstPermRepresentante_ValuesOld.Add("No");
                    }

                    lstValoresOld_Representantes.Add(lstValoresOld_Rptte);
                    lstValoresOld_PermRepresentantes.Add(lstPermRepresentante_ValuesOld);
                }
                if (dr["Log_Reg_Nuevo"] != DBNull.Value && !string.IsNullOrEmpty(dr["Log_Reg_Nuevo"].ToString().Trim()))
                {
                    if (iTTT.Equals(1))
                    {
                        lstValoresOld_Rptte = new ArrayList();
                        lstValoresOld_Representantes.Add(lstValoresOld_Rptte);

                        lstPermRepresentante_ValuesOld = new ArrayList();
                        lstValoresOld_PermRepresentantes.Add(lstPermRepresentante_ValuesOld);
                    }

                    string[] lstValues = dr["Log_Reg_Nuevo"].ToString().Split('|');
                    lstValoresNew_Rptte = new ArrayList();
                    lstValoresNew_Rptte.Add(lstValues[5].Replace("ID6©", ""));
                    lstValoresNew_Rptte.Add(lstValues[7].Replace("ID8©", ""));
                    lstValoresNew_Rptte.Add(lstValues[8].Replace("ID9©", ""));
                    lstValoresNew_Rptte.Add(lstValues[9].Replace("ID10©", ""));
                    lstValoresNew_Rptte.Add(oDAO_ListaDeCampos.obtenerDescripcionTipo("EstadoRegistro", lstValues[11].Replace("ID12©", "")));
                    string sPermRepresentante = lstValues[10].Replace("ID11©", "");
                    lstPermRepresentante_ValuesNew = new ArrayList();
                    string valor;
                    for (int j = 0; j < objListListadeCampo.Count; j++)
                    {
                        if (Convert.ToInt32(sPermRepresentante) == 0)
                        {
                            valor = "0";
                        }
                        else
                        {
                            string conversion = DecimalToBase(Convert.ToInt32(sPermRepresentante), 2);
                            conversion = conversion.PadLeft(objListListadeCampo.Count, '0');
                            valor = conversion.Substring(j, 1);
                        }

                        if (valor == "1")
                            lstPermRepresentante_ValuesNew.Add("Si");
                        else
                            lstPermRepresentante_ValuesNew.Add("No");
                    }

                    lstValoresNew_Representantes.Add(lstValoresNew_Rptte);
                    lstValoresNew_PermRepresentantes.Add(lstPermRepresentante_ValuesNew);
                }
            }
            #endregion

            #region Modulo Venta
            //Modulo Venta
            foreach (DataRow dr in dt_AuditoriaTxCrit.Select("Log_Tabla_Mod = 'TUA_ModVentaComp'", "Cod_Contador ASC"))
            {
                /*if (dr["Log_Reg_Orig"] != DBNull.Value && !string.IsNullOrEmpty(dr["Log_Reg_Orig"].ToString().Trim()))
                {
                    string[] lstValues = dr["Log_Reg_Orig"].ToString().Split('|');

                    lstValoresOld_MVt = new ArrayList();
                    ModalidadVenta oModalidadVenta = oDAO_ModalidadVenta.obtenerxCodigo(lstValues[1].Replace("ID2©", ""));
                    lstValoresOld_MVt.Add(oModalidadVenta.SNomModalidad);//

                    //lstConcepto_MVtAtr = new ArrayList(); //????
                    lstValuesOld_MVtAtr = new ArrayList();

                    foreach (DataRow drA in dt_AuditoriaTxCrit.Select("Log_Tabla_Mod = 'TUA_ModVentaCompAtr' AND Log_Reg_Orig LIKE '%ID3©" + lstValues[1].Replace("ID2©", "") + "%'", "Cod_Contador ASC"))
                    {
                        if (drA["Log_Reg_Orig"] != DBNull.Value && !string.IsNullOrEmpty(drA["Log_Reg_Orig"].ToString().Trim()))
                        {
                            string[] lstValuesA = drA["Log_Reg_Orig"].ToString().Split('|');
                            ParameGeneral oParameGeneral = oDAO_ParameGeneral.obtener(lstValuesA[4].Replace("ID5©", ""));
                            lstConcepto_MVtAtr.Add(oParameGeneral.SDescripcion);//
                            lstValuesOld_MVtAtr.Add(lstValuesA[7].Replace("ID8©", ""));
                        }
                        /*if (drA["Log_Reg_Nuevo"] != DBNull.Value && !string.IsNullOrEmpty(drA["Log_Reg_Nuevo"].ToString().Trim()))
                        {                            
                        }*/
                    /*}

                    lstValoresOld_ModVentas.Add(lstValoresOld_MVt);
                    lstConcepto_ModVentaAtribs.Add(lstConcepto_MVtAtr);
                    lstValoresOld_ModVentaAtribs.Add(lstValuesOld_MVtAtr);
                }*/
                //if (dr["Log_Reg_Nuevo"] != DBNull.Value && !string.IsNullOrEmpty(dr["Log_Reg_Nuevo"].ToString().Trim()))
                {
                    if (iTTT.Equals(1))
                    {
                        lstValoresOld_MVt = new ArrayList();
                        //lstConcepto_MVtAtr = new ArrayList();
                        lstValuesOld_MVtAtr = new ArrayList();

                        lstValoresOld_ModVentas.Add(lstValoresOld_MVt);
                        //lstConcepto_ModVentaAtribs.Add(lstConcepto_MVtAtr);
                        lstValoresOld_ModVentaAtribs.Add(lstValuesOld_MVtAtr);
                    }

                    string[] lstValues = dr["Log_Reg_Nuevo"].ToString().Split('|');

                    lstValoresNew_MVt = new ArrayList();
                    ModalidadVenta oModalidadVenta = oDAO_ModalidadVenta.obtenerxCodigo(lstValues[1].Replace("ID2©", ""));
                    lstValoresNew_MVt.Add(oModalidadVenta.SNomModalidad);//

                    lstConcepto_MVtAtr = new ArrayList();
                    lstValuesOld_MVtAtr = new ArrayList();
                    lstValuesNew_MVtAtr = new ArrayList();

                    foreach (DataRow drA in dt_AuditoriaTxCrit.Select("Log_Tabla_Mod = 'TUA_ModVentaCompAtr' AND Log_Reg_Nuevo LIKE '%ID3©" + lstValues[1].Replace("ID2©", "") + "%'", "Cod_Contador ASC"))
                    {
                        string sDescripcionModVentAtr = string.Empty;
                        if (drA["Log_Reg_Orig"] != DBNull.Value && !string.IsNullOrEmpty(drA["Log_Reg_Orig"].ToString().Trim()))
                        {
                            string[] lstValuesA = drA["Log_Reg_Orig"].ToString().Split('|');
                            ParameGeneral oParameGeneral = oDAO_ParameGeneral.obtener(lstValuesA[4].Replace("ID5©", ""));
                            //lstConcepto_MVtAtr.Add(oParameGeneral.SDescripcion);//
                            sDescripcionModVentAtr = oParameGeneral.SDescripcion;
                            lstValuesOld_MVtAtr.Add(lstValuesA[7].Replace("ID8©", ""));
                        }
                        else
                        {
                            if (iTTT.Equals(2))
                            {
                                lstValuesOld_MVtAtr.Add("");
                            }
                        }

                        if (drA["Log_Reg_Nuevo"] != DBNull.Value && !string.IsNullOrEmpty(drA["Log_Reg_Nuevo"].ToString().Trim()))
                        {
                            string[] lstValuesA = drA["Log_Reg_Nuevo"].ToString().Split('|');
                            ParameGeneral oParameGeneral = oDAO_ParameGeneral.obtener(lstValuesA[4].Replace("ID5©", ""));
                            //lstConcepto_MVtAtr.Add(oParameGeneral.SDescripcion);//
                            sDescripcionModVentAtr = oParameGeneral.SDescripcion;
                            lstValuesNew_MVtAtr.Add(lstValuesA[7].Replace("ID8©", ""));
                        }
                        else
                        {
                            if (iTTT.Equals(2))
                            {
                                lstValuesNew_MVtAtr.Add("");
                            }
                        }

                        lstConcepto_MVtAtr.Add(sDescripcionModVentAtr);//
                    }
                    
                    lstValoresNew_ModVentas.Add(lstValoresNew_MVt);
                    lstConcepto_ModVentaAtribs.Add(lstConcepto_MVtAtr);
                    lstValoresNew_ModVentaAtribs.Add(lstValuesNew_MVtAtr);
                    if (iTTT.Equals(2))
                    {
                        lstValoresOld_ModVentaAtribs.Add(lstValuesOld_MVtAtr);
                    }
                }
            }
            #endregion


            #region "Body"
            sBody =
                    "<table border=\"1\" style=\"border-spacing:0\">" +
                        "<tr>" +
                            "<td>" +
                                "<b>Transacci&oacute;n realizada:</b>" +
                            "</td>" +
                            "<td colspan=\"2\">" +
                                xTxRealizada + "<br>( " + sUri + " )" +
                            "</td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>" +
                                "<b>Operaci&oacute;n realizada:</b>" +
                            "</td>" +
                            "<td colspan=\"2\">" +
                                xOpRealizada +
                            "</td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>" +
                                "<b>Usuario modificador:</b>" +
                            "</td>" +
                            "<td colspan=\"2\">" +
                                oDAO_Usuario.obtenerNombreCompletoUsuario(sCodUsuarioModificador) +
                            "</td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>" +
                                "<b>Fecha modificaci&oacute;n:</b>" +
                            "</td>" +
                            "<td colspan=\"2\">" +
                                DateTime.Now.ToString("dd/MM/yyyy HH:mm") +
                            "</td>" +
                        "</tr>";
            #region Compania
            for(int i = 0; i < lstValoresOld_Companias.Count; i++)
            {
                ArrayList lstValorOld = ((ArrayList)lstValoresOld_Companias[i]);
                ArrayList lstValorNew = ((ArrayList)lstValoresNew_Companias[i]);

                sBody = sBody +
                        "<tr>" +
                            "<td colspan=\"3\" align=\"center\">" +
                                "<b>Datos afectado de Compañía</b>" +
                            "</td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td align=\"center\">" +
                                "<u><b>Concepto</b></u>" +
                            "</td>" +
                            "<td align=\"center\">" +
                                "<u><b>Valor anterior</b></u>" +
                            "</td>" +
                            "<td align=\"center\">" +
                                "<u><b>Valor nuevo</b></u>" +
                            "</td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>" +
                                "Nombre compañía:" +
                            "</td>";
                if (lstValorOld.Count > 0)
                {
                    sBody = sBody +
                            "<td>" +
                                lstValorOld[0] +
                            "</td>";
                }
                else
                {
                    sBody = sBody +
                            "<td>" +
                                " " +
                            "</td>";
                }
                if (lstValorNew.Count > 0)
                {
                    sBody = sBody +
                            "<td>" +
                                lstValorNew[0] +
                            "</td>";
                }
                else
                {
                    sBody = sBody +
                            "<td>" +
                                " " +
                            "</td>";
                }
                sBody = sBody +
                        "</tr>" +
                        "<tr>" +
                            "<td>" +
                                "Tipo compañia:" +
                            "</td>";
                if (lstValorOld.Count > 0)
                {
                    sBody = sBody +
                            "<td>" +
                                lstValorOld[1] +
                            "</td>";
                }
                else
                {
                    sBody = sBody +
                            "<td>" +
                                " " +
                            "</td>";
                }
                if (lstValorNew.Count > 0)
                {
                    sBody = sBody +
                            "<td>" +
                                lstValorNew[1] +
                            "</td>";
                }
                else
                {
                    sBody = sBody +
                            "<td>" +
                                " " +
                            "</td>";
                }
                sBody = sBody +
                        "</tr>" +
                        "<tr>" +
                            "<td>" +
                                "RUC:" +
                            "</td>";
                if (lstValorOld.Count > 0)
                {
                    sBody = sBody +
                            "<td>" +
                                lstValorOld[2] +
                            "</td>";
                }
                else
                {
                    sBody = sBody +
                            "<td>" +
                                " " +
                            "</td>";
                }
                if (lstValorNew.Count > 0)
                {
                    sBody = sBody +
                            "<td>" +
                                lstValorNew[2] +
                            "</td>";
                }
                else
                {
                    sBody = sBody +
                            "<td>" +
                                " " +
                            "</td>";
                }
                sBody = sBody +
                        "</tr>" +
                        "<tr>" +
                            "<td>" +
                                "Estado:" +
                            "</td>";
                if (lstValorOld.Count > 0)
                {
                    sBody = sBody +
                            "<td>" +
                                lstValorOld[3] +
                            "</td>";
                }
                else
                {
                    sBody = sBody +
                            "<td>" +
                                " " +
                            "</td>";
                }
                if (lstValorNew.Count > 0)
                {
                    sBody = sBody +
                            "<td>" +
                                lstValorNew[3] +
                            "</td>";
                }
                else
                {
                    sBody = sBody +
                            "<td>" +
                                " " +
                            "</td>";
                }
                sBody = sBody +
                        "</tr>" +
                        "<tr>" +
                            "<td>" +
                                "Código de Aerolínea:" +
                            "</td>";
                if (lstValorOld.Count > 0)
                {
                    sBody = sBody +
                            "<td>" +
                                lstValorOld[4] +
                            "</td>";
                }
                else
                {
                    sBody = sBody +
                            "<td>" +
                                " " +
                            "</td>";
                }
                if (lstValorNew.Count > 0)
                {
                    sBody = sBody +
                            "<td>" +
                                lstValorNew[4] +
                            "</td>";
                }
                else
                {
                    sBody = sBody +
                            "<td>" +
                                " " +
                            "</td>";
                }
                sBody = sBody +
                        "</tr>" +
                        "<tr>" +
                            "<td>" +
                                "Código IATA:" +
                            "</td>";
                if (lstValorOld.Count > 0)
                {
                    sBody = sBody +
                            "<td>" +
                                lstValorOld[5] +
                            "</td>";
                }
                else
                {
                    sBody = sBody +
                            "<td>" +
                                " " +
                            "</td>";
                }
                if (lstValorNew.Count > 0)
                {
                    sBody = sBody +
                            "<td>" +
                                lstValorNew[5] +
                            "</td>";
                }
                else
                {
                    sBody = sBody +
                            "<td>" +
                                " " +
                            "</td>";
                }
                sBody = sBody +
                        "</tr>" +
                        "<tr>" +
                            "<td>" +
                                "Código OACI:" +
                            "</td>";
                if (lstValorOld.Count > 0)
                {
                    sBody = sBody +
                            "<td>" +
                                lstValorOld[6] +
                            "</td>";
                }
                else
                {
                    sBody = sBody +
                            "<td>" +
                                " " +
                            "</td>";
                }
                if (lstValorNew.Count > 0)
                {
                    sBody = sBody +
                            "<td>" +
                                lstValorNew[6] +
                            "</td>";
                }
                else
                {
                    sBody = sBody +
                            "<td>" +
                                " " +
                            "</td>";
                }
                sBody = sBody +
                        "</tr>" +
                        "<tr>" +
                            "<td>" +
                                "Código SAP:" +
                            "</td>";
                if (lstValorOld.Count > 0)
                {
                    sBody = sBody +
                            "<td>" +
                                lstValorOld[7] +
                            "</td>";
                }
                else
                {
                    sBody = sBody +
                            "<td>" +
                                " " +
                            "</td>";
                }
                if (lstValorNew.Count > 0)
                {
                    sBody = sBody +
                            "<td>" +
                                lstValorNew[7] +
                            "</td>";
                }
                else
                {
                    sBody = sBody +
                            "<td>" +
                                " " +
                            "</td>";
                }
                sBody = sBody +
                        "</tr>" +
                        "<tr>" +
                            "<td>" +
                                "Código Interno:" +
                            "</td>";
                if (lstValorOld.Count > 0)
                {
                    sBody = sBody +
                            "<td>" +
                                lstValorOld[8] +
                            "</td>";
                }
                else
                {
                    sBody = sBody +
                            "<td>" +
                                " " +
                            "</td>";
                }
                if (lstValorNew.Count > 0)
                {
                    sBody = sBody +
                            "<td>" +
                                lstValorNew[8] +
                            "</td>";
                }
                else
                {
                    sBody = sBody +
                            "<td>" +
                                " " +
                            "</td>";
                }
                sBody = sBody +
                        "</tr>" +
                        "<tr>" +
                            "<td>" +
                                "Usuario:" +
                            "</td>";
                if (lstValorOld.Count > 0)
                {
                    sBody = sBody +
                            "<td>" +
                                lstValorOld[9] +
                            "</td>";
                }
                else
                {
                    sBody = sBody +
                            "<td>" +
                                " " +
                            "</td>";
                }
                if (lstValorNew.Count > 0)
                {
                    sBody = sBody +
                            "<td>" +
                                lstValorNew[9] +
                            "</td>";
                }
                else
                {
                    sBody = sBody +
                            "<td>" +
                                " " +
                            "</td>";
                }
                sBody = sBody +
                       "</tr>";

            }
            #endregion

            #region Representantes

            sBody = sBody +
                        "<tr>" +
                            "<td colspan=\"3\" align=\"center\">" +
                                "<b>Datos afectado de Representantes</b>" +
                            "</td>" +
                        "</tr>";

            for (int i = 0; i < lstValoresOld_Representantes.Count; i++)
            {
                ArrayList lstValorOld = ((ArrayList)lstValoresOld_Representantes[i]);
                ArrayList lstValorNew = ((ArrayList)lstValoresNew_Representantes[i]);

                ArrayList lstValorOldPermRep = ((ArrayList)lstValoresOld_PermRepresentantes[i]);
                ArrayList lstValorNewPermRep = ((ArrayList)lstValoresNew_PermRepresentantes[i]);

                if (i != 0)
                {
                    sBody = sBody +
                        "<tr>" +
                            "<td colspan=\"3\" align=\"center\">" +
                                "<b>...</b>" +
                            "</td>" +
                        "</tr>";
                }
                sBody = sBody +
                        "<tr>" +
                            "<td align=\"center\">" +
                                "<u><b>Concepto</b></u>" +
                            "</td>" +
                            "<td align=\"center\">" +
                                "<u><b>Valor anterior</b></u>" +
                            "</td>" +
                            "<td align=\"center\">" +
                                "<u><b>Valor nuevo</b></u>" +
                            "</td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>" +
                                "Nombres:" +
                            "</td>";
                if (lstValorOld.Count > 0)
                {
                    sBody = sBody +
                            "<td>" +
                                lstValorOld[0] +
                            "</td>";
                }
                else
                {
                    sBody = sBody +
                            "<td>" +
                                " " +
                            "</td>";
                }
                if (lstValorNew.Count > 0)
                {
                    sBody = sBody +
                            "<td>" +
                                lstValorNew[0] +
                            "</td>";
                }
                else
                {
                    sBody = sBody +
                            "<td>" +
                                " " +
                            "</td>";
                }
                sBody = sBody +
                        "</tr>" +
                        "<tr>" +
                            "<td>" +
                                "Cargo:" +
                            "</td>";
                if (lstValorOld.Count > 0)
                {
                    sBody = sBody +
                            "<td>" +
                                lstValorOld[1] +
                            "</td>";
                }
                else
                {
                    sBody = sBody +
                            "<td>" +
                                " " +
                            "</td>";
                }
                if (lstValorNew.Count > 0)
                {
                    sBody = sBody +
                            "<td>" +
                                lstValorNew[1] +
                            "</td>";
                }
                else
                {
                    sBody = sBody +
                            "<td>" +
                                " " +
                            "</td>";
                }
                sBody = sBody +
                        "</tr>" +
                        "<tr>" +
                            "<td>" +
                                "Tipo Documento:" +
                            "</td>";
                if (lstValorOld.Count > 0)
                {
                    sBody = sBody +
                            "<td>" +
                                lstValorOld[2] +
                            "</td>";
                }
                else
                {
                    sBody = sBody +
                            "<td>" +
                                " " +
                            "</td>";
                }
                if (lstValorNew.Count > 0)
                {
                    sBody = sBody +
                            "<td>" +
                                lstValorNew[2] +
                            "</td>";
                }
                else
                {
                    sBody = sBody +
                            "<td>" +
                                " " +
                            "</td>";
                }
                sBody = sBody +
                        "</tr>" +
                        "<tr>" +
                            "<td>" +
                                "Número Documento:" +
                            "</td>";
                if (lstValorOld.Count > 0)
                {
                    sBody = sBody +
                            "<td>" +
                                lstValorOld[3] +
                            "</td>";
                }
                else
                {
                    sBody = sBody +
                            "<td>" +
                                " " +
                            "</td>";
                }
                if (lstValorNew.Count > 0)
                {
                    sBody = sBody +
                            "<td>" +
                                lstValorNew[3] +
                            "</td>";
                }
                else
                {
                    sBody = sBody +
                            "<td>" +
                                " " +
                            "</td>";
                }
                sBody = sBody +
                        "</tr>" +
                        "<tr>" +
                            "<td>" +
                                "Estado:" +
                            "</td>";
                if (lstValorOld.Count > 0)
                {
                    sBody = sBody +
                            "<td>" +
                                lstValorOld[4] +
                            "</td>";
                }
                else
                {
                    sBody = sBody +
                            "<td>" +
                                " " +
                            "</td>";
                }
                if (lstValorNew.Count > 0)
                {
                    sBody = sBody +
                            "<td>" +
                                lstValorNew[4] +
                            "</td>";
                }
                else
                {
                    sBody = sBody +
                            "<td>" +
                                " " +
                            "</td>";
                }

                for (int k = 0; k < lstValorNewPermRep.Count; k++)
                {
                    sBody = sBody +
                            "</tr>" +
                            "<tr>" +
                                "<td>" +
                                    lstPermRepresentante_Concepto[k] +
                                "</td>";
                    if (lstValorOldPermRep.Count > 0)
                    {
                        sBody = sBody +
                                "<td>" +
                                    lstValorOldPermRep[k] +
                                "</td>";
                    }
                    else
                    {
                        sBody = sBody +
                                "<td>" +
                                    " " +
                                "</td>";
                    }
                    if (lstValorNewPermRep.Count > 0)
                    {
                        sBody = sBody +
                                "<td>" +
                                    lstValorNewPermRep[k] +
                                "</td>";
                    }
                    else
                    {
                        sBody = sBody +
                                "<td>" +
                                    " " +
                                "</td>";
                    }
                }
                sBody = sBody +
                       "</tr>";

            }
            #endregion

            #region Modalidad Venta

            /*sBody = sBody +
                        "<tr>" +
                            "<td colspan=\"3\" align=\"center\">" +
                                "<b>Datos afectado de Modalidad de Venta</b>" +
                            "</td>" +
                        "</tr>";*/

            for (int i = 0; i < lstValoresNew_ModVentas.Count; i++)
            {
                //ArrayList lstValorOld = ((ArrayList)lstValoresOld_ModVentas[i]);
                ArrayList lstValorNew = ((ArrayList)lstValoresNew_ModVentas[i]);

                ArrayList lstConceptoMVAtr = ((ArrayList)lstConcepto_ModVentaAtribs[i]);

                ArrayList lstValorOldMVAtr = ((ArrayList)lstValoresOld_ModVentaAtribs[i]);
                ArrayList lstValorNewMVAtr = ((ArrayList)lstValoresNew_ModVentaAtribs[i]);

                //if (i != 0)
                {
                    sBody = sBody +
                        "<tr>" +
                            "<td colspan=\"3\" align=\"center\">" +
                                "<b>Datos afectado de Modalidad de Venta: " + lstValorNew[0] + "</b>" +
                            "</td>" +
                        "</tr>";
                }
                sBody = sBody +
                        "<tr>" +
                            "<td align=\"center\">" +
                                "<u><b>Concepto</b></u>" +
                            "</td>" +
                            "<td align=\"center\">" +
                                "<u><b>Valor anterior</b></u>" +
                            "</td>" +
                            "<td align=\"center\">" +
                                "<u><b>Valor nuevo</b></u>" +
                            "</td>";
                /*sBody = sBody +
                        "</tr>" +
                        "<tr>" +
                            "<td>" +
                                "Nombres:" +
                            "</td>";
                if (lstValorOld.Count > 0)
                {
                    sBody = sBody +
                            "<td>" +
                                lstValorOld[0] +
                            "</td>";
                }
                else
                {
                    sBody = sBody +
                            "<td>" +
                                " " +
                            "</td>";
                }
                if (lstValorNew.Count > 0)
                {
                    sBody = sBody +
                            "<td>" +
                                lstValorNew[0] +
                            "</td>";
                }
                else
                {
                    sBody = sBody +
                            "<td>" +
                                " " +
                            "</td>";
                }*/
                
                for (int k = 0; k < lstValorNewMVAtr.Count; k++)
                {
                    sBody = sBody +
                            "</tr>" +
                            "<tr>" +
                                "<td>" +
                                    lstConceptoMVAtr[k] +
                                "</td>";
                    if (lstValorOldMVAtr.Count > 0)
                    {
                        sBody = sBody +
                                "<td>" +
                                    lstValorOldMVAtr[k] +
                                "</td>";
                    }
                    else
                    {
                        sBody = sBody +
                                "<td>" +
                                    " " +
                                "</td>";
                    }
                    if (lstValorNewMVAtr.Count > 0)
                    {
                        sBody = sBody +
                                "<td>" +
                                    lstValorNewMVAtr[k] +
                                "</td>";
                    }
                    else
                    {
                        sBody = sBody +
                                "<td>" +
                                    " " +
                                "</td>";
                    }
                }
                sBody = sBody +
                       "</tr>";

            }
            #endregion
            
            sBody = sBody +
                    "</table>";
            #endregion

            return sBody;
        }
        

        private static void EnviarAlarma(AlarmaGenerada objAlarmaGenerada)
        {
            objDAOCnfgAlarma = new DAO_CnfgAlarma(sPathConfig);
            CnfgAlarma ObjCnfgAlarma = new CnfgAlarma();
            ObjCnfgAlarma = objDAOCnfgAlarma.obtener(objAlarmaGenerada.SCodAlarma, objAlarmaGenerada.SCodModulo);
            EnviarEmail(ObtenerDestinatarios(ObjCnfgAlarma.SDscDestinatarios), objAlarmaGenerada);
        }

        private static DataTable ObtenerDestinatarios(string sXmlDestinatarios)
        {
            DataTable dtTablaSeleccion;
            dtTablaSeleccion = new DataTable();
            dtTablaSeleccion.Columns.Add("Cod_Usuario", System.Type.GetType("System.String"));
            dtTablaSeleccion.Columns.Add("Dsc_Email", System.Type.GetType("System.String"));

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(sXmlDestinatarios);

            XmlElement root = xmlDoc.DocumentElement;
            XmlNodeList elemList = root.GetElementsByTagName("user");

            int i = 0;
            foreach (XmlNode node in elemList)
            {
                DataRow rowSeleccion = dtTablaSeleccion.NewRow();
                dtTablaSeleccion.Rows.Add(rowSeleccion);

                dtTablaSeleccion.Rows[i]["Cod_Usuario"] = node.SelectSingleNode("code").InnerText;
                dtTablaSeleccion.Rows[i]["Dsc_Email"] = node.SelectSingleNode("mail").InnerText;
                i++;

            }

            return dtTablaSeleccion;

        }

        private static bool EnviarEmail(DataTable dtDestinatarios, AlarmaGenerada objAlarmaGenerada)
        {
            Hashtable hsConfig = new Hashtable();
            objDAOParameGeneral = new DAO_ParameGeneral(sPathConfig);
            hsConfig = ConfigCorreo(objDAOParameGeneral.obtener("CC").SValor);

            string sRemitente = (string)hsConfig["remitente"];
            string sServidor = (string)hsConfig["servidor"];
            int iPuerto = Convert.ToInt32(hsConfig["puerto"]);
            string sUser = (string)hsConfig["user"];
            string sPwd = (string)hsConfig["pwd"];
            string sNombre = (string)hsConfig["nombre"];
            bool bSSL = Convert.ToBoolean(hsConfig["SSL"]);
            string sSubject = objAlarmaGenerada.SDscSubject;
            string sBody = objAlarmaGenerada.SDscBody;

            MailMessage message = new MailMessage();

            message.From = new MailAddress(sRemitente, sNombre, System.Text.Encoding.UTF8);

            for (int i = 0; i < dtDestinatarios.Rows.Count; i++)
            {
                message.To.Add(new MailAddress((string)dtDestinatarios.Rows[i]["Dsc_Email"]));
            }

            message.Subject = sSubject;
            message.SubjectEncoding = System.Text.Encoding.UTF8;
            message.Body = sBody;
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.IsBodyHtml = true;


            SmtpClient smtpMail = new SmtpClient();
            smtpMail.EnableSsl = bSSL;
            smtpMail.UseDefaultCredentials = false;
            smtpMail.Host = sServidor;
            smtpMail.Port = iPuerto;
            smtpMail.Credentials = new NetworkCredential(sUser, sPwd);

            try
            {
                smtpMail.Send(message);
                ConfirmarCorreo(objAlarmaGenerada, "1");
                flagError = false;
                return true;
            }
            catch (Exception ex)
            {
                flagError = true;
            }
            finally
            { 
                if(flagError)
                    ConfirmarCorreo(objAlarmaGenerada, "2");
            }
            return true;
        }

        private static void ConfirmarCorreo(AlarmaGenerada objAlarmaGenerada, string sFlagEmail)
        {
            try
            {
                objDAOAlarmaGenerada = new DAO_AlarmaGenerada(sPathConfig);
                AlarmaGenerada objAlarmaRegistrada = new AlarmaGenerada();
                objAlarmaRegistrada = objDAOAlarmaGenerada.ObtenerAlarmaGenerada(objAlarmaGenerada.SCodAlarma, objAlarmaGenerada.SCodModulo, objAlarmaGenerada.SDscEquipo, objAlarmaGenerada.SLogUsuarioMod);
                objAlarmaRegistrada.SFlgEstadoMail = sFlagEmail;

                objDAOAlarmaGenerada.actualizar(objAlarmaRegistrada);

            }
            catch (Exception ex)
            {
                //throw;
            }
        }


        private static Hashtable ConfigCorreo(string SParametros)
        {
            Hashtable Congif = new Hashtable();

            string[] ListaParametros = new string[7];

            ListaParametros = SParametros.Split(';');

            string[] Lista;
            for (int i = 0; i < ListaParametros.Length; i++)
            {
                Lista = new string[2];
                Lista = ListaParametros[i].Split('=');
                Congif.Add(Lista[0], Lista[1]);
            }
            return Congif;
        }

    }
}
