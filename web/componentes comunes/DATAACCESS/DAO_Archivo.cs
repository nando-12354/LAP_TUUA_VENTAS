using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace LAP.TUUA.DAO
{
    public class DAO_Archivo : DAO_BaseDatos
    {
        public DAO_Archivo(Hashtable htSPConfig)
            : base(htSPConfig)
        {
            
        }

        public DataTable obtenerHistoricoArchivamiento()
        {
            DataTable result;
            Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["ARCH_HISTORICO"];
            string sNombreSP = (string)hsSelectByIdUSP["ARCH_HISTORICO"];
            result = base.ListarDataTableSP(sNombreSP);

            return result;
        }

        public DataTable calculaFechaDisponible(String IdParamGeneral)
        {
            DataTable result;
            Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["ARCH_CALCFECHDISP"];
            string sNombreSP = (string)hsSelectByIdUSP["ARCH_CALCFECHDISP"];
            result = base.ListarDataTableSP(sNombreSP, IdParamGeneral);

            return result;
        }

        public DataTable SelectDetalleArchivamiento(String Cod_Archivo)
        {
            DataTable result;
            Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["ARCH_SELDETALLEARCH"];
            string sNombreSP = (string)hsSelectByIdUSP["ARCH_SELDETALLEARCH"];
            result = base.ListarDataTableSP(sNombreSP, Cod_Archivo);

            return result;            
        }

        public bool CopiaInformacionArchivamiento(String host, String DBName, String DBUser, String DBPassword,
            String Tablas, String rangoFechaInicial, String rangoFechaFinal, String Cod_Archivo, ref string strMessage)
        {
            bool boResult;
            try
            {
                Hashtable hsInsertUSP = (Hashtable)htSPConfig["COPY_INFOARCHIV"];
                string sNombreSP = (string)hsInsertUSP["COPY_INFOARCHIV"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["host"], DbType.String, host);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["DBName"], DbType.String, DBName);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["DBUser"], DbType.String, DBUser);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["DBPassword"], DbType.String, DBPassword);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Tablas"], DbType.String, Tablas);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["rangoFechaInicial"], DbType.String, rangoFechaInicial);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["rangoFechaFinal"], DbType.String, rangoFechaFinal);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Archivo"], DbType.String, Cod_Archivo);

                helper.AddOutParameter(objCommandWrapper, (string)hsInsertUSP["Dsc_Message"], DbType.String, 255);
                objCommandWrapper.CommandTimeout = 0;
                boResult = base.mantenerSPSinAuditoria(objCommandWrapper);
                if (boResult)
                {
                    strMessage = (string)helper.GetParameterValue(objCommandWrapper, (string)hsInsertUSP["Dsc_Message"]);
                    if (!strMessage.Equals("OK"))
                        boResult = false;
                }
                return boResult;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool CreaTablasArchivamiento(String host, String DBName, String DBUser, String DBPassword,
            String Tablas, String Cod_Archivo, ref string strMessage)
        {
            bool boResult;
            try
            {
                Hashtable hsInsertUSP = (Hashtable)htSPConfig["CREAR_TABLASARCHIV"];
                string sNombreSP = (string)hsInsertUSP["CREAR_TABLASARCHIV"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["host"], DbType.String, host);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["DBName"], DbType.String, DBName);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["DBUser"], DbType.String, DBUser);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["DBPassword"], DbType.String, DBPassword);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Tablas"], DbType.String, Tablas);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Archivo"], DbType.String, Cod_Archivo);


                helper.AddOutParameter(objCommandWrapper, (string)hsInsertUSP["Dsc_Message"], DbType.String, 255);
                objCommandWrapper.CommandTimeout = 0;
                boResult = base.mantenerSPSinAuditoria(objCommandWrapper);
                if (boResult)
                {
                    strMessage = (string)helper.GetParameterValue(objCommandWrapper, (string)hsInsertUSP["Dsc_Message"]);
                    if (!strMessage.Equals("OK"))
                        boResult = false;
                }
                return boResult;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public DataTable listarEstructuraTablasArchivamiento(String Tablas, String rangoFechaInicial, String rangoFechaFinal, String Cod_Archivo)
        {
            DataTable result;
            Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["LISTAR_ESTRUCTURATABLASARCHIV"];
            string sNombreSP = (string)hsSelectByIdUSP["LISTAR_ESTRUCTURATABLASARCHIV"];
            result = base.ListarDataTableSP(sNombreSP, Tablas, rangoFechaInicial, rangoFechaFinal, Cod_Archivo);

            return result;
        }

        public bool DepuraInformacionArchivamiento(String Tablas, String rangoFechaInicial, String rangoFechaFinal, ref string strMessage)
        {
            bool boResult;
            try
            {
                Hashtable hsInsertUSP = (Hashtable)htSPConfig["DEPURA_INFOARCHIV"];
                string sNombreSP = (string)hsInsertUSP["DEPURA_INFOARCHIV"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Tablas"], DbType.String, Tablas);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["rangoFechaInicial"], DbType.String, rangoFechaInicial);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["rangoFechaFinal"], DbType.String, rangoFechaFinal);

                helper.AddOutParameter(objCommandWrapper, (string)hsInsertUSP["Dsc_Message"], DbType.String, 255);
                objCommandWrapper.CommandTimeout = 0;
                boResult = base.mantenerSPSinAuditoria(objCommandWrapper);
                if (boResult)
                {
                    strMessage = (string)helper.GetParameterValue(objCommandWrapper, (string)hsInsertUSP["Dsc_Message"]);
                    if (!strMessage.Equals("OK"))
                        boResult = false;
                }

                return boResult;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool InsUpd_TUA_Archivo(String Cod_Archivo, String Cod_Periodo, String Fch_Ini, String Fch_Fin, String Tip_Estado,
            String Cod_Etapa, String Xml_Rango, String Log_Usuario_Mod)
        {
            try
            {
                Hashtable hsInsertUSP = (Hashtable)htSPConfig["INSUPD_TUA_ARCHIVO"];
                string sNombreSP = (string)hsInsertUSP["INSUPD_TUA_ARCHIVO"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);

                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Archivo"], DbType.String, Cod_Archivo);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Periodo"], DbType.String, Cod_Periodo);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Fch_Ini"], DbType.String, Fch_Ini);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Fch_Fin"], DbType.String, Fch_Fin);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Tip_Estado"], DbType.String, Tip_Estado);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Etapa"], DbType.String, Cod_Etapa);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Xml_Rango"], DbType.String, Xml_Rango);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Log_Usuario_Mod"], DbType.String, Log_Usuario_Mod);
                isRegistrado = base.mantenerSPSinAuditoria(objCommandWrapper);
                return isRegistrado;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataTable CalculaCodigoArchivamiento()
        {
            DataTable result;
            Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["CALC_CODARCHIVO"];
            string sNombreSP = (string)hsSelectByIdUSP["CALC_CODARCHIVO"];
            result = base.ListarDataTableSP(sNombreSP);

            return result;
        }


        public DataTable ConsultaRangosPorTipo_Archivamiento(String tipo, String Cod_Archivo, String host, String DBName, String DBUser, String DBPassword, String Dsc_Message)
        {
            DataTable result;
            Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["CONSRANGOSPORTIPO_ARCHIV"];
            string sNombreSP = (string)hsSelectByIdUSP["CONSRANGOSPORTIPO_ARCHIV"];
            // '0' -- ticket solo por modalidad
            // '1' -- ticket por modalidad y compania
            // '2' -- boarding por 1 sola modalidad y por compania
            // '3' -- operacion sin agrupamiento

            //  Cod_Modalidad_Venta, Cod_Compania, QUANTITY, MIN, MAX
            result = base.ListarDataTableSP(sNombreSP, tipo, Cod_Archivo, host, DBName, DBUser, DBPassword, Dsc_Message);

            return result;
        }

        public bool VerificarEstadistico(String rangoFechaFinal)
        {
            try
            {
                Hashtable hsInsertUSP = (Hashtable)htSPConfig["ARCH_VERIF_ESTADISTICO"];
                string sNombreSP = (string)hsInsertUSP["ARCH_VERIF_ESTADISTICO"];

                String res = "0";

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["FechaRangoFinalArch"], DbType.String, rangoFechaFinal);
                helper.AddOutParameter(objCommandWrapper, (string)hsInsertUSP["EstadisticoRealizado"], DbType.String, 1);

                if(base.mantenerSPSinAuditoria(objCommandWrapper))
                {
                    res = (string)helper.GetParameterValue(objCommandWrapper, (string)hsInsertUSP["EstadisticoRealizado"]);
                }
                if (res.Equals("0"))
                    return false;
                else
                {
                    return true;
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

    }
}
