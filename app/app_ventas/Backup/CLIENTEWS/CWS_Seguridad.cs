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


        #endregion

        #region Arbol Modulo


        public override ArbolModulo ObtenerArbolModulo(string sCodProceso, string sCodModulo, string sCodRol)
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
                    objWSArbolModulo.SDscLicencia, objWSArbolModulo.INumPosProceso, objWSArbolModulo.SDscModulo, objWSArbolModulo.SNomRol, objWSArbolModulo.INumPosModulo,"");
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

        #endregion

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

        public override string FlagPerfilRolxOpcion(string sCodUsuario, string sCodRol, string sDscArchivo, string sOpcion)
        {

            return objWSSeguridad.FlagPerfilRolxOpcion(sCodUsuario, sCodRol, sDscArchivo, sOpcion);
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


        public override bool actualizarContraseñaUsuario(string sCodUsuario, string sContraseña, string SLogUsuarioMod, DateTime dtFchVigencia)
        {
            return objWSSeguridad.actualizarContraseñaUsuario(sCodUsuario, sContraseña, SLogUsuarioMod, dtFchVigencia);

        }

        public override bool actualizarEstadoUsuario(string sCodUsuario, string sEstado, string SLogUsuarioMod, string strFlgCambioClave)
        {
            return objWSSeguridad.actualizarEstadoUsuario(sCodUsuario, sEstado, SLogUsuarioMod, strFlgCambioClave);

        }

        public override DataTable LLenarMenu(string sCodUsuario)
        {
            DataTable DTArbolModulo;
            DTArbolModulo = objWSSeguridad.listarArbolModulo(sCodUsuario);
            return DTArbolModulo;

        }

        public override DataTable ListaRol()
        {
            throw new Exception("The method or operation is not implemented.");
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

        public override DataTable obtenerFecha()
        {
            return objWSSeguridad.obtenerFecha();
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

        public override DataTable ListarPerfilRolxRol(string sCodRol)
        {
            throw new Exception("The method or operation is not implemented.");
        }



        public override bool obtenerClaveUsuHist(string sCodUsuario, string SDscClave, int iNum)
        {
            return objWSSeguridad.obtenerClaveUsuHist(sCodUsuario, SDscClave, iNum);
        }

        public override List<Moneda> ListarMonedas()
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

        public override bool CrearTurno(string strSec, string strUsuario, string strEquipo, ref string strTurnoError)
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

        public override DataTable ListarVuelosxCompania(string strCompania, string strFecha, string strTipVuelo)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool RegistrarTicket(List<Ticket> listaTickets)
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

        public override DataTable DetalladoCantidadMonedas(string as_codturno, string as_codmoneda)
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

        public override DataTable ListarParametros(string as_identificador)
        {
            wsConsulta.WSConsultas objWSConsultas = new WSConsultas.WSConsultas();
            return objWSConsultas.ListarParametrosGeneral(as_identificador);
        }

        public override System.Data.DataTable ListarAllParametroGenerales(string strParam)
        {
            WSConfiguracion.WSConfiguracion objWSConfiguracion = new WSConfiguracion.WSConfiguracion();
            return objWSConfiguracion.ListarAllParametrosGenerales(strParam);
        }

        public override DataTable ConsultaDetalleTicket(string as_numeroticket)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override DataTable ListarTicketxFecha(string as_FchDesde, string as_FchHasta, string as_CodCompania, string as_TipoTicket, string as_EstadoTicket, string as_TipoPersona, string sfiltro, string sOrdenacion)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override DataTable ListaCamposxNombre(string sNombre)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override DataTable ListarRoles()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override DataTable DetalleParametroGeneralxId(string sIdentificador)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool GrabarParametroGeneral(string sValoresFormulario, string sValoresGrilla)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override DataTable ConsultaTurnoxFiltro(string as_fchIni, string as_fchFin, string as_codusuario, string as_ptoventa, string as_horadesde, string as_horahasta)
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

        public override bool RegistrarTicket(string strCompania, string strVentaMasiva, string strNumVuelo, string strFecVuelo, string strTurno, string strUsuario, decimal decPrecio, string strMoneda, string strModVenta, string strTipTicket, string strTipVuelo, int intTickets, string strFlagCont, string strNumRef, string strTipPago, string strEmpresa, string strRepte, ref string strFecVence, ref string strListaTickets, string strCodTurnoIng, string strFlgCierreTurno, string strCodPrecio)
        {
            throw new NotImplementedException();
        }

        public override bool AnularTuua(string strListaTicket, int intTicket)
        {
            throw new NotImplementedException();
        }

        public override DataTable ListarCuadreTurno(string strMoneda, string strTurno, ref decimal decEfectivoIni, ref int intTicketInt, ref int intTicketNac, ref decimal decTicketInt, ref decimal decTicketNac, ref int intIngCaja, ref decimal decIngCaja, ref int intVentaMoneda, ref decimal decVentaMoneda, ref int intEgreCaja, ref decimal decEgreCaja, ref int intCompraMoneda, ref decimal decCompraMoneda, ref decimal decEfectivoFinal, ref int intAnulaInt, ref int intAnulaNac, ref int intInfanteInt, ref int intInfanteNac, ref int intCreditoInt, ref int intCreditoNac, ref decimal decCreditoInt, ref decimal decCreditoNac)
        {
            throw new NotImplementedException();
        }

        public override bool InsertarAuditoria(string strCodModulo, string strCodSubModulo, string strCodUsuario, string strTipOperacion)
        {
            throw new NotImplementedException();
        }

        public override DataTable ListarTasaCambio()
        {
            throw new NotImplementedException();
        }
    }
}
