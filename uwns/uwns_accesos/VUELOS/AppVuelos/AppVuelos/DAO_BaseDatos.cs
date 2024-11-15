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

namespace LAP.TUUA.DAO
{
    public class DAO_BaseDatos
    {
        public bool isRegistrado;
        public int iResult;
        public bool isActualizado = false;
        public Hashtable htSPConfig;
        public Database helper;
        public Database helperLocal;
        public string Cod_Usuario;
        public string Cod_Modulo;
        public string Cod_Sub_Modulo;

        public DAO_BaseDatos(string sConfigSPPath)
        {
            try
            {
                Conexion();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DAO_BaseDatos()
        {
            try
            {
                Conexion();
            }
            catch (Exception)
            {
                throw;
            }
        }
            
        public bool IsConected(){
              if (helper != null)
              {
                    return true;
              }
              return false;
        }


        protected void Conexion()
        {
            helper = DatabaseFactory.CreateDatabase();
        }

        public void Conexion(string strCnxName)
        {
            helper = DatabaseFactory.CreateDatabase(strCnxName);
        }

        public void ConexionLocal()
        {
              helper = helperLocal;
        }

        public void TestConexionRemota(string strCnxName)
        {
              helper = DatabaseFactory.CreateDatabase(strCnxName);
        }

        public void TestConexionLocal(string strCnxName)
        {
              helperLocal = DatabaseFactory.CreateDatabase(strCnxName);
        } 

        protected bool mantenerSPSinAuditoria(DbCommand objComando)
        {
              try
              {
                  if (helper.ExecuteNonQuery(objComando) != 0)
                          return true;
                  return false;
              }
              catch (Exception ex)
              {
                    throw;
              }
        }		
		
        protected bool mantenerSP(DbCommand objComando)
        {
              try
              {
                  //if (objComando.Parameters.IndexOf("@Log_Usuario_Mod")==-1)
                  //{
                  //    helper.AddInParameter(objComando,"Log_Usuario_Mod", DbType.String,Cod_Usuario);
                       
                  //}

                  //if (objComando.Parameters.IndexOf("@Cod_Modulo_Mod") == -1)
                  //{
                  //      helper.AddInParameter(objComando, "Cod_Modulo_Mod", DbType.String, Cod_Modulo);
                  //}
                  
                  //if (objComando.Parameters.IndexOf("@Cod_SubModulo_Mod") == -1)
                  //{
                  //      helper.AddInParameter(objComando, "Cod_SubModulo_Mod", DbType.String, Cod_Sub_Modulo);
                  //}
                  
                  if (helper.ExecuteNonQuery(objComando) != 0)
                          return true;
                  return false;
              }
              catch (Exception ex)
              {
                    throw;
              }
        }



        protected DataSet listarDataSetSP(string strNombreSP, params object[] parametros)
        {
            try
            {
                DataSet objResult = helper.ExecuteDataSet(strNombreSP, parametros);
                return objResult;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected IDataReader listarDataReaderSP(string strNombreSP, params object[] args)
        {
            try
            {
                IDataReader objResult = null;
                if (args != null)
                    objResult = helper.ExecuteReader(strNombreSP, args);
                else objResult = helper.ExecuteReader(strNombreSP);

                return objResult;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected DataSet ListarSP(string nombreSP, params object[] parametros)
        {
            try
            {
                DataSet result = helper.ExecuteDataSet(nombreSP, parametros);
                return result;
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

        protected DataTable ListarDataTableQY(string strQuery)
        {
            DataSet result = null;
            try
            {
                if (strQuery != null && strQuery.Length > 0)
                    result = helper.ExecuteDataSet(CommandType.Text, strQuery);
                if (result != null)
                    return result.Tables[0];
                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }



    }


}
