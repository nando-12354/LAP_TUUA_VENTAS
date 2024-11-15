using System;
using System.Collections.Generic;
using System.Text;
using LAP.TUUA.ENTIDADES;
using LAP.TUUA.DAO;
using LAP.TUUA.UTIL;
using System.Data.SqlClient;
using System.Data;

namespace LAP.TUUA.LOGS
{
    public class ACS_Resolver
    {
        protected string Dsc_PathSPConfig;
        public DAO_Usuario Obj_DAOUsuario;
        public DAO_ListaDeCampos Obj_DAOListaDeCampos;
        public DAO_Compania Obj_DAOCompania;

        public DAO_BoardingBcbp Obj_DAOBoardingBcbp;
        public DAO_BoardingBcbpErr Obj_DAOBoardingBcbpErr;
        public DAO_LogProcesados Obj_DAOLogProcesados;
        public DAO_VueloProgramado Obj_DAOVueloProgramado;
        public DAO_BoardingBcbpEstHist Obj_DAOBoardingBcbpHist;

        public DAO_Auditoria Obj_DAOAuditoria;
        private ACS_Util Obj_Util;

        public ACS_Resolver(ACS_Util ObjUtil)
        {
            Dsc_PathSPConfig = AppDomain.CurrentDomain.BaseDirectory;
            Obj_DAOUsuario = null;
            Obj_DAOListaDeCampos = null;
            Obj_DAOCompania = null;
            Obj_DAOBoardingBcbp = null;
            Obj_DAOBoardingBcbpErr = null;
            Obj_DAOLogProcesados = null;
            Obj_DAOBoardingBcbpHist = null;
            Obj_DAOVueloProgramado = null;
            Obj_DAOAuditoria = null;


            this.Obj_Util = ObjUtil;
        }


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
                if (ACS_Property.BConexion)
                    return Obj_DAOBoardingBcbp.obtener(strNum_Vuelo, strFechVuelo, strNumeroAsiento, strNomPasajero);

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
                if (ACS_Property.BConexion)
                    return Obj_DAOBoardingBcbp.obtener(strCodCompania, strCodUnicoBCBP);

                return null;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public BoardingBcbp obtenerDAOBoardingBcbp(String strCod_Compania, String strNum_Vuelo,
                                                   String strFechVuelo, String strNumeroAsiento,
                                                   String strNomPasajero, string strtSecuencial_Bcbp)
        {
            try
            {
                if (ACS_Property.BConexion)
                    return Obj_DAOBoardingBcbp.obtener(strCod_Compania, strNum_Vuelo, strFechVuelo,
                                                       strNumeroAsiento, strNomPasajero, null, null, strtSecuencial_Bcbp);

                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion RegBoardingBcbp

        #region RegBoardingErr
        public bool InsertarBoardingBcbpErr(BoardingBcbpErr objBoardingBcbpErr)
        {
            try
            {
                if (ACS_Property.BConexion)
                {
                    Obj_DAOBoardingBcbpErr.insertar(objBoardingBcbpErr);
                    return true;
                }

                return false;

            }
            catch (Exception ex)
            {
                Obj_Util.EscribirLog(this, ex.Message);
                throw;
            }

        }

        public void CrearDAOBoardingBcbpErr()
        {
            Obj_DAOBoardingBcbpErr = new DAO_BoardingBcbpErr(ACS_Property.shelper, ACS_Property.shelperLocal, Property.shtSPConfig);
        }

        #endregion RegBoardingErr

        #region RegLogProcesados
        public bool ArchivoProcesado(string strCodEquipoMod, string strNombreArchivo)
        {
            try
            {
                DataTable dtArchivo = new DataTable();

                dtArchivo = Obj_DAOLogProcesados.ArchivoProcesado(strCodEquipoMod, strNombreArchivo);

                if (dtArchivo == null || dtArchivo.Rows.Count < 1)
                    return false;
                else
                    return true;

            }
            catch (Exception ex)
            {
                Obj_Util.EscribirLog(this, ex.Message);
                throw;
            }
        }

        public DataTable UltimoArchivoProcesado(string strCodEquipoMod)
        {
            try
            {
                DataTable dtArchivo = new DataTable();

                dtArchivo = Obj_DAOLogProcesados.UltimoArchivoProcesado(strCodEquipoMod);

                if (dtArchivo != null && dtArchivo.Rows.Count > 0)
                    return dtArchivo;
                else
                    return null;

            }
            catch (Exception ex)
            {
                Obj_Util.EscribirLog(this, ex.Message);
                throw;
            }
        }

        public bool InsertarLogProcesados(LogProcesados objLogProcesados)
        {
            try
            {
                if (ACS_Property.BConexion)
                {
                    Obj_DAOLogProcesados.insertar(objLogProcesados);
                    return true;
                }

                return false;

            }
            catch (Exception ex)
            {
                Obj_Util.EscribirLog(this, ex.Message);
                throw;
            }

        }

        public void CrearDAOLogProcesados()
        {
            Obj_DAOLogProcesados = new DAO_LogProcesados(ACS_Property.shelper, ACS_Property.shelperLocal, Property.shtSPConfig);
        }

        #endregion RegLogProcesados

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
                if (ACS_Property.BConexion)
                    return Obj_DAOListaDeCampos.listar();

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
            Obj_DAOCompania = new DAO_Compania(ACS_Property.shelper, ACS_Property.shelperLocal, Property.shtSPConfig);
        }

        public Compania obtenerxcodigoDAOCompania(string strCodigo)
        {
            try
            {
                if (ACS_Property.BConexion)
                    return Obj_DAOCompania.obtenerxcodigo(strCodigo);

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
                if (ACS_Property.BConexion)
                    return Obj_DAOCompania.listar();

                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion RegCompania

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
                if (ACS_Property.BConexion)
                    return Obj_DAOUsuario.autenticar(strCuenta);
                //if (ACS_Property.BConLocal)
                //{
                //    //Obj_DAOUsuario.Conexion("tuuacnxlocal");
                //    Obj_DAOUsuario.ConexionLocal();
                //    return Obj_DAOUsuario.autenticar(strCuenta);
                //}
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
                if (ACS_Property.BConexion)
                    return Obj_DAOUsuario.listar();
                //if (ACS_Property.BConLocal)
                //{
                //    Obj_DAOUsuario.ConexionLocal();
                //    return Obj_DAOUsuario.listar();

                //}
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        #endregion RegUsuario



        public bool IsDBConnectionError(Exception ex)
        {

            if (ex.GetType().Name == "SqlException")
            {
                SqlException objSqlEx = (SqlException)ex;
                return objSqlEx.ErrorCode == -2146232060 ? true : false;

            }
            return false;
        }

    }
}
