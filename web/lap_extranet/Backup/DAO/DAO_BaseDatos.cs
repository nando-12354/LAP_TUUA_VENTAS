﻿using System.Diagnostics;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Configuration;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using System.Data.Common;
using LAP.EXTRANET.UTIL;

namespace LAP.EXTRANET.DAO
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
        protected int currentPageNumber = 1;
        private const int PAGE_SIZE = 10;

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

        public DAO_BaseDatos(string sConfigSPPath, string strUsuario, string strModulo, string strSubModulo)
        {
            try
            {
                Cod_Usuario = strUsuario;
                Cod_Modulo = strModulo;
                Cod_Sub_Modulo = strSubModulo;
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

        public DAO_BaseDatos()
        {

        }

        public DAO_BaseDatos(string sConfigSPPath, string strCnxName)
        {
            Conexion(strCnxName);
        }

        public DAO_BaseDatos(Hashtable htSPConfig)
        {
            this.htSPConfig = htSPConfig;
            Conexion();
        }

        public DAO_BaseDatos(Database vhelper, Database vhelperLocal, Hashtable vhtSPConfig)
        {
            helper = vhelper;
            helperLocal = vhelperLocal;
            htSPConfig = vhtSPConfig;
        }

        public bool IsConected()
        {
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
                if (objComando.Parameters.IndexOf("@Log_Usuario_Mod") == -1)
                {
                    helper.AddInParameter(objComando, "Log_Usuario_Mod", DbType.String, Cod_Usuario);

                }

                if (objComando.Parameters.IndexOf("@Cod_Modulo_Mod") == -1)
                {
                    helper.AddInParameter(objComando, "Cod_Modulo_Mod", DbType.String, Cod_Modulo);
                }

                if (objComando.Parameters.IndexOf("@Cod_SubModulo_Mod") == -1)
                {
                    helper.AddInParameter(objComando, "Cod_SubModulo_Mod", DbType.String, Cod_Sub_Modulo);
                }

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
                //DataSet objResult = helper.ExecuteDataSet(strNombreSP, parametros);
                DataSet objResult = new DataSet();
                DbCommand comando = helper.GetStoredProcCommand(strNombreSP, parametros);
                comando.Connection = helper.DbProviderFactory.CreateConnection();
                comando.Connection.ConnectionString = helper.ConnectionString;
                comando.CommandTimeout = 600000;// Agregado por Daniel Castillo -> timeout 10 minutos
                DbDataAdapter adapter = helper.DbProviderFactory.CreateDataAdapter();
                adapter.SelectCommand = comando;
                adapter.Fill(objResult);
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
                {
                    //objResult = helper.ExecuteReader(strNombreSP, args);
                    DbCommand comando = helper.GetStoredProcCommand(strNombreSP, args);
                    comando.Connection = helper.DbProviderFactory.CreateConnection();
                    comando.Connection.ConnectionString = helper.ConnectionString;
                    comando.CommandTimeout = 600000;// Agregado por Daniel Castillo -> timeout 10 minutos
                    objResult= comando.ExecuteReader();

                }
                else {
                    DbCommand comando = helper.GetStoredProcCommand(strNombreSP);
                    comando.Connection = helper.DbProviderFactory.CreateConnection();
                    comando.Connection.ConnectionString = helper.ConnectionString;
                    comando.CommandTimeout = 600000;// Agregado por Daniel Castillo -> timeout 10 minutos
                    objResult = comando.ExecuteReader();
                } 

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
                DataSet result =new DataSet();
                DbCommand comando = helper.GetStoredProcCommand(nombreSP, parametros);
                comando.Connection = helper.DbProviderFactory.CreateConnection();
                comando.Connection.ConnectionString = helper.ConnectionString;
                comando.CommandTimeout = 600000;// Agregado por Daniel Castillo -> timeout 10 minutos
                DbDataAdapter adapter = helper.DbProviderFactory.CreateDataAdapter();
                adapter.SelectCommand = comando;
                adapter.Fill(result);
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
            DbCommand dbCommand;
            try
            {
                if (parametros != null)
                {
                    dbCommand = helper.GetStoredProcCommand(nombreSP, parametros);
                    dbCommand.CommandTimeout = 600000;
                    result = helper.ExecuteDataSet(dbCommand);
                }
                else
                {
                    dbCommand = helper.GetStoredProcCommand(nombreSP);
                    dbCommand.CommandTimeout = 600000;
                    result = helper.ExecuteDataSet(dbCommand);
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

        //protected DataSet ListarDataSetPaging()
        //{

        //    string connectionString = "Server=172.15.1.10; Database=DBTUUA_110110;Trusted_Connection=true";
        //    SqlConnection myConnection = new SqlConnection(connectionString);
        //    SqlCommand myCommand = new SqlCommand("usp_cns_pcs_ticketxfecha_sel2", myConnection);
        //    myCommand.CommandType = CommandType.StoredProcedure;

        //    myCommand.Parameters.AddWithValue("@startRowIndex",currentPageNumber);
        //    myCommand.Parameters.AddWithValue("@maximumRows", PAGE_SIZE);
        //    myCommand.Parameters.Add("@totalRows", SqlDbType.Int, 4);
        //    myCommand.Parameters["@totalRows"].Direction =  ParameterDirection.Output;

        //    SqlDataAdapter ad = new SqlDataAdapter(myCommand);

        //    DataSet ds = new DataSet();
        //    ad.Fill(ds);

        //    return ds;
        //}

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
