using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using LAP.TUUA.DAO;
using LAP.TUUA.UTIL;

namespace LAP.TUUA.CONTROL
{
    public class BO_Configuracion
    {
        private DAO_ParameGeneral dao_ParameGeneral;
        private DAO_ListaDeCampos dao_ListaDeCampos;

        public BO_Configuracion()
        {
            dao_ParameGeneral = new DAO_ParameGeneral(Property.htSPConfig);
            dao_ListaDeCampos = new DAO_ListaDeCampos(Property.htSPConfig);
        }

        public DataTable ObtenerPeriodosAArchivar()
        {
            DataTable dtListaPeriodos = new DataTable();
            dtListaPeriodos = ListaCamposxNombre((string)Property.htProperty[Define.NOM_CAMPO_LISTA_CAMPOS_LISTAPERIODO]);
            return dtListaPeriodos;
        }

        public DataTable ObtenerCodEtapaArchivamiento()
        {
            DataTable dtCodigoEtapa = new DataTable();
            dtCodigoEtapa = ListaCamposxNombre((string)Property.htProperty[Define.NOM_LC_CODETAPA_ARCH]);
            return dtCodigoEtapa;
        }

        public DataTable ListarParametros(string as_identificador)
        {
            try
            {
                return dao_ParameGeneral.ObtenerParametroGeneral(as_identificador);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }

        public DataTable ListaCamposxNombre(string as_nombre)
        {
            try
            {
                return dao_ListaDeCampos.obtenerListaxNombre(as_nombre);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }

        }


    }
}
