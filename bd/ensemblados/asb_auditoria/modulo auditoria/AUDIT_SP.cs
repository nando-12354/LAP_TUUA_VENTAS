using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System.Collections;
using LAP.TUUA.AUDITORIA_CLR;



public partial class AUDIT_SPTabla
{

      [Microsoft.SqlServer.Server.SqlProcedure]

      public static void usp_audit_tabla_sel(string strPK,string strTabla, out string strPista)
      {
            SqlConnection con = new SqlConnection("context connection = true");
            con.Open();

            AUDIT_Tabla obj = new AUDIT_Tabla(strTabla,con);
            obj.GetListaColumnasXML();
            obj.SalvarPista(strPK);
            strPista = obj.Dsc_PistaNuev;
      }

      [Microsoft.SqlServer.Server.SqlProcedure]
      public static void usp_audit_tabla_ins(string strModulo, string strSubModulo, string strUsuario,
                                             string strRol,string strTipOperacion, string strTabla,
                                             string strRegNuevo, string strRegAnterior
                                             )
      {
            SqlConnection con = new SqlConnection("context connection = true");
            con.Open();
            AUDIT_Tabla obj = new AUDIT_Tabla(strTabla, con);
            
            if (obj.GetAuditable())
            {
                  string strSQL = "INSERT INTO TUA_Auditoria(Cod_Modulo,Cod_SubModulo,Cod_Usuario,"+
                                  " Cod_Rol,Tip_Operacion,Log_Reg_Orig,Log_Reg_Nuevo,Log_Tabla_Mod," +
                                  " Fch_Registro) " +
                                  " VALUES('"+strModulo +"' ,'"+strSubModulo+"','"+strUsuario+"', " +
                                  " '"+ strRol+"','"+strTipOperacion+"','"+ strRegAnterior+"','"+strRegNuevo +"','"+
                                  strTabla +"', " + " getdate())";
                  SqlCommand myCommand = new SqlCommand(strSQL, con);
                  myCommand.ExecuteNonQuery();
            }
      }


};
