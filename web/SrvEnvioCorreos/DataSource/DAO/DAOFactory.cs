using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;
using System.Reflection;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.OracleClient;

namespace BMatic.DA
{
    public class DAOFactory
    {
        private TipoServBaseDatos ServerBD;
        private string sCadenaConexion;
        private IDAO objDataBase;
        private Sistema sistema;

        public DAOFactory(Sistema sistema)
        {
            this.sistema = sistema;
        }

        public DAOFactory(TipoBaseDatos sTipoBD, Sistema sistema)
        {
            try
            {
                this.sistema = sistema;
                if (sTipoBD.Equals(TipoBaseDatos.BDReportes))
                {
                    sCadenaConexion = this.strGetCadenaDeConexionReporte();
                }
                else if (sTipoBD.Equals(TipoBaseDatos.BDProduccion))
                {
                    sCadenaConexion = this.strGetCadenaDeConexionProduccion();
                }
            }
            catch (Exception ex)
            {
                //log.Error("Error en obtener cadena de conexion:",ex);
                throw ex;
            }
        }

        public IDAO ObtenerDataBase()
        {
            try
            {
                if (ServerBD.Equals(TipoServBaseDatos.ServBDSqlServer))
                {
                    objDataBase = new DAOSqlServer(sCadenaConexion);
                }
                else if (ServerBD.Equals(TipoServBaseDatos.ServBDOracle))
                {
                    objDataBase = new DAOOracle(sCadenaConexion);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return objDataBase;
        }

        public IDbCommand obtenerComand()
        {
            IDbCommand objComand;
            string strTipoServerDB = string.Empty;
            ServerBD = TipoServBaseDatos.Otro;
            strTipoServerDB = GetString(Define.HKEY_VALUE_TIPOSERVERDB).Trim().ToUpper();
            switch (strTipoServerDB)
            {
                case "SQL":
                case "MSSQL":
                case "SQL2005":
                    objComand = new SqlCommand();
                    break;
                case "ORACLE":
                    objComand = new OracleCommand();
                    break;
                default: //Error 
                    objComand = null;
                    break;
            }

            return objComand;
        }

        public IDbDataParameter obtenerParametro()
        {
            IDbDataParameter objParametro;
            string strTipoServerDB = string.Empty;
            ServerBD = TipoServBaseDatos.Otro;
            strTipoServerDB = GetString(Define.HKEY_VALUE_TIPOSERVERDB).Trim().ToUpper();
            switch (strTipoServerDB)
            {
                case "MSSQL":
                case "SQL":
                case "SQL2005":
                    objParametro = new SqlParameter();
                    break;
                case "ORACLE":
                    objParametro = new OracleParameter();
                    break;
                default: //Error 
                    objParametro = null;
                    break;
            }

            return objParametro;
        }

        public string strGetCadenaDeConexionProduccion()
        {

            string strTipoServerDB = string.Empty;
            ServerBD = TipoServBaseDatos.Otro;
            strTipoServerDB = GetString(Define.HKEY_VALUE_TIPOSERVERDB).Trim().ToUpper();
            switch (strTipoServerDB)
            {
                case "MSSQL":
                case "SQL":
                case "SQL2005":
                    ServerBD = TipoServBaseDatos.ServBDSqlServer;
                    break;
                case "ORACLE":
                    ServerBD = TipoServBaseDatos.ServBDOracle;
                    break;
                default: //Error 
                    ServerBD = TipoServBaseDatos.Otro;
                    break;
            }


            if (!ServerBD.Equals(TipoServBaseDatos.Otro))
            {
                string strServerName = GetString(Define.HKEY_VALUE_SERVERPROD);
                string strDBName = GetString(Define.HKEY_VALUE_DBPNAME);
                string strDBUser = GetString(Define.HKEY_VALUE_DBPUSER);
                string sClave = GetString(Define.HKEY_VALUE_DBPPASSWORD);
                string strTipoCnx = GetString(Define.HKEY_VALUE_DBPTIPOCNX);
                string strDBPassword = String.Empty;

                if (sistema == Sistema.HiperChannel)
                {
                    try
                    {
                        strDBPassword = UtilEncript.UtilitarioEncript.DesencriptarTripleDes(sClave);
                    }
                    catch (Exception e)
                    {
                        //log.Error("Error en desencriptar password de la bd produccion.", e);
                        throw e;
                    }
                }
                else
                {
                    strDBPassword = this.Decrypt(sClave);
                }

                try
                {
                    switch (ServerBD)
                    {
                        case TipoServBaseDatos.ServBDSqlServer:
                         if (strTipoCnx == "0" && strTipoCnx=="")
                               {
                                  Define.CADENA_DE_CONEXION_DBPROD = "Data Source=" + strServerName + ";Initial Catalog=" + strDBName + ";User Id=" + strDBUser + ";Password=" + strDBPassword + ";";
                               }
                             else 
                             {
                                Define.CADENA_DE_CONEXION_DBPROD = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=" + strDBName + ";Data Source=" + strServerName+ ";";                       
                             }                                break;
                        case TipoServBaseDatos.ServBDOracle:
                            Define.CADENA_DE_CONEXION_DBPROD = "Provider=msdaora; Data Source=" + strDBName + "; User Id=" + strDBUser + "; Password=" + strDBPassword + ";";
                            break;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                Exception ex = new Exception("Error en definicion de bd.");
                //log.Error("Error en la definicion del tipo de bd produccion.", ex);
                throw ex;
            }

            return Define.CADENA_DE_CONEXION_DBPROD;

        }

        public string strGetCadenaDeConexionReporte()
        {

            string strTipoServerDB = "";
            ServerBD = TipoServBaseDatos.Otro;
            strTipoServerDB = GetString(Define.HKEY_VALUE_TIPOSERVERDB).Trim().ToUpper();
            switch (strTipoServerDB)
            {
                case "MSSQL":
                case "SQL":
                case "SQL2005":
                    ServerBD = TipoServBaseDatos.ServBDSqlServer;
                    break;
                case "ORACLE":
                    ServerBD = TipoServBaseDatos.ServBDOracle;
                    break;
                default:
                    ServerBD = TipoServBaseDatos.Otro;
                    break;
            }

            if (!ServerBD.Equals(TipoServBaseDatos.Otro))
            {
                string strServerName = GetString(Define.HKEY_VALUE_SERVERREPORT);
                string strDBName = GetString(Define.HKEY_VALUE_DBRNAME);
                string strDBUser = GetString(Define.HKEY_VALUE_DBRUSER);
                string sClave = GetString(Define.HKEY_VALUE_DBPPASSWORD);
                string strDBPassword = GetString(Define.HKEY_VALUE_DBRPASSWORD);
                string strTipoCnx = GetString(Define.HKEY_VALUE_DBRTIPOCNX);

                if (sistema == Sistema.HiperChannel)
                {
                    try
                    {
                        strDBPassword = UtilEncript.UtilitarioEncript.DesencriptarTripleDes(strDBPassword);
                    }
                    catch (Exception e)
                    {
                        //log.Error("Error en desencriptar password de la bd reporte.", e);
                        throw e;
                    }
                }
                else
                {
                    strDBPassword = this.Decrypt(sClave);
                }

                try
                {
                    switch (ServerBD)
                    {
                        case TipoServBaseDatos.ServBDSqlServer:
                            if (strTipoCnx == "0" || strTipoCnx == "")
                               {
                            Define.CADENA_DE_CONEXION_DBREPORT = "Data Source=" + strServerName + ";Initial Catalog=" + strDBName + ";User Id=" + strDBUser + ";Password=" + strDBPassword + ";";                            
                               }
                            else 
                              {
                                  Define.CADENA_DE_CONEXION_DBREPORT = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=" + strDBName + ";Data Source=" + strServerName + ";";                         
                              }                              break;
                        case TipoServBaseDatos.ServBDOracle:
                            Define.CADENA_DE_CONEXION_DBREPORT = "Provider=msdaora; Data Source=" + strDBName + "; User Id=" + strDBUser + "; Password=" + strDBPassword + ";";
                            break;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                Exception ex = new Exception("Error en definicion de bd.");
                //log.Error("Error en la definicion del tipo de bd reporte.", ex);
                throw ex;
            }
            return Define.CADENA_DE_CONEXION_DBREPORT;
        }



        /*
         * Función : GetString
         * Resumen : Busca del Registro de Windows un valor. Utiliza para su búsqueda los parametros de entrada
         * Parámetros de entrada:
         * hKey			-> llave a nivel de nodo del Registro de Windows
         * strPath		-> Ruta dentro del nodo
         * strValueName -> Nombre campo (ValueName)
         * 
         * Tipo de dator retornado: string
         * Valores de retorno:
         * Si todo es correcto debe retornar el contenido del valor(en el registro) como un string . En caso contrario:
         * "-1" -> No existe el Registry Key en el Registro de Windows
         * "-2" -> No existe el ValueName en el Registro de Windows
         * "-3" -> No se ha escrito el Value (está vacío) en el Registro de Windows
         * 
         * Autor	: EChilo
         * Fecha	: 27/02/06
         * */
        public string GetString(string strValueName)
        {
            string strPath = String.Empty;
            object objValor; //almacenará el valor buscado
            if (sistema == Sistema.HiperChannel)
            {
                strPath = Define.HKEY_PATH_HIPERCHANNEL;
            }
            else
            {
                strPath = Define.HKEY_PATH_BMATIC;
            }
            RegistryKey key = Registry.LocalMachine.OpenSubKey(strPath, true);
            if (key == null)
            {
                objValor = "-1"; //No existe el KEY en el registro 
                // log.Error("Error en obtener key del regedit: " + strPath);
            }
            else
            {
                objValor = key.GetValue(strValueName);
                if (objValor == null)
                {
                    //log.Error("No existe el ValueName: " + strValueName);
                    objValor = "-2"; //No existe el ValueName en el KEY
                }
                else if (objValor.ToString().Trim() == "")
                {
                    //log.Error("No hay un Valor escrito para el ValueName: " + strValueName);
                    objValor = "-3"; //No hay un Valor escrito para el ValueName
                }
            }

            return objValor.ToString().Trim();
        }

        /*
         * Función : Decrypt
         * Resumen :	Desencripta una cadena. Para eso hace una llamada al componente(COM) "vbpDES.dll", quien a su vez 
         *				hace referencia al archivo "desdll.dll".
         *				vbpDES.dll sirve como una interfaz para poder usar fácilmente a "desdll.dll".
         *				NOTA: Sería mejor acceder directamente a "desdll.dll" y prescindir totalmente de "vbpDES.dll"
         * Parámetro de entrada:
         *	cipherText	-> cadena encriptada
         * 
         * Tipo de dator retornado: string
         * Valores de retorno: Debe retornar la cadena desencriptada
         * 
         * Autor	: EChilo
         * Fecha	: 27/02/06
         * */
        public string Decrypt(string cipherText)
        {
            object resultado = null;
            Type tipo = Type.GetTypeFromProgID("vbpDES.Desencriptador");
            object obj = Activator.CreateInstance(tipo);
            object[] args = { cipherText };

            try
            {
                resultado = tipo.InvokeMember("strDesencriptaDES", BindingFlags.InvokeMethod, null, obj, args);
            }
            catch
            {
            }

            obj = null;
            tipo = null;
            return resultado.ToString();
        }
    }
}
