using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using LAP.EXTRANET.UTIL;

namespace LAP.Tuua.Web.UserControl
{
    public partial class CnsDetBoardingPass : System.Web.UI.UserControl
    {
        WSDAOConsulta.WSConsultas objWS = new WSDAOConsulta.WSConsultas();
        DataTable dt_consulta = new DataTable();
        DataTable dt_boardingEstHist = new DataTable();
        DataTable dt_boardingEstHistAsociado = new DataTable();
        FunGenerales objUIControles = new FunGenerales();
        string Num_Secuencial;

        Int32 valorMaxGrilla;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //(string)htLabels["consDetalleDocumento.lblDetalleBoarding"];
                this.lblDetalleBoarding.Text = "Detalle de Boarding";
                this.lblCompania.Text = "Compañía:";
                this.lblFechaVuelo.Text = "Fecha Vuelo:";
                this.lblNumVuelo.Text = "Numero de Vuelo:";
                this.lblNumAsiento.Text = "Número de Asiento:";
                this.lblNombrePasajero.Text = "Pasajero:";
                this.lblTipoIngreso.Text = "Tipo Ingreso:";
                this.lblFechaVenc.Text = "Fecha Vencimiento:";
                this.lblModVenta.Text = "Modalidad Venta:";
                //this.lblNumSecuencial.Text = (string)htLabels["consDetalleDocumento.lblNumSecuencial"];
                this.lblEstadoActual.Text = "Estado Actual:";
                this.lblTPasajero.Text = "Tipo Pasajero:";
                this.lblTVuelo.Text = "Tipo Vuelo:";

                this.lblCodNumeroBCBP.Text = "Nro. Boarding:";
                this.lblCorrelativo.Text = "Nro. Secuencial";
                this.lblBCBPAsociados.Text = "Nro. Asociaciones:";

                //katy 07-01-2011
                this.lblTipoTrasbordo.Text = "Tipo Trasbordo:";
                this.lblPrecio.Text = "Precio:";
                this.lblNumeroBCBP.Text = "Nro Boarding Pass:";
                this.lblDestino.Text = "Destino:";
                this.lblETicket.Text = "ETicket:";
                this.lblNroProcRehabilitacion.Text = "Nro Proceso Rehabilitacion:";
                this.lblBloqueadoUso.Text = "Bloqueado para USO:";
                this.lblIncluyeTUUA.Text = "Incluye TUUA:";
                this.lblInvocacionWBS.Text = "Invocacion Web Service:";
            }
        }

        /*public void CargarDetalleBoarding(DataTable dt_BoardingBcbpDetalle, string CodBcbp)
        {
            try
            {
                Num_Secuencial = CodBcbp;

                if (dt_BoardingBcbpDetalle.Rows.Count > 0)
                {
                    DataRow[] foundRow = dt_BoardingBcbpDetalle.Select("Num_Secuencial_Bcbp= '" + CodBcbp + "'");

                    String MsgOK = "INVOCACION SATISFACTORIA";
                    String MsgError = "ERROR WEB SERVICE";

                    //this.lblDetNumSecuencial.Text = foundRow[0]["Num_Secuencial_Bcbp"].ToString();
                    this.lblDetCompania.Text = (foundRow[0]["Dsc_Compania"] != null) ? foundRow[0]["Dsc_Compania"].ToString() : " - ";
                    this.lblDetFechaVuelo.Text = (foundRow[0]["Fch_Vuelo"] != null && foundRow[0]["Fch_Vuelo"].ToString().Length == 10) ? foundRow[0]["Fch_Vuelo"].ToString() : " - ";
                    this.lblDetNumVuelo.Text = (foundRow[0]["Num_Vuelo"] != null) ? foundRow[0]["Num_Vuelo"].ToString() : " - ";
                    this.lblDetNumAsiento.Text = (foundRow[0]["Num_Asiento"] != null) ? foundRow[0]["Num_Asiento"].ToString() : " - ";
                    this.lblDetNomPasajero.Text = (foundRow[0]["Nom_Pasajero"] != null) ? foundRow[0]["Nom_Pasajero"].ToString() : " - ";
                    this.lblDetTipIngreso.Text = (foundRow[0]["Dsc_Tip_Ingreso"] != null) ? foundRow[0]["Dsc_Tip_Ingreso"].ToString() : " - ";

                    this.lblDetEstadoActual.Text = (foundRow[0]["Dsc_Tip_Estado"] != null) ? foundRow[0]["Dsc_Tip_Estado"].ToString() : " - ";
                    this.lblDetModVenta.Text = (foundRow[0]["Nom_Modalidad"] != null) ? foundRow[0]["Nom_Modalidad"].ToString() : " - ";
                    this.lblDetFechaVenc.Text = (foundRow[0]["Fch_Vencimiento"] != null) ? foundRow[0]["Fch_Vencimiento"].ToString() : " - ";

                    this.lblDetTPasajero.Text = (foundRow[0]["Dsc_Pasajero"] != null && foundRow[0]["Dsc_Pasajero"].ToString().Length > 0) ? foundRow[0]["Dsc_Pasajero"].ToString() : " - ";
                    this.lblDetTVuelo.Text = (foundRow[0]["Dsc_Vuelo"] != null && foundRow[0]["Dsc_Vuelo"].ToString().Length > 0) ? foundRow[0]["Dsc_Vuelo"].ToString() : " - ";

                    this.lblDetCodNumeroBCBP.Text = foundRow[0]["Cod_Numero_Bcbp"].ToString();
                    this.lblDetCorrelativo.Text = foundRow[0]["Correlativo"].ToString();

                    //katy 07-01-2011
                    this.lblDetTipoTrasbordo.Text = foundRow[0]["Tip_Trasbordo"].ToString();
                    this.lblDetPrecio.Text = foundRow[0]["Imp_Precio"].ToString();
                    this.lblDetNumeroBCBP.Text = foundRow[0]["Nro_Boarding"].ToString();
                    this.lblDetDestino.Text = foundRow[0]["Dsc_Destino"].ToString();
                    this.lblDetETicket.Text = foundRow[0]["Cod_Eticket"].ToString();
                    this.lblDetNroProcRehabilitacion.Text = foundRow[0]["Num_Proceso_Rehab"].ToString();
                    this.lblDetBloqueadoUso.Text = foundRow[0]["Flg_Bloqueado"].ToString();
                    this.lblDetIncluyeTUUA.Text = foundRow[0]["Flg_Incluye_Tuua"].ToString();
                    this.lblDetInvocacionWBS.Text = (foundRow[0]["Flg_WSError"].ToString() == "-") ? "-" :
                        (foundRow[0]["Flg_WSError"].ToString() == "0") ? MsgOK : MsgError;
                    //katy 07-01-2011
                }
            }
            catch (Exception ex)
            {
                //Flg_Error = true;
                //ErrorHandler.Trace(ErrorHandler.Desc_Mensaje, ex.Message);
            }
            finally
            {
                //if (Flg_Error)
                //{
                //    Response.Redirect("PaginaError.aspx");
                //}
            }

            try
            {
                ValorMaximoGrilla();

            }
            catch (Exception ex)
            {
                //Flg_Error = true;
                //ErrorHandler.Trace(ErrorHandler.Desc_Mensaje, ex.Message);
            }
            finally
            {
                //if (Flg_Error)
                //{
                //    Response.Redirect("PaginaError.aspx");
                //}
            }

            try
            {
                CargarDataTableBoarding();

                if (dt_boardingEstHist.Rows.Count > 0)
                {
                    grvBoardingEstHist.DataSource = dt_boardingEstHist;
                    grvBoardingEstHist.PageSize = valorMaxGrilla;
                    grvBoardingEstHist.DataBind();
                }
            }
            catch (Exception ex)
            {
                //Flg_Error = true;
                //ErrorHandler.Trace(ErrorHandler.Desc_Mensaje, ex.Message);
            }
            finally
            {
                //if (Flg_Error)
                //{
                //    Response.Redirect("PaginaError.aspx");
                //}
            }
            mpextDetBoarding.Show();
        }*/

        public void CargarDetalleBoarding(string Num_Secuencial_Bcbp)
        {

            Num_Secuencial = Num_Secuencial_Bcbp;
            try
            {
                dt_consulta = objWS.DetalleBoardingHistorica(Num_Secuencial_Bcbp);

                if (dt_consulta != null && dt_consulta.Rows.Count > 0)
                {
                    String MsgOK = "INVOCACION SATISFACTORIA";
                    String MsgError = "ERROR WEB SERVICE";

                    //this.lblDetNumSecuencial.Text = dt_consulta.Rows[0]["Num_Secuencial_Bcbp"].ToString();
                    this.lblDetCompania.Text = (dt_consulta.Rows[0]["Dsc_Compania"] != null) ? dt_consulta.Rows[0]["Dsc_Compania"].ToString() : " - ";
                    this.lblDetFechaVuelo.Text = (dt_consulta.Rows[0]["Fch_Vuelo"] != null && dt_consulta.Rows[0]["Fch_Vuelo"].ToString().Length == 8) ? Fecha.convertSQLToFecha(dt_consulta.Rows[0]["Fch_Vuelo"].ToString(), "") : " - ";
                    this.lblDetNumVuelo.Text = (dt_consulta.Rows[0]["Num_Vuelo"] != null) ? dt_consulta.Rows[0]["Num_Vuelo"].ToString() : " - ";
                    this.lblDetNumAsiento.Text = (dt_consulta.Rows[0]["Num_Asiento"] != null) ? dt_consulta.Rows[0]["Num_Asiento"].ToString() : " - ";
                    this.lblDetNomPasajero.Text = (dt_consulta.Rows[0]["Nom_Pasajero"] != null) ? dt_consulta.Rows[0]["Nom_Pasajero"].ToString() : " - ";
                    this.lblDetTipIngreso.Text = (dt_consulta.Rows[0]["Dsc_Tip_Ingreso"] != null) ? dt_consulta.Rows[0]["Dsc_Tip_Ingreso"].ToString() : " - ";

                    this.lblDetEstadoActual.Text = (dt_consulta.Rows[0]["Dsc_Estado_Actual"] != null) ? dt_consulta.Rows[0]["Dsc_Estado_Actual"].ToString() : " - ";
                    this.lblDetModVenta.Text = (dt_consulta.Rows[0]["Nom_Modalidad"] != null) ? dt_consulta.Rows[0]["Nom_Modalidad"].ToString() : " - ";
                    this.lblDetFechaVenc.Text = (dt_consulta.Rows[0]["Fch_Vencimiento"] != null) ? dt_consulta.Rows[0]["Fch_Vencimiento"].ToString() : " - ";

                    this.lblDetTPasajero.Text = (dt_consulta.Rows[0]["Dsc_Pasajero"] != null) ? dt_consulta.Rows[0]["Dsc_Pasajero"].ToString() : " - ";
                    this.lblDetTVuelo.Text = (dt_consulta.Rows[0]["Dsc_Vuelo"] != null) ? dt_consulta.Rows[0]["Dsc_Vuelo"].ToString() : " - ";

                    this.lblDetCodNumeroBCBP.Text = dt_consulta.Rows[0]["Cod_Numero_Bcbp"].ToString();
                    this.lblDetCorrelativo.Text = dt_consulta.Rows[0]["Correlativo"].ToString();

                    //katy 07-01-2011
                    this.lblDetTipoTrasbordo.Text = dt_consulta.Rows[0]["Tip_Trasbordo"].ToString();
                    this.lblDetPrecio.Text = dt_consulta.Rows[0]["Imp_Precio"].ToString();
                    this.lblDetNumeroBCBP.Text = dt_consulta.Rows[0]["Nro_Boarding"].ToString();
                    this.lblDetDestino.Text = dt_consulta.Rows[0]["Dsc_Destino"].ToString();
                    this.lblDetETicket.Text = dt_consulta.Rows[0]["Cod_Eticket"].ToString();
                    this.lblDetNroProcRehabilitacion.Text = dt_consulta.Rows[0]["Num_Proceso_Rehab"].ToString();
                    this.lblDetBloqueadoUso.Text = dt_consulta.Rows[0]["Flg_Bloqueado"].ToString();
                    this.lblDetIncluyeTUUA.Text = dt_consulta.Rows[0]["Flg_Incluye_Tuua"].ToString();
                    this.lblDetInvocacionWBS.Text = (dt_consulta.Rows[0]["Flg_WSError"].ToString() == "-") ? "-" :
                        (dt_consulta.Rows[0]["Flg_WSError"].ToString() == "0") ? MsgOK : MsgError;
                    //katy 07-01-2011
                }

            }
            catch (Exception ex)
            {
                /*Flg_Error = true;
                ErrorHandler.Trace(ErrorHandler.Desc_Mensaje, ex.Message);*/
            }
            finally
            {
                /*if (Flg_Error)
                {
                    Response.Redirect("PaginaError.aspx");
                }*/
            }
            
            try
            {
                ValorMaximoGrilla();

            }
            catch (Exception ex)
            {
                /*Flg_Error = true;
                ErrorHandler.Trace(ErrorHandler.Desc_Mensaje, ex.Message);*/
            }
            finally
            {
                /*if (Flg_Error)
                {
                    Response.Redirect("PaginaError.aspx");
                }*/
            }

            try
            {
                CargarDataTableBoarding();

                lblTituloAsociacos.Visible = false;

                if (dt_boardingEstHist.Rows.Count > 0)
                {
                    lblMensajeError.Text = "";
                    grvBoardingEstHist.DataSource = dt_boardingEstHist;
                    grvBoardingEstHist.PageSize = valorMaxGrilla;
                    grvBoardingEstHist.DataBind();
                }
                else
                {
                    lblMensajeError.Text = "El boarding no contiene detalle";
                    grvBoardingEstHist.DataSource = null;
                    grvBoardingEstHist.DataBind();
                }

                DataTable dt_parametrogeneral = objWS.ListarParametros("AA");

                if (dt_parametrogeneral.Rows.Count > 0)
                {
                    if (Convert.ToInt32(dt_parametrogeneral.Rows[0].ItemArray.GetValue(4).ToString()) == 1)
                    {
                        CargarDataTableBoardingAsociados();
                        lblTituloAsociacos.Visible = true;

                        if (dt_boardingEstHistAsociado.Rows.Count > 0)
                        {
                            lblMensajeError2.Text = "";
                            grvBoardingAsociados.DataSource = dt_boardingEstHistAsociado;
                            grvBoardingAsociados.PageSize = valorMaxGrilla;
                            grvBoardingAsociados.DataBind();
                            lblDetBCBPAsociados.Text = dt_boardingEstHistAsociado.Rows.Count.ToString();
                        }
                        else
                        {
                            lblMensajeError2.Text = "No contiene Boarding Asociados";
                            grvBoardingAsociados.DataSource = null;
                            grvBoardingAsociados.DataBind();
                            lblDetBCBPAsociados.Text = "0";
                        }
                    }
                    else {
                        lblDetBCBPAsociados.Text = "0";
                    }
                }
            }
            catch (Exception ex)
            {
                /*Flg_Error = true;
                ErrorHandler.Trace(ErrorHandler.Desc_Mensaje, ex.Message);*/
            }
            finally
            {
                /*if (Flg_Error)
                {
                    Response.Redirect("PaginaError.aspx");
                }*/
            }
            mpextDetBoarding.Show();

        }

        public void CargarDetalleBoardingHistorico(string Num_Secuencial_Bcbp)
        {

            Num_Secuencial = Num_Secuencial_Bcbp;

            String MsgOK = "INVOCACION SATISFACTORIA";
            String MsgError = "ERROR WEB SERVICE";

            try
            {
                dt_consulta = objWS.DetalleBoardingHistorica(Num_Secuencial_Bcbp);

                if (dt_consulta.Rows.Count > 0)
                {
                    //this.lblDetNumSecuencial.Text = dt_consulta.Rows[0]["Num_Secuencial_Bcbp"].ToString();
                    this.lblDetCompania.Text = (dt_consulta.Rows[0]["Dsc_Compania"] != null) ? dt_consulta.Rows[0]["Dsc_Compania"].ToString() : " - ";
                    this.lblDetFechaVuelo.Text = (dt_consulta.Rows[0]["Fch_Vuelo"] != null && dt_consulta.Rows[0]["Fch_Vuelo"].ToString().Length == 8) ? Fecha.convertSQLToFecha(dt_consulta.Rows[0]["Fch_Vuelo"].ToString(), "") : " - ";
                    this.lblDetNumVuelo.Text = (dt_consulta.Rows[0]["Num_Vuelo"] != null) ? dt_consulta.Rows[0]["Num_Vuelo"].ToString() : " - ";
                    this.lblDetNumAsiento.Text = (dt_consulta.Rows[0]["Num_Asiento"] != null) ? dt_consulta.Rows[0]["Num_Asiento"].ToString() : " - ";
                    this.lblDetNomPasajero.Text = (dt_consulta.Rows[0]["Nom_Pasajero"] != null) ? dt_consulta.Rows[0]["Nom_Pasajero"].ToString() : " - ";
                    this.lblDetTipIngreso.Text = (dt_consulta.Rows[0]["Dsc_Tip_Ingreso"] != null) ? dt_consulta.Rows[0]["Dsc_Tip_Ingreso"].ToString() : " - ";

                    this.lblDetEstadoActual.Text = (dt_consulta.Rows[0]["Dsc_Estado_Actual"] != null) ? dt_consulta.Rows[0]["Dsc_Estado_Actual"].ToString() : " - ";
                    this.lblDetModVenta.Text = (dt_consulta.Rows[0]["Nom_Modalidad"] != null) ? dt_consulta.Rows[0]["Nom_Modalidad"].ToString() : " - ";
                    this.lblDetFechaVenc.Text = (dt_consulta.Rows[0]["Fch_Vencimiento"] != null) ? dt_consulta.Rows[0]["Fch_Vencimiento"].ToString() : " - ";

                    this.lblDetTPasajero.Text = (dt_consulta.Rows[0]["Dsc_Pasajero"] != null) ? dt_consulta.Rows[0]["Dsc_Pasajero"].ToString() : " - ";
                    this.lblDetTVuelo.Text = (dt_consulta.Rows[0]["Dsc_Vuelo"] != null) ? dt_consulta.Rows[0]["Dsc_Vuelo"].ToString() : " - ";

                    this.lblDetCodNumeroBCBP.Text = dt_consulta.Rows[0]["Cod_Numero_Bcbp"].ToString();
                    this.lblDetCorrelativo.Text = dt_consulta.Rows[0]["Correlativo"].ToString();

                    //katy 11-01-2011
                    this.lblDetTipoTrasbordo.Text = dt_consulta.Rows[0]["Tip_Trasbordo"].ToString();
                    this.lblDetPrecio.Text = dt_consulta.Rows[0]["Imp_Precio"].ToString();
                    this.lblDetNumeroBCBP.Text = dt_consulta.Rows[0]["Nro_Boarding"].ToString();
                    this.lblDetDestino.Text = dt_consulta.Rows[0]["Dsc_Destino"].ToString();
                    this.lblDetETicket.Text = dt_consulta.Rows[0]["Cod_Eticket"].ToString();
                    this.lblDetNroProcRehabilitacion.Text = dt_consulta.Rows[0]["Num_Proceso_Rehab"].ToString();
                    this.lblDetBloqueadoUso.Text = dt_consulta.Rows[0]["Flg_Bloqueado"].ToString();
                    this.lblDetIncluyeTUUA.Text = dt_consulta.Rows[0]["Flg_Incluye_Tuua"].ToString();
                    this.lblDetInvocacionWBS.Text = (dt_consulta.Rows[0]["Flg_WSError"].ToString() == "-") ? "-" :
                        (dt_consulta.Rows[0]["Flg_WSError"].ToString() == "0") ? MsgOK : MsgError;
                }
            }
            catch (Exception ex)
            {
                /*Flg_Error = true;
                ErrorHandler.Trace(ErrorHandler.Desc_Mensaje, ex.Message);*/
            }
            finally
            {
                /*if (Flg_Error)
                {
                    Response.Redirect("PaginaError.aspx");
                }*/
            }

            try
            {
                ValorMaximoGrilla();
            }
            catch (Exception ex)
            {
                /*Flg_Error = true;
                ErrorHandler.Trace(ErrorHandler.Desc_Mensaje, ex.Message);*/
            }
            finally
            {
                /*if (Flg_Error)
                {
                    Response.Redirect("PaginaError.aspx");
                }*/
            }

            try
            {
                CargarDataTableBoarding();

                lblTituloAsociacos.Visible = false;

                if (dt_boardingEstHist.Rows.Count > 0)
                {
                    lblMensajeError.Text = "";
                    grvBoardingEstHist.DataSource = dt_boardingEstHist;
                    grvBoardingEstHist.PageSize = valorMaxGrilla;
                    grvBoardingEstHist.DataBind();
                }
                else
                {
                    lblMensajeError.Text = "El boarding no contiene detalle";
                    grvBoardingEstHist.DataSource = null;
                    grvBoardingEstHist.DataBind();
                }

                DataTable dt_parametrogeneral = objWS.ListarParametros("AA");

                if (dt_parametrogeneral.Rows.Count > 0)
                {
                    if (Convert.ToInt32(dt_parametrogeneral.Rows[0].ItemArray.GetValue(4).ToString()) == 1)
                    {
                        CargarDataTableBoardingAsociados();
                        lblTituloAsociacos.Visible = true;

                        if (dt_boardingEstHistAsociado.Rows.Count > 0)
                        {
                            lblMensajeError2.Text = "";
                            grvBoardingAsociados.DataSource = dt_boardingEstHistAsociado;
                            grvBoardingAsociados.PageSize = valorMaxGrilla;
                            grvBoardingAsociados.DataBind();
                            lblDetBCBPAsociados.Text = dt_boardingEstHistAsociado.Rows.Count.ToString();
                        }
                        else
                        {
                            lblMensajeError2.Text = "No contiene Boarding Asociados";
                            grvBoardingAsociados.DataSource = null;
                            grvBoardingAsociados.DataBind();
                            lblDetBCBPAsociados.Text = "0";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                /*Flg_Error = true;
                ErrorHandler.Trace(ErrorHandler.Desc_Mensaje, ex.Message);*/
            }
            finally
            {
                /*if (Flg_Error)
                {
                    Response.Redirect("PaginaError.aspx");
                }*/
            }

            mpextDetBoarding.Show();

        }

        protected void ValorMaximoGrilla()
        {
            DataTable dt_parametro = new DataTable();
            dt_parametro = objWS.ListarParametros("LG");

            if (dt_parametro.Rows.Count > 0)
            {
                valorMaxGrilla = Convert.ToInt32(dt_parametro.Rows[0].ItemArray.GetValue(4).ToString());
            }
        }

        protected void CargarDataTableBoarding()
        {
            dt_boardingEstHist = objWS.DetalleBoardingEstHist(Num_Secuencial);
            ViewState["DetalleBoarding"] = dt_boardingEstHist;
        }

        //grvBoardingAsociados
        protected void CargarDataTableBoardingAsociados()
        {
            dt_boardingEstHistAsociado = objWS.ListarBoardingAsociados(Num_Secuencial);
            ViewState["DetalleBoardingAsociados"] = dt_boardingEstHistAsociado;
        }

        protected void grvBoardingEstHist_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvBoardingEstHist.PageIndex = e.NewPageIndex;
            grvBoardingEstHist.DataSource = (DataTable)ViewState["DetalleTicket"];
            grvBoardingEstHist.DataBind();
        }

        protected void grvBoardingEstHist_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression;

            if (GridViewSortDirection == System.Web.UI.WebControls.SortDirection.Ascending)
            {
                GridViewSortDirection = System.Web.UI.WebControls.SortDirection.Descending;

                this.txtOrdenacionDet.Text = "DESC";
                SortGridView(sortExpression, "DESC");
            }
            else
            {
                GridViewSortDirection = System.Web.UI.WebControls.SortDirection.Ascending;

                this.txtOrdenacionDet.Text = "ASC";
                SortGridView(sortExpression, "ASC");
            }
            this.txtColumnaDet.Text = sortExpression;
        }

        public System.Web.UI.WebControls.SortDirection GridViewSortDirection
        {
            get
            {
                if (ViewState["SortDirection"] == null)
                {
                    ViewState["SortDirection"] = System.Web.UI.WebControls.SortDirection.Ascending;
                }
                return (SortDirection)ViewState["SortDirection"];

            }
            set
            {
                ViewState["SortDirection"] = value;
            }
        }

        private void SortGridView(string sortExpression, String direction)
        {
            //dt_consulta = objListarTicketxFecha.ListarTicketxFecha(sFechaDesde, sFechaHasta, sIdCompania, sTipoTicket, sEstadoTicket, sPersona, sFiltro, sTipoOperacion, sHoraDesde, sHoraHasta, null);
            ViewState["DetalleBoarding"] = objUIControles.ConvertDataTable(dwConsulta((DataTable)ViewState["DetalleBoarding"], sortExpression, direction));
            grvBoardingEstHist.DataSource = (DataTable)ViewState["DetalleBoarding"];
            ValorMaximoGrilla();
            grvBoardingEstHist.PageSize = valorMaxGrilla;
            grvBoardingEstHist.DataBind();
        }

        protected DataView dwConsulta(DataTable dtConsulta, string sortExpression, String direction)
        {
            DataView dv = new DataView(dtConsulta);

            if (txtOrdenacionDet.Text.CompareTo("") != 0)
            {
                dv.Sort = sortExpression + " " + direction;
            }

            return dv;
        }

        protected void grvBoardingEstHist_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string Tip_Estado = System.Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Tip_Estado"));
                if (Tip_Estado.Trim().ToUpper().Equals("R"))
                {
                    //e.Row.BackColor = System.Drawing.Color.Red;
                    e.Row.BackColor = System.Drawing.Color.FromArgb(254, 233, 194);
                }
                if (Tip_Estado.Trim().ToUpper().Equals("X"))
                {
                    //e.Row.BackColor = System.Drawing.Color.Red;
                    e.Row.BackColor = System.Drawing.Color.FromArgb(253, 234, 234);
                }
            }
        }
    }
}