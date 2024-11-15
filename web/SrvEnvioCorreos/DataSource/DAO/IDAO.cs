using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;

namespace BMatic.DA
{
    public interface IDAO
    {
        int Insertar(IDbCommand objComando);
        int Actualizar(IDbCommand objComando);
        int Eliminar(IDbCommand objComando);
        DataTable SeleccionarDataTable(IDbCommand objComando);
        DataSet SeleccionarDataSet(IDbCommand objComando);
        IDataReader SeleccionarDataReader(IDbCommand objComand);
        void CerrarDataReader();
    }
}
