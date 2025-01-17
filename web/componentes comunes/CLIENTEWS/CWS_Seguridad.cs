using System;
using System.Collections.Generic;
using System.Text;
using LAP.TUUA.ENTIDADES;
using LAP.TUUA.CONEXION;
using wsSeguridad = LAP.TUUA.CLIENTEWS.WSSeguridad;
using wsConsulta = LAP.TUUA.CLIENTEWS.WSConsultas;
using System.Data;

namespace LAP.TUUA.CLIENTEWS
{
    public class CWS_Seguridad : CWS_Conexion
    {
        protected wsSeguridad.WSSeguridad objWSSeguridad;

        public CWS_Seguridad()
        {
            objWSSeguridad = new wsSeguridad.WSSeguridad();
        }


        #region Usuario
        public override Usuario autenticar(string sCuenta, string sClave)
        {
            wsSeguridad.Usuario objWSUsuario = null;
            objWSUsuario = objWSSeguridad.autenticar(sCuenta, sClave);
            if (objWSUsuario != null)
            {

                return new Usuario(objWSUsuario.SCodUsuario, objWSUsuario.SNomUsuario,
                    objWSUsuario.SApeUsuario, objWSUsuario.SCtaUsuario, objWSUsuario.SPwdActualUsuario,
                    objWSUsuario.SFlgCambioClave, objWSUsuario.STipoEstadoActual, objWSUsuario.DtFchVigencia,
                    objWSUsuario.STipoGrupo, objWSUsuario.SFchCreacion, objWSUsuario.SHoraCreacion,
                    objWSUsuario.SCodUsuarioCreacion, objWSUsuario.SLogUsuarioMod, objWSUsuario.SLogFechaMod,
                    objWSUsuario.SLogHoraMod);
            }
            return null;
        }

        public override bool CambiarClave(Usuario objUsuario)
        {
            wsSeguridad.Usuario objWSUsuario = new wsSeguridad.Usuario();
            objWSUsuario.SCodUsuario = objUsuario.SCodUsuario;
            objWSUsuario.SNomUsuario = objUsuario.SNomUsuario;
            objWSUsuario.SApeUsuario = objUsuario.SApeUsuario;
            objWSUsuario.SCtaUsuario = objUsuario.SCtaUsuario;
            objWSUsuario.SPwdActualUsuario = objUsuario.SPwdActualUsuario;
            objWSUsuario.SFlgCambioClave = objUsuario.SFlgCambioClave;
            objWSUsuario.STipoEstadoActual = objUsuario.STipoEstadoActual;
            objWSUsuario.DtFchVigencia = objUsuario.DtFchVigencia;
            objWSUsuario.STipoGrupo = objUsuario.STipoGrupo;
            objWSUsuario.SFchCreacion = objUsuario.SFchCreacion;
            objWSUsuario.SHoraCreacion = objUsuario.SHoraCreacion;
            objWSUsuario.SCodUsuarioCreacion = objUsuario.SCodUsuarioCreacion;
            objWSUsuario.SLogUsuarioMod = objUsuario.SLogUsuarioMod;
            objWSUsuario.SLogFechaMod = objUsuario.SLogFechaMod;
            objWSUsuario.SLogHoraMod = objUsuario.SLogHoraMod;
            return objWSSeguridad.actualizarUsuario(objWSUsuario);
        }

        public override bool registrarUsuario(Usuario objUsuario)
        {

            wsSeguridad.Usuario objWSUsuario = new wsSeguridad.Usuario();

            objWSUsuario.SCodUsuario = objUsuario.SCodUsuario;
            objWSUsuario.SNomUsuario = objUsuario.SNomUsuario;
            objWSUsuario.SApeUsuario = objUsuario.SApeUsuario;
            objWSUsuario.SCtaUsuario = objUsuario.SCtaUsuario;
            objWSUsuario.SPwdActualUsuario = objUsuario.SPwdActualUsuario;
            objWSUsuario.SFlgCambioClave = objUsuario.SFlgCambioClave;
            objWSUsuario.STipoEstadoActual = objUsuario.STipoEstadoActual;
            objWSUsuario.DtFchVigencia = objUsuario.DtFchVigencia;
            objWSUsuario.STipoGrupo = objUsuario.STipoGrupo;
            objWSUsuario.SFchCreacion = objUsuario.SFchCreacion;
            objWSUsuario.SHoraCreacion = objUsuario.SHoraCreacion;
            objWSUsuario.SCodUsuarioCreacion = objUsuario.SCodUsuarioCreacion;
            objWSUsuario.SLogUsuarioMod = objUsuario.SLogUsuarioMod;
            objWSUsuario.SLogFechaMod = objUsuario.SLogFechaMod;
            objWSUsuario.SLogHoraMod = objUsuario.SLogHoraMod;

            return objWSSeguridad.registrarUsuario(objWSUsuario);

        }

        public override bool actualizarUsuario(Usuario objUsuario)
        {

            wsSeguridad.Usuario objWSUsuario = new wsSeguridad.Usuario();

            objWSUsuario.SCodUsuario = objUsuario.SCodUsuario;
            objWSUsuario.SNomUsuario = objUsuario.SNomUsuario;
            objWSUsuario.SApeUsuario = objUsuario.SApeUsuario;
            objWSUsuario.SCtaUsuario = objUsuario.SCtaUsuario;
            objWSUsuario.SPwdActualUsuario = objUsuario.SPwdActualUsuario;
            objWSUsuario.SFlgCambioClave = objUsuario.SFlgCambioClave;
            objWSUsuario.STipoEstadoActual = objUsuario.STipoEstadoActual;
            objWSUsuario.DtFchVigencia = objUsuario.DtFchVigencia;
            objWSUsuario.STipoGrupo = objUsuario.STipoGrupo;
            objWSUsuario.SFchCreacion = objUsuario.SFchCreacion;
            objWSUsuario.SHoraCreacion = objUsuario.SHoraCreacion;
            objWSUsuario.SCodUsuarioCreacion = objUsuario.SCodUsuarioCreacion;
            objWSUsuario.SLogUsuarioMod = objUsuario.SLogUsuarioMod;
            objWSUsuario.SLogFechaMod = objUsuario.SLogFechaMod;
            objWSUsuario.SLogHoraMod = objUsuario.SLogHoraMod;
            return objWSSeguridad.actualizarUsuario(objWSUsuario);
        }

        public override bool eliminarUsuario(string sCodUsuario, string LogUsuarioMod)
        {

            return objWSSeguridad.eliminarUsuario(sCodUsuario, LogUsuarioMod);

        }

        public override Usuario obtenerUsuario(string sCodUsuario)
        {

            wsSeguridad.Usuario objWSUsuario = null;
            objWSUsuario = objWSSeguridad.obtenerUsuario(sCodUsuario);
            if (objWSUsuario != null)
            {
                return new Usuario(objWSUsuario.SCodUsuario, objWSUsuario.SNomUsuario,
                    objWSUsuario.SApeUsuario, objWSUsuario.SCtaUsuario, objWSUsuario.SPwdActualUsuario,
                    objWSUsuario.SFlgCambioClave, objWSUsuario.STipoEstadoActual, objWSUsuario.DtFchVigencia,
                    objWSUsuario.STipoGrupo, objWSUsuario.SFchCreacion, objWSUsuario.SHoraCreacion,
                    objWSUsuario.SCodUsuarioCreacion, objWSUsuario.SLogUsuarioMod, objWSUsuario.SLogFechaMod,
                    objWSUsuario.SLogHoraMod);
            }
            else return null;
        }

        public override Usuario obtenerUsuarioxCuenta(string sCuentaUsuario)
        {

            wsSeguridad.Usuario objWSUsuario = null;
            objWSUsuario = objWSSeguridad.obtenerUsuarioxCuenta(sCuentaUsuario);
            if (objWSUsuario != null)
            {
                return new Usuario(objWSUsuario.SCodUsuario, objWSUsuario.SNomUsuario,
                    objWSUsuario.SApeUsuario, objWSUsuario.SCtaUsuario, objWSUsuario.SPwdActualUsuario,
                    objWSUsuario.SFlgCambioClave, objWSUsuario.STipoEstadoActual, objWSUsuario.DtFchVigencia,
                    objWSUsuario.STipoGrupo, objWSUsuario.SFchCreacion, objWSUsuario.SHoraCreacion,
                    objWSUsuario.SCodUsuarioCreacion, objWSUsuario.SLogUsuarioMod, objWSUsuario.SLogFechaMod,
                    objWSUsuario.SLogHoraMod);
            }
            else return null;
        }

        public override bool actualizarContrase�aUsuario(string sCodUsuario, string sContrase�a, string SLogUsuarioMod, DateTime DtFchVigencia)
        {
            return objWSSeguridad.actualizarContrase�aUsuario(sCodUsuario, sContrase�a, SLogUsuarioMod, DtFchVigencia);

        }

        public override bool actualizarEstadoUsuario(string sCodUsuario, string sEstado, string SLogUsuarioMod, string sFlagCambPw)
        {
            return objWSSeguridad.actualizarEstadoUsuario(sCodUsuario, sEstado, SLogUsuarioMod, sFlagCambPw);

        }

        public override DataTable obtenerFecha()
        {
            return objWSSeguridad.obtenerFecha();
        }

        #endregion

        #region Arbol Modulo


        public override ArbolModulo ObtenerArbolModulo(string sCodModulo, string sCodProceso, string sCodRol)
        {
            wsSeguridad.ArbolModulo objWSArbolModulo = null;
            objWSArbolModulo = objWSSeguridad.ObtenerArbolModulo(sCodModulo, sCodProceso, sCodRol);

            if (objWSArbolModulo != null)
            {
                return new ArbolModulo(objWSArbolModulo.SCodProceso, objWSArbolModulo.SCodProcesoPadre,
                    objWSArbolModulo.SCodModulo, objWSArbolModulo.SIdProceso, objWSArbolModulo.SDscProceso,
                    objWSArbolModulo.ITipNivel, objWSArbolModulo.STipEstado, objWSArbolModulo.SFlgPermitido,
                    objWSArbolModulo.SDscArchivo, objWSArbolModulo.SDscIcono, objWSArbolModulo.SDscTextoAyuda,
                    objWSArbolModulo.SDscIndCritic, objWSArbolModulo.SDscColorCritic, objWSArbolModulo.SDscTabFiltro,
                    objWSArbolModulo.SDscLicencia, objWSArbolModulo.INumPosProceso, objWSArbolModulo.SDscModulo, objWSArbolModulo.SNomRol, objWSArbolModulo.INumPosModulo, objWSArbolModulo.STipModulo);
            }
            else return null;

        }

        public override List<ArbolModulo> ListarPerfilVenta(string strUsuario)
        {
            wsSeguridad.ArbolModulo[] lista = objWSSeguridad.ListarPerfilVenta(strUsuario);
            List<ArbolModulo> listaArbol = new List<ArbolModulo>();
            for (int i = 0; i < lista.Length; i++)
            {
                ArbolModulo objArbol = new ArbolModulo();
                objArbol.SCodModulo = lista[i].SCodModulo;
                objArbol.SCodProceso = lista[i].SCodProceso;
                objArbol.SDscModulo = lista[i].SDscModulo;
                objArbol.SDscProceso = lista[i].SDscProceso;
                objArbol.SFlgPermitido = lista[i].SFlgPermitido;
                listaArbol.Add(objArbol);
            }
            return listaArbol;
        }

        public override DataTable LLenarMenu(string sCodUsuario)
        {
            DataTable DTArbolModulo;
            DTArbolModulo = objWSSeguridad.listarArbolModulo(sCodUsuario);
            return DTArbolModulo;

        }

        #endregion

        #region Usuario Rol

        public override bool registrarRolUsuario(UsuarioRol objRolUsuarioRol)
        {

            wsSeguridad.UsuarioRol objWSUsuarioRol = new wsSeguridad.UsuarioRol();

            objWSUsuarioRol.SCodUsuario = objRolUsuarioRol.SCodUsuario;
            objWSUsuarioRol.SCodRol = objRolUsuarioRol.SCodRol;
            objWSUsuarioRol.SLogUsuarioMod = objRolUsuarioRol.SLogUsuarioMod;
            objWSUsuarioRol.SLogFechaMod = objRolUsuarioRol.SLogFechaMod;
            objWSUsuarioRol.SLogHoraMod = objRolUsuarioRol.SLogHoraMod;

            return objWSSeguridad.registrarUsuarioRol(objWSUsuarioRol);
        }

        public override bool eliminarRolUsuario(string sCodUsuario, string sCodRol)
        {

            return objWSSeguridad.eliminarUsuarioRol(sCodUsuario, sCodRol);
        }

        public override UsuarioRol obtenerUsuarioRolxCodRol(string sCodRol)
        {

            wsSeguridad.UsuarioRol objWSUsuarioRol = null;
            objWSUsuarioRol = objWSSeguridad.obtenerUsuarioRolxCodRol(sCodRol);
            if (objWSUsuarioRol != null)
            {
                return new UsuarioRol(objWSUsuarioRol.SCodUsuario, objWSUsuarioRol.SCodRol,
                    objWSUsuarioRol.SLogUsuarioMod, objWSUsuarioRol.SLogFechaMod, objWSUsuarioRol.SLogHoraMod);
            }
            else return null;
        }


        public override UsuarioRol obtenerRolUsuario(string sCodUsuario, string sCodRol)
        {

            wsSeguridad.UsuarioRol objWSUsuarioRol = null;
            objWSUsuarioRol = objWSSeguridad.obtenerUsuarioRol(sCodUsuario, sCodRol);
            if (objWSUsuarioRol != null)
            {
                return new UsuarioRol(objWSUsuarioRol.SCodUsuario, objWSUsuarioRol.SCodRol,
                    objWSUsuarioRol.SLogUsuarioMod, objWSUsuarioRol.SLogFechaMod, objWSUsuarioRol.SLogHoraMod);
            }
            else return null;
        }

        public override List<UsuarioRol> ListarUsuarioRolxCodUsuario(string sCodUsuario)
        {
            wsSeguridad.UsuarioRol[] lista = objWSSeguridad.ListarUsuarioRolxCodUsuario(sCodUsuario);
            List<UsuarioRol> listaUsuarioRol = new List<UsuarioRol>();
            for (int i = 0; i < lista.Length; i++)
            {
                UsuarioRol objUsuarioRol = new UsuarioRol();

                objUsuarioRol.SCodUsuario = lista[i].SCodUsuario;
                objUsuarioRol.SCodRol = lista[i].SCodRol;
                objUsuarioRol.SLogUsuarioMod = lista[i].SLogUsuarioMod;
                objUsuarioRol.SLogFechaMod = lista[i].SLogFechaMod;
                objUsuarioRol.SLogHoraMod = lista[i].SLogHoraMod;

                listaUsuarioRol.Add(objUsuarioRol);
            }
            return listaUsuarioRol;
        }

        #endregion

        #region Rol


        public override bool registrarRol(Rol objRol)
        {

            wsSeguridad.Rol objWSRol = new wsSeguridad.Rol();

            objWSRol.SCodRol = objRol.SCodRol;
            objWSRol.SNomRol = objRol.SNomRol;
            objWSRol.SCodPadreRol = objRol.SCodPadreRol;
            objWSRol.SLogUsuarioMod = objRol.SLogUsuarioMod;
            return objWSSeguridad.registrarRol(objWSRol);

        }

        public override bool actualizarRol(Rol objRol)
        {

            wsSeguridad.Rol objWSRol = new wsSeguridad.Rol();

            objWSRol.SCodRol = objRol.SCodRol;
            objWSRol.SNomRol = objRol.SNomRol;
            objWSRol.SLogUsuarioMod = objRol.SLogUsuarioMod;
            return objWSSeguridad.actualizarRol(objWSRol);

        }

        public override bool eliminarRol(string sCodRol)
        {

            return objWSSeguridad.eliminarRol(sCodRol);

        }

        public override Rol obtenerRolxnombre(string sNomRol)
        {

            wsSeguridad.Rol objWSRol = null;
            objWSRol = objWSSeguridad.obtenerRolxNombre(sNomRol);
            if (objWSRol != null)
            {
                return new Rol(objWSRol.SCodRol, objWSRol.SCodPadreRol,
                    objWSRol.SNomRol, objWSRol.SLogUsuarioMod, objWSRol.SLogFechaMod,
                    objWSRol.SLogHoraMod);
            }
            else return null;
        }

        public override Rol obtenerRolxcodigo(string sCodRol)
        {

            wsSeguridad.Rol objWSRol = null;
            objWSRol = objWSSeguridad.obtenerRolxCodigo(sCodRol);
            if (objWSRol != null)
            {
                return new Rol(objWSRol.SCodRol, objWSRol.SCodPadreRol,
                    objWSRol.SNomRol, objWSRol.SLogUsuarioMod, objWSRol.SLogFechaMod,
                    objWSRol.SLogHoraMod);
            }
            else return null;
        }

        public override List<Rol> listaDeRoles()
        {
            wsSeguridad.Rol[] lista = objWSSeguridad.listaDeRoles();
            List<Rol> listaRol = new List<Rol>();
            for (int i = 0; i < lista.Length; i++)
            {
                Rol objRol = new Rol();

                objRol.SCodRol = lista[i].SCodRol;
                objRol.SCodPadreRol = lista[i].SCodPadreRol;
                objRol.SNomRol = lista[i].SNomRol;
                objRol.SLogUsuarioMod = lista[i].SLogUsuarioMod;
                objRol.SLogFechaMod = lista[i].SLogFechaMod;
                objRol.SLogHoraMod = lista[i].SLogHoraMod;

                listaRol.Add(objRol);
            }
            return listaRol;

        }
        public override DataTable LLenarArbolRoles(string sCodRol)
        {
            DataTable DTArbolModulo;
            DTArbolModulo = objWSSeguridad.listarArbolModuloxRol(sCodRol);
            return DTArbolModulo;

        }

        public override List<Rol> listarRolesAsignados(string sCodUsuario)
        {

            wsSeguridad.Rol[] lista = objWSSeguridad.listarRolesAsignados(sCodUsuario);
            List<Rol> listaRol = new List<Rol>();
            for (int i = 0; i < lista.Length; i++)
            {
                Rol objRol = new Rol();

                objRol.SCodRol = lista[i].SCodRol;
                objRol.SCodPadreRol = lista[i].SCodPadreRol;
                objRol.SNomRol = lista[i].SNomRol;
                objRol.SLogUsuarioMod = lista[i].SLogUsuarioMod;
                objRol.SLogFechaMod = lista[i].SLogFechaMod;
                objRol.SLogHoraMod = lista[i].SLogHoraMod;

                listaRol.Add(objRol);
            }
            return listaRol;
        }

        public override List<Rol> listarRolesSinAsignar(string sCodUsuario)
        {
            wsSeguridad.Rol[] lista = objWSSeguridad.listarRolesSinAsignar(sCodUsuario);
            List<Rol> listaRol = new List<Rol>();
            for (int i = 0; i < lista.Length; i++)
            {
                Rol objRol = new Rol();

                objRol.SCodRol = lista[i].SCodRol;
                objRol.SCodPadreRol = lista[i].SCodPadreRol;
                objRol.SNomRol = lista[i].SNomRol;
                objRol.SLogUsuarioMod = lista[i].SLogUsuarioMod;
                objRol.SLogFechaMod = lista[i].SLogFechaMod;
                objRol.SLogHoraMod = lista[i].SLogHoraMod;

                listaRol.Add(objRol);
            }
            return listaRol;
        }

        public override bool obtenerRolHijo(string sCodRol)
        {
            return objWSSeguridad.obtenerRolHijo(sCodRol);
        }

        #endregion

        #region Perfil Rol

        public override bool registrarPerfilRol(PerfilRol objPerfilRol)
        {
            wsSeguridad.PerfilRol objWSPerfilRol = new wsSeguridad.PerfilRol();

            objWSPerfilRol.SCodModulo = objPerfilRol.SCodModulo;
            objWSPerfilRol.SCodProceso = objPerfilRol.SCodProceso;
            objWSPerfilRol.SCodRol = objPerfilRol.SCodRol;
            objWSPerfilRol.SLogUsuarioMod = objPerfilRol.SLogUsuarioMod;
            return objWSSeguridad.registrarPerfilRol(objWSPerfilRol);

        }

        public override bool actualizarPerfilRol(PerfilRol objPerfilRol)
        {
            wsSeguridad.PerfilRol objWSPerfilRol = new wsSeguridad.PerfilRol();

            objWSPerfilRol.SCodModulo = objPerfilRol.SCodModulo;
            objWSPerfilRol.SCodProceso = objPerfilRol.SCodProceso;
            objWSPerfilRol.SCodRol = objPerfilRol.SCodRol;
            objWSPerfilRol.SFlgPermitido = objPerfilRol.SFlgPermitido;
            objWSPerfilRol.SLogUsuarioMod = objPerfilRol.SLogUsuarioMod;
            return objWSSeguridad.actualizarPerfilRol(objWSPerfilRol);

        }

        public override List<PerfilRol> ListadoPerfilRolxRol(string sCodRol)
        {
            wsSeguridad.PerfilRol[] lista = objWSSeguridad.ListadoPerfilRolxRol(sCodRol);
            List<PerfilRol> listaPerfilRol = new List<PerfilRol>();
            for (int i = 0; i < lista.Length; i++)
            {
                PerfilRol objPerfilRol = new PerfilRol();

                objPerfilRol.SCodModulo = lista[i].SCodModulo;
                objPerfilRol.SCodProceso = lista[i].SCodProceso;
                objPerfilRol.SCodRol = lista[i].SCodRol;
                objPerfilRol.SFlgPermitido = lista[i].SFlgPermitido;
                objPerfilRol.SLogUsuarioMod = lista[i].SLogUsuarioMod;

                listaPerfilRol.Add(objPerfilRol);
            }
            return listaPerfilRol;

        }

        public override string FlagPerfilRolxOpcion(string sCodUsuario, string sCodRol, string sDscArchivo, string sOpcion)
        {

            return objWSSeguridad.FlagPerfilRolxOpcion(sCodUsuario, sCodRol, sDscArchivo, sOpcion);
        }

        #endregion

        #region Clave Usuario Historico

        public override bool obtenerClaveUsuHist(string sCodUsuario, string SDscClave, int iNum)
        {
            return objWSSeguridad.obtenerClaveUsuHist(sCodUsuario, SDscClave, iNum);
        }


        public override ClaveUsuHist obtenerUsuarioHist(string sCodUsuario)
        {
            wsSeguridad.ClaveUsuHist objWSClaveUsuHist = null;
            objWSClaveUsuHist = objWSSeguridad.obtenerUsuarioHist(sCodUsuario);
            if (objWSClaveUsuHist != null)
            {
                return new ClaveUsuHist(objWSClaveUsuHist.SCodUsuario, objWSClaveUsuHist.SLogFechaMod, objWSClaveUsuHist.SDscClave
                    , objWSClaveUsuHist.SLogUsuarioMod, objWSClaveUsuHist.SLogHoraMod,
                    objWSClaveUsuHist.INumContadorUsuario);
            }
            else return null;

        }
        #endregion

        #region Modulo

        public override DataTable ListarAllModulo()
        {
            return objWSSeguridad.ListarAllModulo();
        }
        #endregion


        public override DataTable ListaRol()
        {
            throw new Exception("The method or operation is not implemented.");
        }


        public override DataTable ListarPerfilRolxRol(string sCodRol)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override List<Moneda> ListarMonedas()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override List<Moneda> ListarMonedasxTipoTicket()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool verificarTurnoCerradoxUsuario(string strUsuario)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool verificarTurnoCerradoxPtoVenta(string strEquipo)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool CrearTurno(string strSec, string strUsuario, string strEquipo)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override Turno ObtenerTurnoIniciado(string strUsuario)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool RegistrarTurnoMonto(List<TurnoMonto> listaMontos)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool ActualizarTurno(Turno objTurno)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool ActualizarTurnoMonto(List<TurnoMonto> listaMontos)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override List<TurnoMonto> ListarTurnoMontosPorTurno(string strTurno)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override List<Moneda> ListarMonedasInter()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override List<Limite> ListarLimitesPorOperacion(string stipoope)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool RegistrarOperacion(LogOperacion objOperacion)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool RegistrarOpeCaja(List<LogOperacCaja> objLista)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override TasaCambio ObtenerTasaCambioPorMoneda(string strMoneda, string strTipo)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool RegistrarCompraVenta(LogCompraVenta objCompraVenta)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override TipoTicket ObtenerPrecioTicket(string strTipoVuelo, string strTipoPas, string strTipoTrans)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override DataTable ConsultarCompaniaxFiltro(string strEstado, string strTipo, string CadFiltro, string sOrdenacion)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override DataTable ListarVuelosxCompania(string strCompania, string strFecha)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool RegistrarTicket(List<Ticket> listaTickets, ref string strListaTickets)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool IsError()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override string GetErrorCode()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override List<RepresentantCia> ListarRepteCia(string strCia)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool RegistrarVentaMasiva(VentaMasiva objVentaMasiva)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool ActualizarVentaMasiva(VentaMasiva objVentaMasiva)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override List<TipoTicket> ListarAllTipoTicket()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override List<ModVentaComp> ListarCompaniaxModVenta(string strCodModVenta, string strTipComp)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override List<ModalidadAtrib> ListarAtributosxModVenta(string strCodModVenta)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override List<ModalidadAtrib> ListarAtributosxModVentaCompania(string strCodModVenta, string strCompania)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override DataTable listarAllMonedas()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override DataTable obtenerDetalleMoneda(string sCodMoneda)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool registrarTipoMoneda(Moneda objTipoMoneda)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool actualizarTipoMoneda(Moneda objTipoMoneda)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool eliminarTipoMoneda(string sCodMoneda)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override DataTable ListaTipoTicket()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool registrarTipoTicket(TipoTicket TipoTicket)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool actualizarTipoTicket(TipoTicket TipoTicket)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override TipoTicket obtenerTipoTicket(string sCodTipoTicket)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override DataTable listarAllPuntoVenta()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override DataTable obtenerDetallePuntoVenta(string sCodEquipo, string strIP)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool registrarPuntoVenta(EstacionPtoVta objPuntoVenta)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool actualizarPuntoVenta(EstacionPtoVta objPuntoVenta)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool eliminarPuntoVenta(string sCodEquipo, string sLogUsuarioMod)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override List<ListaDeCampo> obtenerListadeCampo(string sNomCampo)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool insertarModalidadVenta(ModalidadVenta objModalidadVenta)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool actualizarModalidadVenta(ModalidadVenta objModalidadVenta)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override ModalidadVenta obtenerModalidadVentaxCodigo(string sCodModalidadVenta)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override ModalidadVenta obtenerModalidadVentaxNombre(string sNomModalidad)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override DataTable ListarAllModalidadVenta()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override List<ModalidadVenta> listarModalidadVenta()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override DataTable ListarAllTurno(string as_codturno)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override DataTable CantidadMonedasTurno(string as_codturno)
        {
            throw new Exception("The method or operation is not implemented.");
        }


        public override DataTable ConsultaCompaniaxFiltro(string strEstado, string strTipo, string sfiltro, string sOrdenacion)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override DataTable listarAllCompania()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override DataTable ConsultaUsuarioxFiltro(string as_rol, string as_estado, string as_grupo, string sfiltro, string sOrdenacion)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override DataTable ListarAllUsuario()
        {

            DataTable DTListarAllUsuario;

            DTListarAllUsuario = objWSSeguridad.listarAllUsuario();

            return DTListarAllUsuario;

        }

        public override DataTable ListarUsuarioxRol(string sCod_Rol)
        {
            throw new NotImplementedException();
        }

        public override DataTable ListarParametros(string as_identificador)
        {
            wsConsulta.WSConsultas objWSConsultas = new WSConsultas.WSConsultas();
            return objWSConsultas.ListarParametrosGeneral(as_identificador);
        }


        public override DataTable DetalleBoardingEstHist_Arch(string Num_Secuencial_Bcbp)
        {
            throw new NotImplementedException();
        }

        public override DataTable ListaCamposxNombre(string sNombre)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override DataTable ListarRoles()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override DataTable ListarAllParametroGenerales(string strParametro)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override DataTable DetalleParametroGeneralxId(string sIdentificador)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool RegistrarListaDeCampo(ListaDeCampo objListaCampo, int intTipo)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override DataTable ObtenerListaDeCampo(string strNomCampo, string strCodCampo)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool RegistrarTasaCambio(TasaCambio objTasaCambio)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool EliminarTasaCambio(string strCodTasaCambio)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override DataTable ObtenerTasaCambio(string strCodTasaCambio)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool RegistrarTasaCambioHist(TasaCambioHist objTasaCambioHist)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override DataTable ObtenerTasaCambioHist(string strCodTasaCambio)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool RegistrarPrecioTicket(PrecioTicket objPrecioTicket)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool EliminarPrecioTicket(string strCodPrecioTicket)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override DataTable ObtenerPrecioTicket(string strCodPrecioTicket)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool RegistrarPrecioTicketHist(PrecioTicketHist objPrecioTicketHist)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool EliminarPrecioTicketHist(string strCodPrecioTicket)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override DataTable ObtenerPrecioTicketHist(string strCodPrecioTicket)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override DataTable ConsultaDetalleTicket(string as_numeroticket, string as_ticketdesde, string as_tickerhasta)
        {
            throw new NotImplementedException();
        }



        public override DataTable ConsultaDetalleTicket(string sNumeroTicket)
        {
            throw new NotImplementedException();
        }

        public override DataTable ListarTicketEstHist(string as_numeroticket)
        {
            throw new NotImplementedException();
        }

        public override DataTable ListarTicketEstHist_Arch(string as_numeroticket)
        {
            throw new NotImplementedException();
        }

        public override DataTable DetalleBoardingEstHist(String Num_Secuencial_Bcbp)
        {
            throw new NotImplementedException();
        }
        public override System.Data.DataTable ConsultarRepresXRehabilitacionYCia(String strCia)
        {
            throw new NotImplementedException();
        }

        public override bool registrarRehabilitacionTicket(TicketEstHist objTicketEstHist, int flag, int sizeOutput)
        {
            throw new NotImplementedException();
        }

        public override DataTable consultarVuelosTicketPorCiaFecha(string sCompania, string fechaVuelo)
        {
            throw new NotImplementedException();
        }

        public override DataTable consultarTicketsPorVuelo(string sCompania, string fechaVuelo, string dsc_Num_Vuelo)
        {
            throw new NotImplementedException();
        }

        public override DataTable ListarTurnosAbiertos()
        {
            throw new NotImplementedException();
        }

        public override void IngresarDatosATemporalBoardingPass(string strTrama)
        {
            throw new NotImplementedException();
        }

        public override DataTable DetalleBoarding(string strCodCompania, string strNumVuelo, string strFechVuelo, string strNumAsiento, string strPasajero, string tipEstado, String Cod_Unico_Bcbp, String Num_Secuencial_Bcbp)
        {
            throw new NotImplementedException();
        }

        public override List<ParameGeneral> listarAtributosGenerales()
        {
            throw new NotImplementedException();
        }

        public override bool ActualizarTicket(List<Ticket> listaTickets)
        {
            throw new NotImplementedException();
        }

        public override DataTable ListarTicketxFecha(string as_FchDesde, string as_FchHasta, string as_CodCompania, string as_TipoTicket, string as_EstadoTicket, string as_TipoPersona, string as_filtro, string as_Ordenacion, string as_HoraDesde, string as_HoraHasta, string strTurno)
        {
            throw new NotImplementedException();
        }


        public override DataTable ObtenerDetallexLineaVuelo(string sFechaDesde, string sFechaHasta, string sCodCompania, string sColumnaSort, int iStartRows, int iMaxRows, string sPaginacion, string sFlgTotalRows)
        {
            throw new NotImplementedException();
        }


        public override DataTable obtenerUsuarioxFechaOperacion(string sFechaOperacion, string sCodUsuario, string sTipoOperacion, string sCodMoneda)
        {
            throw new NotImplementedException();
        }

        public override bool ExtornarCompraVenta(string strCodOpera, string strTurno, int intCantidad, ref string strMessage)
        {
            throw new NotImplementedException();
        }

        public override bool insertarModVentaAtributo(ModalidadAtrib objModalidadAtrib)
        {
            throw new NotImplementedException();
        }

        public override bool actualizarModVentaAtributo(ModalidadAtrib objModalidadAtrib)
        {
            throw new NotImplementedException();
        }

        public override bool eliminarModVentaAtributo(string sCodModalidadVenta, string sCodAtributo, string sCodTipoTicket)
        {
            throw new NotImplementedException();
        }

        public override List<ModalidadAtrib> ListarAtributosxModVentaTipoTicket(string strCodModVenta, string strTipoTicket)
        {
            throw new NotImplementedException();
        }

        public override ParameGeneral obtenerParametroGeneral(string sCodParam)
        {
            throw new NotImplementedException();
        }


        public override bool ExtornoRehabilitacion(string strListaTickets, int intCantidad, string strUsuario, string strEstado, bool transaccion)
        {
            throw new NotImplementedException();
        }

        public override DataTable DetalladoCantidadMonedas(string as_codturno, string as_codmoneda, string as_idDetalle)
        {
            throw new NotImplementedException();
        }

        public override bool ExtornarTicket(string strListaTickets, string strTurno, int intCantidad, string strUsuario, string strMotivo, ref string strMessage)
        {
            throw new NotImplementedException();
        }

        public override bool ExtenderVigenciaTicket(string strListaTickets, string strListaFechas, string strUsuario)
        {
            throw new NotImplementedException();
        }

        public override bool insertarCompania(Compania objCompania)
        {
            throw new NotImplementedException();
        }

        public override bool actualizarCompania(Compania objCompania)
        {
            throw new NotImplementedException();
        }

        public override bool insertarRepresentante(RepresentantCia objRepresentante)
        {
            throw new NotImplementedException();
        }

        public override bool actualizarRepresentante(RepresentantCia objRepresentante)
        {
            throw new NotImplementedException();
        }

        public override bool insertarModVentaComp(ModVentaComp objModComp)
        {
            throw new NotImplementedException();
        }

        public override bool eliminarModVentaComp(string sCodCompania, string sCodModalidadVenta)
        {
            throw new NotImplementedException();
        }

        public override List<ModVentaComp> ListarModVentaCompxCompa�ia(string sCodCompania)
        {
            throw new NotImplementedException();
        }

        public override bool insertarModVentaCompAtr(ModVentaCompAtr objRModCompAtr)
        {
            throw new NotImplementedException();
        }

        public override bool actualizarModVentaCompAtr(ModVentaCompAtr objRModCompAtr)
        {
            throw new NotImplementedException();
        }

        public override bool eliminarModVentaCompAtr(string sCodCompania, string sCodModalidadVenta, string CodAtributo)
        {
            throw new NotImplementedException();
        }

        public override List<ModVentaCompAtr> ObtenerModVentaCompAtr(string sCodCompania, string sCodModalidadVenta)
        {
            throw new NotImplementedException();
        }

        public override Compania obtenerCompa�iaxcodigo(string sCodigoCompania)
        {
            throw new NotImplementedException();
        }

        public override Compania obtenerCompa�iaxnombre(string sNombreCompania)
        {
            throw new NotImplementedException();
        }


        public override bool actualizarMolinete(Molinete objMolinete)
        {
            throw new NotImplementedException();
        }



        public override DataTable ListarMolinetes(string strCodMolinete, string strDscIp)
        {
            throw new NotImplementedException();
        }
        public override DataTable consultarVuelosBCBPPorCiaFecha(string sCompania, string fechaVuelo)
        {
            throw new NotImplementedException();
        }

        public override bool registrarRehabilitacionBCBP(BoardingBcbpEstHist boardingBcbpEstHist, int flag, int sizeOutput)
        {
            throw new NotImplementedException();
        }

        public override DataTable listarCompania_xCodigoEspecial(string codigoEspecial)
        {
            throw new NotImplementedException();
        }

        public override DataTable obteneterBoardingsByRangoFechas(string sCompania, string sFchDesde, string sFchHasta, string sHoraDesde, string sHoraHasta)
        {
            throw new NotImplementedException();
        }

        public override bool AnularTicket(string sCodNumeroTicket, string sDscMotivo, string sUsuarioMod)
        {
            throw new NotImplementedException();
        }

        public override bool ActualizarEstadoBCBP(BoardingBcbp objBoardingBcbp)
        {
            throw new NotImplementedException();
        }

        public override bool AnularBCBP(BoardingBcbp objBoardingBcbp)
        {
            throw new NotImplementedException();
        }

        public override bool insertarCnfgAlarma(CnfgAlarma objCnfgAlarma)
        {
            throw new NotImplementedException();
        }

        public override bool actualizarCnfgAlarma(CnfgAlarma objCnfgAlarma)
        {
            throw new NotImplementedException();
        }

        public override bool eliminarCnfgAlarma(string sCodAlarma, string sCodModulo)
        {
            throw new NotImplementedException();
        }

        public override DataTable ListarAllCnfgAlarma()
        {
            throw new NotImplementedException();
        }

        public override CnfgAlarma obtenerCnfgAlarma(string sCodAlarma, string sCodModulo)
        {
            throw new NotImplementedException();
        }

        public override bool insertarAlarmaGenerada(AlarmaGenerada objAlarmaGenerada)
        {
            throw new NotImplementedException();
        }

        public override bool actualizarAlarmaGenerada(AlarmaGenerada objAlarmaGenerada)
        {
            throw new NotImplementedException();
        }

        public override DataTable ListarAllAlarmaGenerada()
        {
            throw new NotImplementedException();
        }

        public override AlarmaGenerada obtenerAlarmaGenerada(string sCodAlarmaGenerada)
        {
            throw new NotImplementedException();
        }

        public override DataTable ObtenerAlarmaxCodModulo(string sCodModulo)
        {
            throw new NotImplementedException();
        }

        public override DataTable ListarAlarmaGeneradaEnviadas()
        {
            throw new NotImplementedException();
        }

        public override DataTable ConsultaAlarmaGenerada(string sFchDesde, string sFchHasta, string sHoraDesde, string sHoraHasta, string sModulo, string sTipoAlarma, string sEstado)
        {
            throw new NotImplementedException();
        }

        public override bool EliminarListaDeCampo(string strNomCampo, string strCodCampo)
        {
            throw new NotImplementedException();
        }

        public override bool RegistrarTicket(string strCompania, string strVentaMasiva, string strNumVuelo, string strFecVuelo, string strTurno, string strUsuario, decimal decPrecio, string strMoneda, string strModVenta, string strTipTicket, string strTipVuelo, int intTickets, string strFlagCont, string strNumRef, string strTipPago, string strEmpresa, string strRepte, ref string strFecVence, ref string strListaTickets, string strCodTurnoIng, string strFlgCierreTurno, string strCodPrecio)
        {
            throw new NotImplementedException();
        }

        public override DataTable obtenerMovimientoTicketContingencia(string sFchDesde, string sFchHasta, string sEstado, string sTipoTicket, string sEstadoTicket, string sRangoMinTicket, string sRangoMaxTicket)
        {
            throw new NotImplementedException();
        }

        public override DataTable consultarTicketBoardingUsados(string sCodCompania, string sNumVuelo, string sTipoDocumento, string sTipoTicket, string sTipoFiltro, string sFechaInicial, string sFechaFinal, string sTimeInicial, string sTimeFinal, string sDestino, string sColumnaSort, int iStartRows, int iMaxRows, string sPaginacion, string sMostrarResumen, string sFlgTotalRows)
        {
            throw new NotImplementedException();
        }

        public override DataTable ObtenerVentaTicket(string strFecIni, string strFecFin, string strTipVenta, string strFlgAero)
        {
            throw new NotImplementedException();
        }

        public override DataTable ObtenerComprobanteSEAE(string sAnio, string sMes, string sTDocumento, string strTipVenta, string strFlgAero)
        {
            throw new NotImplementedException();
        }

        public override bool GrabarParametroGeneral(string sValoresFormulario, string sValoresGrilla, string sParametroVenta)
        {
            throw new NotImplementedException();
        }

        public override DataTable obtenerResumenTicketVendidosCredito(string sFechaInicial, string sFechaFinal, string sTipoTicket, string sNumVuelo, string sAeroLinea, string sCodPago)
        {
            throw new NotImplementedException();
        }

        public override DataTable obtenerRecaudacionMensual(string anio)
        {
            throw new NotImplementedException();
        }



        public override DataTable BoardingLeidosMolinete(string strCodCompania, string strFechVuelo, string strNum_Vuelo, string strFechaLecturaIni, string strFechaLecturaFin, string strCodEstado, string strNumBoarding, string strFlagResumen)
        {
            throw new NotImplementedException();
        }

        public override DataTable obtenerCuadreTicketEmitidos(string as_FchDesde, string as_FchHasta, string as_TipoDocumento, string as_FlagAnulado)
        {
            throw new NotImplementedException();
        }

        public override DataTable obtenerTicketsAnulados(string as_FchDesde, string as_FchHasta)
        {
            throw new NotImplementedException();
        }

        public override DataTable ConsultarTicketVencidos(string strFecIni, string strFecFin, string strTipoTicket)
        {
            throw new NotImplementedException();
        }

        public override DataTable obtenerDetalleVentaCompania(string sFchDesde, string sFchHasta, string sHoraDesde, string sHoraHasta)
        {
            throw new NotImplementedException();
        }

        public override DataTable ConsultarLiquidVenta(string strFecIni, string strFecFin)
        {
            throw new NotImplementedException();
        }

        public override DataTable ConsultarUsoContingencia(string strFecIni, string strFecFin)
        {
            throw new NotImplementedException();
        }

        public override DataTable ConsultarUsoContingenciaUsado(string strFecIni, string strFecFin)
        {
            throw new NotImplementedException();
        }

        public override bool RegistrarVentaContingencia(string strCompania, string strNumVuelo, string strUsuario, string strMoneda, string strTipTicket, string strFecVenta, int intTickets, string strListaTickets, string strCodTurno, string strFlagTurno, ref string strCodTurnoCreado, ref string strCodError)
        {
            throw new NotImplementedException();
        }

        public override DataTable FiltrosAuditoria(string strCodModulo, string strFlgSubModulo, string strTablaXml)
        {
            throw new NotImplementedException();
        }

        public override DataTable ObtenerConsultaAuditoria(string strTipOperacion, string strTabla, string strCodModulo, string strCodSubModulo, string strCodUsuario, string strFchDesde, string strFchHasta, string strHraDesde, string strHraHasta)
        {
            throw new NotImplementedException();
        }

        public override DataTable ListarContingencia(string strTipTikcet, string strFlgConti, string strNumIni, string strNumFin, string strUsuario)
        {
            throw new NotImplementedException();
        }

        public override DataTable obtenerLiquidacionVenta(string sFchDesde, string sFchHasta)
        {
            throw new NotImplementedException();
        }

        public override DataTable obtenerLiquidacionVentaResumen(string sFchDesde, string sFchHasta)
        {
            throw new NotImplementedException();
        }

        public override DataTable obtenerTicketBoardingRehabilitados(string sFchDesde, string sFchHasta, string sHoraDesde, string sHoraHasta, string sDocumento, string sTicket, string sAerolinea, string sVuelo, string sMotivo)
        {
            throw new NotImplementedException();
        }

        public override DataTable obtenerBoardingRehabilitados(string sFchDesde, string sFchHasta, string sHoraDesde, string sHoraHasta, string sCompania, string sMotivo, string sTipoVuelo, string sTipoPersona, string sNumVuelo, string sColumnaSort, int iStartRows, int iMaxRows, string sPaginacion, string sMostrarResumen, string sFlgTotalRows)
        {
            throw new NotImplementedException();
        }

        public override DataTable consultarBoardingPassDiario(string sFechaDesde, string sFechaHasta, string sHoraDesde, string sHoraHasta, string sCodCompania, string sTipoPasajero, string sTipoVuelo, string sTipoTrasbordo, string sFechaVuelo, string sNumVuelo, string sPasajero, string sNumAsiento, string sCodIata, string sTipReporte, string sColumnSort, int iIniRows, int iMaxRows, string sTotalRows)
        {
            throw new NotImplementedException();
        }

        public override DataTable ObtenerDetalleAuditoria(string strNombreTabla, string strContador)
        {
            throw new NotImplementedException();
        }

        public override bool insertarSecuenciaModVentaComp(string codModalidad)
        {
            throw new NotImplementedException();
        }

        public override int validarSerieTicket(int serieInicial, int serieFinal, string modalidad)
        {
            throw new NotImplementedException();
        }


        public override int validarSerieTicketCompa(int serieInicial, int serieFinal, string modalidad, string compania)
        {
            throw new NotImplementedException();
        }


        public override DataTable ConsultaTurnoxFiltro2(string strFchIni, string strFchFin, string strCodUsuario, string strCodTurno)
        {
            throw new NotImplementedException();
        }


        public override DataTable ListarTicketProcesado(string strCodTurno)
        {
            throw new NotImplementedException();
        }

        public override DataTable ListarResumenCompraVenta(string strTipo, string strFecIni, string strHorIni, string strFecFin, string strHorFin, string strTurnoIni, string strTurnoFin)
        {
            throw new NotImplementedException();
        }

        public override DataTable ListarTicketVendido(string strTipo, string strFecIni, string strHorIni, string strFecFin, string strHorFin, string strTurnoIni, string strTurnoFin)
        {
            throw new NotImplementedException();
        }

        public override DataTable ListarResumenTasaCambio(string strTipo, string strFecIni, string strHorIni, string strFecFin, string strHorFin, string strTurnoIni, string strTurnoFin)
        {
            throw new NotImplementedException();
        }
        public override DataTable ListarTicketxFecha_Reh(string as_FchDesde, string as_FchHasta, string as_CodCompania, string as_TipoTicket, string as_EstadoTicket, string as_TipoPersona, string as_filtro, string as_Ordenacion, string as_HoraDesde, string as_HoraHasta, string strTurno)
        {
            throw new NotImplementedException();
        }
        public override DataTable ConsultaDetalleTicket_Reh(string as_numeroticket, string as_ticketdesde, string as_tickerhasta)
        {
            throw new NotImplementedException();
        }

        public override bool InsertarAuditoria(string strCodModulo, string strCodSubModulo, string strCodUsuario, string strTipOperacion)
        {
            throw new NotImplementedException();
        }

        public override bool AnularTuua(string strListaTicket, int intTicket)
        {
            throw new NotImplementedException();
        }

        public override DataTable obtenerTicketBoardingUsados(string as_FechaDesde, string as_FechaHasta, string as_HoraDesde, string as_HoraHasta, string as_CodCompania, string as_TipoVuelo, string as_NumVuelo, string as_TipoPasajero, string as_TipoDocumento, string as_TipoTrasbordo, string as_FechaVuelo, string as_Estado)
        {
            throw new NotImplementedException();
        }

        public override int validarDocumento(string sNombre, string sApellido, string sTpDocumento, string sNroDocumento)
        {
            throw new NotImplementedException();
        }

        public override int validarAnulacionModalidad(string sModalidad, string sCompania)
        {
            throw new NotImplementedException();
        }

        public override DataTable ConsultaTurnoxFiltro(string as_fchIni, string as_fchFin, string as_codusuario, string as_ptoventa, string as_horadesde, string as_horahasta, string as_FlgReporte)
        {
            throw new NotImplementedException();
        }

        public override DataTable ObtenerEstadistico()
        {
            throw new NotImplementedException();
        }
        public override DataTable ListaCamposxNombreOrderByDesc(string sNombre)
        {
            throw new NotImplementedException();
        }
        public override DataTable DetalleBoarding_REH(string strCodCompania, string strNumVuelo, string strFechVuelo, string strNumAsiento, string strPasajero, string tipEstado, String Cod_Unico_Bcbp, String Num_Secuencial_Bcbp, string Flag_Fch_Vuelo, string Check_Seq_Number)
        {
            throw new NotImplementedException();
        }

        public override DataTable ConsultaDetalleTicket_Ope(string sNumTicket, string sTicketDesde, string sTicketHasta, string stipoticket, string sTickets_Sel, string sColumnaSort, int iStartRows, int iMaxRows, string sPaginacion, string sFlgTotalRows)
        {
            throw new NotImplementedException();
        }

        public override TipoTicket validarTipoTicket(string strTipoVuelo, string strTipoPas, string strTipoTrans)
        {
            throw new NotImplementedException();
        }

        public override DataTable ConsultaDetalleTicketPagin(string as_numeroticket, string as_ticketdesde, string as_tickerhasta, string as_ColumnSort, int i_IniRows, int i_MaxRows, string as_TotalRows)
        {
            throw new NotImplementedException();
        }

        public override DataTable DetalleBoardingPagin(string strCodCompania, string strNumVuelo, string strFechVuelo, string strNumAsiento, string strPasajero, string tipEstado, string Cod_Unico_Bcbp, string Num_Secuencial_Bcbp, string sColumnSort, int iIniRows, int iMaxRows, string sTotalRows)
        {
            throw new NotImplementedException();
        }

        public override DataTable ObtenerConsultaAuditoriaPagin(string strTipOperacion, string strTabla, string strCodModulo, string strCodSubModulo, string strCodUsuario, string strFchDesde, string strFchHasta, string strHraDesde, string strHraHasta, string sColumnSort, int iIniRows, int iMaxRows, string sTotalRows)
        {
            throw new NotImplementedException();
        }

        public override string ObtenerFechaActual()
        {
            throw new NotImplementedException();
        }

        public override string obtenerFechaEstadistico(string sEstadistico)
        {
            throw new NotImplementedException();
        }

        public override DataTable ConsultaDetalleTicket2_Reh(string as_numeroticket, string as_ticketdesde, string as_tickerhasta, string as_flgtotal)
        {
            throw new NotImplementedException();
        }



        public override DataTable ListarTicketxFechaPagin(string as_TipDoc, string as_FchDesde, string as_FchHasta, string as_HoraDesde, string as_HoraHasta, string as_CodCompania, string as_EstadoTicket, string as_TipoTicket, string as_TipoPersona, string as_TipoVuelo, string as_CodBoarding, string as_Turno, string as_FlgCobro, string as_FlgMasiva, string as_EstadoTurno, string as_Cajero, string as_MedioAnulacion, string sColumnSort, int iIniRows, int iMaxRows, string sTotalRows)
        {
            throw new NotImplementedException();
        }

        public override DataTable DetalleBoardingArchivado(string Num_Secuencial_Bcbp)
        {
            throw new NotImplementedException();
        }

        public override DataTable ListarBoardingAsociados(string Num_Secuencial_Bcbp)
        {
            throw new NotImplementedException();
        }

        public override bool registrarRehabilitacionBCBPAmpliacion(BoardingBcbpEstHist boardingBcbpEstHist, int flag, int sizeOutput)
        {
            throw new NotImplementedException();
        }

        public override string validarAsocBCBP(string sNumAsiento, string sNumVuelo, string sFchVuelo, string sNomPersona, string sCompania, string sCodBcbpBase)
        {
            throw new NotImplementedException();
        }

        public override DataTable obtenerDetalleWebBCBP(string sCodCompania, string sNroVuelo, string sFchVuelo, string sAsiento, string sPasajero)
        {
            throw new NotImplementedException();
        }

        public override DataTable ListarTicketBoardingUsadosPagin(string as_FechaDesde, string as_FechaHasta, string as_HoraDesde, string as_HoraHasta, string as_CodCompania, string as_TipoVuelo, string as_NumVuelo, string as_TipoPasajero, string as_TipoDocumento, string as_TipoTrasbordo, string as_FechaVuelo, string as_Estado, string sColumnSort, int iIniRows, int iMaxRows, string sTotalRows)
        {
            throw new NotImplementedException();
        }

        public override DataTable ListarTicketBoardingUsadosResumen(string as_FechaDesde, string as_FechaHasta, string as_HoraDesde, string as_HoraHasta, string as_CodCompania, string as_TipoVuelo, string as_NumVuelo, string as_TipoPasajero, string as_TipoDocumento, string as_TipoTrasbordo, string as_FechaVuelo, string as_Estado)
        {
            throw new NotImplementedException();
        }

        public override DataTable ListarResumenCompaniaPagin(string as_FechaDesde, string as_FechaHasta, string as_HoraDesde, string as_HoraHasta, string sColumnSort, int iIniRows, int iMaxRows, string sTotalRows)
        {
            throw new NotImplementedException();
        }

        public override DataTable ListarTicketVencidosPagin(string strFecIni, string strFecFin, string strTipoTicket, string sColumnSort, int iIniRows, int iMaxRows, string sTotalRows)
        {
            throw new NotImplementedException();
        }

        public override DataTable obtenerResumenStockTicketContingencia(string sTipoTicket, string sFchAl, string sTipoResumen)
        {
            throw new NotImplementedException();
        }

        public override DataTable ListarBcbpxConciliar(string sCodCompania, string strFchVuelo, string strNumVuelo, string strNumAsiento, string strPasajero, string strFecUsoIni, string strFecUsoFin, string strFlg)
        {
            throw new NotImplementedException();
        }

        public override bool RegistrarBcbpxConciliar(string sBcbpBase, string sBcbpUlt, string sBcbpAsoc)
        {
            throw new NotImplementedException();
        }

        public override DataTable ListarBoardingLeidosMolinetePagin(string strCodCompania, string strFechVuelo, string strNum_Vuelo, string strFechaLecturaIni, string strFechaLecturaFin, string strCodEstado, string strNumBoarding, string strFlagResumen, string sColumnSort, int iIniRows, int iMaxRows, string sTotalRows)
        {
            throw new NotImplementedException();
        }

        public override DataTable ListarTicketBoardingUsadosDiaMesPagin(string sFchDesde, string sFchHasta, string sFchMes, string sTipoDocumento, string sCodCompania, string sNumVuelo, string sTipTicket, string sDestino, string sTipReporte, string sColumnSort, int iIniRows, int iMaxRows, string sTotalRows)
        {
            throw new NotImplementedException();
        }

        public override DataTable ListarResumenTicketVendidosCreditoPagin(string sFechaInicial, string sFechaFinal, string sTipoTicket, string sNumVuelo, string sAeroLinea, string sCodPago, string sFlagResumen, string sColumnSort, int iIniRows, int iMaxRows, string sTotalRows)
        {
            throw new NotImplementedException();
        }

        public override DataTable ListarLogErroresMolinete(string sFechaDesde, string sFechaHasta, string sHoraDesde, string sHoraHasta, string sIDError, string sTipoError, string sCompania, string sCodMolinete, string sTipoBoarding, string sTipIngreso, string sFchVuelo, string sNumVuelo, string sNumAsiento, string sNomPasajero, string sColumnaSort, int iStartRows, int iMaxRows, string sPaginacion, string sMostrarResumen, string sFlgTotalRows)
        {
            throw new NotImplementedException();
        }

        public override DataTable ListarTicketsExtorno(string as_Cod_Ticket, string as_Ticket_Desde, string as_Ticket_Hasta, string as_Cod_Turno, string as_ColumnaSort, int as_iStartRows, int as_iMaxRows, string as_Paginacion, string as_FlgTotalRows)
        {
            throw new NotImplementedException();
        }
        public override DataTable obtenerBoardingUsados(string sFechaDesde, string sFechaHasta, string sHoraDesde, string sHoraHasta, string sBoardingSel, string sCodCompania, string sNumVuelo, string sNumAsiento, string sColumnaSort, int iStartRows, int iMaxRows, string sPaginacion, string sFlgTotal)
        {
            throw new NotImplementedException();
        }

        public override DataTable ListarParametrosDefaultValue(string as_identificador)
        {
            throw new NotImplementedException();
        }

        public override bool ObtenerDetalleTurnoActual(string strCodUsuario, ref string strCantTickets, ref string strCodTurno, ref string strFecHorTurno)
        {
            throw new NotImplementedException();
        }

        public override DataTable ListarCuadreTurno(string strMoneda, string strTurno, ref decimal decEfectivoIni, ref int intTicketInt, ref int intTicketNac, ref decimal decTicketInt, ref decimal decTicketNac, ref int intIngCaja, ref decimal decIngCaja, ref int intVentaMoneda, ref decimal decVentaMoneda, ref int intEgreCaja, ref decimal decEgreCaja, ref int intCompraMoneda, ref decimal decCompraMoneda, ref decimal decEfectivoFinal, ref int intAnulaInt, ref int intAnulaNac, ref int intInfanteInt, ref int intInfanteNac, ref int intCreditoInt, ref int intCreditoNac, ref decimal decCreditoInt, ref decimal decCreditoNac)
        {
            throw new NotImplementedException();
        }

        public override DataTable ListarTasaCambio()
        {
            throw new NotImplementedException();
        }

        public override DataTable ListarSincronizacion()
        {
            throw new NotImplementedException();
        }





        public override DataTable ListarFiltroSincronizacion(string as_molinete, string as_estado, string as_TipoSincronizacion, string as_TablaSincronizacion, string strFchDesde, string strFchHasta, string strHraDesde, string strHraHasta,  string sfiltro, string sOrdenacion)
        {
            throw new NotImplementedException();
        }

        public override bool actualizarHoras(ListaDeCampo objListaCampo)
        {
            throw new NotImplementedException();
        }

        public override bool actualizarestado(Sincronizacion objListaSincronizacion)
        {
            throw new NotImplementedException();
        }

        public override DataTable ListarFiltroDepuracion(string as_molinete, string as_estado, string as_TablaSincronizacion, string strFchDesde, string strFchHasta, string strHraDesde, string strHraHasta, string sfiltro, string sOrdenacion)
        {
            throw new NotImplementedException();
        }

        public override DataTable ListarAllMolinetes()
        {
            throw new NotImplementedException();
        }

        public override DataTable obtenerMolinete(string strMolinete)
        {
            throw new NotImplementedException();
        }

        public override bool actualizarUnMolinete(Molinete objMolinete)
        {
            throw new NotImplementedException();
        }

        public override DataTable obtenerTicketsTablaTemporal()
        {
            throw new NotImplementedException();
        }
    }
}
