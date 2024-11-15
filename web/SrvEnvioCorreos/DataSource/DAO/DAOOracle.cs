using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OracleClient;

namespace BMatic.DA
{
    public class DAOOracle : IDAO
    {
        public DAOOracle(string strCadenaConexion)
        {

        }

        #region IDAO Members

        public int Insertar(IDbCommand objComando)
        {
            throw new NotImplementedException();
        }

        public int Actualizar(IDbCommand objComando)
        {
            throw new NotImplementedException();
        }

        public int Eliminar(IDbCommand objComando)
        {
            throw new NotImplementedException();
        }

        public DataTable SeleccionarDataTable(IDbCommand objComando)
        {
            throw new NotImplementedException();
        }

        public DataSet SeleccionarDataSet(IDbCommand objComando)
        {
            throw new NotImplementedException();
        }

        public IDataReader SeleccionarDataReader(IDbCommand objComand)
        {
            throw new NotImplementedException();
        }

        public void CerrarDataReader()
        {
            throw new NotImplementedException();
        }

        #endregion
    }

}
