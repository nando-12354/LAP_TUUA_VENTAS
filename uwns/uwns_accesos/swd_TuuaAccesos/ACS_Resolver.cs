using System;
using System.Collections.Generic;
using System.Text;
using LAP.TUUA.ENTIDADES;
using LAP.TUUA.DAO;
using LAP.TUUA.UTIL;
using System.Data.SqlClient;
using System.Data;


namespace LAP.TUUA.ACCESOS
{
    class ACS_Resolver
    {
        protected string Dsc_PathSPConfig;
        public DAO_Usuario Obj_DAOUsuario;
        public DAO_ListaDeCampos Obj_DAOListaDeCampos;
        public DAO_Compania Obj_DAOCompania;
        public DAO_TipoTicket Obj_DAOTipoTicket;

        public DAO_BoardingBcbp Obj_DAOBoardingBcbp;
        public DAO_BoardingBcbpErr Obj_DAOBoardingBcbpErr;
        public DAO_ModVentaComp Obj_DAOModVentaComp;
        public DAO_VueloProgramado Obj_DAOVueloProgramado;
        public DAO_Ticket Obj_DAOTicket;
        public DAO_TicketEstHist Obj_DAOTicketEstHist;
        public DAO_TicketErr Obj_DAOTicketErr;
        public DAO_BoardingBcbpEstHist Obj_DAOBoardingBcbpHist;

        public DAO_Auditoria Obj_DAOAuditoria;

        public DAO_Molinete Obj_DAOMolinete;
        private ACS_Util Obj_Util;

        private bool blnModoLocal;


        public ACS_Resolver(ACS_Util ObjUtil)
        {
            Dsc_PathSPConfig = AppDomain.CurrentDomain.BaseDirectory;
            Obj_DAOUsuario = null;
            Obj_DAOListaDeCampos = null;
            Obj_DAOCompania = null;
            Obj_DAOTipoTicket = null;
            Obj_DAOBoardingBcbp = null;
            Obj_DAOBoardingBcbpErr = null;
            Obj_DAOBoardingBcbpHist = null;
            Obj_DAOModVentaComp = null;
            Obj_DAOVueloProgramado = null;
            Obj_DAOTicket = null;
            Obj_DAOVueloProgramado = null;
            Obj_DAOTicketEstHist = null;
            Obj_DAOTicketErr = null;
            Obj_DAOAuditoria = null;
            Obj_DAOMolinete = null;

            this.blnModoLocal = false;

            this.Obj_Util = ObjUtil;
        }




        #region RegTicketErr
        public void CrearDAOTicketErr()
        {
            //Obj_DAOTicketErr = new DAO_TicketErr(Dsc_PathSPConfig);
            Obj_DAOTicketErr = new DAO_TicketErr(ACS_Property.shelper, ACS_Property.shelperLocal, Property.shtSPConfig);

        }

        /// <summary>
        /// Actualizar Ticket Error
        /// LAP-TUUA-9163 - 19-06-2012 (RS) CMONTES
        /// </summary>
        /// <param name="objTicketErr"></param>
        /// <returns></returns>
        public bool InsertarTicketErr(TicketErr objTicketErr)
        {
            try
            {
                Obj_DAOTicketErr.Cod_Modulo = ACS_Define.Cod_Modulo;
                Obj_DAOTicketErr.Cod_Sub_Modulo = ACS_Define.Cod_SModRegTicket;

                if (ACS_Property.BConRemota)
                {
                    Obj_DAOTicketErr.insertar(objTicketErr);
                    return true;
                }

                if (ACS_Property.BConLocal)
                {
                    Obj_DAOTicketErr.ConexionLocal();
                    Obj_DAOTicketErr.insertar(objTicketErr);
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                throw;
            }

        }
        #endregion

        #region RegTicketEstHist

        public void CrearTicketEstHist()
        {
            //Obj_DAOTicketEstHist = new DAO_TicketEstHist(Dsc_PathSPConfig);
            Obj_DAOTicketEstHist = new DAO_TicketEstHist(ACS_Property.shelper, ACS_Property.shelperLocal, Property.shtSPConfig);
        }


        public bool InsertarTicketEstHist(TicketEstHist objTicketEstHist)
        {

            try
            {
                if (ACS_Property.BConRemota)
                    return Obj_DAOTicketEstHist.insertar(objTicketEstHist);
                if (ACS_Property.BConLocal)
                {
                    //Obj_DAOTicket.Conexion("tuuacnxlocal");
                    Obj_DAOTicketEstHist.ConexionLocal();
                    return Obj_DAOTicketEstHist.insertar(objTicketEstHist);
                }
                return false;
            }
            catch (Exception ex)
            {
                throw;
            }



            //try
            //{
            //      bool Rpta = Obj_DAOTicketEstHist.insertar(objTicketEstHist);
            //      ValidaBDRemota();
            //      return Rpta;
            //}
            //catch (Exception ex)
            //{
            //      if (IsDBConnectionError(ex))
            //      {
            //            Obj_DAOTicket.Conexion("tuuacnxlocal");
            //            ValidaBDLocal();
            //            return Obj_DAOTicketEstHist.insertar(objTicketEstHist);
            //      }
            //      throw;
            //}
        }

        public List<TicketEstHist> ListarTicketEstHist(string strNumeroTicket)
        {
            try
            {
                if (ACS_Property.BConRemota)
                    return Obj_DAOTicketEstHist.listarxNumeroTicket(strNumeroTicket);
                if (ACS_Property.BConLocal)
                {
                    Obj_DAOTicketEstHist.ConexionLocal();
                    return Obj_DAOTicketEstHist.listarxNumeroTicket(strNumeroTicket);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        #endregion RegTicketEstHist

        #region RegTicket

        public void CrearDAOTicket()
        {
            //Obj_DAOTicket = new DAO_Ticket(Dsc_PathSPConfig);
            Obj_DAOTicket = new DAO_Ticket(ACS_Property.shelper, ACS_Property.shelperLocal, Property.shtSPConfig);
        }

        public Ticket obtenerTicket(string sCodNumeroTicket)
        {
            try
            {
                if (ACS_Property.BConRemota)
                    return Obj_DAOTicket.obtener(sCodNumeroTicket);
                if (ACS_Property.BConLocal)
                {
                    //Obj_DAOTicket.Conexion("tuuacnxlocal");
                    Obj_DAOTicket.ConexionLocal();
                    return Obj_DAOTicket.obtener(sCodNumeroTicket);
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Actualiza los Datos de un Ticket solo para conexion con la central
        /// LAP-TUUA-9163 - 15-06-2012 (RS) CMONTES:
        /// </summary>
        /// <param name="objTicket"></param>
        /// <returns></returns>
        public bool ActualizarTicket(Ticket objTicket)
        {
            try
            {
                if (ACS_Property.BConRemota)
                {
                    objTicket.Flg_Sincroniza = "0";
                    return Obj_DAOTicket.actualizar(objTicket);
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Actualiza los Datos de un Ticket en OffLine
        /// LAP-TUUA-9163 - 18-06-2012 (RS) CMONTES:
        /// Solo debe grabar en la BD local cuando no exista conexion
        /// con la BD remota.
        /// </summary>
        /// <param name="objTicket"></param>
        /// <returns></returns>
        public bool ActualizarTicketOffLine(Ticket objTicket)
        {
            try
            {
                if (ACS_Property.BConLocal)
                {
                    Obj_DAOTicket.Conexion("tuuacnxlocal");
                    objTicket.Flg_Sincroniza = "0";
                    Obj_DAOTicket.ConexionLocal();
                    return Obj_DAOTicket.actualizarOffLine(objTicket);
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion RegTicket

        #region RegVueloProgramado
        public void CrearDAOVueloProgramado()
        {
            //Obj_DAOVueloProgramado = new DAO_VueloProgramado(Dsc_PathSPConfig);
            Obj_DAOVueloProgramado = new DAO_VueloProgramado(ACS_Property.shelper, ACS_Property.shelperLocal, Property.shtSPConfig);
        }

        public VueloProgramado obtenerVueloProgramado(string sCodCompania, string FchInicio, string NumVuelo)
        {
            try
            {
                if (ACS_Property.BConRemota)
                    return Obj_DAOVueloProgramado.obtener(sCodCompania, FchInicio, NumVuelo);
                if (ACS_Property.BConLocal)
                {
                    Obj_DAOModVentaComp.Conexion("tuuacnxlocal");
                    return Obj_DAOVueloProgramado.obtener(sCodCompania, FchInicio, NumVuelo);
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public VueloProgramado obtenerVueloProgramado(string FchInicio, string NumVuelo)
        {
            try
            {
                if (ACS_Property.BConRemota)
                    return Obj_DAOVueloProgramado.ObtenerVueloxNum(FchInicio, NumVuelo);
                if (ACS_Property.BConLocal)
                {
                    VueloProgramado objVueloPrg = new VueloProgramado();
                    objVueloPrg.Tip_Vuelo = (string)ACS_Property.shtMolinete["Tip_Vuelo"];
                    return objVueloPrg;
                    // Obj_DAOVueloProgramado.Conexion("tuuacnxlocal");
                    //Obj_DAOVueloProgramado.ConexionLocal();
                    //return Obj_DAOVueloProgramado.ObtenerVueloxNum(FchInicio, NumVuelo);
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion RegVueloProgramado

        #region RegBoardingBcbpHist
        public void CrearDAOBoardingBcbpHist()
        {
            //Obj_DAOBoardingBcbpHist = new DAO_BoardingBcbpEstHist(Dsc_PathSPConfig);
            Obj_DAOBoardingBcbpHist = new DAO_BoardingBcbpEstHist(ACS_Property.shelper, ACS_Property.shelperLocal, Property.shtSPConfig);
        }

        public DataTable obtenerDAOBoardingBcbpHist(string Cod_Compania, string Num_Vuelo, string Fch_Vuelo, string Num_Asiento,
                                                    string Nom_Pasajero)
        {
            try
            {
                if (ACS_Property.BConRemota)
                    return Obj_DAOBoardingBcbpHist.DetalleBoardingEstHist(Cod_Compania, Num_Vuelo, Fch_Vuelo, Num_Asiento,
                                                                          Nom_Pasajero);
                if (ACS_Property.BConLocal)
                {
                    Obj_DAOBoardingBcbp.ConexionLocal();
                    return Obj_DAOBoardingBcbpHist.DetalleBoardingEstHist(Cod_Compania, Num_Vuelo, Fch_Vuelo, Num_Asiento,
                                                                          Nom_Pasajero);
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region RegModVentaComp
        public void CrearDAOModVentaComp()
        {
            //Obj_DAOModVentaComp = new DAO_ModVentaComp(Dsc_PathSPConfig);
            Obj_DAOModVentaComp = new DAO_ModVentaComp(ACS_Property.shelper, ACS_Property.shelperLocal, Property.shtSPConfig);
        }

        public ModVentaComp obtenerModVentaComp(string sCodCompania, string strNomModalidad)
        {
            try
            {
                if (ACS_Property.BConRemota)
                    return Obj_DAOModVentaComp.obtener(sCodCompania, strNomModalidad);
                if (ACS_Property.BConLocal)
                {
                    //Obj_DAOModVentaComp.Conexion("tuuacnxlocal");
                    Obj_DAOModVentaComp.ConexionLocal();
                    return Obj_DAOModVentaComp.obtener(sCodCompania, strNomModalidad);
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }

        }
        #endregion RegModVentaComp

        #region RegBoardingBcbp
        public void CrearDAOBoardingBcbp()
        {
            //Obj_DAOBoardingBcbp = new DAO_BoardingBcbp(Dsc_PathSPConfig);
            Obj_DAOBoardingBcbp = new DAO_BoardingBcbp(ACS_Property.shelper, ACS_Property.shelperLocal, Property.shtSPConfig);

        }

        public BoardingBcbp obtenerDAOBoardingBcbp(String strNum_Vuelo, String strFechVuelo, String strNumeroAsiento,
                                                    String strNomPasajero)
        {
            try
            {
                if (ACS_Property.BConRemota)
                    return Obj_DAOBoardingBcbp.obtener(strNum_Vuelo, strFechVuelo, strNumeroAsiento, strNomPasajero);
                if (ACS_Property.BConLocal)
                {
                    Obj_DAOBoardingBcbp.ConexionLocal();
                    return Obj_DAOBoardingBcbp.obtener(strNum_Vuelo, strFechVuelo, strNumeroAsiento, strNomPasajero);
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public BoardingBcbp obtenerDAOBoardingBcbpxCodUnicoBCBP(String strCodCompania, String strCodUnicoBCBP)
        {
            try
            {
                if (ACS_Property.BConRemota)
                    return Obj_DAOBoardingBcbp.obtener(strCodCompania, strCodUnicoBCBP);
                if (ACS_Property.BConLocal)
                {
                    Obj_DAOBoardingBcbp.ConexionLocal();
                    return Obj_DAOBoardingBcbp.obtener(strCodCompania, strCodUnicoBCBP);
                }
                return null;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public BoardingBcbp obtenerDAOBoardingBcbp(String strCod_Compania, String strNum_Vuelo,
                                                   String strFechVuelo, String strNumeroAsiento,
                                                   String strNomPasajero, string strtSecuencial_Bcbp, string strNumCheckin)
        {
            try
            {
                if (ACS_Property.BConRemota)
                    return Obj_DAOBoardingBcbp.obtener(strCod_Compania, strNum_Vuelo, strFechVuelo,
                                                       strNumeroAsiento, strNomPasajero, null, null, strtSecuencial_Bcbp, strNumCheckin);
                if (ACS_Property.BConLocal)
                {
                    Obj_DAOBoardingBcbp.ConexionLocal();
                    return Obj_DAOBoardingBcbp.obtener(strCod_Compania, strNum_Vuelo, strFechVuelo,
                                                       strNumeroAsiento, strNomPasajero, null, null, strtSecuencial_Bcbp, strNumCheckin);
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Inserta el BCBP
        /// LAP-TUUA-9163 - 15-06-2012 (RS) CMONTES: 
        /// Solo se debe grabar en la BD Local cuando no existe conexion remota.
        /// </summary>
        /// <param name="objBoardingBcbp"></param>
        /// <returns></returns>
        public bool insertarDAOBoardingBcbp(BoardingBcbp objBoardingBcbp)
        {
            try
            {
                if (ACS_Property.BConRemota)
                {
                    objBoardingBcbp.StrFlg_Sincroniza = "0";
                    return Obj_DAOBoardingBcbp.insertar(objBoardingBcbp);//0
                }

                if (ACS_Property.BConLocal)
                {
                    objBoardingBcbp.StrFlg_Sincroniza = "0";
                    Obj_DAOBoardingBcbp.ConexionLocal();
                    return Obj_DAOBoardingBcbp.insertarOffLine(objBoardingBcbp);//0
                }

                return false;
            }
            catch (Exception)
            {
                throw;
            }

        }

        /// <summary>
        /// LAP-TUUA-9163 - 19-06-2012 (RS) CMONTES
        /// </summary>
        /// <param name="objBoardingBcbp"></param>
        public void actualizarDAOBoardingBcbp(BoardingBcbp objBoardingBcbp)
        {
            try
            {
                if (ACS_Property.BConRemota)
                {
                    objBoardingBcbp.StrFlg_Sincroniza = "0";
                    Obj_DAOBoardingBcbp.actualizarEstado(objBoardingBcbp);//0                    
                }

                if (ACS_Property.BConLocal)
                {
                    objBoardingBcbp.StrFlg_Sincroniza = "0";//REMOTA FAIL SOLO BCBP
                    //Obj_DAOBoardingBcbp.Conexion("tuuacnxlocal");
                    Obj_DAOBoardingBcbp.ConexionLocal();
                    Obj_DAOBoardingBcbp.actualizarEstado(objBoardingBcbp);//0                    
                }

                return;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion RegBoardingBcbp

        #region RegBoardingErr
        /// <summary>
        /// LAP-TUUA-9163 - 19-06-2012 (RS) CMONTES
        /// </summary>
        /// <param name="objBoardingBcbpErr"></param>
        /// <returns></returns>
        public bool InsertarBoardingBcbpErr(BoardingBcbpErr objBoardingBcbpErr)
        {
            try
            {
                if (ACS_Property.BConRemota)
                {
                    Obj_DAOBoardingBcbpErr.insertar(objBoardingBcbpErr);
                    return true;
                }

                if (ACS_Property.BConLocal)
                {
                    //Obj_DAOBoardingBcbpErr.Conexion("tuuacnxlocal");
                    Obj_DAOBoardingBcbpErr.ConexionLocal();
                    Obj_DAOBoardingBcbpErr.insertar(objBoardingBcbpErr);
                    return true;
                }
                return false;

            }
            catch (Exception)
            {
                throw;
            }

        }

        public void CrearDAOBoardingBcbpErr()
        {
            //Obj_DAOBoardingBcbpErr = new DAO_BoardingBcbpErr(Dsc_PathSPConfig);
            Obj_DAOBoardingBcbpErr = new DAO_BoardingBcbpErr(ACS_Property.shelper, ACS_Property.shelperLocal, Property.shtSPConfig);
        }

        #endregion RegBoardingErr

        #region RegListaDeCampos
        public void CrearDAOListaDeCampos()
        {

            Obj_DAOListaDeCampos = new DAO_ListaDeCampos(ACS_Property.shelper,
                                                         ACS_Property.shelperLocal,
                                                         Property.shtSPConfig);
        }

        public List<ListaDeCampo> listarListaDeCampos()
        {
            try
            {
                if (ACS_Property.BConRemota)
                    return Obj_DAOListaDeCampos.listar();
                if (ACS_Property.BConLocal)
                {
                    //Obj_DAOListaDeCampos.Conexion("tuuacnxlocal");
                    Obj_DAOListaDeCampos.ConexionLocal();
                    return Obj_DAOListaDeCampos.listar();
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion RegListaDeCampos

        #region RegCompania

        public void CrearDAOCompania()
        {
            //Obj_DAOCompania = new DAO_Compania(Dsc_PathSPConfig);
            Obj_DAOCompania = new DAO_Compania(ACS_Property.shelper, ACS_Property.shelperLocal, Property.shtSPConfig);
        }

        public Compania obtenerxcodigoDAOCompania(string strCodigo)
        {
            try
            {
                if (ACS_Property.BConRemota)
                    return Obj_DAOCompania.obtenerxcodigo(strCodigo);
                if (ACS_Property.BConLocal)
                {
                    //Obj_DAOCompania.Conexion("tuuacnxlocal");
                    Obj_DAOCompania.ConexionLocal();
                    return Obj_DAOCompania.obtenerxcodigo(strCodigo);
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<Compania> listarCompania()
        {
            try
            {
                if (ACS_Property.BConRemota)
                    return Obj_DAOCompania.listar();
                if (ACS_Property.BConLocal)
                {
                    //Obj_DAOCompania.Conexion("tuuacnxlocal");
                    Obj_DAOCompania.ConexionLocal();
                    return Obj_DAOCompania.listar();
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion RegCompania

        #region RegTipoTicket
        public void CrearDAOTipoTicket()
        {
            //Obj_DAOTipoTicket = new DAO_TipoTicket(Dsc_PathSPConfig);
            Obj_DAOTipoTicket = new DAO_TipoTicket(ACS_Property.shelper, ACS_Property.shelperLocal, Property.shtSPConfig);
        }



        public List<TipoTicket> listarTipoTicket()
        {
            try
            {
                if (ACS_Property.BConRemota)
                    return Obj_DAOTipoTicket.listarTabla();
                if (ACS_Property.BConLocal)
                {
                    Obj_DAOTipoTicket.ConexionLocal();
                    return Obj_DAOTipoTicket.listarTabla();
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }


        #endregion RegTipoTicket

        #region RegUsuario


        public void CrearDAOUsuario()
        {

            //Obj_DAOUsuario = new DAO_Usuario(Dsc_PathSPConfig);
            Obj_DAOUsuario = new DAO_Usuario(ACS_Property.shelper, ACS_Property.shelperLocal, Property.shtSPConfig);

        }

        public Usuario autenticar(string strCuenta)
        {
            try
            {
                if (ACS_Property.BConRemota)
                    return Obj_DAOUsuario.autenticar(strCuenta);
                if (ACS_Property.BConLocal)
                {
                    //Obj_DAOUsuario.Conexion("tuuacnxlocal");
                    Obj_DAOUsuario.ConexionLocal();
                    return Obj_DAOUsuario.autenticar(strCuenta);
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Usuario> listarUsuario()
        {
            try
            {
                if (ACS_Property.BConRemota)
                    return Obj_DAOUsuario.listar();
                if (ACS_Property.BConLocal)
                {
                    Obj_DAOUsuario.ConexionLocal();
                    return Obj_DAOUsuario.listar();

                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
        //***Agregado por jortega 01/06/2010

        //**
        #endregion RegUsuario



        #region RegAuditHabMolinete
        public void CrearDAOAuditoria()
        {
            Obj_DAOAuditoria = new DAO_Auditoria(ACS_Property.shelper, ACS_Property.shelperLocal, Property.shtSPConfig);

        }
        /// <summary>
        /// LAP-TUUA-9163 - 19-06-2012 (RS) CMONTES
        /// </summary>
        /// <param name="Cod_Acceso"></param>
        /// <param name="Cod_Web"></param>
        /// <param name="Cod_Molinete"></param>
        /// <returns></returns>
        public bool InsertarAuditoria(string Cod_Acceso, string Cod_Web, string Cod_Molinete)
        {
            try
            {
                if (ACS_Property.BConRemota)
                {
                    Obj_DAOAuditoria.insertar_habmolinete(Cod_Acceso, Cod_Web, Cod_Molinete);
                    return true;
                }

                if (ACS_Property.BConLocal)
                {
                    Obj_DAOAuditoria.ConexionLocal();
                    Obj_DAOAuditoria.insertar_habmolinete(Cod_Acceso, Cod_Web, Cod_Molinete);
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public bool InsertarAuditoriaModificarTipoMolinete(string Cod_Molinete, string Tipo_Molinete, string Usuario)
        {
            try
            {
                if (ACS_Property.BConRemota)
                {
                    Obj_DAOAuditoria.InsertarAuditoriaModificarTipoMolinete(Cod_Molinete, Tipo_Molinete, Usuario);
                    return true;
                }

                if (ACS_Property.BConLocal)
                {
                    Obj_DAOAuditoria.ConexionLocal();
                    Obj_DAOAuditoria.InsertarAuditoriaModificarTipoMolinete(Cod_Molinete, Tipo_Molinete, Usuario);
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public bool InsertarAuditoriaDestrabeMolinete(string Cod_Molinete, string Usuario, string TipResultado)
        {
            try
            {
                if (ACS_Property.BConRemota)
                {
                    Obj_DAOAuditoria.InsertarAuditoriaDestrabeMolinete(Cod_Molinete, Usuario, TipResultado);
                    return true;
                }

                /*
                if (ACS_Property.BConLocal)
                {
                    Obj_DAOAuditoria.ConexionLocal();
                    Obj_DAOAuditoria.InsertarAuditoriaDestrabeMolinete(Cod_Molinete, Usuario);
                    return true;
                }
                */

                return false;
            }
            catch (Exception)
            {
                throw;
            }

        }
        #endregion



        public bool IsDBConnectionError(Exception ex)
        {

            if (ex.GetType().Name == "SqlException")
            {
                SqlException objSqlEx = (SqlException)ex;
                return objSqlEx.ErrorCode == -2146232060 ? true : false;

            }
            return false;
        }

        public void ValidaBDLocal()
        {
            if (this.blnModoLocal == false)
            {
                this.blnModoLocal = true;
                Obj_Util.EscribirLog(this, "Modo BD Local Iniciado...");
            }
        }
        public void ValidaBDRemota()
        {
            if (this.blnModoLocal)
            {
                this.blnModoLocal = false;
                Obj_Util.EscribirLog(this, "Modo BD Remota Reiniciado...");
            }
        }

        public DataTable ListarSupervisor(string strRol, string strEstado)
        {
            try
            {
                if (ACS_Property.BConRemota)
                    return Obj_DAOUsuario.consultarUsuarioxFiltro(strRol, strEstado, null, null, null);
                if (ACS_Property.BConLocal)
                {
                    Obj_DAOUsuario.ConexionLocal();
                    return Obj_DAOUsuario.consultarUsuarioxFiltro(strRol, strEstado, null, null, null);
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataTable ListarAtributosWSAerolinea(string strAerolinea)
        {
            try
            {
                if (ACS_Property.BConRemota)
                    return Obj_DAOModVentaComp.ListarAtributosWSAerolinea(strAerolinea);
                if (ACS_Property.BConLocal)
                {
                    //Obj_DAOModVentaComp.Conexion("tuuacnxlocal");
                    Obj_DAOModVentaComp.ConexionLocal();
                    return Obj_DAOModVentaComp.ListarAtributosWSAerolinea(strAerolinea);
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public void CrearDAOMolinete()
        {
            Obj_DAOMolinete = new DAO_Molinete(ACS_Property.shelper, ACS_Property.shelperLocal, Property.shtSPConfig);
        }

        public void ActualizarTipoVueloMolinete(string Cod_Molinete, string TipoVuelo, string Cod_Usuario, string TipoResultado)
        {
            try
            {
                if (ACS_Property.BConRemota)
                {
                    Obj_DAOMolinete.actualizarTipoVueloMolinete(Cod_Molinete, TipoVuelo, Cod_Usuario, TipoResultado, "1");
                     //SuperaSincronismo - Adicionar un campo que indique el sincronismo para el modo local (P)
                     Obj_DAOMolinete.ConexionLocal();
                     Obj_DAOMolinete.actualizarTipoVueloMolinete(Cod_Molinete, TipoVuelo, Cod_Usuario, TipoResultado, "0");                                        
                }
                   
                if (ACS_Property.BConLocal)
                {
                     Obj_DAOMolinete.ConexionLocal();
                     Obj_DAOMolinete.actualizarTipoVueloMolinete(Cod_Molinete, TipoVuelo, Cod_Usuario, TipoResultado, "0");
                }               
            }
            catch (Exception)
            {
                throw;
            }
        }



    }
}
