using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using LAP.EXTRANET.UTIL;

namespace TuuaExtranet
{
    public partial class Cns_BcbpLeidosMolinete : System.Web.UI.Page
    {
        LAP.Tuua.Web.WSDAOConsulta.WSConsultas objWS = new LAP.Tuua.Web.WSDAOConsulta.WSConsultas();

        //Filtros
        string sFechaDesde, sFechaHasta;
        string sHoraDesde, sHoraHasta;
        string sFechaVuelo;
        string sNumVuelo;
        string sNumAsiento;
        string sNumBoarding;
        string sEticket;
        string sNomPasajero;
        string sEstado;

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
            //Carga combo Estado ticket
            DataTable dt_estado = new DataTable();

            dt_estado = objWS.listarCamposxNombre("EstadoBcbp");

            //Agregamos a la tabla estado el tipo VENCIDO
            DataRow newEstado = dt_estado.NewRow();

            newEstado["Cod_Campo"] = "1";
            newEstado["Dsc_Campo"] = "VENCIDO";

            dt_estado.Rows.Add(newEstado);
            FunGenerales objCombo = new FunGenerales();
            objCombo.LlenarCombo(ddlEstado, dt_estado, "Cod_Campo", "Dsc_Campo", true, false);

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
            sNumAsiento = txtNumAsiento.Text;
            sNumBoarding = txtNumBoarding.Text == "" ? "0" : txtNumBoarding.Text;
            sEticket = txtETicket.Text;
            sNomPasajero = txtNomPasajero.Text;
            sEstado = ddlEstado.SelectedValue;

            sFechaDesde = Fecha.convertToFechaSQL2(txtDesde.Text);
            sFechaHasta = Fecha.convertToFechaSQL2(txtHasta.Text);
            sFechaVuelo = Fecha.convertToFechaSQL2(txtFechaVuelo.Text);
            sHoraDesde = Fecha.convertToHoraSQL(txtHoraDesde.Text);
            sHoraHasta = Fecha.convertToHoraSQL(txtHoraHasta.Text);
        }


        private bool IsFechaValida()
        {
            int iValiFechas;
            if (txtHoraDesde.Text != "" && txtHoraHasta.Text == "")
            {
                string pNewHraDesde = txtHoraDesde.Text;
                string pNewHraHasta = txtHoraHasta.Text;
                pNewHraDesde = "23:59:59";
                pNewHraHasta = "23:59:59";

                iValiFechas = DateTime.Compare(Convert.ToDateTime(this.txtDesde.Text + " " + pNewHraDesde), Convert.ToDateTime(this.txtHasta.Text + " " + pNewHraHasta));
            }
            else
            {
                iValiFechas = DateTime.Compare(Convert.ToDateTime(this.txtDesde.Text + " " + sHoraDesde), Convert.ToDateTime(this.txtHasta.Text + " " + sHoraHasta));
            }

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

        protected void grvPaginacionBoarding_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            SaveFiltros();
            grvPaginacionBoarding.PageIndex = e.NewPageIndex;
            BindPagingGrid();
        }

        protected void grvPaginacionBoarding_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (((System.Data.DataRowView)(e.Row.DataItem)).Row["Tip_Estado"].ToString().TrimEnd() == "X")
                {
                    e.Row.BackColor = System.Drawing.Color.FromArgb(253, 234, 234);
                }
                if (((System.Data.DataRowView)(e.Row.DataItem)).Row["Tip_Estado"].ToString().TrimEnd() == "R")
                {
                    e.Row.BackColor = System.Drawing.Color.FromArgb(254, 233, 194);
                }
            }
        }

        protected void grvPaginacionBoarding_Sorting(object sender, GridViewSortEventArgs e)
        {
            SaveFiltros();
            BindPagingGrid();
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

            dt_resumen = objWS.ConsultarBoardingLeidosMolinete(sFechaDesde,
                                                               sFechaHasta,
                                                               sHoraDesde,
                                                               sHoraHasta,
                                                               sFechaVuelo,
                                                               sNumVuelo,
                                                               sNumAsiento,
                                                               sNomPasajero,
                                                               sNumBoarding,
                                                               sEticket,
                                                               sEstado, Cod_IATA, null, 0, 0,
                                                               "0", "0", "1", "0").Tables[0];

            if (dt_resumen.Columns.Contains("TotRows"))
                count = Convert.ToInt32(dt_resumen.Rows[0]["TotRows"].ToString());
            else
                lblMensajeError.Text = dt_resumen.Rows[0].ItemArray.GetValue(1).ToString();
            return count;

        }

        private DataTable GetDataPage(int pageIndex, int pageSize, string sortExpression)
        {
            DataSet ds_BP = objWS.ConsultarBoardingLeidosMolinete(sFechaDesde,
                                                                    sFechaHasta,
                                                                    sHoraDesde,
                                                                    sHoraHasta,
                                                                    sFechaVuelo,
                                                                    sNumVuelo,
                                                                    sNumAsiento,
                                                                    sNomPasajero,
                                                                    sNumBoarding,
                                                                    sEticket,
                                                                    sEstado, Cod_IATA,
                                                                    sortExpression, pageIndex, valorMaxGrilla,
                                                                    "1", "1", "0", "0");

            dt_consulta = ds_BP.Tables[0];
            dt_resumen = ds_BP.Tables[1];

            Session["tablaBoarding"] = dt_consulta;
            Session["tablaDetBoarding"] = dt_resumen;


            return dt_consulta;
        }
        #endregion

        protected void grvPaginacionBoarding_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ShowBoarding")
            {
                int rowIndex = Int32.Parse(e.CommandArgument.ToString());
                DataTable dtTicketRehabilitados = (DataTable)Session["tablaBoarding"];
                String codTicket = dtTicketRehabilitados.Rows[rowIndex]["Num_Secuencial_Bcbp"].ToString();

                String codSecuenciaBase = dtTicketRehabilitados.Rows[rowIndex]["Num_Secuencial_Bcbp"].ToString();
                String codBoarding = String.Empty;
                if (codSecuenciaBase != null && codSecuenciaBase != "" && codSecuenciaBase != "0")
                {
                    codTicket = dtTicketRehabilitados.Rows[rowIndex]["Num_Secuencial_Bcbp"].ToString();
                }
                else
                {
                    codTicket = dtTicketRehabilitados.Rows[rowIndex]["Num_Secuencial_Bcbp"].ToString();
                }

                DataTable dt_DetalleBcbp = new DataTable();
                dt_DetalleBcbp = (DataTable)Session["tablaBoarding"];
                CnsDetBoardingPass1.CargarDetalleBoardingHistorico(codTicket);
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
                dt_BoardingBcbp = objWS.ConsultarBoardingLeidosMolinete(sFechaDesde,
                                                                        sFechaHasta,
                                                                        sHoraDesde,
                                                                        sHoraHasta,
                                                                        sFechaVuelo,
                                                                        sNumVuelo,
                                                                        sNumAsiento,
                                                                        sNomPasajero,
                                                                        sNumBoarding,
                                                                        sEticket,
                                                                        sEstado, Cod_IATA,
                                                                        null, 0, 0, "0", "0", "0", "1").Tables[0];
                Session["tablaBP"] = dt_BoardingBcbp;
                Response.Redirect("excelBP.aspx?fechaI=" + sFechaDesde + "&fechaF=" + sFechaHasta + "");
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

        protected void grvResumen_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                //AGREGAMOS EL TOTAL                
                try
                {
                    DataTable dt_Total = new DataTable();
                    dt_Total = (DataTable)Session["tablaDetBoarding"];
                    Int64 total = Convert.ToInt64(dt_Total.Compute("Sum(Cantidad)", "").ToString());
                    e.Row.Cells[1].Text = "TOTAL";
                    e.Row.Cells[2].Text = total.ToString();
                }
                catch { }
            }
        }
    }
}
