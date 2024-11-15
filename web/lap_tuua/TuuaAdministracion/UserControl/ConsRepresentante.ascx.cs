using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using LAP.TUUA.CONTROL;
using LAP.TUUA.UTIL;

public partial class UserControl_ConsRepresentante : System.Web.UI.UserControl
{
    protected Hashtable htLabels;
    protected bool Flg_Error;
    BO_Consultas objBOConsultas = new BO_Consultas();

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    #region Inicio
    public void Inicio()
    {
        mpext.Show();
        htLabels = LabelConfig.htLabels;

        try
        {
            lblCompania.Text = htLabels["consRpteCompania.lblCompania"].ToString();
            lblRepresentante.Text = htLabels["consRpteCompania.lblRepresentante"].ToString();
            lblRpteCompania.Text = htLabels["consRpteCompania.lblRpteCompania"].ToString();

            DataTable dtCompanias = objBOConsultas.listarAllCompania();
            cboCompania.DataSource = dtCompanias;
            cboCompania.DataTextField = "Dsc_Compania";
            cboCompania.DataValueField = "Cod_Compania";
            cboCompania.DataBind();
            cboCompania.Items.Insert(0, "Seleccionar");

            DataTable dt_parametro = objBOConsultas.ListarParametros("LG");
            grvDetalleRepresentantes.PageSize = Int32.Parse(((String) dt_parametro.Rows[0].ItemArray.GetValue(4)));

            DataTable dtDetalleRepre = new DataTable();
            dtDetalleRepre.Columns.Add("Numero", System.Type.GetType("System.String"));
            dtDetalleRepre.Columns.Add("Nombres", System.Type.GetType("System.String"));
            dtDetalleRepre.Columns.Add("Estado", System.Type.GetType("System.String"));
            dtDetalleRepre.Rows.Add(dtDetalleRepre.NewRow());
            grvDetalleRepresentantes.DataSource = dtDetalleRepre;
            grvDetalleRepresentantes.DataBind();
        }
        catch (Exception ex)
        {
            Flg_Error = true;
            ErrorHandler.Trace(ErrorHandler.Desc_Mensaje, ex.Message);
        }
        finally
        {
            if (Flg_Error)
            {
                Response.Redirect("PaginaError.aspx");
            }
        }

    }
    #endregion

    #region cboCompania_SelectedIndexChanged
    protected void cboCompania_SelectedIndexChanged(object sender, EventArgs e)
    {
        String strCodCompania = cboCompania.SelectedItem.Value;
        BO_Rehabilitacion objBO_Rehabilitacion = new BO_Rehabilitacion();
        DataTable dtRepresXCia = objBO_Rehabilitacion.ConsultarRepresXRehabilitacionYCia(strCodCompania);


        DataTable dtRepresentantes = new DataTable();
        dtRepresentantes.Columns.Add("Numero", Type.GetType("System.String"));
        dtRepresentantes.Columns.Add("Nombres", Type.GetType("System.String"));
        dtRepresentantes.Columns.Add("Estado", Type.GetType("System.String"));
        if (dtRepresXCia.Rows.Count == 0)
        {
            DataRow row = dtRepresentantes.NewRow();
            dtRepresentantes.Rows.Add(row);
        }
        for (int i = 0; i < dtRepresXCia.Rows.Count; i++)
        {
            DataRow row = dtRepresentantes.NewRow();
            row["Numero"] = i + 1;
            row["Nombres"] = dtRepresXCia.Rows[i]["Nom_Representante"].ToString().Trim() + " " +
                             dtRepresXCia.Rows[i]["Ape_Representante"].ToString().Trim();
            if (dtRepresXCia.Rows[i]["Tip_Estado"].ToString().Trim().Equals("1"))
            {
                row["Estado"] = "Activo";
            }
            else
            {
                row["Estado"] = "Inactivo";
            }
            dtRepresentantes.Rows.Add(row);
        }

        grvDetalleRepresentantes.PageIndex = 0;
        grvDetalleRepresentantes.DataSource = dtRepresentantes;
        grvDetalleRepresentantes.DataBind();
        ViewState["dtRepresentantes"] = dtRepresentantes;
    }
    #endregion

    #region grvDetalleRepresentantes_RowDataBound
    protected void grvDetalleRepresentantes_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string Estado = System.Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Estado"));

            if (Estado.Equals("Inactivo"))
            {
                e.Row.Cells[1].ForeColor = Color.Red;
                e.Row.Cells[2].ForeColor = Color.Red;
            }
        }
    }
    #endregion

    protected void btnCerrarPopup_Click(object sender, EventArgs e)
    {
        //mpext.Hide();
        ////pnlPopup.Visible = false;
    }

    #region grvDetalleRepresentantes_PageIndexChanging
    protected void grvDetalleRepresentantes_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grvDetalleRepresentantes.PageIndex = e.NewPageIndex;
        grvDetalleRepresentantes.DataSource = (DataTable)ViewState["dtRepresentantes"];
        grvDetalleRepresentantes.DataBind();
    }
    #endregion
}
