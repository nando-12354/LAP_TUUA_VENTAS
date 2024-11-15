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
using System.Globalization;

public partial class Cns_CompraVenta : System.Web.UI.Page
{
    protected Hashtable htLabels;
    protected bool Flg_Error;
    DataTable dt_usuario = new DataTable();
    BO_Consultas objConsultas = new BO_Consultas();
    DataTable dt_cantidadmoneda = new DataTable();
    DataTable dt_operacionCompraMoneda = new DataTable();
    DataTable dt_operacionVentaMoneda = new DataTable();
    string sFechaOperacion;
    bool flagError;

    DataTable dt_operacionCompraMoneda11 = new DataTable();
    DataTable dt_operacionVentaMoneda12 = new DataTable(); 
    DataTable dt_operacionCompraMoneda21 = new DataTable();
    DataTable dt_operacionVentaMoneda22 = new DataTable();   
    DataTable dt_operacionCompraMoneda31 = new DataTable();
    DataTable dt_operacionVentaMoneda32 = new DataTable();    
    DataTable dt_operacionCompraMoneda41 = new DataTable();
    DataTable dt_operacionVentaMoneda42 = new DataTable();   
    DataTable dt_operacionCompraMoneda51 = new DataTable();
    DataTable dt_operacionVentaMoneda52 = new DataTable();     
    DataTable dt_operacionCompraMoneda61 = new DataTable();
    DataTable dt_operacionVentaMoneda62 = new DataTable();     
    DataTable dt_operacionCompraMoneda71 = new DataTable();
    DataTable dt_operacionVentaMoneda72 = new DataTable();   

    private Decimal orderTotalaCambiar11 = 0.0M, orderTotalCambiado11 = 0.0M, orderTotalaCambiar12 = 0.0M, orderTotalCambiado12 = 0.0M;
    private Decimal orderTotalaCambiar21 = 0.0M, orderTotalCambiado21 = 0.0M, orderTotalaCambiar22 = 0.0M, orderTotalCambiado22 = 0.0M;
    private Decimal orderTotalaCambiar31 = 0.0M, orderTotalCambiado31 = 0.0M, orderTotalaCambiar32 = 0.0M, orderTotalCambiado32 = 0.0M;
    private Decimal orderTotalaCambiar41 = 0.0M, orderTotalCambiado41 = 0.0M, orderTotalaCambiar42 = 0.0M, orderTotalCambiado42 = 0.0M;
    private Decimal orderTotalaCambiar51 = 0.0M, orderTotalCambiado51 = 0.0M, orderTotalaCambiar52 = 0.0M, orderTotalCambiado52 = 0.0M;
    private Decimal orderTotalaCambiar61 = 0.0M, orderTotalCambiado61 = 0.0M, orderTotalaCambiar62 = 0.0M, orderTotalCambiado62 = 0.0M;
    private Decimal orderTotalaCambiar71 = 0.0M, orderTotalCambiado71 = 0.0M, orderTotalaCambiar72 = 0.0M, orderTotalCambiado72 = 0.0M;


    protected void Page_Load(object sender, EventArgs e)
    {
        CultureInfo culturaPeru = new CultureInfo("es-PE");
        System.Threading.Thread.CurrentThread.CurrentCulture = culturaPeru;

        Flg_Error = false;
        if (!IsPostBack)
        {
            htLabels = LabelConfig.htLabels;
            try
            {
                
                this.lblFecha.Text = htLabels["mconsultaCompraVenta.lblFecha.Text"].ToString();
                this.lblUsuario.Text = htLabels["mconsultaCompraVenta.lblUsuario.Text"].ToString();
                this.btnConsultar.Text = htLabels["consCompania.btnConsultar"].ToString();
            }
            catch (Exception ex)
            {
                Flg_Error = true;
                ErrorHandler.Cod_Error = Define.ERR_008;
                ErrorHandler.Trace(ErrorHandler.htErrorType[Define.ERR_008].ToString(), ex.Message);
            }
            finally
            {
                if (Flg_Error)
                {
                    Response.Redirect("PaginaError.aspx");
                }
            }

            txtFecha.Text = DateTime.Now.ToShortDateString();
            CargarCombo();
        }
    }

    public void CargarCombo()
    {
        Flg_Error = false;
        try
        {
            string FechaOperacion = txtFecha.Text;

            if (FechaOperacion != "")
            {
                string[] wordsDesde = FechaOperacion.Split('/');
                FechaOperacion = wordsDesde[2] + "" + wordsDesde[1] + "" + wordsDesde[0];
            }
            else { FechaOperacion = null; }
            

            //Establecer datatable con datos del estado de compañía 
            BO_Consultas objConsulta = new BO_Consultas();
            dt_usuario = objConsulta.UsuarioxFechaOperacion(FechaOperacion,null,null,null);
            if (FechaOperacion == "") {
                for (int i = 0; i<dt_usuario.Rows.Count; i++)
                {
                    dt_usuario.Rows[i].Delete();
                }                
            }

            if (dt_usuario.Rows.Count < 1)
            {
                EvaluarGrillas();
            }

            //Cargar Combo de usuario
            UIControles objCargaComboGrupo = new UIControles();
            objCargaComboGrupo.LlenarCombo(ddlUsuario, dt_usuario, "Cod_Usuario", "Usuario", true,false);
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

    protected void EvaluarDataTable(DataTable objDataTable)
    {
        if (objDataTable.Rows.Count == 0)
        {   DataRow row = objDataTable.NewRow();
            row["Cod_Turno"] = "-";
            row["Hora"] = "-";
            row["Imp_Tasa_Cambio"] = "0.0";
            row["Imp_ACambiar"] = "0.0";
            row["Imp_Cambiado"] = "0.0";
            row["Usuario"] = "-";
            objDataTable.Rows.Add(row);
        }
    }


    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        htLabels = LabelConfig.htLabels;
        Hashtable htGrillas=new Hashtable();
        sFechaOperacion = txtFecha.Text;
        if (sFechaOperacion != "")
        {
            string[] wordsFechaDesde = sFechaOperacion.Split('/');
            sFechaOperacion = wordsFechaDesde[2] + "" + wordsFechaDesde[1] + "" + wordsFechaDesde[0];
        }
        else { sFechaOperacion = null; }

        try
        {
            dt_cantidadmoneda = objConsultas.UsuarioxFechaOperacion(sFechaOperacion, ddlUsuario.SelectedValue,null,null);

            if (dt_cantidadmoneda.Columns.Contains("ErrRows"))
            {
                lblMensajeErrorData.Text = dt_cantidadmoneda.Rows[0].ItemArray.GetValue(0).ToString();
                return;
            }

            if (dt_cantidadmoneda.Rows.Count > 0)
            {
                DataTable dtOperaciones_Order = new DataTable();

                dtOperaciones_Order.Columns.Add("Cod_Moneda");
                dtOperaciones_Order.Columns.Add("Dsc_Moneda");
                dtOperaciones_Order.Columns.Add("Num_Order");

                foreach (DataRow row in dt_cantidadmoneda.Rows)
                {
                    string s = row["Cod_Moneda"].ToString();
                    string dsm = row["Dsc_Moneda"].ToString();
                    DataRow[] drx = dtOperaciones_Order.Select("Cod_Moneda= '" + s + "'");
                    DataRow[] drxDsc = dtOperaciones_Order.Select("Dsc_Moneda= '" + dsm + "'");
                    if (drx.Length == 0)
                    {
                        int iOrder = 3;
                        if (s == "DOL")
                        {
                            iOrder = 1;
                        }
                        else if (s == "EUR")
                        {
                            iOrder = 2;
                        }
                        dtOperaciones_Order.Rows.Add(new object[] { s, dsm, iOrder });
                    }
                }

                //kinzi - order moneda
                DataTable dtOperaciones = new DataTable();
                dtOperaciones.Columns.Add("Cod_Moneda");
                dtOperaciones.Columns.Add("Dsc_Moneda");
                DataRow[] foundRowTipoMoneda = dtOperaciones_Order.Select("", "Num_Order ASC");
                foreach (DataRow r in foundRowTipoMoneda)
                {
                    dtOperaciones.Rows.Add(new object[] { r["Cod_Moneda"].ToString(), r["Dsc_Moneda"].ToString() });
                }

                if (dtOperaciones.Rows.Count > 0)
                {
                    lblMensajeErrorData.Text = "";

                    for (int i = 0; i < dtOperaciones.Rows.Count; i++)
                    {
                        //htGrillas.Add(dt_cantidadmoneda.Rows[i].ItemArray.GetValue(1).ToString(),new GridView());
                        GridView grviewMoneda = new GridView();
                        if (i == 0)
                        {

                            lblMoneda1.Text = dtOperaciones.Rows[i].ItemArray.GetValue(1).ToString();
                            lblTipoOperacion11.Text = htLabels["mconsultaCompraVenta.lblTipoOperacionCompra"].ToString();
                            Session["IdMoneda11"] = dtOperaciones.Rows[i].ItemArray.GetValue(0).ToString();
                            dt_operacionCompraMoneda11 = objConsultas.UsuarioxFechaOperacion(sFechaOperacion, ddlUsuario.SelectedValue, "CM", dtOperaciones.Rows[i].ItemArray.GetValue(0).ToString());
                            EvaluarDataTable(dt_operacionCompraMoneda11);
                            grvMoneda11.DataSource = dt_operacionCompraMoneda11;
                            grvMoneda11.DataBind();


                            //for (int j = 0; j < 3; j++)
                            //{
                            //    GridView grview+j = new GridView();
                            //    grview.AutoGenerateColumns = false;
                            //    BoundField bFld = null;
                            //}



                            //bFld = new BoundField();
                            //bFld.DataField = "Cod_Turno";
                            //bFld.HeaderText = "Codigo";
                            //grview.Columns.Add(bFld);

                            //bFld = new BoundField();
                            //bFld.DataField = "Cod_Usuario";
                            //bFld.HeaderText = "Usuario";
                            //grview.Columns.Add(bFld);


                            //bFld = new BoundField();
                            //bFld.DataField = "Cod_Operacion";
                            //bFld.HeaderText = "Operacion";
                            //grview.Columns.Add(bFld);


                            //DataTable objTable = new DataTable();
                            //DataColumn colum;
                            //colum = new DataColumn();
                            //colum.ColumnName = "Cod_Turno";
                            //objTable.Columns.Add(colum);
                            //colum = new DataColumn();
                            //colum.ColumnName = "Cod_Usuario";
                            //objTable.Columns.Add(colum);
                            //colum = new DataColumn();
                            //colum.ColumnName = "Cod_Operacion";
                            //objTable.Columns.Add(colum);


                            //DataRow rows = objTable.NewRow();
                            //rows[0] = "0001";
                            //rows[1] = "U00K6";
                            //rows[2] = "OP999";
                            //objTable.Rows.Add(rows);

                            //grview.DataSource = objTable;
                            //grview.DataBind();


                            //pnlOperacion.Controls.Add(grview);


                            dt_operacionVentaMoneda12 = objConsultas.UsuarioxFechaOperacion(sFechaOperacion, ddlUsuario.SelectedValue, "VM", dtOperaciones.Rows[i].ItemArray.GetValue(0).ToString());
                            lblTipoOperacion12.Text = htLabels["mconsultaCompraVenta.lblTipoOperacionVenta"].ToString();
                            EvaluarDataTable(dt_operacionVentaMoneda12);
                            grvMoneda12.DataSource = dt_operacionVentaMoneda12;
                            grvMoneda12.DataBind();
                        }

                        if (i == 1)
                        {

                            lblMoneda2.Text = dtOperaciones.Rows[i].ItemArray.GetValue(1).ToString();
                            lblTipoOperacion21.Text = htLabels["mconsultaCompraVenta.lblTipoOperacionCompra"].ToString();
                            dt_operacionCompraMoneda21 = objConsultas.UsuarioxFechaOperacion(sFechaOperacion, ddlUsuario.SelectedValue, "CM", dtOperaciones.Rows[i].ItemArray.GetValue(0).ToString());
                            EvaluarDataTable(dt_operacionCompraMoneda21);
                            grvMoneda21.DataSource = dt_operacionCompraMoneda21;
                            grvMoneda21.DataBind();

                            dt_operacionVentaMoneda22 = objConsultas.UsuarioxFechaOperacion(sFechaOperacion, ddlUsuario.SelectedValue, "VM", dtOperaciones.Rows[i].ItemArray.GetValue(0).ToString());
                            lblTipoOperacion22.Text = htLabels["mconsultaCompraVenta.lblTipoOperacionVenta"].ToString();
                            EvaluarDataTable(dt_operacionVentaMoneda22);
                            grvMoneda22.DataSource = dt_operacionVentaMoneda22;
                            grvMoneda22.DataBind();
                        }


                        if (i == 2)
                        {
                            lblMoneda3.Text = dtOperaciones.Rows[i].ItemArray.GetValue(1).ToString();
                            lblTipoOperacion31.Text = htLabels["mconsultaCompraVenta.lblTipoOperacionCompra"].ToString();
                            dt_operacionCompraMoneda31 = objConsultas.UsuarioxFechaOperacion(sFechaOperacion, ddlUsuario.SelectedValue, "CM", dtOperaciones.Rows[i].ItemArray.GetValue(0).ToString());
                            EvaluarDataTable(dt_operacionCompraMoneda31);
                            grvMoneda31.DataSource = dt_operacionCompraMoneda31;
                            grvMoneda31.DataBind();

                            dt_operacionVentaMoneda32 = objConsultas.UsuarioxFechaOperacion(sFechaOperacion, ddlUsuario.SelectedValue, "VM", dtOperaciones.Rows[i].ItemArray.GetValue(0).ToString());
                            lblTipoOperacion32.Text = htLabels["mconsultaCompraVenta.lblTipoOperacionVenta"].ToString();
                            EvaluarDataTable(dt_operacionVentaMoneda32);
                            grvMoneda32.DataSource = dt_operacionVentaMoneda32;
                            grvMoneda32.DataBind();



                        }


                        if (i == 3)
                        {
                            lblMoneda4.Text = dtOperaciones.Rows[i].ItemArray.GetValue(1).ToString();
                            lblTipoOperacion41.Text = htLabels["mconsultaCompraVenta.lblTipoOperacionCompra"].ToString();
                            dt_operacionCompraMoneda41 = objConsultas.UsuarioxFechaOperacion(sFechaOperacion, ddlUsuario.SelectedValue, "CM", dtOperaciones.Rows[i].ItemArray.GetValue(0).ToString());
                            EvaluarDataTable(dt_operacionCompraMoneda41);
                            grvMoneda41.DataSource = dt_operacionCompraMoneda41;
                            grvMoneda41.DataBind();

                            dt_operacionVentaMoneda42 = objConsultas.UsuarioxFechaOperacion(sFechaOperacion, ddlUsuario.SelectedValue, "VM", dtOperaciones.Rows[i].ItemArray.GetValue(0).ToString());
                            lblTipoOperacion42.Text = htLabels["mconsultaCompraVenta.lblTipoOperacionVenta"].ToString();
                            EvaluarDataTable(dt_operacionVentaMoneda42);
                            grvMoneda42.DataSource = dt_operacionVentaMoneda42;
                            grvMoneda42.DataBind();



                        }


                        if (i == 4)
                        {
                            lblMoneda5.Text = dtOperaciones.Rows[i].ItemArray.GetValue(1).ToString();
                            lblTipoOperacion51.Text = htLabels["mconsultaCompraVenta.lblTipoOperacionCompra"].ToString();
                            dt_operacionCompraMoneda51 = objConsultas.UsuarioxFechaOperacion(sFechaOperacion, ddlUsuario.SelectedValue, "CM", dtOperaciones.Rows[i].ItemArray.GetValue(0).ToString());
                            EvaluarDataTable(dt_operacionCompraMoneda51);
                            grvMoneda51.DataSource = dt_operacionCompraMoneda51;
                            grvMoneda51.DataBind();

                            dt_operacionVentaMoneda52 = objConsultas.UsuarioxFechaOperacion(sFechaOperacion, ddlUsuario.SelectedValue, "VM", dtOperaciones.Rows[i].ItemArray.GetValue(0).ToString());
                            lblTipoOperacion52.Text = htLabels["mconsultaCompraVenta.lblTipoOperacionVenta"].ToString();
                            EvaluarDataTable(dt_operacionVentaMoneda52);
                            grvMoneda52.DataSource = dt_operacionVentaMoneda52;
                            grvMoneda52.DataBind();



                        }


                        if (i == 5)
                        {
                            lblMoneda6.Text = dtOperaciones.Rows[i].ItemArray.GetValue(1).ToString();
                            lblTipoOperacion61.Text = htLabels["mconsultaCompraVenta.lblTipoOperacionCompra"].ToString();
                            dt_operacionCompraMoneda61 = objConsultas.UsuarioxFechaOperacion(sFechaOperacion, ddlUsuario.SelectedValue, "CM", dtOperaciones.Rows[i].ItemArray.GetValue(0).ToString());
                            EvaluarDataTable(dt_operacionCompraMoneda61);
                            grvMoneda61.DataSource = dt_operacionCompraMoneda61;
                            grvMoneda61.DataBind();

                            dt_operacionVentaMoneda62 = objConsultas.UsuarioxFechaOperacion(sFechaOperacion, ddlUsuario.SelectedValue, "VM", dtOperaciones.Rows[i].ItemArray.GetValue(0).ToString());
                            lblTipoOperacion62.Text = htLabels["mconsultaCompraVenta.lblTipoOperacionVenta"].ToString();
                            EvaluarDataTable(dt_operacionVentaMoneda62);
                            grvMoneda62.DataSource = dt_operacionVentaMoneda62;
                            grvMoneda62.DataBind();
                        }


                        if (i == 6)
                        {
                            lblMoneda7.Text = dtOperaciones.Rows[i].ItemArray.GetValue(1).ToString();
                            lblTipoOperacion71.Text = htLabels["mconsultaCompraVenta.lblTipoOperacionCompra"].ToString();
                            dt_operacionCompraMoneda71 = objConsultas.UsuarioxFechaOperacion(sFechaOperacion, ddlUsuario.SelectedValue, "CM", dtOperaciones.Rows[i].ItemArray.GetValue(0).ToString());
                            EvaluarDataTable(dt_operacionCompraMoneda71);
                            grvMoneda71.DataSource = dt_operacionCompraMoneda71;
                            grvMoneda71.DataBind();

                            dt_operacionVentaMoneda72 = objConsultas.UsuarioxFechaOperacion(sFechaOperacion, ddlUsuario.SelectedValue, "VM", dtOperaciones.Rows[i].ItemArray.GetValue(0).ToString());
                            lblTipoOperacion72.Text = htLabels["mconsultaCompraVenta.lblTipoOperacionVenta"].ToString();
                            EvaluarDataTable(dt_operacionVentaMoneda72);
                            grvMoneda72.DataSource = dt_operacionVentaMoneda72;
                            grvMoneda72.DataBind();
                        }
                    }
                }
                else
                {
                    lblMensajeErrorData.Text = htLabels["mconsultaNoData.lblMensajeError.Text"].ToString();
                }

                EvaluarGrillas();
            }
            else
            {
                if (ddlUsuario.SelectedValue != "0")
                    lblMensajeErrorData.Text = "No existe operaciones de compra y venta para el usuario seleccionado";
                else
                    lblMensajeErrorData.Text = htLabels["mconsultaNoData.lblMensajeError.Text"].ToString();

                EvaluarGrillas();
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


    public void EvaluarGrillas()
    {
        if (dt_operacionCompraMoneda11.Rows.Count == 0 && dt_operacionVentaMoneda12.Rows.Count == 0)
        {
            lblMoneda1.Text = "";
            lblTipoOperacion11.Text = "";
            lblTipoOperacion12.Text = "";
            grvMoneda11.DataSource = null;
            grvMoneda11.DataBind();

            grvMoneda12.DataSource = null;
            grvMoneda12.DataBind();
        }
        if (dt_operacionCompraMoneda21.Rows.Count == 0 && dt_operacionVentaMoneda22.Rows.Count == 0)
        {
            lblMoneda2.Text = "";            
            lblTipoOperacion21.Text = "";
            lblTipoOperacion22.Text = "";
            grvMoneda21.DataSource = null;
            grvMoneda21.DataBind();

            grvMoneda22.DataSource = null;
            grvMoneda22.DataBind();
        }

        if (dt_operacionCompraMoneda31.Rows.Count == 0 && dt_operacionVentaMoneda32.Rows.Count == 0)
        {
            lblMoneda3.Text = "";            
            lblTipoOperacion31.Text = "";
            lblTipoOperacion32.Text = "";
            grvMoneda31.DataSource = null;
            grvMoneda31.DataBind();

            grvMoneda32.DataSource = null;
            grvMoneda32.DataBind();
        }
        if (dt_operacionCompraMoneda41.Rows.Count == 0 && dt_operacionVentaMoneda42.Rows.Count == 0)
        {
            lblMoneda4.Text = "";            
            lblTipoOperacion41.Text = "";
            lblTipoOperacion42.Text = "";
            grvMoneda41.DataSource = null;
            grvMoneda41.DataBind();

            grvMoneda42.DataSource = null;
            grvMoneda42.DataBind();
        }

        if (dt_operacionCompraMoneda51.Rows.Count == 0 && dt_operacionVentaMoneda52.Rows.Count == 0)
        {
            lblMoneda5.Text = "";            
            lblTipoOperacion51.Text = "";
            lblTipoOperacion52.Text = "";
            grvMoneda51.DataSource = null;
            grvMoneda51.DataBind();

            grvMoneda52.DataSource = null;
            grvMoneda52.DataBind();
        }

        if (dt_operacionCompraMoneda61.Rows.Count == 0 && dt_operacionVentaMoneda62.Rows.Count == 0)
        {
            lblMoneda6.Text = "";            
            lblTipoOperacion61.Text = "";
            lblTipoOperacion62.Text = "";
            grvMoneda61.DataSource = null;
            grvMoneda61.DataBind();

            grvMoneda62.DataSource = null;
            grvMoneda62.DataBind();
        }

        if (dt_operacionCompraMoneda71.Rows.Count == 0 && dt_operacionVentaMoneda72.Rows.Count == 0)
        {
            lblMoneda7.Text = "";            
            lblTipoOperacion71.Text = "";
            lblTipoOperacion72.Text = "";
            grvMoneda71.DataSource = null;
            grvMoneda71.DataBind();

            grvMoneda72.DataSource = null;
            grvMoneda72.DataBind();
        }

    }

    protected void grvMoneda11_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // Get the cell that contains the item total.
            TableCell cellACambiar = e.Row.Cells[3];
            TableCell cellCambiado = e.Row.Cells[4];
            // Get the DataBoundLiteralControl control that contains the data-bound value.
            DataBoundLiteralControl boundControlACambiar = (DataBoundLiteralControl)cellACambiar.Controls[0];
            DataBoundLiteralControl boundControlCambiado = (DataBoundLiteralControl)cellCambiado.Controls[0];

            if (((System.Data.DataRowView)(e.Row.DataItem)).Row["Tip_Estado_Actual"].ToString() == "X")
            {
                e.Row.BackColor = System.Drawing.Color.FromArgb(253, 234, 234);
            }
            else
            {            
                // Add the total for an item (row) to the order total.
                orderTotalaCambiar11 += Convert.ToDecimal(boundControlACambiar.Text);
                orderTotalCambiado11 += Convert.ToDecimal(boundControlCambiado.Text);
            }
        }
    }

    protected void grvMoneda11_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            // Get the OrderTotalLabel Label control in the footer row.
            Label totalACambiar = (Label)e.Row.FindControl("Imp_ACambiarLabel");
            Label totalCambiado = (Label)e.Row.FindControl("Imp_CambiadoLabel");
            Label lblTotal = (Label)e.Row.FindControl("lblTotal");

            // Display the grand total of the order formatted as currency.
            if (totalACambiar != null || totalCambiado != null)
            {
                totalACambiar.Text = orderTotalaCambiar11.ToString();
                totalCambiado.Text = orderTotalCambiado11.ToString();
                lblTotal.Text = "Total";
            }
        }
    }

    protected void grvMoneda12_RowDataBound(object sender, GridViewRowEventArgs e)
    {       
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // Get the cell that contains the item total.
            TableCell cellACambiar = e.Row.Cells[3];
            TableCell cellCambiado = e.Row.Cells[4];
            // Get the DataBoundLiteralControl control that contains the data-bound value.
            DataBoundLiteralControl boundControlACambiar = (DataBoundLiteralControl)cellACambiar.Controls[0];
            DataBoundLiteralControl boundControlCambiado = (DataBoundLiteralControl)cellCambiado.Controls[0];            
           
            if (((System.Data.DataRowView)(e.Row.DataItem)).Row["Tip_Estado_Actual"].ToString() == "X")
            {
                e.Row.BackColor = System.Drawing.Color.FromArgb(253, 234, 234);
            }
            else
            {            
                // Add the total for an item (row) to the order total.
                orderTotalaCambiar12 += Convert.ToDecimal(boundControlACambiar.Text);
                orderTotalCambiado12 += Convert.ToDecimal(boundControlCambiado.Text);
            }

        }
    }
    

    protected void grvMoneda12_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            // Get the OrderTotalLabel Label control in the footer row.
            Label totalACambiar = (Label)e.Row.FindControl("Imp_ACambiarLabel");
            Label totalCambiado = (Label)e.Row.FindControl("Imp_CambiadoLabel");
            Label lblTotal = (Label)e.Row.FindControl("lblTotal");
            
            // Display the grand total of the order formatted as currency.
            if (totalACambiar != null || totalCambiado!=null)
            {
                totalACambiar.Text = orderTotalaCambiar12.ToString();
                totalCambiado.Text = orderTotalCambiado12.ToString();
                lblTotal.Text = "Total";
            }
        }
    }
    protected void grvMoneda21_RowDataBound(object sender, GridViewRowEventArgs e)
    {       
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // Get the cell that contains the item total.
            TableCell cellACambiar = e.Row.Cells[3];
            TableCell cellCambiado = e.Row.Cells[4];
            // Get the DataBoundLiteralControl control that contains the data-bound value.
            DataBoundLiteralControl boundControlACambiar = (DataBoundLiteralControl)cellACambiar.Controls[0];
            DataBoundLiteralControl boundControlCambiado = (DataBoundLiteralControl)cellCambiado.Controls[0];

            if (((System.Data.DataRowView)(e.Row.DataItem)).Row["Tip_Estado_Actual"].ToString() == "X")
            {
                e.Row.BackColor = System.Drawing.Color.FromArgb(253, 234, 234);
            }
            else
            {            
                // Add the total for an item (row) to the order total.
                orderTotalaCambiar21 += Convert.ToDecimal(boundControlACambiar.Text);
                orderTotalCambiado21 += Convert.ToDecimal(boundControlCambiado.Text);
            }
        }
    }
    protected void grvMoneda21_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            // Get the OrderTotalLabel Label control in the footer row.
            Label totalACambiar = (Label)e.Row.FindControl("Imp_ACambiarLabel");
            Label totalCambiado = (Label)e.Row.FindControl("Imp_CambiadoLabel");
            Label lblTotal = (Label)e.Row.FindControl("lblTotal");

            // Display the grand total of the order formatted as currency.
            if (totalACambiar != null || totalCambiado != null)
            {
                totalACambiar.Text = orderTotalaCambiar21.ToString();
                totalCambiado.Text = orderTotalCambiado21.ToString();
                lblTotal.Text = "Total";
            }
        }
    }

   
    protected void grvMoneda22_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // Get the cell that contains the item total.
            TableCell cellACambiar = e.Row.Cells[3];
            TableCell cellCambiado = e.Row.Cells[4];
            // Get the DataBoundLiteralControl control that contains the data-bound value.
            DataBoundLiteralControl boundControlACambiar = (DataBoundLiteralControl)cellACambiar.Controls[0];
            DataBoundLiteralControl boundControlCambiado = (DataBoundLiteralControl)cellCambiado.Controls[0];

            if (((System.Data.DataRowView)(e.Row.DataItem)).Row["Tip_Estado_Actual"].ToString() == "X")
            {
                e.Row.BackColor = System.Drawing.Color.FromArgb(253, 234, 234);
            }
            else
            {            
                // Add the total for an item (row) to the order total.
                orderTotalaCambiar22 += Convert.ToDecimal(boundControlACambiar.Text);
                orderTotalCambiado22 += Convert.ToDecimal(boundControlCambiado.Text);
            }
        }
    }


    protected void grvMoneda22_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            // Get the OrderTotalLabel Label control in the footer row.
            Label totalACambiar = (Label)e.Row.FindControl("Imp_ACambiarLabel");
            Label totalCambiado = (Label)e.Row.FindControl("Imp_CambiadoLabel");
            Label lblTotal = (Label)e.Row.FindControl("lblTotal");

            // Display the grand total of the order formatted as currency.
            if (totalACambiar != null || totalCambiado != null)
            {
                totalACambiar.Text = orderTotalaCambiar22.ToString();
                totalCambiado.Text = orderTotalCambiado22.ToString();
                lblTotal.Text = "Total";
            }
        }
    }

    protected void txtFecha_TextChanged(object sender, EventArgs e)
    {
        CargarCombo();
        EvaluarGrillas();

    }
    protected void grvMoneda11_Sorting(object sender, GridViewSortEventArgs e)
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



    private void SortGridView(string sortExpression, String direction)
    {
        try
        {
            sFechaOperacion = txtFecha.Text;
            if (sFechaOperacion != "")
            {
                string[] wordsFechaDesde = sFechaOperacion.Split('/');
                sFechaOperacion = wordsFechaDesde[2] + "" + wordsFechaDesde[1] + "" + wordsFechaDesde[0];
            }
            else { sFechaOperacion = null; }

            dt_operacionCompraMoneda = objConsultas.UsuarioxFechaOperacion(sFechaOperacion, ddlUsuario.SelectedValue, "CM", (string)Session["IdMoneda11"]);
            EvaluarDataTable(dt_operacionCompraMoneda);

            //cargarDataTable();
            //Session["dt_consultaCompania"] = objControles.ConvertDataTable(dwConsulta(dt_consulta, sortExpression, direction));
            this.grvMoneda11.DataSource = dwConsulta(dt_operacionCompraMoneda, sortExpression, direction);
            //this.grvMoneda11.PageSize = Convert.ToInt32(txtValorMaximoGrilla.Text);
            grvMoneda11.DataBind();
           // Session.Contents.Remove("IdMoneda11");
             
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



    protected void grvMoneda32_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // Get the cell that contains the item total.
            TableCell cellACambiar = e.Row.Cells[3];
            TableCell cellCambiado = e.Row.Cells[4];
            // Get the DataBoundLiteralControl control that contains the data-bound value.
            DataBoundLiteralControl boundControlACambiar = (DataBoundLiteralControl)cellACambiar.Controls[0];
            DataBoundLiteralControl boundControlCambiado = (DataBoundLiteralControl)cellCambiado.Controls[0];

            if (((System.Data.DataRowView)(e.Row.DataItem)).Row["Tip_Estado_Actual"].ToString() == "X")
            {
                e.Row.BackColor = System.Drawing.Color.FromArgb(253, 234, 234);
            }
            else
            {            
                // Add the total for an item (row) to the order total.
                orderTotalaCambiar32 += Convert.ToDecimal(boundControlACambiar.Text);
                orderTotalCambiado32 += Convert.ToDecimal(boundControlCambiado.Text);
            }
        }
    }


    protected void grvMoneda32_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            // Get the OrderTotalLabel Label control in the footer row.
            Label totalACambiar = (Label)e.Row.FindControl("Imp_ACambiarLabel");
            Label totalCambiado = (Label)e.Row.FindControl("Imp_CambiadoLabel");
            Label lblTotal = (Label)e.Row.FindControl("lblTotal");

            // Display the grand total of the order formatted as currency.
            if (totalACambiar != null || totalCambiado != null)
            {
                totalACambiar.Text = orderTotalaCambiar32.ToString();
                totalCambiado.Text = orderTotalCambiado32.ToString();
                lblTotal.Text = "Total";
            }
        }
    }




    protected void grvMoneda31_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // Get the cell that contains the item total.
            TableCell cellACambiar = e.Row.Cells[3];
            TableCell cellCambiado = e.Row.Cells[4];
            // Get the DataBoundLiteralControl control that contains the data-bound value.
            DataBoundLiteralControl boundControlACambiar = (DataBoundLiteralControl)cellACambiar.Controls[0];
            DataBoundLiteralControl boundControlCambiado = (DataBoundLiteralControl)cellCambiado.Controls[0];

            if (((System.Data.DataRowView)(e.Row.DataItem)).Row["Tip_Estado_Actual"].ToString() == "X")
            {
                e.Row.BackColor = System.Drawing.Color.FromArgb(253, 234, 234);
            }
            else
            {        
                // Add the total for an item (row) to the order total.
                orderTotalaCambiar31 += Convert.ToDecimal(boundControlACambiar.Text);
                orderTotalCambiado31 += Convert.ToDecimal(boundControlCambiado.Text);
            }
        }
    }



    protected void grvMoneda31_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            // Get the OrderTotalLabel Label control in the footer row.
            Label totalACambiar = (Label)e.Row.FindControl("Imp_ACambiarLabel");
            Label totalCambiado = (Label)e.Row.FindControl("Imp_CambiadoLabel");
            Label lblTotal = (Label)e.Row.FindControl("lblTotal");

            // Display the grand total of the order formatted as currency.
            if (totalACambiar != null || totalCambiado != null)
            {
                totalACambiar.Text = orderTotalaCambiar31.ToString();
                totalCambiado.Text = orderTotalCambiado31.ToString();
                lblTotal.Text = "Total";
            }
        }
    }








    protected void grvMoneda41_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // Get the cell that contains the item total.
            TableCell cellACambiar = e.Row.Cells[3];
            TableCell cellCambiado = e.Row.Cells[4];
            // Get the DataBoundLiteralControl control that contains the data-bound value.
            DataBoundLiteralControl boundControlACambiar = (DataBoundLiteralControl)cellACambiar.Controls[0];
            DataBoundLiteralControl boundControlCambiado = (DataBoundLiteralControl)cellCambiado.Controls[0];

            if (((System.Data.DataRowView)(e.Row.DataItem)).Row["Tip_Estado_Actual"].ToString() == "X")
            {
                e.Row.BackColor = System.Drawing.Color.FromArgb(253, 234, 234);
            }
            else
            {
                // Add the total for an item (row) to the order total.
                orderTotalaCambiar41 += Convert.ToDecimal(boundControlACambiar.Text);
                orderTotalCambiado41 += Convert.ToDecimal(boundControlCambiado.Text);
            }
        }
    }


    protected void grvMoneda41_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            // Get the OrderTotalLabel Label control in the footer row.
            Label totalACambiar = (Label)e.Row.FindControl("Imp_ACambiarLabel");
            Label totalCambiado = (Label)e.Row.FindControl("Imp_CambiadoLabel");
            Label lblTotal = (Label)e.Row.FindControl("lblTotal");

            // Display the grand total of the order formatted as currency.
            if (totalACambiar != null || totalCambiado != null)
            {
                totalACambiar.Text = orderTotalaCambiar41.ToString();
                totalCambiado.Text = orderTotalCambiado41.ToString();
                lblTotal.Text = "Total";
            }
        }
    }






    protected void grvMoneda42_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // Get the cell that contains the item total.
            TableCell cellACambiar = e.Row.Cells[3];
            TableCell cellCambiado = e.Row.Cells[4];
            // Get the DataBoundLiteralControl control that contains the data-bound value.
            DataBoundLiteralControl boundControlACambiar = (DataBoundLiteralControl)cellACambiar.Controls[0];
            DataBoundLiteralControl boundControlCambiado = (DataBoundLiteralControl)cellCambiado.Controls[0];

            if (((System.Data.DataRowView)(e.Row.DataItem)).Row["Tip_Estado_Actual"].ToString() == "X")
            {
                e.Row.BackColor = System.Drawing.Color.FromArgb(253, 234, 234);
            }
            else
            { 
                // Add the total for an item (row) to the order total.
                orderTotalaCambiar42 += Convert.ToDecimal(boundControlACambiar.Text);
                orderTotalCambiado42 += Convert.ToDecimal(boundControlCambiado.Text);
            }
        }
    }

    protected void grvMoneda42_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            // Get the OrderTotalLabel Label control in the footer row.
            Label totalACambiar = (Label)e.Row.FindControl("Imp_ACambiarLabel");
            Label totalCambiado = (Label)e.Row.FindControl("Imp_CambiadoLabel");
            Label lblTotal = (Label)e.Row.FindControl("lblTotal");

            // Display the grand total of the order formatted as currency.
            if (totalACambiar != null || totalCambiado != null)
            {
                totalACambiar.Text = orderTotalaCambiar42.ToString();
                totalCambiado.Text = orderTotalCambiado42.ToString();
                lblTotal.Text = "Total";
            }
        }
    }



    protected void grvMoneda51_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // Get the cell that contains the item total.
            TableCell cellACambiar = e.Row.Cells[3];
            TableCell cellCambiado = e.Row.Cells[4];
            // Get the DataBoundLiteralControl control that contains the data-bound value.
            DataBoundLiteralControl boundControlACambiar = (DataBoundLiteralControl)cellACambiar.Controls[0];
            DataBoundLiteralControl boundControlCambiado = (DataBoundLiteralControl)cellCambiado.Controls[0];

            if (((System.Data.DataRowView)(e.Row.DataItem)).Row["Tip_Estado_Actual"].ToString() == "X")
            {
                e.Row.BackColor = System.Drawing.Color.FromArgb(253, 234, 234);
            }
            else
            {          
                // Add the total for an item (row) to the order total.
                orderTotalaCambiar51 += Convert.ToDecimal(boundControlACambiar.Text);
                orderTotalCambiado51 += Convert.ToDecimal(boundControlCambiado.Text);
            }
        }
    }

    protected void grvMoneda51_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            // Get the OrderTotalLabel Label control in the footer row.
            Label totalACambiar = (Label)e.Row.FindControl("Imp_ACambiarLabel");
            Label totalCambiado = (Label)e.Row.FindControl("Imp_CambiadoLabel");
            Label lblTotal = (Label)e.Row.FindControl("lblTotal");

            // Display the grand total of the order formatted as currency.
            if (totalACambiar != null || totalCambiado != null)
            {
                totalACambiar.Text = orderTotalaCambiar51.ToString();
                totalCambiado.Text = orderTotalCambiado51.ToString();
                lblTotal.Text = "Total";
            }
        }
    }


    protected void grvMoneda52_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // Get the cell that contains the item total.
            TableCell cellACambiar = e.Row.Cells[3];
            TableCell cellCambiado = e.Row.Cells[4];
            // Get the DataBoundLiteralControl control that contains the data-bound value.
            DataBoundLiteralControl boundControlACambiar = (DataBoundLiteralControl)cellACambiar.Controls[0];
            DataBoundLiteralControl boundControlCambiado = (DataBoundLiteralControl)cellCambiado.Controls[0];

            if (((System.Data.DataRowView)(e.Row.DataItem)).Row["Tip_Estado_Actual"].ToString() == "X")
            {
                e.Row.BackColor = System.Drawing.Color.FromArgb(253, 234, 234);
            }
            else
            {
                // Add the total for an item (row) to the order total.
                orderTotalaCambiar52 += Convert.ToDecimal(boundControlACambiar.Text);
                orderTotalCambiado52 += Convert.ToDecimal(boundControlCambiado.Text);
            }
        }
    }


    protected void grvMoneda52_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            // Get the OrderTotalLabel Label control in the footer row.
            Label totalACambiar = (Label)e.Row.FindControl("Imp_ACambiarLabel");
            Label totalCambiado = (Label)e.Row.FindControl("Imp_CambiadoLabel");
            Label lblTotal = (Label)e.Row.FindControl("lblTotal");

            // Display the grand total of the order formatted as currency.
            if (totalACambiar != null || totalCambiado != null)
            {
                totalACambiar.Text = orderTotalaCambiar52.ToString();
                totalCambiado.Text = orderTotalCambiado52.ToString();
                lblTotal.Text = "Total";
            }
        }
    }





    protected void grvMoneda61_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // Get the cell that contains the item total.
            TableCell cellACambiar = e.Row.Cells[3];
            TableCell cellCambiado = e.Row.Cells[4];
            // Get the DataBoundLiteralControl control that contains the data-bound value.
            DataBoundLiteralControl boundControlACambiar = (DataBoundLiteralControl)cellACambiar.Controls[0];
            DataBoundLiteralControl boundControlCambiado = (DataBoundLiteralControl)cellCambiado.Controls[0];

            if (((System.Data.DataRowView)(e.Row.DataItem)).Row["Tip_Estado_Actual"].ToString() == "X")
            {
                e.Row.BackColor = System.Drawing.Color.FromArgb(253, 234, 234);
            }
            else
            {            
                // Add the total for an item (row) to the order total.
                orderTotalaCambiar61 += Convert.ToDecimal(boundControlACambiar.Text);
                orderTotalCambiado61 += Convert.ToDecimal(boundControlCambiado.Text);
            }
        }
    }

    protected void grvMoneda61_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            // Get the OrderTotalLabel Label control in the footer row.
            Label totalACambiar = (Label)e.Row.FindControl("Imp_ACambiarLabel");
            Label totalCambiado = (Label)e.Row.FindControl("Imp_CambiadoLabel");
            Label lblTotal = (Label)e.Row.FindControl("lblTotal");

            // Display the grand total of the order formatted as currency.
            if (totalACambiar != null || totalCambiado != null)
            {
                totalACambiar.Text = orderTotalaCambiar61.ToString();
                totalCambiado.Text = orderTotalCambiado61.ToString();
                lblTotal.Text = "Total";
            }
        }
    }
    protected void grvMoneda62_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // Get the cell that contains the item total.
            TableCell cellACambiar = e.Row.Cells[3];
            TableCell cellCambiado = e.Row.Cells[4];
            // Get the DataBoundLiteralControl control that contains the data-bound value.
            DataBoundLiteralControl boundControlACambiar = (DataBoundLiteralControl)cellACambiar.Controls[0];
            DataBoundLiteralControl boundControlCambiado = (DataBoundLiteralControl)cellCambiado.Controls[0];

            if (((System.Data.DataRowView)(e.Row.DataItem)).Row["Tip_Estado_Actual"].ToString() == "X")
            {
                e.Row.BackColor = System.Drawing.Color.FromArgb(253, 234, 234);
            }
            else
            {            
                // Add the total for an item (row) to the order total.
                orderTotalaCambiar62 += Convert.ToDecimal(boundControlACambiar.Text);
                orderTotalCambiado62 += Convert.ToDecimal(boundControlCambiado.Text);
            }
        }
    }


    protected void grvMoneda62_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            // Get the OrderTotalLabel Label control in the footer row.
            Label totalACambiar = (Label)e.Row.FindControl("Imp_ACambiarLabel");
            Label totalCambiado = (Label)e.Row.FindControl("Imp_CambiadoLabel");
            Label lblTotal = (Label)e.Row.FindControl("lblTotal");

            // Display the grand total of the order formatted as currency.
            if (totalACambiar != null || totalCambiado != null)
            {
                totalACambiar.Text = orderTotalaCambiar62.ToString();
                totalCambiado.Text = orderTotalCambiado62.ToString();
                lblTotal.Text = "Total";
            }
        }
    }




    protected void grvMoneda71_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // Get the cell that contains the item total.
            TableCell cellACambiar = e.Row.Cells[3];
            TableCell cellCambiado = e.Row.Cells[4];
            // Get the DataBoundLiteralControl control that contains the data-bound value.
            DataBoundLiteralControl boundControlACambiar = (DataBoundLiteralControl)cellACambiar.Controls[0];
            DataBoundLiteralControl boundControlCambiado = (DataBoundLiteralControl)cellCambiado.Controls[0];

            if (((System.Data.DataRowView)(e.Row.DataItem)).Row["Tip_Estado_Actual"].ToString() == "X")
            {
                e.Row.BackColor = System.Drawing.Color.FromArgb(253, 234, 234);
            }
            else
            {           
                // Add the total for an item (row) to the order total.
                orderTotalaCambiar71 += Convert.ToDecimal(boundControlACambiar.Text);
                orderTotalCambiado71 += Convert.ToDecimal(boundControlCambiado.Text);
            }
        }
    }


    protected void grvMoneda71_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            // Get the OrderTotalLabel Label control in the footer row.
            Label totalACambiar = (Label)e.Row.FindControl("Imp_ACambiarLabel");
            Label totalCambiado = (Label)e.Row.FindControl("Imp_CambiadoLabel");
            Label lblTotal = (Label)e.Row.FindControl("lblTotal");

            // Display the grand total of the order formatted as currency.
            if (totalACambiar != null || totalCambiado != null)
            {
                totalACambiar.Text = orderTotalaCambiar71.ToString();
                totalCambiado.Text = orderTotalCambiado71.ToString();
                lblTotal.Text = "Total";
            }
        }
    }


    protected void grvMoneda72_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // Get the cell that contains the item total.
            TableCell cellACambiar = e.Row.Cells[3];
            TableCell cellCambiado = e.Row.Cells[4];
            // Get the DataBoundLiteralControl control that contains the data-bound value.
            DataBoundLiteralControl boundControlACambiar = (DataBoundLiteralControl)cellACambiar.Controls[0];
            DataBoundLiteralControl boundControlCambiado = (DataBoundLiteralControl)cellCambiado.Controls[0];

            if (((System.Data.DataRowView)(e.Row.DataItem)).Row["Tip_Estado_Actual"].ToString() == "X")
            {
                e.Row.BackColor = System.Drawing.Color.FromArgb(253, 234, 234);
            }
            else
            {   
                // Add the total for an item (row) to the order total.
                orderTotalaCambiar72 += Convert.ToDecimal(boundControlACambiar.Text);
                orderTotalCambiado72 += Convert.ToDecimal(boundControlCambiado.Text);
            }
        }
    }


    protected void grvMoneda72_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            // Get the OrderTotalLabel Label control in the footer row.
            Label totalACambiar = (Label)e.Row.FindControl("Imp_ACambiarLabel");
            Label totalCambiado = (Label)e.Row.FindControl("Imp_CambiadoLabel");
            Label lblTotal = (Label)e.Row.FindControl("lblTotal");

            // Display the grand total of the order formatted as currency.
            if (totalACambiar != null || totalCambiado != null)
            {
                totalACambiar.Text = orderTotalaCambiar72.ToString();
                totalCambiado.Text = orderTotalCambiado72.ToString();
                lblTotal.Text = "Total";
            }
        }
    }


}
