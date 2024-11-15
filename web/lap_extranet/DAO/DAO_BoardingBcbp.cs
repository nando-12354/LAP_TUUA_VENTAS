using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Collections;
using System.Data;

namespace LAP.EXTRANET.DAO
{
    public class DAO_BoardingBcbp : DAO_BaseDatos   
    {
        public DAO_BoardingBcbp(string sConfigSPPath)
            : base(sConfigSPPath)            
        { 
        }

        public DataSet ConsultaBcbpMensualLeidosMolinete(string sFchDesde, string sFchHasta, string sNumVuelo, string sTipVuelo, string sTipPersona, string sCodIATA, string sSort, int iFila, int iMaxFila, string sPaginacion, string sMostrarResumen, string sTotalRows, string sExcel)
        {
            try
            {
                DataSet objResult;
                Hashtable hsSelectAllUSP = (Hashtable)htSPConfig["BPSP_CNS_MENSUAL"];
                string sNombreSP = (string)hsSelectAllUSP["BPSP_CNS_MENSUAL"];
                objResult = base.listarDataSetSP(sNombreSP, sFchDesde, sFchHasta, sNumVuelo, sTipVuelo, sTipPersona, sCodIATA, sSort, iFila, iMaxFila, sPaginacion, sMostrarResumen, sTotalRows, sExcel);
                return objResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet BoardingLeidosMolinete(string sFchDesde, string sFchHasta, string sHoraDesde, string sHoraHasta, string strFechVuelo, string strNum_Vuelo, string strNumeroAsiento,
                                      string strNomPasajero, string strNroBoarding, string strETicket, string strTipEstado, string sCodIATA,  string strSort, int iFila, int iMaxFila, string strPaginacion, string strMostrarResumen,string strFlgTotal, string strFlgExcel)
        {
            try
            {
                DataSet objResult;
                Hashtable hsSelectAllUSP = (Hashtable)htSPConfig["BPSP_OBTENERBPMOLINETE"];
                string sNombreSP = (string)hsSelectAllUSP["BPSP_OBTENERBPMOLINETE"];
                objResult = base.listarDataSetSP(sNombreSP, sFchDesde, sFchHasta, sHoraDesde, sHoraHasta, strFechVuelo, strNum_Vuelo, strNumeroAsiento, strNomPasajero, strNroBoarding, strETicket, strTipEstado, sCodIATA, strSort, iFila, iMaxFila, strPaginacion, strMostrarResumen, strFlgTotal, strFlgExcel);
                return objResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public DataTable DetalleBoardingHistorica(string Num_Secuencial_Bcbp)
        {
            try
            {
                DataTable objResult;
                Hashtable hsSelectAllUSP = (Hashtable)htSPConfig["BPSP_BOARDING_HISTORICO"];
                string sNombreSP = (string)hsSelectAllUSP["BPSP_BOARDING_HISTORICO"];
                objResult = base.ListarDataTableSP(sNombreSP, Num_Secuencial_Bcbp);
                return objResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable DetalleBoardingEstHist(String Num_Secuencial_Bcbp)
        {
            try
            {
                DataTable objResult;
                Hashtable hsSelectAllUSP = (Hashtable)htSPConfig["BPSP_OBTENER_xNUMSEC_BOARDINGESTHIST"];
                string sNombreSP = (string)hsSelectAllUSP["BPSP_OBTENER_xNUMSEC_BOARDINGESTHIST"];
                objResult = base.ListarDataTableSP(sNombreSP, Num_Secuencial_Bcbp);
                objResult.Dispose();
                return objResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ListarBoardingAsociados(String Num_Secuencial_Bcbp)
        {
            try
            {
                DataTable objResult;
                Hashtable hsSelectAllUSP = (Hashtable)htSPConfig["BPSP_BOARDING_ASOCIADO"];
                string sNombreSP = (string)hsSelectAllUSP["BPSP_BOARDING_ASOCIADO"];
                objResult = base.ListarDataTableSP(sNombreSP, Num_Secuencial_Bcbp);
                return objResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ValidarCompaniaBCBP(string Cod_IATA)
        {
            try
            {
                DataTable objResult;
                Hashtable hsSelectAllUSP = (Hashtable)htSPConfig["BPSP_VALIDAR_COMPANIA_BCBP"];
                string sNombreSP = (string)hsSelectAllUSP["BPSP_VALIDAR_COMPANIA_BCBP"];
                objResult = base.ListarDataTableSP(sNombreSP, Cod_IATA);
                return objResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //consultarBoardingPassDiario  - 20.12.2010
        public DataSet consultarBoardingPassDiario(string sFechaDesde,
                                                    string sFechaHasta,
                                                    string sHoraDesde,
                                                    string sHoraHasta,
                                                    string sCodCompania,
                                                    string sTipoPasajero,                                        
                                                    string sTipoVuelo,
                                                    string sTipoTrasbordo,
                                                    string sFechaVuelo,
                                                    string sNumVuelo,
                                                    string sPasajero,
                                                    string sNumAsiento,
                                                    string sCodIata,
                                                    string sPassword,
                                                    string sTipReporte,
                                                    string sColumnSort,
                                                    int iIniRows,
                                                    int iMaxRows,
                                                    string sTotalRows
                                                    )
        {
            try
            {
                DataSet result;
                Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["BPSP_RPT_BCBPDIARIO"];
                string sNombreSP = (string)hsSelectByIdUSP["BPSP_RPT_BCBPDIARIO"];
                result = base.listarDataSetSP(sNombreSP,
                                                sFechaDesde,
                                                sFechaHasta,
                                                sHoraDesde,
                                                sHoraHasta,
                                                sCodCompania,
                                                sTipoPasajero,
                                                sTipoVuelo,
                                                sTipoTrasbordo,
                                                sFechaVuelo,
                                                sNumVuelo,
                                                sPasajero,
                                                sNumAsiento,
                                                sCodIata,
                                                sPassword,
                                                sTipReporte,
                                                sColumnSort,
                                                iIniRows,
                                                iMaxRows,
                                                sTotalRows);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
