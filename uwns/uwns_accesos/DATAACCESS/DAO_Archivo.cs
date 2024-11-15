using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
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


    }
}
