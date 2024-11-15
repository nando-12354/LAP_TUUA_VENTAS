using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using LAP.TUUA.DAO;
using LAP.TUUA.ENTIDADES;
using LAP.TUUA.UTIL;


namespace LAP.TUUA.CONTROL
{
    public class BO_Seguridad
    {
        private Cryptografia crypto;

        private DAO_Usuario dao_Usuario;
        private DAO_ArbolModulo dao_ArbolModulo;
        private DAO_ParameGeneral dao_ParameGeneral;

        public List<ArbolModulo> listaPerfil;

        private int numMaxIntentos;

        private Hashtable htIntentos;

        private String errorMessage;

        public BO_Seguridad()
        {
            dao_Usuario = new DAO_Usuario(Property.htSPConfig);
            dao_ArbolModulo = new DAO_ArbolModulo(Property.htSPConfig);
            dao_ParameGeneral = new DAO_ParameGeneral(Property.htSPConfig);

            crypto = new Cryptografia();
            numMaxIntentos = ObtenerMaximoIntentos();
            htIntentos = new Hashtable();
        }

        public void CleanNumIntentos()
        {
            htIntentos.Clear();
        }

        public Usuario autenticar(string sCuenta, string sClave, ref String codUser)
        {
            DataTable dtConsulta = ListarParametros((string)Property.htProperty[Define.ID_PARAM_MIN_CLAVE]);
            int intMinClave = Int32.Parse(dtConsulta.Rows[0].ItemArray.GetValue(4).ToString());
            dtConsulta = ListarParametros((string)Property.htProperty[Define.ID_PARAM_MAX_CLAVE]);
            int intMaxClave = Int32.Parse(dtConsulta.Rows[0].ItemArray.GetValue(4).ToString());
            if (sClave.Length < intMinClave || sClave.Length > intMaxClave)
            {
                errorMessage = "Longitud de clave debe estar entre " + intMinClave.ToString() + " y " + intMaxClave;
                //codUser = "";
                return null;
            }

            Usuario objUsuario = obtenerUsuarioxCuenta(sCuenta);
            dtConsulta = ListarParametros((string)Property.htProperty[Define.ID_PARAM_KEY]);
            String sKey = Convert.ToString(dtConsulta.Rows[0].ItemArray.GetValue(4).ToString());
            String sPwEncriptado = crypto.Encriptar(sClave, sKey);
            if (objUsuario != null)
            {

                codUser = objUsuario.SCodUsuario;

                if (objUsuario.STipoEstadoActual.ToUpper() != "V")
                {
                    errorMessage = (string)LabelConfig.htLabels["logueo.msgVigente"];
                    return null;
                }
                
                if (sPwEncriptado != objUsuario.SPwdActualUsuario)
                {
                    if(htIntentos[objUsuario.SCodUsuario]!=null)
                    {
                        htIntentos[objUsuario.SCodUsuario] = Int32.Parse(htIntentos[objUsuario.SCodUsuario].ToString()) + 1;
                    }
                    else
                    {
                        htIntentos.Add(objUsuario.SCodUsuario, 1);
                    }

                    
                    if (Int32.Parse(htIntentos[objUsuario.SCodUsuario].ToString()) > numMaxIntentos)
                    {
                        actualizarEstadoUsuario(objUsuario.SCodUsuario, "B", objUsuario.SCodUsuario, "1");
                        errorMessage = (string)LabelConfig.htLabels["logueo.msgBloqueado"];
                        return null;
                    }

                    errorMessage = (string)LabelConfig.htLabels["logueo.msgOpeFail"];
                    return null;
                }

                listaPerfil = ListarPerfilArchiving(objUsuario.SCodUsuario);
                if (!VerificarPermisos())
                {
                    errorMessage = (string)LabelConfig.htLabels["logueo.msgPermiso"];
                    return null;
                }
                return objUsuario;
            }

            errorMessage = (string)LabelConfig.htLabels["logueo.msgOpeFail"];
            return null;
        }

        private bool VerificarPermisos()
        {
            if (listaPerfil == null)
            {
                return false;
            }
            for (int i = 0; i < listaPerfil.Count; i++)
            {
                if (listaPerfil[i].SFlgPermitido == "1")
                {
                    return true;
                }
            }
            return false;
        }



        #region Obteniendo parametros
        public int ObtenerMaximoIntentos()
        {
            DataTable dtMaxIntentos = new DataTable();
            dtMaxIntentos = ListarParametros((string)Property.htProperty[Define.ID_PARAM_MAX_INTENTOS]);
            Int32 iMaxIntentos;
            try
            {
                iMaxIntentos = Convert.ToInt32(dtMaxIntentos.Rows[0].ItemArray.GetValue(4).ToString());
            }
            catch
            {
                iMaxIntentos = 4;
            }
            return iMaxIntentos;
        }

        public int ObtenerMaximoInactividad()
        {
            DataTable dtMaxIntentos = new DataTable();
            dtMaxIntentos = ListarParametros((string)Property.htProperty[Define.ID_PARAM_INACTIVIDAD]);
            Int32 iMaxIntentos;
            try
            {
                iMaxIntentos = Convert.ToInt32(dtMaxIntentos.Rows[0].ItemArray.GetValue(4).ToString());
            }
            catch
            {
                iMaxIntentos = 50;
            }
            return iMaxIntentos;
        }
        #endregion

        #region Acceso a Datos directamente
        public List<ArbolModulo> ListarPerfilArchiving(string strUsuario)
        {
            try
            {
                return dao_ArbolModulo.ListarPerfilArchiving(strUsuario);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }

        public DataTable ListarParametros(string as_identificador)
        {
            try
            {
                return dao_ParameGeneral.ObtenerParametroGeneral(as_identificador);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }

        public Usuario obtenerUsuarioxCuenta(string sCuentaUsuario)
        {
            try
            {
                return dao_Usuario.obtenerxCuenta(sCuentaUsuario);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }

        public bool actualizarEstadoUsuario(string sCodUsuario, string sEstado, string SLogUsuarioMod, string strFlgCambioClave)
        {
            try
            {
                return dao_Usuario.actualizarEstado(sCodUsuario, sEstado, SLogUsuarioMod, strFlgCambioClave);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }
        #endregion

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { errorMessage = value; }
        }


        #region  Auditoria
        public bool InsertarAuditoria(string strCodModulo, string strCodSubModulo,
                                               string strCodUsuario, string strTipOperacion)
        {
            DAO_Auditoria daoAuditoria = new DAO_Auditoria(Property.htSPConfig);
            return daoAuditoria.insertar(strCodModulo, strCodSubModulo, strCodUsuario, strTipOperacion);
        }
        #endregion

        public bool InsertarAccesos(LogAcceso objLogAcceso)
        {
            LAP.TUUA.DAO.DAOLogAcceso daoLogAcceso = new DAOLogAcceso(Property.htSPConfig);
            return daoLogAcceso.insertar(objLogAcceso);
        }

    }
}