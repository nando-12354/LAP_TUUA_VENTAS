using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Xml;
using System.Data.SqlClient;

namespace LAP.TUUA.AUDITORIA_CLR
{
      class AUDIT_FileTablaXML
       {

            public ArrayList selectAllColumnsByTable(String table, SqlConnection sqlCon)
            {
                  ArrayList lstColumnas;

                  string sql = "select xml  from  TUA_ParameGeneral where Identificador='AD' ";
                  SqlCommand myCommand = new SqlCommand(sql, sqlCon);
                  SqlDataReader rdr = myCommand.ExecuteReader();
                  rdr.Read();
                  XmlDocument xmlDoc = new XmlDocument();
                  xmlDoc.LoadXml(rdr[0].ToString());
                  rdr.Close();
                  lstColumnas = new ArrayList();

                  XmlElement root = xmlDoc.DocumentElement;
                  XmlNodeList elemList = root.GetElementsByTagName("TABLA");


                  AUDIT_ColumnaXML columna;
                  foreach (XmlNode node in elemList)
                  {
                        if (node.SelectSingleNode("NOMBRE_TABLE").InnerText.Equals(table))
                        {
                              XmlNode columnas = node.SelectSingleNode("COLUMNAS");
                              XmlNodeList cols= columnas.ChildNodes;

                              foreach( XmlNode nodocol in cols)
                              {
                                   columna= new AUDIT_ColumnaXML();
                                   columna.Nom_Columna = nodocol.SelectSingleNode("NOMBRE_COL").InnerText.Trim();
                                   columna.Nom_Alias = nodocol.SelectSingleNode("ALIAS_COL").InnerText.Trim();
                                   columna.Dsc_Tipo = nodocol.SelectSingleNode("TIPO").InnerText.Trim();
                                   columna.Ord_ID = nodocol.SelectSingleNode("ORDINAL_ID").InnerText.Trim();
                                   //<relacion>
                                   //<options>
                                   columna.Audit_Flag = nodocol.SelectSingleNode("AUDIT_COL").InnerText.Trim();
                                   columna.Key_Flag = nodocol.SelectSingleNode("KEY").InnerText.Trim();
                                   if (columna.Audit_Flag.ToUpper().Equals("TRUE"))
                                         lstColumnas.Add(columna);
                              }
                        }
                  }
                  return lstColumnas;
            }

            public string selectByTable(String table, SqlConnection sqlCon)
            {
                  ArrayList lstColumnas;

                  string sql = "select xml  from  TUA_ParameGeneral  where Identificador='AD'";
                  SqlCommand myCommand = new SqlCommand(sql, sqlCon);
                  SqlDataReader rdr = myCommand.ExecuteReader();
                  rdr.Read();
                  XmlDocument xmlDoc = new XmlDocument();
                  xmlDoc.LoadXml(rdr[0].ToString());
                  rdr.Close();
                  lstColumnas = new ArrayList();

                  XmlElement root = xmlDoc.DocumentElement;
                  XmlNodeList elemList = root.GetElementsByTagName("TABLA");

                  foreach (XmlNode node in elemList)
                  {
                        if (node.SelectSingleNode("NOMBRE_TABLE").InnerText.Equals(table))
                        {
                              return node.SelectSingleNode("AUDITABLE").InnerText;
                              
                        }
                  }
                  return "";
            }
      }


}
