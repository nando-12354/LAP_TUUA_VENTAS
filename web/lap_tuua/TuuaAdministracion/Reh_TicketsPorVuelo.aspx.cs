using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using LAP.TUUA.CONTROL;
using LAP.TUUA.UTIL;
using LAP.TUUA.ENTIDADES;
using LAP.TUUA.ALARMAS;

public partial class Reh_TicketsPorVuelo : System.Web.UI.Page
{
    protected Hashtable htLabels;
    bool flagError;
    private BO_Consultas objBOConsultas = new BO_Consultas();
    private BO_Rehabilitacion objBORehabilitacion = new BO_Rehabilitacion();

    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        spnRehabilitar.Text = "";
        lblErrorMsg.Text = "";

        if (!IsPostBack)
        {
            Session["tabla"] = null;//Elimino la tabla real.        //EAG 21/10/2010

            htLabels = LabelConfig.htLabels;
            try
            {
                btnRehabilitar.Text = htLabels["rehabilitacionticketPorVuelo.btnRehabilitar"].ToString();
                lblCia.Text = htLabels["rehabilitacionticketPorVuelo.lblCia"].ToString();
                lblFechaVuelo.Text = htLabels["rehabilitacionticketPorVuelo.lblFechaVuelo"].ToString();
                lblVuelo.Text = htLabels["rehabilitacionticketPorVuelo.lblVuelo"].ToString();
                lblMotivo.Text = htLabels["rehabilitacionticketPorVuelo.lblMotivo"].ToString();
                lblTotalSeleccionados.Text = htLabels["rehabilitacionticketPorVuelo.lblTotalSeleccionados"].ToString();
                lblTotalIngresados.Text = htLabels["rehabilitacionticketPorVuelo.lblTotalIngresados"].ToString();
                lblConformidad.Text = htLabels["rehabilitacionticketPorVuelo.lblConformidad"].ToString();

                DataTable dtActivarConsRepreReh = objBOConsultas.ListarParametros("CR");
                int activarConsRepre = Int32.Parse(dtActivarConsRepreReh.Rows[0]["Valor"].ToString());
                if (activarConsRepre == 1)
                {
                    lnkRepresentante.Visible = true;
                    lblConsRepresentante.Text = htLabels["rehabilitacionticketPorVuelo.lblConsRepresentante"].ToString();
                }
                else
                {
                    lnkRepresentante.Visible = false;
                }

                DataTable dt_parametro = objBOConsultas.ListarParametros("LG");
                if (dt_parametro.Rows.Count > 0)
                {
                    gwvRehabilitarPorTicket.PageSize = Int32.Parse(Convert.ToString(dt_parametro.Rows[0].ItemArray.GetValue(4).ToString()));
                }
                //else el default es 10.

                int maxRehabilitaciones = 0;
                dt_parametro = objBOConsultas.ListarParametros("RTM");
                if (dt_parametro.Rows.Count > 0)
                {
                    maxRehabilitaciones = Int32.Parse(Convert.ToString(dt_parametro.Rows[0].ItemArray.GetValue(4).ToString()));
                }
                else
                {
                    maxRehabilitaciones = 800;
                }

                lblDescripcionLimite.Text = String.Format("Solo es posible Rehabilitar {0} TICKET por proceso", maxRehabilitaciones); 



            }
            catch (Exception ex)
            {
                flagError = true;
                ErrorHandler.Cod_Error = Define.ERR_008;
                ErrorHandler.Trace(ErrorHandler.htErrorType[Define.ERR_008].ToString(), ex.Message);
            }
            finally
            {
                if (flagError)
                    Response.Redirect("PaginaError.aspx");

            }

            #region Llenando combos de busqueda: Lleno las companias y vacio el cboVuelo
            DataTable dtCompanias = objBOConsultas.listarAllCompania();
            cboCompanias.DataSource = dtCompanias;
            cboCompanias.DataTextField = "Dsc_Compania";
            cboCompanias.DataValueField = "Cod_Compania";
            cboCompanias.DataBind();
            cboCompanias.Items.Insert(0, "Seleccionar");

            cboVuelo.Items.Insert(0, "Seleccionar");
            #endregion

            #region Agregando fila vacia a la grilla por default
            DataTable dtTicketRehabilitados = new DataTable();
            dtTicketRehabilitados.Columns.Add("Cod_Numero_Ticket", System.Type.GetType("System.String"));
            dtTicketRehabilitados.Columns.Add("Observacion", System.Type.GetType("System.String"));
            dtTicketRehabilitados.Columns.Add("Numero", System.Type.GetType("System.String"));
            dtTicketRehabilitados.Rows.Add(dtTicketRehabilitados.NewRow());
            gwvRehabilitarPorTicket.DataSource = dtTicketRehabilitados;
            gwvRehabilitarPorTicket.DataBind();

            gwvRehabilitarPorTicket.Rows[0].Cells[0].Text = "";
            gwvRehabilitarPorTicket.Rows[0].FindControl("btnEliminar").Visible = false;
            gwvRehabilitarPorTicket.Rows[0].FindControl("chkSeleccionar").Visible = false;
            #endregion

            #region Llenando data del combo de causal
            DataTable dtCausalReh = objBOConsultas.ListaCamposxNombre("CausalRehabilitacion");
            cboMotivo.DataSource = dtCausalReh;
            cboMotivo.DataTextField = "Dsc_Campo";
            cboMotivo.DataValueField = "Cod_Campo";
            cboMotivo.DataBind();
            #endregion
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "<script language=\"javascript\">CheckBoxHeaderGrilla();</script>", false);
        }

    }
    #endregion

    #region lnkRepresentante_Click
    protected void lnkRepresentante_Click(object sender, EventArgs e)
    {
        consRepre.Inicio();
    }
    #endregion

    #region cboCompanias_SelectedIndexChanged
    protected void cboCompanias_SelectedIndexChanged(object sender, EventArgs e)
    {
        #region Agregando fila vacia a la grilla por default y eliminado la tabla viewstate.
        DataTable dtTicketRehabilitados = new DataTable();
        dtTicketRehabilitados.Columns.Add("Cod_Numero_Ticket", System.Type.GetType("System.String"));
        dtTicketRehabilitados.Columns.Add("Observacion", System.Type.GetType("System.String"));
        dtTicketRehabilitados.Columns.Add("Numero", System.Type.GetType("System.String"));
        dtTicketRehabilitados.Rows.Add(dtTicketRehabilitados.NewRow());
        gwvRehabilitarPorTicket.DataSource = dtTicketRehabilitados;
        gwvRehabilitarPorTicket.DataBind();

        gwvRehabilitarPorTicket.Rows[0].Cells[0].Text = "";
        gwvRehabilitarPorTicket.Rows[0].FindControl("btnEliminar").Visible = false;
        gwvRehabilitarPorTicket.Rows[0].FindControl("chkSeleccionar").Visible = false;

        lblTxtIngresados.Text = "0";
        lblTxtSeleccionados.Text = "0 (0 Observaciones / 0 Normales)";
        hdNumSelConObs.Value = "0";
        hdNumSelTotal.Value = "0";

        Session["tabla"] = null;//Elimino la tabla real.
        #endregion

        //Borro la fecha ingresada
        txtFechaVuelo.Text = "";
        
        #region Vaciando el combo de vuelos
        cboVuelo.Items.Clear();
        cboVuelo.Items.Insert(0, "Seleccionar");
        #endregion

    }
    #endregion

    #region txtFecha_Vuelo_TextChanged
    protected void txtFechaVuelo_TextChanged(object sender, EventArgs e)
    {
        if (cboCompanias.SelectedIndex > 0 && txtFechaVuelo.Text.Length > 0)
        {
            String sCompania = cboCompanias.SelectedItem.Value;
            String fechaVueloAux = txtFechaVuelo.Text;
            String fechaVuelo = fechaVueloAux.Substring(6, 4) + fechaVueloAux.Substring(3, 2) + fechaVueloAux.Substring(0, 2);

            DataTable dtVuelos = objBORehabilitacion.consultarVuelosTicketPorCiaFecha(sCompania, fechaVuelo);
            cboVuelo.DataSource = dtVuelos;
            cboVuelo.DataTextField = "Dsc_Num_Vuelo";
            //cboVuelo.DataValueField = "";
            cboVuelo.DataBind();

            cboVuelo.Items.Insert(0, "Seleccionar");

            #region Agregando fila vacia a la grilla por default y eliminado la tabla viewstate.
            DataTable dtTicketRehabilitados = new DataTable();
            dtTicketRehabilitados.Columns.Add("Cod_Numero_Ticket", System.Type.GetType("System.String"));
            dtTicketRehabilitados.Columns.Add("Observacion", System.Type.GetType("System.String"));
            dtTicketRehabilitados.Columns.Add("Numero", System.Type.GetType("System.String"));
            dtTicketRehabilitados.Rows.Add(dtTicketRehabilitados.NewRow());
            gwvRehabilitarPorTicket.DataSource = dtTicketRehabilitados;
            gwvRehabilitarPorTicket.DataBind();

            gwvRehabilitarPorTicket.Rows[0].Cells[0].Text = "";
            gwvRehabilitarPorTicket.Rows[0].FindControl("btnEliminar").Visible = false;
            gwvRehabilitarPorTicket.Rows[0].FindControl("chkSeleccionar").Visible = false;

            lblTxtIngresados.Text = "0";
            lblTxtSeleccionados.Text = "0 (0 Observaciones / 0 Normales)";
            hdNumSelConObs.Value = "0";
            hdNumSelTotal.Value = "0";

            Session["tabla"] = null;//Elimino la tabla real.
            #endregion
        }
    }
    #endregion

    #region cboVuelo_OnSelectedIndexChanged

    protected void cboVuelo_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (cboCompanias.SelectedIndex > 0 && txtFechaVuelo.Text.Length > 0 /*&& cboVuelo.SelectedIndex > 0*/)
        {
            if(cboVuelo.SelectedIndex==0)
            {
                #region Agregando fila vacia a la grilla por default y eliminado la tabla viewstate.
                DataTable dtTicketRehabilitados = new DataTable();
                dtTicketRehabilitados.Columns.Add("Cod_Numero_Ticket", System.Type.GetType("System.String"));
                dtTicketRehabilitados.Columns.Add("Observacion", System.Type.GetType("System.String"));
                dtTicketRehabilitados.Columns.Add("Numero", System.Type.GetType("System.String"));
                dtTicketRehabilitados.Rows.Add(dtTicketRehabilitados.NewRow());
                gwvRehabilitarPorTicket.DataSource = dtTicketRehabilitados;
                gwvRehabilitarPorTicket.DataBind();

                gwvRehabilitarPorTicket.Rows[0].Cells[0].Text = "";
                gwvRehabilitarPorTicket.Rows[0].FindControl("btnEliminar").Visible = false;
                gwvRehabilitarPorTicket.Rows[0].FindControl("chkSeleccionar").Visible = false;

                lblTxtIngresados.Text = "0";
                lblTxtSeleccionados.Text = "0 (0 Observaciones / 0 Normales)";
                hdNumSelConObs.Value = "0";
                hdNumSelTotal.Value = "0";

                Session["tabla"] = null;//Elimino la tabla real.
                #endregion
                //return;
            }
            else
            {
                String sCompania = cboCompanias.SelectedItem.Value;
                String fechaVueloAux = txtFechaVuelo.Text;
                String fechaVuelo = fechaVueloAux.Substring(6, 4) + fechaVueloAux.Substring(3, 2) + fechaVueloAux.Substring(0, 2);
                String dsc_Num_Vuelo = cboVuelo.SelectedValue;

                DataTable dtTicketDetalle = objBORehabilitacion.consultarTicketsPorVuelo(sCompania, fechaVuelo, dsc_Num_Vuelo);

                DataTable dtTicketRehabilitados = new DataTable();
                dtTicketRehabilitados.Columns.Add("Numero", System.Type.GetType("System.String"));
                dtTicketRehabilitados.Columns.Add("Cod_Numero_Ticket", System.Type.GetType("System.String"));
                dtTicketRehabilitados.Columns.Add("Observacion", System.Type.GetType("System.String"));
                dtTicketRehabilitados.Columns.Add("Check", System.Type.GetType("System.Boolean"));

                //No es necesario verificar dtTicketDetalle.Rows.Count > 0 pues si viene si o si data.

                DataRow row;
                DataTable dtParametroGeneral = objBOConsultas.ListarParametros("MR");
                int MaxRehabilitaciones = Int32.Parse(dtParametroGeneral.Rows[0]["Valor"].ToString());
                for (int i = 0; i < dtTicketDetalle.Rows.Count; i++)
                {
                    String estado = dtTicketDetalle.Rows[i]["Tip_Estado_Actual"].ToString();
                    if(estado.Trim().ToUpper().Equals("U"))//Solo se agregan los que estan en estado USADO.
                    {
                        row = dtTicketRehabilitados.NewRow();
                        row["Numero"] = i + 1;
                        row["Cod_Numero_Ticket"] = dtTicketDetalle.Rows[i]["Cod_Numero_Ticket"].ToString();
                        String observaciones = "";
                        #region Observaciones

                        int numRehabilitaciones = Int32.Parse(dtTicketDetalle.Rows[i]["Num_Rehabilitaciones"].ToString());
                        if (numRehabilitaciones >= MaxRehabilitaciones)
                        {
                            observaciones = numRehabilitaciones + " Rehabilitaciones";
                        }
                        //if (estado.Equals("B"))
                        //{
                        //    if (!observaciones.Equals(String.Empty))
                        //        observaciones += " - ";
                        //    observaciones += "Borrado";
                        //}
                        //else if (estado.Equals("X"))
                        //{
                        //    if (!observaciones.Equals(String.Empty))
                        //        observaciones += " - ";
                        //    observaciones += "Anulado";
                        //}
                        if (observaciones.Equals(String.Empty))
                        {
                            observaciones = "-";
                        }
                        #endregion
                        row["Observacion"] = observaciones;
                        row["Check"] = false;
                        dtTicketRehabilitados.Rows.Add(row);                        
                    }
                }
                if(dtTicketRehabilitados.Rows.Count==0)
                {
                    Session["tabla"] = null;//Elimino la tabla real.

                    #region Agregando fila vacia a la grilla por default
                    dtTicketRehabilitados.Rows.Add(dtTicketRehabilitados.NewRow());
                    gwvRehabilitarPorTicket.DataSource = dtTicketRehabilitados;
                    gwvRehabilitarPorTicket.DataBind();

                    gwvRehabilitarPorTicket.Rows[0].Cells[0].Text = "";
                    gwvRehabilitarPorTicket.Rows[0].FindControl("btnEliminar").Visible = false;
                    gwvRehabilitarPorTicket.Rows[0].FindControl("chkSeleccionar").Visible = false;

                    lblTxtIngresados.Text = "0";
                    lblTxtSeleccionados.Text = "0 (0 Observaciones / 0 Normales)";
                    hdNumSelConObs.Value = "0";
                    hdNumSelTotal.Value = "0";
                   
                    #endregion                    

                    lblErrorMsg.Text = "No se encontro resultado alguno.";
                }
                else
                {
                    Session["tabla"] = dtTicketRehabilitados;
                    gwvRehabilitarPorTicket.PageIndex = 0;
                    gwvRehabilitarPorTicket.DataSource = dtTicketRehabilitados;
                    gwvRehabilitarPorTicket.DataBind();

                    lblTxtIngresados.Text = dtTicketRehabilitados.Rows.Count.ToString();
                    lblTxtSeleccionados.Text = "0 (0 Observaciones / 0 Normales)";
                    hdNumSelConObs.Value = "0";
                    hdNumSelTotal.Value = "0";
                }
            }
        }
    }
    #endregion

    #region btnRefresh_Click
    protected void btnRefresh_Click(object sender, ImageClickEventArgs e)
    {
        if (cboCompanias.SelectedIndex > 0 && txtFechaVuelo.Text.Length > 0 && cboVuelo.SelectedIndex > 0)
        {
            String sCompania = cboCompanias.SelectedItem.Value;
            String fechaVueloAux = txtFechaVuelo.Text;
            String fechaVuelo = fechaVueloAux.Substring(6, 4) + fechaVueloAux.Substring(3, 2) + fechaVueloAux.Substring(0, 2);
            String dsc_Num_Vuelo = cboVuelo.SelectedValue;

            DataTable dtTicketDetalle = objBORehabilitacion.consultarTicketsPorVuelo(sCompania, fechaVuelo, dsc_Num_Vuelo);

            DataTable dtTicketRehabilitados = new DataTable();
            dtTicketRehabilitados.Columns.Add("Numero", System.Type.GetType("System.String"));
            dtTicketRehabilitados.Columns.Add("Cod_Numero_Ticket", System.Type.GetType("System.String"));
            dtTicketRehabilitados.Columns.Add("Observacion", System.Type.GetType("System.String"));
            dtTicketRehabilitados.Columns.Add("Check", System.Type.GetType("System.Boolean"));

            //No es necesario verificar dtTicketDetalle.Rows.Count > 0 pues si viene si o si data.

            DataRow row;
            DataTable dtParametroGeneral = objBOConsultas.ListarParametros("MR");
            int MaxRehabilitaciones = Int32.Parse(dtParametroGeneral.Rows[0]["Valor"].ToString());
            for (int i = 0; i < dtTicketDetalle.Rows.Count; i++)
            {
                String estado = dtTicketDetalle.Rows[i]["Tip_Estado_Actual"].ToString();
                if (estado.Trim().ToUpper().Equals("U"))//Solo se agregan los que estan en estado USADO.
                {
                    row = dtTicketRehabilitados.NewRow();
                    row["Numero"] = i + 1;
                    row["Cod_Numero_Ticket"] = dtTicketDetalle.Rows[i]["Cod_Numero_Ticket"].ToString();
                    String observaciones = "";

                    #region Observaciones

                    int numRehabilitaciones = Int32.Parse(dtTicketDetalle.Rows[i]["Num_Rehabilitaciones"].ToString());
                    if (numRehabilitaciones >= MaxRehabilitaciones)
                    {
                        observaciones = numRehabilitaciones + " Rehabilitaciones";
                    }
                    //if (estado.Equals("B"))
                    //{
                    //    if (!observaciones.Equals(String.Empty))
                    //        observaciones += " - ";
                    //    observaciones += "Borrado";
                    //}
                    //else if (estado.Equals("X"))
                    //{
                    //    if (!observaciones.Equals(String.Empty))
                    //        observaciones += " - ";
                    //    observaciones += "Anulado";
                    //}
                    if (observaciones.Equals(String.Empty))
                    {
                        observaciones = "-";
                    }

                    #endregion

                    row["Observacion"] = observaciones;
                    row["Check"] = false;
                    dtTicketRehabilitados.Rows.Add(row);
                }
            }
            if (dtTicketRehabilitados.Rows.Count == 0)
            {
                Session["tabla"] = null;//Elimino la tabla real.

                #region Agregando fila vacia a la grilla por default
                dtTicketRehabilitados.Rows.Add(dtTicketRehabilitados.NewRow());
                gwvRehabilitarPorTicket.DataSource = dtTicketRehabilitados;
                gwvRehabilitarPorTicket.DataBind();

                gwvRehabilitarPorTicket.Rows[0].Cells[0].Text = "";
                gwvRehabilitarPorTicket.Rows[0].FindControl("btnEliminar").Visible = false;
                gwvRehabilitarPorTicket.Rows[0].FindControl("chkSeleccionar").Visible = false;

                lblTxtIngresados.Text = "0";
                lblTxtSeleccionados.Text = "0 (0 Observaciones / 0 Normales)";
                hdNumSelConObs.Value = "0";
                hdNumSelTotal.Value = "0";

                #endregion    
                
                lblErrorMsg.Text = "No se encontro resultado alguno.";
            }
            else
            {
                Session["tabla"] = dtTicketRehabilitados;
                gwvRehabilitarPorTicket.PageIndex = 0;
                gwvRehabilitarPorTicket.DataSource = dtTicketRehabilitados;
                gwvRehabilitarPorTicket.DataBind();

                lblTxtIngresados.Text = dtTicketRehabilitados.Rows.Count.ToString();
                lblTxtSeleccionados.Text = "0 (0 Observaciones / 0 Normales)";
                hdNumSelConObs.Value = "0";
                hdNumSelTotal.Value = "0";
            }


        }
    }
    #endregion

    #region gwvRehabilitarPorTicket_RowDataBound
    protected void gwvRehabilitarPorTicket_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string Observacion = System.Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Observacion"));
            if (!Observacion.Equals("-"))
            {
                e.Row.Cells[2].ForeColor = Color.Red;
            }
        }
    }
    #endregion

    #region gwvRehabilitarPorTicket_RowCommand

    protected void gwvRehabilitarPorTicket_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("Eliminar"))
        {
            int rowIndex = Int32.Parse(e.CommandArgument.ToString());
            DataTable dtTicketRehabilitados = (DataTable)Session["tabla"];

            int pageIndex = gwvRehabilitarPorTicket.PageIndex;
            int pageSize = gwvRehabilitarPorTicket.PageSize;
            int pageCount = gwvRehabilitarPorTicket.PageCount;

            #region Actualizar Resumen
            Boolean isChecked = ((CheckBox)(gwvRehabilitarPorTicket.Rows[rowIndex - (pageIndex * pageSize)].FindControl("chkSeleccionar"))).Checked;
            String observaciones = dtTicketRehabilitados.Rows[rowIndex]["Observacion"].ToString();
            #endregion
            dtTicketRehabilitados.Rows.RemoveAt(rowIndex);
            //dtTicketRehabilitados.AcceptChanges();

            #region Guardo las selecciones del combo y checkbox
            int limite;
            if ((pageIndex + 1) < pageCount)
            {
                limite = pageSize;
            }
            else
            {
                limite = dtTicketRehabilitados.Rows.Count + 1 - (pageIndex * pageSize);//Sumarle 1 pues lo removio.
            }
            for (int i = (pageIndex * pageSize), j = 0; j < limite; i++, j++)
            {
                if (j != rowIndex - (pageIndex * pageSize))
                {
                    //DropDownList cboMotivo = (DropDownList)gwvRehabilitarPorTicket.Rows[j].FindControl("cboMotivo");
                    CheckBox chkSeleccionar = (CheckBox)gwvRehabilitarPorTicket.Rows[j].FindControl("chkSeleccionar");
                    //dtTicketRehabilitados.Rows[i]["Motivo"] = cboMotivo.SelectedItem.Text;
                    dtTicketRehabilitados.Rows[i]["Check"] = chkSeleccionar.Checked;
                    //dtTicketRehabilitados.Rows[i]["Numero"] = i + 1;
                }
                else
                {
                    i--;
                }
            }
            #endregion
            for (int i=0; i<dtTicketRehabilitados.Rows.Count;i++)
            {
                dtTicketRehabilitados.Rows[i]["Numero"] = i + 1;
            }

            #region Define el PageIndex
            if (dtTicketRehabilitados.Rows.Count == (pageIndex * pageSize) && pageIndex != 0)//esta ultima condicion por el eliminar el unico elemento
            {
                gwvRehabilitarPorTicket.PageIndex = pageIndex - 1;
            }
            #endregion

            if (dtTicketRehabilitados.Rows.Count == 0)
            {
                Session["tabla"] = null;
                #region Agregando fila vacia a la grilla por default
                dtTicketRehabilitados.Rows.Add(dtTicketRehabilitados.NewRow());

                gwvRehabilitarPorTicket.DataSource = dtTicketRehabilitados;
                gwvRehabilitarPorTicket.DataBind();

                //gwvRehabilitarPorTicket.Rows[0].Cells[5].Controls.Clear();
                gwvRehabilitarPorTicket.Rows[0].Cells[0].Text = "";
                gwvRehabilitarPorTicket.Rows[0].FindControl("btnEliminar").Visible = false;
                gwvRehabilitarPorTicket.Rows[0].FindControl("chkSeleccionar").Visible = false;
                //gwvRehabilitarPorTicket.Rows[0].FindControl("cboMotivo").Visible = false;
                #endregion
            }
            else
            {
                gwvRehabilitarPorTicket.DataSource = dtTicketRehabilitados;
                gwvRehabilitarPorTicket.DataBind();

                #region Lleno el combo. Ademas actualizo la seleccion del combo y el check.
                //DataTable dtCausalReh = objBOConsultas.ListaCamposxNombreOrderByDesc("CausalRehabilitacion");

                pageIndex = gwvRehabilitarPorTicket.PageIndex;
                pageCount = gwvRehabilitarPorTicket.PageCount;

                if ((pageIndex + 1) < pageCount)
                {
                    limite = pageSize;
                }
                else
                {
                    limite = dtTicketRehabilitados.Rows.Count - (pageIndex * pageSize);
                }

                for (int i = (pageIndex * pageSize), j = 0; j < limite; i++, j++)
                {
                    //DropDownList cboMotivo = (DropDownList)gwvRehabilitarPorTicket.Rows[j].FindControl("cboMotivo");
                    //cboMotivo.DataSource = dtCausalReh;
                    //cboMotivo.DataTextField = "Dsc_Campo";
                    //cboMotivo.DataValueField = "Cod_Campo";
                    //cboMotivo.DataBind();

                    //cboMotivo.ClearSelection();
                    //cboMotivo.Items.FindByText(dtTicketRehabilitados.Rows[i]["Motivo"].ToString()).Selected = true;
                    CheckBox chkSeleccionar = (CheckBox)gwvRehabilitarPorTicket.Rows[j].FindControl("chkSeleccionar");
                    chkSeleccionar.Checked = (Boolean)dtTicketRehabilitados.Rows[i]["Check"];
                }
                #endregion

            }

            #region Actualizar Resumen
            if(isChecked)
            {
                hdNumSelTotal.Value = "" + (Int32.Parse(hdNumSelTotal.Value) - 1);
                if (!observaciones.Equals("-"))
                    hdNumSelConObs.Value = "" + (Int32.Parse(hdNumSelConObs.Value) - 1);
                int normales = Int32.Parse(hdNumSelTotal.Value) - Int32.Parse(hdNumSelConObs.Value);
                lblTxtSeleccionados.Text = hdNumSelTotal.Value + " (" + hdNumSelConObs.Value + " Observaciones / " + normales + " Normales)";
            }
            lblTxtIngresados.Text = "" + (Int32.Parse(lblTxtIngresados.Text) - 1);
            #endregion
        }
        else if (e.CommandName.Equals("ShowTicket"))
        {
            int rowIndex = Int32.Parse(e.CommandArgument.ToString());
            DataTable dtTicketRehabilitados = (DataTable)Session["tabla"];
            String codTicket = dtTicketRehabilitados.Rows[rowIndex]["Cod_Numero_Ticket"].ToString()+"-"+dtTicketRehabilitados.Rows[rowIndex]["Cod_Numero_Ticket"].ToString();
            //consTicket.Inicio(codTicket);
            ConsDetTicket.Inicio(codTicket);
        }
    }

    #endregion

    #region Paginacion

    protected void gwvRehabilitarPorTicket_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        int pageIndex;
        int pageSize = gwvRehabilitarPorTicket.PageSize;

        DataTable dtTicketRehabilitados = (DataTable)Session["tabla"];

        #region Guardo las selecciones del combo y checkbox
        pageIndex = gwvRehabilitarPorTicket.PageIndex;
        int limite;
        if ((pageIndex + 1) < gwvRehabilitarPorTicket.PageCount)
        {
            limite = pageSize;
        }
        else
        {
            limite = dtTicketRehabilitados.Rows.Count - (pageIndex * pageSize);
        }

        for (int i = (pageIndex * pageSize), j = 0; j < limite; i++, j++)
        {
            //DropDownList cboMotivo = (DropDownList)gwvRehabilitarPorTicket.Rows[j].FindControl("cboMotivo");
            CheckBox chkSeleccionar = (CheckBox)gwvRehabilitarPorTicket.Rows[j].FindControl("chkSeleccionar");
            //dtTicketRehabilitados.Rows[i]["Motivo"] = cboMotivo.SelectedItem.Text;
            dtTicketRehabilitados.Rows[i]["Check"] = chkSeleccionar.Checked;
        }
        #endregion

        gwvRehabilitarPorTicket.PageIndex = e.NewPageIndex;

        gwvRehabilitarPorTicket.DataSource = dtTicketRehabilitados;
        gwvRehabilitarPorTicket.DataBind();

        #region Lleno el combo. Ademas actualizo la seleccion del combo y el check.
        //DataTable dtCausalReh = objBOConsultas.ListaCamposxNombreOrderByDesc("CausalRehabilitacion");

        pageIndex = gwvRehabilitarPorTicket.PageIndex;

        if ((pageIndex + 1) < gwvRehabilitarPorTicket.PageCount)
        {
            limite = pageSize;
        }
        else
        {
            limite = dtTicketRehabilitados.Rows.Count - (pageIndex * pageSize);
        }

        for (int i = (pageIndex * pageSize), j = 0; j < limite; i++, j++)
        {
            //DropDownList cboMotivo = (DropDownList)gwvRehabilitarPorTicket.Rows[j].FindControl("cboMotivo");
            //cboMotivo.DataSource = dtCausalReh;
            //cboMotivo.DataTextField = "Dsc_Campo";
            //cboMotivo.DataValueField = "Cod_Campo";
            //cboMotivo.DataBind();

            //Aqui no hay condicion pues no he agregado ninguna fila.
            //cboMotivo.ClearSelection();
            //cboMotivo.Items.FindByText(dtTicketRehabilitados.Rows[i]["Motivo"].ToString()).Selected = true;
            CheckBox chkSeleccionar = (CheckBox)gwvRehabilitarPorTicket.Rows[j].FindControl("chkSeleccionar");
            chkSeleccionar.Checked = (Boolean)dtTicketRehabilitados.Rows[i]["Check"];
        }
        #endregion
    }

    #endregion

    #region gwvRehabilitarPorTicket_Sorting
    protected void gwvRehabilitarPorTicket_Sorting(object sender, GridViewSortEventArgs e)
    {
        DataTable dtTicketRehabilitados = (DataTable)Session["tabla"];
        if (dtTicketRehabilitados == null)
        {
            return;
        }

        GridViewSortExpression = e.SortExpression;

        #region Guardo las selecciones del checkbox
        int pageIndex = gwvRehabilitarPorTicket.PageIndex;
        int pageSize = gwvRehabilitarPorTicket.PageSize;
        int limite;
        if ((pageIndex + 1) < gwvRehabilitarPorTicket.PageCount)
        {
            limite = pageSize;
        }
        else
        {
            limite = dtTicketRehabilitados.Rows.Count - (pageIndex * pageSize);
        }

        for (int i = (pageIndex * pageSize), j = 0; j < limite; i++, j++)
        {
            //DropDownList cboMotivo = (DropDownList)gwvRehabilitarPorTicket.Rows[j].FindControl("cboMotivo");
            CheckBox chkSeleccionar = (CheckBox)gwvRehabilitarPorTicket.Rows[j].FindControl("chkSeleccionar");
            //dtTicketRehabilitados.Rows[i]["Motivo"] = cboMotivo.SelectedItem.Text;
            dtTicketRehabilitados.Rows[i]["Check"] = chkSeleccionar.Checked;
        }
        #endregion

        //No es necesario...creo.
        //gwvRehabilitarPorTicket.PageIndex = e.NewPageIndex;

        //Truco para que en la paginacion no este haciendo sort tambien. Esto porque necesito guardar el estado del checkbox..seria muy complicado.
        Session["tabla"] = SortDataTable(dtTicketRehabilitados, false).ToTable();
        //reactualizo la tabla
        dtTicketRehabilitados = (DataTable)Session["tabla"];

        gwvRehabilitarPorTicket.DataSource = (DataTable)Session["tabla"];

        gwvRehabilitarPorTicket.DataBind();

        #region Actualizo la selecciones del checkBox.
        for (int i = (pageIndex * pageSize), j = 0; j < limite; i++, j++)
        {
            //DropDownList cboMotivo = (DropDownList)gwvRehabilitarPorTicket.Rows[j].FindControl("cboMotivo");
            //cboMotivo.DataSource = dtCausalReh;
            //cboMotivo.DataTextField = "Dsc_Campo";
            //cboMotivo.DataValueField = "Cod_Campo";
            //cboMotivo.DataBind();

            //Aqui no hay condicion pues no he agregado ninguna fila.
            //cboMotivo.ClearSelection();
            //cboMotivo.Items.FindByText(dtTicketRehabilitados.Rows[i]["Motivo"].ToString()).Selected = true;
            CheckBox chkSeleccionar = (CheckBox)gwvRehabilitarPorTicket.Rows[j].FindControl("chkSeleccionar");
            chkSeleccionar.Checked = (Boolean)dtTicketRehabilitados.Rows[i]["Check"];
        }
        #endregion

    }
    #endregion

    #region Metodo Generales para el Sort
    //Method that sorts data
    protected DataView SortDataTable(DataTable dataTable, bool isPageIndexChanging)
    {
        if (dataTable != null)
        {
            DataView dataView = new DataView(dataTable);
            if (GridViewSortExpression != string.Empty)
            {
                if (isPageIndexChanging)
                {
                    dataView.Sort = string.Format("{0} {1}", GridViewSortExpression, GridViewSortDirection);
                }
                else
                {
                    dataView.Sort = string.Format("{0} {1}", GridViewSortExpression, GetSortDirection());
                }
            }
            else
            {
                dataView.Sort = string.Format("{0} {1}", "LastName", "ASC");
            }
            return dataView;
        }
        else
        {
            return new DataView();
        }
    }


    private string GridViewSortDirection
    {
        get { return ViewState["SortDirection"] as string ?? "ASC"; }
        set { ViewState["SortDirection"] = value; }
    }

    private string GetSortDirection()
    {
        switch (GridViewSortDirection)
        {
            case "ASC":
                GridViewSortDirection = "DESC";
                break;
            case "DESC":
                GridViewSortDirection = "ASC";
                break;
        }
        return GridViewSortDirection;
    }

    private string GridViewSortExpression
    {
        get { return ViewState["SortExpression"] as string ?? string.Empty; }
        set { ViewState["SortExpression"] = value; }
    }
    #endregion

    #region btnRehabilitar_Click
    protected void btnRehabilitar_Click(object sender, EventArgs e)
    {
        if (Session["tabla"] == null)
        {
            //No hay filas en la grilla
        }
        else
        {
            htLabels = LabelConfig.htLabels;

            int maxRehabilitaciones = 0;
            DataTable dt_parametro = objBOConsultas.ListarParametros("RTM");
            if (dt_parametro.Rows.Count > 0)
            {
                maxRehabilitaciones = Int32.Parse(Convert.ToString(dt_parametro.Rows[0].ItemArray.GetValue(4).ToString()));
            }
            else
            {
                maxRehabilitaciones = 800;
            }

            DataTable dtTicketRehabilitados = (DataTable)Session["tabla"];

            #region Guardo las selecciones del checkbox en la pagina donde se le dio click en Rehabilitar
            int pageSize = gwvRehabilitarPorTicket.PageSize;
            int pageIndex = gwvRehabilitarPorTicket.PageIndex;
            int limite;
            if ((pageIndex + 1) < gwvRehabilitarPorTicket.PageCount)
            {
                limite = pageSize;
            }
            else
            {
                limite = dtTicketRehabilitados.Rows.Count - (pageIndex * pageSize);
            }

            for (int i = (pageIndex * pageSize), j = 0; j < limite; i++, j++)
            {
                //DropDownList cboMotivo = (DropDownList)gwvRehabilitarPorTicket.Rows[j].FindControl("cboMotivo");
                CheckBox chkSeleccionar = (CheckBox)gwvRehabilitarPorTicket.Rows[j].FindControl("chkSeleccionar");
                //dtTicketRehabilitados.Rows[i]["Motivo"] = cboMotivo.SelectedItem.Text;
                dtTicketRehabilitados.Rows[i]["Check"] = chkSeleccionar.Checked;
            }
            #endregion

            int nroSeleccionados = dtTicketRehabilitados.Select("Check=true").Count();
            //int nroSeleccionados2 = Int32.Parse((String)Request.Form["hdNumSelTotal"]);
            if (nroSeleccionados > maxRehabilitaciones)
            {
                System.Threading.Thread.Sleep(500);
                spnRehabilitar.Text = String.Format(htLabels["rehabilitacionticket.lblMensajeError5.Text"].ToString(), maxRehabilitaciones + "");
                return;
            }

            String motivo = cboMotivo.SelectedItem.Value;

            StringBuilder cod_Numero_Tickets = new StringBuilder();

            int iNumTicket = 0;
            for (int i = 0; i < dtTicketRehabilitados.Rows.Count; i++)
            {
                bool checkedRehabilitar = (Boolean)dtTicketRehabilitados.Rows[i]["Check"];
                if (checkedRehabilitar)
                {
                    iNumTicket++;
                    String codigoTicket = dtTicketRehabilitados.Rows[i]["Cod_Numero_Ticket"].ToString();
                    cod_Numero_Tickets.Append(codigoTicket + "|");
                }
            }

            TicketEstHist ticketEstHist = new TicketEstHist();
            ticketEstHist.SCodNumeroTicket = cod_Numero_Tickets.ToString();
            ticketEstHist.SDscNumVuelo = "";
            ticketEstHist.SCausalRehabilitacion = motivo;
            ticketEstHist.SLogUsuarioMod = Convert.ToString(Session["Cod_Usuario"]);

            objBORehabilitacion = new BO_Rehabilitacion((string)Session["Cod_Usuario"], (string)Session["Cod_Modulo"], (string)Session["Cod_SubModulo"]);
            bool ret = objBORehabilitacion.registrarRehabilitacionTicket(ticketEstHist, 1, 4000);
            if (!ret)
            {
                System.Threading.Thread.Sleep(500);
                spnRehabilitar.Text = htLabels["rehabilitacionticket.lblMensajeError4.Text"].ToString();
                return;
            }
            else
            {
                //GeneraAlarma
                string IpClient = Request.UserHostAddress;
                GestionAlarma.Registrar(HttpContext.Current.Server.MapPath(""), "W0000041", "005", IpClient, "1", "Alerta W0000041", "Proceso de Rehabilitacion de Ticket por Vuelo, completado correctamente, Usuario: " + Convert.ToString(Session["Cod_Usuario"]), Convert.ToString(Session["Cod_Usuario"]));

            }

            int iTotalNoRehab = 0;

            //Quiere decir que hubieron tickets que estaban en estado != 'U'
            if (ticketEstHist.SCodNumeroTicket.Length > 0)
            {
                String ticketsNoRehabilitados = ticketEstHist.SCodNumeroTicket.Substring(0, ticketEstHist.SCodNumeroTicket.Length - 1);
                String[] ticketsNoReh = ticketsNoRehabilitados.Split(new char[] { '|' });
                for (int i = 0; i < ticketsNoReh.Length; i++)
                {
                    foreach (DataRow row in dtTicketRehabilitados.Rows)
                    {
                        if (ticketsNoReh[i].ToString().Substring(0, 16).Equals(row["Cod_Numero_Ticket"].ToString()))
                        {
                            string strObservacion = "";

                            switch (ticketsNoReh[i].ToString().Substring(16, 1))
                            {
                                case "U": strObservacion = htLabels["rehabilitacionticket.lblMensajeError2.Text"].ToString(); break;
                                case "R": strObservacion = htLabels["rehabilitacionticket.lblMensajeError1.Text"].ToString(); break;
                                default: strObservacion = htLabels["rehabilitacionticket.lblMensajeError3.Text"].ToString(); break;
                            }

                            row["Check"] = false;
                            row["Observacion"] = strObservacion;

                            iTotalNoRehab++;
                            break;
                        }  
                    }
                }
            }

            //pnlPrincipal.Attributes.Add("style", "display:none");//Visible=false; generaba problemas con el script de checkbox.
            pnlPrincipal.Visible = false;
            //pnlConformidad.Attributes.Add("style", "display:inherit");
            pnlConformidad.Visible = true;
            tableRehabilitar.Visible = false;

            //See summary
            int iTotal = dtTicketRehabilitados.Rows.Count;
            this.lblTotalRehab.Text = (iNumTicket - iTotalNoRehab) + "";
            this.lblTotalNoRehab.Text = (iTotal - (iNumTicket - iTotalNoRehab)) + "";

            System.Threading.Thread.Sleep(500);
        }
    }
    #endregion

    #region btnReporte_click
    protected void btnReporte_click(object sender, ImageClickEventArgs e)
    {
        DataTable dtTicketRehabilitados = (DataTable)Session["tabla"];

        String url = "ReporteREH/ReporteREH_Ticket.aspx?titulo=Rehabilitacion Tickets Por Vuelo";
        string winFeatures = "toolbar=no,status=no,menubar=no,location=center,scrollbars=yes,resizable=yes,height=700,width=800";
        Session.Add("dtTicketRehabilitados", dtTicketRehabilitados);
        ScriptManager.RegisterStartupScript(
            this,
            this.GetType(), "newWindow",
            string.Format("<script type='text/javascript'>window.open('{0}', 'yourWin', '{1}');</script>", url, winFeatures),
            false);
    }
    #endregion



}
