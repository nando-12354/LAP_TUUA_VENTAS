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
      public class DAO_TasaCambio : DAO_BaseDatos
      {
            #region Fields

            public List<TasaCambio> objListaTasaCambio;

            #endregion

            #region Constructors

            public DAO_TasaCambio(string sConfigSPPathg)
                  : base(sConfigSPPathg)
            {
                  objListaTasaCambio = new List<TasaCambio>();
            }
            #endregion

            #region Methods

            public DataTable ListarTasaCambio()
            {
                try
                {
                    DataTable result = null;
                    Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["TASASP_SELECTALL"];
                    string sNombreSP = (string)hsSelectByIdUSP["TASASP_SELECTALL"];
                    result = base.ListarDataTableSP(sNombreSP,null);
                    return result;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            /// <summary>
            /// Selects a single record from the DAOTasaCambio table.
            /// </summary>
            public TasaCambio ObtenerUltimoPorMoneda(string strMoneda, string strTipo)
            {
                  TasaCambio objTasaCambio = null;
                  IDataReader result;
                  try
                  {
                        Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["TCSP_SELECTBYMONEDA"];
                        string sNombreSP = (string)hsSelectByIdUSP["TCSP_SELECTBYMONEDA"];
                        result = base.listarDataReaderSP(sNombreSP, strMoneda, strTipo);

                        while (result.Read())
                        {
                              objTasaCambio = poblar(result);

                        }
                        result.Dispose();
                        result.Close();
                        return objTasaCambio;
                  }
                  catch (Exception)
                  {
                        throw;
                  }
            }

            /// <summary>
            /// Selects all records from the DAOTasaCambio table.
            /// </summary>
            public List<TasaCambio> listar()
            {
                  IDataReader objResult;
                  Hashtable hsSelectAllUSP = (Hashtable)htSPConfig["USP_SELECTALL"];
                  string sNombreSP = (string)hsSelectAllUSP["USP_SELECTALL"];
                  objResult = base.listarDataReaderSP(sNombreSP, null);

                  while (objResult.Read())
                  {
                        objListaTasaCambio.Add(poblar(objResult));

                  }
                  objResult.Dispose();
                  objResult.Close();
                  return objListaTasaCambio;
            }


            /// <summary>
            /// Creates a new instance of the DAOTasaCambio class and populates it with data from the specified SqlDataReader.
            /// </summary>
            public TasaCambio poblar(IDataReader dataReader)
            {
                  Hashtable htSelectAllUSP = (Hashtable)htSPConfig["TCSP_SELECTALL"];
                  if (dataReader.FieldCount == 0)
                  {
                        return null;
                  }
                  TasaCambio objTasaCambio = new TasaCambio();
                  if (dataReader[(string)htSelectAllUSP["Imp_Cambio_Actual"]] != DBNull.Value)
                        objTasaCambio.DImpCambioActual = decimal.Parse(dataReader[(string)htSelectAllUSP["Imp_Cambio_Actual"]].ToString());
                  if (dataReader[(string)htSelectAllUSP["Cod_Moneda"]] != DBNull.Value)
                        objTasaCambio.SCodMoneda = (string)dataReader[(string)htSelectAllUSP["Cod_Moneda"]];
                  if (dataReader[(string)htSelectAllUSP["Tip_Cambio"]] != DBNull.Value)
                        objTasaCambio.STipCambio = (string)dataReader[(string)htSelectAllUSP["Tip_Cambio"]];
                  if (dataReader[(string)htSelectAllUSP["Fch_Proceso"]] != DBNull.Value)
                        objTasaCambio.DtFchProceso = Convert.ToDateTime(dataReader[(string)htSelectAllUSP["Fch_Proceso"]]);
                  if (dataReader[(string)htSelectAllUSP["Log_Usuario_Mod"]] != DBNull.Value)
                        objTasaCambio.SLogUsuarioMod = (string)dataReader[(string)htSelectAllUSP["Log_Usuario_Mod"]];

                  return objTasaCambio;
            }

            #endregion


            #region Additional Methods - kinzi
            /// <summary>
            /// Saves a new record to the TUA_TasaCambio table.
            /// </summary>
            public bool insertar(TasaCambio objTasaCambio)
            {
                  try
                  {
                        Hashtable hsInsertUSP = (Hashtable)htSPConfig["TC_NEW_SP_INSERT"];
                        string sNombreSP = (string)hsInsertUSP["TC_NEW_SP_INSERT"];

                        DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                        helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Tasa_Cambio"], DbType.String, objTasaCambio.SCodTasaCambio);
                        helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Tip_Cambio"], DbType.String, objTasaCambio.STipCambio);
                        helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Moneda"], DbType.String, objTasaCambio.SCodMoneda);
                        helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Imp_Cambio_Actual"], DbType.Decimal, objTasaCambio.DImpCambioActual);
                        helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Log_Usuario_Mod"], DbType.String, objTasaCambio.SLogUsuarioMod);
                        helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Tip_Ingreso"], DbType.String, objTasaCambio.STipIngreso);
                        helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Tip_Estado"], DbType.String, objTasaCambio.STipEstado);
                        if (objTasaCambio.DtFchProgramacion == DateTime.MinValue)
                        {
                              helper.AddInParameter(objCommandWrapper, "Fch_Prog", DbType.DateTime, null);
                        }
                        else
                        {
                              helper.AddInParameter(objCommandWrapper, "Fch_Prog", DbType.DateTime, objTasaCambio.DtFchProgramacion);
                        }
                        helper.AddOutParameter(objCommandWrapper, (string)hsInsertUSP["Out_Cod_Retorno"], DbType.String, 3);

                        isRegistrado = base.mantenerSP(objCommandWrapper);

                        if (isRegistrado)//Ejecuto el Store Procedure satisfactoriamente
                        {
                              objTasaCambio.SCodRetorno = (string)helper.GetParameterValue(objCommandWrapper, (string)hsInsertUSP["Out_Cod_Retorno"]);
                        }
                        return isRegistrado;
                  }
                  catch (Exception ex)
                  {
                        throw;
                  }
            }

            public bool insertarCrit(TasaCambio objTasaCambio)
            {
                try
                {
                    //Hashtable hsInsertUSP = (Hashtable)htSPConfig["TC_NEW_SP_INSERT"];
                    //string sNombreSP = (string)hsInsertUSP["TC_NEW_SP_INSERT"];

                    DbCommand objCommandWrapper = helper.GetStoredProcCommand("usp_ope_pcs_tasacambio_upd_crit");
                    helper.AddInParameter(objCommandWrapper, "Cod_Tasa_Cambio", DbType.String, objTasaCambio.SCodTasaCambio);
                    helper.AddInParameter(objCommandWrapper, "Tip_Cambio", DbType.String, objTasaCambio.STipCambio);
                    helper.AddInParameter(objCommandWrapper, "Cod_Moneda", DbType.String, objTasaCambio.SCodMoneda);
                    helper.AddInParameter(objCommandWrapper, "Imp_Cambio_Actual", DbType.Decimal, objTasaCambio.DImpCambioActual);
                    helper.AddInParameter(objCommandWrapper, "Log_Usuario_Mod", DbType.String, objTasaCambio.SLogUsuarioMod);
                    helper.AddInParameter(objCommandWrapper, "Tip_Ingreso", DbType.String, objTasaCambio.STipIngreso);
                    helper.AddInParameter(objCommandWrapper, "Tip_Estado", DbType.String, objTasaCambio.STipEstado);
                    if (objTasaCambio.DtFchProgramacion == DateTime.MinValue)
                    {
                        helper.AddInParameter(objCommandWrapper, "Fch_Prog", DbType.DateTime, null);
                    }
                    else
                    {
                        helper.AddInParameter(objCommandWrapper, "Fch_Prog", DbType.DateTime, objTasaCambio.DtFchProgramacion);
                    }
                    helper.AddOutParameter(objCommandWrapper, "Cod_Retorno", DbType.String, 3);

                    helper.AddInParameter(objCommandWrapper, "IdTxCritica", DbType.Int64, objTasaCambio.IdTxCritica);

                    isRegistrado = base.mantenerSP(objCommandWrapper);

                    if (isRegistrado)//Ejecuto el Store Procedure satisfactoriamente
                    {
                        objTasaCambio.SCodRetorno = (string)helper.GetParameterValue(objCommandWrapper, "Cod_Retorno");
                    }
                    return isRegistrado;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }

           // #region esilva adicionales
            /// <summary>
            /// Saves a record to the TUA_TasaCambio table.
            /// </summary>
            public bool insertar2(TasaCambio objTasaCambio)
            {
                  try
                  {
                        Hashtable hsInsertUSP = (Hashtable)htSPConfig["TCSP_INSERT"];
                        string sNombreSP = (string)hsInsertUSP["TCSP_INSERT"];

                        DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                        helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Tasa_Cambio"], DbType.String, objTasaCambio.SCodTasaCambio);
                        helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Tip_Cambio"], DbType.String, objTasaCambio.STipCambio);
                        helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Moneda"], DbType.String, objTasaCambio.SCodMoneda);
                        helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Imp_Cambio_Actual"], DbType.Decimal, objTasaCambio.DImpCambioActual);
                        helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Log_Usuario_Mod"], DbType.String, objTasaCambio.SLogUsuarioMod);

                        helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Tip_Ingreso"], DbType.String, objTasaCambio.STipIngreso);
                        helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Tip_Estado"], DbType.String, objTasaCambio.STipEstado);
                        ////add Tip_Ingreso and Tip_Estado
                        //helper.AddInParameter(objCommandWrapper, "Tip_Ingreso", DbType.String, objTasaCambio.STipIngreso);
                        //helper.AddInParameter(objCommandWrapper, "Tip_Estado", DbType.String, objTasaCambio.STipEstado);

                        if (objTasaCambio.DtFchProgramacion == DateTime.MinValue)
                        {
                              helper.AddInParameter(objCommandWrapper, "Fch_Prog", DbType.DateTime, null);
                        }
                        else
                        {
                              helper.AddInParameter(objCommandWrapper, "Fch_Prog", DbType.DateTime, objTasaCambio.DtFchProgramacion);
                        }

                        isRegistrado = base.mantenerSP(objCommandWrapper);
                        return isRegistrado;

                  }
                  catch (Exception ex)
                  {

                        throw;
                  }
            }


            public int insertarlista(List<TasaCambioHist> Lst_TasaCambioHist, List<TasaCambio> Lst_TasaCambioIns,
                                     List<LogTasaCambio> Lst_TasaCambioLog, string SLogUsuarioMod)
            {
                  int nInsert = 0;
                  DbCommand objCommandWrapper;
                  using (System.Data.Common.DbConnection connection = helper.CreateConnection())
                  {
                        connection.Open();
                        System.Data.Common.DbTransaction transaction = connection.BeginTransaction();

                        try
                        {
                              //Insertar Hist y eliminar tasa cambio
                              Hashtable hsInsertUSP = (Hashtable)htSPConfig["TCHSP_INSERT"];
                              string sNombreSP = (string)hsInsertUSP["TCHSP_INSERT"];
                              foreach (TasaCambioHist obj in Lst_TasaCambioHist)
                              {
                                    objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                                    helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Tasa_Cambio"], DbType.String, obj.SCodTasaCambio);
                                    helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Tip_Cambio"], DbType.String, obj.STipCambio);
                                    helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Moneda"], DbType.String, obj.SCodMoneda);
                                    helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Imp_Valor"], DbType.Decimal, new Decimal(obj.DImpValor));
                                    helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Moneda2"], DbType.String, obj.SCodMoneda2);
                                    helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Imp_Valor2"], DbType.Double, obj.DImpValor2);
                                    helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Fch_Ini"], DbType.DateTime, obj.DtFchIni);
                                    helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Fch_Fin"], DbType.DateTime, obj.DtFchFin);
                                    helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Log_Usuario_Mod"], DbType.String, SLogUsuarioMod);
                                    if (helper.ExecuteNonQuery(objCommandWrapper, transaction) != 0)
                                          nInsert++;

                                    Hashtable hsDeleteUSP = (Hashtable)htSPConfig["TCSP_DELETE"];
                                    string sNombreSPDel = (string)hsDeleteUSP["TCSP_DELETE"];
                                    objCommandWrapper = helper.GetStoredProcCommand(sNombreSPDel);
                                    
                                    helper.AddInParameter(objCommandWrapper, (string)hsDeleteUSP["Cod_Tasa_Cambio"], DbType.String, obj.SCodTasaCambio);
                                    helper.AddInParameter(objCommandWrapper, (string)hsDeleteUSP["Log_Usuario_Mod"], DbType.String, SLogUsuarioMod);
                                    helper.ExecuteNonQuery(objCommandWrapper, transaction);
                              }

                              //Insertar tasa cambio
                              hsInsertUSP = (Hashtable)htSPConfig["TCSP_INSERT"];
                              sNombreSP = (string)hsInsertUSP["TCSP_INSERT"];
                              foreach (TasaCambio obj in Lst_TasaCambioIns)
                              {
                                    objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                                    helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Tasa_Cambio"], DbType.String, obj.SCodTasaCambio);
                                    helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Tip_Cambio"], DbType.String, obj.STipCambio);
                                    helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Moneda"], DbType.String, obj.SCodMoneda);
                                    helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Imp_Cambio_Actual"], DbType.Decimal, obj.DImpCambioActual);
                                    helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Log_Usuario_Mod"], DbType.String, obj.SLogUsuarioMod);
                                    helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Tip_Ingreso"], DbType.String, obj.STipIngreso);
                                    helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Tip_Estado"], DbType.String, obj.STipEstado);

                                    if (obj.DtFchProgramacion == DateTime.MinValue)
                                    {
                                          helper.AddInParameter(objCommandWrapper, "Fch_Prog", DbType.DateTime, null);
                                    }
                                    else
                                    {
                                          helper.AddInParameter(objCommandWrapper, "Fch_Prog", DbType.DateTime, obj.DtFchProgramacion);
                                    }
                                    helper.ExecuteNonQuery(objCommandWrapper, transaction);
                              }


                              //Insertar Log TasaCambio
                              hsInsertUSP = (Hashtable)htSPConfig["LTCSP_INSERT"];
                              sNombreSP = (string)hsInsertUSP["LTCSP_INSERT"];
                              foreach (LogTasaCambio obj in Lst_TasaCambioLog)
                              {
                                    objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                                    helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Moneda"], DbType.String, obj.SCodMoneda);
                                    helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Imp_Compra"], DbType.Decimal, obj.DImpCompra);
                                    helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Imp_Venta"], DbType.Decimal, obj.DImpVenta);
                                    helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Log_Usuario_Mod"], DbType.String, obj.SLogUsuarioMod);
                                    helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Log_Fecha_Mod"], DbType.String, obj.SLogFechaMod);
                                    helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Log_Hora_Mod"], DbType.String, obj.SLogHoraMod);
                                    helper.ExecuteNonQuery(objCommandWrapper, transaction);
                              }

                              transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                              transaction.Rollback();
                              throw ex;
                        }
                        finally
                        {
                              connection.Close();
                        }
                  }
                  return nInsert;
            }
            /// <summary>
            /// Deletes a record from the TUA_TasaCambio table by its primary key.
            /// </summary>
            public bool eliminar(string sCodTasaCambio)
            {
                  try
                  {
                        Hashtable hsDeleteUSP = (Hashtable)htSPConfig["TCSP_DELETE"];
                        string sNombreSP = (string)hsDeleteUSP["TCSP_DELETE"];

                        DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                        helper.AddInParameter(objCommandWrapper, (string)hsDeleteUSP["Cod_Tasa_Cambio"], DbType.String, sCodTasaCambio);

                        isActualizado = base.mantenerSP(objCommandWrapper);
                        return isActualizado;
                  }
                  catch (Exception)
                  {

                        throw;
                  }
            }

            public bool eliminarCrit(TasaCambio oTasaCambio)
            {
                try
                {
                    //Hashtable hsDeleteUSP = (Hashtable)htSPConfig["TCSP_DELETE"];
                    //string sNombreSP = (string)hsDeleteUSP["TCSP_DELETE"];

                    DbCommand objCommandWrapper = helper.GetStoredProcCommand("usp_ope_pcs_tasacambio_del_crit");
                    helper.AddInParameter(objCommandWrapper, "Cod_Tasa_Cambio", DbType.String, oTasaCambio.SCodTasaCambio);
                    helper.AddInParameter(objCommandWrapper, "idTxCritica", DbType.Int64, oTasaCambio.IdTxCritica);

                    isActualizado = base.mantenerSP(objCommandWrapper);
                    return isActualizado;
                }
                catch (Exception)
                {

                    throw;
                }
            }

            /// <summary>
            /// Get records from the TUA_TasaCambio table.
            /// </summary>
            public DataTable obtener(string sCodTasaCambio)
            {
                  DataTable result;

                  //Begin Build Query --------------------
                  String strSQL;
                  String strWhere = "";
                  strSQL = "SELECT tc.Cod_Tasa_Cambio, tc.Tip_Cambio, tc.Cod_Moneda, tc.Imp_Cambio_Actual," +
                               " dbo.fnFormatDate (tc.Fch_Proceso, 'DD/MM/YYYY hh:xx:ss') Fch_Proceso, tc.Log_Usuario_Mod, tc.Log_Fecha_Mod, tc.Log_Hora_Mod," +
                               " tlc.Dsc_Campo, tm.Dsc_Moneda, tm.Dsc_Simbolo, tc.Tip_Estado, tc.Tip_Ingreso," +
                               " dbo.fnFormatDate (tc.Fch_Programacion, 'DD/MM/YYYY hh:xx:ss') Fch_Programacion," +
                               " CASE WHEN (tu.Ape_Usuario IS NULL AND tu.Nom_Usuario IS NULL) THEN tc.Log_Usuario_Mod" +
                               " ELSE ISNULL(tu.Nom_Usuario,' - ') +', '+ ISNULL(tu.Ape_Usuario, ' - ') END Nom_Usuario" +
                           " FROM TUA_TasaCambio tc LEFT JOIN TUA_Usuario tu ON tc.Log_Usuario_Mod = tu.Cod_Usuario, TUA_ListaDeCampos tlc, TUA_Moneda tm" +

                           " WHERE tc.Tip_Cambio = tlc.Cod_Campo" +
                           " AND tlc.Nom_Campo = 'TipoTasaCambio' AND tc.Cod_Moneda = tm.Cod_Moneda";

                  if (sCodTasaCambio != null && sCodTasaCambio.Length > 0)
                  {
                        strWhere = strWhere + " AND tc.Cod_Tasa_Cambio = '" + sCodTasaCambio.Trim() + "'";
                  }
                  strSQL = strSQL + strWhere + " ORDER BY tc.Cod_Tasa_Cambio DESC";
                  //End Build Query --------------------

                  result = base.ListarDataTableQY(strSQL);
                  return result;
            }


            /// <summary>
            /// Get records from the TUA_TasaCambio table.
            /// </summary>
            public DataTable obtener2(string sCodTasaCambio)
            {
                  DataTable result;

                  //Begin Build Query --------------------
                  String strSQL;
                  String strWhere = "";
                  strSQL = "SELECT tc.Cod_Tasa_Cambio, tc.Tip_Cambio, tc.Cod_Moneda, tc.Imp_Cambio_Actual," +
                               "tc.Fch_Proceso, tc.Log_Usuario_Mod, tc.Log_Fecha_Mod, tc.Log_Hora_Mod," +

                               " tlc.Dsc_Campo, tm.Dsc_Moneda, tm.Dsc_Simbolo, tc.Tip_Estado, tc.Tip_Ingreso, tc.Fch_Programacion, tu.Nom_Usuario+', '+tu.Ape_Usuario Nom_Usuario_Mod" +
                           " FROM TUA_TasaCambio tc LEFT JOIN TUA_Usuario tu ON tc.Log_Usuario_Mod = tu.Cod_Usuario, TUA_ListaDeCampos tlc, TUA_Moneda tm" +

                           " WHERE tc.Tip_Cambio = tlc.Cod_Campo" +
                           " AND tlc.Nom_Campo = 'TipoTasaCambio' AND tc.Cod_Moneda = tm.Cod_Moneda";

                  if (sCodTasaCambio != null && sCodTasaCambio.Length > 0)
                  {
                        strWhere = strWhere + " AND tc.Cod_Tasa_Cambio = '" + sCodTasaCambio.Trim() + "'";
                  }
                  strSQL = strSQL + strWhere + " ORDER BY tc.Cod_Tasa_Cambio DESC";
                  //End Build Query --------------------

                  result = base.ListarDataTableQY(strSQL);
                  return result;
            }

            public bool actualizar()
            {
                  try
                  {
                        Hashtable hsUpdateUSP = (Hashtable)htSPConfig["TCSP_UPDATE"];
                        string sNombreSP = (string)hsUpdateUSP["TCSP_UPDATE"];

                        DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                        isActualizado = base.mantenerSP(objCommandWrapper);
                        return isActualizado;
                  }
                  catch (Exception ex)
                  {

                        throw ex;
                  }
            }

            public bool actualizar2(string Log_Usuario_Mod, string Cod_Modulo_Mod, string Cod_SubModulo_Mod)
            {
                  try
                  {
                        Hashtable hsUpdateUSP = (Hashtable)htSPConfig["TCSP_UPDATE"];
                        string sNombreSP = (string)hsUpdateUSP["TCSP_UPDATE"];

                        DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                        helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Log_Usuario_Mod"], DbType.String, Log_Usuario_Mod);
                        helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Cod_Modulo_Mod"], DbType.String, Cod_Modulo_Mod);
                        helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Cod_SubModulo_Mod"], DbType.String, Cod_SubModulo_Mod);

                        isActualizado = base.mantenerSP(objCommandWrapper);
                        return isActualizado;
                  }
                  catch (Exception ex)
                  {

                        throw ex;
                  }
            }

            public DataTable obtenerResumenTasaCambio(string strTipo, string strFecIni, string strHorIni, string strFecFin, string strHorFin, string strTurnoIni, string strTurnoFin)
            {
                  try
                  {
                        DataTable result = null;
                        Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["OPERACIONSP_RESUMENTASACAMBIO"];
                        string sNombreSP = (string)hsSelectByIdUSP["OPERACIONSP_RESUMENTASACAMBIO"];
                        if (strTipo != null && strTipo.Length > 0)
                        {
                              if (strTipo == "1" || strTipo == "3")
                                    result = base.ListarDataTableSP(sNombreSP, strTipo, strFecIni, strHorIni, strFecFin, strHorFin, null, null);
                              else
                                    result = base.ListarDataTableSP(sNombreSP, "2", null, null, null, null, strTurnoIni, strTurnoFin);
                        }
                        return result;
                  }
                  catch (Exception ex)
                  {
                        throw ex;
                  }
            }
            #endregion
      }
}
