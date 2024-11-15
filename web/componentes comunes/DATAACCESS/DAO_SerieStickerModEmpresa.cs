using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Collections;

namespace LAP.TUUA.DAO
{
    public class DAO_SerieStickerModEmpresa : DAO_BaseDatos
    {
        public string objModEmpresa;

        public DAO_SerieStickerModEmpresa(string htSPSerie) : base(htSPSerie)
        {
            objModEmpresa = htSPSerie;
        }
        //FL.
        public string ValidarSerie(string codigoEmpresa, string codigoModalidad)
        {
            string respuesta = string.Empty;

            try
            {
                string sNombreSP = "usp_ope_validar_serie_sel";

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);

                helper.AddInParameter(objCommandWrapper, "@codigoEmpresa", DbType.String, codigoEmpresa);
                helper.AddInParameter(objCommandWrapper, "@codigoModalidad", DbType.String, codigoModalidad);
                helper.AddOutParameter(objCommandWrapper, "@respuesta", DbType.String, -1);
                helper.ExecuteNonQuery(objCommandWrapper);

                respuesta = (string)objCommandWrapper.Parameters["@respuesta"].Value;
            }
            catch (Exception ex)
            {
                throw;
            }

            return respuesta;
        }
    }
}
