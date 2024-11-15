using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using Microsoft.Win32;

//using log4net;
using System.Reflection;

namespace BMatic.DA
{
    public class DataSource
    {
        //private static ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        IDbCommand objComand;
        IDataReader objReader;

        //private static ILog log = LogManager.GetLogger(typeof(DataSource));

        public DataSource()
        {
            //log4net.GlobalContext.Properties["LogFileName"] = @"C:\BMatic\Servicios\SrvEnvioCorreo\WWW\file1"; //log file path 
            //log4net.Config.XmlConfigurator.Configure(new Uri(@"C:\BMatic\Servicios\SrvEnvioCorreo\log4Net.config"));
        }

        public DataSource(string strCodTipoVentanilla, string strPathLog)
        {
            //log4net.GlobalContext.Properties["LogFileName"] = strPathLog + @"\Asignar_" + strCodTipoVentanilla;
            //log4net.Config.XmlConfigurator.Configure(new Uri(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\log4Net.config"));
        }

        /// <summary>
        /// Función necesaria para insertar registros a la bd de reportes
        /// </summary>
        /// <returns></returns>
        public int InsertarReportes()
        {
            try
            {
                IDbCommand objCmd = objComand;
                objCmd.CommandTimeout = Define.TIME_OUT;
                DAOFactory objDAO = new DAOFactory(TipoBaseDatos.BDReportes, Sistema.BMatic);
                return objDAO.ObtenerDataBase().Insertar(objCmd);
            }
            catch (Exception e)
            {
                //log.Error("Error en insertar registro a bd reporte:", e);
                throw e;
            }
        }

        /// <summary>
        /// Funcion necesaria para insertar registros a la bd de produccion
        /// </summary>
        /// <returns></returns>
        public int InsertarProduccion()
        {
            try
            {
                IDbCommand objCmd = objComand;
                objCmd.CommandTimeout = Define.TIME_OUT;
                DAOFactory objDAO = new DAOFactory(TipoBaseDatos.BDProduccion, Sistema.BMatic);
                return objDAO.ObtenerDataBase().Insertar(objCmd);
            }
            catch (Exception e)
            {
                //log.Error("Error en insertar registro a bd produccion:", e);
                throw e;
            }
        }

        /// <summary>
        /// Función necesaria para actualizar registros de la bd de reportes
        /// </summary>
        /// <returns></returns>
        public int ActualizarReportes()
        {
            try
            {
                IDbCommand objCmd = objComand;
                objCmd.CommandTimeout = Define.TIME_OUT;
                DAOFactory objDAO = new DAOFactory(TipoBaseDatos.BDReportes, Sistema.BMatic);
                return objDAO.ObtenerDataBase().Actualizar(objCmd);
            }
            catch (Exception e)
            {
                //log.Error("Error en actualizar registro a bd reportes:", e);
                throw e;
            }
        }

        /// <summary>
        /// Función necesaria para actualizar registros de la bd de produccion.
        /// </summary>
        /// <returns></returns>
        public int ActualizarProduccion()
        {
            try
            {
                IDbCommand objCmd = objComand;
                objCmd.CommandTimeout = Define.TIME_OUT;
                DAOFactory objDAO = new DAOFactory(TipoBaseDatos.BDProduccion, Sistema.BMatic);
                return objDAO.ObtenerDataBase().Actualizar(objCmd);
            }
            catch (Exception e)
            {
                //log.Error("Error en actualizar registro a bd produccion:", e);
                throw e;
            }
        }

        /// <summary>
        /// Función necesaria para eliminar registros del bd de reportes
        /// </summary>
        /// <returns></returns>
        public int EliminarReportes()
        {
            try
            {
                IDbCommand objCmd = objComand;
                objCmd.CommandTimeout = Define.TIME_OUT;
                DAOFactory objDAO = new DAOFactory(TipoBaseDatos.BDReportes, Sistema.BMatic);
                return objDAO.ObtenerDataBase().Eliminar(objCmd);
            }
            catch (Exception e)
            {
                //log.Error("Error en eliminar registro a bd reportes:", e);
                throw e;
            }
        }

        /// <summary>
        /// Función necesaria para eliminar registros de la bd Produccion.
        /// </summary>
        /// <returns></returns>
        public int EliminarProduccion()
        {
            try
            {
                IDbCommand objCmd = objComand;
                objCmd.CommandTimeout = Define.TIME_OUT;
                DAOFactory objDAO = new DAOFactory(TipoBaseDatos.BDProduccion, Sistema.BMatic);
                return objDAO.ObtenerDataBase().Eliminar(objCmd);
            }
            catch (Exception e)
            {
                //log.Error("Error en eliminar registro a bd produccion:", e);
                throw e;
            }
        }

        /// <summary>
        /// Función para ejecutar una consulta a la bd de reportes usando un datatable
        /// como medio
        /// </summary>
        /// <returns>Objeto de datatable </returns>
        public DataTable SeleccionarDataTableReportes()
        {
            try
            {
                IDbCommand objCmd = objComand;
                objCmd.CommandTimeout = Define.TIME_OUT;
                DAOFactory objDAO = new DAOFactory(TipoBaseDatos.BDReportes, Sistema.BMatic);
                return objDAO.ObtenerDataBase().SeleccionarDataTable(objCmd);
            }
            catch (Exception e)
            {
                //log.Error("Error en obtener datatable de la bd reportes:", e);
                throw e;
            }
        }

        /// <summary>
        /// Función para ejecutar una consulta a la bd de produccion usando un datatable
        /// como medio
        /// </summary>
        /// <returns>Objeto de datatable </returns>
        public DataTable SeleccionarDataTableProduccion()
        {
            try
            {
                IDbCommand objCmd = objComand;
                objCmd.CommandTimeout = Define.TIME_OUT;
                DAOFactory objDAO = new DAOFactory(TipoBaseDatos.BDProduccion, Sistema.BMatic);
                return objDAO.ObtenerDataBase().SeleccionarDataTable(objCmd);
            }
            catch (Exception e)
            {
                //log.Error("Error en obtener datatable de la bd produccion:", e);
                throw e;
            }
        }

        /// <summary>
        /// Función para ejecutar una consulta a la bd de reportes usando un dataset
        /// como medio
        /// </summary>
        /// <returns>Objeto de Dataset </returns>
        public DataSet SeleccionarDataSetReportes()
        {
            try
            {
                IDbCommand objCmd = objComand;
                objCmd.CommandTimeout = Define.TIME_OUT;
                DAOFactory objDAO = new DAOFactory(TipoBaseDatos.BDReportes, Sistema.BMatic);
                return objDAO.ObtenerDataBase().SeleccionarDataSet(objCmd);
            }
            catch (Exception e)
            {
                //log.Error("Error en obtener dataset de la bd reportes:", e);
                throw e;
            }
        }

        /// <summary>
        /// Función para ejecutar una consulta a la bd de produccion usando un dataset
        /// como medio
        /// </summary>
        /// <returns>Objeto de Dataset </returns>
        public DataSet SeleccionarDataSetProduccion()
        {
            try
            {
                IDbCommand objCmd = objComand;
                objCmd.CommandTimeout = Define.TIME_OUT;
                DAOFactory objDAO = new DAOFactory(TipoBaseDatos.BDProduccion, Sistema.BMatic);
                return objDAO.ObtenerDataBase().SeleccionarDataSet(objCmd);
            }
            catch (Exception e)
            {
                //log.Error("Error en obtener dataset de la bd produccion:", e);
                throw e;
            }
        }

        /// <summary>
        /// Función para ejecutar una consulta a la bd de producción usando un datareader
        /// como medio
        /// </summary>
        /// <returns>Objeto de IDataReader implementado por el proveedor de BD definido</returns>
        public IDataReader SeleccionarDataReaderProduccion()
        {
            try
            {
                IDbCommand objCmd = objComand;
                objCmd.CommandTimeout = Define.TIME_OUT;
                DAOFactory objDAO = new DAOFactory(TipoBaseDatos.BDProduccion, Sistema.BMatic);
                objReader = objDAO.ObtenerDataBase().SeleccionarDataReader(objCmd);
                return objReader;
            }
            catch (Exception e)
            {
                //log.Error("Error en obtener datareader de la bd produccion:", e);
                throw e;
            }
        }

        /// <summary>
        /// Función para ejecutar una consulta a la bd de reporte usando un datareader
        /// como medio
        /// </summary>
        /// <returns>Objeto de IDataReader implementado por el proveedor de BD definido</returns>
        public IDataReader SeleccionarDataReaderReporte()
        {
            try
            {
                IDbCommand objCmd = objComand;
                objCmd.CommandTimeout = Define.TIME_OUT;
                DAOFactory objDAO = new DAOFactory(TipoBaseDatos.BDReportes, Sistema.BMatic);
                objReader = objDAO.ObtenerDataBase().SeleccionarDataReader(objCmd);
                return objReader;
            }
            catch (Exception e)
            {
                //log.Error("Error en obtener datareader de la bd reporte:", e);
                throw e;
            }
        }

        /// <summary>
        /// Función necesaria para cerrar la conexion de una consulta echa por un 
        /// datareader
        /// </summary>
        public void CerrarDataReader()
        {
            try
            {
                if (objComand != null)
                {
                    if (objComand.Connection != null)
                    {
                        if (!objComand.Connection.State.Equals(ConnectionState.Closed))
                        {
                            objComand.Connection.Close();
                        }
                        if (objReader != null)
                        {
                            objReader.Close();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                //log.Error("Error en cerrar conexion a bd:", e);
                throw e;
            }
        }

        /// <summary>
        /// Función necesaria para ingresar el script sql a ejecutar
        /// </summary>
        /// <param name="sQueryComand">Script sql</param>
        public void SetearComandText(string sQueryComand)
        {
            DAOFactory objDAO = new DAOFactory(Sistema.BMatic);
            objComand = objDAO.obtenerComand();
            if (objComand != null)
            {
                objComand.CommandText = sQueryComand;
                objComand.CommandType = CommandType.Text;
            }
            else
            {
                Exception ex = new Exception("Error en DataSource.SetearComandText()");
                //log.Error("Error en  SetearComandText() en la definición de IDbCommand",ex);
                throw ex;
            }
        }

        /// <summary>
        /// Función necesaria para ingresar los parametros de la consulta
        /// </summary>
        /// <param name="sParamName">Nombre del parametro</param>
        /// <param name="sParamValue">Valor del parametro</param>
        public void SetearParametro(string sParamName, object sParamValue)
        {
            DAOFactory objDAO = new DAOFactory(Sistema.BMatic);
            IDbDataParameter objParam = objDAO.obtenerParametro();
            objParam.ParameterName = sParamName;
            objParam.Value = sParamValue;
            if (objParam != null)
            {
                objComand.Parameters.Add(objParam);
            }
            else
            {
                Exception ex = new Exception("Error en DataSource.SetearParametro()");
                //log.Error("Error en  SetearParametro() en la definición de IDbDataParameter",ex);
                throw ex;
            }
        }

        ///<summary>
        /// Función necesaria para ingresar un parametro de tipo datetime en la consulta
        /// </summary>
        /// <param name="sParamName">Nombre del parametro</param>
        /// <param name="sParamValue">Valor del parametro</param>
        public void SetearParametroDateTime(string sParamName, object sParamValue)
        {
            DAOFactory objDAO = new DAOFactory(Sistema.BMatic);
            IDbDataParameter objParam = objDAO.obtenerParametro();
            objParam.ParameterName = sParamName;
            objParam.DbType = DbType.DateTime;
            objParam.Value = sParamValue;
            if (objParam != null)
            {
                objComand.Parameters.Add(objParam);
            }
            else
            {
                Exception ex = new Exception("Error en DataSource.SetearParametroDateTime()");
                throw ex;
            }

        }

        /// <summary>
        /// Función necesaria para ingresar un parametro de tipo Integer en la consulta
        /// </summary>
        /// <param name="sParamName">Nombre del parametro</param>
        /// <param name="sParamValue">Valor del parametro</param>
        public void SetearParametroInt(string sParamName, object sParamValue)
        {
            DAOFactory objDAO = new DAOFactory(Sistema.BMatic);
            IDbDataParameter objParam = objDAO.obtenerParametro();
            objParam.ParameterName = sParamName;
            objParam.DbType = DbType.Int32;
            objParam.Value = sParamValue;
            if (objParam != null)
            {
                objComand.Parameters.Add(objParam);
            }
            else
            {
                Exception ex = new Exception("Error en DataSource.SetearParametroInt()");
                throw ex;
            }

        }

        /// <summary>
        /// Función necesaria para obtener los valores del Key del regedit
        /// </summary>
        /// <param name="sParametro">Nombre de valor</param>
        /// <returns>Valor</returns>
        public string ObtenerKeyRegedit(string sParametro)
        {
            DAOFactory objDAO = new DAOFactory(Sistema.BMatic);
            return objDAO.GetString(sParametro);
        }

        /// <summary>
        /// Función que devuelve el valor "Null" si no se encuentra datos para ser insertado u actualizado en la base de datos
        /// </summary>
        /// <param name="valor">valor ha validar</param>
        /// <returns>retorna el objeto con el valor nulo</returns>
        public object ValidarNull2(string valor)
        {

            string strResultado = string.Empty;

            if (valor == "null" || valor == "NULL" || valor == null)
            {
                return DBNull.Value;
            }
            else
            {
                //strResultado = valor;
                return valor;
            }
        }
        

        /// <summary>
        /// Función que devuelve el valor "Null" si no se encuentra datos para ser insertado u actualizado en la base de datos
        /// </summary>
        /// <param name="valor">valor ha validar</param>
        /// <returns>retorna el objeto con el valor nulo</returns>
        public object ValidarNull(string valor)
        {

            string strResultado = string.Empty;

            if (valor == "" || valor == "null" || valor == "NULL" || valor == null)
            {
                return DBNull.Value;
            }
            else
            {
                //strResultado = valor;
                return valor;
            }
        }
    }
}
