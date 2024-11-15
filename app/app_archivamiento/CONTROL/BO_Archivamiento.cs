using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using LAP.TUUA.DAO;
using LAP.TUUA.UTIL;

namespace LAP.TUUA.CONTROL
{
    public class BO_Archivamiento
    {
        private DAO_Archivo dao_archivo;

        public BO_Archivamiento()
        {
            dao_archivo = new DAO_Archivo(Property.htSPConfig);
        }

        public bool CopiaInformacionArchivamiento(String host, String DBName, String DBUser, String DBPassword,
                    String Tablas, String rangoFechaInicial, String rangoFechaFinal, String Cod_Archivo, ref string strMessage)
        {
            bool pasoCompletado = dao_archivo.CopiaInformacionArchivamiento(host, DBName, DBUser, DBPassword, Tablas, rangoFechaInicial, rangoFechaFinal, Cod_Archivo, ref strMessage);
            return pasoCompletado;
        }

        public bool CreaTablasArchivamiento(String host, String DBName, String DBUser, String DBPassword,
                    String Tablas, String Cod_Archivo, ref string strMessage)
        {
            bool pasoCompletado = dao_archivo.CreaTablasArchivamiento(host, DBName, DBUser, DBPassword, Tablas, Cod_Archivo, ref strMessage);
            return pasoCompletado;
        }

        public DataTable listarEstructuraTablasArchivamiento(String Tablas, String rangoFechaInicial, String rangoFechaFinal, String Cod_Archivo)
        {
            return dao_archivo.listarEstructuraTablasArchivamiento(Tablas, rangoFechaInicial, rangoFechaFinal, Cod_Archivo);
        }

        public bool DepuraInformacionArchivamiento(String Tablas, String rangoFechaInicial, String rangoFechaFinal, ref string strMessage)
        {
            bool pasoCompletado = dao_archivo.DepuraInformacionArchivamiento(Tablas, rangoFechaInicial, rangoFechaFinal, ref strMessage);
            return pasoCompletado;
        }

        public DataTable CalculaCodigoArchivamiento()
        {
            return dao_archivo.CalculaCodigoArchivamiento();
        }

        public bool InsUpd_TUA_Archivo(String Cod_Archivo, String Cod_Periodo, String Fch_Ini, String Fch_Fin, String Tip_Estado,
            String Cod_Etapa, String Xml_Rango, String Log_Usuario_Mod)
        {
            return dao_archivo.InsUpd_TUA_Archivo(Cod_Archivo, Cod_Periodo, Fch_Ini, Fch_Fin, Tip_Estado, Cod_Etapa, Xml_Rango, Log_Usuario_Mod);
        }

        public DataTable ConsultaRangosPorTipo_Archivamiento(String tipo, String Cod_Archivo, String host, String DBName, String DBUser, String DBPassword, String Dsc_Message)
        {
            return dao_archivo.ConsultaRangosPorTipo_Archivamiento(tipo, Cod_Archivo, host, DBName, DBUser, DBPassword, Dsc_Message);
        }

        public bool VerificarEstadistico(String rangoFechaFinal)
        {
            return dao_archivo.VerificarEstadistico(rangoFechaFinal);
        }

    }
}
