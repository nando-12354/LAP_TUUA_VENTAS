using System;
using System.Collections.Generic;
using System.Text;
using LAP.TUUA.ENTIDADES;
using LAP.TUUA.CONEXION;
using wsAdministracion = LAP.TUUA.CLIENTEWS.WSAdministracion;
using System.Data;

namespace LAP.TUUA.CLIENTEWS
{
    public class CWS_Administracion : CWS_Conexion
    {
        protected wsAdministracion.WSAdministracion objWSAdministracion;

        public CWS_Administracion()
        {
            objWSAdministracion = new wsAdministracion.WSAdministracion();
        }

        #region Moneda

        public override DataTable listarAllMonedas()
        {
            DataTable DTListaAllMonedas;

            DTListaAllMonedas = objWSAdministracion.listarAllMonedas();

            return DTListaAllMonedas;

        }



        public override DataTable obtenerDetalleMoneda(string sCodMoneda)
        {
            DataTable DTDetalleMoneda;

            DTDetalleMoneda = objWSAdministracion.obtenerDetalleMoneda(sCodMoneda);

            return DTDetalleMoneda;

        }


        public override bool registrarTipoMoneda(Moneda objTipoMoneda)
        {
            wsAdministracion.Moneda objWSTipoMoneda = new wsAdministracion.Moneda();

            objWSTipoMoneda.SCodMoneda = objTipoMoneda.SCodMoneda;
            objWSTipoMoneda.SDscMoneda = objTipoMoneda.SDscMoneda;
            objWSTipoMoneda.SDscSimbolo = objTipoMoneda.SDscSimbolo;
            objWSTipoMoneda.STipEstado = objTipoMoneda.STipEstado;
            objWSTipoMoneda.SLogUsuarioMod = objTipoMoneda.SLogUsuarioMod;
            objWSTipoMoneda.DtLogFechaMod = objTipoMoneda.DtLogFechaMod;
            objWSTipoMoneda.SLogHoraMod = objTipoMoneda.SLogHoraMod;

            return objWSAdministracion.registrarTipoMoneda(objWSTipoMoneda);
        }

        public override bool actualizarTipoMoneda(Moneda objTipoMoneda)
        {
            wsAdministracion.Moneda objWSTipoMoneda = new wsAdministracion.Moneda();

            objWSTipoMoneda.SCodMoneda = objTipoMoneda.SCodMoneda;
            objWSTipoMoneda.SDscMoneda = objTipoMoneda.SDscMoneda;
            objWSTipoMoneda.SDscSimbolo = objTipoMoneda.SDscSimbolo;
            objWSTipoMoneda.STipEstado = objTipoMoneda.STipEstado;
            objWSTipoMoneda.SLogUsuarioMod = objTipoMoneda.SLogUsuarioMod;
            objWSTipoMoneda.DtLogFechaMod = objTipoMoneda.DtLogFechaMod;
            objWSTipoMoneda.SLogHoraMod = objTipoMoneda.SLogHoraMod;

            return objWSAdministracion.actualizarTipoMoneda(objWSTipoMoneda);
        }



        public override bool eliminarTipoMoneda(string sCodMoneda)
        {
            wsAdministracion.Moneda objWSTipoMoneda = new wsAdministracion.Moneda();
            return objWSAdministracion.eliminarTipoMoneda(sCodMoneda);
        }

        #endregion

        #region Tipo Ticket

        public override DataTable ListaTipoTicket()
        {

            DataTable DTTipoTicket;

            DTTipoTicket = objWSAdministracion.ListarTipoTicket();
            return DTTipoTicket;
        }

        public override bool registrarTipoTicket(TipoTicket objTipoTicket)
        {
            wsAdministracion.TipoTicket objWSTipoTicket = new wsAdministracion.TipoTicket();

            objWSTipoTicket.SCodTipoTicket = objTipoTicket.SCodTipoTicket;
            objWSTipoTicket.SNomTipoTicket = objTipoTicket.SNomTipoTicket;
            objWSTipoTicket.STipVuelo = objTipoTicket.STipVuelo;
            objWSTipoTicket.STipPasajero = objTipoTicket.STipPasajero;
            objWSTipoTicket.STipTrasbordo = objTipoTicket.STipTrasbordo;
            objWSTipoTicket.STipEstado = objTipoTicket.STipEstado;
            objWSTipoTicket.SCodMoneda = objTipoTicket.SCodMoneda;
            objWSTipoTicket.DImpPrecio = objTipoTicket.DImpPrecio;
            objWSTipoTicket.SLogUsuarioMod = objTipoTicket.SLogUsuarioMod;
            objWSTipoTicket.SLogHoraMod = objTipoTicket.SLogHoraMod;
            objWSTipoTicket.SLogFechaMod = objTipoTicket.SLogFechaMod;

            return objWSAdministracion.registrarTipoTicket(objWSTipoTicket);

        }

        public override bool actualizarTipoTicket(TipoTicket objTipoTicket)
        {

            wsAdministracion.TipoTicket objWSTipoTicket = new wsAdministracion.TipoTicket();

            objWSTipoTicket.SCodTipoTicket = objTipoTicket.SCodTipoTicket;
            objWSTipoTicket.SNomTipoTicket = objTipoTicket.SNomTipoTicket;
            objWSTipoTicket.STipVuelo = objTipoTicket.STipVuelo;
            objWSTipoTicket.STipPasajero = objTipoTicket.STipPasajero;
            objWSTipoTicket.STipTrasbordo = objTipoTicket.STipTrasbordo;
            objWSTipoTicket.STipEstado = objTipoTicket.STipEstado;
            objWSTipoTicket.SCodMoneda = objTipoTicket.SCodMoneda;
            objWSTipoTicket.DImpPrecio = objTipoTicket.DImpPrecio;
            objWSTipoTicket.SLogUsuarioMod = objTipoTicket.SLogUsuarioMod;
            objWSTipoTicket.SLogHoraMod = objTipoTicket.SLogHoraMod;
            objWSTipoTicket.SLogFechaMod = objTipoTicket.SLogFechaMod;
            return objWSAdministracion.actualizarTipoTicket(objWSTipoTicket);
        }

        public override TipoTicket obtenerTipoTicket(string sCodTipoTicket)
        {
            wsAdministracion.TipoTicket objWSTipoTicket = null;
            objWSTipoTicket = objWSAdministracion.obtenerTipoTicket(sCodTipoTicket);
            if (objWSTipoTicket != null)
            {
                return new TipoTicket(objWSTipoTicket.SCodTipoTicket, objWSTipoTicket.SNomTipoTicket,
                    objWSTipoTicket.STipVuelo, objWSTipoTicket.DImpPrecio, objWSTipoTicket.STipEstado,
                    objWSTipoTicket.SCodMoneda, objWSTipoTicket.STipPasajero, objWSTipoTicket.STipTrasbordo,
                    objWSTipoTicket.SLogFechaMod, objWSTipoTicket.SLogUsuarioMod,
                    objWSTipoTicket.SLogHoraMod);
            }
            else return null;
        }

        #endregion


        #region Punto de Venta

        public override DataTable listarAllPuntoVenta()
        {
            DataTable DTListaAllPuntoVenta;

            DTListaAllPuntoVenta = objWSAdministracion.listarAllPuntoVenta();

            return DTListaAllPuntoVenta;

        }

        public override DataTable obtenerDetallePuntoVenta(string sCodEquipo,string strIP)
        {
            DataTable DTDetallePuntoVenta;

            DTDetallePuntoVenta = objWSAdministracion.obtenerDetallePuntoVenta(sCodEquipo, strIP);


            return DTDetallePuntoVenta;

        }


        public override bool registrarPuntoVenta(EstacionPtoVta objPuntoVenta)
        {
            wsAdministracion.EstacionPtoVta objWSPuntoVenta = new wsAdministracion.EstacionPtoVta();

            objWSPuntoVenta.SCodEquipo = objPuntoVenta.SCodEquipo;
            objWSPuntoVenta.SNumIpEquipo = objPuntoVenta.SNumIpEquipo;
            objWSPuntoVenta.SFlgEstado = objPuntoVenta.SFlgEstado;
            objWSPuntoVenta.SCodUsuarioCreacion = objPuntoVenta.SCodUsuarioCreacion;
            objWSPuntoVenta.SLogUsuarioMod = objPuntoVenta.SLogUsuarioMod;
            objWSPuntoVenta.DtLogFechaMod = objPuntoVenta.DtLogFechaMod;
            objWSPuntoVenta.SLogHoraMod = objPuntoVenta.SLogHoraMod;

            return objWSAdministracion.registrarPuntoVenta(objWSPuntoVenta);
        }



        public override bool actualizarPuntoVenta(EstacionPtoVta objPuntoVenta)
        {
            wsAdministracion.EstacionPtoVta objWSPuntoVenta = new wsAdministracion.EstacionPtoVta();

            objWSPuntoVenta.SCodEquipo = objPuntoVenta.SCodEquipo;
            objWSPuntoVenta.SNumIpEquipo = objPuntoVenta.SNumIpEquipo;
            objWSPuntoVenta.SFlgEstado = objPuntoVenta.SFlgEstado;
            objWSPuntoVenta.SCodUsuarioCreacion = objPuntoVenta.SCodUsuarioCreacion;
            objWSPuntoVenta.SLogUsuarioMod = objPuntoVenta.SLogUsuarioMod;
            objWSPuntoVenta.DtLogFechaMod = objPuntoVenta.DtLogFechaMod;
            objWSPuntoVenta.SLogHoraMod = objPuntoVenta.SLogHoraMod;

            return objWSAdministracion.actualizarPuntoVenta(objWSPuntoVenta);
        }



        public override bool eliminarPuntoVenta(string sCodEquipo, string sLogUsuarioMod)
        {
            return objWSAdministracion.eliminarPuntoVenta(sCodEquipo, sLogUsuarioMod);
        }

        #endregion

        #region Lista de Campo

        public override List<ListaDeCampo> obtenerListadeCampo(string sNomCampo)
        {
            wsAdministracion.ListaDeCampo[] lista = objWSAdministracion.obtenerListadeCampo(sNomCampo);
            List<ListaDeCampo> listaListaDeCampo = new List<ListaDeCampo>();
            for (int i = 0; i < lista.Length; i++)
            {
                ListaDeCampo objListaDeCampo = new ListaDeCampo();

                objListaDeCampo.SNomCampo = lista[i].SNomCampo;
                objListaDeCampo.SCodCampo = lista[i].SCodCampo;
                objListaDeCampo.SCodRelativo = lista[i].SCodRelativo;
                objListaDeCampo.SDscCampo = lista[i].SDscCampo;
                objListaDeCampo.SLogUsuarioMod = lista[i].SLogUsuarioMod;
                objListaDeCampo.SLogFechaMod = lista[i].SLogFechaMod;
                objListaDeCampo.SLogHoraMod = lista[i].SLogHoraMod;

                listaListaDeCampo.Add(objListaDeCampo);
            }

            return listaListaDeCampo;
        }

        #endregion

        #region Modalidad de Venta


        public override bool insertarModalidadVenta(ModalidadVenta objModalidadVenta)
        {
            wsAdministracion.ModalidadVenta objWSModalidadVenta = new wsAdministracion.ModalidadVenta();

            objWSModalidadVenta.SNomModalidad = objModalidadVenta.SNomModalidad;
            objWSModalidadVenta.STipModalidad = objModalidadVenta.STipModalidad;
            objWSModalidadVenta.SLogUsuarioMod = objModalidadVenta.SLogUsuarioMod;

            return objWSAdministracion.insertarModalidadVenta(objWSModalidadVenta);

        }


        public override bool actualizarModalidadVenta(ModalidadVenta objModalidadVenta)
        {
            wsAdministracion.ModalidadVenta objWSModalidadVenta = new wsAdministracion.ModalidadVenta();

            objWSModalidadVenta.SCodModalidadVenta = objModalidadVenta.SCodModalidadVenta;
            objWSModalidadVenta.SNomModalidad = objModalidadVenta.SNomModalidad;
            objWSModalidadVenta.STipModalidad = objModalidadVenta.STipModalidad;
            objWSModalidadVenta.STipEstado = objModalidadVenta.STipEstado;
            objWSModalidadVenta.SLogUsuarioMod = objModalidadVenta.SLogUsuarioMod;

            return objWSAdministracion.insertarModalidadVenta(objWSModalidadVenta);
           
        }


        public override ModalidadVenta obtenerModalidadVentaxCodigo(string sCodModalidad)
        {
            wsAdministracion.ModalidadVenta objWSModalidadVenta = null;
            objWSModalidadVenta = objWSAdministracion.obtenerModalidadVentaxCodigo(sCodModalidad);
            if (objWSModalidadVenta != null)
            {
                return new ModalidadVenta(objWSModalidadVenta.SCodModalidadVenta, objWSModalidadVenta.SNomModalidad,
                    objWSModalidadVenta.STipModalidad, objWSModalidadVenta.STipEstado, objWSModalidadVenta.SLogUsuarioMod,
                    objWSModalidadVenta.SLogFechaMod, objWSModalidadVenta.SLogHoraMod);
            }
            else return null;
           
        }


        public override ModalidadVenta obtenerModalidadVentaxNombre(string sNomModalidad)
        {
            wsAdministracion.ModalidadVenta objWSModalidadVenta = null;
            objWSModalidadVenta = objWSAdministracion.obtenerModalidadVentaxNombre(sNomModalidad);
            if (objWSModalidadVenta != null)
            {
                return new ModalidadVenta(objWSModalidadVenta.SCodModalidadVenta, objWSModalidadVenta.SNomModalidad,
                    objWSModalidadVenta.STipModalidad, objWSModalidadVenta.STipEstado, objWSModalidadVenta.SLogUsuarioMod,
                    objWSModalidadVenta.SLogFechaMod, objWSModalidadVenta.SLogHoraMod);
            }
            else return null;
            
        }


        public override DataTable ListarAllModalidadVenta()
        {
            DataTable DTModalidadVenta;

            DTModalidadVenta = objWSAdministracion.ListarAllModalidadVenta();
            return DTModalidadVenta;
        }


        public override List<ModalidadVenta> listarModalidadVenta()
        {
            wsAdministracion.ModalidadVenta[] lista = objWSAdministracion.listarModalidadVenta();
            List<ModalidadVenta> listaModalidadVenta = new List<ModalidadVenta>();
            for (int i = 0; i < lista.Length; i++)
            {
                ModalidadVenta objModalidadVenta = new ModalidadVenta();

                objModalidadVenta.SCodModalidadVenta = lista[i].SCodModalidadVenta;
                objModalidadVenta.SNomModalidad = lista[i].SNomModalidad;
                objModalidadVenta.STipModalidad = lista[i].STipModalidad;
                objModalidadVenta.STipEstado = lista[i].STipEstado;
                objModalidadVenta.SLogUsuarioMod = lista[i].SLogUsuarioMod;
                objModalidadVenta.SLogFechaMod = lista[i].SLogFechaMod;
                objModalidadVenta.SLogHoraMod = lista[i].SLogHoraMod;

                listaModalidadVenta.Add(objModalidadVenta);
            }

            return listaModalidadVenta;

        }



        #endregion


        #region esilva
        // TasaCambio
        public override bool RegistrarTasaCambio(TasaCambio objTasaCambio)
        {
            //Casting
            wsAdministracion.TasaCambio objWSTasaCambio = new wsAdministracion.TasaCambio();
            objWSTasaCambio.SCodTasaCambio = objTasaCambio.SCodTasaCambio;
            objWSTasaCambio.STipCambio = objTasaCambio.STipCambio;
            objWSTasaCambio.SCodMoneda = objTasaCambio.SCodMoneda;
            objWSTasaCambio.DImpCambioActual = objTasaCambio.DImpCambioActual;
            objWSTasaCambio.SLogUsuarioMod = objTasaCambio.SLogUsuarioMod;

            return objWSAdministracion.RegistrarTasaCambio(objWSTasaCambio);
        }
        public override bool EliminarTasaCambio(string strCodTasaCambio)
        {
            return objWSAdministracion.EliminarTasaCambio(strCodTasaCambio);
        }
        public override DataTable ObtenerTasaCambio(string strCodTasaCambio)
        {
            DataTable dtTasaCambio;
            dtTasaCambio = objWSAdministracion.ObtenerTasaCambio(strCodTasaCambio);
            return dtTasaCambio;
        }
        // TasaCambioHist
        public override bool RegistrarTasaCambioHist(TasaCambioHist objTasaCambioHist)
        {
            //Casting
            wsAdministracion.TasaCambioHist objWSTasaCambioHist = new wsAdministracion.TasaCambioHist();
            objWSTasaCambioHist.SCodTasaCambio = objTasaCambioHist.SCodTasaCambio;
            objWSTasaCambioHist.STipCambio = objTasaCambioHist.STipCambio;
            objWSTasaCambioHist.SCodMoneda = objTasaCambioHist.SCodMoneda;
            objWSTasaCambioHist.DImpValor = objTasaCambioHist.DImpValor;
            objWSTasaCambioHist.SCodMoneda2 = objTasaCambioHist.SCodMoneda2;
            objWSTasaCambioHist.DImpValor2 = objTasaCambioHist.DImpValor2;
            objWSTasaCambioHist.DtFchIni = objTasaCambioHist.DtFchIni;
            objWSTasaCambioHist.DtFchFin = objTasaCambioHist.DtFchFin;
            objWSTasaCambioHist.SLogUsuarioMod = objTasaCambioHist.SLogUsuarioMod;

            return objWSAdministracion.RegistrarTasaCambioHist(objWSTasaCambioHist);
        }
        public override DataTable ObtenerTasaCambioHist(string strCodTasaCambio)
        {
            DataTable dtTasaCambioHist;
            dtTasaCambioHist = objWSAdministracion.ObtenerTasaCambioHist(strCodTasaCambio);
            return dtTasaCambioHist;
        }
        // PrecioTicket
        public override bool RegistrarPrecioTicket(PrecioTicket objPrecioTicket)
        {
            //Casting
            wsAdministracion.PrecioTicket objWSPrecioTicket = new wsAdministracion.PrecioTicket();
            objWSPrecioTicket.SCodPrecioTicket = objPrecioTicket.SCodPrecioTicket;
            objWSPrecioTicket.SCodTipoTicket = objPrecioTicket.SCodTipoTicket;
            objWSPrecioTicket.SCodMoneda = objPrecioTicket.SCodMoneda;
            objWSPrecioTicket.DImpPrecio = objPrecioTicket.DImpPrecio;
            objWSPrecioTicket.SLogUsuarioMod = objPrecioTicket.SLogUsuarioMod;

            return objWSAdministracion.RegistrarPrecioTicket(objWSPrecioTicket);
        }
        public override bool EliminarPrecioTicket(string strCodPrecioTicket)
        {
            return objWSAdministracion.EliminarPrecioTicket(strCodPrecioTicket);
        }
        public override DataTable ObtenerPrecioTicket(string strCodPrecioTicket)
        {
            DataTable dtPrecioTicket;
            dtPrecioTicket = objWSAdministracion.ObtenerPrecioTicket(strCodPrecioTicket);
            return dtPrecioTicket;
        }
        // PrecioTicketHist
        public override bool RegistrarPrecioTicketHist(PrecioTicketHist objPrecioTicketHist)
        {
            //Casting
            wsAdministracion.PrecioTicketHist objWSPrecioTicketHist = new wsAdministracion.PrecioTicketHist();
            objWSPrecioTicketHist.SCodPrecioTicket = objPrecioTicketHist.SCodPrecioTicket;
            objWSPrecioTicketHist.SCodTipoTicket = objPrecioTicketHist.SCodTipoTicket;
            objWSPrecioTicketHist.SCodMoneda = objPrecioTicketHist.SCodMoneda;
            objWSPrecioTicketHist.DImpPrecio = objPrecioTicketHist.DImpPrecio;
            objWSPrecioTicketHist.SCodMoneda2 = objPrecioTicketHist.SCodMoneda2;
            objWSPrecioTicketHist.DImpPrecio2 = objPrecioTicketHist.DImpPrecio2;
            objWSPrecioTicketHist.DtFchIni = objPrecioTicketHist.DtFchIni;
            objWSPrecioTicketHist.DtFchFin = objPrecioTicketHist.DtFchFin;
            objWSPrecioTicketHist.SLogUsuarioMod = objPrecioTicketHist.SLogUsuarioMod;

            return objWSAdministracion.RegistrarPrecioTicketHist(objWSPrecioTicketHist);
        }
        public override bool EliminarPrecioTicketHist(string strCodPrecioTicket)
        {
            return objWSAdministracion.EliminarPrecioTicketHist(strCodPrecioTicket);
        }
        public override DataTable ObtenerPrecioTicketHist(string strCodPrecioTicket)
        {
            DataTable dtPrecioTicketHist;
            dtPrecioTicketHist = objWSAdministracion.ObtenerPrecioTicketHist(strCodPrecioTicket);
            return dtPrecioTicketHist;
        }
        #endregion



        public override Usuario autenticar(string sCuenta, string sClave)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool CambiarClave(Usuario objUsuario)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool registrarUsuario(Usuario objUsuario)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool actualizarUsuario(Usuario objUsuario)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool eliminarUsuario(string sCodUsuario, string LogUsuarioMod)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override Usuario obtenerUsuario(string sCodUsuario)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override Usuario obtenerUsuarioxCuenta(string sCuentaUsuario)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool actualizarContraseñaUsuario(string sCodUsuario, string sContraseña, string SLogUsuarioMod, DateTime dtFchVigencia)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool actualizarEstadoUsuario(string sCodUsuario, string sEstado, string SLogUsuarioMod, string strFlgCambioClave)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override DataTable LLenarMenu(string sCodUsuario)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override DataTable LLenarArbolRoles(string sCodRol)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override ArbolModulo ObtenerArbolModulo(string sCodProceso, string sCodModulo, string sCodRol)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override List<ArbolModulo> ListarPerfilVenta(string strUsuario)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool registrarRolUsuario(UsuarioRol objRolUsuarioRol)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool eliminarRolUsuario(string sCodUsuario, string sCodRol)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override UsuarioRol obtenerUsuarioRolxCodRol(string sCodRol)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override List<UsuarioRol> ListarUsuarioRolxCodUsuario(string sCodUsuario)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override UsuarioRol obtenerRolUsuario(string sCodUsuario, string sCodRol)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override DataTable ListaRol()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool registrarRol(Rol objRol)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool actualizarRol(Rol objRol)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool eliminarRol(string sCodRol)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override Rol obtenerRolxnombre(string sNomRol)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override Rol obtenerRolxcodigo(string sCodRol)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override List<Rol> listaDeRoles()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override List<Rol> listarRolesAsignados(string sCodUsuario)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override List<Rol> listarRolesSinAsignar(string sCodUsuario)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool registrarPerfilRol(PerfilRol objPerfilRol)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool actualizarPerfilRol(PerfilRol objPerfilRol)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override DataTable ListarPerfilRolxRol(string sCodRol)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override List<PerfilRol> ListadoPerfilRolxRol(string sCodRol)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override string FlagPerfilRolxOpcion(string sCodUsuario, string sCodRol, string sDscArchivo, string sOpcion)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool obtenerClaveUsuHist(string sCodUsuario, string SDscClave, int iNum)
        {
            throw new Exception("The method or operation is not implemented.");
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
            throw new Exception("The method or operation is not implemented.");
        }

        public override DataTable ListarParametros(string as_identificador)
        {
            throw new Exception("The method or operation is not implemented.");
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

        public override DataTable ListarAllParametroGenerales(string strParam)
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

        public override DataTable ObtenerEmpresaPorIdentificadorPadre(string identificador)
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


        public override DataTable ConsultaTurnoxFiltro(string as_fchIni, string as_fchFin, string as_codusuario, string as_ptoventa, string as_horadesde, string as_horahasta, string flagReporte = "0")
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

        public override bool RegistrarTicket(string strCompania, string strVentaMasiva, string strNumVuelo, string strFecVuelo, string strTurno, string strUsuario, decimal decPrecio, string strMoneda, string strModVenta, string strTipTicket, string strTipVuelo, int intTickets, string strFlagCont, string strNumRef, string strTipPago, string strEmpresa, string strRepte, ref string strFecVence, ref string strListaTickets, string strCodTurnoIng, string strFlgCierreTurno, string strCodPrecio, string strMetPago, string strEmpresaRecaudadora)
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

        public override DataTable obtenerFecha()
        {
            throw new NotImplementedException();
        }

        public override ClaveUsuHist obtenerUsuarioHist(string sCodUsuario)
        {
            throw new NotImplementedException();
        }

        public override DataTable ListarTasaCambio()
        {
            throw new NotImplementedException();
        }

        public override DataTable GetTicketsByFilter(string strTicketDesde, string strTicketHasta, string strCodTurno)
        {
            throw new NotImplementedException();
        }

        public override bool ExtornarTicket(string strListaTickets, string strTurno, int intCantidad , string strMotivo, string strUsuario, ref string strMessage)
        {
            throw new NotImplementedException();
        }

        public override DataTable consultarDetalleTicket(string sNumTicket, string sTicketDesde, string sTicketHasta)
        {
            throw new NotImplementedException();
        }

        public override DataTable consultarHistTicket(string sNumTicket)
        {
            throw new NotImplementedException();
        }
    }
}
