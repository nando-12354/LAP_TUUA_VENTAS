using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace BMatic.DA
{
    class DAOSqlServer : IDAO
    {
        SqlConnection objConexion;
        SqlDataReader dtReader;

        public DAOSqlServer(string strCadenaConexion)
        {
            objConexion = new SqlConnection(strCadenaConexion);
        }

        #region IDAO Members

        public int Insertar(IDbCommand objComando)
        {
            int iRow = 0;
            try
            {
                SqlCommand cmd = (SqlCommand)objComando;
                cmd.Connection = objConexion;
                objConexion.Open();
                iRow = cmd.ExecuteNonQuery();
                objConexion.Close();
            }
            catch (SqlException e)
            {
                //log.Error("Error en insertar registro: SQL>" + objComando.CommandText + " CNX>" + objConexion.ConnectionString, e);
                throw e;
            }
            finally
            {
                if (!objConexion.State.Equals(ConnectionState.Closed))
                {
                    objConexion.Close();
                }
            }

            return iRow;
        }

        public int Actualizar(IDbCommand objComando)
        {
            int iRow = 0;
            try
            {
                SqlCommand cmd = (SqlCommand)objComando;
                cmd.Connection = objConexion;
                objConexion.Open();
                iRow = cmd.ExecuteNonQuery();
                objConexion.Close();
            }
            catch (SqlException e)
            {
                //log.Error("Error en actualizar registro: SQL>" + objComando.CommandText + " CNX>" + objConexion.ConnectionString, e);
                throw e;
            }
            finally
            {
                if (!objConexion.State.Equals(ConnectionState.Closed))
                {
                    objConexion.Close();
                }
            }

            return iRow;
        }

        public int Eliminar(IDbCommand objComando)
        {
            int iRow = 0;
            try
            {
                SqlCommand cmd = (SqlCommand)objComando;
                cmd.Connection = objConexion;
                objConexion.Open();
                iRow = cmd.ExecuteNonQuery();
                objConexion.Close();
            }
            catch (SqlException e)
            {
                // log.Error("Error en eliminar registro: SQL>" + objComando.CommandText + " CNX>" + objConexion.ConnectionString, e);
                throw e;
            }
            finally
            {
                if (!objConexion.State.Equals(ConnectionState.Closed))
                {
                    objConexion.Close();
                }
            }

            return iRow;
        }

        public DataTable SeleccionarDataTable(IDbCommand objComando)
        {
            DataTable dtTable = new DataTable(Define.NAME_TABLE_DEFAULT);

            try
            {
                SqlCommand cmd = (SqlCommand)objComando;
                cmd.Connection = objConexion;
                SqlDataAdapter Adapter = new SqlDataAdapter(cmd);
                Adapter.Fill(dtTable);
            }
            catch (SqlException e)
            {
                //log.Error("Error en listar registros: SQL>" + objComando.CommandText + " CNX>" + objConexion.ConnectionString, e);
                throw e;
            }

            return dtTable;
        }

        public DataSet SeleccionarDataSet(IDbCommand objComando)
        {
            DataSet dtDatos = new DataSet(Define.NAME_TABLE_DEFAULT);

            try
            {
                SqlCommand cmd = (SqlCommand)objComando;
                cmd.Connection = objConexion;
                SqlDataAdapter Adapter = new SqlDataAdapter(cmd);
                Adapter.Fill(dtDatos);
            }
            catch (SqlException e)
            {
                // log.Error("Error en listar registros: SQL>" + objComando.CommandText + " CNX>" + objConexion.ConnectionString, e);
                throw e;
            }

            return dtDatos;
        }

        public IDataReader SeleccionarDataReader(IDbCommand objComando)
        {
            try
            {
                SqlCommand cmd = (SqlCommand)objComando;
                cmd.Connection = objConexion;
                objConexion.Open();
                dtReader = cmd.ExecuteReader();
            }
            catch (SqlException e)
            {
                //log.Error("Error en listar registros: SQL>" + objComando.CommandText + " CNX>" + objConexion.ConnectionString, e);
                throw e;
            }

            return dtReader;
        }

        public void CerrarDataReader()
        {
            try
            {
                if (!objConexion.State.Equals(ConnectionState.Closed))
                {
                    objConexion.Close();
                }
                if (dtReader != null)
                {
                    dtReader.Close();
                }
            }
            catch (SqlException e)
            {
                throw e;
            }
        }

        #endregion
    }
}
