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
            con.Close();
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
            con.Close();
      }

      [Microsoft.SqlServer.Server.SqlProcedure]

      public static void usp_audit_tabla_selall(string strPK, string strTabla, out string strPista)
      {
            SqlConnection con = new SqlConnection("context connection = true");
            con.Open();

            AUDIT_Tabla obj = new AUDIT_Tabla(strTabla, con);
            obj.GetListaColumnasXML();
            obj.SalvarPistaAll(strPK);
            strPista = obj.Dsc_PistaNuev;
            con.Close();
      }


      //strPKS = campo@valor|campo@valor..;campo@valor|.....
      [Microsoft.SqlServer.Server.SqlProcedure]
      public static void usp_audit_tabla_ins_multiple(string strModulo, string strSubModulo, string strUsuario,
                                             string strRol, string strTipOperacion, string strTabla, [SqlFacet(MaxSize = -1)] SqlString strPKS
                                             )
      {
            string strRegNuevo, strRegAnterior ;
            strRegNuevo = ""; strRegAnterior = "";

            SqlConnection con = new SqlConnection("context connection = true");
            con.Open();
            AUDIT_Tabla obj = new AUDIT_Tabla(strTabla, con);
            
            string[] arrPKS = ((string)strPKS).Split('~');

            obj.GetListaColumnasXML();

            if (obj.GetAuditable())
            {
                  for (int i = 0; i < arrPKS.Length;i++ )
                  {
                        obj.SalvarPista(arrPKS[i]);
                        strRegNuevo = obj.Dsc_PistaNuev;

                        string strSQL = "INSERT INTO TUA_Auditoria(Cod_Modulo,Cod_SubModulo,Cod_Usuario," +
                                  " Cod_Rol,Tip_Operacion,Log_Reg_Orig,Log_Reg_Nuevo,Log_Tabla_Mod," +
                                  " Fch_Registro) " +
                                  " VALUES('" + strModulo + "' ,'" + strSubModulo + "','" + strUsuario + "', " +
                                  " '" + strRol + "','" + strTipOperacion + "','" + strRegAnterior + "','" + strRegNuevo + "','" +
                                  strTabla + "', " + " getdate())";
                        SqlCommand myCommand = new SqlCommand(strSQL, con);
                        myCommand.ExecuteNonQuery();
                  }
            }
            con.Close();
      }


      [Microsoft.SqlServer.Server.SqlProcedure]
      public static void usp_audit_tabla_del_multiple(string strModulo, string strSubModulo, string strUsuario,
                                                      string strRol, string strTipOperacion, string strTabla, [SqlFacet(MaxSize = -1)] SqlString strPKS
                                                      )
      {
            string strRegNuevo, strRegAnterior;
            strRegNuevo = ""; strRegAnterior = "";
            
            SqlConnection con = new SqlConnection("context connection = true");
            con.Open();
            AUDIT_Tabla obj = new AUDIT_Tabla(strTabla, con);

            string[] arrPKS = ((string)strPKS).Split('~');

            obj.GetListaColumnasXML();

            if (obj.GetAuditable())
            {
                  for (int i = 0; i < arrPKS.Length; i++)
                  //      for (int i = 85; i < 86; i++)
                  {
                        if (arrPKS[i].Trim().Length > 1)
                        {
                              obj.SalvarPista(arrPKS[i].Trim());
                              strRegAnterior = obj.Dsc_PistaNuev;

                              string strSQL = "INSERT INTO TUA_Auditoria(Cod_Modulo,Cod_SubModulo,Cod_Usuario," +
                                        " Cod_Rol,Tip_Operacion,Log_Reg_Orig,Log_Reg_Nuevo,Log_Tabla_Mod," +
                                        " Fch_Registro) " +
                                        " VALUES('" + strModulo + "' ,'" + strSubModulo + "','" + strUsuario + "', " +
                                        " '" + strRol + "','" + strTipOperacion + "','" + strRegAnterior + "','" + strRegNuevo + "','" +
                                        strTabla + "', " + " getdate())";
                              SqlCommand myCommand = new SqlCommand(strSQL, con);
                              myCommand.ExecuteNonQuery();
                              
                        }
                  }
            }
            con.Close();
      }
};
