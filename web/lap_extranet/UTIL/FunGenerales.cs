using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web.UI.WebControls;
using System.Collections;

namespace LAP.EXTRANET.UTIL
{
    public class FunGenerales
    {
        public void LlenarCombo(DropDownList ddlBase, DataTable dtBase, string DataValueField, string DataTextField, bool sAgregarItemTodos, bool sAgregarSeleccione)
        {
            try
            {
                if (sAgregarItemTodos == true)
                {
                    DataRow rows;
                    rows = dtBase.NewRow();
                    rows[DataValueField] = "0";
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

