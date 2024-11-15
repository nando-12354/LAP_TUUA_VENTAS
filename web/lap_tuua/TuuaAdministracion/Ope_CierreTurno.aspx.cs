using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using LAP.TUUA.ENTIDADES;
using LAP.TUUA.CONTROL;
using LAP.TUUA.UTIL;
using System.Collections.Generic;
using System.Xml;
using LAP.TUUA.ALARMAS;

public partial class Ope_CierreTurno : System.Web.UI.Page
{
    protected Hashtable htLabels;
    protected BO_Operacion objBOOperacion = new BO_Operacion();
    protected DataTable dt_consulta;
    protected bool Flg_Error;
    protected BO_Error objError;
    protected List<Turno> listaTurnos;

    protected void Page_Load(object sender, EventArgs e)
    {
        htLabels = LabelConfig.htLabels;
        if (!IsPostBack)
        {
            try
            {
                btnCerrar.Text = (string)htLabels["opeCierreTurno.btnCerrar"];
                lblTurno.Text = (string)htLabels["opeCierreTurno.lblTurno"];
                btnCerrar_ConfirmButtonExtender.ConfirmText = (string)htLabels["opeCierreTurno.msgConfirm"];

                SetValorGrilla();
                CargarGrilla();
            }
            catch (Exception ex)
            {
                Flg_Error = true;
                ErrorHandler.Trace(ErrorHandler.Desc_Mensaje, ex);
            }
            finally
            {
                if (Flg_Error)
                {
                    Response.Redirect("PaginaError.aspx");
                }
            }
        }
    }

    public void CargarGrilla()
    {
        try
        {
            string strTurno = Session["Cod_Turno_Cierre"] == null ? "" : (string)Session["Cod_Turno_Cierre"];
            dt_consulta = objBOOperacion.ListarTurnosAbiertos(strTurno);
            Session["dt_turno"] = dt_consulta;
            if (dt_consulta.Rows.Count == 0)
            {
                lblMensajeError.Text = (string)htLabels["opeCierreTurno.lblMensajeErrorData"];
                btnCerrar.Enabled = false;
                grvTurno.DataSource = null;
                grvTurno.DataBind();
            }
            else
            {
                btnCerrar.Enabled = true;
                CrearGrillaDinamica();
                //Traer valor de tamaño de la grilla desde parametro general
                SetValorGrilla();
                //Cargar datos en la grilla                
                this.grvTurno.DataSource = dt_consulta;
                this.grvTurno.AllowPaging = true;
                this.grvTurno.PageSize = Convert.ToInt32(txtValorMaximoGrilla.Text);
                this.grvTurno.DataBind();
                this.lblMensajeError.Text = "";
            }
        }
        catch (Exception ex)
        {
            Flg_Error = true;
            ErrorHandler.Trace(ErrorHandler.Desc_Mensaje, ex);
        }
        finally
        {
            if (Flg_Error)
            {
                Response.Redirect("PaginaError.aspx");
            }
        }
    }

    private void CrearGrillaDinamica()
    {
        string[,] data = { {"Cod_Turno", "Dsc_Usuario", "", "Fch_Inicio", "Fch_Fin", "Dsc_Estacion"}, 
                           {"Código","Usuario","", "Fecha Apertura", "Fecha Cierre", "Nro. Caja"} };

        //grvTurno.Columns.Clear();
        for (int i = 1; i < grvTurno.Columns.Count; i++)
        {
            grvTurno.Columns.RemoveAt(i--);
        }
        
        //TemplateColumn tcsel = new TemplateColumn();
        //tcsel.HeaderText = "Seleccionar";
        //CheckBox ckbCierre = new CheckBox();
        //ckbCierre.ID = "ckbCierre";
        //tcsel.ItemTemplate.InstantiateIn(ckbCierre);

        //TemplateField tfSel = new TemplateField();
        //tfSel.HeaderText = "Seleccionar";
        //tfSel.ItemTemplate = new CheckBoxTemplate();
        //grvTurno.Columns.Add(tfSel);

        //Cod_Turno Column
        BoundField bf_left = new BoundField();
        bf_left.DataField = data[0, 0];
        //bf_left.SortExpression = data[0, 0];
        bf_left.HeaderText = data[1, 0];
        grvTurno.Columns.Add(bf_left);

        //User Column
        bf_left = new BoundField();
        bf_left.DataField = data[0, 1];
        //bf_left.SortExpression = data[0, 1];
        bf_left.HeaderText = data[1, 1];
        grvTurno.Columns.Add(bf_left);

        //Caja Column
        /*TemplateField bfield = new TemplateField();
        //bfield.HeaderTemplate = new GridViewTemplate(ListItemType.Header, data[1, 5]);        
        bfield.SortExpression = "Dsc_Estacion";
        bfield.ItemTemplate = new  GridViewTemplate(ListItemType.Item, "Dsc_Estacion", "Cod_Equipo");
        bfield.HeaderText = "Nro. Caja";
        grvTurno.Columns.Add(bfield);*/
        bf_left = new BoundField();
        bf_left.DataField = data[0, 5];
        //bf_left.SortExpression = data[0, 5];
        bf_left.HeaderText = data[1, 5];
        grvTurno.Columns.Add(bf_left);

        //Fecha Apertura Column
        bf_left = new BoundField();
        bf_left.DataField = data[0, 3];
        //bf_left.SortExpression = data[0, 3];
        bf_left.HeaderText = data[1, 3];
        grvTurno.Columns.Add(bf_left);

        //Fecha Cierre Column
        bf_left = new BoundField();
        bf_left.DataField = data[0, 4];
        //bf_left.SortExpression = data[0, 4];
        bf_left.HeaderText = data[1, 4];
        bf_left.NullDisplayText = "ABIERTO";
        grvTurno.Columns.Add(bf_left);

        foreach (DataColumn col in dt_consulta.Columns)
        {
            BoundField bf1 = new BoundField();

            switch (col.ColumnName)
            {
                case "Cod_Turno":
                case "Dsc_Usuario":
                case "Cod_Equipo":
                case "Num_Ip_Equipo":
                case "Fch_Inicio":
                case "Fch_Fin":
                case "Dsc_Estacion":
                case "Cta_Usuario":
                    continue;
                default:
                    string sHeader = (col.ColumnName.Contains("_ImporteIni")) ? col.ColumnName.Replace("_ImporteIni", "") : col.ColumnName.Replace("_ImporteFin", "");
                    bf1.HeaderText = sHeader;
                    bf1.ItemStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                    break;
            }
            bf1.DataField = col.ColumnName;
            //bf1.SortExpression = col.ColumnName;
            grvTurno.Columns.Add(bf1);
        }
    }

    protected void btnCerrar_Click(object sender, EventArgs e)
    {
        try
        {
            if (!Validar())
            {
                return;
            }
            objBOOperacion = new BO_Operacion((string)Session["Cod_Usuario"], (string)Session["Cod_Modulo"], (string)Session["Cod_SubModulo"]);
            if (!objBOOperacion.CerrarTurno(listaTurnos))
            {
                lblMensajeError.Text = objBOOperacion.Dsc_Message;
            }
            else
            {
                //GeneraAlarma
                string IpClient = Request.UserHostAddress;
                GestionAlarma.Registrar(HttpContext.Current.Server.MapPath(""), "W0000038", "004", IpClient, "1", "Alerta W0000038", "Turnos cerrados exitosamente, Usuario: " + Convert.ToString(Session["Cod_Usuario"]), Convert.ToString(Session["Cod_Usuario"]));

                omb.ShowMessage((string)htLabels["opeCierreTurno.msgTrxOK"], (string)htLabels["opeCierreTurno.lblCierreTurno"], "Ope_CierreTurno.aspx");
            }
        }
        catch (Exception ex)
        {
            Flg_Error = true;
            ErrorHandler.Trace(ErrorHandler.Desc_Mensaje, ex);
        }
        finally
        {
            if (Flg_Error)
            {
                Response.Redirect("PaginaError.aspx");
            }
        }
    }

    protected bool Validar()
    {
        listaTurnos = new List<Turno>();
        dt_consulta = (DataTable)Session["dt_turno"];
        try
        {
            for (int i = 0; i < grvTurno.Rows.Count; i++)
            {
                GridViewRow row = this.grvTurno.Rows[i];
                bool isChecked = ((System.Web.UI.WebControls.CheckBox)row.FindControl("ckbCierre")).Checked;
                if (isChecked)
                {
                    Turno objTurno = new Turno();
                    objTurno.SCodUsuarioCierre = (string)Session["Cod_Usuario"] + "#1";
                    objTurno.SCodTurno = dt_consulta.Rows[i].ItemArray.GetValue(0).ToString();
                    listaTurnos.Add(objTurno);
                }
            }
        }
        catch
        {
            return false;
        }
        if (listaTurnos.Count == 0)
        {
            CargarGrilla();
            lblMensajeError.Text = (string)htLabels["opeCierreTurno.msgSeleccion"];
            return false;
        }
        return true;
    }

    protected void ibtnBuscar_Click(object sender, ImageClickEventArgs e)
    {
        Session["Cod_Turno_Cierre"] = txtTurno.Text;
        //CargarGrilla();
        Page.Response.Redirect(Page.Request.Url.AbsolutePath);
    }

    private void Limpiar()
    {
        txtTurno.Text = "";
        CargarGrilla();
    }

    private void SetValorGrilla()
    {
        this.txtValorMaximoGrilla.Text = (string)Session["GridViewRows"];
    }
    protected void grvTurno_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView oGridView = (GridView)sender;

            GridViewRow oGridViewRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

            int tam = (grvTurno.Columns.Count - 6) / 2;

            //agrego una columna sin nombre
            TableCell oTableCell = new TableCell();
            oTableCell.ColumnSpan = 6;
            oTableCell.BackColor = System.Drawing.Color.White;
            oGridViewRow.Cells.Add(oTableCell);

            //agrego una columna moneda inicio
            oTableCell = new TableCell();
            oTableCell.Text = "Moneda Inicio";
            oTableCell.Style.Add("font-size", "larger");
            oTableCell.Style.Add("font-family", "Verdana");
            oTableCell.Style.Add("font-weight", "bold");
            oTableCell.Style.Add("color", "#000000");
            oTableCell.ColumnSpan = tam;
            oGridViewRow.Cells.Add(oTableCell);

            //agrego una columna moneda fin
            oTableCell = new TableCell();
            oTableCell.Text = "Moneda Fin";
            oTableCell.Style.Add("font-size", "larger");
            oTableCell.Style.Add("font-family", "Verdana");
            oTableCell.Style.Add("font-weight", "bold");
            oTableCell.Style.Add("color", "#000000");
            oTableCell.ColumnSpan = tam;

            oGridViewRow.Cells.Add(oTableCell);
            oGridView.Controls[0].Controls.AddAt(0, oGridViewRow);
        }
    }

    private class CheckBoxTemplate : ITemplate
    {
        public void InstantiateIn(System.Web.UI.Control container)
        {
            // Create a check box
            CheckBox cb = new CheckBox();
            cb.ID = "ckbCierre";
            //Attach method to delegate
            container.Controls.Add(cb);
        }
    }
}
