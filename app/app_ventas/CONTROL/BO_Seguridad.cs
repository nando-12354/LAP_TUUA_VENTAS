/*
Sistema		    :   TUUA
Aplicación		:   Ventas
Objetivo		:   Proceso de gestión de seguridad.
Especificaciones:   Se considera aquellas marcaciones según el rango programado.
Fecha Creacion	:   11/07/2009	
Programador		:	JCISNEROS
Observaciones	:	
*/ 
using System;
using System.Net;
using System.Collections.Generic;
using System.Text;
using LAP.TUUA.ENTIDADES;
using LAP.TUUA.RESOLVER;
using LAP.TUUA.CONEXION;
using LAP.TUUA.UTIL;
using LAP.TUUA.ALARMAS;
using System.Data;

namespace LAP.TUUA.CONTROL
{
    public class BO_Seguridad
    {
        private Conexion objConexion;
        private string sErrorMessage;
        private Cryptografia objCrypto;
        public int Num_Intentos;
        protected int Num_MaxIntentos;
        public bool Flg_Bloqueado;
        public List<ArbolModulo> listaPerfil;

        public BO_Seguridad()
        {
            objConexion = Resolver.ObtenerConexion(Define.CNX_01);
            objCrypto = new Cryptografia();
            Num_Intentos = 0;
            Flg_Bloqueado = false;
            Num_MaxIntentos = ObtenerMaximoIntentos();

            objConexion.Cod_Usuario = Property.strUsuario;
            objConexion.Cod_Modulo = Property.strModulo;
            objConexion.Cod_Sub_Modulo = Property.strSubModulo;
        }

        public string SErrorMessage
        {
            get
            {
                return sErrorMessage;
            }
            set
            {
                sErrorMessage = value;
            }
        }

        public bool CambiarClave(Usuario objUsuario)
        {
            string sPwEncriptado;
            int intMinClave;
            int intMaxClave;
            DataTable dtConsulta = new DataTable();
            dtConsulta = objConexion.ListarParametros((string)Property.htProperty[Define.ID_PARAM_MIN_CLAVE]);
            intMinClave = Int32.Parse(dtConsulta.Rows[0].ItemArray.GetValue(4).ToString());
            dtConsulta = objConexion.ListarParametros((string)Property.htProperty[Define.ID_PARAM_MAX_CLAVE]);
            intMaxClave = Int32.Parse(dtConsulta.Rows[0].ItemArray.GetValue(4).ToString());
            if (objUsuario.SPwdActualUsuario.Length < intMinClave || objUsuario.SPwdActualUsuario.Length > intMaxClave)
            {
                sErrorMessage = "Longitud de clave debe estar entre " + intMinClave.ToString() + " y " + intMaxClave;
                return false;
            }

            if (VerificarClaveHistorica(objUsuario.SCodUsuario, objUsuario.SPwdActualUsuario))
            {
                dtConsulta = objConexion.ListarParametros((string)Property.htProperty[Define.ID_PARAM_MAX_CLAVE_HIST]);
                int iNumKeyHist = 0;
                if (dtConsulta.Rows.Count > 0)
                {
                    iNumKeyHist = Convert.ToInt32(dtConsulta.Rows[0].ItemArray.GetValue(4).ToString());
                }
                sErrorMessage = "La contraseña se encuentra registrada dentro de los ultimos " + iNumKeyHist + " ingresos de Clave";
                return false;
            }

            dtConsulta = objConexion.ListarParametros((string)Property.htProperty[Define.ID_PARAM_KEY]);
            
            string sKey = Convert.ToString(dtConsulta.Rows[0].ItemArray.GetValue(4).ToString());
            sPwEncriptado = objCrypto.Encriptar(objUsuario.SPwdActualUsuario, sKey);
            objUsuario.SPwdActualUsuario = sPwEncriptado;

            int DiasVencimiento = 0;
            DataTable dt_parametro = objConexion.ListarParametros((string)Property.htProperty[Define.ID_PARAM_DIAS_VIGENCIA]);

            if (dt_parametro.Rows.Count > 0)
            {
                DiasVencimiento = Convert.ToInt32(dt_parametro.Rows[0].ItemArray.GetValue(4).ToString());
            }

            DateTime hoy = DateTime.Today.Date;
            DateTime diaV = hoy.AddDays(+DiasVencimiento);
            return objConexion.actualizarContraseñaUsuario(objUsuario.SCodUsuario, sPwEncriptado, objUsuario.SCodUsuario, diaV);
        }

        public bool VerificarClaveHistorica(string strUsuario,string strClave)
        {
            string sKey = "";
            int sNumKeyHist = 0;
            string sPwEncriptado;
            DataTable dtConsulta;
            dtConsulta = new DataTable();

            dtConsulta = objConexion.ListarParametros((string)Property.htProperty[Define.ID_PARAM_KEY]);
            if (dtConsulta.Rows.Count > 0)
            {
                sKey = Convert.ToString(dtConsulta.Rows[0].ItemArray.GetValue(4).ToString());
            }

            dtConsulta = new DataTable();
            dtConsulta = objConexion.ListarParametros((string)Property.htProperty[Define.ID_PARAM_MAX_CLAVE_HIST]);
            if (dtConsulta.Rows.Count > 0)
            {
                sNumKeyHist = Convert.ToInt32(dtConsulta.Rows[0].ItemArray.GetValue(4).ToString());
            }

            if (sNumKeyHist > 0 && sKey != "")
            {
                sPwEncriptado = objCrypto.Encriptar(strClave, sKey);

                return objConexion.obtenerClaveUsuHist(strUsuario, sPwEncriptado, sNumKeyHist);
            }
            return false;
        }
        
        public Usuario autenticar(string sCuenta, string sClave)
        {
            Usuario objUsuario = null;
            DataTable dtConsulta = new DataTable();

            int intMinClave;
            int intMaxClave;
            dtConsulta = objConexion.ListarParametros((string)Property.htProperty[Define.ID_PARAM_MIN_CLAVE]);
            intMinClave = Int32.Parse(dtConsulta.Rows[0].ItemArray.GetValue(4).ToString());
            dtConsulta = objConexion.ListarParametros((string)Property.htProperty[Define.ID_PARAM_MAX_CLAVE]);
            intMaxClave = Int32.Parse(dtConsulta.Rows[0].ItemArray.GetValue(4).ToString());

            if (sClave.Length < intMinClave || sClave.Length > intMaxClave)
            {
                sErrorMessage = "Longitud de clave debe estar entre " + intMinClave.ToString() + " y " + intMaxClave;
                return null;
            }

            objUsuario = objConexion.obtenerUsuarioxCuenta(sCuenta);
            dtConsulta = objConexion.ListarParametros((string)Property.htProperty[Define.ID_PARAM_KEY]);
            string sKey = Convert.ToString(dtConsulta.Rows[0].ItemArray.GetValue(4).ToString());
            string sPwEncriptado;
            sPwEncriptado = objCrypto.Encriptar(sClave, sKey);
            
            if (objUsuario != null)
            {
                IPHostEntry IPs = Dns.GetHostByName("");
                IPAddress[] Direcciones = IPs.AddressList;
                String IpClient = Direcciones[0].ToString();
                
                Num_Intentos++;
                if (sPwEncriptado != objUsuario.SPwdActualUsuario)
                {
                    this.sErrorMessage = (string)LabelConfig.htLabels["logueo.msgOpeFail"];
                    if (Num_Intentos > Num_MaxIntentos)
                    {
                        objConexion.actualizarEstadoUsuario(objUsuario.SCodUsuario, "B", objUsuario.SCodUsuario,"1");
                        Flg_Bloqueado = true;


                        //GeneraAlarma - Bloqueo de Usuario
                        GestionAlarma.Registrar((string)Property.htProperty["PATHRECURSOS"], "W0000064", "I21", IpClient, "2", "Alerta W0000064", "Bloqueo de usuario en el Modulo de Ventas, al tratar de cambiar la contraseña, usuario: " + objUsuario.SCodUsuario, Convert.ToString(objUsuario.SCodUsuario));
 
                        SErrorMessage = (string)LabelConfig.htLabels["logueo.msgBloqueado"];
                        Num_Intentos = 0;
                        return null;
                    }
                    return null;
                }

                if (objUsuario.STipoEstadoActual.ToUpper() != "V")
                {
                    this.sErrorMessage = (string)LabelConfig.htLabels["logueo.msgVigente"];
                    Num_Intentos = 0;
                    return null;
                }
                listaPerfil = objConexion.ListarPerfilVenta(objUsuario.SCodUsuario);
                if (!VerificarPermisos())
                {
                    this.sErrorMessage = (string)LabelConfig.htLabels["logueo.msgPermiso"];
                    return null;
                }       
                return objUsuario;
            }

            this.sErrorMessage = (string)LabelConfig.htLabels["logueo.msgOpeFail"];
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

        public int ObtenerMaximoIntentos()
        {
            DataTable dtMaxIntentos = new DataTable();
            dtMaxIntentos = objConexion.ListarParametros((string)Property.htProperty[Define.ID_PARAM_MAX_INTENTOS]);
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
            dtMaxIntentos = objConexion.ListarParametros((string)Property.htProperty[Define.ID_PARAM_INACTIVIDAD]);
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

        public Usuario AutenticarSupervisor(string sCuenta, string sClave)
        {
            Usuario objUsuario = null;
            DataTable dtConsulta = new DataTable();

            int intMinClave;
            int intMaxClave;
            dtConsulta = objConexion.ListarParametros((string)Property.htProperty[Define.ID_PARAM_MIN_CLAVE]);
            intMinClave = Int32.Parse(dtConsulta.Rows[0].ItemArray.GetValue(4).ToString());
            dtConsulta = objConexion.ListarParametros((string)Property.htProperty[Define.ID_PARAM_MAX_CLAVE]);
            intMaxClave = Int32.Parse(dtConsulta.Rows[0].ItemArray.GetValue(4).ToString());
            if (sClave.Length < intMinClave || sClave.Length > intMaxClave)
            {
                sErrorMessage = "Longitud de clave debe estar entre " + intMinClave.ToString() + " y " + intMaxClave;
                return null;
            }

            objUsuario = objConexion.obtenerUsuarioxCuenta(sCuenta);
            dtConsulta = objConexion.ListarParametros((string)Property.htProperty[Define.ID_PARAM_KEY]);
            string sKey = Convert.ToString(dtConsulta.Rows[0].ItemArray.GetValue(4).ToString());
            string sPwEncriptado;
            sPwEncriptado = objCrypto.Encriptar(sClave, sKey);
            
            if (objUsuario != null)
            {
                if (objUsuario.STipoEstadoActual.ToUpper() != "V")
                {
                    this.sErrorMessage = (string)LabelConfig.htLabels["logueo.msgVigente"];
                    Num_Intentos = 0;
                    return null;
                }
                Num_Intentos++;
                if (sPwEncriptado != objUsuario.SPwdActualUsuario)
                {
                    this.sErrorMessage = (string)LabelConfig.htLabels["logueo.msgOpeFail"];
                    if (Num_Intentos > Num_MaxIntentos)
                    {
                        objConexion.actualizarEstadoUsuario(objUsuario.SCodUsuario, "B", objUsuario.SCodUsuario,"1");
                        Flg_Bloqueado = true;
                        SErrorMessage = (string)LabelConfig.htLabels["logueo.msgBloqueado"];
                        Num_Intentos = 0;
                        return null;
                    }
                    return null;
                }

                if (objUsuario.STipoEstadoActual.ToUpper() != "V")
                {
                    this.sErrorMessage = (string)LabelConfig.htLabels["logueo.msgVigente"];
                    Num_Intentos = 0;
                    return null;
                }

                if (!VerificarSupervisor(objUsuario.SCodUsuario))
                {
                    this.sErrorMessage = (string)LabelConfig.htLabels["logueo.msgSupervisor"];
                    return null;
                }
                return objUsuario;
            }

            this.sErrorMessage = (string)LabelConfig.htLabels["logueo.msgOpeFail"];
            return null;
        }

        public bool VerificarSupervisor(string strUsuario)
        {
            List<Rol> listaRoles = objConexion.listarRolesAsignados(strUsuario);
            if (listaRoles == null)
            {
                return false;
            }
            for (int i = 0; i < listaRoles.Count; i++)
            {
                if (listaRoles[i].SCodRol == (string)Property.htProperty[Define.COD_ROL_SUPERVISOR])
                {
                    return true;
                }
            }
            return false;
        }

        public bool InsertarAuditoria(string strCodUsuario)
        {
            string strCodModulo = "VTA";
            string strCodSubModulo = "L02";
            string strTipOperacion = "1";
            return objConexion.InsertarAuditoria(strCodModulo, strCodSubModulo, strCodUsuario, strTipOperacion);
        }

        public DataTable obtenerFecha()
        {
            return objConexion.obtenerFecha();
        }

        public ClaveUsuHist obtenerUsuarioHist(string sCodUsuario)
        {
            return objConexion.obtenerUsuarioHist(sCodUsuario);
        }

    }

}
