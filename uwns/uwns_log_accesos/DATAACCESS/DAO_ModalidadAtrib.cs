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
    public class DAO_ModalidadAtrib : DAO_BaseDatos
    {
        #region Fields

          public List<ModalidadAtrib> objListaModalidadAtrib;

        #endregion

        #region Constructors

        public DAO_ModalidadAtrib(string htSPConfig)
            : base(htSPConfig)
        {
              objListaModalidadAtrib = new List<ModalidadAtrib>();
            
        }

        #endregion

        #region Methods

        /// <summary>
        /// Saves a record to the TUA_ModalidadAtrib table.
        /// </summary>
        public bool insertar(ModalidadAtrib objModAtrib)
        {
            try
            {
                Hashtable htInsertUSP = (Hashtable)htSPConfig["MODATSP_INSERT"];
                string sNombreSP = (string)htInsertUSP["MODATSP_INSERT"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                helper.AddInParameter(objCommandWrapper, (string)htInsertUSP["Cod_Modalidad_Venta"], DbType.String, objModAtrib.SCodModalidadVenta);
                helper.AddInParameter(objCommandWrapper, (string)htInsertUSP["Cod_Atributo"], DbType.String, objModAtrib.SCodAtributo);
                helper.AddInParameter(objCommandWrapper, (string)htInsertUSP["Tip_Atributo"], DbType.String, objModAtrib.Tip_Atributo);
                helper.AddInParameter(objCommandWrapper, (string)htInsertUSP["Dsc_Valor"], DbType.String, objModAtrib.SDscValor);
                helper.AddInParameter(objCommandWrapper, (string)htInsertUSP["Cod_Tipo_Ticket"], DbType.String, objModAtrib.SCodTipoTicket);
                helper.AddInParameter(objCommandWrapper, (string)htInsertUSP["Log_Usuario_Mod"], DbType.String, objModAtrib.SLogUsuarioMod);
                isRegistrado = base.mantenerSP(objCommandWrapper);
                return isRegistrado;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Updates a record in the TUA_ModalidadAtrib table.
        /// </summary>
        public bool actualizar(ModalidadAtrib objModAtrib)
        {
            try
            {
                Hashtable htUpdateUSP = (Hashtable)htSPConfig["MODATSP_UPDATE"];
                string sNombreSP = (string)htUpdateUSP["MODATSP_UPDATE"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                helper.AddInParameter(objCommandWrapper, (string)htUpdateUSP["Cod_Modalidad_Venta"], DbType.String, objModAtrib.SCodModalidadVenta);
                helper.AddInParameter(objCommandWrapper, (string)htUpdateUSP["Cod_Atributo"], DbType.String, objModAtrib.SCodAtributo);
                helper.AddInParameter(objCommandWrapper, (string)htUpdateUSP["Dsc_Valor"], DbType.String, objModAtrib.SDscValor);
                helper.AddInParameter(objCommandWrapper, (string)htUpdateUSP["Cod_Tipo_Ticket"], DbType.String, objModAtrib.SCodTipoTicket);
                helper.AddInParameter(objCommandWrapper, (string)htUpdateUSP["Log_Usuario_Mod"], DbType.String, objModAtrib.SLogUsuarioMod);
                isActualizado = base.mantenerSP(objCommandWrapper);
                return isActualizado;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Deletes a record from the DAOModalidadAtrib table by its primary key.
        /// </summary>
        public bool eliminar(string sCodModalidadVenta, string sCodAtributo, string sCodTipoTicket)
        {
            try
            {
                Hashtable hsDeleteUSP = (Hashtable)htSPConfig["MODATSP_DELETE"];
                string sNombreSP = (string)hsDeleteUSP["MODATSP_DELETE"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                {
                    helper.AddInParameter(objCommandWrapper, (string)hsDeleteUSP["Cod_Modalidad_Venta"], DbType.String, sCodModalidadVenta);
                    helper.AddInParameter(objCommandWrapper, (string)hsDeleteUSP["Cod_Atributo"], DbType.String, sCodAtributo);
                    helper.AddInParameter(objCommandWrapper, (string)hsDeleteUSP["Cod_Tipo_Ticket"], DbType.String, sCodTipoTicket);
                };

                isActualizado = base.mantenerSP(objCommandWrapper);
                return isActualizado;

            }
            catch (Exception)
            {

                throw;
            }

        }

        public List<ModalidadAtrib> ListarAtributosxModVenta(string strCodModVenta)
        {
            List<ModalidadAtrib> lista = new List<ModalidadAtrib>();
            IDataReader result;
            try
            {
                Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["MODVTAATR_SELECBYMODVENTA"];
                string sNombreSP = (string)hsSelectByIdUSP["MODVTAATR_SELECBYMODVENTA"];
                result = base.listarDataReaderSP(sNombreSP, strCodModVenta);

                while (result.Read())
                {
                    lista.Add(poblar(result));
                }
                
                result.Close();
                return lista;
            }
            catch
            {
                throw;
            }
        }


        public List<ModalidadAtrib> Listar()
        {
              IDataReader result;
              try
              {
                    Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["MASP_SELECT"];
                    string sNombreSP = (string)hsSelectByIdUSP["MASP_SELECT"];
                    result = base.listarDataReaderSP(sNombreSP,null);

                    while (result.Read())
                    {
                          objListaModalidadAtrib.Add(poblarall(result));
                    }
                    
                    result.Close();
                    return objListaModalidadAtrib;
              }
              catch
              {
                    throw;
              }
        }

        protected ModalidadAtrib poblarall(IDataReader dataReader)
        {
              Hashtable htSelectAllUSP = (Hashtable)htSPConfig["MASP_SELECT"];
              ModalidadAtrib objModVentaAtr = new ModalidadAtrib();

              if (dataReader[(string)htSelectAllUSP["Cod_Atributo"]] != DBNull.Value)
              {
                    objModVentaAtr.SCodAtributo = (string)dataReader[(string)htSelectAllUSP["Cod_Atributo"]];
              }
              if (dataReader[(string)htSelectAllUSP["Cod_Modalidad_Venta"]] != DBNull.Value)
              {
                    objModVentaAtr.SCodModalidadVenta = (string)dataReader[(string)htSelectAllUSP["Cod_Modalidad_Venta"]];
              }
              if (dataReader[(string)htSelectAllUSP["Cod_Tipo_Ticket"]] != DBNull.Value)
              {
                    objModVentaAtr.SCodTipoTicket = (string)dataReader[(string)htSelectAllUSP["Cod_Tipo_Ticket"]];
              }
              if (dataReader[(string)htSelectAllUSP["Dsc_Valor"]] != DBNull.Value)
              {
                    objModVentaAtr.SDscValor = (string)dataReader[(string)htSelectAllUSP["Dsc_Valor"]];
              }
              if (dataReader[(string)htSelectAllUSP["Tip_Atributo"]] != DBNull.Value)
              {
                    objModVentaAtr.Tip_Atributo = (string)dataReader[(string)htSelectAllUSP["Tip_Atributo"]];
              }
              return objModVentaAtr;
        } 


        public List<ModalidadAtrib> ListarAtributosxModVentaTipoTicket(string strCodModVenta,string strTipoTicket)
        {
            List<ModalidadAtrib> lista = new List<ModalidadAtrib>();
            IDataReader result;
            try
            {
                Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["MODVTAATR_SELECBYMODVENTA_TT"];
                string sNombreSP = (string)hsSelectByIdUSP["MODVTAATR_SELECBYMODVENTA_TT"];
                result = base.listarDataReaderSP(sNombreSP, strCodModVenta);

                while (result.Read())
                {
                    lista.Add(poblar(result));
                }
                
                result.Close();
                return lista;
            }
            catch
            {
                throw;
            }
        }



        public List<ModalidadAtrib> ListarAtributosxModVentaCompania(string strCodModVenta,string strCompania)
        {
            List<ModalidadAtrib> lista = new List<ModalidadAtrib>();
            IDataReader result;
            Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["MODVTAATR_SELECBYMODVENTACOMP"];
            string sNombreSP = (string)hsSelectByIdUSP["MODVTAATR_SELECBYMODVENTACOMP"];
            result = base.listarDataReaderSP(sNombreSP, strCodModVenta, strCompania);

            while (result.Read())
            {
                lista.Add(poblar(result));
            }
            
            result.Close();
            return lista;
        }

        //NO MODIFICAR ESTE METODO PARA NADA
        protected ModalidadAtrib poblar(IDataReader dataReader)
        {
            Hashtable htSelectAllUSP = (Hashtable)htSPConfig["MODVENTAATR_SELECTALL"];
            ModalidadAtrib objModVentaAtr = new ModalidadAtrib();
            
            objModVentaAtr.SCodAtributo = (string)dataReader[(string)htSelectAllUSP["Cod_Atributo"]];

            objModVentaAtr.SCodModalidadVenta = (string)dataReader[(string)htSelectAllUSP["Cod_Modalidad_Venta"]];
            if (dataReader[(string)htSelectAllUSP["Cod_Tipo_Ticket"]] != DBNull.Value)
            {
                objModVentaAtr.SCodTipoTicket = (string)dataReader[(string)htSelectAllUSP["Cod_Tipo_Ticket"]];
            }
            if (dataReader[(string)htSelectAllUSP["Dsc_Valor"]] != DBNull.Value)
            {
                objModVentaAtr.SDscValor = (string)dataReader[(string)htSelectAllUSP["Dsc_Valor"]];
            }
            if (dataReader[(string)htSelectAllUSP["Tip_Atributo"]] != DBNull.Value)
            {
                objModVentaAtr.Tip_Atributo = (string)dataReader[(string)htSelectAllUSP["Tip_Atributo"]];
            }
            return objModVentaAtr;
        }

        #endregion
    }
}
