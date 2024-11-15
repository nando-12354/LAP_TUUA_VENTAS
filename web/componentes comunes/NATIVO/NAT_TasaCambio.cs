using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LAP.TUUA.CONEXION;
using LAP.TUUA.DAO;
using LAP.TUUA.UTIL;
using LAP.TUUA.ENTIDADES;

namespace LAP.TUUA.NATIVO
{
    public class NAT_TasaCambio : IAdministracion
    {
        public string Dsc_PathSPConfig;
        DAO_Auditoria objDAOAuditoria;
        DAO_TasaCambio objDAOTasaCambio;

        public NAT_TasaCambio()
        {
            Dsc_PathSPConfig = (string)Property.htProperty["PATHRECURSOS"];
            objDAOAuditoria = new DAO_Auditoria(Dsc_PathSPConfig);
            objDAOTasaCambio = new DAO_TasaCambio(Dsc_PathSPConfig);
        }

        #region Miembros de IAdministracion

        private string cod_Usuario;
        public string Cod_Usuario
        {
            get
            {
                return cod_Usuario;
            }
            set
            {
                cod_Usuario = value;
            }
        }
        private string cod_Modulo;
        public string Cod_Modulo
        {
            get
            {
                return cod_Modulo;
            }
            set
            {
                cod_Modulo = value;
            }
        }
        private string cod_Sub_Modulo;
        public string Cod_Sub_Modulo
        {
            get
            {
                return cod_Sub_Modulo;
            }
            set
            {
                cod_Sub_Modulo = value;
            }
        }

        #endregion

        #region Miembros de IAdministracion

        public object Registrar(params object[] objeto)
        {
            try
            {
                objDAOTasaCambio.Cod_Modulo = this.Cod_Modulo;
                objDAOTasaCambio.Cod_Sub_Modulo = this.Cod_Sub_Modulo;
                objDAOTasaCambio.Cod_Usuario = this.Cod_Usuario;
                return objDAOTasaCambio.insertarCrit((TasaCambio)objeto[0]);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }

        public object Actualizar(params object[] objeto)
        {
            throw new NotImplementedException();
        }

        #endregion




        #region Miembros de IAdministracion


        public object Eliminar(params object[] objeto)
        {
            try
            {
                objDAOTasaCambio.Cod_Modulo = Cod_Modulo;
                objDAOTasaCambio.Cod_Sub_Modulo = Cod_Sub_Modulo;
                objDAOTasaCambio.Cod_Usuario = Cod_Usuario;
                return objDAOTasaCambio.eliminarCrit((TasaCambio)objeto[0]);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }

        #endregion
    }
}
