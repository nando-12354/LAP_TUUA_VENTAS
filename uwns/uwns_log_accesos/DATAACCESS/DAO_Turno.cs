using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using LAP.TUUA.ENTIDADES;
using System.Collections;

namespace LAP.TUUA.DAO
{
    public class DAO_Turno : DAO_BaseDatos  
    {
        #region Fields  
        public List<Turno> objListaTurno;

        #endregion

        #region Constructors

        public DAO_Turno(string sConfigSPPath)
            : base(sConfigSPPath)
        {
            objListaTurno = new List<Turno>();

        }
        #endregion

        #region Methods

        /// <summary>
        /// Saves a record to the DAOTurno table.
        /// </summary>
        public bool crear(string as_cod_secuencial, string as_cod_usuario, string as_cod_equipo)
        {
            try
            {
                Hashtable htInsertUSP = (Hashtable)htSPConfig["TUR_CREATE"];
                string sNombreSP = (string)htInsertUSP["TUR_CREATE"];


                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                helper.AddInParameter(objCommandWrapper, (string)htInsertUSP["Cod_Secuencial"], DbType.String, as_cod_secuencial);
                helper.AddInParameter(objCommandWrapper, (string)htInsertUSP["Cod_Usuario"], DbType.String, as_cod_usuario);
                helper.AddInParameter(objCommandWrapper, (string)htInsertUSP["Cod_Equipo"], DbType.String, as_cod_equipo);

                isRegistrado = base.ejecutarTrxSP(objCommandWrapper);
                return isRegistrado;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool insertar(Turno objTurno)
        {

            try
            {
                Hashtable hsInsertUSP = (Hashtable)htSPConfig["USP_INSERT"];
                string sNombreSP = (string)hsInsertUSP["USP_INSERT"];


                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Turno"], DbType.String, objTurno.SCodTurno);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Fch_Inicio"], DbType.DateTime, objTurno.DtFchInicio);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Fch_Fin"], DbType.DateTime, objTurno.DtFchFin);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Usuario_Cierre"], DbType.String, objTurno.SCodUsuarioCierre);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Usuario"], DbType.String, objTurno.SCodUsuario);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Equipo"], DbType.String, objTurno.SCodEquipo);

                isRegistrado = base.mantenerSP(objCommandWrapper);
                return isRegistrado;
            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// Updates a record in the DAOTurno table.
        /// </summary>
        public bool actualizar(Turno objTurno)
        {

            try
            {
                Hashtable hsUpdateUSP = (Hashtable)htSPConfig["TURSP_UPDATE"];
                string sNombreSP = (string)hsUpdateUSP["TURSP_UPDATE"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);

                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Cod_Turno"], DbType.String, objTurno.SCodTurno);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Cod_Usuario_Cierre"], DbType.String, objTurno.SCodUsuarioCierre);
                isActualizado = base.mantenerSP(objCommandWrapper);
                return isActualizado;
            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// Deletes a record from the DAOTurno table by its primary key.
        /// </summary>
        public bool eliminar(string sCodTurno)
        {

            try
            {

                Hashtable hsDeleteUSP = (Hashtable)htSPConfig["TURSP_DELETE"];
                string sNombreSP = (string)hsDeleteUSP["TURSP_DELETE"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);

                helper.AddInParameter(objCommandWrapper, (string)hsDeleteUSP["Cod_Turno"], DbType.String, sCodTurno);


                isActualizado = base.mantenerSP(objCommandWrapper);
                return isActualizado;
            }
            catch (Exception)
            {

                throw;
            }

        }


        /// <summary>
        /// Lista los turno deacuerdo al filtro por fechas, usuario y pto de venta.
        /// </summary>
        /// <param name="sFchIni"></param>
        /// <param name="sFchFin"></param>
        /// <param name="sCodUsuario"></param>
        /// <param name="sPtoVenta"></param>
        /// <returns></returns>
        public DataTable consultarTurnoxFiltro(string sFchIni, string sFchFin, string sCodUsuario, string sPtoVenta, string sHoraDesde, string sHoraHasta)
        {
            DataTable result;
            try
            {
                Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["TURSP_LISTARTURNOFILTRO"];
                string sNombreSP = (string)hsSelectByIdUSP["TURSP_LISTARTURNOFILTRO"];
                result = base.ListarDataTableSP(sNombreSP, sFchIni, sFchFin, sCodUsuario, sPtoVenta, sHoraDesde, sHoraHasta);
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DataTable listarAllTurno(string sCodTurno)
        {
            DataTable result;
            Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["TURSP_LISTARALLTURNO"];
            string sNombreSP = (string)hsSelectByIdUSP["TURSP_LISTARALLTURNO"];
            result = base.ListarDataTableSP(sNombreSP, sCodTurno);
            return result;
        }

        /// <summary>
        /// Define la cantidad de monedas para un turno
        /// </summary>
        public DataTable cantidadmonedaxTurno(string sCodTurno)
        {
            DataTable result;
            try
            {
                Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["TURNONUMMDA_SELECT"];
                string sNombreSP = (string)hsSelectByIdUSP["TURNONUMMDA_SELECT"];
                result = base.ListarDataTableSP(sNombreSP, sCodTurno);
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Define el detallado de operaciones por turno/moneda
        /// </summary>
        /// <param name="sCodTurno"></param>
        /// <param name="sMoneda"></param>
        /// <returns></returns>
        public DataTable detallemonedaxTurno(string sCodTurno, string sMoneda, string sIdDetalle)
        {
            DataTable result;
            Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["TURNODETALLADONUMMDA_SELECT"];
            string sNombreSP = (string)hsSelectByIdUSP["TURNODETALLADONUMMDA_SELECT"];
            result = base.ListarDataTableSP(sNombreSP, sCodTurno, sMoneda, sIdDetalle);


            return result;
        }


        /// <summary>
        /// Selects a single record from the DAOTurno table.
        /// </summary>
        public Turno obtener(string sCodTurno)
        {
            Turno objTurno = null;
            IDataReader result;
            Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["USP_OBTENER_TURNO"];
            string sNombreSP = (string)hsSelectByIdUSP["USP_OBTENER_TURNO"];
            result = base.listarDataReaderSP(sNombreSP, sCodTurno);

            while (result.Read())
            {
                objTurno = poblar(result);

            }
            
            result.Close();
            return objTurno;
        }

        public Turno obtenerTurnoIniciado(string as_usuario)
        {
            Turno objTurno = null;
            IDataReader result;
            try
            {
                Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["TURSP_INICIADO"];
                string sNombreSP = (string)hsSelectByIdUSP["TURSP_INICIADO"];
                result = base.listarDataReaderSP(sNombreSP, as_usuario);

                while (result.Read())
                {
                    objTurno = poblar(result);

                }
                
                result.Close();
                return objTurno;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int selectCountTurnoAbiertoxUsuario(string as_usuario)
        {
            IDataReader result;
            try
            {
                Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["TURSP_SELECTCOUNTUSER"];
                string sNombreSP = (string)hsSelectByIdUSP["TURSP_SELECTCOUNTUSER"];
                int iResult = 0;
                result = base.listarDataReaderSP(sNombreSP, as_usuario);
                while (result.Read())
                {
                    iResult = (Int32)result[(string)hsSelectByIdUSP["cant_turnos"]];
                }
                result.Close();
                return iResult;
            }catch (Exception)
            {

                throw;
            }
        }

        public int selectCountTurnoAbiertoxPtoVenta(string as_ptoventa)
        {
            IDataReader result;
            try
            {
                Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["TURSP_SELECTCOUNTPTOVTA"];
                string sNombreSP = (string)hsSelectByIdUSP["TURSP_SELECTCOUNTPTOVTA"];
                int iResult = 0;
                result = base.listarDataReaderSP(sNombreSP, as_ptoventa);
                while (result.Read())
                {
                    iResult = (Int32)result[(string)hsSelectByIdUSP["cant_turnos"]];
                }
                result.Close();
                return iResult;
            }
            catch (Exception)
            {

                throw;
            }
        }







        /// <summary>
        /// Selects all records from the DAOTurno table.
        /// </summary>
        public DataTable ListarTurnosAbiertos()
        {
            DataTable result;
            Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["TURNOSP_SELECALLABIERTO"];
            string sNombreSP = (string)hsSelectByIdUSP["TURNOSP_SELECALLABIERTO"];
            result = base.ListarDataTableSP(sNombreSP,null);
            return result;
        }

        /// <summary>
        /// Creates a new instance of the DAOTurno class and populates it with data from the specified SqlDataReader.
        /// </summary>
        public Turno poblar(IDataReader dataReader)
        {

            Hashtable htSelectAllUSP = (Hashtable)htSPConfig["TURSP_SELECTALL"];
            Turno objTurno = new Turno();
            if (dataReader.FieldCount == 0)
            {
                return null;
            }
            try
            {
                if (dataReader[(string)htSelectAllUSP["Cod_Turno"]] != DBNull.Value)
                    objTurno.SCodTurno = ((string)dataReader[(string)htSelectAllUSP["Cod_Turno"]]).Trim();
                if (dataReader[(string)htSelectAllUSP["Fch_Inicio"]] != DBNull.Value)
                    objTurno.DtFchInicio = (string)dataReader[(string)htSelectAllUSP["Fch_Inicio"]];
                if (dataReader[(string)htSelectAllUSP["Hor_Inicio"]] != DBNull.Value)
                    objTurno.SHoraInicio = (string)dataReader[(string)htSelectAllUSP["Hor_Inicio"]];
                if (!Convert.IsDBNull(dataReader[(string)htSelectAllUSP["Fch_Fin"]]))
                    objTurno.DtFchFin = (string)dataReader[(string)htSelectAllUSP["Fch_Fin"]];
                objTurno.SHoraFin = !Convert.IsDBNull(dataReader[(string)htSelectAllUSP["Hor_Fin"]]) ? (string)dataReader[(string)htSelectAllUSP["Hor_Fin"]] : "";
                objTurno.SCodUsuarioCierre = !Convert.IsDBNull(dataReader[(string)htSelectAllUSP["Cod_Usuario_Cierre"]]) ? (string)dataReader[(string)htSelectAllUSP["Cod_Usuario_Cierre"]] : "";
                if (dataReader[(string)htSelectAllUSP["Cod_Usuario"]] != DBNull.Value)
                    objTurno.SCodUsuario = ((string)dataReader[(string)htSelectAllUSP["Cod_Usuario"]]).Trim();
                if (dataReader[(string)htSelectAllUSP["Cod_Equipo"]] != DBNull.Value)
                    objTurno.SCodEquipo = (string)dataReader[(string)htSelectAllUSP["Cod_Equipo"]];
            }
            catch (Exception e)
            {
                return null;
            }
            return objTurno;
        }

        #endregion
    }
}
