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
    public class DAO_Auditoria : DAO_BaseDatos
    {
        #region Fields

        public List<Auditoria> objListaAuditoria;

        #endregion

        #region Constructors

        public DAO_Auditoria(string htSPConfig)
            : base(htSPConfig)
        {
            objListaAuditoria = new List<Auditoria>();
        }

        public DAO_Auditoria(Database vhelper, Database vhelperLocal, Hashtable vhtSPConfig)
                 : base(vhelper, vhelperLocal, vhtSPConfig)
        {
              objListaAuditoria = new List<Auditoria>();
        }

        #endregion

        #region Methods


        /// <summary>
        /// Saves a record to the DAOAuditoria table.
        /// </summary>

        public bool insertar(Auditoria objAuditoria)
        {
            try
            {
                Hashtable hsInsertUSP = (Hashtable)htSPConfig["USP_INSERT"];
                string sNombreSP = (string)hsInsertUSP["USP_INSERT"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Contador"], DbType.Int32, objAuditoria.ICodContador);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Modulo"], DbType.String, objAuditoria.SCodModulo);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_SubModulo"], DbType.String, objAuditoria.SCodSubModulo);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Fch_Registro"], DbType.DateTime, objAuditoria.DtFchRegistro);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Rol"], DbType.String, objAuditoria.SCodRol);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Tip_Operacion"], DbType.String, objAuditoria.STipOperacion);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Log_Reg_Orig"], DbType.String, objAuditoria.SLogRegOrig);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Log_Reg_Nuevo"], DbType.String, objAuditoria.SLogRegNuevo);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Log_Tabla_Mod"], DbType.String, objAuditoria.SLogTablaMod);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Log_Colum_Mod"], DbType.String, objAuditoria.SLogColumMod);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Log_Hora_Mod"], DbType.String, objAuditoria.SLogHoraMod);
                isRegistrado = base.mantenerSP(objCommandWrapper);
                return isRegistrado;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public bool insertar(string strCodModulo, string strCodSubModulo,string strCodUsuario, string strTipOperacion)
        {
              try
              {
                    Hashtable hsInsertUSP = (Hashtable)htSPConfig["USP_AUDIT_INSERT"];
                    string sNombreSP = (string)hsInsertUSP["USP_AUDIT_INSERT"];

                    DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                    helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Modulo_Mod"], DbType.String, strCodModulo);
                    helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_SubModulo_Mod"], DbType.String, strCodSubModulo);
                    helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Log_Usuario_Mod"], DbType.String, strCodUsuario);
                    helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Tip_Operacion"], DbType.String, strTipOperacion);
                    isRegistrado = base.mantenerSP(objCommandWrapper);
                    return isRegistrado;
              }
              catch (Exception)
              {
                    throw;
              }
        }

        public bool insertar_habmolinete(string Cod_Acceso, string Cod_Web, string Cod_Molinete)
        {
              try
              {
                    Hashtable hsInsertUSP = (Hashtable)htSPConfig["HABMOLINETE_INSERT"];
                    string sNombreSP = (string)hsInsertUSP["HABMOLINETE_INSERT"];

                    DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                    helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Acceso"], DbType.String, Cod_Acceso);
                    helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Web"], DbType.String, Cod_Web);
                    helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Molinete"], DbType.String, Cod_Molinete);
                    isRegistrado = base.mantenerSPSinAuditoria(objCommandWrapper);
                    return isRegistrado;
              }
              catch (Exception)
              {
                    throw;
              }
        }


        /// <summary>
        /// Updates a record in the DAOAuditoria table.
        /// </summary>

        public bool actualizar(Auditoria objAuditoria)
        {
            try
            {
                Hashtable hsUpdateUSP = (Hashtable)htSPConfig["USP_UPDATE"];
                string sNombreSP = (string)hsUpdateUSP["USP_UPDATE"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Cod_Contador"], DbType.Int32, objAuditoria.ICodContador);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Cod_Modulo"], DbType.String, objAuditoria.SCodModulo);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Cod_SubModulo"], DbType.String, objAuditoria.SCodSubModulo);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Fch_Registro"], DbType.String, objAuditoria.DtFchRegistro);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Cod_Rol"], DbType.String, objAuditoria.SCodRol);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Tip_Operacion"], DbType.String, objAuditoria.STipOperacion);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Log_Reg_Orig"], DbType.String, objAuditoria.SLogRegOrig);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Log_Reg_Nuevo"], DbType.String, objAuditoria.SLogRegNuevo);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Log_Tabla_Mod"], DbType.String, objAuditoria.SLogTablaMod);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Log_Colum_Mod"], DbType.String, objAuditoria.SLogColumMod);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Log_Hora_Mod"], DbType.String, objAuditoria.SLogHoraMod);
                isActualizado = base.mantenerSP(objCommandWrapper);
                return isActualizado;
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Deletes a record from the DAOAuditoria table by its primary key.
        /// </summary>

        public bool eliminar(string sCodContador)
        {
            try
            {
                Hashtable htDeleteUSP = (Hashtable)htSPConfig["USP_DELETE"];
                string sNombreSP = (string)htDeleteUSP["USP_DELETE"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);

                helper.AddInParameter(objCommandWrapper, (string)htDeleteUSP["Cod_Contador"], DbType.String, sCodContador);


                isActualizado = base.mantenerSP(objCommandWrapper);
                return isActualizado;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }




        /// <summary>
        /// Selects a single record from the DAOAuditoria table.
        /// </summary>

        public List<Auditoria> listar()
        {
            IDataReader objResult;
            Hashtable hsSelectAllUSP = (Hashtable)htSPConfig["USP_SELECTALL"];
            string sNombreSP = (string)hsSelectAllUSP["USP_SELECTALL"];
            objResult = base.listarDataReaderSP(sNombreSP, null);

            while (objResult.Read())
            {
                objListaAuditoria.Add(poblar(objResult));

            }
 
            objResult.Close();
            return objListaAuditoria;
        }



        /// <summary>
        /// Selects all records from the DAOAuditoria table.
        /// </summary>

        public Auditoria obtener(string sCodAuditoria)
        {

            Auditoria objAuditoria = null;
            IDataReader result;
            Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["USP_OBTENER_AUDITORIA"];
            string sNombreSP = (string)hsSelectByIdUSP["USP_OBTENER_AUDITORIA"];
            result = base.listarDataReaderSP(sNombreSP, sCodAuditoria);

            while (result.Read())
            {
                objAuditoria = poblar(result);

            }

            result.Close();
            return objAuditoria;
        }



        /// <summary>
        /// Creates a new instance of the DAOAuditoria class and populates it with data from the specified SqlDataReader.
        /// </summary>

        public Auditoria poblar(IDataReader dataReader)
        {
            Hashtable htSelectAllUSP = (Hashtable)htSPConfig["USP_SELECTALL"];
            Auditoria objAuditoria = new Auditoria();
            objAuditoria.ICodContador = (Int32)dataReader[(Int32)htSelectAllUSP["Cod_Contador"]];
            objAuditoria.SCodModulo = (string)dataReader[(string)htSelectAllUSP["Cod_Modulo"]];
            objAuditoria.SCodSubModulo = (string)dataReader[(string)htSelectAllUSP["Cod_SubModulo"]];
            objAuditoria.DtFchRegistro = Convert.ToDateTime(dataReader[(string)htSelectAllUSP["Fch_Registro"]]);
            objAuditoria.SCodRol = (string)dataReader[(string)htSelectAllUSP["Cod_Rol"]];
            objAuditoria.STipOperacion = (string)dataReader[(string)htSelectAllUSP["Tip_Operacion"]];
            objAuditoria.SLogRegOrig = (string)dataReader[(string)htSelectAllUSP["Log_Reg_Orig"]];
            objAuditoria.SLogRegNuevo = (string)dataReader[(string)htSelectAllUSP["Log_Reg_Nuevo"]];
            objAuditoria.SLogTablaMod = (string)dataReader[(string)htSelectAllUSP["Log_Tabla_Mod"]];
            objAuditoria.SLogColumMod = (string)dataReader[(string)htSelectAllUSP["Log_Colum_Mod"]];
            objAuditoria.SLogHoraMod = (string)dataReader[(string)htSelectAllUSP["Log_Hora_Mod"]];
            return objAuditoria;
        }


        public DataTable FiltrosAuditoria(string strCodModulo, string strFlgSubModulo, string strTablaXml)
        {
            DataTable result;
            Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["CSSP_FILTROAUDITORIA_SEL"];
            string sNombreSP = (string)hsSelectByIdUSP["CSSP_FILTROAUDITORIA_SEL"];
            result = base.ListarDataTableSP(sNombreSP, strCodModulo, strFlgSubModulo, strTablaXml);
            return result;
        }


        public DataTable obtenerconsultaAuditoria(string strTipOperacion, string strTabla, string strCodModulo, string strCodSubModulo,
                                                string strCodUsuario, string strFchDesde, string strFchHasta, string strHraDesde,
                                                string strHraHasta)
        {
            DataTable result;
            Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["CSSP_OBTENERCONSULTAAUDITORIA_SEL"];
            string sNombreSP = (string)hsSelectByIdUSP["CSSP_OBTENERCONSULTAAUDITORIA_SEL"];
            result = base.ListarDataTableSP(sNombreSP, strTipOperacion, strTabla, strCodModulo,strCodSubModulo,strCodUsuario,strFchDesde,strFchHasta,strHraDesde,strHraHasta);
            return result;
        }


        public DataTable obtenerdetalleAuditoria(string strNombreTabla, string strContador)
        {
            DataTable result;
            Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["CSSP_OBTENERDETALLEAUDITORIA_SEL"];
            string sNombreSP = (string)hsSelectByIdUSP["CSSP_OBTENERDETALLEAUDITORIA_SEL"];
            result = base.ListarDataTableSP(sNombreSP, strNombreTabla, strContador);
            return result;
        }


        #endregion
    }
}
