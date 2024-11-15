using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LAP.TUUA.CONEXION;
using LAP.TUUA.DAO;
using LAP.TUUA.UTIL;

namespace LAP.TUUA.NATIVO
{
    class NAT_Auditoria : IAuditoria
    {
        protected string Dsc_PathSPConfig;
        DAO_Auditoria objDAOAuditoria;

        public NAT_Auditoria()
        {
            Dsc_PathSPConfig = (string)Property.htProperty["PATHRECURSOS"];
            objDAOAuditoria = new DAO_Auditoria(Dsc_PathSPConfig);
        }

        #region Miembros de IAuditoria

        public Int64 obtenerIdTransaccionCritica()
        {
            try
            {
                Int64 idTransaccionCritica;

                System.Data.DataTable dt = objDAOAuditoria.obtenerIdTransaccionCritica();

                if (dt != null)
                {
                    if (dt.Rows[0][0] == DBNull.Value)
                    {
                        idTransaccionCritica = 1;
                    }
                    else
                    {
                        idTransaccionCritica = Convert.ToInt64(dt.Rows[0][0]) + 1;
                    }
                }
                else
                {
                    idTransaccionCritica = 1;
                }

                return idTransaccionCritica;
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }

        #endregion
    }
}
