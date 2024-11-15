using System;
using System.Collections;
using System.Configuration;
using System.Data;
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


public partial class UserControl_DetalleTransaccionCritica : System.Web.UI.UserControl
{
    protected Hashtable htLabels;
    protected bool Flg_Error;
    protected bool flagError;
    BO_Consultas objWBConsultas = new BO_Consultas(Define.CNX_05);
    int valorMaxGrilla;

    protected void Page_Load(object sender, EventArgs e)
    {
        htLabels = LabelConfig.htLabels;
        try
        {
            lblDetalleTicket.Text = (String)htLabels["consDetalleDocumento.lblDetalleTicket"];

            this.lblUsuario.Text = (string)htLabels["consDetalleAuditoria.lblUsuario"];
            this.lblRolesUsuario.Text = (string)htLabels["consDetalleAuditoria.lblRolesUsuario"];
            this.lblTablaMod.Text = (string)htLabels["consDetalleAuditoria.lblTablaMod"];

            //txtUsuariorpt.Text = (string)htLabels["consDetalleAuditoria.lblUsuario"];
            //txtRolesrpt.Text = (string)htLabels["consDetalleAuditoria.lblRolesUsuario"];
            //txtNomTablarpt.Text = (string)htLabels["consDetalleAuditoria.lblTablaMod"];
            //txtContadorrpt.Text = CodContador;

        }
        catch (Exception ex)
        {
            Flg_Error = true;
            ErrorHandler.Cod_Error = Define.ERR_008;
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

    public void CargarDetalle(string NomUsuario, string NomRoles, string NomTabla,string CodContador)
    {
        lblDetUsuario.Text = NomUsuario;
        lblDetRolesUsuario.Text = NomRoles;
        lblDetTablaMod.Text = NomTabla;

        //hfUsuario.Value = NomUsuario;
        //hfRoles.Value = NomRoles;
        //hfNombreTabla.Value = NomTabla;
        //hfContador.Value = CodContador;

        //txtUsuariorpt.Text = NomUsuario;
        //txtRolesrpt.Text = NomRoles;
        //txtNomTablarpt.Text = NomTabla;
        //txtContadorrpt.Text = CodContador;
        
        try
        {
            DataTable dt_parametro = objWBConsultas.ListarParametros("LG");

            if (dt_parametro.Rows.Count > 0)
            {
                
                txtValorMaximoGrilla.Text = dt_parametro.Rows[0].ItemArray.GetValue(4).ToString();
            }
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

        try
        {
            DataTable dt_consulta = objWBConsultas.obtenerdetalleAuditoriaCrit(NomTabla, CodContador);
            if (dt_consulta.Rows.Count > 0)
            {
                ViewState["DetalleAuditoria"] = dt_consulta; 
                grvDetalleAuditoria.DataSource = dt_consulta;
                grvDetalleAuditoria.PageSize = Convert.ToInt32(txtValorMaximoGrilla.Text);
                grvDetalleAuditoria.PageIndex = 0;
                grvDetalleAuditoria.DataBind();
                             
                msgNoHist.Text="";
            }
            else
            {
                grvDetalleAuditoria.DataSource = null;
                grvDetalleAuditoria.DataBind();
                msgNoHist.Text = (string)htLabels["consDetalleAuditoria.msgNoHist"];
            }

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

        mpext.Show();
    }



    protected void grvDetalleAuditoria_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grvDetalleAuditoria.PageIndex = e.NewPageIndex;
        grvDetalleAuditoria.DataSource = (DataTable)ViewState["DetalleAuditoria"];
        grvDetalleAuditoria.DataBind();
    }


    protected void grvDetalleAuditoria_Sorting(object sender, GridViewSortEventArgs e)
    {
        string sortExpression = e.SortExpression;

        if (GridViewSortDirection == SortDirection.Ascending)
        {
            GridViewSortDirection = SortDirection.Descending;

            this.txtOrdenacion.Text = "DESC";
            SortGridView(sortExpression, "DESC");

        }
        else
        {
            GridViewSortDirection = SortDirection.Ascending;

            this.txtOrdenacion.Text = "ASC";
            SortGridView(sortExpression, "ASC");

        }
        this.txtColumna.Text = sortExpression;
    }




    private void SortGridView(string sortExpression, String direction)
    {
        try
        {
            grvDetalleAuditoria.DataSource = dwConsulta((DataTable)ViewState["DetalleAuditoria"], sortExpression, direction);
            grvDetalleAuditoria.PageSize = Convert.ToInt32(txtValorMaximoGrilla.Text);
            grvDetalleAuditoria.DataBind();
        }
        catch (Exception ex)
        {
            flagError = true;
            ErrorHandler.Trace(ErrorHandler.Desc_Mensaje, ex.Message);
        }
        finally
        {
            if (flagError)
                Response.Redirect("PaginaError.aspx");

        }

    }

    protected DataView dwConsulta(DataTable dtConsulta, string sortExpression, String direction)
    {
        DataView dv = new DataView(dtConsulta);

        if (txtOrdenacion.Text.CompareTo("") != 0)
        {
            dv.Sort = sortExpression + " " + direction;
        }

        return dv;
    }

    public SortDirection GridViewSortDirection
    {
        get
        {
            if (ViewState["SortDirection"] == null)
            {
                ViewState["SortDirection"] = SortDirection.Ascending;
            }
            return (SortDirection)ViewState["SortDirection"];

        }
        set
        {
            ViewState["SortDirection"] = value;
        }
    }



    protected void grvDetalleAuditoria_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[2].ToString() != ((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[3].ToString() )
            {
                e.Row.BackColor = System.Drawing.Color.FromArgb(255, 255, 213);
            }
        }
    }
}
