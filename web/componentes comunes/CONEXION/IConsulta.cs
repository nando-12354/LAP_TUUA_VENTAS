using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAP.TUUA.CONEXION
{
    public interface IConsulta
    {
        System.Data.DataTable ObtenerConsultaAuditoriaPaginCrit(string strTipOperacion, string strTabla,
            string strCodModulo, string strCodSubModulo, string strCodUsuario, string strFchDesde,
            string strFchHasta, string strHraDesde, string strHraHasta, string sColumnSort,
            int iIniRows, int iMaxRows, string sTotalRows);

        System.Data.DataTable ObtenerDetalleAuditoriaCrit(string strNombreTabla, string strContador);

    }
}
