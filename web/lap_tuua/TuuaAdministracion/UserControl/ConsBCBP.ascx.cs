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

public partial class UserControl_ConsBCBP : System.Web.UI.UserControl
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
    public void Inicio(String Num_Secuencial_Bcbp)
    {
        htLabels = LabelConfig.htLabels;
        try
        {
            lblDetalleBCBP.Text = (String) htLabels["consDetalleBCBP.lblDetalleBCBP"];

            this.lblCompania.Text = (string) htLabels["consDetalleBCBP.lblCompania"];
            this.lblFechaVuelo.Text = (string) htLabels["consDetalleBCBP.lblFechaVuelo"];
            this.lblNumVuelo.Text = (string) htLabels["consDetalleBCBP.lblNumVuelo"];
            this.lblNumAsiento.Text = (string) htLabels["consDetalleBCBP.lblNumAsiento"];
            this.lblNomPasajero.Text = (string) htLabels["consDetalleBCBP.lblNomPasajero"];
            this.lblTipoIngreso.Text = (string) htLabels["consDetalleBCBP.lblTipoIngreso"];

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



        try
        {
            DataTable dt_consulta = objWBConsultas.DetalleBoarding("", "", "", "", "", "", null, Num_Secuencial_Bcbp);

            if (dt_consulta.Rows.Count > 0)
            {
                this.lblDetCompania.Text = dt_consulta.Rows[0]["Dsc_Compania"].ToString();
                String fch_Vuelo = dt_consulta.Rows[0]["Fch_Vuelo"].ToString();
                fch_Vuelo = fch_Vuelo.Substring(6, 2) + "/" + fch_Vuelo.Substring(4, 2) + "/" + fch_Vuelo.Substring(0, 4);
                this.lblDetFechaVuelo.Text = fch_Vuelo;
                this.lblDetNumVuelo.Text = dt_consulta.Rows[0]["Num_Vuelo"].ToString();
                this.lblDetNumAsiento.Text = dt_consulta.Rows[0]["Num_Asiento"].ToString();
                this.lblDetNomPasajero.Text = dt_consulta.Rows[0]["Nom_Pasajero"].ToString();
                this.lblDetTipoIngreso.Text = dt_consulta.Rows[0]["Tip_Ingreso"].ToString();
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
            DataTable dt_boardingEstHist = objWBConsultas.DetalleBoardingEstHist(Num_Secuencial_Bcbp);

            if (dt_boardingEstHist.Rows.Count > 0)
            {
                grvBoardingEstHist.DataSource = dt_boardingEstHist;
                grvBoardingEstHist.PageSize = valorMaxGrilla;
                grvBoardingEstHist.DataBind();
                ViewState["DetalleBCBP"] = dt_boardingEstHist;
                grvBoardingEstHist.Visible = true;
                msgNoHist.Visible = false;
            }
            else
            {
                grvBoardingEstHist.Visible = false;
                msgNoHist.Visible = true;
                msgNoHist.Text = (string) htLabels["consDetalleBCBP.msgNoHist"];
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

    #endregion

    #region grvBoardingEstHist_PageIndexChanging
    protected void grvBoardingEstHist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grvBoardingEstHist.PageIndex = e.NewPageIndex;
        grvBoardingEstHist.DataSource = (DataTable)ViewState["DetalleBCBP"];
        grvBoardingEstHist.DataBind();
    }
    #endregion

    protected void grvBoardingEstHist_Sorting(object sender, GridViewSortEventArgs e)
    {

    }





}
