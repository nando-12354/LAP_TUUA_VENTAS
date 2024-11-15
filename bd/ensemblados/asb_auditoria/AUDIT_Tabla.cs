using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using Microsoft.SqlServer.Server;
using System.Data.SqlClient;
using System.Xml;

namespace LAP.TUUA.AUDITORIA_CLR
{
      public class AUDIT_Tabla
      {
            public string Cod_Modulo;
            public string Cod_SubModulo;
            public string Dsc_Tabla;
            public string Dsc_PistaNuev;
            public string Dsc_PistaAnt;
            public ArrayList Lst_Columnas;
            private SqlConnection sqlCon;

            public AUDIT_Tabla(string strTabla, SqlConnection sqlCon) 
            {
                  this.Dsc_Tabla = strTabla;
                  this.sqlCon = sqlCon;
            }


            public AUDIT_Tabla(string strModulo, string strSubModulo, string strTabla, SqlConnection sqlCon)
            {
                  this.Dsc_Tabla = strTabla;
                  this.Cod_Modulo = strModulo;
                  this.Cod_SubModulo = strSubModulo;
                  this.sqlCon = sqlCon;
                  this.Lst_Columnas = GetListaColumnasXML();
            }


            public string SalvarPK(string[] strCampoValor) 
            { 
                  string strRpta="";
                  AUDIT_ColumnaXML obj;
                  string strAnd = "";
                  for (int j = 0; j < strCampoValor.Length; j++)
                  {
                        string[] valor = strCampoValor[j].Split('©');

                        for (int i = 0; i < Lst_Columnas.Count; i++)
                        {
                              obj = (AUDIT_ColumnaXML)Lst_Columnas[i];
                              if (valor[0].Equals(obj.Nom_Columna))
                              {
                                    if (obj.Dsc_Tipo.ToUpper().Trim().Equals("NUM"))
                                          strRpta =strRpta+ strAnd+ valor[0] + "=" + valor[1] + " ";
                                    else
                                          strRpta =strRpta+ strAnd+ valor[0] + "='" + valor[1] + "' ";
                                    strAnd = "and ";
                                    break;
                              }
                        }
                  }
                  return strRpta;
            }


            public void SalvarPistas() 
            { 

            }
            /// <summary>
            /// recupera pista, segun el pk de la tabla 
            /// </summary>
            /// <param name="helper"></param>
            public void SalvarPista(string strPK)
            {
                  char chrCamp='|';

                  string[] arrayCampoValor= strPK.Split(chrCamp);

                  string strPista = "";
                  string strCols = "";
                  string strWhere = "  where  ";
   
                  //obtiene campos y valores
                  for (int i = 0; i < Lst_Columnas.Count; i++)
                  {
                        AUDIT_ColumnaXML obj = (AUDIT_ColumnaXML)Lst_Columnas[i];
                        if (!((i + 1) == Lst_Columnas.Count))
                        {
                              strCols = strCols + obj.Nom_Columna + ",";

                        }
                        else
                        {
                              strCols = strCols + obj.Nom_Columna;
                        }
                        
                  }

                  //obtiene pks
                  string sql = "select " + strCols + "  from " + Dsc_Tabla +
                               strWhere+ SalvarPK(arrayCampoValor);

                  SqlCommand myCommand = new SqlCommand(sql,sqlCon);

                  SqlDataReader rdr = myCommand.ExecuteReader();

                  if (rdr.Read())
                  {
                        for (int i = 0; i < Lst_Columnas.Count; i++)
                        {
                              AUDIT_ColumnaXML obj = (AUDIT_ColumnaXML)Lst_Columnas[i];
                              if (!((i + 1) == Lst_Columnas.Count))
                                    strPista = strPista + obj.Ord_ID + "©" + rdr[i].ToString() + "|";
                              else
                                    strPista = strPista + obj.Ord_ID + "©" + rdr[i].ToString();
                        }
                  }
                  this.Dsc_PistaNuev = strPista;
                  rdr.Close();
            }

            /// <summary>
            /// Graba pista previamente almacenado
            /// </summary>
            public void RegistrarPista()
            {

            }

            /// <summary>
            /// Devuelve las columnas de una tabla en un arreglo
            /// </summary>
            /// <returns></returns>
            public ArrayList GetListaColumnasXML()
            {
                  AUDIT_FileTablaXML tablasXML = new AUDIT_FileTablaXML();
                  ArrayList Lst_Columnas = new ArrayList();
                  if (Dsc_Tabla != null)
                        Lst_Columnas = tablasXML.selectAllColumnsByTable(Dsc_Tabla,sqlCon);
                  this.Lst_Columnas = Lst_Columnas;
                  return Lst_Columnas;
            }

            public bool GetAuditable()
            {
                  AUDIT_FileTablaXML tablasXML = new AUDIT_FileTablaXML();
                  string strEstado= tablasXML.selectByTable(Dsc_Tabla, sqlCon);
                  if (strEstado.Equals("TRUE")  )
                        return true;
                  else 
                        return false;   
            }

            public void SalvarPistaAll(string strPK)
            {
                  char chrCamp = '|';

                  string[] arrayCampoValor = strPK.Split(chrCamp);

                  string strPista = "";
                  string strCols = "";
                  string strWhere = "  where  ";

                  //obtiene campos y valores
                  for (int i = 0; i < Lst_Columnas.Count; i++)
                  {
                        AUDIT_ColumnaXML obj = (AUDIT_ColumnaXML)Lst_Columnas[i];
                        if (!((i + 1) == Lst_Columnas.Count))
                        {
                              strCols = strCols + obj.Nom_Columna + ",";

                        }
                        else
                        {
                              strCols = strCols + obj.Nom_Columna;
                        }

                  }

                  //obtiene pks
                  string sql = "select " + strCols + "  from " + Dsc_Tabla +
                               strWhere + SalvarPK(arrayCampoValor);

                  SqlCommand myCommand = new SqlCommand(sql, sqlCon);

                  SqlDataReader rdr = myCommand.ExecuteReader();

                  while (rdr.Read())
                  {
                        for (int i = 0; i < Lst_Columnas.Count; i++)
                        {
                              AUDIT_ColumnaXML obj = (AUDIT_ColumnaXML)Lst_Columnas[i];
                              if (!((i + 1) == Lst_Columnas.Count))
                                    strPista = strPista + obj.Ord_ID + "©" + rdr[i].ToString() + "|";
                              else
                                    strPista = strPista + obj.Ord_ID + "©" + rdr[i].ToString();
                        }
                  }
                  this.Dsc_PistaNuev = strPista;
            }

      }
}
