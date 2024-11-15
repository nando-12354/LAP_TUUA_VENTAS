using System.Diagnostics;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Configuration;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using System.Data.Common;
using LAP.TUUA.UTIL;

namespace LAP.TUUA.DAO
{
    public class DAO_BaseDatos
    {
        public Hashtable htSPConfig;
        public Database helper;

        public DAO_BaseDatos(string sConfigSPPath)
        {
            try
            {
                SPConfigXml objSPConfig = new SPConfigXml();
                objSPConfig.cargarSPConfig(sConfigSPPath);
                htSPConfig = objSPConfig.HtSPConfig;
                Conexion();
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void Conexion()
        {
            helper = DatabaseFactory.CreateDatabase();
        }

        protected bool mantenerSP(DbCommand objComando)
        {
              try
              {
                  objComando.CommandTimeout = 0;
                    if (helper.ExecuteNonQuery(objComando) != 0)
                          return true;
                    return false;
              }
              catch (Exception ex)
              {
                    throw;
              }
        }

        protected DataTable ListarDataTableSP(string nombreSP, params object[] parametros)
        {
            DataSet result;
            try
            {
                if (parametros != null)
                {
                    result = helper.ExecuteDataSet(nombreSP, parametros);
                }
                else
                {
                    result = helper.ExecuteDataSet(nombreSP);
                }
                if (result != null)
                {
                    return result.Tables[0];
                }
                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

     

        protected bool ejecutarTrxSP(DbCommand objComando)
        {
            using (System.Data.Common.DbConnection connection = helper.CreateConnection())
            {
                connection.Open();
                System.Data.Common.DbTransaction transaction = connection.BeginTransaction();

                try
                {
                    objComando.CommandTimeout = 0;
                    helper.ExecuteNonQuery(objComando, transaction);
                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

    }


}
