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

public partial class UserControl_ConsTicket : System.Web.UI.UserControl
{

    protected Hashtable htLabels;
    protected bool Flg_Error;
    protected BO_Error objError;
    BO_Consultas objWBConsultas = new BO_Consultas();
    int valorMaxGrilla;

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    #region Inicio
    public void Inicio(String codigo)
    {
        htLabels = LabelConfig.htLabels;
        try
        {
            lblDetalleTicket.Text = (String)htLabels["consDetalleDocumento.lblDetalleTicket"];

            this.lblNumTicket.Text = (string)htLabels["consDetalleDocumento.lblNumTicket"];
            this.lblTipoTicket.Text = (string)htLabels["consDetalleDocumento.lblTipoTicket"];
            this.lblCompania.Text = (string)htLabels["consDetalleDocumento.lblCompania"];

            this.lblNumVuelo.Text = (string)htLabels["consDetalleDocumento.lblNumVuelo"];
            this.lblFechaVencimiento.Text = (string)htLabels["consDetalleDocumento.lblFechaVencimiento"];
            this.lblTipoPersona.Text = (string)htLabels["consDetalleDocumento.lblTipoPersona"];

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


        string NumTicket = codigo;
        if (NumTicket != null)
        {
            try
            {
                DataTable dt_consulta = objWBConsultas.ConsultaDetalleTicket_Reh(NumTicket, "", "");//EAG 13/01/2010

                if (dt_consulta.Rows.Count > 0)
                {
                    this.lblDetNumTicket.Text = dt_consulta.Rows[0].ItemArray.GetValue(0).ToString();
                    this.lblDetTipoTicket.Text = dt_consulta.Rows[0].ItemArray.GetValue(1).ToString();
                    this.lblDetCompania.Text = dt_consulta.Rows[0].ItemArray.GetValue(2).ToString();
                    this.lblDetNumVuelo.Text = dt_consulta.Rows[0].ItemArray.GetValue(3).ToString();           
                    string sFechaVencimiento = dt_consulta.Rows[0].ItemArray.GetValue(5).ToString();
                    sFechaVencimiento = sFechaVencimiento.Substring(6,2)+"/"+ sFechaVencimiento.Substring(4,2)+ "/" + sFechaVencimiento.Substring(0, 4);
                    this.lblDetFchVencimiento.Text = sFechaVencimiento;
                    this.lblDetTipPersona.Text = dt_consulta.Rows[0].ItemArray.GetValue(15).ToString();
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
                DataTable dt_parametro = objWBConsultas.ListarParametros("LG");

                if (dt_parametro.Rows.Count > 0)
                {
                    valorMaxGrilla = Convert.ToInt32(dt_parametro.Rows[0].ItemArray.GetValue(4).ToString());
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
                DataTable dt_ticketEstHist = objWBConsultas.ListarTicketEstHist(NumTicket);

                if (dt_ticketEstHist.Rows.Count > 0)
                {
                    grvSubDetalleTicket.DataSource = dt_ticketEstHist;
                    grvSubDetalleTicket.PageSize = valorMaxGrilla;
                    grvSubDetalleTicket.DataBind();
                    ViewState["DetalleTicket"] = dt_ticketEstHist;
                    grvSubDetalleTicket.Visible = true;
                    msgNoHist.Visible = false;
                }
                else
                {
                    grvSubDetalleTicket.Visible = false;
                    msgNoHist.Visible = true;
                    msgNoHist.Text = (string)htLabels["consDetalleDocumento.msgNoHist"];
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


        }           


        mpext.Show();
    }
    #endregion

    #region grvSubDetalleTicket_PageIndexChanging
    protected void grvSubDetalleTicket_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grvSubDetalleTicket.PageIndex = e.NewPageIndex;
        grvSubDetalleTicket.DataSource = (DataTable)ViewState["DetalleTicket"];
        grvSubDetalleTicket.DataBind();
    }
    #endregion

    protected void grvSubDetalleTicket_Sorting(object sender, GridViewSortEventArgs e)
    {

    }

    protected void grvSubDetalleTicket_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string Tip_Estado = System.Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Tip_Estado"));
            if (Tip_Estado.Trim().ToUpper().Equals("R"))
            {
                //e.Row.BackColor = System.Drawing.Color.Red;
                e.Row.BackColor = System.Drawing.Color.FromArgb(253, 234, 234);
            }
        }      
    }
    


}
