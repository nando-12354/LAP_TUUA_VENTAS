using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using LAP.EXTRANET.UTIL;

namespace TuuaExtranet
{
    public partial class Cns_BcbpMensualLeidosMolinete : System.Web.UI.Page
    {
        LAP.Tuua.Web.WSDAOConsulta.WSConsultas objWS = new LAP.Tuua.Web.WSDAOConsulta.WSConsultas();
        //Filtros
        string sFechaDesde, sFechaHasta;
        string sNumVuelo;
        string sTipVuelo;
        string sTipPasajero;

        DataTable dt_consulta = new DataTable();
        DataTable dt_resumen = new DataTable();
        DataTable dt_parametroTurno = new DataTable();
        Int32 valorMaxGrilla;
        string Cod_IATA = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    this.txtDesde.Text = DateTime.Now.ToShortDateString();
                    this.txtHasta.Text = DateTime.Now.ToShortDateString();

                    CargarEstados();
                }

                //VALIDAMOS LA COMPANIA SI TIENES PERMISOS
                if (Request.QueryString.Count > 0)
                {
                    Cod_IATA = Request.QueryString[0].ToString();
                    DataTable dt_validarBCBP = new DataTable();
                    dt_validarBCBP = objWS.ValidarCompaniaBCBP(Cod_IATA);

                    if ((dt_validarBCBP != null) && (dt_validarBCBP.Rows.Count > 0))
                    {
                        Cod_IATA = Request.QueryString[0].ToString();
                        lblCompania.Text = dt_validarBCBP.Rows[0]["Dsc_Compania"].ToString();
                    }
                    else
                        Cod_IATA = "";
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("PagError.aspx");
                throw ex;
            }
        }

        protected void CargarEstados()
        {
            DataTable dt_estado = new DataTable();

            dt_estado = objWS.listarCamposxNombre("TipoVuelo");
            FunGenerales objComboEstado = new FunGenerales();
            objComboEstado.LlenarCombo(ddlTipVuelo, dt_estado, "Cod_Campo", "Dsc_Campo", true, false);

            dt_estado = objWS.listarCamposxNombre("TipoPasajero");

            FunGenerales objComboPasajero = new FunGenerales();
            objComboPasajero.LlenarCombo(ddlTipPersona, dt_estado, "Cod_Campo", "Dsc_Campo", true, false);
        }


        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsFechaValida())
                {
                    if (Cod_IATA != "")
                    {
                        SaveFiltros();
                        BindPagingGrid();
                    }
                    else
                    {
                        lblMensajeError.Text = "La aerolinea está deshabilitada o no tiene la modalidad de venta BCBP activada";
                        lblMensajeErrorData.Text = "";
                        grvPaginacionBoarding.Visible = false;
                        grvResumen.Visible = false;
                        lblTotal.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                string strMessage = "Solicitud no completada, informe al Administrador del Sistema";
                lblMensajeError.Text = strMessage;
            }
        }

        private void SaveFiltros()
        {
            sNumVuelo = txtNumVuelo.Text;
            sTipVuelo = ddlTipVuelo.SelectedValue;
            sTipPasajero = ddlTipPersona.SelectedValue;

            sFechaDesde = Fecha.convertToFechaSQL2(txtDesde.Text);
            sFechaHasta = Fecha.convertToFechaSQL2(txtHasta.Text);
        }

        

        private bool IsFechaValida()
        {
            int iValiFechas;

            iValiFechas = DateTime.Compare(Convert.ToDateTime(this.txtDesde.Text), Convert.ToDateTime(this.txtHasta.Text));

            if (iValiFechas == 1)
            {
                lblMensajeError.Text = "Filtro de fecha invalido";
                lblMensajeErrorData.Text = "";
                grvPaginacionBoarding.Visible = false;
                grvResumen.Visible = false;
                lblTotal.Text = "";
                return false;
            }
            else
            {
                this.lblMensajeError.Text = "";
                return true;

            }
        }

        private void BindPagingGrid()
        {
            try
            {
                ValidarTamanoGrilla();
                grvPaginacionBoarding.VirtualItemCount = GetRowCount();

                dt_consulta = GetDataPage(grvPaginacionBoarding.PageIndex, grvPaginacionBoarding.PageSize, grvPaginacionBoarding.OrderBy);

                if (dt_consulta.Rows.Count == 0)
                {
                    grvPaginacionBoarding.Visible = false;
                    grvResumen.Visible = false;
                    lblMensajeErrorData.Text = "La búsqueda no retorna resultado";
                    lblTotal.Text = "";
                }
                else
                {

                    lblMensajeErrorData.Text = "";
                    //htLabels = LabelConfig.htLabels;

                    grvPaginacionBoarding.Visible = true;
                    grvPaginacionBoarding.DataSource = dt_consulta;
                    grvPaginacionBoarding.PageSize = valorMaxGrilla;
                    grvPaginacionBoarding.DataBind();

                    lblTotal.Text = "Total de Registros:" + grvPaginacionBoarding.VirtualItemCount;

                    grvResumen.Visible = true;
                    grvResumen.DataSource = dt_resumen;
                    grvResumen.DataBind();

                }
            }
            catch (Exception ex)
            {
                string strMessage = "Solicitud no completada, informe al Administrador del Sistema";
                lblMensajeError.Text = strMessage;
            }
        }

        #region Dynamic data query
        private int GetRowCount()
        {
            int count = 0;
            dt_resumen = objWS.ConsultaBcbpMensualLeidosMolinete(sFechaDesde,
                                                                sFechaHasta,
                                                                sNumVuelo,
                                                                sTipVuelo,
                                                                sTipPasajero,
                                                                Cod_IATA,null,
                                                                0,0,"0","0","1","0").Tables[0];
                                                               

            if (dt_resumen.Columns.Contains("TotRows"))
                count = Convert.ToInt32(dt_resumen.Rows[0]["TotRows"].ToString());
            else
                lblMensajeError.Text = dt_resumen.Rows[0].ItemArray.GetValue(1).ToString();
            return count;

        }

        private DataTable GetDataPage(int pageIndex, int pageSize, string sortExpression)
        {
            DataSet ds_BP = objWS.ConsultaBcbpMensualLeidosMolinete(sFechaDesde,
                                                                    sFechaHasta,
                                                                    sNumVuelo,
                                                                    sTipVuelo,
                                                                    sTipPasajero,
                                                                    Cod_IATA, sortExpression,
                                                                    pageIndex, valorMaxGrilla, "1", "1", "0", "0");

            dt_consulta = ds_BP.Tables[0];
            dt_resumen = ds_BP.Tables[1];

            Session["tablaBoarding"] = dt_consulta;
            Session["tablaDetBoarding"] = dt_resumen;

            return dt_consulta;
        }
        #endregion

        protected void grvPaginacionBoarding_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            SaveFiltros();
            grvPaginacionBoarding.PageIndex = e.NewPageIndex;
            BindPagingGrid();
        }

        protected void grvPaginacionBoarding_Sorting(object sender, GridViewSortEventArgs e)
        {
            SaveFiltros();
            BindPagingGrid();
        }

        protected void grvResumen_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                //AGREGAMOS EL TOTAL                
                try
                {
                    Int64 total = Convert.ToInt64(dt_resumen.Compute("Sum(Cantidad)", "").ToString());
                    e.Row.Cells[1].Text = "TOTAL";
                    e.Row.Cells[2].Text = total.ToString();
                }
                catch { }
            }
        }

        void ValidarTamanoGrilla()
        {
            //Traer valor de tamaño de la grilla desde parametro general              
            dt_parametroTurno = objWS.ListarParametros("LG");

            if (dt_parametroTurno.Rows.Count > 0)
            {
                valorMaxGrilla = Convert.ToInt32(dt_parametroTurno.Rows[0].ItemArray.GetValue(4).ToString());
            }
        }

        protected void lblExportar_Click(object sender, EventArgs e)
        {
            DataTable dtDatos = (DataTable)Session["tablaBoarding"];
            if ((dtDatos != null) && (dtDatos.Rows.Count > 0))
            {
                DataTable dt_BoardingBcbp = new DataTable();
                SaveFiltros();
                dt_BoardingBcbp = objWS.ConsultaBcbpMensualLeidosMolinete(sFechaDesde,
                                                                        sFechaHasta,
                                                                        sNumVuelo,
                                                                        sTipVuelo,
                                                                        sTipPasajero,
                                                                        Cod_IATA, null,
                                                                        0, 0, "0", "0", "0", "1").Tables[0];
                Session["tablaBP"] = dt_BoardingBcbp;
                Response.Redirect("excelBPMensual.aspx?fechaI=" + sFechaDesde + "&fechaF=" + sFechaHasta + "");
            }
            else
            {
                lblMensajeError.Text = "No existen datos para exportar";
                lblMensajeErrorData.Text = "";
                grvPaginacionBoarding.Visible = false;
                grvResumen.Visible = false;
                lblTotal.Text = "";
            }
        }
    }
}
