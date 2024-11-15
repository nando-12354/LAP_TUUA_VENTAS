using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

public partial class _Default : System.Web.UI.Page 
{
	/* Edwin Acuña
	Conexion de Prueba lo que vale es la libreria dll que se genera en el bin*/
    private const string demoConnString = @"Password=123456;Persist Security Info=True;User ID=sa;Initial Catalog=DBTUUA_110110;Data Source=172.15.1.10";
    private const string demoTableName = "TUA_Ticket";
    private const string demoTableDefaultOrderBy = "Cod_Numero_Ticket";

    #region Page and Control event handling
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            PagingGridView1.VirtualItemCount = GetRowCount();
            BindPagingGrid();
        }
    }
    
    protected void PagingGridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        PagingGridView1.PageIndex = e.NewPageIndex;
        BindPagingGrid();
    }

    protected void PagingGridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        BindPagingGrid();
    }

    /// <summary>
    /// Helper to bind the grid to the dynamic data
    /// </summary>
    private void BindPagingGrid()
    {
        PagingGridView1.DataSource = GetDataPage(PagingGridView1.PageIndex, PagingGridView1.PageSize, PagingGridView1.OrderBy);
        PagingGridView1.DataBind();
    }
    #endregion

    #region Dynamic data query
    private int GetRowCount()
    {
        using (SqlConnection conn = new SqlConnection(demoConnString))
        {
            conn.Open();
            SqlCommand comm = new SqlCommand(@"SELECT COUNT(*) FROM " + demoTableName, conn);
            int count = Convert.ToInt32(comm.ExecuteScalar());
            conn.Close();
            return count;
        }
    }

    private DataTable GetDataPage(int pageIndex, int pageSize, string sortExpression)
    {
        using (SqlConnection conn = new SqlConnection(demoConnString))
        {
            // Nosotros siempre necesitamos ordenar las columnas con un ROW_NUMBER() 
            if (sortExpression.Trim().Length == 0)
                sortExpression = demoTableDefaultOrderBy;

            conn.Open();
            
            string commandText = string.Format("SELECT * FROM (select TOP {0} ROW_NUMBER() OVER (ORDER BY {1}) as ROW_NUM, *  " +
                                                "FROM {2} ORDER BY ROW_NUM) innerSelect WHERE ROW_NUM > {3}",
                                                ((pageIndex + 1) * pageSize), 
                                                sortExpression, 
                                                demoTableName,
                                                (pageIndex * pageSize));
            SqlDataAdapter adapter = new SqlDataAdapter(commandText, conn);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            conn.Close();
            dt.Columns.Remove("ROW_NUM");
            return dt;
        }

    }
    #endregion
}
