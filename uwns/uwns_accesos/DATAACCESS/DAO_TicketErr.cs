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
      public class DAO_TicketErr : DAO_BaseDatos
      {
            #region Fields

            public List<TicketErr> objListaTicketErr;

            #endregion

            #region Constructors

            public DAO_TicketErr(string sConfigSPPath)
            : base(sConfigSPPath)
            {
                  objListaTicketErr = new List<TicketErr>();
            }
            public DAO_TicketErr(Database vhelper, Database vhelperLocal, Hashtable vhtSPConfig)
                  : base(vhelper, vhelperLocal, vhtSPConfig)
            {
                  objListaTicketErr = new List<TicketErr>();
            }

            #endregion

            #region Methods
            /// <summary>
            /// Saves a record to the DAOTicket table.
            /// </summary>
            public bool insertar(TicketErr objTicketErr)
            {

                  try
                  {
                        Hashtable hsInsertUSP = (Hashtable)htSPConfig["TICKETERROR_INSERT"];
                        string sNombreSP = (string)hsInsertUSP["TICKETERROR_INSERT"];

                        DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);

                        helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Numero_Ticket"], DbType.String, objTicketErr.SCodNumeroTicket);
                        helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Molinete"], DbType.String, objTicketErr.SCodMolinete);
                        helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Tipo_Ticket"], DbType.String, objTicketErr.SCodTipoTicket);
                        helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Dsc_Error"], DbType.String, objTicketErr.SDscError);
                        helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Tip_Ingreso"], DbType.String, objTicketErr.STipIngreso);
                        helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Tip_Error"], DbType.String, objTicketErr.STipError);
                        helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Log_Usuario_Mod"], DbType.String, objTicketErr.SLogUsuarioMod);
                        helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Log_Fecha_Mod"], DbType.String, objTicketErr.SLogFechaMod);
                        helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Log_Hora_Mod"], DbType.String, objTicketErr.SLogHoraMod);

                        isRegistrado = base.mantenerSP(objCommandWrapper);
                       
                        return isRegistrado;
                  }
                  catch (Exception)
                  {
                        throw;
                  }
            }
            #endregion

      }
}
