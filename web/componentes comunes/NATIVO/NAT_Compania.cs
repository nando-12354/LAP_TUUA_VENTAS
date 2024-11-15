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
    class NAT_Compania : IAdministracion
    {
        public string Dsc_PathSPConfig;
        DAO_Compania objDAOCompania;
        DAO_Representante objDAORepresentante;
        DAO_ModVentaComp objDAO_ModVentaComp;
        DAO_ModVentaCompAtr objDAOModVentaCompAtr;

        public NAT_Compania()
        {
            Dsc_PathSPConfig = (string)Property.htProperty["PATHRECURSOS"];
            objDAOCompania = new DAO_Compania(Dsc_PathSPConfig);
            objDAORepresentante = new DAO_Representante(Dsc_PathSPConfig);
            objDAO_ModVentaComp = new DAO_ModVentaComp(Dsc_PathSPConfig);
            objDAOModVentaCompAtr = new DAO_ModVentaCompAtr(Dsc_PathSPConfig);
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

        public object Registrar(params object[] objeto)
        {
            bool bRpta = false;

            if (((string)objeto[1]).Equals("Compania"))
            {
                try
                {
                    objDAOCompania.Cod_Modulo = Cod_Modulo;
                    objDAOCompania.Cod_Sub_Modulo = Cod_Sub_Modulo;
                    objDAOCompania.Cod_Usuario = Cod_Usuario;
                    bRpta = objDAOCompania.insertarCrit((Compania)objeto[0]);
                }
                catch (Exception ex)
                {
                    ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                    ErrorHandler.Flg_Error = true;
                    throw;
                }
            }
            else if (((string)objeto[1]).Equals("RepresentantCia"))
            {
                try
                {
                    objDAORepresentante.Cod_Modulo = Cod_Modulo;
                    objDAORepresentante.Cod_Sub_Modulo = Cod_Sub_Modulo;
                    objDAORepresentante.Cod_Usuario = Cod_Usuario;
                    bRpta = objDAORepresentante.insertarCrit((RepresentantCia)objeto[0]);
                }
                catch (Exception ex)
                {
                    ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                    ErrorHandler.Flg_Error = true;
                    throw;
                }
            }
            else if (((string)objeto[1]).Equals("ModVentaComp"))
            {
                try
                {
                    objDAO_ModVentaComp.Cod_Modulo = Cod_Modulo;
                    objDAO_ModVentaComp.Cod_Sub_Modulo = Cod_Sub_Modulo;
                    objDAO_ModVentaComp.Cod_Usuario = Cod_Usuario;
                    bRpta = objDAO_ModVentaComp.insertarCrit((ModVentaComp)objeto[0]);
                }
                catch (Exception ex)
                {
                    ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                    ErrorHandler.Flg_Error = true;
                    throw;
                }
            }
            else if (((string)objeto[1]).Equals("ModVentaCompAtr"))
            {
                try
                {
                    objDAOModVentaCompAtr.Cod_Modulo = Cod_Modulo;
                    objDAOModVentaCompAtr.Cod_Sub_Modulo = Cod_Sub_Modulo;
                    objDAOModVentaCompAtr.Cod_Usuario = Cod_Usuario;
                    bRpta = objDAOModVentaCompAtr.insertarCrit((ModVentaCompAtr)objeto[0]);
                }
                catch (Exception ex)
                {
                    ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                    ErrorHandler.Flg_Error = true;
                    throw;
                }
            }

            return bRpta;
        }

        public object Actualizar(params object[] objeto)
        {
            bool bRpta = false;

            if (((string)objeto[1]).Equals("Compania"))
            {
                try
                {
                    objDAOCompania.Cod_Modulo = Cod_Modulo;
                    objDAOCompania.Cod_Sub_Modulo = Cod_Sub_Modulo;
                    objDAOCompania.Cod_Usuario = Cod_Usuario;
                    bRpta = objDAOCompania.actualizarCrit((Compania)objeto[0]);
                }
                catch (Exception ex)
                {
                    ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                    ErrorHandler.Flg_Error = true;
                    throw;
                }
            }
            else if (((string)objeto[1]).Equals("RepresentantCia"))
            {
                try
                {
                    objDAORepresentante.Cod_Modulo = Cod_Modulo;
                    objDAORepresentante.Cod_Sub_Modulo = Cod_Sub_Modulo;
                    objDAORepresentante.Cod_Usuario = Cod_Usuario;
                    bRpta = objDAORepresentante.actualizarCrit((RepresentantCia)objeto[0]);
                }
                catch (Exception ex)
                {
                    ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                    ErrorHandler.Flg_Error = true;
                    throw;
                }
            }
            else if (((string)objeto[1]).Equals("ModVentaComp"))
            {
                try
                {
                    objDAO_ModVentaComp.Cod_Modulo = Cod_Modulo;
                    objDAO_ModVentaComp.Cod_Sub_Modulo = Cod_Sub_Modulo;
                    objDAO_ModVentaComp.Cod_Usuario = Cod_Usuario;
                    bRpta = objDAO_ModVentaComp.actualizarCrit((ModVentaComp)objeto[0]);
                }
                catch (Exception ex)
                {
                    ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                    ErrorHandler.Flg_Error = true;
                    throw;
                }
            }
            else if (((string)objeto[1]).Equals("ModVentaCompAtr"))
            {
                try
                {
                    objDAOModVentaCompAtr.Cod_Modulo = Cod_Modulo;
                    objDAOModVentaCompAtr.Cod_Sub_Modulo = Cod_Sub_Modulo;
                    objDAOModVentaCompAtr.Cod_Usuario = Cod_Usuario;
                    bRpta = objDAOModVentaCompAtr.actualizarCrit((ModVentaCompAtr)objeto[0]);
                }
                catch (Exception ex)
                {
                    ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                    ErrorHandler.Flg_Error = true;
                    throw;
                }
            }

            return bRpta;
        }

        public object Eliminar(params object[] objeto)
        {
            bool bRpta = false;

            if (((string)objeto[1]).Equals("ModVentaComp"))
            {
                try
                {
                    objDAO_ModVentaComp.Cod_Modulo = Cod_Modulo;
                    objDAO_ModVentaComp.Cod_Sub_Modulo = Cod_Sub_Modulo;
                    objDAO_ModVentaComp.Cod_Usuario = Cod_Usuario;
                    bRpta = objDAO_ModVentaComp.eliminarCrit((ModVentaComp)objeto[0]);
                }
                catch (Exception ex)
                {
                    ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                    ErrorHandler.Flg_Error = true;
                    throw;
                }
            }
            else if (((string)objeto[1]).Equals("ModVentaCompAtr"))
            {
                try
                {
                    objDAOModVentaCompAtr.Cod_Modulo = Cod_Modulo;
                    objDAOModVentaCompAtr.Cod_Sub_Modulo = Cod_Sub_Modulo;
                    objDAOModVentaCompAtr.Cod_Usuario = Cod_Usuario;
                    bRpta = objDAOModVentaCompAtr.eliminarCrit((ModVentaCompAtr)objeto[0]);
                }
                catch (Exception ex)
                {
                    ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                    ErrorHandler.Flg_Error = true;
                    throw;
                }
            }

            return bRpta;
        }

        #endregion
    }
}
