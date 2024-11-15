using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using System.Collections;


namespace LAP.TUUA.UTIL
{
   public class UIControles
    {




       public void LlenarCombo(DropDownList ddlBase, DataTable dtBase, string DataValueField,string DataTextField,bool sAgregarItemTodos,bool sAgregarSeleccione)
       {
           try
           {
               if (sAgregarItemTodos == true)
               {
                   DataRow rows;
                   rows = dtBase.NewRow();
                   rows[DataValueField]="0";
                   if (sAgregarSeleccione == true)
                   {
                       rows[DataTextField] = " -Seleccionar- ";
                   }
                   else
                   {
                       rows[DataTextField] = " <Todos> ";
                   }
                   dtBase.Rows.Add(rows);

                   dtBase.DefaultView.Sort = dtBase.Columns[DataTextField].ColumnName.ToString();
                   dtBase = dtBase.DefaultView.ToTable();

                   ddlBase.DataSource = dtBase;
                   ddlBase.DataTextField = DataTextField;
                   ddlBase.DataValueField = DataValueField; 
                   ddlBase.SelectedValue = "0";  
                   ddlBase.DataBind();

               }
               else
               {
                   ddlBase.DataSource = dtBase;
                   ddlBase.DataTextField = DataTextField;
                   ddlBase.DataValueField = DataValueField;
                   ddlBase.DataBind();
               }               

           }
           catch (Exception ex)
           {
               throw ex;
           }

       }

       public void LlenarComboSinValue(DropDownList ddlBase, DataTable dtBase, string DataValueField, string DataTextField, bool sAgregarItemTodos, bool sAgregarSeleccione)
       {
           try
           {
               if (sAgregarItemTodos == true)
               {
                   DataRow rows;
                   rows = dtBase.NewRow();
                   rows[DataValueField] = "-";
                   if (sAgregarSeleccione == true)
                   {
                       rows[DataTextField] = " -Seleccionar- ";
                   }
                   else
                   {
                       rows[DataTextField] = " <Todos> ";
                   }
                   dtBase.Rows.Add(rows);

                   dtBase.DefaultView.Sort = dtBase.Columns[DataTextField].ColumnName.ToString();
                   dtBase = dtBase.DefaultView.ToTable();

                   ddlBase.DataSource = dtBase;
                   ddlBase.DataTextField = DataTextField;
                   ddlBase.DataValueField = DataValueField;
                   ddlBase.SelectedValue = "-";
                   ddlBase.DataBind();

               }
               else
               {
                   ddlBase.DataSource = dtBase;
                   ddlBase.DataTextField = DataTextField;
                   ddlBase.DataValueField = DataValueField;
                   ddlBase.DataBind();
               }

           }
           catch (Exception ex)
           {
               throw ex;
           }

       }

       private bool ColumnEqual(object A, object B)
       {

           // Compares two values to see if they are equal. Also compares DBNULL.Value.
           // Note: If your DataTable contains object fields, then you must extend this
           // function to handle them in a meaningful way if you intend to group on them.

           if (A == DBNull.Value && B == DBNull.Value) //  both are DBNull.Value
               return true;
           if (A == DBNull.Value || B == DBNull.Value) //  only one is DBNull.Value
               return false;
           return (A.Equals(B));  // value type standard comparison
       }



       public DataTable SelectDistinct(string TableName, DataTable SourceTable, string FieldName)
       {
           DataTable dt = new DataTable(TableName);
           dt.Columns.Add(FieldName, SourceTable.Columns[FieldName].DataType);

           object LastValue = null;
           foreach (DataRow dr in SourceTable.Select("", FieldName))
           {
               if (LastValue == null || !(ColumnEqual(LastValue, dr[FieldName])))
               {
                   LastValue = dr[FieldName];
                   dt.Rows.Add(new object[] { LastValue });
               }
           }
           //if (ds != null)
           //    ds.Tables.Add(dt);
           return dt;
       }




       public DataTable ConvertDataTable(DataView obDataView)
       {
           if (null == obDataView)
           {
               throw new ArgumentNullException
               ("DataView", "Invalid DataView object specified");
           }

           DataTable obNewDt = obDataView.Table.Clone();
           int idx = 0;
           string[] strColNames = new string[obNewDt.Columns.Count];
           foreach (DataColumn col in obNewDt.Columns)
           {
               strColNames[idx++] = col.ColumnName;
           }

           IEnumerator viewEnumerator = obDataView.GetEnumerator();
           while (viewEnumerator.MoveNext())
           {
               DataRowView drv = (DataRowView)viewEnumerator.Current;
               DataRow dr = obNewDt.NewRow();
               try
               {
                   foreach (string strName in strColNames)
                   {
                       dr[strName] = drv[strName];
                   }
               }
               catch (Exception ex)
               {
                   //(ex.Message);
               }
               obNewDt.Rows.Add(dr);
           }

           return obNewDt;
       }			


    }
}
