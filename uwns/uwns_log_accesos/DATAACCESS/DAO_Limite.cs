using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using LAP.TUUA.ENTIDADES;
using System.Collections;

namespace LAP.TUUA.DAO
{
    public class DAO_Limite : DAO_BaseDatos
    {
        public DAO_Limite(string sConfigSPPath)
            : base(sConfigSPPath)
        {
        }

        public List<Limite> ListarPorOperacion(string stipope)
        {
            IDataReader objResult;
            try
            {
                List<Limite> objListaLimite = new List<Limite>();
                Hashtable hsSelectAllUSP = (Hashtable)htSPConfig["LIMSP_SELECTBYOPE"];
                string sNombreSP = (string)hsSelectAllUSP["LIMSP_SELECTBYOPE"];
                objResult = base.listarDataReaderSP(sNombreSP, stipope);

                while (objResult.Read())
                {
                    objListaLimite.Add(poblar(objResult));
                }

                objResult.Close();
                return objListaLimite;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Limite poblar(IDataReader dataReader)
        {

            Hashtable htSelectAllUSP = (Hashtable)htSPConfig["LIMSP_SELECTBYOPE"];
            Limite objLimite = new Limite();
            if (dataReader.FieldCount == 0)
            {
                return null;
            }
            try
            {
                objLimite.Cod_Moneda = ((string)dataReader[(string)htSelectAllUSP["Cod_Moneda"]]).Trim();
                objLimite.Cod_TipoOpera = (string)dataReader[(string)htSelectAllUSP["Cod_Tipo_Operacion"]];
                if (!Convert.IsDBNull(dataReader[(string)htSelectAllUSP["LimMax"]]))
                {
                    objLimite.Imp_LimMaximo = decimal.Parse(dataReader[(string)htSelectAllUSP["LimMax"]].ToString());

                }
                if (!Convert.IsDBNull(dataReader[(string)htSelectAllUSP["LimMin"]]))
                {
                    objLimite.Imp_LimMinimo = decimal.Parse(dataReader[(string)htSelectAllUSP["LimMin"]].ToString());
                }
                if (!Convert.IsDBNull(dataReader[(string)htSelectAllUSP["Margen"]]))
                {
                    objLimite.Imp_MargenCaja = decimal.Parse(dataReader[(string)htSelectAllUSP["Margen"]].ToString());
                }
            }
            catch (Exception e)
            {
                return null;
            }
            return objLimite;
        }

    }
}
