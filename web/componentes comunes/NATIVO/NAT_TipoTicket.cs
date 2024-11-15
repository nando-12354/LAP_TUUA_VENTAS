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
    class NAT_TipoTicket : IAdministracion
    {
        public string Dsc_PathSPConfig;
        DAO_Auditoria objDAOAuditoria;
        DAO_PrecioTicket objDAOPrecioTicket;

        public NAT_TipoTicket()
        {
            Dsc_PathSPConfig = (string)Property.htProperty["PATHRECURSOS"];
            objDAOAuditoria = new DAO_Auditoria(Dsc_PathSPConfig);
            objDAOPrecioTicket = new DAO_PrecioTicket(Dsc_PathSPConfig);
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
                //DAO_PrecioTicket objDAOPrcTicket = new DAO_PrecioTicket(Dsc_PathSPConfig);
                objDAOPrecioTicket.Cod_Modulo = Cod_Modulo;
                objDAOPrecioTicket.Cod_Sub_Modulo = Cod_Sub_Modulo;
                objDAOPrecioTicket.Cod_Usuario = Cod_Usuario;
                return objDAOPrecioTicket.insertarCrit((PrecioTicket)objeto[0]);
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
                objDAOPrecioTicket.Cod_Modulo = Cod_Modulo;
                objDAOPrecioTicket.Cod_Sub_Modulo = Cod_Sub_Modulo;
                objDAOPrecioTicket.Cod_Usuario = Cod_Usuario;
                return objDAOPrecioTicket.eliminarCrit((PrecioTicket)objeto[0]);
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
